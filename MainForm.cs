using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compiler
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            inputRichTextBox.Size = new Size(this.Size.Width-40, this.Height-240);
            outputRichTextBox.Location = new Point(outputRichTextBox.Location.X, inputRichTextBox.Location.Y+inputRichTextBox.Height + 10);
            toolStrip.Size = new Size(this.Size.Width, this.Size.Height);
            outputRichTextBox.Size = new Size(this.Size.Width-40, this.Height-inputRichTextBox.Height-toolStrip.Height-mainMenu.Height-65);
        }
    }
}
