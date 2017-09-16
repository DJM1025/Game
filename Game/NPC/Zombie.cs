using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.NPC
{
    [Serializable]
    class Zombie : Npc
    {
        public Zombie() : base()
        {
            Speed = RandomHelper.Next(2) + 1;
            Image = Properties.Resources.zombieFront;
            //Animation a = new Animation();
            //a.Load(Scripting.BaseScript.animPath + "playerBack.bin");
            //_animations.Add(Enums.Direction.Up, a);
            //a = new Animation();
            //a.Load(Scripting.BaseScript.animPath + "playerFront.bin");
            //_animations.Add(Enums.Direction.Down, a);
            //a = new Animation();
            //a.Load(Scripting.BaseScript.animPath + "playerLeft.bin");
            //_animations.Add(Enums.Direction.Left, a);
            //a = new Animation();
            //a.Load(Scripting.BaseScript.animPath + "playerRight.bin");
            //_animations.Add(Enums.Direction.Right, a);
        }
    }
}
