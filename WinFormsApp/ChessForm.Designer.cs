using Domain.Models;

namespace WinFormsApp
{
    partial class ChessForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Button soloButton;
        private Button multiButton;

		/// <summary>
		///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(600, 600);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
			MaximizeBox = false;
            this.soloButton = new Button()
            {
                Location = new Point(100, 100),
                Size = new Size(100, 30),
                BackColor = Color.Orange,
                ForeColor = Color.White,
                Text = "Solo Play"
            };
			this.multiButton = new Button()
			{
				Location = new Point(300, 100),
				Size = new Size(100, 30),
				BackColor = Color.Blue,
				ForeColor = Color.White,
				Text = "Multi Play",
			};
            this.soloButton.Click += SoloButton_Click;
            this.multiButton.Click += MultiButton_Click;

			Controls.Add(soloButton);
            Controls.Add(multiButton);

			this.ResumeLayout(false);

        }

        #endregion
    }
}