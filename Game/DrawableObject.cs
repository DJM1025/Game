using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Game
{
    [Serializable]
    abstract class DrawableObject
    {
        public Point Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }

        public Bitmap Image
        {
            get
            {
                lock (_image)
                {
                    return _image;
                }
            }
            set
            {
                lock (_image)
                {
                    _image = value;
                }
            }
        }

        public virtual void Draw(Graphics g)
        {
            g.DrawImage(_image, _location);
        }

        protected Point _location = new Point(0, 0);
        protected Bitmap _image = Properties.Resources.playerFront;
    }
}
