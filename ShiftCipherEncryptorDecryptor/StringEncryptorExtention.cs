using System;

namespace ShiftCipherEncryptorDecryptor
{
    public static class StringEncryptorExtention
    {
        private static string _alphabet = "abcdefghijklmnopqrstuvwxyz";

        public static string ShiftChipherEncrypt(this string stringToEncrypt, byte shiftKey)
        {
            var encryptedString = string.Empty;

            foreach (var charToEncrypt in stringToEncrypt)
            {
                var positionInTheAlphabet = _alphabet.IndexOf(char.ToLower(charToEncrypt));

                if (positionInTheAlphabet == -1)
                {
                    encryptedString += charToEncrypt;
                    continue;
                }

                var encryptedChar = _alphabet[(positionInTheAlphabet + shiftKey) % _alphabet.Length];

                encryptedString += char.IsUpper(charToEncrypt) ? char.ToUpper(encryptedChar) : encryptedChar;
            }

            return encryptedString;
        }
    }
}
