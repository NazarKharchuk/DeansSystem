﻿using System;

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
                if (Int32.TryParse(splited[i],out x))
                {
                    total += (float) Int32.Parse(splited[i]) / 100;
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
    }
}