using System;
using System.Collections.Generic;
using System.Linq;

namespace ShiftCipherEncryptorDecryptor
{
    public static class StringDecryptorExtention
    {
        private static string _alphabetOrderedByFrequencyOfUsing = "etaoinshrdlcumwfgypbvkjxqz";
        private static string _alphabet = "abcdefghijklmnopqrstuvwxyz";
        private static uint _lettersAmountForDecrypt = 5u;

        public static string ShiftChipherDecrypt(this string stringToDecrypt, byte shiftKey)
        {
            var stringToDecryptCharsFrequency = _getCharsFrequency(stringToDecrypt, orderByFrequency: true);

            _lettersAmountForDecrypt = _lettersAmountForDecrypt > stringToDecryptCharsFrequency.Count
                ? (uint)stringToDecryptCharsFrequency.Count
                : _lettersAmountForDecrypt;

            var theMostFrequentLettersFromStringToDecrypt = stringToDecryptCharsFrequency
                .Take((int)_lettersAmountForDecrypt)
                .Select(_ => _.Key)
                .ToArray();

            var theMostFrequentLettersFromAlphabet = _alphabetOrderedByFrequencyOfUsing
                .Take((int)_lettersAmountForDecrypt)
                .ToArray();

            int[,] matrixOfFrequency = new int[_lettersAmountForDecrypt, _lettersAmountForDecrypt];

            for (var i = 0; i < theMostFrequentLettersFromStringToDecrypt.Length; i++)
            {
                var indexOfFirstLetter = _alphabet.IndexOf(theMostFrequentLettersFromStringToDecrypt[i]);

                for (var j = 0; j < theMostFrequentLettersFromAlphabet.Length; j++)
                {
                    var indexOfSecondLetter = _alphabet.IndexOf(theMostFrequentLettersFromAlphabet[j]);
                    
                    matrixOfFrequency[i, j] = indexOfFirstLetter - indexOfSecondLetter + (indexOfFirstLetter < indexOfSecondLetter ? _alphabet.Length : 0);
                }
            }

            var theMostFrequentShiftKey = matrixOfFrequency.Cast<int>()
                .ToArray()
                .GroupBy(_ => _)
                .OrderByDescending(_ => _.Count())
                .FirstOrDefault()?.Key;

            if (theMostFrequentShiftKey == null)
                throw new InvalidOperationException();

            return stringToDecrypt.ShiftChipherEncrypt((byte)(_alphabet.Length - theMostFrequentShiftKey));
        }

        private static Dictionary<char, uint> _getCharsFrequency(
            string stringToAnalyze,
            bool ignoreCase = true,
            bool orderByFrequency = false,
            bool mapToAlphabet = false)
        {
            var charsFrequency = mapToAlphabet ? _alphabet.ToDictionary(_ => _, _ => 0u) : new Dictionary<char, uint>();

            if (ignoreCase)
            {
                stringToAnalyze = stringToAnalyze.ToLower();
            }

            foreach (var item in stringToAnalyze)
            {
                if (!_alphabet.Contains(char.ToLower(item))) continue;

                if (mapToAlphabet || charsFrequency.Any(_ => _.Key.Equals(item)))
                {
                    charsFrequency[item]++;
                }
                else
                {
                    charsFrequency[item] = 1;
                }
            }

            return orderByFrequency ? charsFrequency.OrderByDescending(_ => _.Value).ToDictionary(_ => _.Key, _ => _.Value) : charsFrequency;
        }
    }
}
