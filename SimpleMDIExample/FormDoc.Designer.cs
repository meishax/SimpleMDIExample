namespace SimpleMDIExample
{
    partial class FormDoc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Doc_richTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // Doc_richTextBox
            // 
            this.Doc_richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Doc_richTextBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Doc_richTextBox.Location = new System.Drawing.Point(0, 0);
            this.Doc_richTextBox.Name = "Doc_richTextBox";
            this.Doc_richTextBox.Size = new System.Drawing.Size(1014, 695);
            this.Doc_richTextBox.TabIndex = 0;
            this.Doc_richTextBox.Text = "";
            this.Doc_richTextBox.TextChanged += new System.EventHandler(this.Doc_richTextBox_TextChanged);
            // 
            // FormDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1014, 695);
            this.Controls.Add(this.Doc_richTextBox);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FormDoc";
            this.Text = "FormDoc";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox Doc_richTextBox;

    }
}