using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpartanController
{
    public partial class multiCommandWizard : Form
    {

        public MainWindow main;
        public List<String> commandsForList;
        public List<Command> commands;
        public multiCommandWizard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Command newCommand = new Command();
            newCommand.changeName(textBox1.Text);
            newCommand.changeType("Multi");
            string executionPath = "Will Execute: ";
           foreach(int itemChecked in checkedListBox1.CheckedIndices)
            {
                newCommand.addMulti(commands[itemChecked]);
                executionPath += commands[itemChecked].getName() + ", ";
            }
            newCommand.changePath(executionPath);
            main.addCommand(newCommand);

            main.populateListBox();
            this.Close();
        }

        private void multiCommandWizard_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            checkedListBox1.DataSource = null;
            checkedListBox1.DataSource = commandsForList;
        }
    }
}
