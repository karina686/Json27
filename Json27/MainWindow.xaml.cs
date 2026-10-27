using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace Json27
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Student> FriendsList = new ObservableCollection<Student>();

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Add addWindow = new Add();
            addWindow.Owner = this;

            if (addWindow.ShowDialog() == true)
            {
                string name = addWindow.tbName.Text;
                string phone = addWindow.tbPhone.Text;
                int.TryParse(addWindow.tbAge.Text, out int age);
                var newFriend = new Student
                {
                    Name = name,
                    Age = age,
                    Phone = phone
                };
                lbNames.Items.Add(newFriend);
            }
        }
        

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (lbNames.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Вы уверены, что хотите удалить выбранного друга?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    lbNames.Items.Remove(lbNames.SelectedItem);
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonSerializer.Serialize(lbNames.Items, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            });

            File.WriteAllText("students.json", json);
            MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButton.OK);
        
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists("students.json"))
            {
                MessageBox.Show("Файл students.json не найден!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                string json = File.ReadAllText("students.json");

                List<Student> loadedFriends = JsonSerializer.Deserialize<List<Student>>(json);

                if (loadedFriends != null)
                {
                    lbNames.Items.Clear();

                    foreach (var friend in loadedFriends)
                    {
                        lbNames.Items.Add(friend);
                    }

                    MessageBox.Show("Данные успешно загружены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при чтении файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}