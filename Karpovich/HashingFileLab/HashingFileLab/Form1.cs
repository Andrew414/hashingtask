using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HashingFileLab
{
    public partial class Form1 : Form
    {
        private const string ProcessingLabel = "Processing...";
        public Form1()
        {
            InitializeComponent();

            numericUpDown.Minimum = 1024;
            numericUpDown.Maximum = 1048576;
            numericUpDown.ValueChanged += NumericUpDownOnLostFocus;

            comboBox1.SelectedIndex = (int) AlgorithmType.Crc32;
        }

        private void NumericUpDownOnLostFocus(object sender, EventArgs eventArgs)
        {
            var usersValue = (int)numericUpDown.Value;
            var n = Math.Round(Math.Log(usersValue, 2));
            numericUpDown.Value = (int)Math.Pow(2, n);
        }

        private void buttonChooseFile_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxFileInput.Text = openFileDialog.FileName;
            }
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                labelResult.Text = ProcessingLabel;
                buttonStart.Enabled = false;

                var type = (AlgorithmType)comboBox1.SelectedIndex;
                var input = textBoxFileInput.Text;
                var fileInfo = new FileInfo(input);
                var output = $"{fileInfo.DirectoryName}/{fileInfo.Name}.hashes_{type.ToString().ToLower()}";
                var isAsync = checkBoxIsAsync.Checked;
                var blockSize = (int)numericUpDown.Value;

                var hashService = new HashService(output, input, blockSize, isAsync, type);

                var result = string.Empty;
                await Task.Run(() => result = hashService.HashFile() + "");

                labelResult.Text = result;
            }
            catch (Exception exception)
            {
                labelResult.Text = exception.Message;
            }
            finally
            {
                buttonStart.Enabled = true;
            }
            
        }
    }
}
