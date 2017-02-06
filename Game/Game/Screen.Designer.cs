namespace Game
{
    partial class Screen
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
            this._gamePanel = new System.Windows.Forms.PictureBox();
            this._chatBox = new System.Windows.Forms.TextBox();
            this._sideBar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._gamePanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._sideBar)).BeginInit();
            this.SuspendLayout();
            // 
            // _gamePanel
            // 
            this._gamePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._gamePanel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this._gamePanel.Location = new System.Drawing.Point(0, 0);
            this._gamePanel.Name = "_gamePanel";
            this._gamePanel.Size = new System.Drawing.Size(1100, 700);
            this._gamePanel.TabIndex = 0;
            this._gamePanel.TabStop = false;
            this._gamePanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this._gamePanel_MouseClick);
            // 
            // _chatBox
            // 
            this._chatBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._chatBox.Location = new System.Drawing.Point(0, 700);
            this._chatBox.Multiline = true;
            this._chatBox.Name = "_chatBox";
            this._chatBox.Size = new System.Drawing.Size(1100, 263);
            this._chatBox.TabIndex = 1;
            // 
            // _sideBar
            // 
            this._sideBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._sideBar.Location = new System.Drawing.Point(1100, 0);
            this._sideBar.Name = "_sideBar";
            this._sideBar.Size = new System.Drawing.Size(383, 962);
            this._sideBar.TabIndex = 2;
            this._sideBar.TabStop = false;
            // 
            // Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 961);
            this.Controls.Add(this._sideBar);
            this.Controls.Add(this._chatBox);
            this.Controls.Add(this._gamePanel);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(1500, 1000);
            this.MinimumSize = new System.Drawing.Size(1500, 1000);
            this.Name = "Screen";
            this.Text = "Game";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this._gamePanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._sideBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox _gamePanel;
        private System.Windows.Forms.TextBox _chatBox;
        private System.Windows.Forms.PictureBox _sideBar;

    }
}

