﻿using System;
using System.Collections.Generic;

namespace DeansSystem
{
    public class Output
    {
        private string login;
        public string path = Environment.CurrentDirectory;
        public string logsFile = @"logins.csv";
        public string studentsFile = @"students.csv";
        public string marksFile = @"marks.csv";
        private User _user;

        public void StartWork()
        {
            Console.WriteLine("Enter your login:");
            login = Console.ReadLine();
            _user = new User(login, path, logsFile);
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();
            _user.Authorization(password);
        }
        public void AdminCommands()
        {
            Console.WriteLine($"Wellcome back, {login}");
            Admin admin = new Admin(login, path, logsFile);
            Console.WriteLine("Your functional:");
            Console.WriteLine("Enter '1' to add user \n" + "Enter '2' to remove user \n" + "Enter '3' to change user information \n" + "Enter '4' to change student's group \n" + "Enter '5' to change student's course \n" + "Enter '6' to exit");
            int command = Int32.Parse(Console.ReadLine());
            if (command == 6) Environment.Exit(0);
            Console.WriteLine("Enter user's login:");
            string userLogin = Console.ReadLine();
            if (command == 1)
            {
                Console.WriteLine("Enter a password for new user:");
                string password = Console.ReadLine();
                Console.WriteLine("Enter a access level for new user:");
                string accessLevel = Console.ReadLine();
                admin.AddUser(userLogin, password, accessLevel);
            }
            else if (command == 2) admin.RemoveUser(userLogin);
            else if (command == 3)
            {
                Console.WriteLine("Enter a new user information (login and password through ',')");
                string line = Console.ReadLine();
                admin.ChangeUserInformation(userLogin, line);
            }
            else if (command == 4)
            {
                Console.WriteLine("Enter a group which you want to join:");
                string group = Console.ReadLine();
                admin.ChangeGroup(userLogin, group, studentsFile);
            }
            else if (command == 5) admin.ChangeCourse(login, studentsFile);
            AdminCommands();
        }
        public void TeacherCommands()
        {
            Console.WriteLine($"Wellcome back, {login}");
            Teacher teacher = new Teacher(login, path, marksFile);
            Console.WriteLine("Your functional:");
            Console.WriteLine("Enter '1' to change student's marks \n" + "Enter '2' to watch student's marks \n" + "Enter '3' to watch all students marks \n" + "Enter '4' to exit");
            int command = Int32.Parse(Console.ReadLine());
            if (command == 1)
            {
                Console.WriteLine("Enter student's login:");
                string userLogin = Console.ReadLine();
                Console.WriteLine("Enter new marks for this student:");
                string marks = Console.ReadLine();
                teacher.ChangeMarks(userLogin, marks);
            }
            else if (command == 2)
            {
                Console.WriteLine("Enter student's login:");
                string userLogin = Console.ReadLine();
                teacher.WatchStudent(userLogin);
            }
            else if (command == 3) teacher.WatchAllInformation();
            else if (command == 4) Environment.Exit(0);
            TeacherCommands();
        }
        public void StudentCommands()
        {
            Console.WriteLine($"Wellcome back, {login}");
            Student student = new Student(login, path, marksFile);
            Console.WriteLine("Your functional:");
            Console.WriteLine("Enter '1' to check your marks \n" + "Enter '2' to check your group \n" + "Enter '3' to check your course \n" + "Enter '4' to exit");
            int command = Int32.Parse(Console.ReadLine());
            if (command == 1) student.CheckYourMarks();
            else if (command == 2) student.CheckGroup();
            else if (command == 3) student.CheckCourse();
            else if (command == 4) Environment.Exit(0);
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