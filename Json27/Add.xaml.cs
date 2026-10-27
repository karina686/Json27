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

namespace Json27
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public string FriendName { get; private set; }
        public int FriendAge { get; private set; }
        public string FriendPhone { get; private set; }

        public Add()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text) || string.IsNullOrWhiteSpace(tbPhone.Text))
            {
                MessageBox.Show("Заполните поля 'Имя' и 'Номер телефона'.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(tbAge.Text, out int age) || age <= 0 || age > 120)
            {
                MessageBox.Show("Введите корректный возраст (число).", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            FriendName = tbName.Text;
            FriendAge = age;
            FriendPhone = tbPhone.Text;
            this.DialogResult = true;
            this.Close();
        }
    }
}
    

