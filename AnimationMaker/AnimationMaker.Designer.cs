namespace AnimationMaker
{
    partial class AnimationMaker
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
            this._loadButton = new System.Windows.Forms.Button();
            this._preview = new System.Windows.Forms.PictureBox();
            this._selectionBox = new System.Windows.Forms.PictureBox();
            this._animationBox = new System.Windows.Forms.PictureBox();
            this._saveButton = new System.Windows.Forms.Button();
            this._speed = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this._defaultImage = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this._name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openAnimationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeTimedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this._preview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._selectionBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._animationBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._speed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._defaultImage)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _loadButton
            // 
            this._loadButton.Location = new System.Drawing.Point(850, 193);
            this._loadButton.Name = "_loadButton";
            this._loadButton.Size = new System.Drawing.Size(128, 23);
            this._loadButton.TabIndex = 0;
            this._loadButton.Text = "Load";
            this._loadButton.UseVisualStyleBackColor = true;
            this._loadButton.Click += new System.EventHandler(this._loadButton_Click);
            // 
            // _preview
            // 
            this._preview.Location = new System.Drawing.Point(880, 37);
            this._preview.Name = "_preview";
            this._preview.Size = new System.Drawing.Size(64, 64);
            this._preview.TabIndex = 1;
            this._preview.TabStop = false;
            // 
            // _selectionBox
            // 
            this._selectionBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._selectionBox.Location = new System.Drawing.Point(12, 37);
            this._selectionBox.Name = "_selectionBox";
            this._selectionBox.Size = new System.Drawing.Size(832, 225);
            this._selectionBox.TabIndex = 2;
            this._selectionBox.TabStop = false;
            this._selectionBox.Click += new System.EventHandler(this._selectionBox_Click);
            // 
            // _animationBox
            // 
            this._animationBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._animationBox.Location = new System.Drawing.Point(12, 268);
            this._animationBox.Name = "_animationBox";
            this._animationBox.Size = new System.Drawing.Size(832, 64);
            this._animationBox.TabIndex = 3;
            this._animationBox.TabStop = false;
            this._animationBox.Click += new System.EventHandler(this._animationBox_Click);
            // 
            // _saveButton
            // 
            this._saveButton.Location = new System.Drawing.Point(850, 222);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(128, 23);
            this._saveButton.TabIndex = 4;
            this._saveButton.Text = "Save";
            this._saveButton.UseVisualStyleBackColor = true;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _speed
            // 
            this._speed.Location = new System.Drawing.Point(850, 127);
            this._speed.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this._speed.Name = "_speed";
            this._speed.Size = new System.Drawing.Size(128, 20);
            this._speed.TabIndex = 5;
            this._speed.ValueChanged += new System.EventHandler(this._speed_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(871, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Animation Delay";
            // 
            // _defaultImage
            // 
            this._defaultImage.Location = new System.Drawing.Point(880, 268);
            this._defaultImage.Name = "_defaultImage";
            this._defaultImage.Size = new System.Drawing.Size(64, 64);
            this._defaultImage.TabIndex = 7;
            this._defaultImage.TabStop = false;
            this._defaultImage.Click += new System.EventHandler(this._defaultImage_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(889, 253);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Default";
            // 
            // _name
            // 
            this._name.Location = new System.Drawing.Point(850, 169);
            this._name.Name = "_name";
            this._name.Size = new System.Drawing.Size(128, 20);
            this._name.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(871, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Animation Name";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(979, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openAnimationToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openAnimationToolStripMenuItem
            // 
            this.openAnimationToolStripMenuItem.Name = "openAnimationToolStripMenuItem";
            this.openAnimationToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.openAnimationToolStripMenuItem.Text = "Open Animation";
            this.openAnimationToolStripMenuItem.Click += new System.EventHandler(this.openAnimationToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.makeTimedToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // makeTimedToolStripMenuItem
            // 
            this.makeTimedToolStripMenuItem.Name = "makeTimedToolStripMenuItem";
            this.makeTimedToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            // 
            // AnimationMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 338);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._defaultImage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._speed);
            this.Controls.Add(this._saveButton);
            this.Controls.Add(this._animationBox);
            this.Controls.Add(this._selectionBox);
            this.Controls.Add(this._preview);
            this.Controls.Add(this._loadButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AnimationMaker";
            this.Text = "AnimationMaker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AnimationMaker_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this._preview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._selectionBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._animationBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._speed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._defaultImage)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _loadButton;
        private System.Windows.Forms.PictureBox _preview;
        private System.Windows.Forms.PictureBox _selectionBox;
        private System.Windows.Forms.PictureBox _animationBox;
        private System.Windows.Forms.Button _saveButton;
        private System.Windows.Forms.NumericUpDown _speed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox _defaultImage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openAnimationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeTimedToolStripMenuItem;
    }
}