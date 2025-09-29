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
using Constructor_Konevskii.Classes;

namespace Constructor_Konevskii.Elements
{
    /// <summary>
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : UserControl
    {
        private Classes.Student student;
        private Student originalControl;
        private int originalIndex;
        public EditUser(Student student)
        {
            InitializeComponent();
            tb_fio.Text = student.tb_fio.Content.ToString();
            tb_scholarship.Content = student.tb_scholarship.Content.ToString();
            tb_Course.Text = $"Курс: {student.tb_Course}";
        }


        private void ScholarShipChange(object sender, RoutedEventArgs e)
        {
            if (tb_scholarship.Content == "Получает")
                tb_scholarship.Content = "Не получает";
            else
                tb_scholarship.Content = "Получает";
        }

        private void ReturnToStudentView()
        {
            var parent = Parent as Panel;
            if (parent != null)
            {
                parent.Children.RemoveAt(originalIndex);
                parent.Children.Insert(originalIndex, new Elements.Student(student));
            }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                CancelChanges();
                e.Handled = true;
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                student.Firstname = tb_fio.Text.Split(' ')[0];
                student.Lastname = tb_fio.Text.Split(' ')[1];
                student.Surname = tb_fio.Text.Split(' ')[2];
                int.TryParse(tb_Course.Text, out student.Course);
                if (tb_scholarship.Content == "Получает")
                    student.Scholarship = true;
                else
                    student.Scholarship = false;
                ReturnToStudentView();
                e.Handled = true;
            }
        }
        private void CancelChanges()
        {
            ReturnToStudentView();
        }
    }
}
