using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CaesarCipherWPF
{
    /// <summary>
    /// Логика взаимодействия для FreqCryptaWin.xaml
    /// </summary>
    public partial class FreqCryptaWin : Window
    {

        SelectionWin _sW;
        string _text = String.Empty; //Исходный текст
        StringBuilder _changedText = new StringBuilder(); //Измененный текст
        List<AdvLetter> lettersDict = new List<AdvLetter>(); //Словарь перестановок(старое слово, новое слово, индекс), где индексу присваивается индекс заменяемого символа в тексте
        Loader _loader = new Loader();



        public FreqCryptaWin(SelectionWin sW, string text)
        {
            InitializeComponent();
            _sW = sW;
            _text = text.ToUpper();
            _changedText.Append(_text);
            //Отображение начального текста
            UpdateText(_text);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            _sW.Show();
            e.Cancel = false;
        }


        #region Buttons
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            ch_letters.IsChecked = false;
            ch_text.IsChecked = false;
            AddLetterInList();
            UpdateReplacementList();
            UpdateText();
        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            ch_letters.IsChecked = false;
            ch_text.IsChecked = false;
            DelLetterInList();
            UpdateReplacementList();
            UpdateText();
        }
        #endregion

        #region CheckBox
        private void ch_letters_Checked(object sender, RoutedEventArgs e)//Показать только замененные
        {
            UpdateText(0.2);
        }

        private void ch_text_Checked(object sender, RoutedEventArgs e)//Показывает исходный текст
        {
            UpdateText(_text);
        }

        private void ch_letters_Click(object sender, RoutedEventArgs e)
        {
            if (ch_letters.IsChecked == false)
                UpdateText();
            ch_text.IsChecked = false;
        }

        private void ch_text_Click(object sender, RoutedEventArgs e)
        {
            if (ch_text.IsChecked == false)
                UpdateText();
            ch_letters.IsChecked = false;
        }
        #endregion

        #region EditDictionary

        bool isIndexExists(int ind)
        {
            bool check = false;
            foreach (var letter in lettersDict)
            {
                if (letter.Index == ind)
                {
                    check = true;
                    break;
                }
                else
                    check = false;
            }
            return check;
        }

        void AddLetterInList()
        {
            int index = 0;
            char newLetter = tb_replacement.Text.ToUpper().Single();
            foreach (char letter in _changedText.ToString())
            {
                if (letter == tb_select.Text.ToUpper().Single() && !isIndexExists(index))
                {
                    lettersDict.Add(new AdvLetter(letter, newLetter, index));
                }
                index++;
            }
        }

        void DelLetterInList()
        {
            int index = 0;
            foreach (var letter in lettersDict.ToArray())
            {
                if (letter.OldLetter == tb_select.Text.ToUpper().Single() &&
                    letter.NewLetter == tb_replacement.Text.ToUpper().Single())
                {
                    lettersDict.RemoveAt(index);
                    index--;
                }
                index++;
            }
        }
        #endregion

        #region ShowDictionary
        void UpdateReplacementList()
        {
            tb_replacementList.Text = String.Empty;
            List<AdvLetter> tempLettersDict = new List<AdvLetter>();
            tempLettersDict.Add(new AdvLetter());
            int index = 0;
            foreach (var letter in lettersDict)
            {
                int index2 = 0;
                foreach (var newLetter in tempLettersDict.ToArray())
                {
                    if (letter.OldLetter == newLetter.OldLetter && letter.NewLetter == newLetter.NewLetter)
                        break;
                    else if (index2 == tempLettersDict.Count - 1)
                        tempLettersDict.Add(new AdvLetter(letter.OldLetter, letter.NewLetter, index));
                    index++;
                    index2++;
                }
            }
            tempLettersDict.RemoveAt(0);
            foreach (var letter in tempLettersDict)
            {
                tb_replacementList.Text += letter.ToString() + "\n";
            }
        }
        #endregion

        #region UpdateText
        void UpdateText()
        {
            _changedText.Clear();
            _changedText.Append(_text);
            foreach (var letter in lettersDict)
            {
                _changedText[letter.Index] = letter.NewLetter;
            }
            
            FlowDocument fDoc = new FlowDocument();
            Run fragment = new Run();
            Paragraph paragraph = new Paragraph();
            bool isAdded = false;
            for (int i = 0; i < _changedText.Length; i++)
            {
                foreach(var letter in lettersDict)
                {
                    if (i == letter.Index)
                    {
                        fragment = new Run(_changedText[i].ToString());
                        fragment.Foreground = Brushes.Red;
                        paragraph.Inlines.Add(fragment);
                        isAdded = true;
                        break;
                    }
                    else
                        isAdded = false;
                }
                if (!isAdded)
                {
                    fragment = new Run(_changedText[i].ToString());
                    paragraph.Inlines.Add(fragment);
                }
                
            }

            fDoc.Blocks.Add(paragraph);
            rtb_space.Document = fDoc;
        }

        void UpdateText(object text)
        {
            FlowDocument fDoc = new FlowDocument();
            Run codingText = new Run(text.ToString());
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(codingText);
            fDoc.Blocks.Add(paragraph);
            rtb_space.Document = fDoc;
        }

        void UpdateText(double opacity)
        {
            _changedText.Clear();
            _changedText.Append(_text);
            foreach (var letter in lettersDict)
            {
                _changedText[letter.Index] = letter.NewLetter;
            }

            FlowDocument fDoc = new FlowDocument();
            Run fragment = new Run();
            Paragraph paragraph = new Paragraph();
            bool isAdded = false;
            for (int i = 0; i < _changedText.Length; i++)
            {
                foreach (var letter in lettersDict)
                {
                    if (i == letter.Index)
                    {
                        fragment = new Run(_changedText[i].ToString());
                        paragraph.Inlines.Add(fragment);
                        isAdded = true;
                        break;
                    }
                    else
                        isAdded = false;
                }
                if (!isAdded)
                {
                    fragment = new Run(_changedText[i].ToString());
                    if (fragment.Foreground.IsFrozen)
                        fragment.Foreground = fragment.Foreground.Clone();
                    fragment.Foreground.Opacity = opacity;
                    paragraph.Inlines.Add(fragment);
                }
            }
            fDoc.Blocks.Add(paragraph);
            rtb_space.Document = fDoc;
        }
        #endregion

        #region FreqTable
        private void btn_freq_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<char, double> dictionaryRussian = new Dictionary<char, double>()
            {
                { 'А', 8.01 },
                { 'Б', 1.59 },
                { 'В', 4.54 },
                { 'Г', 1.70 },
                { 'Д', 2.98 },
                { 'Е', 8.45 },
                { 'Ё', 0.04 },
                { 'Ж', 0.94 },
                { 'З', 1.65 },
                { 'И', 7.35 },
                { 'Й', 1.21 },
                { 'К', 3.49 },
                { 'Л', 4.40 },
                { 'М', 3.21 },
                { 'Н', 6.70 },
                { 'О', 1.97 },
                { 'П', 2.81 },
                { 'Р', 4.73 },
                { 'С', 5.47 },
                { 'Т', 6.26 },
                { 'У', 2.62 },
                { 'Ф', 0.26 },
                { 'Х', 0.97 },
                { 'Ц', 0.48 },
                { 'Ч', 1.44 },
                { 'Ш', 0.73 },
                { 'Щ', 0.36 },
                { 'Ъ', 0.04 },
                { 'Ы', 1.90 },
                { 'Ь', 1.74 },
                { 'Э', 0.32 },
                { 'Ю', 0.64 },
                { 'Я', 2.01 },
            };
            FreqTable fT = new FreqTable(dictionaryRussian);
            fT.Show();
        }

        private void btn_curFreq_Click(object sender, RoutedEventArgs e)
        {
            CharCounter();
        }

        void CharCounter()
        {
            Dictionary<char, double> curDictionaryRussian = new Dictionary<char, double>()
            {
                { 'А', 0 },
                { 'Б', 0 },
                { 'В', 0 },
                { 'Г', 0 },
                { 'Д', 0 },
                { 'Е', 0 },
                { 'Ё', 0 },
                { 'Ж', 0 },
                { 'З', 0 },
                { 'И', 0 },
                { 'Й', 0 },
                { 'К', 0 },
                { 'Л', 0 },
                { 'М', 0 },
                { 'Н', 0 },
                { 'О', 0 },
                { 'П', 0 },
                { 'Р', 0 },
                { 'С', 0 },
                { 'Т', 0 },
                { 'У', 0 },
                { 'Ф', 0 },
                { 'Х', 0 },
                { 'Ц', 0 },
                { 'Ч', 0 },
                { 'Ш', 0 },
                { 'Щ', 0 },
                { 'Ъ', 0 },
                { 'Ы', 0 },
                { 'Ь', 0 },
                { 'Э', 0 },
                { 'Ю', 0 },
                { 'Я', 0 },
            };

            char[] tempArrayChar = _text.ToCharArray();
            for (int i = 0; i < tempArrayChar.Length; i++)
            {
                if (curDictionaryRussian.ContainsKey(tempArrayChar[i]))
                    curDictionaryRussian[tempArrayChar[i]]++;
            }

            foreach (var charfreq in curDictionaryRussian.ToArray())
            {
                curDictionaryRussian[charfreq.Key] = Math.Round((charfreq.Value / _text.Length * 100), 3);
            }
            FreqTable fT = new FreqTable(curDictionaryRussian);
            fT.Show();
        }
        #endregion

        private void btn_Export_Click(object sender, RoutedEventArgs e)
        {
            _loader.Export(new TextRange(rtb_space.Document.ContentStart, rtb_space.Document.ContentEnd).Text);
        }

        private void btn_Import_Click(object sender, RoutedEventArgs e)
        {
            string receivedText = _loader.Import().ToUpper();
            _text = receivedText;
            _changedText.Clear();
            _changedText.Append(receivedText);
            UpdateText();
        }
    }
}
