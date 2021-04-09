using System;

namespace DeansSystem
{
    public class Admin : User
    {
        private string login;
        private FileOperations fo;
        private string path;
        private Output _output;
        public Admin(string login, string path, string fileName) : base(login, path, fileName)
        {
            fo = new FileOperations(path, fileName);
            _output = new Output();
            this.path = path;
        }
        public void AddUser(string loginToAdd, string password, string accessLevel)
        {
            string line = loginToAdd + "," + password + "," + accessLevel;
            fo.AddToFile(line);
            if (Convert.ToInt32(accessLevel) == 1)
            {
                FileOperations fileOps = new FileOperations(path, _output.studentsFile);
                fileOps.AddToFile(loginToAdd);
                fileOps = new FileOperations(path, _output.marksFile);
                fileOps.AddToFile(loginToAdd);
            }
        }
        public void RemoveUser(string loginToRemove) => fo.RemoveFromFile(loginToRemove);
        public void ChangeUserInformation(string loginToChange, string newLine) => fo.ChangeInFile(loginToChange, newLine);
        public void ChangeGroup(string login, string groupToChange, string fileName)
        {
            FileOperations fileOps = new FileOperations(path, fileName);
            string line = fileOps.GetLine(login);
            string[] splited = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
            string newLine = String.Empty;
            for (int i = 0; i < splited.Length; i++)
            {
                if (i == splited.Length - 1) newLine += "," + groupToChange;
                else if (i == 0) newLine += splited[i];
                else newLine += "," + splited[i];
            }
            fileOps.ChangeInFile(login, newLine);
            _output.Success();
        }
        public void ChangeCourse(string login, string fileName)
        {
            bool isEnough = false;
            FileOperations fops = new FileOperations(path, _output.studentsFile);
            string[] splited = fops.GetLine(login).Split(",", StringSplitOptions.RemoveEmptyEntries);
            float total = 0;
            int marksNumber = 0;
            for (int i = 0; i < splited.Length; i++)
            {
                int x = 0;
                if (Int32.TryParse(splited[i], out x))
                {
                    total += (float)Int32.Parse(splited[i]) / 100;
                    marksNumber++;
                }
            }
            if (total / marksNumber >= 0.6) isEnough = true;
            if (isEnough == true)
            {
                FileOperations fileOps = new FileOperations(path, fileName);
                string line = fileOps.GetLine(login);
                splited = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
                string newLine = String.Empty;
                for (int i = 0; i < splited.Length; i++)
                {
                    if (i == splited.Length - 1) newLine += "," + Convert.ToString(Int32.Parse(splited[i]) + 1);
                    else if (i == 0) newLine += splited[i];
                    else newLine += "," + splited[i];
                }
                fileOps.ChangeInFile(login, newLine);
                _output.Success();
            }
        }
        public void CheckGroup(string st_login)
        {
            fo = new FileOperations(_output.path, _output.studentsFile);
            string[] splited = fo.GetLine(st_login).Split(",", StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(st_login + "'s group: "); _output.LineOutput(splited[splited.Length - 2]);
        }
        public void CheckCourse(string st_login)
        {
            fo = new FileOperations(_output.path, _output.studentsFile);
            string[] splited = fo.GetLine(st_login).Split(",", StringSplitOptions.RemoveEmptyEntries);
            Console.Write(st_login+"'s course: "); _output.LineOutput(splited[splited.Length - 1]);
        }
        public void CheckMarks(string st_login) => _output.LineOutput(fo.GetLine(st_login));
    }
}
