using System;
using System.Windows.Forms;

namespace TomogramVisualizer
{
    public partial class Form1 : Form
    {
        int FrameCount;
        Bin bin = new Bin();
        View view = new View();
        bool loaded = false;
        int currentLayer = 0;
        private int min, width;
        DateTime NextFPSUpdate = DateTime.Now.AddSeconds(1);

        void displayFPS()
        {
            if (DateTime.Now >= NextFPSUpdate)
            {
                this.Text = String.Format("CT Visualizer (fps={0})", FrameCount);
                NextFPSUpdate = DateTime.Now.AddSeconds(1);
                FrameCount = 0;
            }
            FrameCount++;
        }
        public Form1()
        {
            InitializeComponent();
            trackBar2.Maximum = 2000;
            trackBar2.Value = view.min;
            trackBar3.Minimum = 1;
            trackBar3.Maximum = 2000;
            trackBar3.Value = view.max - view.min;
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string str = dialog.FileName;
                bin.readBIN(str);
                view.setupView(glControl1.Width, glControl1.Height);
                loaded = true;
                trackBar1.Maximum = Bin.Z - 1;
                glControl1.Invalidate();
            }
        }

        bool needReload = false;
        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (loaded)
            {
                if (checkBox2.Checked)
                {
                    if (needReload)
                    {
                        view.generateTextureImage(currentLayer, min, width);
                        view.load2DTexture();
                        needReload = false;
                    }
                    view.drawTexture();
                    glControl1.SwapBuffers();
                }

                else if (checkBox1.Checked)
                {
                    view.drawQuads(currentLayer);
                    glControl1.SwapBuffers();
                }

                else if (checkBox3.Checked)
                {
                    view.drawQuadStrip(currentLayer);
                    glControl1.SwapBuffers();
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            currentLayer = trackBar1.Value;
            needReload = true;
        }

        void Application_Idle(object sender, EventArgs e)
        {
            while (glControl1.IsIdle)
            {
                displayFPS();
                glControl1.Invalidate();
            }
        }

        private void redrawing()
        {
            glControl1.Invalidate();
            needReload = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Application.Idle += Application_Idle;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
            }
            else
            {
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
            }
            redrawing();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Enabled = false;
                checkBox3.Enabled = false;
            }
            else
            {
                checkBox1.Enabled = true;
                checkBox3.Enabled = true;
            }
            redrawing();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            view.max = trackBar2.Value + trackBar3.Value;
            redrawing();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
            }
            else
            {
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
            }
            redrawing();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            view.min = trackBar2.Value;
            redrawing();
        }
    }
}
