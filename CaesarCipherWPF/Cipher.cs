using System;
using System.Text;

namespace CaesarCipherWPF
{
    class Cipher
    {
        private string _alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        private string _ALPHABET = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        /// <summary>
        /// Зашифровывает текст с указаннием ключа
        /// </summary>
        /// <param name="text">Входной текст</param>
        /// <param name="key">Ключ смещения</param>
        /// <returns>
        /// Зашифрованный текст
        /// </returns>
        public string Encryption(string text, int key)
        {

            var res = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {

                for (int j = 0; j < _alphabet.Length; j++)
                {
                    if (text[i] == _alphabet[j])
                    {
                        res.Append(_alphabet[(j + key) % _alphabet.Length]);
                        break;
                    }

                    else if (text[i] == _ALPHABET[j])
                    {
                        res.Append(_ALPHABET[(j + key) % _ALPHABET.Length]);
                        break;
                    }

                    else if (j == _alphabet.Length - 1)
                    {
                        res.Append(text[i]);
                        break;
                    }
                }

            }
                
            return res.ToString();
        }
        /// <summary>
        /// Расшифровывает текст с указаннием ключа
        /// </summary>
        /// <param name="text">Зашифрованный текст</param>
        /// <param name="key">Ключ смещения</param>
        /// <returns>
        /// Расшифрованный текст
        /// </returns>
        public string Decryption(string text, int key)
        {
            var res = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {

                for (int j = 0; j < _alphabet.Length; j++)
                {
                    if (text[i] == _alphabet[j])
                    {
                        res.Append(_alphabet[(j - key + _alphabet.Length) % _alphabet.Length]);
                        break;
                    }

                    else if (text[i] == _ALPHABET[j])
                    {
                        res.Append(_ALPHABET[(j - key + _ALPHABET.Length) % _ALPHABET.Length]);
                        break;
                    }

                    else if (j == _alphabet.Length - 1)
                    {
                        res.Append(text[i]);
                        break;
                    }
                }

            }

            return res.ToString();
        }

    }

}
