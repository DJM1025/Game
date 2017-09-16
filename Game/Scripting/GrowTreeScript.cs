using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Scripting
{
    [Serializable]
    class GrowTreeScript : BaseScript
    {
        public GrowTreeScript()
        {
            this.Name = "Grow Tree";
            this.IsInterruptable = true;
            this.PlayerTriggerable = false;
        }

        public override void ScriptLogic()
        {
            Boolean isRunning = true;
            if (!_actionObject.Scripts.Any(script => script.Name == "Chop Tree"))
            {
                while (RandomHelper.Next(100) < 98 && isRunning && !AllScriptsEnding)
                {
                    PauseScript(1000);
                }
                if (isRunning && !AllScriptsEnding)
                {
                    SetActionObjectCollision(true);
                    SetActionObjectAnimating(false);
                    SetActionObjectImage(Properties.Resources.tree);
                    if (!_actionObject.Scripts.Any(script => script.Name == "Chop Tree"))
                    {
                        _actionObject.Scripts.Add(new ChopTreeScript());
                    }
                }
            }
            OnScriptComplete();
        }

        protected override void interrupt()
        {
            isRunning = false;
            SetActionObjectAnimating(false);
            OnScriptComplete();
        }

        protected override void Stop()
        {
            isRunning = false;
            SetActionObjectAnimating(false);
            OnScriptComplete();
        }

        Boolean isRunning = true;
    }
}
