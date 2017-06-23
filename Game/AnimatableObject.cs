using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Game
{
    [Serializable]
    public class AnimatableObject : CollidableObject
    {
        public AnimatableObject(Animation animation)
        {
            _animation = animation;
        }

        public AnimatableObject()
        {

        }

        public void Animate()
        {
            if (_animation == null)
            {
                _animation = new Animation();
            }
            _image = _animation.Image;
        }

        public Animation Animation
        {
            get
            {
                return _animation;
            }
            set
            {
                _animation = value;
            }
        }

        public Boolean Animating
        {
            get
            {
                return _animation.Animating;
            }
            set
            {
                _animation.Animating = value;
            }
        }

        public new Bitmap Image
        {
            get
            {
                return _image;
            }
            set
            {
                Animation.DefaultImage = value;
                _image = value;
            }
        }

        private Animation _animation = new Animation();
    }
}
