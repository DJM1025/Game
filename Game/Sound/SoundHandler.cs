using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Sound
{
    public static class SoundHandler
    {

        public static Int32 PlaySound(String soundPath, Boolean loop)
        {
            Int32 tempSoundId = ++_currentSoundId;
            SoundHelper player = new SoundHelper(soundPath, tempSoundId);
            _soundFiles.Add(tempSoundId, player);
            RestartSound(tempSoundId, loop);
            return tempSoundId;
        }

        public static void RestartSound(Int32 soundId, Boolean loop)
        {
            if (loop)
            {
                _soundFiles[soundId].PlayLooping();
            }
            else
            {
                _soundFiles[soundId].Play();
            }       
        }

        public static void SetVolume(Int32 volume, Int32 soundId)
        {
            if (volume > 0)
            {
                volume = 0;
            }
            if (volume < -10000)
            {
                volume = -10000;
            }
            _soundFiles[soundId].SetVolume(volume);
        }

        public static void StopPlayingSound(Int32 soundId)
        {
            _soundFiles[soundId].Stop();
        }

        public static void UnLoadSound(Int32 soundId)
        {
            _soundFiles[soundId].Close();
            _soundFiles.Remove(soundId);
        }

        public static void UnloadAllSounds()
        {
            foreach (var sound in _soundFiles.Keys)
            {
                _soundFiles[sound].Close();
            }
        }

        private static Dictionary<Int32, SoundHelper> _soundFiles = new Dictionary<Int32, SoundHelper>();
        private static Int32 _currentSoundId = 0;
    }
}
