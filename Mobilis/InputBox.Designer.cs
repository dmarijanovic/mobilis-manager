namespace Mobilis
{
    partial class InputBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputBox));
            this.btnUredu = new System.Windows.Forms.Button();
            this.brnPonisti = new System.Windows.Forms.Button();
            this.txtUnos = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUredu
            // 
            this.btnUredu.Location = new System.Drawing.Point(140, 60);
            this.btnUredu.Name = "btnUredu";
            this.btnUredu.Size = new System.Drawing.Size(75, 23);
            this.btnUredu.TabIndex = 1;
            this.btnUredu.Text = "&U redu";
            this.btnUredu.UseVisualStyleBackColor = true;
            this.btnUredu.Click += new System.EventHandler(this.btnUredu_Click);
            // 
            // brnPonisti
            // 
            this.brnPonisti.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.brnPonisti.Location = new System.Drawing.Point(221, 60);
            this.brnPonisti.Name = "brnPonisti";
            this.brnPonisti.Size = new System.Drawing.Size(75, 23);
            this.brnPonisti.TabIndex = 2;
            this.brnPonisti.Text = "&Poništi";
            this.brnPonisti.UseVisualStyleBackColor = true;
            this.brnPonisti.Click += new System.EventHandler(this.brnPonisti_Click);
            // 
            // txtUnos
            // 
            this.txtUnos.Location = new System.Drawing.Point(85, 34);
            this.txtUnos.Name = "txtUnos";
            this.txtUnos.Size = new System.Drawing.Size(211, 20);
            this.txtUnos.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Da bi ste potvrdili brisanje upišite rijeè \'obriši\'";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(57, 50);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // InputBox
            // 
            this.AcceptButton = this.btnUredu;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 90);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUnos);
            this.Controls.Add(this.brnPonisti);
            this.Controls.Add(this.btnUredu);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "InputBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mobilis - Potvrda";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnUredu;
        private System.Windows.Forms.Button brnPonisti;
        private System.Windows.Forms.TextBox txtUnos;
        private System.Windows.Forms.Label label1;
    }
}