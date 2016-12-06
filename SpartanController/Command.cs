using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SpartanController
{
    public class Command
    {
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public string executionPath;
        [XmlAttribute]
        public string type;
        [XmlArray]
        public List<Command> multiCommands = new List<Command>();

        public Command(string passedName, string ExecutionPath, string Type)
        {
            name = passedName;
            executionPath = ExecutionPath;
            type = Type;
        }

        public Command()
        {
            this.name = "";
            this.executionPath = "";
            this.type = "";
        }

        public string getName()
        {
            return name;
        }
        public string getPath()
        {
            return executionPath;
        }
        public string getType()
        {
            return type;
        }
        public void changePath(String path)
        {
            executionPath = path;
        }
        public void changeName(String name)
        {
            this.name = name;
        }
        public void changeType(String type)
        {
            this.type = type;
        }
        public void execute()
        {
            switch (this.type)
            {
                case "Exe":
                    System.Diagnostics.Process.Start(this.executionPath);
                    break;
                case "Multi":
                    foreach (Command temp in multiCommands)
                    {
                        temp.execute();
                    }
                    break;
                case "Site":
                    Process process = new Process();
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.FileName = @"C:\Windows\system32\WindowsPowerShell\v1.0\powershell.exe";
                    process.StartInfo.Arguments = "Start " + this.getPath();
                    process.Start();
                    break;
                case "Type":
                    foreach (char temp in this.getPath())
                    {
                        SendKeys.SendWait(temp.ToString());
                    }
                    break;
                case "PowerShell":
                    Process process2 = new Process();
                    process2.StartInfo.RedirectStandardOutput = true;
                    process2.StartInfo.UseShellExecute = false;
                    process2.StartInfo.CreateNoWindow = true;
                    process2.StartInfo.FileName = @"C:\Windows\system32\WindowsPowerShell\v1.0\powershell.exe";
                    process2.StartInfo.Arguments = this.getPath();
                    process2.Start();
                    break;
                default:

                    break;
            }
        }

        public void addMulti(Command ob)
        {
            multiCommands.Add(ob);
        }

    }

    
}
