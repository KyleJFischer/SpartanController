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
    public partial class editCommandWizard : Form
    {
        public MainWindow main;
        public Boolean multi;
        public Command command;
        public editCommandWizard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            command.changeName(nameTextBox.Text);
            
            if (!multi)
            {
                command.changePath(pathTextBox.Text);
                command.changeType(typeComboBox.Text);
            }
            main.populateListBox();
            Close();
        }

        private void editCommandWizard_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            typeComboBox.Enabled = !multi;
            pathTextBox.Enabled = !multi;
            nameTextBox.Text = command.getName();
            pathTextBox.Text = command.getPath();
            if (!multi)
            {
                typeComboBox.Text = command.getType();
            }
        }

        private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
