using System;
using System.Collections.Generic;

namespace DeansSystem
{
    public class Teacher : User
    {
        private string login;
        private FileOperations fo;
        private Output _output;

        public Teacher(string login, string path, string fileName) : base(login, path, fileName)
        {
            this.login = login;
            fo = new FileOperations(path, fileName);
            _output = new Output();
        }
        public void ChangeMarks(string userLogin, string line)
        {
            fo.ChangeInFile(userLogin, line);
            List<string> information = fo.ReadAllFile();
            _output.ListOutput(information);
        }
        public void WatchStudent(string userLogin) => _output.LineOutput(fo.GetLine(userLogin));
        public void WatchAllInformation()
        {
            List<string> infromation = fo.ReadAllFile();
            _output.ListOutput(infromation);
        }
        public void CheckGroup(string st_login)
        {
            fo = new FileOperations(_output.path, _output.studentsFile);
            string[] splited = fo.GetLine(st_login).Split(",", StringSplitOptions.RemoveEmptyEntries);
            Console.Write(st_login+"'s group: "); _output.LineOutput(splited[splited.Length - 2]);
        }
        public void CheckCourse(string st_login)
        {
            fo = new FileOperations(_output.path, _output.studentsFile);
            string[] splited = fo.GetLine(st_login).Split(",", StringSplitOptions.RemoveEmptyEntries);
            Console.Write(st_login+"'s course: "); _output.LineOutput(splited[splited.Length - 1]);
        }
    }
}
