using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Scripting
{
    [Serializable]
    class PlayerLightScript : BaseScript
    {
        public PlayerLightScript(Int32 radius)
        {
            Name = "Light";
            PlayerTriggerable = false;
            IsInterruptable = false;
            _radius = radius;
        }

        public override void ScriptLogic()
        {
            if (!_isRunning)
            {
                _isRunning = true;
                _actionObject = ScriptDataBridge.Player.CurrentTile;
                if (GlobalLighting.Instance.Brightness <= -100)
                {
                    FlickerLights(_radius, 50);
                }
                ScriptDataBridge.Player.PlayerMoved += Player_PlayerMoved;
            }
        }

        private void Player_PlayerMoved(object sender, EventArgs e)
        {
            StopFlickering();
            if (GlobalLighting.Instance.Brightness <= -100)
            {
                lock (_actionObject)
                {
                    _actionObject = ScriptDataBridge.Player.CurrentTile;
                }
                FlickerLights(_radius, 50);
            }
        }

        protected override void interrupt()
        {
            throw new NotImplementedException();
        }

        protected override void Stop()
        {
            StopFlickering();
            ScriptDataBridge.Player.PlayerMoved -= Player_PlayerMoved;
        }

        private static Int32 _radius;

        [NonSerialized]
        private static Boolean _isRunning = false;
    }
}
