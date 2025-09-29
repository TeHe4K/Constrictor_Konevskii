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

namespace Constructor_Konevskii
{
    public partial class MainWindow : Window
    {
        public List<Classes.Student> AllStudent = Classes.RepoStudents.AllStudent();
        public int Count = 10;
        public int Step = 0;
        public MainWindow()
        {
            InitializeComponent();
            CreateStudent();
        }

        public void CreateStudent()
        {
            for (int iStudent = Step; iStudent < Step + Count; iStudent++)
                if (AllStudent.Count > iStudent)
                {
                    var studentElement = new Elements.Student(AllStudent[iStudent]);
                    studentElement.OnEditRequested += Student_OnEditRequested;
                    parent.Children.Add(studentElement);
                }
            this.Step += Count;
        }

        private void Student_OnEditRequested(Elements.Student studentControl)
        {
            var panel = studentControl.Parent as Panel;
            if (panel != null)
            {
                int index = panel.Children.IndexOf(studentControl);
                var editUser = new Elements.EditUser(studentControl.StudentData, index);
                panel.Children.RemoveAt(index);
                panel.Children.Insert(index, editUser);
            }
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer scroll = sender as ScrollViewer;
            double ParentHeight = parent.ActualHeight;
            double WindowHeight = scroll.ActualHeight - 20;
            double DeltaHeight = ParentHeight - WindowHeight;

            if (DeltaHeight - scroll.VerticalOffset < 140)
            {
                CreateStudent();
            }
        }
    }
}