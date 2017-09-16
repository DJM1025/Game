using Game.NPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Scripting
{
    [Serializable]
    class KillZombieScript : BaseScript
    {

        public KillZombieScript()
        {
            Name = "Attack Zombie";
            PlayerTriggerable = true;
            IsInterruptable = false;
        }
        public override void ScriptLogic()
        {
            if (count > 0)
            {
                var zombie = _actionObject as Zombie;
                zombie.DefaultScript.StopScript();
                zombie.StopMoving();
                NPC.NpcHelper.RemoveNPC(zombie);
            }
            else
            {
                count++;
            }
            OnScriptComplete();
        }

        protected override void interrupt()
        {
            OnScriptComplete();
        }

        protected override void Stop()
        {
            OnScriptComplete();
        }

        Int32 count = 0;
    }
}
