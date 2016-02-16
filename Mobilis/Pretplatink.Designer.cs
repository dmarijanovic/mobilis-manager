namespace Mobilis
{
    partial class Pretplatink
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pretplatink));
            this.label1 = new System.Windows.Forms.Label();
            this.txtIme = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrezime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUlica = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.stripMeduspremnik = new System.Windows.Forms.ToolStripButton();
            this.stripInformacije = new System.Windows.Forms.ToolStripLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNapomena = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtGrad = new Mobilis.TextBoxTab();
            this.txtMBG = new Mobilis.TextBoxNumbers();
            this.txtPB = new Mobilis.TextBoxNumbers();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.chkPretplate = new System.Windows.Forms.CheckBox();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Ime:";
            // 
            // txtIme
            // 
            this.txtIme.Location = new System.Drawing.Point(91, 21);
            this.txtIme.Name = "txtIme";
            this.txtIme.Size = new System.Drawing.Size(146, 20);
            this.txtIme.TabIndex = 1;
            this.txtIme.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(262, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "&Prezime:";
            // 
            // txtPrezime
            // 
            this.txtPrezime.Location = new System.Drawing.Point(350, 21);
            this.txtPrezime.Name = "txtPrezime";
            this.txtPrezime.Size = new System.Drawing.Size(162, 20);
            this.txtPrezime.TabIndex = 3;
            this.txtPrezime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "&Ulica:";
            // 
            // txtUlica
            // 
            this.txtUlica.Location = new System.Drawing.Point(91, 60);
            this.txtUlica.Name = "txtUlica";
            this.txtUlica.Size = new System.Drawing.Size(421, 20);
            this.txtUlica.TabIndex = 5;
            this.txtUlica.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "&Grad:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "&MB/MBG:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(262, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Poštanski &broj:";
            // 
            // btnDodaj
            // 
            this.btnDodaj.Location = new System.Drawing.Point(397, 269);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(75, 23);
            this.btnDodaj.TabIndex = 12;
            this.btnDodaj.Text = "&Dodaj";
            this.btnDodaj.UseVisualStyleBackColor = true;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(478, 269);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "P&oništi";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripMeduspremnik,
            this.stripInformacije});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(555, 25);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // stripMeduspremnik
            // 
            this.stripMeduspremnik.Image = ((System.Drawing.Image)(resources.GetObject("stripMeduspremnik.Image")));
            this.stripMeduspremnik.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stripMeduspremnik.Name = "stripMeduspremnik";
            this.stripMeduspremnik.Size = new System.Drawing.Size(95, 22);
            this.stripMeduspremnik.Text = "M&eðuspremnik";
            this.stripMeduspremnik.ToolTipText = "Preuzmi podatke iz meðuspremnika";
            this.stripMeduspremnik.Click += new System.EventHandler(this.stripMeduspremnik_Click);
            // 
            // stripInformacije
            // 
            this.stripInformacije.ForeColor = System.Drawing.SystemColors.GrayText;
            this.stripInformacije.Name = "stripInformacije";
            this.stripInformacije.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.stripInformacije.Size = new System.Drawing.Size(10, 22);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNapomena);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtGrad);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtUlica);
            this.groupBox1.Controls.Add(this.txtMBG);
            this.groupBox1.Controls.Add(this.txtIme);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtPB);
            this.groupBox1.Controls.Add(this.txtPrezime);
            this.groupBox1.Location = new System.Drawing.Point(0, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(553, 235);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // txtNapomena
            // 
            this.txtNapomena.Location = new System.Drawing.Point(91, 177);
            this.txtNapomena.Multiline = true;
            this.txtNapomena.Name = "txtNapomena";
            this.txtNapomena.Size = new System.Drawing.Size(421, 47);
            this.txtNapomena.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "&Napomena:";
            // 
            // txtGrad
            // 
            this.txtGrad.AcceptsTab = true;
            this.txtGrad.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtGrad.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtGrad.Location = new System.Drawing.Point(91, 99);
            this.txtGrad.Name = "txtGrad";
            this.txtGrad.Size = new System.Drawing.Size(146, 20);
            this.txtGrad.TabIndex = 7;
            this.txtGrad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGrad.TabStisnut += new Mobilis.TextBoxTab.delTab(this.txtGrad_TabStisnut);
            // 
            // txtMBG
            // 
            this.txtMBG.Location = new System.Drawing.Point(91, 138);
            this.txtMBG.MaxLength = 13;
            this.txtMBG.Name = "txtMBG";
            this.txtMBG.Size = new System.Drawing.Size(421, 20);
            this.txtMBG.TabIndex = 11;
            this.txtMBG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMBG.TextChanged += new System.EventHandler(this.txtMBG_TextChanged);
            // 
            // txtPB
            // 
            this.txtPB.Location = new System.Drawing.Point(350, 99);
            this.txtPB.Name = "txtPB";
            this.txtPB.Size = new System.Drawing.Size(162, 20);
            this.txtPB.TabIndex = 9;
            this.txtPB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // chkPretplate
            // 
            this.chkPretplate.AutoSize = true;
            this.chkPretplate.Location = new System.Drawing.Point(12, 275);
            this.chkPretplate.Name = "chkPretplate";
            this.chkPretplate.Size = new System.Drawing.Size(98, 17);
            this.chkPretplate.TabIndex = 16;
            this.chkPretplate.Text = "Otvori p&retplate";
            this.chkPretplate.UseVisualStyleBackColor = true;
            // 
            // btnObrisi
            // 
            this.btnObrisi.Location = new System.Drawing.Point(303, 269);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(75, 23);
            this.btnObrisi.TabIndex = 17;
            this.btnObrisi.Text = "&Obriši";
            this.btnObrisi.UseVisualStyleBackColor = true;
            this.btnObrisi.Visible = false;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // Pretplatink
            // 
            this.AcceptButton = this.btnDodaj;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(555, 302);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.chkPretplate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDodaj);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Pretplatink";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mobilis - Pretplatnik";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Pretplatink_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Pretplatink_KeyDown);
            this.Load += new System.EventHandler(this.Pretplatink_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIme;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPrezime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUlica;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        TextBoxNumbers txtMBG;
        private TextBoxTab txtGrad;
        private System.Windows.Forms.Label label6;
        TextBoxNumbers txtPB;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton stripMeduspremnik;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtNapomena;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkPretplate;
        private System.Windows.Forms.ToolStripLabel stripInformacije;
        private System.Windows.Forms.Button btnObrisi;
    }
}