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
            this.btnRemoveInputFile = new System.Windows.Forms.Button();
            this.lstInputFiles = new System.Windows.Forms.ListBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.chkReverse = new System.Windows.Forms.CheckBox();
            this.chkZeroInvalidData = new System.Windows.Forms.CheckBox();
            this.grpInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddInputFile
            // 
            this.btnAddInputFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddInputFile.Location = new System.Drawing.Point(436, 30);
            this.btnAddInputFile.Name = "btnAddInputFile";
            this.btnAddInputFile.Size = new System.Drawing.Size(112, 34);
            this.btnAddInputFile.TabIndex = 0;
            this.btnAddInputFile.Text = "Add";
            this.btnAddInputFile.UseVisualStyleBackColor = true;
            this.btnAddInputFile.Click += new System.EventHandler(this.BtnAddInputFile_Click);
            // 
            // grpInput
            // 
            this.grpInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpInput.Controls.Add(this.btnRemoveInputFile);
            this.grpInput.Controls.Add(this.lstInputFiles);
            this.grpInput.Controls.Add(this.btnAddInputFile);
            this.grpInput.Location = new System.Drawing.Point(12, 12);
            this.grpInput.Name = "grpInput";
            this.grpInput.Size = new System.Drawing.Size(554, 306);
            this.grpInput.TabIndex = 1;
            this.grpInput.TabStop = false;
            this.grpInput.Text = "Input files";
            // 
            // btnRemoveInputFile
            // 
            this.btnRemoveInputFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveInputFile.Enabled = false;
            this.btnRemoveInputFile.Location = new System.Drawing.Point(436, 70);
            this.btnRemoveInputFile.Name = "btnRemoveInputFile";
            this.btnRemoveInputFile.Size = new System.Drawing.Size(112, 34);
            this.btnRemoveInputFile.TabIndex = 1;
            this.btnRemoveInputFile.Text = "Remove";
            this.btnRemoveInputFile.UseVisualStyleBackColor = true;
            this.btnRemoveInputFile.Click += new System.EventHandler(this.BtnRemoveInputFile_Click);
            // 
            // lstInputFiles
            // 
            this.lstInputFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstInputFiles.FormattingEnabled = true;
            this.lstInputFiles.ItemHeight = 25;
            this.lstInputFiles.Location = new System.Drawing.Point(6, 30);
            this.lstInputFiles.Name = "lstInputFiles";
            this.lstInputFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstInputFiles.Size = new System.Drawing.Size(424, 254);
            this.lstInputFiles.TabIndex = 0;
            this.lstInputFiles.SelectedIndexChanged += new System.EventHandler(this.LstInputFiles_SelectedIndexChanged);
            // 
            // btnConvert
            // 
            this.btnConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConvert.Enabled = false;
            this.btnConvert.Location = new System.Drawing.Point(454, 324);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(112, 34);
            this.btnConvert.TabIndex = 2;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.BtnConvert_Click);
            // 
            // chkReverse
            // 
            this.chkReverse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkReverse.AutoSize = true;
            this.chkReverse.Checked = true;
            this.chkReverse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReverse.Location = new System.Drawing.Point(254, 328);
            this.chkReverse.Name = "chkReverse";
            this.chkReverse.Size = new System.Drawing.Size(188, 29);
            this.chkReverse.TabIndex = 3;
            this.chkReverse.Text = "Ascending timeline";
            this.chkReverse.UseVisualStyleBackColor = true;
            // 
            // chkZeroInvalidData
            // 
            this.chkZeroInvalidData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkZeroInvalidData.AutoSize = true;
            this.chkZeroInvalidData.Checked = true;
            this.chkZeroInvalidData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkZeroInvalidData.Location = new System.Drawing.Point(12, 328);
            this.chkZeroInvalidData.Name = "chkZeroInvalidData";
            this.chkZeroInvalidData.Size = new System.Drawing.Size(224, 29);
            this.chkZeroInvalidData.TabIndex = 4;
            this.chkZeroInvalidData.Text = "Apply 0 for invalid data";
            this.chkZeroInvalidData.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 369);
            this.Controls.Add(this.chkZeroInvalidData);
            this.Controls.Add(this.chkReverse);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.grpInput);
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSV to GPX file conversion";
            this.grpInput.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnAddInputFile;
        private GroupBox grpInput;
        private ListBox lstInputFiles;
        private Button btnRemoveInputFile;
        private Button btnConvert;
        private CheckBox chkReverse;
        private CheckBox chkZeroInvalidData;
    }
}