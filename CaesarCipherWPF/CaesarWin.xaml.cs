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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CaesarCipherWPF
{
    /// <summary>
    /// Логика взаимодействия для CaesarWin.xaml
    /// </summary>
    public partial class CaesarWin : Window
    {
        Loader _loader = new Loader();
        Cipher _cip = new Cipher();

        SelectionWin _sW;

        public CaesarWin(SelectionWin sW)
        {
            InitializeComponent();
            rb_Encode.IsChecked = true;
            _sW = sW;
        }

        private void rb_Encode_Checked(object sender, RoutedEventArgs e)
        {
            btn_start.Content = "Зашифровать";
        }

        private void rb_Decode_Checked(object sender, RoutedEventArgs e)
        {
            btn_start.Content = "Расшифровать";
        }


        private void s_key_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (rb_Decode.IsChecked == true)
                tb_out.Text = _cip.Decryption(tb_inp.Text, Convert.ToInt32(s_key.Value));
            if (rb_Encode.IsChecked == true)
                tb_out.Text = _cip.Encryption(tb_inp.Text, Convert.ToInt32(s_key.Value));
            tb_key.Text = ((int)s_key.Value).ToString();
        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            if (rb_Decode.IsChecked == true)
                tb_out.Text = _cip.Decryption(tb_inp.Text, Convert.ToInt32(tb_key.Text));
            if (rb_Encode.IsChecked == true)
                tb_out.Text = _cip.Encryption(tb_inp.Text, Convert.ToInt32(tb_key.Text));
        }

        private void btn_replace_Click(object sender, RoutedEventArgs e)
        {
            tb_inp.Text = tb_out.Text;
            tb_out.Clear();
        }

        private void btn_Import_Click(object sender, RoutedEventArgs e)
        {
            tb_inp.Text = _loader.Import();
        }

        private void btn_Export_Click(object sender, RoutedEventArgs e)
        {
            _loader.Export(tb_out.Text);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            e.Cancel = true;
            if (!Status.isClosing)
                _sW.Show();
            this.Hide();
        }


        public string GetText()
        {
            return tb_out.Text;
        }
    }
}
