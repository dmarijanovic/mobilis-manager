using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Mobilis
{
    public partial class InputBox : Form
    {
        bool sucelje = false;
        public InputBox()
        {
            InitializeComponent();
        }
        public InputBox(string naslov, string tekst): this()
        {
            label1.Text = tekst;
            this.Text = "Mobilis -" + naslov;
            sucelje = true;
        }

        private void btnUredu_Click(object sender, EventArgs e)
        {
            if (txtUnos.Text.Trim() == "obrisi" || txtUnos.Text.Trim() == "obriši" || sucelje)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void brnPonisti_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
        public string Tekst
        {
            get { return txtUnos.Text.Trim(); }
        }
    }
}