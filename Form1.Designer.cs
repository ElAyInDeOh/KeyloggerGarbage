namespace KeyloggerDefender
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.logBox = new System.Windows.Forms.TextBox();
            this.protectionCheckbox = new System.Windows.Forms.CheckBox();
            this.exportButton = new System.Windows.Forms.Button();
            this.settingsGroupBox = new System.Windows.Forms.GroupBox();
            this.protectionTrackBar = new System.Windows.Forms.TrackBar();
            this.frequencyLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.activityLogLabel = new System.Windows.Forms.Label();
            this.activityListBox = new System.Windows.Forms.ListBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.settingsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.protectionTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(12, 12);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(460, 100);
            this.logBox.TabIndex = 0;
            this.toolTip1.SetToolTip(this.logBox, "Live keystroke display");
            // 
            // protectionCheckbox
            // 
            this.protectionCheckbox.AutoSize = true;
            this.protectionCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.protectionCheckbox.Location = new System.Drawing.Point(12, 125);
            this.protectionCheckbox.Name = "protectionCheckbox";
            this.protectionCheckbox.Size = new System.Drawing.Size(140, 19);
            this.protectionCheckbox.TabIndex = 1;
            this.protectionCheckbox.Text = "Enable Protection";
            this.protectionCheckbox.UseVisualStyleBackColor = true;
            this.protectionCheckbox.CheckedChanged += new System.EventHandler(this.protectionCheckbox_CheckedChanged);
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(397, 434);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 23);
            this.exportButton.TabIndex = 2;
            this.exportButton.Text = "Export Log";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // settingsGroupBox
            // 
            this.settingsGroupBox.Controls.Add(this.protectionTrackBar);
            this.settingsGroupBox.Controls.Add(this.frequencyLabel);
            this.settingsGroupBox.Location = new System.Drawing.Point(12, 150);
            this.settingsGroupBox.Name = "settingsGroupBox";
            this.settingsGroupBox.Size = new System.Drawing.Size(460, 80);
            this.settingsGroupBox.TabIndex = 3;
            this.settingsGroupBox.TabStop = false;
            this.settingsGroupBox.Text = "Protection Settings";
            // 
            // protectionTrackBar
            // 
            this.protectionTrackBar.Location = new System.Drawing.Point(6, 37);
            this.protectionTrackBar.Minimum = 1;
            this.protectionTrackBar.Name = "protectionTrackBar";
            this.protectionTrackBar.Size = new System.Drawing.Size(448, 45);
            this.protectionTrackBar.TabIndex = 1;
            this.protectionTrackBar.Value = 5;
            // 
            // frequencyLabel
            // 
            this.frequencyLabel.AutoSize = true;
            this.frequencyLabel.Location = new System.Drawing.Point(6, 20);
            this.frequencyLabel.Name = "frequencyLabel";
            this.frequencyLabel.Size = new System.Drawing.Size(154, 13);
            this.frequencyLabel.TabIndex = 0;
            this.frequencyLabel.Text = "Garbage Keystroke Frequency:";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.ForeColor = System.Drawing.Color.Red;
            this.statusLabel.Location = new System.Drawing.Point(155, 126);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(129, 15);
            this.statusLabel.TabIndex = 4;
            this.statusLabel.Text = "Protection: Inactive";
            // 
            // activityLogLabel
            // 
            this.activityLogLabel.AutoSize = true;
            this.activityLogLabel.Location = new System.Drawing.Point(12, 237);
            this.activityLogLabel.Name = "activityLogLabel";
            this.activityLogLabel.Size = new System.Drawing.Size(65, 13);
            this.activityLogLabel.TabIndex = 5;
            this.activityLogLabel.Text = "Activity Log:";
            // 
            // activityListBox
            // 
            this.activityListBox.FormattingEnabled = true;
            this.activityListBox.Location = new System.Drawing.Point(12, 253);
            this.activityListBox.Name = "activityListBox";
            this.activityListBox.Size = new System.Drawing.Size(460, 173);
            this.activityListBox.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 471);
            this.Controls.Add(this.activityListBox);
            this.Controls.Add(this.activityLogLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.settingsGroupBox);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.protectionCheckbox);
            this.Controls.Add(this.logBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Anti-Keylogger Protection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.settingsGroupBox.ResumeLayout(false);
            this.settingsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.protectionTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.CheckBox protectionCheckbox;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.GroupBox settingsGroupBox;
        private System.Windows.Forms.TrackBar protectionTrackBar;
        private System.Windows.Forms.Label frequencyLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label activityLogLabel;
        private System.Windows.Forms.ListBox activityListBox;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}