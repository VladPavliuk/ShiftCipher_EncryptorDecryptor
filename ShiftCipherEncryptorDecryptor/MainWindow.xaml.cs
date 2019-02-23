using System.Windows;
using System.Windows.Controls;

namespace ShiftCipherEncryptorDecryptor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private byte _shiftKey = 15;
        private bool _isShiftKeyValue_IsValid = false;
        private bool _isTextToEncryptKeyValue_IsValid = false;

        private bool _isTextToDecryptKeyValue_IsValid = false;

        public MainWindow()
        {
            InitializeComponent();

            EncryptButton.IsEnabled = false;
            DecryptButton.IsEnabled = false;
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (TextBlockToEncrypt.Text.Length > 0)
            {
                EncryptedTextBlock.Text = TextBlockToEncrypt.Text.ShiftChipherEncrypt(_shiftKey);
            }
            else
            {
                EncryptedTextBlock.Text = string.Empty;
            }
        }

        private void TextBlockShiftKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            var insertedText = (sender as TextBox).Text;
            byte shiftKey = 0;
            _isShiftKeyValue_IsValid = false;

            if (insertedText.Length > 0
                && byte.TryParse(insertedText, out shiftKey)
                && shiftKey >= 0)
            {
                _shiftKey = shiftKey;
                _isShiftKeyValue_IsValid = true;
            }

            CheckEncryptButtonAvaliability();
        }

        private void TextBlockToEncrypt_TextChanged(object sender, TextChangedEventArgs e)
        {
            var insertedText = ((TextBox)sender).Text;

            if (insertedText.Length > 0)
            {
                _isTextToEncryptKeyValue_IsValid = true;
            }
            else
            {
                _isTextToEncryptKeyValue_IsValid = false;
            }

            CheckEncryptButtonAvaliability();
        }

        private void CheckEncryptButtonAvaliability()
        {
            if (EncryptButton != null)
                EncryptButton.IsEnabled = _isTextToEncryptKeyValue_IsValid && _isShiftKeyValue_IsValid;
        }

        private void TextBlockToDecrypt_TextChanged(object sender, TextChangedEventArgs e)
        {
            var insertedText = ((TextBox)sender).Text;

            if (insertedText.Length > 0)
            {
                _isTextToDecryptKeyValue_IsValid = true;
            }
            else
            {
                _isTextToDecryptKeyValue_IsValid = false;
            }

            CheckDecryptButtonAvaliability();
        }

        private void CheckDecryptButtonAvaliability()
        {
            if (DecryptButton != null)
                DecryptButton.IsEnabled = _isTextToDecryptKeyValue_IsValid;
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (TextBlockToDecrypt.Text.Length > 0)
            {
                DecryptedTextBlock.Text = TextBlockToDecrypt.Text.ShiftChipherDecrypt(_shiftKey);
            }
            else
            {
                DecryptedTextBlock.Text = string.Empty;
            }
        }
    }
}
