namespace CDA
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
      this.button1 = new Button();
      this.label2 = new Label();
      this.OriginUrl = new TextBox();
      this.videoDownload = new RadioButton();
      this.catalogCDADownload = new RadioButton();
      this.downloadType = new GroupBox();
      this.catalogArchiveOrgDownload = new RadioButton();
      this.destinationCatalog = new TextBox();
      this.label4 = new Label();
      this.FileName = new ColumnHeader();
      this.Progress = new ColumnHeader();
      this.Speed = new ColumnHeader();
      this.listView1 = new ListView();
      this.folderBrowserDialog1 = new FolderBrowserDialog();
      this.SelectDirectory = new Button();
      this.NavigateToLogs = new Button();
      this.downloadType.SuspendLayout();
      this.SuspendLayout();
      this.button1.Location = new Point(23, 229);
      this.button1.Name = "button1";
      this.button1.Size = new Size(802, 27);
      this.button1.TabIndex = 1;
      this.button1.Text = "Pobierz";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(6, 74);
      this.label2.Name = "label2";
      this.label2.Size = new Size(29, 15);
      this.label2.TabIndex = 2;
      this.label2.Text = "Link";
      this.OriginUrl.Location = new Point(6, 92);
      this.OriginUrl.Name = "OriginUrl";
      this.OriginUrl.Size = new Size(703, 23);
      this.OriginUrl.TabIndex = 0;
      this.videoDownload.AutoSize = true;
      this.videoDownload.Location = new Point(6, 22);
      this.videoDownload.Name = "videoDownload";
      this.videoDownload.Size = new Size(59, 19);
      this.videoDownload.TabIndex = 0;
      this.videoDownload.TabStop = true;
      this.videoDownload.Text = "Wideo";
      this.videoDownload.UseVisualStyleBackColor = true;
      this.catalogCDADownload.AutoSize = true;
      this.catalogCDADownload.Location = new Point(6, 47);
      this.catalogCDADownload.Name = "catalogCDADownload";
      this.catalogCDADownload.Size = new Size(100, 19);
      this.catalogCDADownload.TabIndex = 1;
      this.catalogCDADownload.TabStop = true;
      this.catalogCDADownload.Text = "Katalog (CDA)";
      this.catalogCDADownload.UseVisualStyleBackColor = true;
      this.downloadType.Controls.Add((Control) this.catalogArchiveOrgDownload);
      this.downloadType.Controls.Add((Control) this.catalogCDADownload);
      this.downloadType.Controls.Add((Control) this.videoDownload);
      this.downloadType.Controls.Add((Control) this.OriginUrl);
      this.downloadType.Controls.Add((Control) this.label2);
      this.downloadType.Location = new Point(23, 78);
      this.downloadType.Name = "downloadType";
      this.downloadType.Size = new Size(715, 145);
      this.downloadType.TabIndex = 8;
      this.downloadType.TabStop = false;
      this.downloadType.Text = "Typ pobierania";
      this.catalogArchiveOrgDownload.AutoSize = true;
      this.catalogArchiveOrgDownload.Location = new Point(112, 47);
      this.catalogArchiveOrgDownload.Name = "catalogArchiveOrgDownload";
      this.catalogArchiveOrgDownload.Size = new Size(137, 19);
      this.catalogArchiveOrgDownload.TabIndex = 3;
      this.catalogArchiveOrgDownload.TabStop = true;
      this.catalogArchiveOrgDownload.Text = "Katalog (Archive.org)";
      this.catalogArchiveOrgDownload.UseVisualStyleBackColor = true;
      this.destinationCatalog.Location = new Point(23, 36);
      this.destinationCatalog.Name = "destinationCatalog";
      this.destinationCatalog.Size = new Size(715, 23);
      this.destinationCatalog.TabIndex = 5;
      this.label4.AutoSize = true;
      this.label4.Location = new Point(24, 18);
      this.label4.Name = "label4";
      this.label4.Size = new Size(101, 15);
      this.label4.TabIndex = 6;
      this.label4.Text = "Katalog docelowy";
      this.FileName.Text = "Nazwa";
      this.FileName.Width = 600;
      this.Progress.Text = "Progres";
      this.Progress.Width = 100;
      this.Speed.Text = "Pobieranie";
      this.Speed.Width = 100;
      this.listView1.Columns.AddRange(new ColumnHeader[3]
      {
        this.FileName,
        this.Progress,
        this.Speed
      });
      this.listView1.Location = new Point(23, 262);
      this.listView1.Name = "listView1";
      this.listView1.Size = new Size(804, 374);
      this.listView1.TabIndex = 9;
      this.listView1.UseCompatibleStateImageBehavior = false;
      this.listView1.View = View.Details;
      this.SelectDirectory.Location = new Point(744, 36);
      this.SelectDirectory.Name = "SelectDirectory";
      this.SelectDirectory.Size = new Size(75, 23);
      this.SelectDirectory.TabIndex = 10;
      this.SelectDirectory.Text = "Wybierz";
      this.SelectDirectory.UseVisualStyleBackColor = true;
      this.SelectDirectory.Click += new EventHandler(this.button2_Click);
      this.NavigateToLogs.Location = new Point(744, 96);
      this.NavigateToLogs.Name = "NavigateToLogs";
      this.NavigateToLogs.Size = new Size(75, 23);
      this.NavigateToLogs.TabIndex = 11;
      this.NavigateToLogs.Text = "Logi";
      this.NavigateToLogs.UseVisualStyleBackColor = true;
      this.NavigateToLogs.Click += new EventHandler(this.NavigateToLogs_Click);
      this.AutoScaleDimensions = new SizeF(7f, 15f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(837, 648);
      this.Controls.Add((Control) this.NavigateToLogs);
      this.Controls.Add((Control) this.SelectDirectory);
      this.Controls.Add((Control) this.listView1);
      this.Controls.Add((Control) this.downloadType);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.destinationCatalog);
      this.Controls.Add((Control) this.button1);
      this.MaximumSize = new Size(853, 687);
      this.MinimumSize = new Size(853, 687);
      this.Name = nameof (MainForm);
      this.ShowIcon = false;
      this.Text = "Zajebisty program szwagra";
      this.downloadType.ResumeLayout(false);
      this.downloadType.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
        }

        private System.Windows.Forms.RadioButton catalogArchiveOrgDownload;

        private System.Windows.Forms.Button NavigateToLogs;

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox OriginUrl;
        private System.Windows.Forms.RadioButton videoDownload;
        private System.Windows.Forms.RadioButton catalogCDADownload;
        private System.Windows.Forms.GroupBox downloadType;
        private System.Windows.Forms.TextBox destinationCatalog;
        private Label label4;
        private ColumnHeader FileName;
        private ColumnHeader Progress;
        private ColumnHeader Speed;
        private System.Windows.Forms.ListView listView1;
        public FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button SelectDirectory;
    }
}