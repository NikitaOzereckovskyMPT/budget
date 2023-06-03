using System.Windows;

namespace Budget
{
    /// <summary>
    /// Логика взаимодействия для CreateEntryType.xaml
    /// </summary>
    public partial class CreateEntryType : Window
    {
        private readonly MainWindow window;

        public CreateEntryType(MainWindow window)
        {
            InitializeComponent();
            this.window = window;
        }

        private void btn_createEntryType_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_nameEnrty.Text))
            {
                window.cmb_EntryType.Items.Add(txt_nameEnrty.Text);
                this.Close();
            }
            else
                MessageBox.Show("Вы не указали тип записи!", "Добавить тип", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
