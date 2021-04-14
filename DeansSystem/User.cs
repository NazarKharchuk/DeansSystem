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
            if (fo.IsExists(line))
            {
                string[] splited = fo.GetLine(login).Split(",", StringSplitOptions.RemoveEmptyEntries);
                int accessLevel = Int32.Parse(splited[2]);
                string marksFile = _output.marksFile;
                if (accessLevel == 3)
                {
                    _output.Welcome();
                    _output.AdminCommands();
                    return true;
                }
                else if (accessLevel == 2)
                {
                    _output.Welcome();
                    _output.TeacherCommands();
                    return true;
                }
                else
                {
                    _output.Welcome();
                    _output.StudentCommands();
                    return true;
                }
            }
            else {
                _output.WrongInput();
                return false;
            }
        }
        public void ViewPassword()
        {
            string[] splited = fo.GetLine(login).Split(",", StringSplitOptions.RemoveEmptyEntries);
            _output.LineOutput(splited[1]);
        }
        public void ViewLogin()
        {
            string[] splited = fo.GetLine(login).Split(",", StringSplitOptions.RemoveEmptyEntries);
            _output.LineOutput(splited[0]);
        }
        public void ChangePassword(string newPassword)
        {
            string[] splited = fo.GetLine(login).Split(",", StringSplitOptions.RemoveEmptyEntries);
            fo.ChangeInFile(login, splited[0]+","+newPassword+splited[2]);
        }
    }
}
