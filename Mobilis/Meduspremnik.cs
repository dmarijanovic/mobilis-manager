using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Mobilis
{
    public partial class Meduspremnik : Form
    {
        public Meduspremnik()
        {
            InitializeComponent();
        }
        private void PreuzimanjeClipboard()
        {
            if (Clipboard.ContainsText())
            {
                int yOs = 40;
                int tabIndex = 4;
                int trajanje = 0;
                int index = 1;
                bool analiza = false;
                string[] data = new string[0];
                data = Clipboard.GetText().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (data.Length == 0)
                {
                    MessageBox.Show("Meðuspremnik ne sadrzi ispravan format za pretplatnika", "Mobilis");
                    this.Close();
                    return;
                }
                TextBoxNumbers txtbox = null;
                CheckBox chkbox = null;
                string mobitel = "";
                foreach (string line in data)
                {
                    if (line.IndexOf("Service:") > -1)
                    {
                        int indexOf = line.Trim().IndexOf("Service:");
                        mobitel = Convert.ToString(line.Trim().Substring(0, indexOf)).Trim();
                        if (mobitel.StartsWith("385"))
                        {
                            if (mobitel.Length > 4)
                            {
                                mobitel = mobitel.Substring(3, mobitel.Length - 3);
                                mobitel = "0" + mobitel;
                            }
                        }
                        tabIndex++;
                        txtbox = new TextBoxNumbers();
                        txtbox.Location = new System.Drawing.Point(25, yOs);
                        txtbox.Name = "mob";
                        txtbox.Size = new System.Drawing.Size(148, 20);
                        txtbox.Text = Convert.ToString(mobitel.Trim());
                        txtbox.TabIndex = tabIndex;
                        this.panel1.Controls.Add(txtbox);

                        tabIndex++;
                        txtbox = new TextBoxNumbers();
                        txtbox.Location = new System.Drawing.Point(209, yOs);
                        txtbox.Name = "pre";
                        txtbox.Tag = index;
                        txtbox.Size = new System.Drawing.Size(92, 20);
                        txtbox.Text = Convert.ToString(mobitel.Trim());
                        txtbox.TabIndex = tabIndex;
                        txtbox.TextChanged += new EventHandler(txtbox_TextChanged);
                        this.panel1.Controls.Add(txtbox);

                        tabIndex++;
                        chkbox = new CheckBox();
                        chkbox.AutoSize = true;
                        chkbox.Location = new System.Drawing.Point(322, yOs);
                        chkbox.Name = "chk" + index.ToString();
                        chkbox.Size = new System.Drawing.Size(169, 17);
                        chkbox.TabIndex = tabIndex;
                        chkbox.Text = "Pretplata ide od ovog mjeseca";
                        chkbox.UseVisualStyleBackColor = true;
                        this.panel1.Controls.Add(chkbox);

                        yOs += 30;
                        index++;
                        analiza = true;
                    }
                    if (line.IndexOf("Broj pretplata do isteka") > -1)
                    {
                        int indexOf = line.Trim().LastIndexOf(" ");
                        if (indexOf > -1)
                        {
                            int.TryParse(line.Trim().Substring(indexOf, line.Trim().Length - indexOf), out trajanje);
                            txtbox.Text = trajanje.ToString();
                            if (DateTime.Today.Day < 15 && (trajanje == 12 || trajanje == 24))
                                chkbox.Enabled = true;
                            else
                                chkbox.Enabled = false;
                            analiza = true;
                        }
                    }
                }
                if (!analiza)
                {
                    MessageBox.Show("Meðuspremnik ne sadrzi ispravan format za pretplatnika", "Mobilis");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Meðuspremnik ne sadrzi tekstualne podatke za analizu", "Mobilis");
                this.Close();
            }
        }

        void txtbox_TextChanged(object sender, EventArgs e)
        {
            TextBoxNumbers txt = (TextBoxNumbers )sender ;
            CheckBox chk = (CheckBox)panel1.Controls["chk" + txt.Tag.ToString()];
            if (chk != null)
            {
                if (DateTime.Today.Day < 15 && ( txt.Text.Trim() == "12" ||  txt.Text.Trim() == "24"))
                    chk.Enabled = true;
                else
                    chk.Enabled = false;
            }
        }

        private void Meduspremnik_Load(object sender, EventArgs e)
        {
            PreuzimanjeClipboard();
        }

        private void btnUredu_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnPonisti_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}