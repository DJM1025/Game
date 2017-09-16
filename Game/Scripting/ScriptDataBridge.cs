using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Game.Scripting
{
    [Serializable]
    public class ScriptDataBridge
    {

        private ScriptDataBridge()
        {
        }

        public static ScriptDataBridge GetInstance(BaseScript key)
        {
            if (!_instances.ContainsKey(key))
            {
                _instances.Add(key, new ScriptDataBridge());
            }
            return _instances[key];
        }

        public static Map Map
        {
            get
            {
                if (_map == null)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                return _map;
            }
            set
            {
                _map = value;
            }
        }

        public static Player Player
        {
            get
            {
                return _player;
            }
            set
            {
                _player = value;
                DataLoaded.Set();
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
        public static EventWaitHandle DataLoaded = new EventWaitHandle(false, EventResetMode.ManualReset);

        [NonSerialized]
        private InteractableObject _actionObject;
        [NonSerialized]
        private static Player _player;
        private static Map _map;
        [NonSerialized]
        private static Dictionary<BaseScript, ScriptDataBridge> _instances = new Dictionary<BaseScript, ScriptDataBridge>();
    }
}
