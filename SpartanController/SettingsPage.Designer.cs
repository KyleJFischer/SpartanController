namespace SpartanController
{
    partial class SettingsPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.browseCommandLocation = new System.Windows.Forms.Button();
            this.commandSaveTextbox = new System.Windows.Forms.TextBox();
            this.folderlocationtextbox = new System.Windows.Forms.TextBox();
            this.browseFolder = new System.Windows.Forms.Button();
            this.lockModeEnabled = new System.Windows.Forms.CheckBox();
            this.shutdownModeEnabled = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.restartCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // browseCommandLocation
            // 
            this.browseCommandLocation.Location = new System.Drawing.Point(335, 42);
            this.browseCommandLocation.Name = "browseCommandLocation";
            this.browseCommandLocation.Size = new System.Drawing.Size(96, 23);
            this.browseCommandLocation.TabIndex = 0;
            this.browseCommandLocation.Text = "Browse";
            this.browseCommandLocation.UseVisualStyleBackColor = true;
            this.browseCommandLocation.Click += new System.EventHandler(this.browseCommandLocation_Click);
            // 
            // commandSaveTextbox
            // 
            this.commandSaveTextbox.Location = new System.Drawing.Point(22, 44);
            this.commandSaveTextbox.Name = "commandSaveTextbox";
            this.commandSaveTextbox.Size = new System.Drawing.Size(249, 20);
            this.commandSaveTextbox.TabIndex = 1;
            // 
            // folderlocationtextbox
            // 
            this.folderlocationtextbox.Location = new System.Drawing.Point(22, 83);
            this.folderlocationtextbox.Name = "folderlocationtextbox";
            this.folderlocationtextbox.Size = new System.Drawing.Size(249, 20);
            this.folderlocationtextbox.TabIndex = 3;
            // 
            // browseFolder
            // 
            this.browseFolder.Location = new System.Drawing.Point(335, 81);
            this.browseFolder.Name = "browseFolder";
            this.browseFolder.Size = new System.Drawing.Size(96, 23);
            this.browseFolder.TabIndex = 2;
            this.browseFolder.Text = "Browse";
            this.browseFolder.UseVisualStyleBackColor = true;
            this.browseFolder.Click += new System.EventHandler(this.browseFolder_Click);
            // 
            // lockModeEnabled
            // 
            this.lockModeEnabled.AutoSize = true;
            this.lockModeEnabled.Location = new System.Drawing.Point(22, 138);
            this.lockModeEnabled.Name = "lockModeEnabled";
            this.lockModeEnabled.Size = new System.Drawing.Size(80, 17);
            this.lockModeEnabled.TabIndex = 4;
            this.lockModeEnabled.Text = "Lock Mode";
            this.lockModeEnabled.UseVisualStyleBackColor = true;
            // 
            // shutdownModeEnabled
            // 
            this.shutdownModeEnabled.AutoSize = true;
            this.shutdownModeEnabled.Location = new System.Drawing.Point(22, 162);
            this.shutdownModeEnabled.Name = "shutdownModeEnabled";
            this.shutdownModeEnabled.Size = new System.Drawing.Size(109, 17);
            this.shutdownModeEnabled.TabIndex = 5;
            this.shutdownModeEnabled.Text = "Shut Down Mode";
            this.shutdownModeEnabled.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(356, 272);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // restartCheckBox
            // 
            this.restartCheckBox.AutoSize = true;
            this.restartCheckBox.Location = new System.Drawing.Point(22, 186);
            this.restartCheckBox.Name = "restartCheckBox";
            this.restartCheckBox.Size = new System.Drawing.Size(90, 17);
            this.restartCheckBox.TabIndex = 7;
            this.restartCheckBox.Text = "Restart Mode";
            this.restartCheckBox.UseVisualStyleBackColor = true;
            // 
            // SettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 307);
            this.Controls.Add(this.restartCheckBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.shutdownModeEnabled);
            this.Controls.Add(this.lockModeEnabled);
            this.Controls.Add(this.folderlocationtextbox);
            this.Controls.Add(this.browseFolder);
            this.Controls.Add(this.commandSaveTextbox);
            this.Controls.Add(this.browseCommandLocation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsPage";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SettingsPage";
            this.Load += new System.EventHandler(this.SettingsPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseCommandLocation;
        private System.Windows.Forms.TextBox commandSaveTextbox;
        private System.Windows.Forms.TextBox folderlocationtextbox;
        private System.Windows.Forms.Button browseFolder;
        private System.Windows.Forms.CheckBox lockModeEnabled;
        private System.Windows.Forms.CheckBox shutdownModeEnabled;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox restartCheckBox;
    }
}