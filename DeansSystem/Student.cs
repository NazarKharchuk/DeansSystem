using System;

namespace DeansSystem
{
    public class Student : User
    {
        private string login;
        private FileOperations _fo;
        private Output _output;

        public Student(string login, string path, string fileName) : base(login, path, fileName)
        {
            this.login = login;
            _fo = new FileOperations(path, fileName);
            _output = new Output();
        }
        public void CheckYourMarks() => _output.LineOutput(_fo.GetLine(login));
        public void CheckGroup()
        {
            _fo = new FileOperations(_output.path, _output.studentsFile);
            string[] splited = _fo.GetLine(login).Split();
            _output.LineOutput(splited[splited.Length - 2]);
        }
        public void CheckCourse()
        {
            _fo = new FileOperations(_output.path, _output.studentsFile);
            string[] splited = _fo.GetLine(login).Split();
            _output.LineOutput(splited[splited.Length - 1]);
        }
    }
}
