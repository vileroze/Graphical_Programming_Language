
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.codeArea = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DrawingArea = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DrawingArea)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // commandLine
            // 
            this.commandLine.Location = new System.Drawing.Point(9, 496);
            this.commandLine.Name = "commandLine";
            this.commandLine.Size = new System.Drawing.Size(211, 26);
            this.commandLine.TabIndex = 1;
            // 
            // runCode
            // 
            this.runCode.Location = new System.Drawing.Point(239, 492);
            this.runCode.Name = "runCode";
            this.runCode.Size = new System.Drawing.Size(139, 35);
            this.runCode.TabIndex = 2;
            this.runCode.Text = "EXECUTE";
            this.runCode.UseVisualStyleBackColor = true;
            this.runCode.Click += new System.EventHandler(this.runCode_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.codeArea);
            this.panel1.Controls.Add(this.runCode);
            this.panel1.Controls.Add(this.commandLine);
            this.panel1.Location = new System.Drawing.Point(3, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(388, 537);
            this.panel1.TabIndex = 0;
            // 
            // codeArea
            // 
            this.codeArea.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.codeArea.Font = new System.Drawing.Font("Cascadia Code SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeArea.ForeColor = System.Drawing.SystemColors.Window;
            this.codeArea.Location = new System.Drawing.Point(9, 14);
            this.codeArea.Name = "codeArea";
            this.codeArea.Size = new System.Drawing.Size(369, 472);
            this.codeArea.TabIndex = 3;
            this.codeArea.Text = "";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.DrawingArea);
            this.panel2.Location = new System.Drawing.Point(397, 51);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1047, 537);
            this.panel2.TabIndex = 1;
            // 
            // DrawingArea
            // 
            this.DrawingArea.BackColor = System.Drawing.Color.Silver;
            this.DrawingArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DrawingArea.Location = new System.Drawing.Point(16, 14);
            this.DrawingArea.Name = "DrawingArea";
            this.DrawingArea.Size = new System.Drawing.Size(1014, 508);
            this.DrawingArea.TabIndex = 0;
            this.DrawingArea.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1456, 33);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Image = global::GraphicalProgrammingLanguage.Properties.Resources.Documents_icon;
            this.fileToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(78, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::GraphicalProgrammingLanguage.Properties.Resources.Save_icon;
            this.saveToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(153, 34);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Image = global::GraphicalProgrammingLanguage.Properties.Resources.open_file_icon;
            this.loadToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(153, 34);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::GraphicalProgrammingLanguage.Properties.Resources.Log_Out_icon;
            this.exitToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(153, 34);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1456, 599);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Graphical Programming Language";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DrawingArea)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox commandLine;
        private System.Windows.Forms.Button runCode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox DrawingArea;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.RichTextBox codeArea;
    }
}

