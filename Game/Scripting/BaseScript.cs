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
        [NonSerialized]
        private Thread t;

        public void RunScript()
        {
            if (!MasterRunningScriptList.Closing)
            {
                _dataBridge = ScriptDataBridge.GetInstance(this);
                _actionObject = _dataBridge.ActionObject;
                MasterRunningScriptList.AddScript(this);
                t = new Thread(new ThreadStart(threadProc));
                t.Start();
            }
        }

        private void threadProc()
        {
            if (ScriptDataBridge.Player == null)
            {
                ScriptDataBridge.DataLoaded.WaitOne();
            }
            ScriptLogic();
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
            if (_soundId > 0)
            {
                SoundHandler.StopPlayingSound(_soundId);
            }
        }

        public void ForceMovePlayer(Int32 tilesDown, Int32 tilesRight)
        {
            var targetX = ScriptDataBridge.Player.Location.X + (ScriptDataBridge.Map.TileSize * tilesRight);
            var targetY = ScriptDataBridge.Player.Location.Y + (ScriptDataBridge.Map.TileSize * tilesDown);
            ScriptDataBridge.Player.MoveToLocation(targetX, targetY, ScriptDataBridge.Map);
        }

        public Boolean IsTileWalkable(Int32 tilesDown, Int32 tilesRight)
        {
            var targetX = ScriptDataBridge.Player.Location.X + (ScriptDataBridge.Map.TileSize * tilesRight);
            var targetY = ScriptDataBridge.Player.Location.Y + (ScriptDataBridge.Map.TileSize * tilesDown);
            if (ScriptDataBridge.Map.GetClickedTile(targetX, targetY).Collide)
            {
                return false;
            }
            return true;
        }

        public void SetPlayerAnimation(Animation a, Int32 timeInMs)
        {
            var tempAnimating = ScriptDataBridge.Player.Animating;
            var temp = ScriptDataBridge.Player.Animation;
            SetPlayerAnimation(a);
            System.Timers.Timer t = new System.Timers.Timer(timeInMs);
            t.Elapsed += (sender, e) =>
            {
                SetPlayerAnimation(temp);
                ScriptDataBridge.Player.Animating = tempAnimating;
            };
            t.AutoReset = false;
            t.Start();
            
        }

        public void SetPlayerAnimation(Animation a)
        {
            ScriptDataBridge.Player.Animation = a;
        }

        public void SetPlayerAnimating(Boolean animating)
        {
            ScriptDataBridge.Player.Animating = animating;
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
            var actionObjectTile = ScriptDataBridge.Map.GetClickedTile(_actionObject.Location.X, _actionObject.Location.Y);
            var playerTile = ScriptDataBridge.Player.CurrentTile;
            var deltaX = playerTile.MapRow - actionObjectTile.MapRow;
            var deltaY = playerTile.MapCol - actionObjectTile.MapCol;
            var distance = Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
            return distance <= tileRange;
        }

        public Double GetPlayerRangeFromActionObject()
        {
            var actionObjectTile = ScriptDataBridge.Map.GetClickedTile(_actionObject.Location.X, _actionObject.Location.Y);
            var playerTile = ScriptDataBridge.Player.CurrentTile;
            var deltaX = playerTile.MapRow - actionObjectTile.MapRow;
            var deltaY = playerTile.MapCol - actionObjectTile.MapCol;
            return Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
        }

        public void SetLightRadius(Int32 radius, Int32 brightness)
        {
            var step = brightness / radius;
            _brightnesses = new List<List<Int32>>();
            for (int i = -radius; i < radius; i++)
            {
                _brightnesses.Add(new List<Int32>());
                for (int j = -radius; j < radius; j++)
                {
                    //var X = _dataBridge.Map.findClickedColIndex(_actionObject.Location.X);
                    //var Y = _dataBridge.Map.findClickedRowIndex(_actionObject.Location.Y); 
                    var distance = Math.Sqrt((i * i) + (j * j));
                    if (distance <= radius)//&& shouldLight(X + i, Y + j, X, Y))
                    {
                        _brightnesses[i + radius].Add((Int32)((radius - distance) * step));
                        ScriptDataBridge.Map.Tiles[Y + i][X + j].Brightness += _brightnesses[i + radius][j + radius];
                    }
                    else
                    {
                        _brightnesses[i + radius].Add(0);
                    }
                }
            }
        }

        public void RemoveLightRadius(Int32 radius)
        {
            for (int i = -radius; i < radius; i++)
            {
                for (int j = -radius; j < radius; j++)
                {
                    //var X = _dataBridge.Map.findClickedColIndex(_actionObject.Location.X);
                    //var Y = _dataBridge.Map.findClickedRowIndex(_actionObject.Location.Y);
                    ScriptDataBridge.Map.Tiles[Y + i][X + j].Brightness -= _brightnesses[i + radius][j + radius];
                }
            }
        }

        public void FlickerLights(Int32 radius, Int32 brightness)
        {
            Thread thread = new System.Threading.Thread(() =>
            {
                _flickering = true;
                while (_flickering)
                {
                    lock (_actionObject)
                    {
                        X = ScriptDataBridge.Map.findClickedColIndex(_actionObject.Location.X);
                        Y = ScriptDataBridge.Map.findClickedRowIndex(_actionObject.Location.Y);
                        var light = RandomHelper.Next(brightness / 3) + ((brightness * 2) / 3);
                        lock (GlobalLighting.Instance.Matrix)
                        {
                            SetLightRadius(radius, light);
                        }
                        System.Threading.Thread.Sleep(100);
                        lock (GlobalLighting.Instance.Matrix)
                        {
                            RemoveLightRadius(radius);
                        }
                    }
                }
            });
            thread.Start();
        }

        public void StopFlickering()
        {
            _flickering = false;
        }

        [NonSerialized]
        private Int32 X = 0;
        private Int32 Y = 0;
        private bool _flickering = true;
        List<List<Int32>> _brightnesses = new List<List<int>>();

        public void InterruptScript()
        {
            if (_isInterruptable)
            {
                interrupt();
                MasterRunningScriptList.Remove(this);
            }
        }

        protected abstract void Stop();

        public void StopScript()
        {
            Stop();
            if (t.IsAlive)
            {
                t.Abort();
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
        protected ScriptDataBridge _dataBridge;// = ScriptDataBridge.GetInstance(this);
        public static readonly string animPath = System.IO.Directory.GetCurrentDirectory() + @"\..\..\Animations\";
        public static readonly string soundPath = System.IO.Directory.GetCurrentDirectory() + @"\..\..\Sounds\";

        [NonSerialized]
        protected Int32 _soundId = 0;
        public static Boolean AllScriptsEnding = false;
    }
}


/*
private Boolean shouldLight(Int32 mapX, Int32 mapY, Int32 actionX, Int32 actionY)
{
    var mapTile = _dataBridge.Map.Tiles[mapY][mapX];
    var actionTile = _dataBridge.Map.Tiles[actionY][actionX];

    Int32 mapCenterX = mapTile.Location.X + (mapTile.TileSize / 2);
    Int32 mapCenterY = mapTile.Location.Y + (mapTile.TileSize / 2);
    Int32 actionCenterX = actionTile.Location.X + (mapTile.TileSize / 2);
    Int32 actionCenterY = actionTile.Location.Y + (mapTile.TileSize / 2);

    var dX = mapCenterX - actionCenterX;
    var dY = mapCenterY - actionCenterY;
    var gcd = GCD(Math.Abs(dY), Math.Abs(dX));
    if (gcd == 0)
    {
        return true;
    }
    dX = dX / gcd;
    dY = dY / gcd;

    var currentX = actionCenterX + dX;
    var currentY = actionCenterY + dY;
    var currentTile = _dataBridge.Map.GetClickedTile(currentX, currentY);

    while (currentX != mapCenterX && currentY != mapCenterY)
    {
        if (currentTile.Collide)
        {
            return false;
        }
        currentX += dX;
        currentY += dY;
        currentTile = _dataBridge.Map.GetClickedTile(currentX, currentY);
    }
    return true;
}

private Int32 GCD(int a, int b)
{
    while (b > 0)
    {
        int rem = a % b;
        a = b;
        b = rem;
    }
    return a;
}
*/
