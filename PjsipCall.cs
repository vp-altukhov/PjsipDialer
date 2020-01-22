using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PjsipDialer
{
    public delegate void CallDisconnected(PjsipCall call);
    public delegate void CallIncoming(string remoteUri);

    public class PjsipCall : Call
    {
        private bool isOnLine = false;
        public bool IsOnline { get { return isOnLine; } }

        private CallDisconnected onCallDisconnected;
        public CallDisconnected OnCallDisconnected { set { onCallDisconnected = value; } }

        private CallIncoming onCallIncoming;
        public CallIncoming OnCallIncoming { set { onCallIncoming = value; } }

        public PjsipCall(PjsipAccount acc, int call_id = -1)
            : base(acc, call_id)
        {
        }

        /// <summary>
        /// Обрабатываем событие изменения статуса звонка
        /// </summary>
        /// <param name="prm"></param>
        public override void onCallState(OnCallStateParam prm)
        {
            CallInfo ci = getInfo();
            isOnLine = isActive();

            if (ci.state == pjsip_inv_state.PJSIP_INV_STATE_INCOMING)
            {
                if (onCallIncoming != null) onCallIncoming(ci.remoteUri);
            }

            if (ci.state == pjsip_inv_state.PJSIP_INV_STATE_DISCONNECTED)
            {
                if (onCallDisconnected != null) onCallDisconnected(this);
            }
        }

        /// <summary>
        /// Обрабатываем событие изменения статуса медиа
        /// </summary>
        /// <param name="prm"></param>
        public override void onCallMediaState(OnCallMediaStateParam prm)
        {
            CallInfo ci = getInfo();
            AudioMedia aud_med;

            try
            {
                aud_med = getAudioMedia(-1);
            }
            catch { return; }

            AudDevManager mgr = Endpoint.instance().audDevManager();

            AudioMedia play_dev_med = mgr.getPlaybackDevMedia();
            aud_med.startTransmit(play_dev_med);

            AudioMedia cap_aud_med = mgr.getCaptureDevMedia();
            cap_aud_med.startTransmit(aud_med);
        }
    }
}
