using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using Game.Sound;

namespace Game.Scripting
{
    [Serializable]
    public abstract class BaseScript
    {

        abstract public void ScriptLogic();

        abstract protected void interrupt();

        public void RunScript()
        {
            _actionObject = _dataBridge.ActionObject;
            Thread t = new Thread(new ThreadStart(ScriptLogic));
            t.Start();
        }

        public void SetSoundVolume(Int32 volume)
        {
            SoundHandler.SetVolume(volume, _soundId);
        }

        public void PlaySound(String soundPath)
        {
            if (_soundId > 0)
            {
                SoundHandler.UnLoadSound(_soundId);
            }
            _soundId = SoundHandler.PlaySound(soundPath, false);
        }

        public void PlaySoundOnLoop(String soundPath)
        {
            if (_soundId > 0)
            {
                SoundHandler.UnLoadSound(_soundId);
            }
            _soundId = SoundHandler.PlaySound(soundPath, true);
        }

        public void RestartSound()
        {
            if (_soundId > 0)
            {
                SoundHandler.RestartSound(_soundId, false);
            }
        }

        public void RestartSoundOnLoop()
        {
            if (_soundId > 0)
            {
                SoundHandler.RestartSound(_soundId, true);
            }
        }

        public void LoadSound(String soundPath)
        {
            if (_soundId > 0)
            {
                SoundHandler.UnLoadSound(_soundId);
            }
            _soundId = SoundHandler.PlaySound(soundPath, false);
            SoundHandler.StopPlayingSound(_soundId);
        }

        public void StopPlayingSound()
        {
            SoundHandler.StopPlayingSound(_soundId);
        }

        public void ForceMovePlayer(Int32 tilesDown, Int32 tilesRight)
        {
            var targetX = _dataBridge.Player.Location.X + (_dataBridge.Map.TileSize * tilesRight);
            var targetY = _dataBridge.Player.Location.Y + (_dataBridge.Map.TileSize * tilesDown);
            _dataBridge.Player.MoveToLocation(targetX, targetY, _dataBridge.Map);
        }

        public Boolean IsTileWalkable(Int32 tilesDown, Int32 tilesRight)
        {
            var targetX = _dataBridge.Player.Location.X + (_dataBridge.Map.TileSize * tilesRight);
            var targetY = _dataBridge.Player.Location.Y + (_dataBridge.Map.TileSize * tilesDown);
            if (_dataBridge.Map.GetClickedTile(targetX, targetY).Collide)
            {
                return false;
            }
            return true;
        }

        public void SetPlayerAnimation(Animation a, Int32 timeInMs)
        {
            var tempAnimating = _dataBridge.Player.Animating;
            var temp = _dataBridge.Player.Animation;
            SetPlayerAnimation(a);
            System.Timers.Timer t = new System.Timers.Timer(timeInMs);
            t.Elapsed += (sender, e) =>
            {
                SetPlayerAnimation(temp);
                _dataBridge.Player.Animating = tempAnimating;
            };
            t.AutoReset = false;
            t.Start();
            
        }

        public void SetPlayerAnimation(Animation a)
        {
            _dataBridge.Player.Animation = a;
        }

        public void SetPlayerAnimating(Boolean animating)
        {
            _dataBridge.Player.Animating = animating;
        }

        public void SetActionObjectAnimation(Animation a, Int32 timeInMs)
        {
            var tempAnimating = _actionObject.Animating;
            var temp = _actionObject.Animation;
            SetActionObjectAnimation(a);
            System.Timers.Timer t = new System.Timers.Timer(timeInMs);
            t.Elapsed += (sender, e) =>
            {
                SetActionObjectAnimation(temp);
                _actionObject.Animating = tempAnimating;
            };
            t.AutoReset = false;
            t.Start();

        }

        public void SetActionObjectAnimating(Boolean animating)
        {
            _actionObject.Animating = animating;
        }

        public void SetActionObjectAnimation(Animation a)
        {
            _actionObject.Animation = a;
        }

        public void SetActionObjectImage(Bitmap image)
        {
            _actionObject.Image = image;
        }

        public void SetActionObjectCollision(Boolean enabled)
        {
            _actionObject.Collide = enabled;
        }

        public void PauseScript(Int32 timeInMs)
        {
            System.Threading.Thread.Sleep(timeInMs);
        }

        public Animation LoadAnimation(String animationName)
        {
            Animation a = new Animation();
            a.Load(animationName);
            return a;
        }

        public Boolean ActionObjectInPlayerRange(Int32 tileRange)
        {
            var actionObjectTile = _dataBridge.Map.GetClickedTile(_actionObject.Location.X, _actionObject.Location.Y);
            var playerTile = _dataBridge.Player.CurrentTile;
            var deltaX = playerTile.MapRow - actionObjectTile.MapRow;
            var deltaY = playerTile.MapCol - actionObjectTile.MapCol;
            var distance = Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
            return distance <= tileRange;
        }

        public Double GetPlayerRangeFromActionObject()
        {
            var actionObjectTile = _dataBridge.Map.GetClickedTile(_actionObject.Location.X, _actionObject.Location.Y);
            var playerTile = _dataBridge.Player.CurrentTile;
            var deltaX = playerTile.MapRow - actionObjectTile.MapRow;
            var deltaY = playerTile.MapCol - actionObjectTile.MapCol;
            return Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
        }

        public void InterruptScript()
        {
            if (_isInterruptable)
            {
                interrupt();
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

        public Boolean IsInterruptable
        {
            get
            {
                return _isInterruptable;
            }
            set
            {
                _isInterruptable = value;
            }
        }

        public Boolean PlayerTriggerable
        {
            get
            {
                return _playerTriggerable;
            }
            set
            {
                _playerTriggerable = value;
            }
        }

        protected InteractableObject _actionObject;

        protected void OnScriptComplete()
        {
            ScriptComplete?.Invoke(this, new EventArgs());
        }

        public event EventHandler ScriptComplete;

        private Boolean _playerTriggerable = true;
        private Boolean _isInterruptable = false;
        private String _name = String.Empty;
        protected ScriptDataBridge _dataBridge = ScriptDataBridge.GetInstance();

        [NonSerialized]
        private Int32 _soundId = 0;
    }
}
