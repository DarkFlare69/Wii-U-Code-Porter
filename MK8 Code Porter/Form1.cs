using System;
using System.IO;
using System.Windows.Forms;

namespace MK8_Code_Porter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Program by DarkFlare" + System.Environment.NewLine + "Codetypes & Codehandler by CosmoCortney" + System.Environment.NewLine, "Credits");
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e) // Old to New
        {
            textBox2.Text = "";
            bool pointer8 = false, pointer16 = false, pointer32 = false, pip = false;
            string line = null;
            StringReader LineString = new StringReader(textBox1.Text);
            while (true)
            {
                line = LineString.ReadLine();
                if (line == null)
                {
                    break;
                }
                if ((pointer8 || pointer16 || pointer32 || pip) && (line.StartsWith("001") || line.StartsWith("002")))
                {
                    textBox2.Text += "D0000000 DEADCAFE" + System.Environment.NewLine;
                }
                if ((!pointer8 || !pointer16 || !pointer32) && line.StartsWith("00120000 "))
                {
                    pointer32 = true;
                    textBox2.Text += line.Replace("00120000 ", "30000000 ") + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("00120000 "))
                {
                    pointer32 = true;
                    textBox2.Text += line.Replace("00120000 ", "30000000 ") + System.Environment.NewLine;
                    continue;
                }
                if (!pip && pointer32 && line.StartsWith("0000"))
                {
                    String temp = line.Replace(" ", "");
                    long LineAsInt = long.Parse(temp, System.Globalization.NumberStyles.HexNumber);
                    LineAsInt += 0x12000000000000;
                    temp = LineAsInt.ToString("X");
                    temp = temp.PadLeft(16, '0');
                    temp = temp.PadLeft(17, '\n');
                    textBox2.Text += temp.Insert(9, " ") + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("00110000 "))
                {
                    pointer16 = true;
                    textBox2.Text += line.Replace("00110000 ", "30000000 ") + System.Environment.NewLine;
                    continue;
                }
                if (!pip && pointer16 && line.StartsWith("0000"))
                {
                    String temp = line.Replace(" ", "");
                    long LineAsInt = long.Parse(temp, System.Globalization.NumberStyles.HexNumber);
                    LineAsInt += 0x11000000000000;
                    temp = LineAsInt.ToString("X");
                    temp = temp.PadLeft(16, '0');
                    temp = temp.PadLeft(17, '\n');
                    textBox2.Text += temp.Insert(9, " ") + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("00100000 "))
                {
                    pointer8 = true;
                    textBox2.Text += line.Replace("00100000 ", "30000000 ") + System.Environment.NewLine;
                    continue;
                }
                if (!pip && pointer8 && line.StartsWith("0000"))
                {
                    String temp = line.Replace(" ", "");
                    long LineAsInt = long.Parse(temp, System.Globalization.NumberStyles.HexNumber);
                    LineAsInt += 0x10000000000000;
                    temp = LineAsInt.ToString("X");
                    temp = temp.PadLeft(16, '0');
                    temp = temp.PadLeft(17, '\n');
                    textBox2.Text += temp.Insert(9, " ") + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("00200000 "))
                {
                    pointer8 = true;
                    pip = true;
                    textBox2.Text += line.Replace("00200000 ", "30000000 ") + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("00210000 "))
                {
                    pointer16 = true;
                    pip = true;
                    textBox2.Text += line.Replace("00210000 ", "30000000 ") + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("00220000 "))
                {
                    pointer32 = true;
                    pip = true;
                    textBox2.Text += line.Replace("00220000 ", "30000000 ") + System.Environment.NewLine;
                    continue;
                }
                if (pip && line.StartsWith("0000"))
                {
                    pip = false;
                    line = line.Replace(" 00000000", System.Environment.NewLine + "30100000 00000000");
                    line = line.Insert(0, "31000000 ");
                    textBox2.Text += line + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("D0000000"))
                {
                    pip = false; pointer8 = false; pointer16 = false; pointer32 = false;
                    textBox2.Text += line + System.Environment.NewLine;
                    continue;
                }
                else
                {
                    textBox2.Text += line + System.Environment.NewLine;
                    continue;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // New to Old
        {
            textBox1.Text = "";
            bool pointer8 = false, pointer16 = false, pointer32 = false, pip = false;
            string line = null;
            StringReader LineString = new StringReader(textBox2.Text);
            while (true)
            {
                line = LineString.ReadLine();
                if (line == null)
                {
                    break;
                }

                // will probably work on this eventually


                MessageBox.Show("This feature isn't fully implemented yet.", "Conversion Failed!");




                if (line.StartsWith("F0000000") || line.StartsWith("D2000000 CAFEBABE") || line.StartsWith("C000") || line.StartsWith("0C000000") || line.StartsWith("0D00") || line.StartsWith("10") || line.StartsWith("11") || line.StartsWith("12") || line.StartsWith("13") || line.StartsWith("14") || line.StartsWith("15"))
                {
                    MessageBox.Show("You're trying to convert a code that utilizes features exclusive to the new codehandler. You cannot convert this code to the old codehandler." + System.Environment.NewLine + System.Environment.NewLine + "The first error was encountered on this line: " + line, "Conversion Failed!");
                    break;
                }

                if (line.StartsWith("D0000000"))
                {
                    pip = false; pointer8 = false; pointer16 = false; pointer32 = false;
                    textBox1.Text += line + System.Environment.NewLine;
                    continue;
                }
                else
                {
                    textBox1.Text += line + System.Environment.NewLine;
                    continue;
                }
            }
        }

        private void oldCodehandlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button1.Click += new System.EventHandler(this.button1_Click);
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = theDialog.FileName;
                string[] filelines = File.ReadAllLines(filename);
                textBox1.Text = "";
                foreach (string line in filelines)
                {
                    textBox1.Text += line + System.Environment.NewLine;
                }
            }
        }

        private void newCodehandlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button1.Click += new System.EventHandler(this.button1_Click);
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = theDialog.FileName;
                string[] filelines = File.ReadAllLines(filename);
                textBox2.Text = "";
                foreach (string line in filelines)
                {
                    textBox2.Text += line + System.Environment.NewLine;
                }
            }
        }

        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program is used to convert codes from one codehandler to the other. Any code from the old codehandler can be ported to the new one, but not vice versa. The new codehandler has many useful features, including (but not limited to) integer operations, assembly support, and time-dependence codes.", "About");
        }
    }
}
