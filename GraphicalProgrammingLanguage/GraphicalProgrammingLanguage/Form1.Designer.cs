
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
            this.label1 = new System.Windows.Forms.Label();
            this.codeArea = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawingArea)).BeginInit();
            this.SuspendLayout();
            // 
            // commandLine
            // 
            this.commandLine.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.commandLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.commandLine.Font = new System.Drawing.Font("Cascadia Mono", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commandLine.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.commandLine.Location = new System.Drawing.Point(11, 581);
            this.commandLine.Margin = new System.Windows.Forms.Padding(2);
            this.commandLine.Name = "commandLine";
            this.commandLine.Size = new System.Drawing.Size(211, 20);
            this.commandLine.TabIndex = 1;
            this.commandLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.commandLine_KeyDown);
            // 
            // runCode
            // 
            this.runCode.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runCode.Location = new System.Drawing.Point(226, 575);
            this.runCode.Margin = new System.Windows.Forms.Padding(2);
            this.runCode.Name = "runCode";
            this.runCode.Size = new System.Drawing.Size(119, 32);
            this.runCode.TabIndex = 2;
            this.runCode.Text = "EXECUTE";
            this.runCode.UseVisualStyleBackColor = true;
            this.runCode.Click += new System.EventHandler(this.runCode_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1292, 30);
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(61, 28);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::GraphicalProgrammingLanguage.Properties.Resources.Save_icon;
            this.saveToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Image = global::GraphicalProgrammingLanguage.Properties.Resources.open_file_icon;
            this.loadToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::GraphicalProgrammingLanguage.Properties.Resources.Log_Out_icon;
            this.exitToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // drawingArea
            // 
            this.drawingArea.BackColor = System.Drawing.Color.MistyRose;
            this.drawingArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drawingArea.Location = new System.Drawing.Point(358, 42);
            this.drawingArea.Margin = new System.Windows.Forms.Padding(2);
            this.drawingArea.Name = "drawingArea";
            this.drawingArea.Size = new System.Drawing.Size(923, 479);
            this.drawingArea.TabIndex = 0;
            this.drawingArea.TabStop = false;
            this.drawingArea.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawingArea_Paint);
            // 
            // errorDisplayBox
            // 
            this.errorDisplayBox.BackColor = System.Drawing.Color.OldLace;
            this.errorDisplayBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorDisplayBox.ForeColor = System.Drawing.Color.Red;
            this.errorDisplayBox.Location = new System.Drawing.Point(358, 525);
            this.errorDisplayBox.Margin = new System.Windows.Forms.Padding(2);
            this.errorDisplayBox.Name = "errorDisplayBox";
            this.errorDisplayBox.ReadOnly = true;
            this.errorDisplayBox.Size = new System.Drawing.Size(923, 82);
            this.errorDisplayBox.TabIndex = 3;
            this.errorDisplayBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LightGray;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(360, 529);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Error List:";
            // 
            // codeArea
            // 
            this.codeArea.Font = new System.Drawing.Font("Cascadia Code SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeArea.Location = new System.Drawing.Point(12, 42);
            this.codeArea.Name = "codeArea";
            this.codeArea.Size = new System.Drawing.Size(341, 528);
            this.codeArea.TabIndex = 5;
            this.codeArea.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1292, 618);
            this.Controls.Add(this.codeArea);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.errorDisplayBox);
            this.Controls.Add(this.runCode);
            this.Controls.Add(this.drawingArea);
            this.Controls.Add(this.commandLine);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Graphical Programming Language";
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox codeArea;
    }
}

