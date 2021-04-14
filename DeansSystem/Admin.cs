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

        public void RemoveUser(string loginToRemove)
        {
            _fo.RemoveFromFile(loginToRemove);
            FileOperations fileOps = new FileOperations(path, _output.marksFile);
            fileOps.RemoveFromFile(loginToRemove);
            fileOps = new FileOperations(path, _output.studentsFile);
            fileOps.RemoveFromFile(loginToRemove);
        }
        public void ChangeUsersPassword(string loginToChange, string passw)
        {
            _fo.ChangeInFile(loginToChange, passw);
            string line = _fo.GetLine(loginToChange);
            string[] splited = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
            _fo.ChangeInFile(loginToChange, splited[0] + "," + passw + "," + splited[2]);
            _output.Success();
            }

        public void ChangeGroup(string stLogin, string groupToChange)
        {
            FileOperations fileOps = new FileOperations(path, _output.studentsFile);
            string line = fileOps.GetLine(stLogin);
            string[] splited = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
            if (splited[1][2]==groupToChange[2])
            {
                fileOps.ChangeInFile(stLogin, splited[0]+","+groupToChange+","+splited[2]);
                _output.Success();
            }
            else _output.WrongInput();
        }
        public void ChangeCourse(string stLogin)
        {
            bool isEnough = false;
            FileOperations fops = new FileOperations(path, _output.marksFile);
            string[] splited = fops.GetLine(stLogin).Split(",", StringSplitOptions.RemoveEmptyEntries);
            float total = 0;
            for (int i = 0; i < splited.Length; i++)
            {
                int x = 0;
                if (Int32.TryParse(splited[i], out x))
                {
                    total += (float)Int32.Parse(splited[i]);
                }
            }
            if (total >= 60) isEnough = true;
            if (isEnough)
            {
                fops.ChangeInFile(stLogin, stLogin);
                FileOperations fileOps = new FileOperations(path, _output.studentsFile);
                string line = fileOps.GetLine(stLogin);
                splited = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
                fileOps.ChangeInFile(stLogin, splited[0]+","+splited[1]+","+(Int32.Parse(splited[2])+1));
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

        public void CheckMarks(string stLogin)
        {
            FileOperations fops = new FileOperations(path, _output.marksFile);

            string[] marks = fops.GetLine(stLogin).Split(",", StringSplitOptions.RemoveEmptyEntries);
            string line = $"{stLogin}'s marks: ";
            for (int i = 1; i < marks.Length; i++)
            {
                line += marks[i];
                if (i + 1 < marks.Length) line += ", ";
            }
            _output.LineOutput(line);
        }
    }
}
