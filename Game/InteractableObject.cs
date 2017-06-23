using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Game.Scripting;

namespace Game
{
    [Serializable]
    public class InteractableObject : AnimatableObject
    {
        public InteractableObject() : base()
        {
            Scripts = new List<BaseScript>();
        }

        public void Interact(Player player, PictureBox screen, Int32 x, Int32 y)
        {
            _player = player;
            screen.ContextMenu = getInteractionDropDown();
            screen.ContextMenu.Show(screen, new Point(x, y));
        }

        public void DefaultInteract()
        {
            //Temp Code, Let designer set scripts and searialize them.
            if (Scripts.Count == 0)
            {
                Scripts.Add(new PlayerMoveScript());
                Scripts.Add(new TestScript());
            }
            //Temp Code
            if (!_scriptRunning || _current.IsInterruptable)
            {
                _current?.InterruptScript();
                _current = Scripts.FirstOrDefault();
                if (_current != null)
                {
                    _scriptRunning = true;
                    _current.ScriptComplete += _current_ScriptComplete;
                    _current.RunScript();
                }
            }
        }

        private ContextMenu getInteractionDropDown()
        {
            menu = new System.Windows.Forms.ContextMenu();
            //Temp Code, Let designer set scripts and searialize them.
            if (Scripts.Count == 0)
            {
                Scripts.Add(new PlayerMoveScript());
                Scripts.Add(new TestScript());
            }
            //Temp Code
            foreach (BaseScript s in Scripts)
            {
                if (s.PlayerTriggerable)
                {
                    menu.MenuItems.Add(s.Name);
                }
            }
            foreach (MenuItem item in menu.MenuItems)
            {
                item.Click += clicked;
            }
            return menu;
        }

        protected void clicked(object sender, EventArgs e)
        {
            if (!_scriptRunning)
            {
                _scriptRunning = true;
                _current = Scripts.First(script => script.Name.Equals(((MenuItem)sender).Text));
                _current.ScriptComplete += _current_ScriptComplete;
                _current.RunScript();
            }
            else if(_current.IsInterruptable)
            {
                _current.InterruptScript();
                _scriptRunning = true;
                _current = Scripts.First(script => script.Name.Equals(((MenuItem)sender).Text));
                _current.ScriptComplete += _current_ScriptComplete;
                _current.RunScript();
            }
            menu.Dispose();
        }

        private void _current_ScriptComplete(object sender, EventArgs e)
        {
            _scriptRunning = false;
        }

        public List<BaseScript> Scripts
        {
            get
            {
                return _scripts;
            }
            set
            {
                _scripts = value;
            }
        }

        private Player _player;
        private static Boolean _scriptRunning = false;
        private static BaseScript _current;
        private List<BaseScript> _scripts = new List<BaseScript>();
        [NonSerialized]
        private System.Windows.Forms.ContextMenu menu;
    }
}
