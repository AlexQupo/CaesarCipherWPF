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
    /// Логика взаимодействия для SelectionWin.xaml
    /// </summary>
    public partial class SelectionWin : Window
    {

        CaesarWin _cW;
        FreqCryptaWin _fCW;
        string _text = String.Empty;

        public SelectionWin()
        {
            InitializeComponent();
            comboBox.SelectedIndex = 0;
            _cW = new CaesarWin(this);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool isNewWindowOpend = false;
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    _cW.Show();
                    isNewWindowOpend = true;
                    break;
                case 1:
                    if (_cW.GetText() != null)
                        _text = _cW.GetText();
                    _fCW = new FreqCryptaWin(this, _text);
                    _fCW.Show();
                    isNewWindowOpend = true;
                    break;
                default:
                    MessageBox.Show(
                        "Выберите один из вариантов!",
                        "Предупреждение",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning,
                        MessageBoxResult.None,
                        MessageBoxOptions.DefaultDesktopOnly);
                    break;
            }
            if (isNewWindowOpend)
                this.Hide();
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Status.isClosing = true;
            Application.Current.Shutdown();
        }
    }

    public static class Status
    {
        public static bool isClosing;
    }
}
