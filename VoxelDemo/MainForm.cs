using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoxelDemo
{
    public partial class MainForm : Form
    {
        public VoxelRenderer Renderer { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Renderer.Render(e.Graphics);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    Renderer.RotateX(false);
                    Invalidate();
                    break;
                case Keys.Down:
                    Renderer.RotateX(true);
                    Invalidate();
                    break;
                case Keys.Left:
                    Renderer.RotateY(true);
                    Invalidate();
                    break;
                case Keys.Right:
                    Renderer.RotateY(false);
                    Invalidate();
                    break;
                case Keys.PageUp:
                    Renderer.RotateZ(true);
                    Invalidate();
                    break;
                case Keys.PageDown:
                    Renderer.RotateZ(false);
                    Invalidate();
                    break;
            }
        }
    }
}
