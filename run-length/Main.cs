using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace run_length//
{
    public partial class Main : Form
    {
        private Bitmap btm;
        private string imagefilename = "";
        private string encodeImageFile = "";
        bool en = false; 
        public Main()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About abt = new About();
            abt.ShowDialog();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("are you sure",
                                "notice",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help hlp = new Help();
            hlp.ShowDialog();
        }

        private void compressBtn_Click(object sender, EventArgs e)
        {
            string filename="";
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Run-length encoded text(*.rlt)|*.rlt";
            if (inputTxtBox.Text == "")
            {
                MessageBox.Show("there is no data to compress please enter something\n open a file or copy from clipboard",
                                "warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
                if (encodeToolStripMenuItem.Checked)
                {
                    sf.ShowDialog();
                    filename = sf.FileName;
                    if (filename != "")
                    {
                        textBox1.Text = Encode.encode(inputTxtBox.Text);
                        StreamWriter fw = new StreamWriter(filename);
                        fw.Write(textBox1.Text);
                        fw.Close();
                        label1.Text = "encoding completed original file size is : " + inputTxtBox.Text.Length + " byte encoded file size is : " + textBox1.Text.Length + " byte";
                        en = true;
                    }
                    else
                    {
                        MessageBox.Show("enter file name first", "warning");
                    }
                }
                else
                {
                    textBox1.Text = Encode.encode(inputTxtBox.Text);
                    label1.Text = "encoding completed original file size is : " + inputTxtBox.Text.Length + " byte encoded file size is : " + textBox1.Text.Length + " byte";
                    en = true;
                }
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = "";
            OpenFileDialog of = new OpenFileDialog();
            of.ShowDialog();
            filename= of.FileName;
            if (filename != "")
            {
                StreamReader fr = new StreamReader(filename);
                if (filename.EndsWith(".rlb"))
                {
                    encodeImageFile = fr.ReadToEnd();
                    textBox1.Text = encodeImageFile;
                }
                else
                {
                    inputTxtBox.Text = fr.ReadToEnd();
                    if (filename.EndsWith(".rlt"))
                    {
                        decompressBtn.Enabled = true;
                        compressBtn.Enabled = false;
                    }
                    else
                    {
                        decompressBtn.Enabled = false;
                        compressBtn.Enabled = true;
                    }
                    textBox1.Text = "";
                }
                fr.Close();
            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename;
            SaveFileDialog sf = new SaveFileDialog();
            if (en)
            {
                sf.Filter = "Run-length encoded text(*.rlt)|*.rlt";
            }
            sf.ShowDialog();
            filename = sf.FileName;
            if (filename != "")
            {
                StreamWriter fw = new StreamWriter(filename);
                fw.Write(textBox1.Text);
                fw.Close();
            }
        }

        private void decompressBtn_Click(object sender, EventArgs e)
        {
            string filename = "";
            SaveFileDialog sf = new SaveFileDialog();
            if (inputTxtBox.Text == "")
            {
                MessageBox.Show("there is no data to decode please enter something\n open a file or copy from clipboard",
                                "warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
                if (decodeToolStripMenuItem.Checked)
                {
                    sf.ShowDialog();
                    filename = sf.FileName;
                    if (filename != "")
                    {
                        textBox1.Text = Decode.decompress(inputTxtBox.Text);
                        StreamWriter fw = new StreamWriter(filename);
                        fw.Write(textBox1.Text);
                        fw.Close();
                        label1.Text = "decoding completed encoded file size is : " + inputTxtBox.Text.Length + " byte decoded file size is : " + textBox1.Text.Length + " byte";
                        en = false;
                    }
                }
                else
                {
                    textBox1.Text = Decode.decompress(inputTxtBox.Text);
                    label1.Text = "decoding completed encoded file size is : " + inputTxtBox.Text.Length + " byte decoded file size is : " + textBox1.Text.Length + " byte";
                    en = false;
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "bitmap image(*.bmp)|*.bmp";
            op.ShowDialog();
            imagefilename = op.FileName;
            if (imagefilename !="")
            {
                btm = new Bitmap(imagefilename);
                pictureBox1.ImageLocation = imagefilename;
                textBox2.Text += "\n[" + imagefilename + " ]loaded";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string saveFilename = "";
            EncodeImage enim=new EncodeImage();
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "custom Run-length bitmap file(*.rlb)|*.rlb";
            if (imagefilename != "")
            {
                sf.ShowDialog();
                saveFilename = sf.FileName;
                textBox1.Text = enim.encodeImg(btm);
            }
            else
            {
                MessageBox.Show("load bitmap image file first!", "image not loaded");
            }
            if (saveFilename != "")
            {
                StreamWriter sfs = new StreamWriter(saveFilename);
                sfs.Write(textBox1.Text);
                sfs.Close();
                tabControl1.SelectTab(0);
                label1.Text = enim.imageSize;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = DecodeImage.decodeImg(encodeImageFile);
            pictureBox1.ImageLocation = System.Environment.CurrentDirectory + "\\image.bmp";
            tabControl1.SelectTab(1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string filename = "";
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "bitmap image file(*.bmp)|*.bmp";
            sf.ShowDialog();
            filename = sf.FileName;
            if (filename != "")
            {
                if (File.Exists(System.Environment.CurrentDirectory + "\\image.bmp")){
                    File.Copy(System.Environment.CurrentDirectory + "\\image.bmp", filename, true);
                }
            }
        }
    }
}
