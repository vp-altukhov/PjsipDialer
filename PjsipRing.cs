using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PjsipDialer
{
    public class PjsipRing
    {
        private bool stop = false;
        public bool Stop { set { stop = value; } }

        public void Ringing()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.ring);
            while (!stop)
            {
                player.Play();
                Thread.Sleep(3000);
            }
            player.Stop();
        }

        public PjsipRing()
        {
            Thread thread = new Thread(Ringing);
            thread.Start();
        }
    }
}
