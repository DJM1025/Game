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
                SetPlayerAnimation(LoadAnimation(@"C:\Users\Dan\Desktop\GameGit\Game\Game\Animations\playerMakeFire.bin"));
                PlaySoundOnLoop(@"C:\Users\Dan\Desktop\GameGit\Game\Game\sounds\lighter.wav");
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
                SetActionObjectCollision(true);
                SetActionObjectAnimation(LoadAnimation(@"C:\Users\Dan\Desktop\GameGit\Game\Game\Animations\campFire.bin"), 10000);
                PlaySoundOnLoop(@"C:\Users\Dan\Desktop\GameGit\Game\Game\Sounds\fire.mp3");

                this._dataBridge.Player.PlayerMoved += Player_PlayerMoved;
                SetActionObjectAnimating(true);
                SetPlayerAnimating(false);
                OnScriptComplete();
                PauseScript(10100);
                StopPlayingSound();
                SetActionObjectCollision(false);
                SetActionObjectAnimating(false);
                SetActionObjectImage(Properties.Resources.stone);
            }
            this._dataBridge.Player.PlayerMoved -= Player_PlayerMoved;
            OnScriptComplete();
        }

        private void Player_PlayerMoved(object sender, EventArgs e)
        {
            Int32 volume = -(Int32)(GetPlayerRangeFromActionObject() * 250) + 1;
            SetSoundVolume(volume);
        }

        private Boolean _playing = true;

        protected override void interrupt()
        {
            throw new NotImplementedException();
        }
    }
}
