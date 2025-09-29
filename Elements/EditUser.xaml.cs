using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace Constructor_Konevskii.Elements
{
    public partial class EditUser : UserControl
    {
        private Classes.Student _student;
        private int _originalIndex;

        public EditUser(Classes.Student student, int originalIndex)
        {
            InitializeComponent();

            _student = student;
            _originalIndex = originalIndex;

            tb_fio.Text = student.GetFIO();
            tb_scholarship.Content = student.Scholarship ? "Получает" : "Не получает";
            tb_Course.Text = student.Course.ToString();

            tb_fio.Focus();
            tb_fio.SelectAll();
            Ico_Replace.Source = new BitmapImage(new Uri(student.Src, UriKind.RelativeOrAbsolute));
        }

        private void ScholarShipChange(object sender, RoutedEventArgs e)
        {
            bool currentScholarship = _student.Scholarship;
            _student.Scholarship = !currentScholarship;
            tb_scholarship.Content = _student.Scholarship ? "Получает" : "Не получает";
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                CancelChanges();
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                SaveChanges();
                e.Handled = true;
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SaveChanges();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                CancelChanges();
                e.Handled = true;
            }
        }

        private void SaveChanges()
        {
            try
            {
                string[] fioParts = tb_fio.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (fioParts.Length >= 3)
                {
                    _student.Lastname = fioParts[0];
                    _student.Firstname = fioParts[1];
                    _student.Surname = fioParts[2];
                }
                if (int.TryParse(tb_Course.Text, out int course))
                {
                    _student.Course = course;
                }
                if (Src != null)
                {
                    _student.Src = Src.ToString();
                }
                ReturnToStudentView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelChanges()
        {
            ReturnToStudentView();
        }

        private void ReturnToStudentView()
        {
            var parent = Parent as Panel;
            if (parent != null)
            {
                var studentElement = new Elements.Student(_student);
                studentElement.OnEditRequested += (studentControl) =>
                {
                    var panel = studentControl.Parent as Panel;
                    if (panel != null)
                    {
                        int index = panel.Children.IndexOf(studentControl);
                        var editUser = new Elements.EditUser(studentControl.StudentData, index);
                        panel.Children.RemoveAt(index);
                        panel.Children.Insert(index, editUser);
                    }
                };

                parent.Children.RemoveAt(_originalIndex);
                parent.Children.Insert(_originalIndex, studentElement);
            }
        }

        string Src;
        private void Click_Ico(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Src = openFileDialog.FileName;
                _student.Src = Src;

                ReturnToStudentView();
            }
        }
    }
}