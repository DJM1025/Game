using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Scripting
{
    [Serializable]
    class TestScript : BaseScript
    {
        public TestScript()
        {
            this.Name = "Light Fire";
            this.IsInterruptable = false;
            this.PlayerTriggerable = true;
        }

        public override void ScriptLogic()
        {
            if (ActionObjectInPlayerRange(0))
            {
                ChopTreeScript._woodCount--;
                SetPlayerAnimation(LoadAnimation(animPath + "playerMakeFire.bin"));
                PlaySoundOnLoop(soundPath + "lighter.wav");
                SetPlayerAnimating(true);
                PauseScript(1000);
                if (IsTileWalkable(0, -1))
                {
                    ForceMovePlayer(0, -1);
                }
                else if (IsTileWalkable(0, 1))
                {
                    ForceMovePlayer(0, 1);
                }
                else if (IsTileWalkable(-1, 0))
                {
                    ForceMovePlayer(-1, 0);
                }
                else if (IsTileWalkable(1, 0))
                {
                    ForceMovePlayer(1, 0);
                }
                FlickerLights(5, 100);
                SetActionObjectCollision(true);
                SetActionObjectAnimation(LoadAnimation(animPath + "campFire.bin"), 60000);
                PlaySoundOnLoop(soundPath + "fire.mp3");

                ScriptDataBridge.Player.PlayerMoved += Player_PlayerMoved;
                SetActionObjectAnimating(true);
                SetPlayerAnimating(false);
                OnScriptComplete();
                PauseScript(60100);
                StopPlayingSound();
                SetActionObjectCollision(false);
                SetActionObjectAnimating(false);
                SetActionObjectImage(Properties.Resources.stone);
            }
            StopFlickering();
            ScriptDataBridge.Player.PlayerMoved -= Player_PlayerMoved;
            OnScriptComplete();
        }

        private void Player_PlayerMoved(object sender, EventArgs e)
        {
            Int32 volume = -(Int32)(GetPlayerRangeFromActionObject() * 250) + 1;
            SetSoundVolume(volume);
        }

        protected override void interrupt()
        {
        }

        protected override void Stop()
        {
            ScriptDataBridge.Player.PlayerMoved -= Player_PlayerMoved;
            StopFlickering();
            StopPlayingSound();
            SetActionObjectCollision(false);
            SetActionObjectAnimating(false);
            OnScriptComplete();
        }
    }
}
