using System;

namespace DeansSystem
{
    public class User
    {
        private string login;
        private string path;
        private string fileName;
        private FileOperations fo;
        private Output _output;
        private Student _student;
        private Teacher _teacher;
        private Admin _admin;

        public User(string login, string path, string fileName)
        {
            this.login = login;
            this.path = path;
            this.fileName = fileName;
            fo = new FileOperations(path, fileName);
            _output = new Output();
        }
        public bool Authorization(string password)
        {
            string line = login + "," + password;
            if (fo.IsExists(line) == true)
            {
                string[] splited = fo.GetLine(login).Split(",", StringSplitOptions.RemoveEmptyEntries);
                int accessLevel = Int32.Parse(splited[2]);
                string marksFile = _output.marksFile;
                if (accessLevel == 3)
                {
                    _admin = new Admin(login, path, fileName);
                    _output.welcome(login);
                    _output.AdminCommands();
                    return true;
                }
                else if (accessLevel == 2)
                {
                    _teacher = new Teacher(login, path, marksFile);
                    _output.welcome(login);
                    _output.TeacherCommands();
                    return true;
                }
                else
                {
                    _student = new Student(login, path, marksFile);
                    _output.welcome(login);
                    _output.StudentCommands();
                    return true;
                }
            }
            else {
                _output.WrongInput();
                return false;
            }
        }
    }
}
