using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    [Serializable]
    class CollidableObject : DrawableObject
    {
        public Boolean Collide
        {
            get
            {
                return _collide;
            }
            set
            {
                _collide = value;
            }
        }

        private Boolean _collide = false;
    }
}
