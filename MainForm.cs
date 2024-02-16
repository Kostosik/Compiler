using Compiler.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Compiler
{
    public partial class MainForm : Form
    {
        #region Атрибуты формы
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

        bool isFileSaved = true;
        #endregion


        //Интерфейс с вкладками, позволяющий одновременно работать с несколькими текстами (для окна редактирования).

        //Нумерация строк в окне редактирования текста.

        //Базовая подсветка синтаксиса в окне редактирования.

        //Интерфейс с вкладками, позволяющий работать с разными модулями программы (для окна вывода результатов)

        //Отображение ошибок в окне вывода результатов в виде таблицы.

        inputTabControl inputTabControl;

        public MainForm()
        {
            InitializeComponent();
            this.Text = AssemblyTitle;
            this.MainForm_Resize(null,null);
            

            inputTabControl = new inputTabControl();
            inputTabControl.Parent = this;
            inputTabControl.BringToFront();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            mainSplitContainer.Size = new Size(this.Size.Width - 40, this.Height - 130);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.language == "english")
            {
                this.languageToolStripSplitButton.Image = Resources.флаганглии;
                ChangeLanguage("en");
            }
            if (Properties.Settings.Default.language == "russian")
            {
                this.languageToolStripSplitButton.Image = Resources.флагрф;
                ChangeLanguage("ru-RU");
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control |  Keys.Oemplus))//oemminus
            {
                //doSomething();
                return true;
            }

            if(keyData == (Keys.Control | Keys.OemMinus))
            {
                //doSomething();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private bool showSaveMessageBox()
        {
            const string message =
    "Изменения не сохранены\nВы хотите сохранить изменения?";
            const string caption = "Сохранение изменений";

            var result = MessageBox.Show(message, caption,
                                 MessageBoxButtons.YesNoCancel,
                                 MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
            }
            if(result == DialogResult.Yes)
            {
                saveFileDialog.ShowDialog();
            }
            if(result == DialogResult.Cancel)
            {
                return true;
            }
            return false;
        }

        private void createNewFile()
        {
            if(!isFileSaved)
            {
                if (showSaveMessageBox())
                return;
            }    

            inputRichTextBox.Text = null;
        }

        #region ToolStrip Buttons
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
            if (!isFileSaved)
            {
                if(showSaveMessageBox())
                return;
            }

            openFileDialog.ShowDialog();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {

            saveFileDialog.ShowDialog();
        }

        #endregion

        #region MenuStrip Buttons
        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isFileSaved)
            {
                if(showSaveMessageBox())
                return;
            }

            createNewFile();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isFileSaved)
            {
                if (showSaveMessageBox()) 
                return;
            }

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
            if (!isFileSaved)
            {
                if (showSaveMessageBox()) 
                return;
            }

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
        #endregion

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            inputRichTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            inputRichTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
            isFileSaved = true;
        }

        private void resizeFunction()
        {
            inputRichTextBox.Size = new Size(mainSplitContainer.Width-75, mainSplitContainer.SplitterDistance);
            inputRichTextBox.Location = new Point(mainSplitContainer.Left + 52, mainSplitContainer.Panel1.Top);
            outputRichTextBox.Size = new Size(mainSplitContainer.Width - 10, mainSplitContainer.Size.Height - inputRichTextBox.Size.Height - 10 - statusStrip1.Height);
            lineCountRichTextBox.Size = new Size(60, inputRichTextBox.Size.Height);
            inputTabControl1.Size = new Size(mainSplitContainer.Width, mainSplitContainer.SplitterDistance);
        }

        private void mainSplitContainer_Resize(object sender, EventArgs e)
        {
            resizeFunction();
        }

        private void mainSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            resizeFunction();
        }

        private void inputRichTextBox_TextChanged(object sender, EventArgs e)
        {
            isFileSaved = false;
        }

        private void ChangeLanguage(string lang)
        {
            foreach (Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
                resources.ApplyResources(c, c.Name, new CultureInfo(lang));
            }
            foreach (ToolStripMenuItem item in this.MainMenuStrip.Items)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
                resources.ApplyResources(item, item.Name, new CultureInfo(lang));
            }
            foreach (object item in this.toolStrip.Items)
            {
                if (!(item is ToolStripButton))
                    continue;
                ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
                resources.ApplyResources(item, (item as ToolStripButton).Name , new CultureInfo(lang));
            }
        }

        private void русскийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            languageToolStripSplitButton.Image = Resources.флагрф;
            ChangeLanguage("ru-RU");
            Properties.Settings.Default.language = "russian";
            Properties.Settings.Default.Save();
        }

        private void английскийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            languageToolStripSplitButton.Image = Resources.флаганглии;
            ChangeLanguage("en");
            Properties.Settings.Default.language = "english";
            Properties.Settings.Default.Save();
        }

        private void inputRichTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                lineCountRichTextBox.Text+="\n" +(lineCountRichTextBox.Lines.Length +1).ToString() + ".";
            }

        }

        private void inputRichTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)13)
            //{
            //    lineCountRichTextBox.Text += "\n" + (lineCountRichTextBox.Lines.Length + 1).ToString() + ".";
            //}

            if (e.KeyChar == (char)13)
            {
                lineCountRichTextBox.Text.Insert(0,(lineCountRichTextBox.Lines.Length + 1).ToString() + ".");
                //RTF_Scroll(lineCountRichTextBox, null);
            }

        }

        public class SynchronizedScrollRichTextBox : System.Windows.Forms.RichTextBox
        {
            public event vScrollEventHandler vScroll;
            public delegate void vScrollEventHandler(System.Windows.Forms.Message message);

            public const int WM_VSCROLL = 0x115;

            protected override void WndProc(ref System.Windows.Forms.Message msg)
            {
                if (msg.Msg == WM_VSCROLL)
                {
                    if (vScroll != null)
                    {
                        vScroll(msg);
                    }
                }
                base.WndProc(ref msg);
            }

            public void PubWndProc(ref System.Windows.Forms.Message msg)
            {
                base.WndProc(ref msg);
            }
        }

        private void inputRichTextBox_vScroll(Message message)
        {
            message.HWnd = lineCountRichTextBox.Handle;
            //lineCountRichTextBox.PubWndProc(ref message);
        }

 
        #region MessageEventHandler

        public class MessageEventArgs : EventArgs
        {
            /// <summary>
            /// сообщение
            /// </summary>
            public Message Message { get; private set; }

            /// <summary>
            /// конструктор
            /// </summary>
            public MessageEventArgs()
            {
            }

            /// <summary>
            /// конструктор
            /// </summary>
            /// <param name="msg"> сообщение </param>
            public MessageEventArgs(Message msg)
            {
                this.Message = msg;
            }
        }

        public delegate void MessageEventHandler(object sender, MessageEventArgs e);

        #endregion

        public class ImprovedRichTextBox : RichTextBox
        {
            #region WinAPI

            private const int WM_HSCROLL = 276;
            private const int WM_VSCROLL = 277;

            private const int SB_HORZ = 0;
            private const int SB_VERT = 1;

            [DllImport("user32.dll")]
            public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

            #endregion

            #region Constructors

            /// <summary>
            /// конструктор
            /// </summary>
            public ImprovedRichTextBox()
            {
            }

            #endregion

            #region Events

            public event MessageEventHandler Scroll;

            #endregion

            #region Protected methods

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_HSCROLL || m.Msg == WM_VSCROLL)
                {
                    OnScroll(m);
                }

                base.WndProc(ref m);
            }

            /// <summary>
            /// вызов события 'Scroll'
            /// </summary>
            /// <param name="m"></param>
            protected virtual void OnScroll(Message m)
            {
                if (Scroll != null) Scroll(this, new MessageEventArgs(m));
            }

            #endregion
            #region Public methods

            /// <summary>
            /// послать событие прокрутки
            /// </summary>
            /// <param name="m"></param>
            public void SendScrollMessage(Message m)
            {
                base.WndProc(ref m);

                // прокрутка
                switch (m.Msg)
                {
                    case WM_HSCROLL:
                        SetScrollPos(Handle, SB_HORZ, m.WParam.ToInt32() >> 16, true);
                        break;
                    case WM_VSCROLL:
                        SetScrollPos(Handle, SB_VERT, m.WParam.ToInt32() >> 16, true);
                        break;
                }
            }

            #endregion
        }
        #region Scrolling

        private bool isScrolling = false;       // признак прокрутки контрола

        /// <summary>
        /// прокрутка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RTF_Scroll(object sender, MessageEventArgs e)
        {
            if (!isScrolling)
            {
                isScrolling = true;

                ImprovedRichTextBox senderRtf = sender as ImprovedRichTextBox;
                ImprovedRichTextBox rtf = senderRtf == inputRichTextBox ? lineCountRichTextBox : inputRichTextBox;

                Message m = e.Message;
                m.HWnd = rtf.Handle;
                rtf.SendScrollMessage(m);

                isScrolling = false;
            }
        }

        #endregion

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                string filePath = files[0]; // Первый перетащенный файл
                // Далее можно выполнить необходимые операции с файлом
                // Например, открыть его или прочитать его содержимое
                string fileContent = File.ReadAllText(filePath); ;
                inputRichTextBox.Text = fileContent;
                // В этом примере мы просто выводим путь к файлу в MessageBox
                //MessageBox.Show(filePath, "Перетащенный файл");
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect= DragDropEffects.None;
            }
        }
    }

}