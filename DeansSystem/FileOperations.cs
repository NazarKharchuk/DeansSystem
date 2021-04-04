using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace DeansSystem
{
    public class FileOperations
    {
        private string path;

        public FileOperations(string p, string fileName)
        {
            this.path = Path.Combine(p, fileName);
        }
        public void AddToFile(string line)
        {
            if (!File.Exists(path))
            {
                using (File.Create(path)) { }
            }
            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                sw.WriteLine(line);
            }
        }
        public void ChangeInFile(string login, string line)
        {
            List<string> information = new List<string>();
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string item = sr.ReadLine();
                    string[] splited = item.Split(", ", StringSplitOptions.RemoveEmptyEntries);
                    if (splited[0].Equals(login)) information.Add(line);
                    else information.Add(item);
                }
            }
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                foreach (string item in information)
                {
                    sw.WriteLine(item);
                }
            }
        }
        public void RemoveFromFile(string login)
        {
            List<string> information = new List<string>();
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string item = sr.ReadLine();
                    string[] splited = item.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    if (!splited[0].Equals(login)) information.Add(item);
                }
            }
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                foreach (string item in information)
                {
                    sw.WriteLine(item);
                }
            }
        }
        public string GetLine(string login)
        {
            string item = String.Empty;
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    item = sr.ReadLine();
                    string[] splited = item.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    if (splited[0].Equals(login)) return item;
                }
            }
            return item;
        }
        public List<string> ReadAllFile()
        {
            List<string> information = new List<string>();
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                information.Add(sr.ReadLine());
            }
            return information;
        }
        public bool IsExists(string line)
        {
            string[] splitedLine = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string item = sr.ReadLine();
                    string[] splitedItem = item.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    if (splitedItem[0].Equals(splitedLine[0]) && splitedItem[1].Equals(splitedLine[1])) return true;
                }
            }
            return false;
        }
    }
}