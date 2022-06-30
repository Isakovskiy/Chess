using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp
{
    class CellView : Button
    {

        private System.ComponentModel.IContainer components = null;
        public readonly Color defaultColor;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public CellView(Color color, Point location, Action<int, int> action, int x, int y)
        {
            X = x;
            Y = y;
            _action = action;
            defaultColor = color;
            InitializeComponent(color, location);
        }

        private void InitializeComponent(Color color, Point location)
        {
            this.BackColor = color;
			//this.FlatStyle = FlatStyle.Flat;
			this.FlatAppearance.BorderSize = 1;
            this.FlatAppearance.BorderColor = Color.LightGray;
			this.Location = location;
			this.Margin = new System.Windows.Forms.Padding(0);
            this.Size = new System.Drawing.Size(ChessForm.CELLSIZE, ChessForm.CELLSIZE);
            this.UseVisualStyleBackColor = false;
            this.Click += button_Click;

        }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Avaible { get; set; } = false;

        private Action<int, int> _action;
        private void button_Click(object sender, EventArgs e)
        {
            _action(X, Y);
        }
    }
    
}
