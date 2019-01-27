using System;

namespace ShiftCipherEncryptorDecryptor
{
    public static class StringEncryptorExtention
    {
        private static char[] _alphabet = new char[]
        {
            'a', 'b', 'c', 'd', 'e',
            'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o',
            'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y',
            'z'
        };

        public static string ShiftChipherEncrypt(this string stringToEncrypt, byte shiftKey)
        {
            var encryptedString = string.Empty;

            foreach (var charToEncrypt in stringToEncrypt)
            {
                var positionInTheAlphabet = Array.IndexOf(_alphabet, char.ToLower(charToEncrypt));

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
