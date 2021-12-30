using System.Windows;


namespace CaesarCipherWPF
{
    /// <summary>
    /// Логика взаимодействия для FreqTable.xaml
    /// </summary>
    public partial class FreqTable : Window
    {
        public FreqTable(object data)
        {
            InitializeComponent();
            dg_freq.ItemsSource = (System.Collections.IEnumerable)data;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dg_freq.Columns[0].Header = "Буква";
            dg_freq.Columns[1].Header = "Частота, %";
            dg_freq.Columns[0].Width = 75;
            dg_freq.Columns[1].Width = 75;
        }
    }
}
