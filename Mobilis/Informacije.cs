using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;

namespace Mobilis
{
    public partial class Informacije : BaseForm
    {
        OdbcCommand odbcCommand;
        OdbcConnection odbcConn = new System.Data.Odbc.OdbcConnection();
        ArrayList poslovnice = new ArrayList();
        public Informacije()
        {
            InitializeComponent();
            string connectionString = Program.GetConnString();
            odbcConn = new System.Data.Odbc.OdbcConnection(connectionString);
        }

        private void Informacije_Load(object sender, EventArgs e)
        {
            dtpPocetak.Value = DateTime.Now;
            dtpKraj.Value = DateTime.Now;
            cmbBrziOdabir.SelectedIndex = 0;
            GetPoslovniceZaposlenici();
        }
        private void btnPonisti_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void brnPrikazi_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        private void cmbBrziOdabir_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime time;
            int godina = DateTime.Now.Year;
            int mjesec = DateTime.Now.Month;
            switch (cmbBrziOdabir.SelectedIndex)
            {
                case 0:     //Danas
                    {
                        dtpPocetak.Value = DateTime.Now;
                        dtpKraj.Value = DateTime.Now;
                        break;
                    }
                case 1:     //Trenutni mjesec
                    {
                        time = DateTime.Parse("1." + mjesec + "." + godina);

                        dtpPocetak.Value = time;
                        time = DateTime.Parse(DateTime.DaysInMonth(godina, mjesec) + "." + mjesec + "." + godina);
                        dtpKraj.Value = time;
                        break;
                    }
                case 2:     //Prošli mjesec
                    {

                        if (mjesec == 1)
                        {
                            mjesec = 12;
                            godina--;
                        }
                        else
                            mjesec--;
                        time = DateTime.Parse("1." + mjesec + "." + godina);

                        dtpPocetak.Value = time;
                        time = DateTime.Parse(DateTime.DaysInMonth(godina, mjesec) + "." + mjesec + "." + godina);
                        dtpKraj.Value = time;
                        break;
                    }
                case 3:                 //Dva mjeseca unatrag
                    if (mjesec == 1)
                    {
                        mjesec = 11;
                        godina--;
                    }
                    else
                    {
                        mjesec--;
                        mjesec--;
                    }
                    time = DateTime.Parse("1." + mjesec + "." + godina);

                    dtpPocetak.Value = time;
                    time = DateTime.Parse(DateTime.DaysInMonth(godina, mjesec) + "." + mjesec + "." + godina);
                    dtpKraj.Value = time;
                    break;
                case 4:                 //Tri mjeseca unatrag
                    if (mjesec == 1)
                    {
                        mjesec = 10;
                        godina--;
                    }
                    else
                    {
                        mjesec--;
                        mjesec--;
                        mjesec--;
                    }
                    time = DateTime.Parse("1." + mjesec + "." + godina);

                    dtpPocetak.Value = time;
                    time = DateTime.Parse(DateTime.DaysInMonth(godina, mjesec) + "." + mjesec + "." + godina);
                    dtpKraj.Value = time;
                    break;
                case 5:                 //Ova godina
                    time = DateTime.Parse("1.1." + godina);

                    dtpPocetak.Value = time;
                    time = DateTime.Parse(DateTime.DaysInMonth(godina, 12) + ".12." + godina);
                    dtpKraj.Value = time;
                    break;
                case 6:                 //Prosla godina
                    godina--;
                    time = DateTime.Parse("1.1." + godina);

                    dtpPocetak.Value = time;
                    time = DateTime.Parse(DateTime.DaysInMonth(godina, 12) + ".12." + godina);
                    dtpKraj.Value = time;
                    break;
                default:
                    break;
            }


        }
        private void cmbPoslovnice_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbPoslovnice.SelectedIndex;
            if (index > 0)
            {
                foreach (PoslovniceStruct poslovnica in poslovnice)
                {
                    if (poslovnica.cmbIndex == index)
                    {
                        poslovnica.PopuniCmb(ref cmbZaposlenici, ZaposlenikID);
                        break;
                    }
                }
            }
            else if (index == 0)
            {
                cmbZaposlenici.Items.Clear();
                cmbZaposlenici.Items.Add("Svi");
                cmbZaposlenici.SelectedIndex = 0;
            }
        }

        private void GetPoslovniceZaposlenici()
        {
            // dobiva poslovnice
            int aktivnaPoslovnicaNaIndexu = 0;
            OdbcDataReader dr;
            odbcCommand = new OdbcCommand("select * from poslovnice", odbcConn);
            int pID;
            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                dr = odbcCommand.ExecuteReader();
                cmbPoslovnice.Items.Clear();
                poslovnice.Clear();
                cmbPoslovnice.Items.Add("Sve");

                while (dr.Read())
                {
                    cmbPoslovnice.Items.Add(dr["ime"]);
                    int.TryParse(dr["id"].ToString(), out pID);
                    poslovnice.Add(new PoslovniceStruct(cmbPoslovnice.Items.Count - 1, pID));
                    if (ZaposlenikPoslovnica == pID)
                        aktivnaPoslovnicaNaIndexu = cmbPoslovnice.Items.Count - 1;
                }
                dr.Close();
            }
            catch { MessageBox.Show("Dobivanje liste poslovnica neuspješno", "Greška"); }
            finally
            {
            }
            // dobiva zaposlenike
            odbcCommand = new OdbcCommand("select * from zaposlenici", odbcConn);
            int uID;
            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                dr = odbcCommand.ExecuteReader();
                while (dr.Read())
                {
                    int.TryParse(dr["id"].ToString(), out uID);
                    int.TryParse(dr["pid"].ToString(), out pID);
                    for (int i = 0; i < poslovnice.Count; i++)
                    {
                        PoslovniceStruct poslovnica = (PoslovniceStruct)poslovnice[i];
                        if (pID == poslovnica.pID)
                        {
                            poslovnica.DodajZaposlenika(new ZaposlenikStruct(dr["username"].ToString(), uID));
                            break;
                        }
                    }
                }
                dr.Close();
            }
            catch { MessageBox.Show("Dobivanje liste zaposlenika neuspješno", "Greška"); }
            finally
            {
                if (odbcConn.State == ConnectionState.Open)
                    odbcConn.Close();
            }
            cmbPoslovnice.SelectedIndex = aktivnaPoslovnicaNaIndexu;
        }

        public DateTime Pocetak
        {
            get { return dtpPocetak.Value; }
        }
        public DateTime Kraj
        {
            get { return dtpKraj.Value; }
        }
        public void ZaposlenikSQLINIDLista(out string sqlINLista, out string imena)
        {
            int indexPos = cmbPoslovnice.SelectedIndex;
            int indexZap = cmbZaposlenici.SelectedIndex;
            sqlINLista = "";
            imena = "";
            foreach (PoslovniceStruct poslovnica in poslovnice)
            {
                if (indexPos == 0 || poslovnica.cmbIndex == indexPos)
                {
                    sqlINLista += poslovnica.GetSQLListu(indexZap, ref imena);
                }
            }
        }

        class ZaposlenikStruct
        {
            public string ime;
            public int id;
            public int cmbIndex;
            public ZaposlenikStruct(string ime, int id)
            {
                this.ime = ime;
                this.id = id;
            }
        }
        class PoslovniceStruct
        {
            ArrayList zaposlenici = new ArrayList();
            public int cmbIndex;
            public int pID;
            public PoslovniceStruct(int cmbIndex, int pID)
            {
                this.pID = pID;
                this.cmbIndex = cmbIndex;
            }
            public void DodajZaposlenika(ZaposlenikStruct zaposlenik)
            {
                zaposlenici.Add(zaposlenik);
            }
            public void PopuniCmb(ref ComboBox cmb, int setAktivniID)
            {
                cmb.Items.Clear();
                cmb.Items.Add("Svi");
                for (int i = 0; i < zaposlenici.Count; i++)
                {
                    ZaposlenikStruct zaposlenik = (ZaposlenikStruct)zaposlenici[i];
                    zaposlenik.cmbIndex = cmb.Items.Count;
                    cmb.Items.Add(zaposlenik.ime);
                    if (zaposlenik.id == setAktivniID)
                        cmb.SelectedIndex = zaposlenik.cmbIndex;
                }
                if (setAktivniID == 0)
                    cmb.SelectedIndex = 0;
            }
            public string GetSQLListu(int indexZaposlenika, ref string imena)
            {
                string rezultat = "";
                foreach (ZaposlenikStruct zaposlenik in zaposlenici)
                {
                    if (indexZaposlenika == 0 || zaposlenik.cmbIndex == indexZaposlenika)
                    {
                        rezultat += zaposlenik.id + ",";
                        imena += zaposlenik.ime + ",";
                    }
                        
                }
                if (rezultat.Length > 0)
                {
                    rezultat = rezultat.Substring(0, rezultat.Length - 1);
                    imena = imena.Substring(0, imena.Length - 1);
                }
                return rezultat;
            }
        }

    }
}