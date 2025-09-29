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

namespace Constructor_Konevskii.Elements
{
    /// <summary>
    /// Логика взаимодействия для Student.xaml
    /// </summary>
    public partial class Student : UserControl
    {
        public Classes.Student StudentData { get; private set; }
        public event Action<Student> OnEditRequested;
        public Student(Classes.Student student)
        {
            InitializeComponent();
            tb_fio.Content = student.GetFIO();
            tb_scholarship.Content = student.Scholarship ? "Cтипендпия: Получает" : "Стипендия: Не получает";
            tb_Course.Content = $"Курс: {student.Course}";
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OnEditRequested?.Invoke(this);
        }
    }
}
