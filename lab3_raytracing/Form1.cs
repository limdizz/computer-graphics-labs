using OpenTK.Graphics.OpenGL;
using System;
using System.Windows.Forms;

namespace Raytracing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        View view = new View();

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            view.Draw();
            glControl1.SwapBuffers();
            GL.UseProgram(0);
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            view.initShaders();
            view.initBufferData();
        }
    }
}
