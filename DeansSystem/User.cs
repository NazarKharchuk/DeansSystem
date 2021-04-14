using System;

namespace DeansSystem
{
    public class User
    {
        private string login;
        private string _path;
        private string _fileName;
        private FileOperations _fo;
        private Output _output;

        public User(string login, string path, string fileName)
        {
            this.login = login;
            _path = path;
            this._fileName = fileName;
            _fo = new FileOperations(path, fileName);
            _output = new Output();
        }
        public bool Authorization(string password)
        {
            string line = login + "," + password;
            if (_fo.IsExists(line))
            {
                string[] splited = _fo.GetLine(login).Split(",", StringSplitOptions.RemoveEmptyEntries);
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
            string[] splited = _fo.GetLine(login).Split(",", StringSplitOptions.RemoveEmptyEntries);
            _output.LineOutput(splited[1]);
        }
        public void ChangePassword(string newPassword)
        {
            string[] splited = _fo.GetLine(login).Split(",", StringSplitOptions.RemoveEmptyEntries);
            _fo.ChangeInFile(login, splited[0]+","+newPassword+","+splited[2]);
        }
    }
}
