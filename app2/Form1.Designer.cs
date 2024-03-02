namespace app2
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            buttonChooseFile = new Button();
            openFileDialog1 = new OpenFileDialog();
            menuStrip1 = new MenuStrip();
            аToolStripMenuItemSpravka = new ToolStripMenuItem();
            оПрограммеToolStripMenuItem = new ToolStripMenuItem();
            buttonPodchet = new Button();
            radioButtonVrach = new RadioButton();
            radioButtonProd = new RadioButton();
            radioButtonOhrana = new RadioButton();
            textBoxDokpat = new TextBox();
            textBoxAll = new TextBox();
            panel1 = new Panel();
            label2 = new Label();
            panel2 = new Panel();
            label1 = new Label();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabelFilePath = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonChooseFile
            // 
            buttonChooseFile.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonChooseFile.BackColor = Color.SaddleBrown;
            buttonChooseFile.ForeColor = Color.White;
            buttonChooseFile.Location = new Point(12, 271);
            buttonChooseFile.Name = "buttonChooseFile";
            buttonChooseFile.Size = new Size(155, 37);
            buttonChooseFile.TabIndex = 0;
            buttonChooseFile.Text = "Выбрать файл";
            buttonChooseFile.UseVisualStyleBackColor = false;
            buttonChooseFile.Click += buttonChooseFile_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.SaddleBrown;
            menuStrip1.Items.AddRange(new ToolStripItem[] { аToolStripMenuItemSpravka, оПрограммеToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(584, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // аToolStripMenuItemSpravka
            // 
            аToolStripMenuItemSpravka.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            аToolStripMenuItemSpravka.ForeColor = Color.White;
            аToolStripMenuItemSpravka.Name = "аToolStripMenuItemSpravka";
            аToolStripMenuItemSpravka.Size = new Size(66, 20);
            аToolStripMenuItemSpravka.Text = "Справка";
            аToolStripMenuItemSpravka.Click += аToolStripMenuItemSpravka_Click;
            // 
            // оПрограммеToolStripMenuItem
            // 
            оПрограммеToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            оПрограммеToolStripMenuItem.ForeColor = Color.White;
            оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            оПрограммеToolStripMenuItem.Size = new Size(95, 20);
            оПрограммеToolStripMenuItem.Text = "О программе";
            оПрограммеToolStripMenuItem.Click += оПрограммеToolStripMenuItem_Click;
            // 
            // buttonPodchet
            // 
            buttonPodchet.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonPodchet.BackColor = Color.SaddleBrown;
            buttonPodchet.ForeColor = Color.White;
            buttonPodchet.Location = new Point(12, 314);
            buttonPodchet.Name = "buttonPodchet";
            buttonPodchet.Size = new Size(155, 37);
            buttonPodchet.TabIndex = 1;
            buttonPodchet.Text = "Начать подсчет";
            buttonPodchet.UseVisualStyleBackColor = false;
            buttonPodchet.Click += buttonPodchet_Click;
            // 
            // radioButtonVrach
            // 
            radioButtonVrach.AutoSize = true;
            radioButtonVrach.Location = new Point(9, 87);
            radioButtonVrach.Name = "radioButtonVrach";
            radioButtonVrach.Size = new Size(123, 21);
            radioButtonVrach.TabIndex = 2;
            radioButtonVrach.Text = "Врачевательная";
            radioButtonVrach.UseVisualStyleBackColor = true;
            // 
            // radioButtonProd
            // 
            radioButtonProd.AutoSize = true;
            radioButtonProd.Location = new Point(9, 60);
            radioButtonProd.Name = "radioButtonProd";
            radioButtonProd.Size = new Size(146, 21);
            radioButtonProd.TabIndex = 1;
            radioButtonProd.Text = "Продовольственная";
            radioButtonProd.UseVisualStyleBackColor = true;
            // 
            // radioButtonOhrana
            // 
            radioButtonOhrana.AutoSize = true;
            radioButtonOhrana.Checked = true;
            radioButtonOhrana.Location = new Point(9, 33);
            radioButtonOhrana.Name = "radioButtonOhrana";
            radioButtonOhrana.Size = new Size(86, 21);
            radioButtonOhrana.TabIndex = 0;
            radioButtonOhrana.TabStop = true;
            radioButtonOhrana.Text = "Охранная";
            radioButtonOhrana.UseVisualStyleBackColor = true;
            // 
            // textBoxDokpat
            // 
            textBoxDokpat.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxDokpat.BackColor = Color.FloralWhite;
            textBoxDokpat.Location = new Point(203, 45);
            textBoxDokpat.Multiline = true;
            textBoxDokpat.Name = "textBoxDokpat";
            textBoxDokpat.ReadOnly = true;
            textBoxDokpat.ScrollBars = ScrollBars.Vertical;
            textBoxDokpat.Size = new Size(183, 253);
            textBoxDokpat.TabIndex = 1;
            textBoxDokpat.Text = "докторские патрули";
            // 
            // textBoxAll
            // 
            textBoxAll.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxAll.BackColor = Color.FloralWhite;
            textBoxAll.Location = new Point(8, 45);
            textBoxAll.Multiline = true;
            textBoxAll.Name = "textBoxAll";
            textBoxAll.ReadOnly = true;
            textBoxAll.ScrollBars = ScrollBars.Vertical;
            textBoxAll.Size = new Size(183, 253);
            textBoxAll.TabIndex = 0;
            textBoxAll.Text = "общие отчеты";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.BackColor = Color.AntiqueWhite;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label2);
            panel1.Controls.Add(textBoxDokpat);
            panel1.Controls.Add(textBoxAll);
            panel1.Location = new Point(176, 38);
            panel1.Name = "panel1";
            panel1.Size = new Size(396, 313);
            panel1.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.SaddleBrown;
            label2.Location = new Point(151, 13);
            label2.Name = "label2";
            label2.Size = new Size(105, 16);
            label2.TabIndex = 4;
            label2.Text = "ВЫВОД ДАННЫХ";
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel2.BackColor = Color.AntiqueWhite;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(radioButtonVrach);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(radioButtonOhrana);
            panel2.Controls.Add(radioButtonProd);
            panel2.Location = new Point(12, 38);
            panel2.Name = "panel2";
            panel2.Size = new Size(158, 130);
            panel2.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.SaddleBrown;
            label1.Location = new Point(32, 13);
            label1.Name = "label1";
            label1.Size = new Size(91, 16);
            label1.TabIndex = 3;
            label1.Text = "ВЫБОР СФЕРЫ";
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.SaddleBrown;
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabelFilePath });
            statusStrip1.Location = new Point(0, 359);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(584, 22);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            toolStripStatusLabel1.ForeColor = Color.White;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(37, 17);
            toolStripStatusLabel1.Text = "Путь:";
            // 
            // toolStripStatusLabelFilePath
            // 
            toolStripStatusLabelFilePath.ForeColor = Color.White;
            toolStripStatusLabelFilePath.Name = "toolStripStatusLabelFilePath";
            toolStripStatusLabelFilePath.Size = new Size(59, 17);
            toolStripStatusLabelFilePath.Text = "не указан";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(584, 381);
            Controls.Add(statusStrip1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(buttonPodchet);
            Controls.Add(buttonChooseFile);
            Controls.Add(menuStrip1);
            Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            MaximumSize = new Size(600, 420);
            MinimumSize = new Size(600, 420);
            Name = "Form1";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Помощник главе сфер | ШРК";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonChooseFile;
        private OpenFileDialog openFileDialog1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem аToolStripMenuItemSpravka;
        private Button buttonPodchet;
        private RadioButton radioButtonVrach;
        private RadioButton radioButtonProd;
        private RadioButton radioButtonOhrana;
        private TextBox textBoxAll;
        private TextBox textBoxDokpat;
        private Panel panel1;
        private Label label2;
        private Panel panel2;
        private Label label1;
        private ToolStripMenuItem оПрограммеToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabelFilePath;
    }
}