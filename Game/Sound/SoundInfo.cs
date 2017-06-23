using System; 
using System.Text; 
using System.Runtime.InteropServices; 
 
namespace Game.Sound
{

    public static class SoundInfo
    {

        [DllImport("winmm.dll")]
        private static extern uint mciSendString(
            string command,
            StringBuilder returnValue,
            int returnLength,
            IntPtr winHandle);

        public static int GetSoundLength(string fileName)
        {
            StringBuilder lengthBuf = new StringBuilder(32);
            string filen = fileName.Replace("mp3", "wav");
            mciSendString(string.Format("open \"{0}\" type waveaudio alias wave", filen), null, 0, IntPtr.Zero);
            mciSendString("status wave length", lengthBuf, lengthBuf.Capacity, IntPtr.Zero);
            mciSendString("close wave", null, 0, IntPtr.Zero);

            int length = 0;
            int.TryParse(lengthBuf.ToString(), out length);

            return length;

        }

        public static void SetVolume(Int32 volume, String alias)
        {
            if (volume < 0)
            {
                volume = 0;
            }
            if (volume > 1000)
            {
                volume = 1000;
            }
            SoundActionConsumer.QueueSoundAction(string.Format("setaudio {0} volume to " + volume.ToString(), alias));
        }

        public static void PlaySound(String fileName, String alias)
        {
            SoundActionConsumer.QueueSoundAction(string.Format(@"open ""{0}"" type mpegvideo alias " + alias, fileName));
            SoundActionConsumer.QueueSoundAction(@"play " + alias);
        }

        public static void RestartSound(String alias)
        {
            SoundActionConsumer.QueueSoundAction(string.Format("seek {0} to start ", alias));
            SoundActionConsumer.QueueSoundAction(@"play " + alias);
        }

        public static void StopSound(String alias)
        {
            SoundActionConsumer.QueueSoundAction(@"stop " + alias);
            SoundActionConsumer.QueueSoundAction(string.Format("seek {0} to start ", alias));
        }

        public static void CloseSound(String alias)
        {
            SoundActionConsumer.QueueSoundAction(@"stop " + alias);
            SoundActionConsumer.QueueSoundAction("close " + alias);
        }

        public static void RunAction(String action)
        {
            uint error = mciSendString(action, null, 0, IntPtr.Zero);
        }
    }
}