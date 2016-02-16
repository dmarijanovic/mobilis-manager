namespace Mobilis
{
    partial class Informacije
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Informacije));
            this.dtpPocetak = new System.Windows.Forms.DateTimePicker();
            this.btnPrikazi = new System.Windows.Forms.Button();
            this.btnPonisti = new System.Windows.Forms.Button();
            this.dtpKraj = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbZaposlenici = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbPoslovnice = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbBrziOdabir = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpPocetak
            // 
            this.dtpPocetak.Location = new System.Drawing.Point(207, 65);
            this.dtpPocetak.Name = "dtpPocetak";
            this.dtpPocetak.Size = new System.Drawing.Size(142, 20);
            this.dtpPocetak.TabIndex = 0;
            // 
            // btnPrikazi
            // 
            this.btnPrikazi.Location = new System.Drawing.Point(239, 217);
            this.btnPrikazi.Name = "btnPrikazi";
            this.btnPrikazi.Size = new System.Drawing.Size(75, 23);
            this.btnPrikazi.TabIndex = 2;
            this.btnPrikazi.Text = "&Prikaži";
            this.btnPrikazi.UseVisualStyleBackColor = true;
            this.btnPrikazi.Click += new System.EventHandler(this.brnPrikazi_Click);
            // 
            // btnPonisti
            // 
            this.btnPonisti.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPonisti.Location = new System.Drawing.Point(320, 217);
            this.btnPonisti.Name = "btnPonisti";
            this.btnPonisti.Size = new System.Drawing.Size(75, 23);
            this.btnPonisti.TabIndex = 3;
            this.btnPonisti.Text = "P&oništi";
            this.btnPonisti.UseVisualStyleBackColor = true;
            this.btnPonisti.Click += new System.EventHandler(this.btnPonisti_Click);
            // 
            // dtpKraj
            // 
            this.dtpKraj.Location = new System.Drawing.Point(207, 102);
            this.dtpKraj.Name = "dtpKraj";
            this.dtpKraj.Size = new System.Drawing.Size(142, 20);
            this.dtpKraj.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbZaposlenici);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbPoslovnice);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbBrziOdabir);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.dtpKraj);
            this.groupBox1.Controls.Add(this.dtpPocetak);
            this.groupBox1.Location = new System.Drawing.Point(7, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(388, 201);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Odaberite raspon datuma i zaposlenika";
            // 
            // cmbZaposlenici
            // 
            this.cmbZaposlenici.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZaposlenici.FormattingEnabled = true;
            this.cmbZaposlenici.Location = new System.Drawing.Point(207, 167);
            this.cmbZaposlenici.Name = "cmbZaposlenici";
            this.cmbZaposlenici.Size = new System.Drawing.Size(142, 21);
            this.cmbZaposlenici.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(123, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "&Zaposlenik";
            // 
            // cmbPoslovnice
            // 
            this.cmbPoslovnice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPoslovnice.FormattingEnabled = true;
            this.cmbPoslovnice.Location = new System.Drawing.Point(207, 133);
            this.cmbPoslovnice.Name = "cmbPoslovnice";
            this.cmbPoslovnice.Size = new System.Drawing.Size(142, 21);
            this.cmbPoslovnice.TabIndex = 10;
            this.cmbPoslovnice.SelectedIndexChanged += new System.EventHandler(this.cmbPoslovnice_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(123, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "&Poslovnice";
            // 
            // cmbBrziOdabir
            // 
            this.cmbBrziOdabir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBrziOdabir.FormattingEnabled = true;
            this.cmbBrziOdabir.Items.AddRange(new object[] {
            "Danas",
            "Trenutni mjesec",
            "Prošli mjesec",
            "Dva mjeseca unatrag",
            "Tri mjeseca unatrag",
            "Ova godina",
            "Prošla godina"});
            this.cmbBrziOdabir.Location = new System.Drawing.Point(207, 27);
            this.cmbBrziOdabir.Name = "cmbBrziOdabir";
            this.cmbBrziOdabir.Size = new System.Drawing.Size(142, 21);
            this.cmbBrziOdabir.TabIndex = 8;
            this.cmbBrziOdabir.SelectedIndexChanged += new System.EventHandler(this.cmbBrziOdabir_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(123, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "&Brzi odabir:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "&Kraj:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(123, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Poèe&tak:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(31, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(59, 50);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Informacije
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 247);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPonisti);
            this.Controls.Add(this.btnPrikazi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Informacije";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mobilis - Informacije";
            this.Load += new System.EventHandler(this.Informacije_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpPocetak;
        private System.Windows.Forms.Button btnPrikazi;
        private System.Windows.Forms.Button btnPonisti;
        private System.Windows.Forms.DateTimePicker dtpKraj;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBrziOdabir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbPoslovnice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbZaposlenici;
    }
}