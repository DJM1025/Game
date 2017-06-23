using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;

namespace Game.Sound
{
    static class SoundActionConsumer
    {
        public static void QueueSoundAction(String soundAction)
        {
            _soundActions.Enqueue(soundAction);
        }

        public static void Run()
        {
            Thread t = new Thread(() =>
            {
                while (IsRunning)
                {
                    if (_soundActions.Any())
                    {
                        String result;
                        if (_soundActions.TryDequeue(out result))
                        {
                            SoundInfo.RunAction(result);
                        }
                    }
                    else
                    {
                        Thread.Sleep(50);
                    }
                }
            });
            t.Start();
        }

        private static System.Collections.Concurrent.ConcurrentQueue<String> _soundActions = new System.Collections.Concurrent.ConcurrentQueue<String>();
        public static Boolean IsRunning = false;
    }
}
