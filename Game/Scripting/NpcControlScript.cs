using Game.NPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Scripting
{
    [Serializable]
    class NpcControlScript : BaseScript
    {
        public NpcControlScript()
        {
            Name = "Npc Control";
            IsInterruptable = true;
            PlayerTriggerable = false;
            ScriptDataBridge.Player.PlayerMoved += Player_PlayerMoved;
        }

        private void Player_PlayerMoved(object sender, EventArgs e)
        {
            if (MasterRunningScriptList.Closing)
            {
                ScriptDataBridge.Player.PlayerMoved -= Player_PlayerMoved;
                OnScriptComplete();
                return;
            }
            if (_npc.IsMoving)
            {
                _npc.StopMoving();
            }
        }

        public override void ScriptLogic()
        {
            _npc = _actionObject as Npc;
            _npc.NpcMoved += _npc_NpcMoved;
            _npc.MoveCompleted += _npc_MoveCompleted;
            if (!_npc.IsMoving && !MasterRunningScriptList.Closing)
            {
                _npc.MoveTo(ScriptDataBridge.Player.CurrentTile);
            }
        }

        private void _npc_NpcMoved(object sender, EventArgs e)
        {
            if (_npc.CurrentTile == ScriptDataBridge.Player.CurrentTile)
            {
                ScriptDataBridge.Player.Animating = false;
                ScriptDataBridge.Player.CurrentTile = ScriptDataBridge.Map.Tiles[RandomHelper.Next(ScriptDataBridge.Map.MapSize)][RandomHelper.Next(ScriptDataBridge.Map.MapSize)];
                //ScriptDataBridge.Player.Location = ScriptDataBridge.Player.CurrentTile.Location;
                ScriptDataBridge.Map.CenterOnPlayer(1000, ScriptDataBridge.Player);
            }
        }

        private void _npc_MoveCompleted(object sender, EventArgs e)
        {
            if (MasterRunningScriptList.Closing)
            {
                ScriptDataBridge.Player.PlayerMoved -= _npc_MoveCompleted;
                return;
            }
            _npc.MoveTo(ScriptDataBridge.Player.CurrentTile);
        }

        protected override void interrupt()
        {
            //_npc.StopMoving();
            //_isRunning = false;
        }

        protected override void Stop()
        {
            ScriptDataBridge.Player.PlayerMoved -= Player_PlayerMoved;
            _npc.MoveCompleted -= _npc_MoveCompleted;
            _npc.StopMoving();
            _isRunning = false;
            OnScriptComplete();
        }

        [NonSerialized]
        private Npc _npc;
        private Boolean _isRunning = true;
    }
}
