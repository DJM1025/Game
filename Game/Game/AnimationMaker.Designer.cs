namespace Game
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
            ((System.ComponentModel.ISupportInitialize)(this._preview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._selectionBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._animationBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._speed)).BeginInit();
            this.SuspendLayout();
            // 
            // _loadButton
            // 
            this._loadButton.Location = new System.Drawing.Point(850, 146);
            this._loadButton.Name = "_loadButton";
            this._loadButton.Size = new System.Drawing.Size(128, 23);
            this._loadButton.TabIndex = 0;
            this._loadButton.Text = "Load";
            this._loadButton.UseVisualStyleBackColor = true;
            this._loadButton.Click += new System.EventHandler(this._loadButton_Click);
            // 
            // _preview
            // 
            this._preview.Location = new System.Drawing.Point(880, 243);
            this._preview.Name = "_preview";
            this._preview.Size = new System.Drawing.Size(64, 64);
            this._preview.TabIndex = 1;
            this._preview.TabStop = false;
            // 
            // _selectionBox
            // 
            this._selectionBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._selectionBox.Location = new System.Drawing.Point(12, 12);
            this._selectionBox.Name = "_selectionBox";
            this._selectionBox.Size = new System.Drawing.Size(832, 225);
            this._selectionBox.TabIndex = 2;
            this._selectionBox.TabStop = false;
            this._selectionBox.Click += new System.EventHandler(this._selectionBox_Click);
            // 
            // _animationBox
            // 
            this._animationBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._animationBox.Location = new System.Drawing.Point(12, 243);
            this._animationBox.Name = "_animationBox";
            this._animationBox.Size = new System.Drawing.Size(832, 64);
            this._animationBox.TabIndex = 3;
            this._animationBox.TabStop = false;
            this._animationBox.Click += new System.EventHandler(this._animationBox_Click);
            // 
            // _saveButton
            // 
            this._saveButton.Location = new System.Drawing.Point(850, 175);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(128, 23);
            this._saveButton.TabIndex = 4;
            this._saveButton.Text = "Save";
            this._saveButton.UseVisualStyleBackColor = true;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _speed
            // 
            this._speed.Location = new System.Drawing.Point(850, 217);
            this._speed.Name = "_speed";
            this._speed.Size = new System.Drawing.Size(128, 20);
            this._speed.TabIndex = 5;
            this._speed.ValueChanged += new System.EventHandler(this._speed_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(868, 201);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Animation Speed";
            // 
            // AnimationMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 319);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._speed);
            this.Controls.Add(this._saveButton);
            this.Controls.Add(this._animationBox);
            this.Controls.Add(this._selectionBox);
            this.Controls.Add(this._preview);
            this.Controls.Add(this._loadButton);
            this.Name = "AnimationMaker";
            this.Text = "AnimationMaker";
            ((System.ComponentModel.ISupportInitialize)(this._preview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._selectionBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._animationBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._speed)).EndInit();
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
    }
}