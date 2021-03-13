using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;

namespace SimpleMDIExample
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        float FontSize = 12;
        bool IsChenged = false;

        private void FormMain_Load(object sender, EventArgs e)
        {
            //获取所有字体
            Font_toolStripComboBox.Items.Clear();
            InstalledFontCollection installedFontCollection = new InstalledFontCollection();
            FontFamily[] fontFamily = installedFontCollection.Families;
            foreach (FontFamily ff in fontFamily)
                Font_toolStripComboBox.Items.Add(ff.GetName(1));
            //this.Font_toolStripComboBox.SelectedIndex = 0;
            LayoutMdi(MdiLayout.Cascade);
            Text = "多文档文本编辑器";
            WindowState = FormWindowState.Maximized;

        }

        private void 窗口层叠ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
            this.窗口层叠ToolStripMenuItem.Checked = true;
            this.垂直平铺ToolStripMenuItem.Checked = false;
            this.水平平铺ToolStripMenuItem.Checked = false;
            //_MdiStyle = 0;
        }

        private void 水平平铺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
            this.窗口层叠ToolStripMenuItem.Checked = false;
            this.垂直平铺ToolStripMenuItem.Checked = false;
            this.水平平铺ToolStripMenuItem.Checked = true;
            //_MdiStyle = 1;
        }

        private void 垂直平铺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
            this.窗口层叠ToolStripMenuItem.Checked = false;
            this.垂直平铺ToolStripMenuItem.Checked = true;
            this.水平平铺ToolStripMenuItem.Checked = false;
            //_MdiStyle = 2;
        }
        /// <summary>
        /// 新建按钮
        /// </summary>
        private void NewDoc()
        {
            if (_Num <= 10)
            {
                FormDoc formDoc = new FormDoc
                {
                    MdiParent = this,
                    Text = "Doc" + _Num,
                    WindowState = FormWindowState.Maximized
                };
                formDoc.Show();
                formDoc.Activate();
                _Num++;
            }
            
        }
        private void New_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDoc();
        }
        private void 新建NToolStripButton_Click(object sender, EventArgs e)
        {
            NewDoc();
        }
        /// <summary>
        /// 打开文档
        /// </summary>
        private void OpenDoc()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "RTF格式(*.rtf)|*.rtf|文本文件(*.txt)|*.txt|所有文件(*.*)|*.*",
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //新建一个窗口
                    NewDoc();
                    _Num--;

                    if (openFileDialog.FilterIndex == 1)
                        ((FormDoc)this.ActiveMdiChild).Doc_richTextBox.LoadFile
                            (openFileDialog.FileName, RichTextBoxStreamType.RichText);
                    else
                        ((FormDoc)this.ActiveMdiChild).Doc_richTextBox.LoadFile
                            (openFileDialog.FileName, RichTextBoxStreamType.PlainText);

                    ((FormDoc)this.ActiveMdiChild).Text = openFileDialog.FileName;
                }
                catch
                {
                    MessageBox.Show("打开失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Open_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDoc();
        }
        private void 打开OToolStripButton_Click(object sender, EventArgs e)
        {
            OpenDoc();
        }
        /// <summary>
        /// 保存文档
        /// </summary>
        private void SaveDoc()
        {
            //判断新文件还是打开已有文档
            //正则匹配
            string DocFirst = @"^Doc";
            Regex myRegex = new Regex(DocFirst);

            if (this.MdiChildren.Count() > 0)
            {
                if (myRegex.IsMatch(((FormDoc)this.ActiveMdiChild).Text) )//正则匹配成功，新建保存
                {
                   
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "RTF格式(*.rtf)|*.rtf|文本文件(*.txt)|*.txt|所有文件(*.*)|*.*"
                    };
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            if (saveFileDialog.FilterIndex == 1)
                                ((FormDoc)this.ActiveMdiChild).Doc_richTextBox.
                                    SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                            else
                                ((FormDoc)this.ActiveMdiChild).Doc_richTextBox.
                                    SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);

                            MessageBox.Show("保存成功！", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                        catch
                        {
                            MessageBox.Show("保存失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    saveFileDialog.Dispose();
                }
                else //正则匹配失败，直接按原路径保存
                {
                    try
                    {
                        ((FormDoc)this.ActiveMdiChild).Doc_richTextBox.
                        SaveFile(((FormDoc)this.ActiveMdiChild).Text, RichTextBoxStreamType.RichText);
                        MessageBox.Show("保存成功！", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    catch
                    {
                        MessageBox.Show("保存失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                
            }

        }
        private void Save_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDoc();
        }
        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            SaveDoc();
        }
        /// <summary>
        /// 关闭文档
        /// </summary>
        private void CloseDoc()
        {
            if (this.MdiChildren.Count() > 0)
            {
                if (MessageBox.Show("确定退出？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    foreach (FormDoc fd in this.MdiChildren)
                        fd.Close();
                    Application.Exit();
                }
            }
        }
        private void Close_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseDoc();
        }
        /// <summary>
        /// 字体
        /// </summary>
        private void Font_toolStripComboBox_TextChanged(object sender, EventArgs e)
        {
            if (this.MdiChildren.Count() > 0)
            {
                RichTextBox Temp_RichTextBox = new RichTextBox();
                int RtbStart = ((FormDoc)this.ActiveMdiChild).Doc_richTextBox.SelectionStart;
                int RtbLen = ((FormDoc)this.ActiveMdiChild).Doc_richTextBox.SelectionLength;
                int Temp_RtbStart = 0;

                Font font = ((FormDoc)this.ActiveMdiChild).Doc_richTextBox.SelectionFont;
                if (RtbLen <= 0 && null != font)
                {
                    ((FormDoc)this.ActiveMdiChild).Doc_richTextBox.Font = new Font(Font_toolStripComboBox.Text, font.Size, font.Style);
                    return;
                }
                Temp_RichTextBox.Rtf = ((FormDoc)this.ActiveMdiChild).Doc_richTextBox.SelectedRtf;
                for (int i = 0; i < RtbLen; i++)
                {
                    Temp_RichTextBox.Select(Temp_RtbStart + i, 1);
                    Temp_RichTextBox.SelectionFont = new Font(Font_toolStripComboBox.Text,
                        Temp_RichTextBox.SelectionFont.Size, Temp_RichTextBox.SelectionFont.Style);
                }
                Temp_RichTextBox.Select(Temp_RtbStart, RtbLen);
                ((FormDoc)this.ActiveMdiChild).Doc_richTextBox.SelectedRtf = Temp_RichTextBox.SelectedRtf;
                ((FormDoc)this.ActiveMdiChild).Doc_richTextBox.Select(Temp_RtbStart, RtbLen);
                ((FormDoc)this.ActiveMdiChild).Doc_richTextBox.Focus();
                Temp_RichTextBox.Dispose();

            }
        }

        /// <summary>
        /// 设置改变字体属性的方法
        /// </summary>
        /// <param name="_RTB">设置字体的选中文字</param>
        /// <param name="_Style">设置字体的样式（FontFamily，Bold，FontSize，I，U）</param>
        private void ChangeRTBFontstyle(RichTextBox _RTB, FontStyle _Style)
        {
            if (this.MdiChildren.Count() > 0)
            {
                if (_RTB.SelectedText != "")
                {
                    Font oldfont = _RTB.SelectionFont;
                    if (oldfont != null)
                    {
                        if (oldfont.Bold || oldfont.Italic || oldfont.Underline)
                            _RTB.SelectionFont
                                = new Font(oldfont, oldfont.Style ^ _Style);
                        else _RTB.SelectionFont
                                = new Font(oldfont, oldfont.Style | _Style);
                    }
                    
                }
                else
                {
                    Font oldfont = ((FormDoc)ActiveMdiChild).Doc_richTextBox.SelectionFont;
                    if (oldfont.Bold || oldfont.Italic || oldfont.Underline)
                        ((FormDoc)this.ActiveMdiChild).Doc_richTextBox.Font
                            = new Font(oldfont, oldfont.Style ^ _Style);
                    else ((FormDoc)this.ActiveMdiChild).Doc_richTextBox.Font
                            = new Font(oldfont, oldfont.Style | _Style);
                }
            }
        }

        //粗体
        private void Bold_tSBtn_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Count() > 0)
                ChangeRTBFontstyle(((FormDoc)this.ActiveMdiChild).Doc_richTextBox, FontStyle.Bold);
        }

        //斜体
        private void Italic_toolStripButton_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Count() > 0)
                ChangeRTBFontstyle(((FormDoc)this.ActiveMdiChild).Doc_richTextBox, FontStyle.Italic);
        }

        //下划线
        private void Underline_toolStripButton_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Count() > 0)
                ChangeRTBFontstyle(((FormDoc)this.ActiveMdiChild).Doc_richTextBox, FontStyle.Underline);
        }

        //字体大小
        private void ChangFontSize(float fontSize)
        {
            if (fontSize <= 0.0)
                throw new InvalidProgramException("字号参数错误");
            RichTextBox _RTB = ((FormDoc)this.ActiveMdiChild).Doc_richTextBox;
            int _Start = _RTB.SelectionStart;
            int len = _RTB.SelectionLength;
            RichTextBox Temp_RTB = new RichTextBox();
            int tempRtbStart = 0;
            Font font = _RTB.SelectionFont;
            if (len <= 1 && font != null)
            {
                _RTB.SelectionFont = new Font(font.Name, fontSize, font.Style);
                return;
            }
            Temp_RTB.Rtf = _RTB.SelectedRtf;
            for (int i = 0; i < len; i++)
            {
                Temp_RTB.Select(tempRtbStart + i, 1);
                Temp_RTB.SelectionFont = new Font(Temp_RTB.SelectionFont.Name, fontSize, Temp_RTB.SelectionFont.Style);
            }
            Temp_RTB.Select(tempRtbStart, len);
            _RTB.SelectedRtf = Temp_RTB.SelectedRtf;
            _RTB.Select(_Start, len);
            _RTB.Focus();
        }
        //字体大小选择
        private void FontSize_toolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FontSize = (int)FontSize_toolStripComboBox.SelectedIndex + 8;
            ChangFontSize(FontSize);
        }
        //字体大小增大
        private void SizeUp_toolStripButton_Click(object sender, EventArgs e)
        {

            if (FontSize_toolStripComboBox.SelectedIndex < 16)
                FontSize_toolStripComboBox.SelectedIndex++;
        }
        //字体大小减小
        private void SizeDown_toolStripButton_Click(object sender, EventArgs e)
        {
            if (FontSize_toolStripComboBox.SelectedIndex > 0)
                FontSize_toolStripComboBox.SelectedIndex--;
        }
        /// <summary>
        /// 左对齐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Left_toolStripButton_Click(object sender, EventArgs e)
        {
            ((FormDoc)ActiveMdiChild).Doc_richTextBox.SelectionAlignment = HorizontalAlignment.Left;

        }
        /// <summary>
        /// 居中对齐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Center_toolStripButton_Click(object sender, EventArgs e)
        {
            ((FormDoc)ActiveMdiChild).Doc_richTextBox.SelectionAlignment = HorizontalAlignment.Center;
        }
        /// <summary>
        /// 右对齐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Right_toolStripButton_Click(object sender, EventArgs e)
        {
            ((FormDoc)ActiveMdiChild).Doc_richTextBox.SelectionAlignment = HorizontalAlignment.Right;
        }
        /// <summary>
        /// 搜索按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_toolStripButton_Click(object sender, EventArgs e)
        {
            if (Search_toolStripTextBox.Text == ""||!IsChenged)
            {
                MessageBox.Show("请输入需要查找的内容！");
                Search_toolStripTextBox.Text = "在这里输入搜索内容";
                this.Search_toolStripTextBox.ForeColor = System.Drawing.SystemColors.WindowFrame;
                IsChenged = false;
            }
            else
            {
                if (!FindMyText(Search_toolStripTextBox.Text))
                {
                    MessageBox.Show("未找到指定文本！");
                }
            }
        }
        /// <summary>
        /// 搜索函数
        /// </summary>
        /// <param name="text">搜索的字符串</param>
        /// <returns></returns>
        private bool FindMyText(string text)
        {
            bool returnValue = false;

            if (text.Length > 0&& ((FormDoc)ActiveMdiChild)!=null)
            {
                int indexToText = ((FormDoc)ActiveMdiChild).Doc_richTextBox.Find(text);

                if (indexToText >= 0)
                {
                    ((FormDoc)ActiveMdiChild).Doc_richTextBox.Select(indexToText, text.Length);
                    ((FormDoc)ActiveMdiChild).Doc_richTextBox.Focus();
                    returnValue = true;
                }
            }
            else
            {
                MessageBox.Show("请打开有效文件");
            }

            return returnValue;
        }
        /// <summary>
        /// textbox控件回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_toolStripTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_toolStripButton_Click(sender, e);
            }
        }
        /// <summary>
        /// 第一次点击清空内容，textbox点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_toolStripTextBox_Click(object sender, EventArgs e)
        {
            if (!IsChenged)
            {
                Search_toolStripTextBox.Text = "";
            }
        }
        /// <summary>
        /// textbox点击后内容发生改变事件函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_toolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!IsChenged)
            {
                IsChenged = true;
                this.Search_toolStripTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send("^x");//键盘映射
            //也可以使用Cut Paste 的类函数
        }
        private void 剪切UToolStripButton_Click(object sender, EventArgs e)
        {
            SendKeys.Send("^x");
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send("^c");
        }
        private void 复制CToolStripButton_Click(object sender, EventArgs e)
        {
            SendKeys.Send("^c");
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send("^v");
        }
        private void 粘贴PToolStripButton_Click(object sender, EventArgs e)
        {
            SendKeys.Send("^v");
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((FormDoc)ActiveMdiChild).Doc_richTextBox.SelectedText = "";
        }

        private void 撤回ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((FormDoc)ActiveMdiChild).Doc_richTextBox.Undo();
        }
        private void Undo_toolStripButton_Click(object sender, EventArgs e)
        {
            ((FormDoc)ActiveMdiChild).Doc_richTextBox.Undo();
        }

        private void 重做ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((FormDoc)ActiveMdiChild).Doc_richTextBox.Redo();
        }
        private void Redo_toolStripButton_Click(object sender, EventArgs e)
        {
            ((FormDoc)ActiveMdiChild).Doc_richTextBox.Redo();
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((FormDoc)ActiveMdiChild).Doc_richTextBox.SelectAll();
        }


    }
}
