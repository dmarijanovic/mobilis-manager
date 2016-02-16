namespace Mobilis
{
    partial class Mobiteli
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mobiteli));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabMobiteli = new System.Windows.Forms.TabPage();
            this.tabNoviMobitel = new System.Windows.Forms.TabPage();
            this.btnDodajMobitel = new System.Windows.Forms.Button();
            this.txtImeMobitela = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnPonisti = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabMobiteli.SuspendLayout();
            this.tabNoviMobitel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(588, 187);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabMobiteli);
            this.tabControl1.Controls.Add(this.tabNoviMobitel);
            this.tabControl1.Location = new System.Drawing.Point(7, 9);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(602, 219);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // tabMobiteli
            // 
            this.tabMobiteli.Controls.Add(this.dataGridView1);
            this.tabMobiteli.Location = new System.Drawing.Point(4, 22);
            this.tabMobiteli.Name = "tabMobiteli";
            this.tabMobiteli.Padding = new System.Windows.Forms.Padding(3);
            this.tabMobiteli.Size = new System.Drawing.Size(594, 193);
            this.tabMobiteli.TabIndex = 0;
            this.tabMobiteli.Text = "Mobiteli";
            this.tabMobiteli.UseVisualStyleBackColor = true;
            // 
            // tabNoviMobitel
            // 
            this.tabNoviMobitel.Controls.Add(this.btnDodajMobitel);
            this.tabNoviMobitel.Controls.Add(this.txtImeMobitela);
            this.tabNoviMobitel.Controls.Add(this.label1);
            this.tabNoviMobitel.Controls.Add(this.pictureBox1);
            this.tabNoviMobitel.Location = new System.Drawing.Point(4, 22);
            this.tabNoviMobitel.Name = "tabNoviMobitel";
            this.tabNoviMobitel.Padding = new System.Windows.Forms.Padding(3);
            this.tabNoviMobitel.Size = new System.Drawing.Size(594, 193);
            this.tabNoviMobitel.TabIndex = 1;
            this.tabNoviMobitel.Text = "Novi Mobitel";
            this.tabNoviMobitel.UseVisualStyleBackColor = true;
            // 
            // btnDodajMobitel
            // 
            this.btnDodajMobitel.Location = new System.Drawing.Point(473, 26);
            this.btnDodajMobitel.Name = "btnDodajMobitel";
            this.btnDodajMobitel.Size = new System.Drawing.Size(75, 23);
            this.btnDodajMobitel.TabIndex = 3;
            this.btnDodajMobitel.Text = "&Dodaj";
            this.btnDodajMobitel.UseVisualStyleBackColor = true;
            this.btnDodajMobitel.Click += new System.EventHandler(this.btnDodajMobitel_Click);
            // 
            // txtImeMobitela
            // 
            this.txtImeMobitela.Location = new System.Drawing.Point(215, 26);
            this.txtImeMobitela.Name = "txtImeMobitela";
            this.txtImeMobitela.Size = new System.Drawing.Size(206, 20);
            this.txtImeMobitela.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(129, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "&Ime mobitela:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(25, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(54, 50);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnPonisti
            // 
            this.btnPonisti.Location = new System.Drawing.Point(534, 234);
            this.btnPonisti.Name = "btnPonisti";
            this.btnPonisti.Size = new System.Drawing.Size(75, 23);
            this.btnPonisti.TabIndex = 2;
            this.btnPonisti.Text = "&U redu";
            this.btnPonisti.UseVisualStyleBackColor = true;
            this.btnPonisti.Click += new System.EventHandler(this.btnPonisti_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Mobiteli
            // 
            this.AcceptButton = this.btnDodajMobitel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 265);
            this.Controls.Add(this.btnPonisti);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Mobiteli";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mobilis - Mobiteli";
            this.Load += new System.EventHandler(this.Mobiteli_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabMobiteli.ResumeLayout(false);
            this.tabNoviMobitel.ResumeLayout(false);
            this.tabNoviMobitel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabMobiteli;
        private System.Windows.Forms.TabPage tabNoviMobitel;
        private System.Windows.Forms.Button btnPonisti;
        private System.Windows.Forms.Button btnDodajMobitel;
        private System.Windows.Forms.TextBox txtImeMobitela;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}