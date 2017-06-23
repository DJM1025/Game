using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Text;
using System.Timers;
using Microsoft.DirectX.AudioVideoPlayback;

namespace Game.Sound
{
    public class SoundHelper
    {
        public SoundHelper(String soundPath, Int32 soundId)
        {
            _source = soundPath;
            _timer = new Timer();
            _audio = new Audio(soundPath);
            _alias = soundId.ToString();
        }

        public void SetVolume(Int32 volume)
        {
            lock (_audio)
            {
                _audio.Volume = volume;
            }
            //SoundInfo.SetVolume(volume, _alias);
        }

        public void Play()
        {
            lock (_audio)
            {
                _audio.Play();
            }
            //SoundInfo.PlaySound(_source, _alias);
        }

        public void PlayLooping()
        {
            lock (_audio)
            {
                _looping = true;
                _timer.Elapsed -= loop;
                _timer.Elapsed += loop;
                //_audio.Ending += loop;
                _timer.Interval = _audio.Duration * 1000;
                //SoundInfo.PlaySound(_source, _alias);
                _audio.Play();
                _timer.Start();
            }
        }

        public void Stop()
        {
            lock (_audio)
            {
                _looping = false;
                if (_audio != null && !_audio.Disposed)
                {
                    _audio.Stop();
                }
            }
            //SoundInfo.StopSound(_alias);
        }

        public void Close()
        {
            lock (_audio)
            {
                _audio.Stop();
                _looping = false;
                _audio.Dispose();
            }
            //SoundInfo.CloseSound(_alias);
        }

        private void loop(object sender, EventArgs e)
        {
            if (_looping)
            {
                lock (_audio)
                {
                    _audio.CurrentPosition = 0;
                }
                //SoundInfo.RestartSound(_alias);
            }
            else
            {
                try
                {
                    lock (_audio)
                    {
                        _timer.Stop();
                        //_audio.Ending -= loop;
                        if (!_audio.Disposed && _audio.State != StateFlags.Stopped)
                        {
                            _audio.Stop();
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("error");
                }
                //SoundInfo.StopSound(_alias);
            }
        }


        private Boolean _looping = false;
        private Timer _timer;
        private String _source;
        private String _alias;

        private Audio _audio;
    }
}
