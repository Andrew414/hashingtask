using System;
using System.Text;
using System.Windows.Forms;
using DataStructs;
using DataStructs.Types;
using Implements.Managers;
using Implements.Services;
using Implements.Validators;
using ValidationException = DataStructs.Exceptions.ValidationException;

namespace HashingApp
{
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();

        private void OpenFileDialog(object sender, EventArgs e)
        {
            FileDialog.FileName = string.Empty;
            FileDialog.ShowDialog();
        }

        private async void StartApp(object sender, EventArgs eventArgs)
        {
            SetErrorsVisibleSettings(false);

            var filepath = FileDialog.FileName;
            Enum.TryParse(ProgramModeComboBox.Text, out ProgramMode mode);
            Enum.TryParse(HashAlgorithmComboBox.Text, out HashAlgorithm algo);
            var blockSize = (int) BlockSizeNumericUpDown.Value;

            try
            {
                var opts = new HashOptions
                {
                    HashAlgorithm = algo,
                    ProgramMode = mode,
                    BlockSize = blockSize,
                    InputPath = filepath,
                };

                var hashManager = new HashManager(opts.HashAlgorithm);
                var validator = new HashOptionsValidator();
                var cryptoProvider = new LoggingCryptoService(validator, hashManager);
                await cryptoProvider.HashFileAsync(opts);
                var statistics = cryptoProvider.GetStatistics(opts.BlockSize);
                StatisticsRichTextBox.Text = BuildStatisticsString(statistics);
            }
            catch (ValidationException e)
            {
                ErrorsRichTextBox.Text = e.Message;
                SetErrorsVisibleSettings(true);
            }
        }

        private static string BuildStatisticsString((double avgRead, double avgHash, decimal read, decimal hash) statistics)
        {
            var sb = new StringBuilder();

            sb.Append($"Average read (MB/s): {statistics.avgRead}");
            sb.AppendLine();
            sb.Append($"Average hash (MB/s): {statistics.avgHash}");
            sb.AppendLine();
            sb.Append($"Read (Seconds): {statistics.read}");
            sb.AppendLine();
            sb.Append($"Hash (Seconds): {statistics.hash}");
            sb.AppendLine();

            return sb.ToString();
        }

        private void SetErrorsVisibleSettings(bool option)
        {
            ErrorsRichTextBox.Visible = option;
            ErrorsLabel.Visible = option;
        }
    }
}
