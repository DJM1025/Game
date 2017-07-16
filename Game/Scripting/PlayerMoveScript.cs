using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Scripting
{
    [Serializable]
    class PlayerMoveScript : BaseScript
    {
        public PlayerMoveScript()
        {
            this.Name = "Walk Here";
            this.IsInterruptable = true;
            this.PlayerTriggerable = true;
            LoadSound(soundPath + "walking.mp3");
        }

        public override void ScriptLogic()
        {
            if (_soundId == 0)
            {
                LoadSound(soundPath + "walking.mp3");
            }
            if (_walkDownAnimation == null)
            {
                _walkDownAnimation = LoadAnimation(animPath + "playerFront.bin");
                _walkUpAnimation = LoadAnimation(animPath + "playerBack.bin");
                _walkLeftAnimation = LoadAnimation(animPath + "playerLeft.bin");
                _walkRightAnimation = LoadAnimation(animPath + "playerRight.bin");
            }
            if (!this.ActionObjectInPlayerRange(0))
            {
                SetPlayerAnimation(_walkDownAnimation);
                SetPlayerAnimating(true);
                WalkPlayerToActionTile();
            }
            OnScriptComplete();
        }


        protected override void interrupt()
        {
            SetPlayerAnimation(_walkDownAnimation);
            SetPlayerAnimating(false);
            StopPlayingSound();
            OnScriptComplete();
        }

        /// <summary>
        /// This is not nice code and I know that but im tired and don't want to re-write it to be nice.
        /// </summary>
        public void WalkPlayerToActionTile()
        {
            var player = _dataBridge.Player;
            var clickedTile = _dataBridge.Map.GetClickedTile(_actionObject.Location.X, _actionObject.Location.Y);
            var path = this._dataBridge.Map.GetPath(_dataBridge.Player.CurrentTile, clickedTile);
            RestartSoundOnLoop();
            for (int i = 0; i < path.Count; i++)
            {
                if (!player.Animating)
                {
                    StopPlayingSound();
                    return;
                }
                if (player.CurrentTile.MapRow == clickedTile.MapRow && player.CurrentTile.MapCol == clickedTile.MapCol)
                {
                    _dataBridge.Map.CenterOnPlayer(player.Speed, player);
                    return;
                }
                if (path[i] == 1)
                {
                    player.CurrentTile = _dataBridge.Map.Tiles[player.CurrentTile.MapRow - 1][player.CurrentTile.MapCol];
                    player.Animation = _walkUpAnimation;
                    player.Animating = true;
                }
                else if (path[i] == 2)
                {
                    player.CurrentTile = _dataBridge.Map.Tiles[player.CurrentTile.MapRow][player.CurrentTile.MapCol + 1];
                    player.Animation = _walkRightAnimation;
                    player.Animating = true;
                }
                else if (path[i] == 3)
                {
                    player.CurrentTile = _dataBridge.Map.Tiles[player.CurrentTile.MapRow + 1][player.CurrentTile.MapCol];
                    player.Animation = _walkDownAnimation;;
                    player.Animating = true;
                }
                else if (path[i] == 4)
                {
                    player.CurrentTile = _dataBridge.Map.Tiles[player.CurrentTile.MapRow][player.CurrentTile.MapCol - 1];
                    player.Animation = _walkLeftAnimation;
                    player.Animating = true;
                }
                _dataBridge.Map.CenterOnPlayer(player.Speed, player);
                while (player.Location != player.CurrentTile.Location && player.Animating)
                {
                    if (Math.Abs(player.Location.X - player.CurrentTile.Location.X) <= player.Speed)
                    {
                        if (Math.Abs(player.Location.Y - player.CurrentTile.Location.Y) <= player.Speed)
                        {
                            //StopPlayingSound();
                            break;
                        }
                    }
                };
            }
            player.Animating = false;
            StopPlayingSound();
        }

        private Boolean _playerWalking = false;

        private static Animation _walkDownAnimation;
        private static Animation _walkLeftAnimation;
        private static Animation _walkRightAnimation;
        private static Animation _walkUpAnimation;
    }
}
