namespace ScriptHelper
{
    partial class ScriptHelper
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._scriptListBox = new System.Windows.Forms.ListBox();
            this._addedScriptListBox = new System.Windows.Forms.ListBox();
            this._addButton = new System.Windows.Forms.Button();
            this._removeButton = new System.Windows.Forms.Button();
            this._setDefaultButton = new System.Windows.Forms.Button();
            this._moveUpButton = new System.Windows.Forms.Button();
            this._moveDownButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _scriptListBox
            // 
            this._scriptListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._scriptListBox.FormattingEnabled = true;
            this._scriptListBox.Location = new System.Drawing.Point(12, 15);
            this._scriptListBox.Name = "_scriptListBox";
            this._scriptListBox.Size = new System.Drawing.Size(120, 329);
            this._scriptListBox.TabIndex = 0;
            // 
            // _addedScriptListBox
            // 
            this._addedScriptListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._addedScriptListBox.FormattingEnabled = true;
            this._addedScriptListBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this._addedScriptListBox.Location = new System.Drawing.Point(399, 12);
            this._addedScriptListBox.Name = "_addedScriptListBox";
            this._addedScriptListBox.Size = new System.Drawing.Size(120, 329);
            this._addedScriptListBox.TabIndex = 1;
            // 
            // _addButton
            // 
            this._addButton.Location = new System.Drawing.Point(138, 128);
            this._addButton.Name = "_addButton";
            this._addButton.Size = new System.Drawing.Size(75, 23);
            this._addButton.TabIndex = 2;
            this._addButton.Text = "Add";
            this._addButton.UseVisualStyleBackColor = true;
            this._addButton.Click += new System.EventHandler(this._addButton_Click);
            // 
            // _removeButton
            // 
            this._removeButton.Location = new System.Drawing.Point(138, 157);
            this._removeButton.Name = "_removeButton";
            this._removeButton.Size = new System.Drawing.Size(75, 23);
            this._removeButton.TabIndex = 3;
            this._removeButton.Text = "Remove";
            this._removeButton.UseVisualStyleBackColor = true;
            this._removeButton.Click += new System.EventHandler(this._removeButton_Click);
            // 
            // _setDefaultButton
            // 
            this._setDefaultButton.Location = new System.Drawing.Point(138, 186);
            this._setDefaultButton.Name = "_setDefaultButton";
            this._setDefaultButton.Size = new System.Drawing.Size(75, 23);
            this._setDefaultButton.TabIndex = 4;
            this._setDefaultButton.Text = "Set Default";
            this._setDefaultButton.UseVisualStyleBackColor = true;
            this._setDefaultButton.Click += new System.EventHandler(this._setDefaultButton_Click);
            // 
            // _moveUpButton
            // 
            this._moveUpButton.Location = new System.Drawing.Point(318, 141);
            this._moveUpButton.Name = "_moveUpButton";
            this._moveUpButton.Size = new System.Drawing.Size(75, 23);
            this._moveUpButton.TabIndex = 5;
            this._moveUpButton.Text = "Move Up";
            this._moveUpButton.UseVisualStyleBackColor = true;
            this._moveUpButton.Click += new System.EventHandler(this._moveUpButton_Click);
            // 
            // _moveDownButton
            // 
            this._moveDownButton.Location = new System.Drawing.Point(318, 170);
            this._moveDownButton.Name = "_moveDownButton";
            this._moveDownButton.Size = new System.Drawing.Size(75, 23);
            this._moveDownButton.TabIndex = 6;
            this._moveDownButton.Text = "Move Down";
            this._moveDownButton.UseVisualStyleBackColor = true;
            this._moveDownButton.Click += new System.EventHandler(this._moveDownButton_Click);
            // 
            // ScriptHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 365);
            this.Controls.Add(this._moveDownButton);
            this.Controls.Add(this._moveUpButton);
            this.Controls.Add(this._setDefaultButton);
            this.Controls.Add(this._removeButton);
            this.Controls.Add(this._addButton);
            this.Controls.Add(this._addedScriptListBox);
            this.Controls.Add(this._scriptListBox);
            this.Name = "ScriptHelper";
            this.Text = "Script Helper";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox _scriptListBox;
        private System.Windows.Forms.ListBox _addedScriptListBox;
        private System.Windows.Forms.Button _addButton;
        private System.Windows.Forms.Button _removeButton;
        private System.Windows.Forms.Button _setDefaultButton;
        private System.Windows.Forms.Button _moveUpButton;
        private System.Windows.Forms.Button _moveDownButton;
    }
}

