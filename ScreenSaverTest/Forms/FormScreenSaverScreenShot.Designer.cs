namespace ScreenSaverTest.Forms
{
    partial class FormScreenSaverScreenShot
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
            this.pbScreenShot = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbScreenShot)).BeginInit();
            this.SuspendLayout();
            // 
            // pbScreenShot
            // 
            this.pbScreenShot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbScreenShot.Location = new System.Drawing.Point(0, 0);
            this.pbScreenShot.Name = "pbScreenShot";
            this.pbScreenShot.Size = new System.Drawing.Size(984, 521);
            this.pbScreenShot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbScreenShot.TabIndex = 0;
            this.pbScreenShot.TabStop = false;
            // 
            // FormScreenSaverScreenShot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(984, 521);
            this.Controls.Add(this.pbScreenShot);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormScreenSaverScreenShot";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbScreenShot)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbScreenShot;
    }
}

