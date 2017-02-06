using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Game
{
    [Serializable]
    public class Animation
    {

        public Animation()
        {
            _timer = new System.Threading.Timer(new System.Threading.TimerCallback(updateImage), null, 0, _animationDelayMs);
        }

        [System.Runtime.Serialization.OnDeserializedAttribute()]
        private void RunThisMethod(System.Runtime.Serialization.StreamingContext context)
        {
            if (_images.Count > 0)
            {
                _timer = new System.Threading.Timer(new System.Threading.TimerCallback(updateImage), null, 0, _animationDelayMs);
            }
        }

        public void Save()
        {

        }

        public void Load(String AnimationFilePath)
        {
            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.Stream stream = new System.IO.FileStream(AnimationFilePath,
                                      System.IO.FileMode.Open,
                                      System.IO.FileAccess.Read,
                                      System.IO.FileShare.Read);
            Animation obj = (Animation)formatter.Deserialize(stream);
            stream.Close();
            _currentIndex = obj._currentIndex;
            _defaultImage = obj._defaultImage;
            _images = obj.Images;
            _animating = obj.Animating;
            _animationDelayMs = obj.AnimationDelayMs;
            _timer = new System.Threading.Timer(new System.Threading.TimerCallback(updateImage), null, 0, obj.AnimationDelayMs);
        }

        private void updateImage(object target)
        {
            _currentIndex++;
        }

        public List<Bitmap> Images
        {
            get
            {
                return _images;
            }
            set
            {
                _images = value;
            }
        }

        public Bitmap Image
        {
            get
            {
                if (_images != null && Animating && _images.Count > 0)
                {
                    _currentIndex = _currentIndex % _images.Count;
                    return _images[_currentIndex];
                }
                else
                {
                    return _defaultImage;
                }
            }
        }

        public System.Threading.Timer Timer
        {
            get
            {
                return _timer;
            }
            set
            {
                _timer = value;
            }
        }

        public Bitmap DefaultImage
        {
            get
            {
                return _defaultImage;
            }
            set
            {
                _defaultImage = value;
            }
        }

        public Boolean Animating
        {
            get
            {
                return _animating;
            }
            set
            {
                _animating = value;
            }
        }

        public Int32 AnimationDelayMs
        {
            get
            {
                return _animationDelayMs;
            }
            set
            {
                _animationDelayMs = value;
                _timer.Change(0, _animationDelayMs);
            }
        }

        private Boolean _animating = false;
        private Int32 _currentIndex = 0;
        private List<Bitmap> _images = new List<Bitmap>();
        private Bitmap _defaultImage = Properties.Resources.grass;
        private Int32 _animationDelayMs = 0;
        [NonSerialized]
        private System.Threading.Timer _timer;
    }
}
