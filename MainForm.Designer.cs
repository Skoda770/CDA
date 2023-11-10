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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            label2 = new Label();
            OriginUrl = new TextBox();
            videoDownload = new RadioButton();
            catalogDownload = new RadioButton();
            downloadType = new GroupBox();
            destinationCatalog = new TextBox();
            label4 = new Label();
            FileName = new ColumnHeader();
            Progress = new ColumnHeader();
            Speed = new ColumnHeader();
            listView1 = new ListView();
            folderBrowserDialog1 = new FolderBrowserDialog();
            button2 = new Button();
            downloadType.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(23, 229);
            button1.Name = "button1";
            button1.Size = new Size(510, 27);
            button1.TabIndex = 1;
            button1.Text = "Pobierz";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 74);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 2;
            label2.Text = "Link";
            // 
            // OriginUrl
            // 
            OriginUrl.Location = new Point(6, 92);
            OriginUrl.Name = "OriginUrl";
            OriginUrl.Size = new Size(495, 23);
            OriginUrl.TabIndex = 0;
            OriginUrl.TextChanged += textBox1_TextChanged;
            // 
            // videoDownload
            // 
            videoDownload.AutoSize = true;
            videoDownload.Location = new Point(6, 22);
            videoDownload.Name = "videoDownload";
            videoDownload.Size = new Size(59, 19);
            videoDownload.TabIndex = 0;
            videoDownload.TabStop = true;
            videoDownload.Text = "Wideo";
            videoDownload.UseVisualStyleBackColor = true;
            // 
            // catalogDownload
            // 
            catalogDownload.AutoSize = true;
            catalogDownload.Location = new Point(6, 47);
            catalogDownload.Name = "catalogDownload";
            catalogDownload.Size = new Size(65, 19);
            catalogDownload.TabIndex = 1;
            catalogDownload.TabStop = true;
            catalogDownload.Text = "Katalog";
            catalogDownload.UseVisualStyleBackColor = true;
            // 
            // downloadType
            // 
            downloadType.Controls.Add(catalogDownload);
            downloadType.Controls.Add(videoDownload);
            downloadType.Controls.Add(OriginUrl);
            downloadType.Controls.Add(label2);
            downloadType.Location = new Point(24, 78);
            downloadType.Name = "downloadType";
            downloadType.Size = new Size(509, 145);
            downloadType.TabIndex = 8;
            downloadType.TabStop = false;
            downloadType.Text = "Typ pobierania";
            // 
            // destinationCatalog
            // 
            destinationCatalog.Location = new Point(23, 36);
            destinationCatalog.Name = "destinationCatalog";
            destinationCatalog.Size = new Size(429, 23);
            destinationCatalog.TabIndex = 5;
            destinationCatalog.TextChanged += destinationCatalog_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(24, 18);
            label4.Name = "label4";
            label4.Size = new Size(101, 15);
            label4.TabIndex = 6;
            label4.Text = "Katalog docelowy";
            // 
            // FileName
            // 
            FileName.Text = "Nazwa";
            FileName.Width = 300;
            // 
            // Progress
            // 
            Progress.Text = "Progres";
            Progress.Width = 100;
            // 
            // Speed
            // 
            Speed.Text = "Pobieranie";
            Speed.Width = 100;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { FileName, Progress, Speed });
            listView1.Location = new Point(23, 262);
            listView1.Name = "listView1";
            listView1.Size = new Size(510, 374);
            listView1.TabIndex = 9;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // button2
            // 
            button2.Location = new Point(458, 36);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 10;
            button2.Text = "Wybierz";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(561, 648);
            Controls.Add(button2);
            Controls.Add(listView1);
            Controls.Add(downloadType);
            Controls.Add(label4);
            Controls.Add(destinationCatalog);
            Controls.Add(button1);
            MaximumSize = new Size(831, 687);
            MinimumSize = new Size(577, 687);
            Name = "MainForm";
            ShowIcon = false;
            Text = "Zajebisty program szwagra";
            Load += Form1_Load;
            downloadType.ResumeLayout(false);
            downloadType.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label2;
        private TextBox OriginUrl;
        private RadioButton videoDownload;
        private RadioButton catalogDownload;
        private GroupBox downloadType;
        private TextBox destinationCatalog;
        private Label label4;
        private ColumnHeader FileName;
        private ColumnHeader Progress;
        private ColumnHeader Speed;
        private ListView listView1;
        public FolderBrowserDialog folderBrowserDialog1;
        private Button button2;
    }
}