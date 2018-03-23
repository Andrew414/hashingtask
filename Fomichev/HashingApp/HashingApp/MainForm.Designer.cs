namespace HashingApp
{
    partial class MainForm
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
            this.ProgramModeComboBox = new System.Windows.Forms.ComboBox();
            this.HashAlgorithmComboBox = new System.Windows.Forms.ComboBox();
            this.BlockSizeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.FileDialog = new System.Windows.Forms.OpenFileDialog();
            this.FileDialogButton = new System.Windows.Forms.Button();
            this.ProgramModeLabel = new System.Windows.Forms.Label();
            this.HashAlgorithmLabel = new System.Windows.Forms.Label();
            this.BlockSizeLabel = new System.Windows.Forms.Label();
            this.FilePathLabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.ErrorsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.ErrorsLabel = new System.Windows.Forms.Label();
            this.StatisticsRichTextBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.BlockSizeNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // ProgramModeComboBox
            // 
            this.ProgramModeComboBox.FormattingEnabled = true;
            this.ProgramModeComboBox.Items.AddRange(new object[] {
            "Async",
            "Sync"});
            this.ProgramModeComboBox.Location = new System.Drawing.Point(116, 111);
            this.ProgramModeComboBox.Name = "ProgramModeComboBox";
            this.ProgramModeComboBox.Size = new System.Drawing.Size(121, 21);
            this.ProgramModeComboBox.TabIndex = 0;
            this.ProgramModeComboBox.Text = "Async";
            // 
            // HashAlgorithmComboBox
            // 
            this.HashAlgorithmComboBox.FormattingEnabled = true;
            this.HashAlgorithmComboBox.Items.AddRange(new object[] {
            "Sha256",
            "Sha1",
            "Crc32",
            "Md5"});
            this.HashAlgorithmComboBox.Location = new System.Drawing.Point(259, 111);
            this.HashAlgorithmComboBox.Name = "HashAlgorithmComboBox";
            this.HashAlgorithmComboBox.Size = new System.Drawing.Size(121, 21);
            this.HashAlgorithmComboBox.TabIndex = 1;
            this.HashAlgorithmComboBox.Text = "Sha256";
            // 
            // BlockSizeNumericUpDown
            // 
            this.BlockSizeNumericUpDown.Location = new System.Drawing.Point(407, 112);
            this.BlockSizeNumericUpDown.Maximum = new decimal(new int[] {
            1048576,
            0,
            0,
            0});
            this.BlockSizeNumericUpDown.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.BlockSizeNumericUpDown.Name = "BlockSizeNumericUpDown";
            this.BlockSizeNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.BlockSizeNumericUpDown.TabIndex = 2;
            this.BlockSizeNumericUpDown.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // FileDialogButton
            // 
            this.FileDialogButton.Location = new System.Drawing.Point(552, 109);
            this.FileDialogButton.Name = "FileDialogButton";
            this.FileDialogButton.Size = new System.Drawing.Size(101, 23);
            this.FileDialogButton.TabIndex = 3;
            this.FileDialogButton.Text = "Choose file";
            this.FileDialogButton.UseVisualStyleBackColor = true;
            this.FileDialogButton.Click += new System.EventHandler(this.OpenFileDialog);
            // 
            // ProgramModeLabel
            // 
            this.ProgramModeLabel.AutoSize = true;
            this.ProgramModeLabel.Location = new System.Drawing.Point(142, 85);
            this.ProgramModeLabel.Name = "ProgramModeLabel";
            this.ProgramModeLabel.Size = new System.Drawing.Size(76, 13);
            this.ProgramModeLabel.TabIndex = 4;
            this.ProgramModeLabel.Text = "Program Mode";
            // 
            // HashAlgorithmLabel
            // 
            this.HashAlgorithmLabel.AutoSize = true;
            this.HashAlgorithmLabel.Location = new System.Drawing.Point(286, 85);
            this.HashAlgorithmLabel.Name = "HashAlgorithmLabel";
            this.HashAlgorithmLabel.Size = new System.Drawing.Size(78, 13);
            this.HashAlgorithmLabel.TabIndex = 5;
            this.HashAlgorithmLabel.Text = "Hash Algorithm";
            // 
            // BlockSizeLabel
            // 
            this.BlockSizeLabel.AutoSize = true;
            this.BlockSizeLabel.Location = new System.Drawing.Point(436, 85);
            this.BlockSizeLabel.Name = "BlockSizeLabel";
            this.BlockSizeLabel.Size = new System.Drawing.Size(54, 13);
            this.BlockSizeLabel.TabIndex = 6;
            this.BlockSizeLabel.Text = "BlockSize";
            // 
            // FilePathLabel
            // 
            this.FilePathLabel.AutoSize = true;
            this.FilePathLabel.Location = new System.Drawing.Point(578, 85);
            this.FilePathLabel.Name = "FilePathLabel";
            this.FilePathLabel.Size = new System.Drawing.Size(47, 13);
            this.FilePathLabel.TabIndex = 7;
            this.FilePathLabel.Text = "File path";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(343, 179);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 35);
            this.button2.TabIndex = 8;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.StartApp);
            // 
            // ErrorsRichTextBox
            // 
            this.ErrorsRichTextBox.Location = new System.Drawing.Point(131, 321);
            this.ErrorsRichTextBox.Name = "ErrorsRichTextBox";
            this.ErrorsRichTextBox.Size = new System.Drawing.Size(511, 89);
            this.ErrorsRichTextBox.TabIndex = 9;
            this.ErrorsRichTextBox.Text = "";
            this.ErrorsRichTextBox.Visible = false;
            // 
            // ErrorsLabel
            // 
            this.ErrorsLabel.AutoSize = true;
            this.ErrorsLabel.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ErrorsLabel.Location = new System.Drawing.Point(365, 295);
            this.ErrorsLabel.Name = "ErrorsLabel";
            this.ErrorsLabel.Size = new System.Drawing.Size(63, 23);
            this.ErrorsLabel.TabIndex = 10;
            this.ErrorsLabel.Text = "Errors";
            this.ErrorsLabel.Visible = false;
            // 
            // StatisticsRichTextBox
            // 
            this.StatisticsRichTextBox.Location = new System.Drawing.Point(525, 163);
            this.StatisticsRichTextBox.Name = "StatisticsRichTextBox";
            this.StatisticsRichTextBox.Size = new System.Drawing.Size(128, 96);
            this.StatisticsRichTextBox.TabIndex = 11;
            this.StatisticsRichTextBox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.StatisticsRichTextBox);
            this.Controls.Add(this.ErrorsLabel);
            this.Controls.Add(this.ErrorsRichTextBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.FilePathLabel);
            this.Controls.Add(this.BlockSizeLabel);
            this.Controls.Add(this.HashAlgorithmLabel);
            this.Controls.Add(this.ProgramModeLabel);
            this.Controls.Add(this.FileDialogButton);
            this.Controls.Add(this.BlockSizeNumericUpDown);
            this.Controls.Add(this.HashAlgorithmComboBox);
            this.Controls.Add(this.ProgramModeComboBox);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.BlockSizeNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ProgramModeComboBox;
        private System.Windows.Forms.ComboBox HashAlgorithmComboBox;
        private System.Windows.Forms.NumericUpDown BlockSizeNumericUpDown;
        private System.Windows.Forms.OpenFileDialog FileDialog;
        private System.Windows.Forms.Button FileDialogButton;
        private System.Windows.Forms.Label ProgramModeLabel;
        private System.Windows.Forms.Label HashAlgorithmLabel;
        private System.Windows.Forms.Label BlockSizeLabel;
        private System.Windows.Forms.Label FilePathLabel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox ErrorsRichTextBox;
        private System.Windows.Forms.Label ErrorsLabel;
        private System.Windows.Forms.RichTextBox StatisticsRichTextBox;
    }
}

