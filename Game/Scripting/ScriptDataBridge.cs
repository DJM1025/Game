using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Scripting
{
    [Serializable]
    public class ScriptDataBridge
    {

        private ScriptDataBridge()
        {
        }

        public static ScriptDataBridge GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ScriptDataBridge();
            }
            return _instance;
        }

        public Map Map
        {
            get
            {
                return _map;
            }
            set
            {
                _map = value;
            }
        }

        public Player Player
        {
            get
            {
                return _player;
            }
            set
            {
                _player = value;
            }
        }

        public InteractableObject ActionObject
        {
            get
            {
                return _actionObject;
            }
            set
            {
                _actionObject = value;
            }
        }
        [NonSerialized]
        private InteractableObject _actionObject;
        [NonSerialized]
        private Player _player;
        private Map _map;
        [NonSerialized]
        private static ScriptDataBridge _instance;
    }
}
