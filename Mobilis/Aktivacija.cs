using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Mobilis
{
    public partial class Aktivacija : Form
    {
        public Aktivacija()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CalculateMD5Hash(txtKorisnik.Text + "mobilisprogram?%$") == txtKod.Text)
            {
                Properties.Settings.Default.AktivKorisnik = txtKorisnik.Text;
                Properties.Settings.Default.AktivKod = txtKod.Text;
                Properties.Settings.Default.Save();
                MessageBox.Show("Kod ispravan, molimo pokrenite aplikaciju ponovo", "Aktivacija");
            }

            this.Close();
        }
        static public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

    }
}