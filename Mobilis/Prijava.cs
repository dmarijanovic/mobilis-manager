using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
namespace Mobilis
{
    public partial class Prijava : Form
    {
        OdbcCommand odbcCommand;
        OdbcConnection odbcConn = new System.Data.Odbc.OdbcConnection();
        int id = 0;
        int pristup;
        int poslovnica;
        public Prijava()
        {
            InitializeComponent();
            string connectionString = Program.GetConnString();
            odbcConn = new System.Data.Odbc.OdbcConnection(connectionString);
        }

        private void btnPonisti_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        private void btnPrijava_Click(object sender, EventArgs e)
        {
            if (MyValidate())
            {
                if (PrijaviSe())
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    Status("Prijava nije uspjela");
                    MessageBox.Show("Prijava nije uspjela", "Prijava", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                errorProvider1.SetError(txtIme, "Unesi korisnièko ime");
                errorProvider1.SetError(txtLozinka, "Unesi lozinku");
            }
        }
        private bool PrijaviSe()
        {
            id = 0;
            OdbcDataReader dr;
            odbcCommand = new OdbcCommand("SELECT id, pristup, pid FROM zaposlenici WHERE username = '" + txtIme.Text.Trim() + "' and password = '" + txtLozinka.Text + "'", odbcConn);
            Status("Spajam se na bazu...");
            Application.DoEvents();
            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                dr = odbcCommand.ExecuteReader();
                while (dr.Read())
                {
                    int.TryParse(dr["id"].ToString(), out id);
                    int.TryParse(dr["pristup"].ToString(), out pristup);
                    int.TryParse(dr["pid"].ToString(), out poslovnica);
                }
                dr.Close();
                if (id > 0)
                    Status("Dobro došli " + txtIme.Text);
            }
            catch { MessageBox.Show("Greška prilikom prijave", "Greška"); Status("Greška prilikom prijave"); }
            finally
            {
                if (odbcConn.State == ConnectionState.Open)
                    odbcConn.Close();
            }
            if (id > 0)
                return true;
            else
                return false;
        }
        private bool MyValidate()
        {
            if (txtIme.Text.Trim() == "" && txtLozinka.Text.Trim() == "")
                return false;
            else
                return true;
        }
        private void Status(string data)
        {
            Form1 frm = (Form1)this.Owner;
            frm.toolStripConnStatus.Text = data;
        }

        public int IDZaposlenika
        {
            get { return id; }
        }
        public string Ime
        {
            get { return txtIme.Text.Trim(); }
        }
        public int Pristup
        {
            get { return pristup; }
        }
        public int Poslovnica
        {
            get { return poslovnica; }
        }
    }
}