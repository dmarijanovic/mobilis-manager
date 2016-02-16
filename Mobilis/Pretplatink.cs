using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Data.Odbc;


namespace Mobilis
{
    public partial class Pretplatink : BaseForm
    {
        OdbcCommand odbcCommand;
        OdbcConnection odbcConn = new System.Data.Odbc.OdbcConnection();
        ArrayList gradLista = new ArrayList();
        int pretplatnikID;
        public Pretplatink()
        {
            InitializeComponent();
            string connectionString = Program.GetConnString();
            odbcConn = new System.Data.Odbc.OdbcConnection(connectionString);
        }
        public Pretplatink(int pretplatnikID): this()
        {
            this.pretplatnikID = pretplatnikID;
            btnDodaj.Text = "Ažuriraj";
        }
        void txtGrad_TabStisnut()
        {
            string imeGrada = txtGrad.Text.ToLower().Trim();
            foreach (GradBaza grad in gradLista)
            {
                if (grad.ime.ToLower() == imeGrada)
                    txtPB.Text = grad.pb;
            }
    }
        private void Pretplatink_Load(object sender, EventArgs e)
        {
            LoadGradovi();
            PretplatnikUcitajPodatke();
            toolTip1.SetToolTip(txtGrad, "Pritisnite TAB za automacko dobivanje poštankog broja");
            txtIme.Focus();
        }
        private void Pretplatink_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyValue == 83)
            {
                this.Opacity = 0.4;
                e.SuppressKeyPress = true;
                Form frm = (Form)this.Owner;
                frm.Opacity = 0.3;
            }
        }
        private void Pretplatink_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyValue == 83)
            {
                this.Opacity = 1;
                e.SuppressKeyPress = true;
                Form frm = (Form)this.Owner;
                frm.Opacity = 1;
            }
        }

        private void PretplatnikUcitajPodatke()
        {
            if (pretplatnikID <= 0)
                return;
            OdbcDataReader dr;
            odbcCommand = new OdbcCommand("select * from pretplatnici where id = " + pretplatnikID, odbcConn);
            Status("Spajam se na bazu...");
            Application.DoEvents();
            DateTime created;
            DateTime changes;
            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                dr = odbcCommand.ExecuteReader();
                while (dr.Read())
                {
                    txtIme.Text = dr["ime"].ToString();
                    txtPrezime.Text = dr["prezime"].ToString();
                    txtUlica.Text = dr["ulica"].ToString();
                    txtGrad.Text = dr["grad"].ToString();
                    txtPB.Text = dr["pb"].ToString();
                    txtMBG.Text = dr["mbg"].ToString();
                    txtNapomena.Text = dr["info"].ToString();
                    DateTime.TryParse(dr["changes"].ToString(), out created);
                    DateTime.TryParse(dr["created"].ToString(), out changes);
                    stripInformacije.Text = "Dodan u bazu " + changes.ToString("dd.MMMM.yyyy u hh:mm") + ", zadnja izmjena " + created.ToString("dd.MMMM.yyyy u hh:mm");
                }
                if (dr.HasRows)
                {
                    btnObrisi.Visible = true;
                    Status("Podaci o pretplatniku uspješno uèitani");
                }
                else
                {
                    pretplatnikID = 0;
                    MessageBox.Show("Podaci nisu uèitani", "Mobilis");
                }
                dr.Close();
            }
            catch { MessageBox.Show("Greška prilikom dobivanja podataka o pretplatniku", "Greška"); Status("Greška prilikom dobivanja podataka o pretplatniku"); }
            finally
            {
                if (odbcConn.State == ConnectionState.Open)
                    odbcConn.Close();
            }
        }
        private void LoadGradovi()
        {
            string putanja = Application.StartupPath;
            if(!putanja .EndsWith ("/"))
                putanja += "/";
            putanja += "gradovi.xml";
            if(!File.Exists (putanja ))
                return;

            XmlTextReader reader = new XmlTextReader(putanja);
            while (reader.Read())
            {
                if ( reader.NodeType == XmlNodeType.Element  && reader.Name == "grad")
                {
                    gradLista.Add(new GradBaza(reader.GetAttribute("ime"), reader.GetAttribute("pb")));
                    txtGrad.AutoCompleteCustomSource.Add(reader.GetAttribute("ime"));
                }
            }
        }
        private int NoviPretplatnik()
        {
            int rez = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into pretplatnici (ime, prezime, ulica, grad, pb, mbg, changes, created, info, zid) values ('");
            sb.Append(txtIme.Text.Trim());
            sb.Append("','");
            sb.Append(txtPrezime.Text.Trim());
            sb.Append("','");
            sb.Append(txtUlica.Text.Trim());
            sb.Append("','");
            sb.Append(txtGrad.Text.Trim());
            sb.Append("','");
            sb.Append(txtPB.Text.Trim());
            sb.Append("','");
            sb.Append(txtMBG.Text.Trim());
            sb.Append("','");
            sb.Append(DateTime.MinValue.ToString("yyyy.MM.dd HH:mm:ss"));
            sb.Append("','");
            sb.Append(DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"));
            sb.Append("','");
            sb.Append(txtNapomena.Text.Trim());
            sb.Append ("',");
            sb.Append(base.ZaposlenikID); 
            sb.Append(")");

            odbcCommand = new OdbcCommand(sb.ToString(), odbcConn);
            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                rez = odbcCommand.ExecuteNonQuery();
                if (rez > 0)
                {
                    //TODO: obrisi  MessageBox.Show("Korisnik dodan u bazu");
                    Status("Korisnik  " + txtIme.Text.ToUpper().Trim() + " " + txtPrezime.Text.ToUpper().Trim() + " je dodan u bazu");
                }
            }
            catch { MessageBox.Show("Korisnik nije dodan u bazu", "Greška"); }
            finally
            {
                //if (odbcConn.State == ConnectionState.Open)
                //    odbcConn.Close();
            }
            return rez;
        }
        private int AzurirajPretplatnika()
        {
            int rez = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE pretplatnici SET ime = '");
            sb.Append(txtIme.Text.Trim());
            sb.Append("', prezime = '");
            sb.Append(txtPrezime.Text.Trim());
            sb.Append("', ulica = '");
            sb.Append(txtUlica.Text.Trim());
            sb.Append("', grad = '");
            sb.Append(txtGrad.Text.Trim());
            sb.Append("', pb = '");
            sb.Append(txtPB.Text.Trim());
            sb.Append("', mbg = '");
            sb.Append(txtMBG.Text.Trim());
            sb.Append("', changes = NOW(), ");
            sb.Append ("info = '");
            sb.Append (txtNapomena .Text.Trim ());
            sb.Append ("' WHERE id = ");
            sb.Append(pretplatnikID);
            odbcCommand = new OdbcCommand(sb.ToString(), odbcConn);
            Status("Spajam se na bazu...");
            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                rez = odbcCommand.ExecuteNonQuery();
                if (rez > 0)
                {
                    //MessageBox.Show("Pretplatnièki podaci uspješno ažurirani");
                    Status("Pretplatnik " + txtIme.Text.ToUpper().Trim() + " " + txtPrezime.Text.ToUpper().Trim() + " je ažuriran");
                }

            }
            catch{ MessageBox.Show("Pretplatnik nije ažuriran ", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally
            {
                if (odbcConn.State == ConnectionState.Open)
                    odbcConn.Close();
            }
            return rez;
        }
        private void ObrisiPretplatnika()
        {
            // brisem pretplatnika
            int rez = 0;
            odbcCommand = new OdbcCommand("DELETE FROM pretplatnici WHERE id =" + pretplatnikID, odbcConn);
            Status("Spajam se na bazu...");
            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                rez = odbcCommand.ExecuteNonQuery();
                if (rez > 0)
                {
                    MessageBox.Show("Pretplatnik je obrisan", "Mobilis");
                    Status("Pretplatnik " + txtIme.Text.ToUpper().Trim() + " " + txtPrezime.Text.ToUpper().Trim() + " je obrisan");
                }
                else
                {
                    MessageBox.Show("Pretplatnik nije obrisan", "Mobilis");
                    Status("Pretplatnik " + txtIme.Text.ToUpper().Trim() + " " + txtPrezime.Text.ToUpper().Trim() + " nije obrisan");
                }

            }
            catch { MessageBox.Show("Pretplatnik nije obrisan ", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally
            {
                // cuvam konekciju za jos jedan query
            }
            // brisem sve pretplate od pretplatnika
            odbcCommand = new OdbcCommand("DELETE FROM pretplate WHERE pid =" + pretplatnikID, odbcConn);
            Status("Spajam se na bazu...");
            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                rez = odbcCommand.ExecuteNonQuery();
            }
            catch { MessageBox.Show("Pretplate od obrisanog pretplatnika nisu obrisanenije obrisane ", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally
            {
                if (odbcConn.State == ConnectionState.Open)
                    odbcConn.Close();
            }
        }
        private int ZadnjiKorisnikID()
        {
            int rez = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append("select id from pretplatnici where mbg = '");
            sb.Append(txtMBG.Text.Trim());
            sb.Append("'");

            odbcCommand = new OdbcCommand(sb.ToString(), odbcConn);
            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                OdbcDataReader dr = odbcCommand.ExecuteReader();
                while (dr.Read()) 
                {
                    int.TryParse(dr["id"].ToString(), out rez);
                }
            }
            catch {
                MessageBox.Show("Greška prilikom automatskog vraèanja ID korsinika, potrebna ruèna pretraga", "Greška");
                Status("Greška prilikom automackog vraèanja ID korsinika, potrebna ruèna pretraga");
            }
            finally
            {
                if (odbcConn.State == ConnectionState.Open)
                    odbcConn.Close();
            }
            return rez;
        }
        private void PreuzimanjeClipboard()
        {
            if (Clipboard.ContainsText())
            {
                bool analiza = false;
                txtIme.Text = txtPrezime.Text = txtUlica.Text = txtGrad.Text = txtPB.Text = txtMBG.Text = txtNapomena.Text = "";
                string[] data = new string[0];
                data = Clipboard.GetText().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (data.Length == 0)
                {
                    MessageBox.Show("Meðuspremnik ne sadrzi ispravan format za pretplatnika", "Mobilis");
                    return;
                }
                foreach (string line in data)
                {
                    string[] podatak = new string[0];
                    podatak = line.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if (podatak[0].ToLower() == "naziv")    // ime i prezime
                    {
                        podatak = podatak[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        txtIme.Text = podatak[0].Trim();
                        txtPrezime.Text = podatak[1].Trim();
                        analiza = true;
                    }
                    if (podatak[0].ToLower() == "adresa")    // adresa
                    {
                        podatak = podatak[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (podatak.Length >= 2)
                        {
                            txtUlica.Text = podatak[0].Trim();
                            podatak[1] = podatak[1].Trim();
                            int indexOf = podatak[1].IndexOf(" ");
                            if (indexOf > -1)
                            {
                                txtPB.Text = podatak[1].Substring(0, indexOf).Trim();
                                txtGrad.Text = podatak[1].Substring(indexOf, podatak[1].Length - indexOf).Trim();
                            }
                            else
                                txtGrad.Text = podatak[1].Trim();
                            analiza = true;
                        }
                    }
                    if (podatak[0].ToLower() == "mb/mbg")    // MBG
                    {
                        txtMBG.Text = podatak[1].Trim();
                        analiza = true;
                    }
                }
                if(!analiza)
                    MessageBox.Show("Meðuspremnik ne sadrzi ispravan format za pretplatnika", "Mobilis");
            }
            else
                MessageBox.Show("Meðuspremnik ne sadrzi tekstualne podatke za analizu", "Mobilis");
        }

        private bool MyValidate()
        {
            int rez = 0;
            foreach (Control con in groupBox1.Controls)
            {
                if (con.Text == "" && con.Name != "txtNapomena")
                {
                    rez++;
                    errorProvider1.SetError(con, "Polje nemože biti prazno");
                }
                else
                    errorProvider1.SetError(con, "");
            }
            if (txtPB.Text.Length != 5)
            {
                errorProvider1.SetError(txtPB, "Neispravno");
                rez++;
            }
            else
                errorProvider1.SetError(txtPB, "");
            if (!Program.JMBG(txtMBG.Text))
            {
                errorProvider1.SetError(txtMBG, "Dali ste sigurni da je broj toèan?");
            }
            else
                errorProvider1.SetError(txtMBG, "");

            if (rez > 0)
                return false;
            else
                return true;
        }

        private void stripMeduspremnik_Click(object sender, EventArgs e)
        {
            PreuzimanjeClipboard();
        }
        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (PrijavljenJe)
            {
                if (MyValidate())
                {
                    int rez;
                    if (pretplatnikID <= 0)
                        rez = NoviPretplatnik();
                    else
                        rez = AzurirajPretplatnika();
                    if (rez > 0)
                    {
                        if (chkPretplate.Checked)
                        {
                            if (pretplatnikID <= 0)
                                pretplatnikID = ZadnjiKorisnikID();
                            DialogResult = DialogResult.OK;
                        }
                        this.Close();
                    }
                }
            }

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //AutoFillUsers();
            this.Close();
        }
        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (PrijavljenJe && pretplatnikID > 0)
            {
                InputBox frm = new InputBox();
                DialogResult rez = frm.ShowDialog();
                if (rez == DialogResult.OK)
                {
                    ObrisiPretplatnika();
                    this.Close();
                }
            }
        }

        private void txtMBG_TextChanged(object sender, EventArgs e)
        {
            if (!Program.JMBG(txtMBG.Text))
                errorProvider1.SetError(txtMBG, "Dali ste sigurni da je broj toèan?");
            else
                errorProvider1.SetError(txtMBG, "");
        }

        class GradBaza
        {
            public string ime;
            public string pb;
            public GradBaza(string ime, string pb)
            {
                this.ime = ime;
                this.pb = pb;
            }
        }

        public int PretplatnikID
        {
            get { return pretplatnikID; }
        }
        public string Ime
        {
            get { return txtIme.Text.Trim(); }
        }
        public string Prezime
        {
            get { return txtPrezime.Text.Trim(); }
        }
        public string MBG
        {
            get { return txtMBG.Text; }
        }


        //temp metoda
        //private void AutoFillUsers()
        //{
        //    int brojKorisnika = 1000;
        //    for (int i = 0; i < brojKorisnika; i++)
        //    {
        //        txtIme.Text = i.ToString();
        //        txtPrezime.Text = randomString(11);
        //        txtUlica.Text = randomString(15);
        //        txtGrad.Text = randomString(5);
        //        long  longht = new Random ().Next (11111,99999);
        //        txtPB.Text = longht.ToString();
        //        longht = new Random ().Next (111111,999999);
        //        txtMBG.Text = longht.ToString();
        //        btnDodaj_Click(null, null);
        //        btnCancel.Text = i.ToString();
        //    }
        //    if (odbcConn.State != ConnectionState.Closed)
        //        odbcConn.Close();
        //}
        //public static string randomString(int length)
        //{
        //    string tempString = Guid.NewGuid().ToString().ToLower();
        //    tempString = tempString.Replace("-", "");
        //    while (tempString.Length < length)
        //    {
        //        tempString += tempString;
        //    }
        //    tempString = tempString.Substring(0, length);
        //    return tempString;
        //}
    }
}