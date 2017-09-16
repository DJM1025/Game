using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Game.Scripting
{
    [Serializable]
    class ChopTreeScript : BaseScript
    {
        public ChopTreeScript()
        {
            Name = "Chop Tree";
            IsInterruptable = true;
            PlayerTriggerable = true;
        }

        public override void ScriptLogic()
        {
            _isRunning = true;
            if (ActionObjectInPlayerRange(1))
            {
                playerImage = ScriptDataBridge.Player.Image;
                SetPlayerAnimation(LoadAnimation(animPath + "ChopTreeFront.bin"));
                SetPlayerAnimating(true);
                PlaySoundOnLoop(soundPath + "chop.mp3");
                Int32 count = 0;
                while (_isRunning && (RandomHelper.Next(20) != 0 || count < 20))
                {
                    PauseScript(100);
                    count++;
                }
                if (_isRunning)
                {
                    _actionObject.Scripts.RemoveAll(script => script.Name == Name);
                    SetActionObjectAnimating(false);
                    SetActionObjectCollision(false);
                    SetPlayerAnimating(false);
                    ScriptDataBridge.Player.Image = playerImage;
                    StopPlayingSound();
                    SetActionObjectImage(Properties.Resources.grass);
                    _actionObject.DefaultScript?.StopScript();
                    _actionObject.DefaultScript?.RunScript();
                    _woodCount++;
                }
            }
            OnScriptComplete();
        }

        protected override void interrupt()
        {
            _isRunning = false;
            SetPlayerAnimating(false);
            StopPlayingSound();
            if (playerImage != null)
            {
                ScriptDataBridge.Player.Image = playerImage;
            }
        }

        protected override void Stop()
        {
            interrupt();
        }

        private Bitmap playerImage = null;
        private Boolean _isRunning = true;

        [NonSerialized]
        public static Int32 _woodCount = 0;
    }
}
