using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    [Serializable]
    class Action
    {
        public Action()
        {
            _playerAnimation = new Animation();
            _playerAnimation.Load("playerMakeFire.bin");
            _objectAnimation = new Animation();
            _objectAnimation.Load("campFire.bin");
            Name = "Light a Fire";
        }

        public String toString()
        {
            return Name;
        }

        public Animation PlayerAnimation
        {
            get
            {
                return _playerAnimation;
            }
            set
            {
                _playerAnimation = value;
            }
        }

        public Animation ObjectAnimation
        {
            get
            {
                return _objectAnimation;
            }
            set
            {
                _objectAnimation = value;
            }
        }

        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public Int32 Range
        {
            get
            {
                return _range;
            }
            set
            {
                _range = value;
            }
        }

        private Int32 _range = 1;
        private Animation _playerAnimation;
        private Animation _objectAnimation;
        private String _name;
    }
}
