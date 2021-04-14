using System;

namespace DeansSystem
{
    public class Admin : User
    {
        private string login;
        private FileOperations _fo;
        private string path;
        private Output _output;
        public Admin(string login, string nPath, string fileName) : base(login, nPath, fileName)
        {
            _fo = new FileOperations(nPath, fileName);
            _output = new Output();
            path = nPath;
        }
        public void AddUser(string loginToAdd, string password, string accessLevel, string group = "", string course = "1")
        {
            string line = loginToAdd + "," + password + "," + accessLevel;
            _fo.AddToFile(line);
            if (Convert.ToInt32(accessLevel) == 1)
            {
                FileOperations fileOps = new FileOperations(path, _output.studentsFile);
                fileOps.AddToFile(loginToAdd+","+group+","+course);
                fileOps = new FileOperations(path, _output.marksFile);
                fileOps.AddToFile(loginToAdd);
            }
        }
        public void RemoveUser(string loginToRemove) => _fo.RemoveFromFile(loginToRemove);
        public void ChangeUserInformation(string loginToChange, string newLine) => _fo.ChangeInFile(loginToChange, newLine);
        public void ChangeGroup(string stLogin, string groupToChange, string fileName)
        {
            FileOperations fileOps = new FileOperations(path, fileName);
            string line = fileOps.GetLine(stLogin);
            string[] splited = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
            if (splited[1][2]==groupToChange[2])
            {
                fileOps.ChangeInFile(stLogin, splited[0]+","+groupToChange+","+splited[2]);
                _output.Success();
            }
            else _output.WrongInput();
        }
        public void ChangeCourse(string stLogin, string fileName)
        {
            bool isEnough = false;
            FileOperations fops = new FileOperations(path, _output.studentsFile);
            string[] splited = fops.GetLine(stLogin).Split(",", StringSplitOptions.RemoveEmptyEntries);
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
            if (isEnough)
            {
                FileOperations fileOps = new FileOperations(path, fileName);
                string line = fileOps.GetLine(stLogin);
                splited = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
                string newLine = String.Empty;
                for (int i = 0; i < splited.Length; i++)
                {
                    if (i == splited.Length - 1) newLine += "," + Convert.ToString(Int32.Parse(splited[i]) + 1);
                    else if (i == 0) newLine += splited[i];
                    else newLine += "," + splited[i];
                }
                fileOps.ChangeInFile(stLogin, newLine);
                _output.Success();
            }
        }
        public void CheckGroup(string stLogin)
        {
            _fo = new FileOperations(_output.path, _output.studentsFile);
            string[] splited = _fo.GetLine(stLogin).Split(",", StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(stLogin + "'s group: "); _output.LineOutput(splited[splited.Length - 2]);
        }
        public void CheckCourse(string stLogin)
        {
            _fo = new FileOperations(_output.path, _output.studentsFile);
            string[] splited = _fo.GetLine(stLogin).Split(",", StringSplitOptions.RemoveEmptyEntries);
            Console.Write(stLogin+"'s course: "); _output.LineOutput(splited[splited.Length - 1]);
        }
        public void CheckMarks(string stLogin) => _output.LineOutput(_fo.GetLine(stLogin));
    }
}
