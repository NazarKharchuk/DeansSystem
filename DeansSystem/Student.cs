using System;

namespace DeansSystem
{
    public class Student : User
    {
        private string login;
        private FileOperations fo;
        private Output _output;

        public Student(string login, string path, string fileName) : base(login, path, fileName)
        {
            this.login = login;
            fo = new FileOperations(path, fileName);
            _output = new Output();
        }
        public void CheckYourMarks() => _output.LineOutput(fo.GetLine(login));
        public void CheckGroup()
        {
            fo = new FileOperations(_output.path, _output.studentsFile);
            string[] splited = fo.GetLine(login).Split();
            _output.LineOutput(splited[splited.Length - 2]);
        }
        public void CheckCourse()
        {
            fo = new FileOperations(_output.path, _output.studentsFile);
            string[] splited = fo.GetLine(login).Split();
            _output.LineOutput(splited[splited.Length - 1]);
        }
    }
}
