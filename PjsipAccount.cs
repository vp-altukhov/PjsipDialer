using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PjsipDialer
{
    public delegate void AccountRegState(PjsipAccount acc);
    public delegate void AccountIncomingCall(PjsipAccount acc, PjsipCall call, int callId);

    public class PjsipAccount : Account
    {
        /// <summary>
        /// Состояние аккаунта
        /// </summary>
        public bool IsActive { get { return isActive; } }
        private bool isActive = false;

        /// <summary>
        /// Код состояния аккаунта
        /// </summary>
        public pjsip_status_code Code { get { return code; } }
        private pjsip_status_code code = pjsip_status_code.PJSIP_SC_NULL;

        /// <summary>
        /// Адрес сервера
        /// </summary>
        [XmlElement]
        public string Host;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [XmlElement]
        public string UserName;

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [XmlElement]
        public string Password;

        /// <summary>
        /// Callback по изменению статуса регистрации
        /// </summary>
        public AccountRegState OnAccountRegState { set { onAccountRegState = value; } }
        private AccountRegState onAccountRegState;

        /// <summary>
        /// Callback по входящему вызову
        /// </summary>
        public AccountIncomingCall OnAccountIncomingCall { set { onAccountIncomingCall = value; } }
        private AccountIncomingCall onAccountIncomingCall;

        /// <summary>
        /// Конфигурация аккаунта
        /// </summary>
        private AccountConfig config;
        /// <summary>
        /// Создание и модификация аккаунта
        /// </summary>
        /// <param name="host">Хост сервера</param>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        public void SetConfig(string host, string userName, string password)
        {
            this.Host = host;
            this.UserName = userName;
            this.Password = password;
            SetConfig();
        }

        /// <summary>
        /// Конфигурация аккаунта
        /// </summary>
        public void SetConfig()
        {
            bool newAcc = config == null;
            config = new AccountConfig();

            config.idUri = string.Format("sip:{0}@{1}", UserName, Host);
            config.regConfig.registrarUri = string.Format("sip:{0}", Host);
            config.sipConfig.authCreds.Add(new AuthCredInfo("digest", "*", UserName, 0, Password));

            if (newAcc) create(config);
            else modify(config);
        }

        public override string ToString()
        {
            if (config == null) return "Не инициализирован";
            return string.Format("{0} - {1}", config.idUri, isActive ? "Активный" : "Не активный");
        }

        public PjsipAccount()
        {
            Host = "";
            UserName = "";
            Password = "";
        }

        /// <summary>
        /// Получение статуса аккаунта
        /// </summary>
        /// <returns></returns>
        public bool GetOnlineStatus()
        {
            AccountInfo ai = getInfo();
            return ai.onlineStatus;
        }

        public override void onIncomingCall(OnIncomingCallParam prm)
        {
            CallOpParam cprm = new CallOpParam();
            cprm.statusCode = pjsip_status_code.PJSIP_SC_RINGING;
            PjsipCall call = new PjsipCall(this, prm.callId);
            call.answer(cprm);

            if (onAccountIncomingCall != null) onAccountIncomingCall(this, call, prm.callId);
        }

        /// <summary>
        /// Обрабатываем событие изменение статуса регистрации
        /// </summary>
        /// <param name="prm"></param>
        override public void onRegState(OnRegStateParam prm)
        {
            AccountInfo ai = getInfo();
            isActive = ai.regIsActive;
            code = prm.code;
            if (onAccountRegState != null) onAccountRegState(this);
        }
    }
}
