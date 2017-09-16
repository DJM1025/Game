using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Game.NPC
{
    public class NpcHelper
    {
        public static void AddNPC(Npc npc)
        {
            lock (_npcs)
            {
                _npcs.Add(npc);
            }
        }

        public static void RemoveNPC(Predicate<Npc> match)
        {
            lock (_npcs)
            {
                for (int i = _npcs.Count - 1; i >= 0; i--)
                {
                    if (match(_npcs[i]))
                    {
                        _npcs.RemoveAt(i);
                        return;
                    }
                }
            }
        }

        public static void Draw(Graphics g)
        {
            lock (_npcs)
            {
                foreach (var npc in _npcs)
                {
                    npc.Draw(g);
                }
            }
        }

        public static Int32 NpcCount()
        {
            return _npcs.Count;
        }

        public static void RemoveNPC(Npc npc)
        {
            RemoveNPC(n => n == npc);
        }

        public static Npc GetClickedNPC(Int32 x, Int32 y)
        {
            var filteredList = _npcs?.Where(npc => npc.Location.X <= x && npc.Location.X + npc.Size >= x);
            filteredList = filteredList?.Where(npc => npc.Location.Y <= y && npc.Location.Y + npc.Size >= y);
            return filteredList?.FirstOrDefault();
        }

        private static List<Npc> _npcs = new List<Npc>();
    }
}
