using Game.Scripting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.DevTools.ScriptHelper
{
    public partial class ScriptHelper : Form
    {
        public ScriptHelper(InteractableObject obj)
        {
            InitializeComponent();
            _object = obj;
            _addedScripts = _object.Scripts;
            _scriptListBox.DataSource = _scripts;
            _scriptListBox.DisplayMember = "Name";
            _addedScriptListBox.DisplayMember = "Name";
            _addedScriptListBox.DataSource = _addedScripts;
            this.FormClosing += ScriptHelper_FormClosing;
        }

        private void ScriptHelper_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_object.DefaultScript != null)
            {
                _object.StartDefaultScript();
            }
        }

        private void _addButton_Click(object sender, EventArgs e)
        {
            if (_scriptListBox.SelectedIndex >= 0)
            {
                _addedScripts.Add((BaseScript)_scriptListBox.SelectedItem);
            }
            updateDatasource();
        }

        private void _removeButton_Click(object sender, EventArgs e)
        {
            if (_addedScriptListBox.SelectedIndex >= 0)
            {
                _addedScripts.RemoveAt(_addedScriptListBox.SelectedIndex);
            }
            updateDatasource();
        }

        private void _setDefaultButton_Click(object sender, EventArgs e)
        {
            _object.DefaultScript = (BaseScript)_scriptListBox.SelectedItem;
        }

        private void _moveUpButton_Click(object sender, EventArgs e)
        {
            var tempIndex = _scriptListBox.SelectedIndex - 1;
            if (tempIndex >= 0)
            {
                var tempItem = _scriptListBox.Items[tempIndex];
                _addedScriptListBox.Items[tempIndex] = _scriptListBox.SelectedItem;
                _scriptListBox.Items[_scriptListBox.SelectedIndex] = tempItem;
            }
            updateDatasource();
        }

        private void _moveDownButton_Click(object sender, EventArgs e)
        {
            var tempIndex = _scriptListBox.SelectedIndex + 1;
            if (tempIndex < _scriptListBox.Items.Count - 1 && tempIndex > 0)
            {
                var tempItem = _scriptListBox.Items[tempIndex];
                _addedScriptListBox.Items[tempIndex] = _scriptListBox.SelectedItem;
                _scriptListBox.Items[_scriptListBox.SelectedIndex] = tempItem;
            }
            updateDatasource();
        }

        private void updateDatasource()
        {
            _object.Scripts = _addedScripts;
            _addedScriptListBox.DataSource = null;
            _addedScriptListBox.DataSource = _addedScripts;
        }

        public void CopyScriptsToObject(InteractableObject obj)
        {
            if (_object != null)
            {
                foreach (var script in _object.Scripts)
                {
                    Type t = script.GetType();
                    var s = (BaseScript)Activator.CreateInstance(t);
                    obj.Scripts.Add(s);
                }
                if (_object.DefaultScript != null)
                {
                    Type t = _object.DefaultScript.GetType();
                    var s = (BaseScript)Activator.CreateInstance(t);
                    obj.DefaultScript = s;
                    s.RunScript();
                }
            }        
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
                _scriptListBox.DataSource = _scripts;
            }
        }

        private InteractableObject _object;
        private List<BaseScript> _scripts = new List<BaseScript>();
        private List<BaseScript> _addedScripts = new List<BaseScript>();
    }
}
