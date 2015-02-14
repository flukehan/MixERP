namespace MixERP.Net.Utility.Installer.UI
{
    partial class InstallationForm
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
            this.InstallButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.GroupBox = new System.Windows.Forms.GroupBox();
            this.IISStatusLabel = new System.Windows.Forms.Label();
            this.ValidateSiteButton = new System.Windows.Forms.Button();
            this.WillBeCreatedLabel = new System.Windows.Forms.Label();
            this.ValidateDatabaseButton = new System.Windows.Forms.Button();
            this.PostgreSQLStatusLabel = new System.Windows.Forms.Label();
            this.DatabaseNameTextBox = new System.Windows.Forms.TextBox();
            this.DatabaseNameLabel = new System.Windows.Forms.Label();
            this.ReportUserPassword = new System.Windows.Forms.TextBox();
            this.ReportUserPasswordLabel = new System.Windows.Forms.Label();
            this.MixERPPassword = new System.Windows.Forms.TextBox();
            this.MixERPUserPasswordLabel = new System.Windows.Forms.Label();
            this.PostgresPassword = new System.Windows.Forms.TextBox();
            this.PostgresUserPasswordLabel = new System.Windows.Forms.Label();
            this.PortNumberTextBox = new System.Windows.Forms.TextBox();
            this.PortNumberLabel = new System.Windows.Forms.Label();
            this.HostNameTextBox = new System.Windows.Forms.TextBox();
            this.HostnameLabel = new System.Windows.Forms.Label();
            this.ApplicationPoolNameTextBox = new System.Windows.Forms.TextBox();
            this.ApplicationPoolNameLabel = new System.Windows.Forms.Label();
            this.SiteNameTextBox = new System.Windows.Forms.TextBox();
            this.SiteNameLabel = new System.Windows.Forms.Label();
            this.BrowseInstallDirectoryButton = new System.Windows.Forms.Button();
            this.InstallationDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.InstallationDirectoryLabel = new System.Windows.Forms.Label();
            this.ActivityProgressBar = new System.Windows.Forms.ProgressBar();
            this.StatusProgressBar = new System.Windows.Forms.ProgressBar();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.StatusProgressLabel = new System.Windows.Forms.Label();
            this.OfficeInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.AdminPasswordTextBox = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.AdminUserNameTextBox = new System.Windows.Forms.TextBox();
            this.AdminUserNameLabel = new System.Windows.Forms.Label();
            this.RegistrationDatePicker = new System.Windows.Forms.DateTimePicker();
            this.AdminNameTextBox = new System.Windows.Forms.TextBox();
            this.AdminNameLabel = new System.Windows.Forms.Label();
            this.HundredthNameTextBox = new System.Windows.Forms.TextBox();
            this.HundredthNameLabel = new System.Windows.Forms.Label();
            this.CurrencyNameTextBox = new System.Windows.Forms.TextBox();
            this.CurrencyNameLabel = new System.Windows.Forms.Label();
            this.CurrencySymbolTextBox = new System.Windows.Forms.TextBox();
            this.CurrencySymbolLabel = new System.Windows.Forms.Label();
            this.CurrencyCodeTextBox = new System.Windows.Forms.TextBox();
            this.CurrencyCodeLabel = new System.Windows.Forms.Label();
            this.RegistrationDateLabel = new System.Windows.Forms.Label();
            this.NickNameTextBox = new System.Windows.Forms.TextBox();
            this.NickNameLabel = new System.Windows.Forms.Label();
            this.OfficeNameTextBox = new System.Windows.Forms.TextBox();
            this.OfficeNameLabel = new System.Windows.Forms.Label();
            this.OfficeCodeTextBox = new System.Windows.Forms.TextBox();
            this.OfficeCodeLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.InstallSampleCheckBox = new System.Windows.Forms.CheckBox();
            this.GroupBox.SuspendLayout();
            this.OfficeInfoGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // InstallButton
            // 
            this.InstallButton.Location = new System.Drawing.Point(12, 545);
            this.InstallButton.Name = "InstallButton";
            this.InstallButton.Size = new System.Drawing.Size(75, 23);
            this.InstallButton.TabIndex = 51;
            this.InstallButton.Text = "&Install";
            this.InstallButton.UseVisualStyleBackColor = true;
            this.InstallButton.Click += new System.EventHandler(this.InstallButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(94, 545);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 52;
            this.CloseButton.Text = "&Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // GroupBox
            // 
            this.GroupBox.Controls.Add(this.IISStatusLabel);
            this.GroupBox.Controls.Add(this.ValidateSiteButton);
            this.GroupBox.Controls.Add(this.WillBeCreatedLabel);
            this.GroupBox.Controls.Add(this.ValidateDatabaseButton);
            this.GroupBox.Controls.Add(this.PostgreSQLStatusLabel);
            this.GroupBox.Controls.Add(this.DatabaseNameTextBox);
            this.GroupBox.Controls.Add(this.DatabaseNameLabel);
            this.GroupBox.Controls.Add(this.ReportUserPassword);
            this.GroupBox.Controls.Add(this.ReportUserPasswordLabel);
            this.GroupBox.Controls.Add(this.MixERPPassword);
            this.GroupBox.Controls.Add(this.MixERPUserPasswordLabel);
            this.GroupBox.Controls.Add(this.PostgresPassword);
            this.GroupBox.Controls.Add(this.PostgresUserPasswordLabel);
            this.GroupBox.Controls.Add(this.PortNumberTextBox);
            this.GroupBox.Controls.Add(this.PortNumberLabel);
            this.GroupBox.Controls.Add(this.HostNameTextBox);
            this.GroupBox.Controls.Add(this.HostnameLabel);
            this.GroupBox.Controls.Add(this.ApplicationPoolNameTextBox);
            this.GroupBox.Controls.Add(this.ApplicationPoolNameLabel);
            this.GroupBox.Controls.Add(this.SiteNameTextBox);
            this.GroupBox.Controls.Add(this.SiteNameLabel);
            this.GroupBox.Controls.Add(this.BrowseInstallDirectoryButton);
            this.GroupBox.Controls.Add(this.InstallationDirectoryTextBox);
            this.GroupBox.Controls.Add(this.InstallationDirectoryLabel);
            this.GroupBox.Location = new System.Drawing.Point(12, 73);
            this.GroupBox.Name = "GroupBox";
            this.GroupBox.Size = new System.Drawing.Size(435, 367);
            this.GroupBox.TabIndex = 5;
            this.GroupBox.TabStop = false;
            this.GroupBox.Text = "Server Information";
            // 
            // IISStatusLabel
            // 
            this.IISStatusLabel.AutoSize = true;
            this.IISStatusLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IISStatusLabel.ForeColor = System.Drawing.Color.OliveDrab;
            this.IISStatusLabel.Location = new System.Drawing.Point(295, 149);
            this.IISStatusLabel.Name = "IISStatusLabel";
            this.IISStatusLabel.Size = new System.Drawing.Size(67, 13);
            this.IISStatusLabel.TabIndex = 9;
            this.IISStatusLabel.Text = "PostgreSQL";
            // 
            // ValidateSiteButton
            // 
            this.ValidateSiteButton.Location = new System.Drawing.Point(295, 173);
            this.ValidateSiteButton.Name = "ValidateSiteButton";
            this.ValidateSiteButton.Size = new System.Drawing.Size(75, 23);
            this.ValidateSiteButton.TabIndex = 12;
            this.ValidateSiteButton.Text = "&Validate";
            this.ValidateSiteButton.UseVisualStyleBackColor = true;
            this.ValidateSiteButton.Visible = false;
            this.ValidateSiteButton.Click += new System.EventHandler(this.ValidateSiteButton_Click);
            // 
            // WillBeCreatedLabel
            // 
            this.WillBeCreatedLabel.AutoSize = true;
            this.WillBeCreatedLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WillBeCreatedLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.WillBeCreatedLabel.Location = new System.Drawing.Point(295, 293);
            this.WillBeCreatedLabel.Name = "WillBeCreatedLabel";
            this.WillBeCreatedLabel.Size = new System.Drawing.Size(84, 13);
            this.WillBeCreatedLabel.TabIndex = 21;
            this.WillBeCreatedLabel.Text = "Will be created";
            this.WillBeCreatedLabel.Visible = false;
            // 
            // ValidateDatabaseButton
            // 
            this.ValidateDatabaseButton.Location = new System.Drawing.Point(213, 318);
            this.ValidateDatabaseButton.Name = "ValidateDatabaseButton";
            this.ValidateDatabaseButton.Size = new System.Drawing.Size(205, 22);
            this.ValidateDatabaseButton.TabIndex = 22;
            this.ValidateDatabaseButton.Text = "&Validate Database Information";
            this.ValidateDatabaseButton.UseVisualStyleBackColor = true;
            this.ValidateDatabaseButton.Visible = false;
            this.ValidateDatabaseButton.Click += new System.EventHandler(this.ValidateDatabaseButton_Click);
            // 
            // PostgreSQLStatusLabel
            // 
            this.PostgreSQLStatusLabel.AutoSize = true;
            this.PostgreSQLStatusLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PostgreSQLStatusLabel.ForeColor = System.Drawing.Color.OliveDrab;
            this.PostgreSQLStatusLabel.Location = new System.Drawing.Point(210, 343);
            this.PostgreSQLStatusLabel.Name = "PostgreSQLStatusLabel";
            this.PostgreSQLStatusLabel.Size = new System.Drawing.Size(0, 13);
            this.PostgreSQLStatusLabel.TabIndex = 23;
            // 
            // DatabaseNameTextBox
            // 
            this.DatabaseNameTextBox.Location = new System.Drawing.Point(213, 289);
            this.DatabaseNameTextBox.MaxLength = 24;
            this.DatabaseNameTextBox.Name = "DatabaseNameTextBox";
            this.DatabaseNameTextBox.Size = new System.Drawing.Size(75, 22);
            this.DatabaseNameTextBox.TabIndex = 20;
            this.DatabaseNameTextBox.Text = "mixerp";
            // 
            // DatabaseNameLabel
            // 
            this.DatabaseNameLabel.AutoSize = true;
            this.DatabaseNameLabel.Location = new System.Drawing.Point(22, 294);
            this.DatabaseNameLabel.Name = "DatabaseNameLabel";
            this.DatabaseNameLabel.Size = new System.Drawing.Size(87, 13);
            this.DatabaseNameLabel.TabIndex = 19;
            this.DatabaseNameLabel.Text = "&Database Name";
            // 
            // ReportUserPassword
            // 
            this.ReportUserPassword.Location = new System.Drawing.Point(213, 260);
            this.ReportUserPassword.MaxLength = 48;
            this.ReportUserPassword.Name = "ReportUserPassword";
            this.ReportUserPassword.PasswordChar = '*';
            this.ReportUserPassword.Size = new System.Drawing.Size(205, 22);
            this.ReportUserPassword.TabIndex = 18;
            // 
            // ReportUserPasswordLabel
            // 
            this.ReportUserPasswordLabel.AutoSize = true;
            this.ReportUserPasswordLabel.Location = new System.Drawing.Point(22, 265);
            this.ReportUserPasswordLabel.Name = "ReportUserPasswordLabel";
            this.ReportUserPasswordLabel.Size = new System.Drawing.Size(120, 13);
            this.ReportUserPasswordLabel.TabIndex = 17;
            this.ReportUserPasswordLabel.Text = "&Report User Password";
            // 
            // MixERPPassword
            // 
            this.MixERPPassword.Location = new System.Drawing.Point(213, 231);
            this.MixERPPassword.MaxLength = 48;
            this.MixERPPassword.Name = "MixERPPassword";
            this.MixERPPassword.PasswordChar = '*';
            this.MixERPPassword.Size = new System.Drawing.Size(205, 22);
            this.MixERPPassword.TabIndex = 16;
            // 
            // MixERPUserPasswordLabel
            // 
            this.MixERPUserPasswordLabel.AutoSize = true;
            this.MixERPUserPasswordLabel.Location = new System.Drawing.Point(22, 236);
            this.MixERPUserPasswordLabel.Name = "MixERPUserPasswordLabel";
            this.MixERPUserPasswordLabel.Size = new System.Drawing.Size(122, 13);
            this.MixERPUserPasswordLabel.TabIndex = 15;
            this.MixERPUserPasswordLabel.Text = "&MixERP User Password";
            // 
            // PostgresPassword
            // 
            this.PostgresPassword.Location = new System.Drawing.Point(213, 202);
            this.PostgresPassword.MaxLength = 48;
            this.PostgresPassword.Name = "PostgresPassword";
            this.PostgresPassword.PasswordChar = '*';
            this.PostgresPassword.Size = new System.Drawing.Size(205, 22);
            this.PostgresPassword.TabIndex = 14;
            // 
            // PostgresUserPasswordLabel
            // 
            this.PostgresUserPasswordLabel.AutoSize = true;
            this.PostgresUserPasswordLabel.Location = new System.Drawing.Point(22, 207);
            this.PostgresUserPasswordLabel.Name = "PostgresUserPasswordLabel";
            this.PostgresUserPasswordLabel.Size = new System.Drawing.Size(129, 13);
            this.PostgresUserPasswordLabel.TabIndex = 13;
            this.PostgresUserPasswordLabel.Text = "Postgres &User Password";
            // 
            // PortNumberTextBox
            // 
            this.PortNumberTextBox.Location = new System.Drawing.Point(213, 173);
            this.PortNumberTextBox.MaxLength = 5;
            this.PortNumberTextBox.Name = "PortNumberTextBox";
            this.PortNumberTextBox.Size = new System.Drawing.Size(75, 22);
            this.PortNumberTextBox.TabIndex = 11;
            this.PortNumberTextBox.Text = "8080";
            // 
            // PortNumberLabel
            // 
            this.PortNumberLabel.AutoSize = true;
            this.PortNumberLabel.Location = new System.Drawing.Point(22, 178);
            this.PortNumberLabel.Name = "PortNumberLabel";
            this.PortNumberLabel.Size = new System.Drawing.Size(72, 13);
            this.PortNumberLabel.TabIndex = 10;
            this.PortNumberLabel.Text = "&Port Number";
            // 
            // HostNameTextBox
            // 
            this.HostNameTextBox.Location = new System.Drawing.Point(213, 144);
            this.HostNameTextBox.MaxLength = 64;
            this.HostNameTextBox.Name = "HostNameTextBox";
            this.HostNameTextBox.Size = new System.Drawing.Size(75, 22);
            this.HostNameTextBox.TabIndex = 8;
            this.HostNameTextBox.Text = "localhost";
            // 
            // HostnameLabel
            // 
            this.HostnameLabel.AutoSize = true;
            this.HostnameLabel.Location = new System.Drawing.Point(22, 149);
            this.HostnameLabel.Name = "HostnameLabel";
            this.HostnameLabel.Size = new System.Drawing.Size(59, 13);
            this.HostnameLabel.TabIndex = 7;
            this.HostnameLabel.Text = "&Hostname";
            // 
            // ApplicationPoolNameTextBox
            // 
            this.ApplicationPoolNameTextBox.Location = new System.Drawing.Point(213, 56);
            this.ApplicationPoolNameTextBox.MaxLength = 50;
            this.ApplicationPoolNameTextBox.Name = "ApplicationPoolNameTextBox";
            this.ApplicationPoolNameTextBox.Size = new System.Drawing.Size(75, 22);
            this.ApplicationPoolNameTextBox.TabIndex = 3;
            this.ApplicationPoolNameTextBox.Text = "MixERP";
            // 
            // ApplicationPoolNameLabel
            // 
            this.ApplicationPoolNameLabel.AutoSize = true;
            this.ApplicationPoolNameLabel.Location = new System.Drawing.Point(22, 61);
            this.ApplicationPoolNameLabel.Name = "ApplicationPoolNameLabel";
            this.ApplicationPoolNameLabel.Size = new System.Drawing.Size(124, 13);
            this.ApplicationPoolNameLabel.TabIndex = 2;
            this.ApplicationPoolNameLabel.Text = "Application &Pool Name";
            // 
            // SiteNameTextBox
            // 
            this.SiteNameTextBox.Location = new System.Drawing.Point(213, 27);
            this.SiteNameTextBox.MaxLength = 50;
            this.SiteNameTextBox.Name = "SiteNameTextBox";
            this.SiteNameTextBox.Size = new System.Drawing.Size(75, 22);
            this.SiteNameTextBox.TabIndex = 1;
            this.SiteNameTextBox.Text = "MixERP";
            this.SiteNameTextBox.TextChanged += new System.EventHandler(this.SiteNameTextBox_TextChanged);
            // 
            // SiteNameLabel
            // 
            this.SiteNameLabel.AutoSize = true;
            this.SiteNameLabel.Location = new System.Drawing.Point(22, 32);
            this.SiteNameLabel.Name = "SiteNameLabel";
            this.SiteNameLabel.Size = new System.Drawing.Size(58, 13);
            this.SiteNameLabel.TabIndex = 0;
            this.SiteNameLabel.Text = "&Site Name";
            // 
            // BrowseInstallDirectoryButton
            // 
            this.BrowseInstallDirectoryButton.Location = new System.Drawing.Point(213, 114);
            this.BrowseInstallDirectoryButton.Name = "BrowseInstallDirectoryButton";
            this.BrowseInstallDirectoryButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseInstallDirectoryButton.TabIndex = 6;
            this.BrowseInstallDirectoryButton.Text = "&Browse";
            this.BrowseInstallDirectoryButton.UseVisualStyleBackColor = true;
            this.BrowseInstallDirectoryButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // InstallationDirectoryTextBox
            // 
            this.InstallationDirectoryTextBox.Location = new System.Drawing.Point(213, 85);
            this.InstallationDirectoryTextBox.MaxLength = 200;
            this.InstallationDirectoryTextBox.Name = "InstallationDirectoryTextBox";
            this.InstallationDirectoryTextBox.Size = new System.Drawing.Size(205, 22);
            this.InstallationDirectoryTextBox.TabIndex = 5;
            // 
            // InstallationDirectoryLabel
            // 
            this.InstallationDirectoryLabel.AutoSize = true;
            this.InstallationDirectoryLabel.Location = new System.Drawing.Point(22, 90);
            this.InstallationDirectoryLabel.Name = "InstallationDirectoryLabel";
            this.InstallationDirectoryLabel.Size = new System.Drawing.Size(114, 13);
            this.InstallationDirectoryLabel.TabIndex = 4;
            this.InstallationDirectoryLabel.Text = "&Installation Directory";
            // 
            // ActivityProgressBar
            // 
            this.ActivityProgressBar.Location = new System.Drawing.Point(13, 453);
            this.ActivityProgressBar.Name = "ActivityProgressBar";
            this.ActivityProgressBar.Size = new System.Drawing.Size(820, 10);
            this.ActivityProgressBar.TabIndex = 47;
            // 
            // StatusProgressBar
            // 
            this.StatusProgressBar.Location = new System.Drawing.Point(13, 469);
            this.StatusProgressBar.Name = "StatusProgressBar";
            this.StatusProgressBar.Size = new System.Drawing.Size(820, 10);
            this.StatusProgressBar.TabIndex = 48;
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.StatusLabel.Location = new System.Drawing.Point(12, 489);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(76, 13);
            this.StatusLabel.TabIndex = 49;
            this.StatusLabel.Text = "Installing foo";
            this.StatusLabel.Visible = false;
            // 
            // StatusProgressLabel
            // 
            this.StatusProgressLabel.AutoSize = true;
            this.StatusProgressLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusProgressLabel.ForeColor = System.Drawing.Color.Red;
            this.StatusProgressLabel.Location = new System.Drawing.Point(12, 510);
            this.StatusProgressLabel.Name = "StatusProgressLabel";
            this.StatusProgressLabel.Size = new System.Drawing.Size(84, 13);
            this.StatusProgressLabel.TabIndex = 50;
            this.StatusProgressLabel.Text = "n/n completed";
            this.StatusProgressLabel.Visible = false;
            // 
            // OfficeInfoGroupBox
            // 
            this.OfficeInfoGroupBox.Controls.Add(this.InstallSampleCheckBox);
            this.OfficeInfoGroupBox.Controls.Add(this.AdminPasswordTextBox);
            this.OfficeInfoGroupBox.Controls.Add(this.PasswordLabel);
            this.OfficeInfoGroupBox.Controls.Add(this.AdminUserNameTextBox);
            this.OfficeInfoGroupBox.Controls.Add(this.AdminUserNameLabel);
            this.OfficeInfoGroupBox.Controls.Add(this.RegistrationDatePicker);
            this.OfficeInfoGroupBox.Controls.Add(this.AdminNameTextBox);
            this.OfficeInfoGroupBox.Controls.Add(this.AdminNameLabel);
            this.OfficeInfoGroupBox.Controls.Add(this.HundredthNameTextBox);
            this.OfficeInfoGroupBox.Controls.Add(this.HundredthNameLabel);
            this.OfficeInfoGroupBox.Controls.Add(this.CurrencyNameTextBox);
            this.OfficeInfoGroupBox.Controls.Add(this.CurrencyNameLabel);
            this.OfficeInfoGroupBox.Controls.Add(this.CurrencySymbolTextBox);
            this.OfficeInfoGroupBox.Controls.Add(this.CurrencySymbolLabel);
            this.OfficeInfoGroupBox.Controls.Add(this.CurrencyCodeTextBox);
            this.OfficeInfoGroupBox.Controls.Add(this.CurrencyCodeLabel);
            this.OfficeInfoGroupBox.Controls.Add(this.RegistrationDateLabel);
            this.OfficeInfoGroupBox.Controls.Add(this.NickNameTextBox);
            this.OfficeInfoGroupBox.Controls.Add(this.NickNameLabel);
            this.OfficeInfoGroupBox.Controls.Add(this.OfficeNameTextBox);
            this.OfficeInfoGroupBox.Controls.Add(this.OfficeNameLabel);
            this.OfficeInfoGroupBox.Controls.Add(this.OfficeCodeTextBox);
            this.OfficeInfoGroupBox.Controls.Add(this.OfficeCodeLabel);
            this.OfficeInfoGroupBox.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OfficeInfoGroupBox.Location = new System.Drawing.Point(454, 73);
            this.OfficeInfoGroupBox.Name = "OfficeInfoGroupBox";
            this.OfficeInfoGroupBox.Size = new System.Drawing.Size(379, 367);
            this.OfficeInfoGroupBox.TabIndex = 24;
            this.OfficeInfoGroupBox.TabStop = false;
            this.OfficeInfoGroupBox.Text = "Office Information";
            // 
            // AdminPasswordTextBox
            // 
            this.AdminPasswordTextBox.Location = new System.Drawing.Point(160, 310);
            this.AdminPasswordTextBox.MaxLength = 48;
            this.AdminPasswordTextBox.Name = "AdminPasswordTextBox";
            this.AdminPasswordTextBox.PasswordChar = '*';
            this.AdminPasswordTextBox.Size = new System.Drawing.Size(105, 22);
            this.AdminPasswordTextBox.TabIndex = 46;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(24, 315);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(56, 13);
            this.PasswordLabel.TabIndex = 45;
            this.PasswordLabel.Text = "Password";
            // 
            // AdminUserNameTextBox
            // 
            this.AdminUserNameTextBox.Location = new System.Drawing.Point(160, 282);
            this.AdminUserNameTextBox.MaxLength = 50;
            this.AdminUserNameTextBox.Name = "AdminUserNameTextBox";
            this.AdminUserNameTextBox.Size = new System.Drawing.Size(117, 22);
            this.AdminUserNameTextBox.TabIndex = 44;
            // 
            // AdminUserNameLabel
            // 
            this.AdminUserNameLabel.AutoSize = true;
            this.AdminUserNameLabel.Location = new System.Drawing.Point(24, 287);
            this.AdminUserNameLabel.Name = "AdminUserNameLabel";
            this.AdminUserNameLabel.Size = new System.Drawing.Size(94, 13);
            this.AdminUserNameLabel.TabIndex = 43;
            this.AdminUserNameLabel.Text = "Admin Username";
            // 
            // RegistrationDatePicker
            // 
            this.RegistrationDatePicker.Location = new System.Drawing.Point(160, 114);
            this.RegistrationDatePicker.Name = "RegistrationDatePicker";
            this.RegistrationDatePicker.Size = new System.Drawing.Size(203, 22);
            this.RegistrationDatePicker.TabIndex = 32;
            // 
            // AdminNameTextBox
            // 
            this.AdminNameTextBox.Location = new System.Drawing.Point(160, 254);
            this.AdminNameTextBox.MaxLength = 100;
            this.AdminNameTextBox.Name = "AdminNameTextBox";
            this.AdminNameTextBox.Size = new System.Drawing.Size(203, 22);
            this.AdminNameTextBox.TabIndex = 42;
            // 
            // AdminNameLabel
            // 
            this.AdminNameLabel.AutoSize = true;
            this.AdminNameLabel.Location = new System.Drawing.Point(24, 259);
            this.AdminNameLabel.Name = "AdminNameLabel";
            this.AdminNameLabel.Size = new System.Drawing.Size(72, 13);
            this.AdminNameLabel.TabIndex = 41;
            this.AdminNameLabel.Text = "Admin Name";
            // 
            // HundredthNameTextBox
            // 
            this.HundredthNameTextBox.Location = new System.Drawing.Point(160, 226);
            this.HundredthNameTextBox.MaxLength = 48;
            this.HundredthNameTextBox.Name = "HundredthNameTextBox";
            this.HundredthNameTextBox.Size = new System.Drawing.Size(123, 22);
            this.HundredthNameTextBox.TabIndex = 40;
            // 
            // HundredthNameLabel
            // 
            this.HundredthNameLabel.AutoSize = true;
            this.HundredthNameLabel.Location = new System.Drawing.Point(24, 231);
            this.HundredthNameLabel.Name = "HundredthNameLabel";
            this.HundredthNameLabel.Size = new System.Drawing.Size(96, 13);
            this.HundredthNameLabel.TabIndex = 39;
            this.HundredthNameLabel.Text = "Hundredth Name";
            // 
            // CurrencyNameTextBox
            // 
            this.CurrencyNameTextBox.Location = new System.Drawing.Point(160, 198);
            this.CurrencyNameTextBox.MaxLength = 48;
            this.CurrencyNameTextBox.Name = "CurrencyNameTextBox";
            this.CurrencyNameTextBox.Size = new System.Drawing.Size(123, 22);
            this.CurrencyNameTextBox.TabIndex = 38;
            // 
            // CurrencyNameLabel
            // 
            this.CurrencyNameLabel.AutoSize = true;
            this.CurrencyNameLabel.Location = new System.Drawing.Point(24, 203);
            this.CurrencyNameLabel.Name = "CurrencyNameLabel";
            this.CurrencyNameLabel.Size = new System.Drawing.Size(84, 13);
            this.CurrencyNameLabel.TabIndex = 37;
            this.CurrencyNameLabel.Text = "Currency Name";
            // 
            // CurrencySymbolTextBox
            // 
            this.CurrencySymbolTextBox.Location = new System.Drawing.Point(160, 170);
            this.CurrencySymbolTextBox.MaxLength = 12;
            this.CurrencySymbolTextBox.Name = "CurrencySymbolTextBox";
            this.CurrencySymbolTextBox.Size = new System.Drawing.Size(90, 22);
            this.CurrencySymbolTextBox.TabIndex = 36;
            // 
            // CurrencySymbolLabel
            // 
            this.CurrencySymbolLabel.AutoSize = true;
            this.CurrencySymbolLabel.Location = new System.Drawing.Point(24, 175);
            this.CurrencySymbolLabel.Name = "CurrencySymbolLabel";
            this.CurrencySymbolLabel.Size = new System.Drawing.Size(92, 13);
            this.CurrencySymbolLabel.TabIndex = 35;
            this.CurrencySymbolLabel.Text = "Currency Symbol";
            // 
            // CurrencyCodeTextBox
            // 
            this.CurrencyCodeTextBox.Location = new System.Drawing.Point(160, 142);
            this.CurrencyCodeTextBox.MaxLength = 12;
            this.CurrencyCodeTextBox.Name = "CurrencyCodeTextBox";
            this.CurrencyCodeTextBox.Size = new System.Drawing.Size(90, 22);
            this.CurrencyCodeTextBox.TabIndex = 34;
            // 
            // CurrencyCodeLabel
            // 
            this.CurrencyCodeLabel.AutoSize = true;
            this.CurrencyCodeLabel.Location = new System.Drawing.Point(24, 147);
            this.CurrencyCodeLabel.Name = "CurrencyCodeLabel";
            this.CurrencyCodeLabel.Size = new System.Drawing.Size(82, 13);
            this.CurrencyCodeLabel.TabIndex = 33;
            this.CurrencyCodeLabel.Text = "Currency Code";
            // 
            // RegistrationDateLabel
            // 
            this.RegistrationDateLabel.AutoSize = true;
            this.RegistrationDateLabel.Location = new System.Drawing.Point(24, 119);
            this.RegistrationDateLabel.Name = "RegistrationDateLabel";
            this.RegistrationDateLabel.Size = new System.Drawing.Size(97, 13);
            this.RegistrationDateLabel.TabIndex = 31;
            this.RegistrationDateLabel.Text = "Registration Date";
            // 
            // NickNameTextBox
            // 
            this.NickNameTextBox.Location = new System.Drawing.Point(160, 86);
            this.NickNameTextBox.MaxLength = 50;
            this.NickNameTextBox.Name = "NickNameTextBox";
            this.NickNameTextBox.Size = new System.Drawing.Size(117, 22);
            this.NickNameTextBox.TabIndex = 30;
            // 
            // NickNameLabel
            // 
            this.NickNameLabel.AutoSize = true;
            this.NickNameLabel.Location = new System.Drawing.Point(24, 91);
            this.NickNameLabel.Name = "NickNameLabel";
            this.NickNameLabel.Size = new System.Drawing.Size(95, 13);
            this.NickNameLabel.TabIndex = 29;
            this.NickNameLabel.Text = "Office Nick Name";
            // 
            // OfficeNameTextBox
            // 
            this.OfficeNameTextBox.Location = new System.Drawing.Point(160, 58);
            this.OfficeNameTextBox.MaxLength = 150;
            this.OfficeNameTextBox.Name = "OfficeNameTextBox";
            this.OfficeNameTextBox.Size = new System.Drawing.Size(203, 22);
            this.OfficeNameTextBox.TabIndex = 28;
            // 
            // OfficeNameLabel
            // 
            this.OfficeNameLabel.AutoSize = true;
            this.OfficeNameLabel.Location = new System.Drawing.Point(24, 63);
            this.OfficeNameLabel.Name = "OfficeNameLabel";
            this.OfficeNameLabel.Size = new System.Drawing.Size(70, 13);
            this.OfficeNameLabel.TabIndex = 27;
            this.OfficeNameLabel.Text = "Office Name";
            // 
            // OfficeCodeTextBox
            // 
            this.OfficeCodeTextBox.Location = new System.Drawing.Point(160, 30);
            this.OfficeCodeTextBox.MaxLength = 12;
            this.OfficeCodeTextBox.Name = "OfficeCodeTextBox";
            this.OfficeCodeTextBox.Size = new System.Drawing.Size(59, 22);
            this.OfficeCodeTextBox.TabIndex = 26;
            // 
            // OfficeCodeLabel
            // 
            this.OfficeCodeLabel.AutoSize = true;
            this.OfficeCodeLabel.Location = new System.Drawing.Point(24, 35);
            this.OfficeCodeLabel.Name = "OfficeCodeLabel";
            this.OfficeCodeLabel.Size = new System.Drawing.Size(68, 13);
            this.OfficeCodeLabel.TabIndex = 25;
            this.OfficeCodeLabel.Text = "Office Code";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MixERP.Net.Utility.Installer.Properties.Resources.mixerp;
            this.pictureBox1.Location = new System.Drawing.Point(13, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 53;
            this.pictureBox1.TabStop = false;
            // 
            // InstallSampleCheckBox
            // 
            this.InstallSampleCheckBox.AutoSize = true;
            this.InstallSampleCheckBox.Location = new System.Drawing.Point(160, 338);
            this.InstallSampleCheckBox.Name = "InstallSampleCheckBox";
            this.InstallSampleCheckBox.Size = new System.Drawing.Size(97, 17);
            this.InstallSampleCheckBox.TabIndex = 47;
            this.InstallSampleCheckBox.Text = "Install Sample";
            this.InstallSampleCheckBox.UseVisualStyleBackColor = true;
            // 
            // InstallationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(849, 588);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.OfficeInfoGroupBox);
            this.Controls.Add(this.StatusProgressLabel);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.StatusProgressBar);
            this.Controls.Add(this.ActivityProgressBar);
            this.Controls.Add(this.GroupBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.InstallButton);
            this.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InstallationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MixERP Deployment";
            this.Load += new System.EventHandler(this.InstallationForm_Load);
            this.GroupBox.ResumeLayout(false);
            this.GroupBox.PerformLayout();
            this.OfficeInfoGroupBox.ResumeLayout(false);
            this.OfficeInfoGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button InstallButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
        private System.Windows.Forms.GroupBox GroupBox;
        private System.Windows.Forms.Button BrowseInstallDirectoryButton;
        private System.Windows.Forms.TextBox InstallationDirectoryTextBox;
        private System.Windows.Forms.Label InstallationDirectoryLabel;
        private System.Windows.Forms.TextBox ApplicationPoolNameTextBox;
        private System.Windows.Forms.Label ApplicationPoolNameLabel;
        private System.Windows.Forms.TextBox SiteNameTextBox;
        private System.Windows.Forms.Label SiteNameLabel;
        private System.Windows.Forms.TextBox HostNameTextBox;
        private System.Windows.Forms.Label HostnameLabel;
        private System.Windows.Forms.TextBox PortNumberTextBox;
        private System.Windows.Forms.Label PortNumberLabel;
        private System.Windows.Forms.TextBox PostgresPassword;
        private System.Windows.Forms.Label PostgresUserPasswordLabel;
        private System.Windows.Forms.TextBox ReportUserPassword;
        private System.Windows.Forms.Label ReportUserPasswordLabel;
        private System.Windows.Forms.TextBox MixERPPassword;
        private System.Windows.Forms.Label MixERPUserPasswordLabel;
        private System.Windows.Forms.TextBox DatabaseNameTextBox;
        private System.Windows.Forms.Label DatabaseNameLabel;
        private System.Windows.Forms.ProgressBar ActivityProgressBar;
        private System.Windows.Forms.ProgressBar StatusProgressBar;
        private System.Windows.Forms.Label PostgreSQLStatusLabel;
        private System.Windows.Forms.Button ValidateDatabaseButton;
        private System.Windows.Forms.Label WillBeCreatedLabel;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Label StatusProgressLabel;
        private System.Windows.Forms.Button ValidateSiteButton;
        private System.Windows.Forms.Label IISStatusLabel;
        private System.Windows.Forms.GroupBox OfficeInfoGroupBox;
        private System.Windows.Forms.TextBox CurrencyCodeTextBox;
        private System.Windows.Forms.Label CurrencyCodeLabel;
        private System.Windows.Forms.Label RegistrationDateLabel;
        private System.Windows.Forms.TextBox NickNameTextBox;
        private System.Windows.Forms.Label NickNameLabel;
        private System.Windows.Forms.TextBox OfficeNameTextBox;
        private System.Windows.Forms.Label OfficeNameLabel;
        private System.Windows.Forms.TextBox OfficeCodeTextBox;
        private System.Windows.Forms.Label OfficeCodeLabel;
        private System.Windows.Forms.DateTimePicker RegistrationDatePicker;
        private System.Windows.Forms.TextBox AdminNameTextBox;
        private System.Windows.Forms.Label AdminNameLabel;
        private System.Windows.Forms.TextBox HundredthNameTextBox;
        private System.Windows.Forms.Label HundredthNameLabel;
        private System.Windows.Forms.TextBox CurrencyNameTextBox;
        private System.Windows.Forms.Label CurrencyNameLabel;
        private System.Windows.Forms.TextBox CurrencySymbolTextBox;
        private System.Windows.Forms.Label CurrencySymbolLabel;
        private System.Windows.Forms.TextBox AdminPasswordTextBox;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox AdminUserNameTextBox;
        private System.Windows.Forms.Label AdminUserNameLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox InstallSampleCheckBox;

    }
}

