namespace Mobilis
{
    partial class Pretplate
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pretplate));
            this.grpPrikaz = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.stripPrikaz = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.stripNoviZahtjev = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.stripZahtjevDodan = new System.Windows.Forms.ToolStripLabel();
            this.stripMeduspremnik = new System.Windows.Forms.ToolStripButton();
            this.stripMeduspremnikDD = new System.Windows.Forms.ToolStripDropDown();
            this.lblPretplatnik = new System.Windows.Forms.Label();
            this.grpZahtjev = new System.Windows.Forms.GroupBox();
            this.cmbUredaji = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMobitel = new Mobilis.TextBoxNumbers();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmbTrajanje = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rdbProduljenje = new System.Windows.Forms.RadioButton();
            this.rdbSimpaPrijelaz = new System.Windows.Forms.RadioButton();
            this.rdbNovaPretplata = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPonisti = new System.Windows.Forms.Button();
            this.btnAzuriraj = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpPrikaz.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.grpZahtjev.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpPrikaz
            // 
            this.grpPrikaz.Controls.Add(this.dataGridView1);
            this.grpPrikaz.Location = new System.Drawing.Point(9, 72);
            this.grpPrikaz.Name = "grpPrikaz";
            this.grpPrikaz.Size = new System.Drawing.Size(717, 280);
            this.grpPrikaz.TabIndex = 0;
            this.grpPrikaz.TabStop = false;
            this.grpPrikaz.Text = "Prikaz svih pretplata";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(711, 261);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripPrikaz,
            this.toolStripSeparator1,
            this.stripNoviZahtjev,
            this.toolStripSeparator2,
            this.stripZahtjevDodan,
            this.stripMeduspremnik});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(734, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // stripPrikaz
            // 
            this.stripPrikaz.Image = ((System.Drawing.Image)(resources.GetObject("stripPrikaz.Image")));
            this.stripPrikaz.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stripPrikaz.Name = "stripPrikaz";
            this.stripPrikaz.Size = new System.Drawing.Size(124, 22);
            this.stripPrikaz.Text = "&Pokaži sve pretplate";
            this.stripPrikaz.Click += new System.EventHandler(this.stripPrikaz_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // stripNoviZahtjev
            // 
            this.stripNoviZahtjev.Image = ((System.Drawing.Image)(resources.GetObject("stripNoviZahtjev.Image")));
            this.stripNoviZahtjev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stripNoviZahtjev.Name = "stripNoviZahtjev";
            this.stripNoviZahtjev.Size = new System.Drawing.Size(87, 22);
            this.stripNoviZahtjev.Text = "&Novi zahtjev";
            this.stripNoviZahtjev.Click += new System.EventHandler(this.stripNoviZahtjev_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // stripZahtjevDodan
            // 
            this.stripZahtjevDodan.ForeColor = System.Drawing.SystemColors.GrayText;
            this.stripZahtjevDodan.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.stripZahtjevDodan.Name = "stripZahtjevDodan";
            this.stripZahtjevDodan.Size = new System.Drawing.Size(0, 22);
            // 
            // stripMeduspremnik
            // 
            this.stripMeduspremnik.Image = ((System.Drawing.Image)(resources.GetObject("stripMeduspremnik.Image")));
            this.stripMeduspremnik.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stripMeduspremnik.Name = "stripMeduspremnik";
            this.stripMeduspremnik.Size = new System.Drawing.Size(95, 22);
            this.stripMeduspremnik.Text = "M&eðuspremnik";
            this.stripMeduspremnik.Click += new System.EventHandler(this.stripMeduspremnik_Click);
            // 
            // stripMeduspremnikDD
            // 
            this.stripMeduspremnikDD.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.stripMeduspremnikDD.Name = "stripIdiNaStranicu";
            this.stripMeduspremnikDD.Size = new System.Drawing.Size(2, 4);
            // 
            // lblPretplatnik
            // 
            this.lblPretplatnik.AutoSize = true;
            this.lblPretplatnik.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPretplatnik.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblPretplatnik.Location = new System.Drawing.Point(12, 38);
            this.lblPretplatnik.Name = "lblPretplatnik";
            this.lblPretplatnik.Size = new System.Drawing.Size(167, 20);
            this.lblPretplatnik.TabIndex = 1;
            this.lblPretplatnik.Text = "Ime i prezime (mbg)";
            // 
            // grpZahtjev
            // 
            this.grpZahtjev.Controls.Add(this.cmbUredaji);
            this.grpZahtjev.Controls.Add(this.label6);
            this.grpZahtjev.Controls.Add(this.txtMobitel);
            this.grpZahtjev.Controls.Add(this.label5);
            this.grpZahtjev.Controls.Add(this.dateTimePicker1);
            this.grpZahtjev.Controls.Add(this.label1);
            this.grpZahtjev.Controls.Add(this.pictureBox1);
            this.grpZahtjev.Controls.Add(this.cmbTrajanje);
            this.grpZahtjev.Controls.Add(this.label4);
            this.grpZahtjev.Controls.Add(this.label3);
            this.grpZahtjev.Controls.Add(this.rdbProduljenje);
            this.grpZahtjev.Controls.Add(this.rdbSimpaPrijelaz);
            this.grpZahtjev.Controls.Add(this.rdbNovaPretplata);
            this.grpZahtjev.Controls.Add(this.label2);
            this.grpZahtjev.Location = new System.Drawing.Point(9, 72);
            this.grpZahtjev.Name = "grpZahtjev";
            this.grpZahtjev.Size = new System.Drawing.Size(717, 280);
            this.grpZahtjev.TabIndex = 2;
            this.grpZahtjev.TabStop = false;
            this.grpZahtjev.Text = "Novi zahtjev";
            this.grpZahtjev.Visible = false;
            // 
            // cmbUredaji
            // 
            this.cmbUredaji.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbUredaji.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbUredaji.FormattingEnabled = true;
            this.cmbUredaji.Location = new System.Drawing.Point(315, 246);
            this.cmbUredaji.Name = "cmbUredaji";
            this.cmbUredaji.Size = new System.Drawing.Size(210, 21);
            this.cmbUredaji.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(127, 254);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "&Mobilni ureðaj:";
            // 
            // txtMobitel
            // 
            this.txtMobitel.Location = new System.Drawing.Point(315, 43);
            this.txtMobitel.MaxLength = 11;
            this.txtMobitel.Name = "txtMobitel";
            this.txtMobitel.Size = new System.Drawing.Size(210, 20);
            this.txtMobitel.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(285, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 18);
            this.label5.TabIndex = 12;
            this.label5.Text = "09";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(315, 214);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(142, 20);
            this.dateTimePicker1.TabIndex = 11;
            this.dateTimePicker1.Value = new System.DateTime(2008, 4, 20, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "&Datum:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(33, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(57, 50);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // cmbTrajanje
            // 
            this.cmbTrajanje.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrajanje.FormattingEnabled = true;
            this.cmbTrajanje.Items.AddRange(new object[] {
            "12",
            "24"});
            this.cmbTrajanje.Location = new System.Drawing.Point(315, 172);
            this.cmbTrajanje.Name = "cmbTrajanje";
            this.cmbTrajanje.Size = new System.Drawing.Size(142, 21);
            this.cmbTrajanje.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(127, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "&Trajanje pretplatnièkog odnosa:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(127, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "&Zahtjev:";
            // 
            // rdbProduljenje
            // 
            this.rdbProduljenje.AutoSize = true;
            this.rdbProduljenje.Location = new System.Drawing.Point(315, 137);
            this.rdbProduljenje.Name = "rdbProduljenje";
            this.rdbProduljenje.Size = new System.Drawing.Size(330, 17);
            this.rdbProduljenje.TabIndex = 4;
            this.rdbProduljenje.Text = "Zahtjev za produljenje obaveznog trajanja pretplatnièkog odnosa";
            this.rdbProduljenje.UseVisualStyleBackColor = true;
            // 
            // rdbSimpaPrijelaz
            // 
            this.rdbSimpaPrijelaz.AutoSize = true;
            this.rdbSimpaPrijelaz.Location = new System.Drawing.Point(315, 114);
            this.rdbSimpaPrijelaz.Name = "rdbSimpaPrijelaz";
            this.rdbSimpaPrijelaz.Size = new System.Drawing.Size(142, 17);
            this.rdbSimpaPrijelaz.TabIndex = 3;
            this.rdbSimpaPrijelaz.TabStop = true;
            this.rdbSimpaPrijelaz.Text = "Zahtjev za Simpa prijelaz";
            this.rdbSimpaPrijelaz.UseVisualStyleBackColor = true;
            // 
            // rdbNovaPretplata
            // 
            this.rdbNovaPretplata.AutoSize = true;
            this.rdbNovaPretplata.Checked = true;
            this.rdbNovaPretplata.Location = new System.Drawing.Point(315, 91);
            this.rdbNovaPretplata.Name = "rdbNovaPretplata";
            this.rdbNovaPretplata.Size = new System.Drawing.Size(236, 17);
            this.rdbNovaPretplata.TabIndex = 2;
            this.rdbNovaPretplata.TabStop = true;
            this.rdbNovaPretplata.Text = "Zahtjev za zasnivanje pretplatnièkog odnosa";
            this.rdbNovaPretplata.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "&Broj mobitela:";
            // 
            // btnPonisti
            // 
            this.btnPonisti.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPonisti.Location = new System.Drawing.Point(651, 358);
            this.btnPonisti.Name = "btnPonisti";
            this.btnPonisti.Size = new System.Drawing.Size(75, 23);
            this.btnPonisti.TabIndex = 3;
            this.btnPonisti.Text = "P&oništi";
            this.btnPonisti.UseVisualStyleBackColor = true;
            this.btnPonisti.Click += new System.EventHandler(this.btnPonisti_Click);
            // 
            // btnAzuriraj
            // 
            this.btnAzuriraj.Location = new System.Drawing.Point(570, 358);
            this.btnAzuriraj.Name = "btnAzuriraj";
            this.btnAzuriraj.Size = new System.Drawing.Size(75, 23);
            this.btnAzuriraj.TabIndex = 4;
            this.btnAzuriraj.Text = "&Ažuriraj";
            this.btnAzuriraj.UseVisualStyleBackColor = true;
            this.btnAzuriraj.Visible = false;
            this.btnAzuriraj.Click += new System.EventHandler(this.btnAzuriraj_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Pretplate
            // 
            this.AcceptButton = this.btnAzuriraj;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnPonisti;
            this.ClientSize = new System.Drawing.Size(734, 387);
            this.Controls.Add(this.grpZahtjev);
            this.Controls.Add(this.btnAzuriraj);
            this.Controls.Add(this.lblPretplatnik);
            this.Controls.Add(this.btnPonisti);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.grpPrikaz);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Pretplate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mobilis - Pretplate";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Pretplate_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Pretplate_KeyDown);
            this.Load += new System.EventHandler(this.Pretplate_Load);
            this.grpPrikaz.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grpZahtjev.ResumeLayout(false);
            this.grpZahtjev.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPrikaz;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton stripNoviZahtjev;
        private System.Windows.Forms.ToolStripButton stripPrikaz;
        private System.Windows.Forms.Label lblPretplatnik;
        private System.Windows.Forms.GroupBox grpZahtjev;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdbProduljenje;
        private System.Windows.Forms.RadioButton rdbSimpaPrijelaz;
        private System.Windows.Forms.RadioButton rdbNovaPretplata;
        TextBoxNumbers txtMobitel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTrajanje;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnPonisti;
        private System.Windows.Forms.Button btnAzuriraj;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbUredaji;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripLabel stripZahtjevDodan;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStripDropDown stripMeduspremnikDD;
        private System.Windows.Forms.ToolStripButton stripMeduspremnik;
    }
}