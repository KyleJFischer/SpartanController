using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Timers;
using System.Threading;

namespace SpartanController
{
    public partial class MainWindow : Form
    {
        private delegate void SafeCallDelegate(string text);
        /// <summary>
        /// Display a warning at the bottom of the program by setting this string to your warning. Be sure to clear it!
        /// </summary>
        static string warning;
        static List<Command> commands = new List<Command>();
        static List<string> commandsForList = new List<string>();
        static DateTime lastTimeRan = DateTime.MinValue;
        bool saveUponExit = true;
       
        DateTime latestCommandTime = DateTime.Now;
        
        bool lockDownEnabled = Properties.Settings.Default.lockdownEnabled;

        System.Timers.Timer updateTimer = new System.Timers.Timer(1000);


        private void UpdateWarning(object sender, ElapsedEventArgs e)
        {
            WriteTextSafe(warning);
        }

        private void WriteTextSafe(string text)
        {
            if (warningText.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteTextSafe);
                warningText.Invoke(d, new object[] { text });
            }
            else
            {
                warningText.Text = text;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            if (Properties.Settings.Default.startMinimized)
            {
                
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
           

            CreateFileWatcher(Properties.Settings.Default.FolderLocation);

            loadCommands(Properties.Settings.Default.FilePath);
            listBox1.DataSource = commandsForList;
            refreshListBox();

            updateTimer.Elapsed += UpdateWarning;
            updateTimer.Start();
        }

        private void createNewBasicCommand(String command, string path)
        {
            commands.Add(new Command(command, path, comboBox1.Text));
            commandsForList.Add(command + ":  " + path);
            refreshListBox();
        }


        private void refreshListBox()
        {
            listBox1.DataSource = null;
            listBox1.DataSource = commandsForList;
        }

        public void populateListBox()
        {
            commandsForList.Clear();
            foreach (Command temp in commands)
            {
               // commandsForList.Add(temp.getName() + "\t  " + temp.getPath());
                commandsForList.Add(String.Format("{0}  =>  {1}", temp.getName(), temp.getPath()));
            }
            refreshListBox();
        }

        public static void launchTaskControlFile(String path)
        {
            int counter = 0;
            string line;
            System.IO.StreamReader file;

            // Arylos:
            // This is an imperfect solution to issue #2.
            // This is caused by Dropbox still updating the file when the controller tries to read it.
            // Here, try and read the file, if it failes, sleep and try again after Dropbox has finished.
            // If there is a trigger for when Dropbox is finished, that should be used.
            try
            {
                file = new System.IO.StreamReader(path);
            }
            catch (System.IO.IOException)
            {
                warning = "Waiting on Dropbox to finish...";
                Thread.Sleep(Properties.Settings.Default.delay * 1000);
                file = new System.IO.StreamReader(path);
            }
            warning = string.Empty;

            // Read the file and display it line by line.  
            String nonNullLastCommand = "";
            while ((line = file.ReadLine()) != null)
            {
                nonNullLastCommand = line.TrimStart('#');
                counter++;
            }

            file.Close();
            //System.Threading.Thread.Sleep(1000);
            //System.IO.StreamWriter writeFile = new System.IO.StreamWriter(path, false);
            //writeFile.WriteLine("");
            //writeFile.Close();
            if (nonNullLastCommand.Contains("Google"))
            {
                googleSearch(nonNullLastCommand);
            } else if (nonNullLastCommand.Contains("LockDown") && Properties.Settings.Default.lockdownEnabled)
            {
               executeLock();
            } else if (nonNullLastCommand.Contains("Shutdown") && Properties.Settings.Default.shutdownEnabled)
            {
                executeShutdown();
            } else if (nonNullLastCommand.Contains("Restart") && Properties.Settings.Default.restartEnabled)
            {
                executeRestart();
            } else if (nonNullLastCommand.Contains("Password"))
            {
                SendKeys.SendWait(" ");
                System.Threading.Thread.Sleep(500);
                foreach (char temp in "potato")
                {
                    
                    SendKeys.SendWait(temp.ToString());
                }
                //Testing of a new feature. Doesn't work sadly. However, its very unsecure to use this one so it might be a good thing it doesn't work
            } else
            {
                launchApplication(nonNullLastCommand);
            }
            
            // Suspend the screen.  
            System.Console.ReadLine();
        }

        public static void executeShutdown()
        {
            System.Diagnostics.Process.Start("shutdown.exe", "-s -c \"Spartan Controller Shut us down\"");
        }


        public static void executeRestart()
        {
            System.Diagnostics.Process.Start("shutdown.exe", "-r -c \"Spartan Controller Restarted Us it\"");

        }

        public static void executeLock()
        {
            new Command("", "rundll32.exe user32.dll,LockWorkStation", "PowerShell").execute();
            
        }

        public static void googleSearch(string query)
        {
            query = query.Substring(query.IndexOf(" ") + 1);
            query = query.Replace(" ", "+");            
            Command temp = new Command("", "www.google.com/#q=" + query, "Site");
            temp.execute();

        }

        public static void youtubeSearch(string query)
        {
            query = query.Substring(query.IndexOf(" ") + 1);
            query = query.Replace(" ", "+");
            Console.WriteLine(query);
            //Command temp = new Command("Google", "www.netflix.com", "Site");
            Process process = new Process();

            // Stop the process from opening a new window
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            // Setup executable and parameters
            process.StartInfo.FileName = @"C:\Windows\system32\WindowsPowerShell\v1.0\powershell.exe";
            process.StartInfo.Arguments = "Start " + "www.youtube.com/results?search+query=" + query;

            // Go
            process.Start();
            // temp.execute();
        }

        public static void launchApplication(String Command)
        {
            foreach (Command temp in commands)
            {
                if (String.Compare(temp.getName(), Command, true) == 0)
                {
                    temp.execute();
                    break;
                }
            }
        }

        public void CreateFileWatcher(string path)
        {
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = path;
            /* Watch for changes in LastAccess and LastWrite times, and 
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            // Only watch text files.
            watcher.Filter = "controlfile.txt";

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
            
        }

       
        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {

            var lastTimeAccessed = File.GetLastWriteTime(e.FullPath);
            var diffInSeconds = (lastTimeAccessed - lastTimeRan).TotalSeconds;
            // TODO: This does not seem to work, as it will access the file anyway, even with an increased value
            if (diffInSeconds > 5.0)
            {
                lastTimeRan = lastTimeAccessed;
                launchTaskControlFile(e.FullPath);
            }
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(commandTextBox.Text))
            {
                this.createNewBasicCommand(commandTextBox.Text, pathTextBox.Text);
            }
            
            
        }

        private void saveCommands(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Command>));
            TextWriter writer = new StreamWriter(path);
            serializer.Serialize(writer, commands);
            writer.Close();
        }
        private void loadCommands(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Command>));
                    FileStream fs = new FileStream(path, FileMode.Open);
                    List<Command> temp = (List<Command>)serializer.Deserialize(fs);
                    commands.AddRange(temp);
                    populateListBox();
                    fs.Close();
                } catch (Exception e)
                {
                    string message = e.Message;
                    const string caption = "Error Loading Commands from File!";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Error);
                }
             
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (saveUponExit)
            {
                saveCommands(Properties.Settings.Default.FilePath);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Command temp = commands[listBox1.SelectedIndex];
            commandTextBox.Text = temp.getName();
            pathTextBox.Text = temp.getPath();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            multiCommandWizard frm = new multiCommandWizard();
            frm.main = this;
            frm.commands = commands;
            frm.commandsForList = commandsForList;
            frm.Show();
        }

        public void addCommand(Command temp)
        {
            commands.Add(temp);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                commands.RemoveAt(listBox1.SelectedIndex);
            }
            
            populateListBox();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                return;
            }
            commands[listBox1.SelectedIndex].execute();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            editCommandWizard form = new editCommandWizard();
            form.main = this;
            form.command = commands[listBox1.SelectedIndex];
            form.multi = (form.command.getType() == "Multi");
            form.Show();
        }

        public void editCommand(int index, string name, string path, string type)
        {

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsPage form = new SettingsPage();
            form.Show();
        }

        private void saveCommandsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveCommands(Properties.Settings.Default.FilePath);
        }

        private void saveExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exitWOSavingCommandsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveUponExit = false;
            this.Close();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Exe files (*.exe)|*.exe|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if(openFileDialog1.ShowDialog() == DialogResult.OK){
                pathTextBox.Text = openFileDialog1.FileName;
                if (pathTextBox.Text.EndsWith(".exe"))
                {
                    comboBox1.Text = "Exe";
                }
            }
        }

        private void iFTTTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Command("", "www.ifttt.com", "Site").execute();
        }

        private void importCommandsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "xml files (*.xml)|*.xml";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                loadCommands(openFileDialog1.FileName);
            }
            
        }
    }
}
