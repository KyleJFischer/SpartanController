using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SpartanController
{
   public class Project
    {
        [XmlAttribute]
        public string settingsPath;
        [XmlArray]
        public List<Command> commands;
        

        public Project()
        {
            commands = new List<Command>();
            settingsPath = "Hello World";
        }
       public Project(List<Command> commandsT, string path)
        {
            this.commands = commandsT;
            this.settingsPath = path;
        }
    }

}
