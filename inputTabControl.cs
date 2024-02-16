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
    public partial class inputTabControl : UserControl
    {
        public inputTabControl()
        {
            InitializeComponent();
            this.addTabPage();
            this.addTabPage();
        }

        internal class CustomTabPage:TabPage
        {
            public RichTextBox inputRichTextBox;
            public RichTextBox linesRichTextBox;

            public CustomTabPage(int index)
            {
                this.Resize += new System.EventHandler(this.myResize);
                this.Text = index.ToString();


                
            }


            public void myResize(object sender, EventArgs e)
            {
                inputRichTextBox = new RichTextBox();
                inputRichTextBox.Size = new Size(this.Width - 30, this.Height);
                inputRichTextBox.Location = new Point(20, 0);
                inputRichTextBox.Parent = this;

                linesRichTextBox = new RichTextBox();
                linesRichTextBox.Size = new Size(20, this.Height);
                linesRichTextBox.Location = new Point(0, 0);
                linesRichTextBox.Parent = this;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex == tabControl1.TabCount-1)
            {
                MessageBox.Show("Last");
                addTabPage();
            }

            

        }

        private void addTabPage()
        {
            tabControl1.TabPages.Add(new CustomTabPage(tabControl1.TabPages.Count));
            tabControl1.TabPages[tabControl1.TabPages.Count - 1].Size = tabControl1.Size;
        }

        private void richTextBox2_ControlAdded(object sender, ControlEventArgs e)
        {
            MessageBox.Show("Added");
        }

        private void tabControl1_Resize(object sender, EventArgs e)
        {

        }
    }
}
