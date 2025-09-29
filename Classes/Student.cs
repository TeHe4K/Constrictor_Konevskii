using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Constructor_Konevskii.Classes
{
    public partial class Student
    {
        public string Firstname = "";
        public string Lastname = "";
        public string Surname = "";
        public bool Scholarship = false;
        public int Course = 4;
        public string Src = "C:\\Users\\student-a502\\Source\\Repos\\Constrictor_Konevskii\\Images\\ic_user.png";
        

        public Student(string Firstname, string Lastname, string Surname)
        {
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.Surname = Surname;
        }

        public Student(string Firstname, string Lastname, string Surname, bool Scholarship) : this(Firstname, Lastname, Surname)
        {
            this.Scholarship = Scholarship;
        }

        public Student(string Firstname, string Lastname, string Surname, bool Scholarship, int Course) : this(Firstname, Lastname, Surname, Scholarship)
        {
            this.Course = Course;
        }
        public Student(string Firstname, string Lastname, string Surname, bool Scholarship, int Course, string Src) : this(Firstname, Lastname, Surname, Scholarship, Course)
        {
            this.Src = Src;
        }

        public string GetFIO()
        {
            return $"{Lastname} {Firstname} {Surname}";
        }
    }
}