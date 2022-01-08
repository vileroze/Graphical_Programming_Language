
namespace GraphicalProgrammingLanguage
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.commandLine = new System.Windows.Forms.TextBox();
            this.runCode = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawingArea = new System.Windows.Forms.PictureBox();
            this.errorDisplayBox = new System.Windows.Forms.RichTextBox();
            this.codeArea = new System.Windows.Forms.RichTextBox();
            this.displayLineNumber = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.debugButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawingArea)).BeginInit();
            this.SuspendLayout();
            // 
            // commandLine
            // 
            this.commandLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.commandLine.BackColor = System.Drawing.Color.AliceBlue;
            this.commandLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.commandLine.Font = new System.Drawing.Font("Cascadia Code SemiBold", 10F, System.Drawing.FontStyle.Bold);
            this.commandLine.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.commandLine.Location = new System.Drawing.Point(33, 728);
            this.commandLine.Margin = new System.Windows.Forms.Padding(2);
            this.commandLine.Name = "commandLine";
            this.commandLine.Size = new System.Drawing.Size(193, 23);
            this.commandLine.TabIndex = 2;
            this.commandLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.commandLine_KeyDown);
            // 
            // runCode
            // 
            this.runCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.runCode.BackColor = System.Drawing.Color.NavajoWhite;
            this.runCode.Enabled = false;
            this.runCode.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runCode.Location = new System.Drawing.Point(230, 727);
            this.runCode.Margin = new System.Windows.Forms.Padding(2);
            this.runCode.Name = "runCode";
            this.runCode.Size = new System.Drawing.Size(80, 25);
            this.runCode.TabIndex = 3;
            this.runCode.Text = "EXECUTE";
            this.runCode.UseVisualStyleBackColor = false;
            this.runCode.Click += new System.EventHandler(this.runCode_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.NavajoWhite;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1708, 30);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.Image = global::GraphicalProgrammingLanguage.Properties.Resources.Documents_icon;
            this.fileToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(68, 28);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.BackColor = System.Drawing.Color.NavajoWhite;
            this.saveToolStripMenuItem.Image = global::GraphicalProgrammingLanguage.Properties.Resources.Save_icon;
            this.saveToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(160, 24);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.BackColor = System.Drawing.Color.NavajoWhite;
            this.loadToolStripMenuItem.Image = global::GraphicalProgrammingLanguage.Properties.Resources.open_file_icon;
            this.loadToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(160, 24);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.Color.NavajoWhite;
            this.exitToolStripMenuItem.Image = global::GraphicalProgrammingLanguage.Properties.Resources.Log_Out_icon;
            this.exitToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(160, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // drawingArea
            // 
            this.drawingArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drawingArea.BackColor = System.Drawing.Color.MistyRose;
            this.drawingArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drawingArea.Location = new System.Drawing.Point(392, 42);
            this.drawingArea.Margin = new System.Windows.Forms.Padding(2);
            this.drawingArea.Name = "drawingArea";
            this.drawingArea.Size = new System.Drawing.Size(1305, 562);
            this.drawingArea.TabIndex = 0;
            this.drawingArea.TabStop = false;
            this.drawingArea.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawingArea_Paint);
            // 
            // errorDisplayBox
            // 
            this.errorDisplayBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorDisplayBox.BackColor = System.Drawing.Color.OldLace;
            this.errorDisplayBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorDisplayBox.ForeColor = System.Drawing.Color.Red;
            this.errorDisplayBox.Location = new System.Drawing.Point(392, 608);
            this.errorDisplayBox.Margin = new System.Windows.Forms.Padding(2);
            this.errorDisplayBox.Name = "errorDisplayBox";
            this.errorDisplayBox.ReadOnly = true;
            this.errorDisplayBox.Size = new System.Drawing.Size(1305, 145);
            this.errorDisplayBox.TabIndex = 4;
            this.errorDisplayBox.Text = "";
            // 
            // codeArea
            // 
            this.codeArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.codeArea.BackColor = System.Drawing.Color.AliceBlue;
            this.codeArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.codeArea.Font = new System.Drawing.Font("Cascadia Code SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeArea.Location = new System.Drawing.Point(33, 42);
            this.codeArea.Name = "codeArea";
            this.codeArea.Size = new System.Drawing.Size(354, 686);
            this.codeArea.TabIndex = 1;
            this.codeArea.Text = "";
            this.codeArea.VScroll += new System.EventHandler(this.codeArea_VScroll);
            this.codeArea.KeyUp += new System.Windows.Forms.KeyEventHandler(this.codeArea_KeyUp);
            // 
            // displayLineNumber
            // 
            this.displayLineNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.displayLineNumber.BackColor = System.Drawing.Color.AliceBlue;
            this.displayLineNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.displayLineNumber.Enabled = false;
            this.displayLineNumber.Font = new System.Drawing.Font("Cascadia Code SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayLineNumber.Location = new System.Drawing.Point(3, 42);
            this.displayLineNumber.Multiline = true;
            this.displayLineNumber.Name = "displayLineNumber";
            this.displayLineNumber.ReadOnly = true;
            this.displayLineNumber.Size = new System.Drawing.Size(31, 686);
            this.displayLineNumber.TabIndex = 6;
            this.displayLineNumber.WordWrap = false;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(394, 610);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(1302, 17);
            this.textBox2.TabIndex = 7;
            this.textBox2.Text = " Error List";
            // 
            // debugButton
            // 
            this.debugButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.debugButton.BackColor = System.Drawing.Color.NavajoWhite;
            this.debugButton.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debugButton.Location = new System.Drawing.Point(314, 728);
            this.debugButton.Margin = new System.Windows.Forms.Padding(2);
            this.debugButton.Name = "debugButton";
            this.debugButton.Size = new System.Drawing.Size(73, 25);
            this.debugButton.TabIndex = 8;
            this.debugButton.Text = "DEBUG";
            this.debugButton.UseVisualStyleBackColor = false;
            this.debugButton.Click += new System.EventHandler(this.debugButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1708, 763);
            this.Controls.Add(this.debugButton);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.displayLineNumber);
            this.Controls.Add(this.codeArea);
            this.Controls.Add(this.errorDisplayBox);
            this.Controls.Add(this.runCode);
            this.Controls.Add(this.drawingArea);
            this.Controls.Add(this.commandLine);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Graphical Programming Language";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawingArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox commandLine;
        private System.Windows.Forms.Button runCode;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.PictureBox drawingArea;
        private System.Windows.Forms.RichTextBox errorDisplayBox;
        private System.Windows.Forms.RichTextBox codeArea;
        private System.Windows.Forms.TextBox displayLineNumber;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button debugButton;
    }
}

