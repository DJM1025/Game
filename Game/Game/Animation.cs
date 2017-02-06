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
        }

        public void Save()
        {

        }

        public void Load(String AnimationFilePath)
        {

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
                    return _images[_currentIndex++];
                }
                else
                {
                    return _defaultImage;
                }
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

        private Boolean _animating = false;
        private Int32 _currentIndex = 0;
        private List<Bitmap> _images = new List<Bitmap>();
        private Bitmap _defaultImage = Properties.Resources.grass;
    }
}
