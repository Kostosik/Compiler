﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Compiler.MainForm;

namespace Compiler
{
    internal class CustomTabPage:TabPage
    {
        public ImprovedRichTextBox inputRichTextBox;
        public ImprovedRichTextBox linesRichTextBox;
        public bool isFileSaved;

        public CustomTabPage(int index)
        {
            this.Resize += new System.EventHandler(this.myResize);
            this.Text = index.ToString();

            isFileSaved = true;

            inputRichTextBox = new ImprovedRichTextBox();
            linesRichTextBox = new ImprovedRichTextBox();
            inputRichTextBox.Parent = this;
            linesRichTextBox.Parent = this;
            linesRichTextBox.Text = 1.ToString();
            inputRichTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.newLineKey);
            inputRichTextBox.TextChanged += new System.EventHandler(textChanged);
        }

        public void newLineKey(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                linesRichTextBox.Text += "\n" + (linesRichTextBox.Lines.Length + 1).ToString();
            }
        }

        public void textChanged(object sender, EventArgs e)
        {
            this.isFileSaved = false;
        }

        public void Redo()
        {
            inputRichTextBox.Redo();
        }
        public void Undo()
        {
            inputRichTextBox.Undo();
        }
        public void Cut()
        {
            inputRichTextBox.Cut();
        }
        public void Copy()
        {
            inputRichTextBox.Copy();

        }

        public void myResize(object sender, EventArgs e)
        {
            inputRichTextBox.Size = new Size(this.Width - 30, this.Height);
            inputRichTextBox.Location = new Point(20, 0);
                        
            linesRichTextBox.Size = new Size(30, this.Height);
            linesRichTextBox.Location = new Point(0, 0);
            
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

        #region Improved RichTextBox
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
                //ImprovedRichTextBox rtf = senderRtf == inputRichTextBox ? lineCountRichTextBox : inputRichTextBox;

                Message m = e.Message;
                //m.HWnd = rtf.Handle;
                 //rtf.SendScrollMessage(m);

                isScrolling = false;
            }
        }

        #endregion
        #endregion
    }


}
