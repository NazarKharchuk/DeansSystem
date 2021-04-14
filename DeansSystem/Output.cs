using System;
using System.Collections.Generic;

namespace DeansSystem
{
    public class Output
    {
        public static string login;
        public string path = Environment.CurrentDirectory;
        public string logsFile = @"logins.csv";
        public string studentsFile = @"students.csv";
        public string marksFile = @"marks.csv";
        private User _user;

        public void StartWork()
        {
            for (int i = 0; i < 3; i++)
            {
                while (true)
                {
                    Console.Write("Enter your login: ");
                    login = Console.ReadLine();
                    if (login != "") break;
                    Console.WriteLine("\nError: Empty login! Try again:\n");
                }
                _user = new User(login, path, logsFile);
                Console.Write("Enter your password: ");
                string password = Console.ReadLine();
                bool flag = _user.Authorization(password);
                if (flag) break;
            }
            Console.Clear();
            Console.WriteLine("To many atemptions! Try again later.");
            Environment.Exit(1);
        }

        public void Welcome()
        {
            Console.Clear();
            Console.WriteLine($"Wellcome back, {login}!\n");
        }
        public void UserCommands()
        {
            _user = new User(login, path, logsFile);
            Console.WriteLine("Your functional:");
            Console.WriteLine("Enter '1' check your password \n" + "Enter '2' to check your login \n" + "Enter '3' to change your password \n");
            string command = Console.ReadLine();
            Console.Clear();
            if (command == "1") _user.ViewPassword();
            else if (command == "2") Console.WriteLine($"Your login: {login}");
            else if (command == "3")
            {
                Console.WriteLine("Enter a new password:");
                string password = Console.ReadLine();
                _user.ChangePassword(password);
            }
        }
        public void AdminCommands()
        {
            Admin admin = new Admin(login, path, logsFile);
            Console.WriteLine("Your functional:");
            Console.WriteLine("Enter '1' to add user \n" + "Enter '2' to remove user \n" + "Enter '3' to change user information \n" + "Enter '4' to change student's group \n" + "Enter '5' to change student's course \n" + "Enter '6' to check student's course \n" + "Enter '7' to check student's group \n" + "Enter '8' to check student's marks \n" + "Enter '9' to exit \n" + "Enter '0' to open user's menu");
            string command = Console.ReadLine();
            Console.Clear();
            if (command == "9") Environment.Exit(0);
            else if (command == "0") { UserCommands(); AdminCommands(); }
            Console.Write("Enter user's login: ");
            string userLogin = Console.ReadLine();
            if (command == "1")
            {
                Console.WriteLine("Enter a password for new user:");
                string password = Console.ReadLine();
                Console.WriteLine("Enter a access level for new user:");
                string accessLevel = Console.ReadLine();
                if (accessLevel == "1")
                {
                    Console.WriteLine("Enter a group for new student:");
                    string group = Console.ReadLine();
                    Console.WriteLine("Enter a course for new student:");
                    string course = Console.ReadLine();
                    admin.AddUser(userLogin, password, accessLevel, group, course);
                }
                else admin.AddUser(userLogin, password, accessLevel);
            }
            else if (command == "2") admin.RemoveUser(userLogin);
            else if (command == "3")
            {
                Console.WriteLine("Enter a new user information (login and password through ',')");
                string line = Console.ReadLine();
                admin.ChangeUserInformation(userLogin, line);
            }
            else if (command == "4")
            {
                string group;
                while (true)
                {
                    Console.WriteLine("Enter a group which you want to join:");
                    group = Console.ReadLine();
                    if (group.Length == 4 && Char.IsLetter(group[0]) && Char.IsLetter(group[1]) &&
                        Char.IsDigit(group[2]) && Char.IsDigit(group[3])) break;
                    Console.Clear();
                    Console.WriteLine("Incorrect input!");
                }
                admin.ChangeGroup(userLogin, group, studentsFile);
            }
            else if (command == "5") admin.ChangeCourse(userLogin, studentsFile);
            else if (command == "6") admin.CheckCourse(userLogin);
            else if (command == "7") admin.CheckGroup(userLogin);
            else if (command == "8") admin.CheckMarks(userLogin);
            else Console.WriteLine("Incorrect input!");
            AdminCommands();
        }

        public void TeacherCommands()
        {
            Teacher teacher = new Teacher(login, path, marksFile);
            Console.WriteLine("Your functional:");
            Console.WriteLine("Enter '1' to change student's marks \n" + "Enter '2' to watch student's marks \n" + "Enter '3' to watch all students marks \n" + "Enter '4' to check student's course \n" + "Enter '5' to check student's group \n" + "Enter '6' to exit\n" + "Enter '0' to open user's menu");
            string command = Console.ReadLine();
            if (command == "1")
            {
                Console.WriteLine("Enter student's login:");
                string userLogin = Console.ReadLine();
                Console.WriteLine("Enter new marks for this student:");
                string marks = Console.ReadLine();
                teacher.ChangeMarks(userLogin, marks);
            }
            else if (command == "0") { UserCommands(); TeacherCommands(); }
            else if (command == "2")
            {
                Console.WriteLine("Enter student's login:");
                string userLogin = Console.ReadLine();
                teacher.WatchStudent(userLogin);
            }
            else if (command == "3") teacher.WatchAllInformation();
            else if (command == "4")
            {
                Console.WriteLine("Enter student's login:");
                string userLogin = Console.ReadLine();
                teacher.CheckCourse(userLogin);
            }
            else if (command == "5")
            {
                Console.WriteLine("Enter student's login:");
                string userLogin = Console.ReadLine();
                teacher.CheckGroup(userLogin);
            }
            else if (command == "6") Environment.Exit(0);
            else Console.WriteLine("Incorrect input!");
            TeacherCommands();
        }

        public void StudentCommands()
        {
            Student student = new Student(login, path, marksFile);
            Console.WriteLine("Your functional:");
            Console.WriteLine("Enter '1' to check your marks \n" + "Enter '2' to check your group \n" + "Enter '3' to check your course \n" + "Enter '4' to exit\n" + "Enter '0' to open user's menu");
            string command = Console.ReadLine();
            if (command == "1") student.CheckYourMarks();
            else if (command == "0") { UserCommands(); StudentCommands(); }
            else if (command == "2") student.CheckGroup();
            else if (command == "3") student.CheckCourse();
            else if (command == "4") Environment.Exit(0);
            else Console.WriteLine("Incorrect input!");
            StudentCommands();
        }
        public void WrongInput() => Console.WriteLine("Wrong input");
        public void Success() => Console.WriteLine("Success");
        public void ListOutput(List<string> information)
        {
            foreach (string item in information)
            {
                Console.WriteLine(item);
            }
        }
        public void LineOutput(string line) => Console.WriteLine(line);
    }
}
