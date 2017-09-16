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

namespace ScriptHelper
{
    public partial class ScriptHelper : Form
    {
        public ScriptHelper()
        {
            InitializeComponent();
            _scriptListBox.DataSource = _scripts;
            _scriptListBox.DisplayMember = "Name";
        }

        private void _addButton_Click(object sender, EventArgs e)
        {
            if (_scriptListBox.SelectedIndex >= 0)
            {
                _addedScriptListBox.Items.Add(_scriptListBox.SelectedItem);
            }
        }

        private void _removeButton_Click(object sender, EventArgs e)
        {
            if (_scriptListBox.SelectedIndex >= 0)
            {
                _scriptListBox.Items.RemoveAt(_scriptListBox.SelectedIndex);
            }
        }

        private void _setDefaultButton_Click(object sender, EventArgs e)
        {
            //TODO
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

        private List<BaseScript> _scripts = new List<BaseScript>();
    }
}
