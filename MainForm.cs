using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compiler
{
    public partial class MainForm : Form
    {
        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }
        public MainForm()
        {
            InitializeComponent();
            this.Text = AssemblyTitle;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            inputRichTextBox.Size = new Size(this.Size.Width-40, this.Height-240);
            outputRichTextBox.Location = new Point(outputRichTextBox.Location.X, inputRichTextBox.Location.Y+inputRichTextBox.Height + 10);
            toolStrip.Size = new Size(this.Size.Width, this.Size.Height);
            outputRichTextBox.Size = new Size(this.Size.Width-40, this.Height-inputRichTextBox.Height-toolStrip.Height-mainMenu.Height-65);
        }

        private void createNewFile()
        {
            inputRichTextBox.Text = null;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            createNewFile();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm(this);
            aboutForm.Show();
            this.Visible = false;
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm(this);
            aboutForm.Show();
            this.Visible = false;
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            inputRichTextBox.Copy();
        }

        private void CutButton_Click(object sender, EventArgs e)
        {
            inputRichTextBox.Cut();
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            inputRichTextBox.Paste();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            inputRichTextBox.Undo();
        }

        private void RepeatButton_Click(object sender, EventArgs e)
        {
            inputRichTextBox.Redo();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {

            saveFileDialog.ShowDialog();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {

            inputRichTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            inputRichTextBox.SaveFile(saveFileDialog.FileName,RichTextBoxStreamType.PlainText);
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createNewFile();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inputRichTextBox.SaveFile("", RichTextBoxStreamType.PlainText);
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inputRichTextBox.Undo();
        }

        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inputRichTextBox.Redo();

        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inputRichTextBox.Cut();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inputRichTextBox.Copy();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inputRichTextBox.Paste();
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            inputRichTextBox.SelectedText = "";
        }

        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inputRichTextBox.SelectAll();
        }
    }
}
