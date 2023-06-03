using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Budget
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Entry> Entrys { get; set; } = new ObservableCollection<Entry>();

        public MainWindow()
        {
            InitializeComponent();
            UpdateEntryList();
            UpdateTotal();
        }

        private void btn_createEntryType_Click(object sender, RoutedEventArgs e)
        {
            CreateEntryType createEntryType = new CreateEntryType(this);
            createEntryType.ShowDialog();
        }

        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_EntryName.Text) && !string.IsNullOrWhiteSpace(txt_summa.Text) && cmb_EntryType.SelectedValue != null && dt_selectDate.SelectedDate.HasValue)
            {
                int summa;
                if (int.TryParse(txt_summa.Text, out summa))
                {
                    Entry entry = new Entry(txt_EntryName.Text, cmb_EntryType.SelectedValue.ToString(), summa, dt_selectDate.SelectedDate.Value);
                    Entrys.Add(entry);
                    UpdateEntryList();
                    UpdateTotal();
                }
                else
                    MessageBox.Show("Некорректное значение в поле суммы!", "Добавление записи", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Не все поля были заполнены!", "Добавление записи", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void dt_selectDate_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) => UpdateEntryList();

        private void UpdateEntryList() => dg_EntryList.ItemsSource = Entrys.Where(x => x.Date == dt_selectDate.SelectedDate.Value);

        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {
            if (dg_EntryList.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись для удаления!", "Удаление записи", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = dg_EntryList.SelectedItem as Entry;
            Entrys.Remove(result);
            UpdateEntryList();
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            int total = 0;
            foreach (var item in Entrys)
            {
                if (item.IsReceiptOrDeduction)
                    total += item.Money;
                else
                    total -= item.Money;
            }

            txt_total.Text = $"Итого: {total}";
        }

        private void dg_EntryList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var result = dg_EntryList.SelectedItem as Entry;
            if (result != null)
            {
                txt_EntryName.Text = result.Name;
                txt_summa.Text = result.IsReceiptOrDeduction ? result.Money.ToString() : (result.Money * -1).ToString();
                cmb_EntryType.SelectedValue = result.Type;
            }
            else
            {
                txt_EntryName.Text = string.Empty;
                txt_summa.Text = string.Empty;
                cmb_EntryType.SelectedValue = string.Empty;
            }
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            if (dg_EntryList.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись для изменений!", "Удаление записи", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = dg_EntryList.SelectedItem as Entry;
            if (result != null)
            {
                int summa;
                if (int.TryParse(txt_summa.Text, out summa))
                {
                    result.Name = txt_EntryName.Text;
                    result.Type = cmb_EntryType.SelectedValue.ToString();
                    result.Money = summa;
                    UpdateEntryList();
                    UpdateTotal();
                }
                else
                {
                    MessageBox.Show("Некорректное значение в поле суммы!", "Изменение записи", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }
    }
}