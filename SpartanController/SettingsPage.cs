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
    public partial class SettingsPage : Form
    {
     
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void SettingsPage_Load(object sender, EventArgs e)
        {
            lockModeEnabled.Checked = Properties.Settings.Default.lockdownEnabled;
            shutdownModeEnabled.Checked = Properties.Settings.Default.shutdownEnabled;
            restartCheckBox.Checked = Properties.Settings.Default.restartEnabled;
            folderlocationtextbox.Text = Properties.Settings.Default.FolderLocation;
            commandSaveTextbox.Text = Properties.Settings.Default.FilePath;
            startMiniCheckbox.Checked = Properties.Settings.Default.startMinimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.FilePath = commandSaveTextbox.Text;
            Properties.Settings.Default.lockdownEnabled = lockModeEnabled.Checked;
            Properties.Settings.Default.shutdownEnabled = shutdownModeEnabled.Checked;
            Properties.Settings.Default.FolderLocation = folderlocationtextbox.Text;
            Properties.Settings.Default.restartEnabled = restartCheckBox.Checked;
            Properties.Settings.Default.startMinimized = startMiniCheckbox.Checked;
            Properties.Settings.Default.Save();
            this.Close();

        }

        private void browseCommandLocation_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "xml files (*.xml)|*.xml";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                commandSaveTextbox.Text = saveFileDialog1.FileName;
            }
        }

        private void browseFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderlocationtextbox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
