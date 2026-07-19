namespace TrackConversion
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAddInputFile = new System.Windows.Forms.Button();
            this.grpInput = new System.Windows.Forms.GroupBox();
            this.grpOutput = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.grpInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddInputFile
            // 
            this.btnAddInputFile.Location = new System.Drawing.Point(6, 265);
            this.btnAddInputFile.Name = "btnAddInputFile";
            this.btnAddInputFile.Size = new System.Drawing.Size(112, 34);
            this.btnAddInputFile.TabIndex = 0;
            this.btnAddInputFile.Text = "button1";
            this.btnAddInputFile.UseVisualStyleBackColor = true;
            // 
            // grpInput
            // 
            this.grpInput.Controls.Add(this.listBox1);
            this.grpInput.Controls.Add(this.btnAddInputFile);
            this.grpInput.Location = new System.Drawing.Point(12, 12);
            this.grpInput.Name = "grpInput";
            this.grpInput.Size = new System.Drawing.Size(456, 309);
            this.grpInput.TabIndex = 1;
            this.grpInput.TabStop = false;
            this.grpInput.Text = "Input files";
            // 
            // grpOutput
            // 
            this.grpOutput.Location = new System.Drawing.Point(474, 12);
            this.grpOutput.Name = "grpOutput";
            this.grpOutput.Size = new System.Drawing.Size(456, 309);
            this.grpOutput.TabIndex = 2;
            this.grpOutput.TabStop = false;
            this.grpOutput.Text = "Input files";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 25;
            this.listBox1.Location = new System.Drawing.Point(6, 30);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(444, 229);
            this.listBox1.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 906);
            this.Controls.Add(this.grpOutput);
            this.Controls.Add(this.grpInput);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CSV to GPX file conversion";
            this.grpInput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnAddInputFile;
        private GroupBox grpInput;
        private ListBox listBox1;
        private GroupBox grpOutput;
    }
}