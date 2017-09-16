using System;
using Game.NPC;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Scripting
{
    [Serializable]
    class MonsterSpawnScript : BaseScript
    {
        public MonsterSpawnScript()
        {
            Name = "Monster Spawn";
            IsInterruptable = true;
            PlayerTriggerable = false;
        }

        public override void ScriptLogic()
        {
            Npc npc = null;
            _spawning = true;
            while (!MasterRunningScriptList.Closing && _spawning)
            {
                while (_spawning && !MasterRunningScriptList.Closing)
                {
                    Int32 x = RandomHelper.Next(ScriptDataBridge.Map.MapSize);
                    Int32 y = RandomHelper.Next(ScriptDataBridge.Map.MapSize);
                    //Int32 x = 55;
                    //Int32 y = 55;
                    if (GlobalLighting.Instance.Brightness <= -75)
                    {
                        if (!ScriptDataBridge.Map.Tiles[y][x].Collide && NpcHelper.NpcCount() < 150 && !Screen.isDevMode)
                        {
                            npc = new Zombie();
                            npc.Scripts.Add(new KillZombieScript());
                            npc.CurrentTile = ScriptDataBridge.Map.Tiles[y][x];
                            npc.Location = new System.Drawing.Point(npc.CurrentTile.Location.X, npc.CurrentTile.Location.Y);
                            npc.DefaultScript = new NpcControlScript();
                            npc.StartDefaultScript();
                        }
                    }
                    //_spawning = false;
                    System.Threading.Thread.Sleep(2000);
                }
            }
            //OnScriptComplete();
        }

        protected override void interrupt()
        {
            //throw new NotImplementedException();
        }

        protected override void Stop()
        {
            _spawning = false;
        }

        [NonSerialized]
        private Boolean _spawning = true;
    }
}
