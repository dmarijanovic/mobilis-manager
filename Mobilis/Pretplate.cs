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
    public partial class Pretplate : BaseForm 
    {
        OdbcCommand odbcCommand;
        OdbcConnection odbcConn = new System.Data.Odbc.OdbcConnection();
        string ime;
        string prezime;
        int pid;            // pretplatnik ID
        string mbg;
        bool meduspremnik = false;
        public Pretplate(string ime,string prezime, int pid, string mbg)
        {
            InitializeComponent();
            this.ime = ime;
            this.prezime = prezime;
            this.pid = pid;
            this.mbg = mbg;
            lblPretplatnik.Text = ime + " " + prezime + " (" + mbg + ")";
            string connectionString = Program.GetConnString();
            odbcConn = new System.Data.Odbc.OdbcConnection(connectionString);
        }
        private void Pretplate_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            cmbTrajanje.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Now;
            ShowPretplate();
            MobilniUredaji.Prenesi(ref cmbUredaji);
        }
        private void Pretplate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyValue == 83)
            {
                this.Opacity = 0.4;
                e.SuppressKeyPress = true;
                Form frm = (Form)this.Owner;
                frm.Opacity = 0.3;
            }
        }
        private void Pretplate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyValue == 83)
            {
                this.Opacity = 1;
                e.SuppressKeyPress = true;
                Form frm = (Form)this.Owner;
                frm.Opacity = 1;
            }
        }

        private void ShowPretplate()
        {
            OdbcDataReader dr;
            odbcCommand = new OdbcCommand("select * from pretplate where pid = " + pid , odbcConn);
            DateTime pocetak = DateTime.Today;
            DateTime isticeDatum;
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("mobitel", "Mobitel");
            dataGridView1.Columns.Add("created", "Dodano u bazu");
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns.Add("pocetak", "Poèetak pretplate");
            dataGridView1.Columns.Add("trajanje", "Trajenja");
            dataGridView1.Columns["trajanje"].Visible = false;
            dataGridView1.Columns.Add("osnovno_trajanje", "Trajenja");
            dataGridView1.Columns.Add("produljenje_trajanje", "Produljenje");
            dataGridView1.Columns.Add("produljenje_trajanje2", "Produljenje");
            dataGridView1.Columns["produljenje_trajanje2"].Visible = false;
            dataGridView1.Columns.Add("isticedat", "Istièe datum");
            dataGridView1.Columns.Add("isticemje", "Istièe za");
            DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();
            DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();
            DataGridViewButtonColumn col3 = new DataGridViewButtonColumn();
            col1.HeaderText = "Pretplata";
            col1.Name = "produzi";
            col2.HeaderText = "Obriši";
            col2.Name = "obrisi";
            col3.HeaderText = "Istièe";
            col3.Name = "dodaj";
            dataGridView1.Columns.Add(col1);
            dataGridView1.Columns.Add(col2);
            dataGridView1.Columns.Add(col3);
            int pocetakTrajanje;
            int produljenjeTrajanje;
            int zaMjeseci;
            int zaDana;
            Status("Spajam se na bazu...");
            Application.DoEvents();
            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                dr = odbcCommand.ExecuteReader();
                while (dr.Read())
                {
                    DataGridViewRow red = dataGridView1.Rows[dataGridView1.Rows.Add()];
                    red.Cells["produzi"].Value = "Produži";

                    DateTime.TryParse(dr["pocetak"].ToString(), out pocetak);
                    int.TryParse(dr["trajanje"].ToString(), out pocetakTrajanje);
                    int.TryParse(dr["produljenje_trajanje"].ToString(), out produljenjeTrajanje);

                    red.Cells["produljenje_trajanje2"].Value = produljenjeTrajanje;
                    isticeDatum = pocetak.AddMonths(pocetakTrajanje + produljenjeTrajanje); // provjeri

                    DateTime isticeOsnovno = pocetak.AddMonths(pocetakTrajanje);
                    if (DateTime.Compare(DateTime.Today, isticeOsnovno) > 0)
                    {
                        // istekla osnova pretplata
                        isticeOsnovno = isticeOsnovno.AddMonths(produljenjeTrajanje);
                        if (DateTime.Compare(DateTime.Today, isticeOsnovno) > 0)
                        {
                            // pretplata je skroz istekla
                            pocetakTrajanje = 0;
                            produljenjeTrajanje = 0;
                        }
                        else
                        {
                            // pretplata sa starim produzenje jos NIJE istekla
                            // moram omoguciti novo produljenje
                            //pocetakTrajanje += produljenjeTrajanje;
                            produljenjeTrajanje = 0;

                        }
                    }
                    else
                    {
                        // osnovna pretpla NIJE jos istekla                    
                        if (produljenjeTrajanje > 0)
                        {
                            isticeOsnovno = isticeOsnovno.AddMonths(produljenjeTrajanje);
                            if (DateTime.Compare(DateTime.Today, isticeOsnovno) < 0)
                            {
                                // pretplata ni sa produzenjem nije istekla dopusta
                                // mjenjanje zadnje pretplate
                                red.Cells["produzi"].Value = "Ažuriraj";
                            }
                        }
                    }


                    red.Cells["mobitel"].Value = dr["mobitel"].ToString();
                    red.Cells["created"].Value = dr["created"].ToString();
                    red.Cells["pocetak"].Value = pocetak.ToString("MMMM.yyyy");
                    red.Cells["osnovno_trajanje"].Value = dr["osnovno_trajanje"].ToString(); ;
                    red.Cells["trajanje"].Value = pocetakTrajanje;
                    red.Cells["produljenje_trajanje"].Value = produljenjeTrajanje;
                    if (pocetakTrajanje != 0)
                    {
                        red.Cells["isticedat"].Value = isticeDatum.ToString("dd.MMMM.yyyy");
                        PretplataIstice(isticeDatum, out zaMjeseci, out zaDana);
                        red.Cells["isticemje"].Value = zaMjeseci + " mjeseci i " + zaDana + " dana";
                    }
                    else
                    {
                        red.Cells["isticedat"].Value = "Isteklo";
                        red.Cells["isticemje"].Value = "Isteklo";
                    }
                    red.Cells["obrisi"].Value = "Obriši";
                    red.Cells["dodaj"].Value = "Dodaj";
                }
                dr.Close();
                Status("Naredba izvršena uspješno");
            }
            catch { MessageBox.Show("Greška prilikom dobivanja pretplata", "Greška"); Status("Greška prilikom dobivanja pretplata"); }
            finally
            {
                if (odbcConn.State == ConnectionState.Open)
                    odbcConn.Close();
            }
        }
        private void btnAzuriraj_Click(object sender, EventArgs e)
        {
            if (PrijavljenJe)
            {
                bool rez = true;
                if (meduspremnik)
                {
                }
                else
                {
                    if (rdbNovaPretplata.Checked == true || rdbSimpaPrijelaz.Checked == true)
                        rez = NoviZahtjev();
                    else if (rdbProduljenje.Checked == true)
                        AzurirajZahtjev();
                }
                if (rez)
                    this.Close();
            }
        }
        private void btnPonisti_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool NoviZahtjev()
        {
            int rez = 0;
            if (MyValidate())
            {
                string mobitel = txtMobitel.Text.Trim();
                if (!mobitel.StartsWith("09"))
                    mobitel = "09" + mobitel;

                int trajanje = 0;
                if (cmbTrajanje.SelectedIndex > -1)
                    int.TryParse(cmbTrajanje.Items[cmbTrajanje.SelectedIndex].ToString(), out trajanje);

                bool useMyTime;   // pocetak pretplate lokalno ili sa servera
                string myTime = "";
                if (dateTimePicker1.Value.Year != DateTime.Now.Year || dateTimePicker1.Value.Month != DateTime.Now.Month)
                {
                    useMyTime = true;
                    myTime = dateTimePicker1.Value.ToString("yyyy-MM-15");
                }
                else
                    useMyTime = false;

                StringBuilder sb = new StringBuilder();
                sb.Append("insert into pretplate (mobitel, created, pocetak, osnovno_trajanje, trajanje, produljenje_trajanje, izmjena, zid, pid) values ('");
                sb.Append(mobitel);
                sb.Append("', NOW(), ");
                if (useMyTime)
                {
                    sb.Append("'");
                    sb.Append(myTime);                                // trajanje (sa mog kompa)
                    sb.Append("', ");
                }
                else
                    sb.Append("DATE_FORMAT(now(), '%Y-%c-15'), ");  // trajanje (sa servera)
                sb.Append(trajanje);    //  osnovno_trajanje
                sb.Append(", ");
                sb.Append(trajanje);    //  trajanje
                sb.Append(", 0, NOW(), ");       // produljenje_trajanje, izmjena
                sb.Append(base.ZaposlenikID);
                sb.Append(", ");
                sb.Append(pid);
                sb.Append(")");
                odbcCommand = new OdbcCommand(sb.ToString(), odbcConn);
                Status("Spajam se na bazu...");
                try
                {
                    if (odbcConn.State != ConnectionState.Open)
                        odbcConn.Open();
                    rez = odbcCommand.ExecuteNonQuery();
                    if (rez > 0)
                    {
                        MessageBox.Show("Zahtjev dodan u bazu", "Mobilis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Status("Pretplatnik " + ime.ToUpper() + " " + prezime.ToUpper() + " je ažuriran");
                    }
                }
                catch (Exception err) { MessageBox.Show(err.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                finally
                {
                    if (!meduspremnik)
                        DodajInformacije(true);
                    if (odbcConn.State == ConnectionState.Open)
                        odbcConn.Close();
                }
            }
            else
                MessageBox.Show("Molimo popunite sva polja ispravno", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (rez > 0)
                return true;
            else
                return false;
            
        }
        private void AzurirajZahtjev()
        {
            if (MyValidate())
            {
                bool informacijeDodajZahtjev;
                if (rdbProduljenje.Enabled)
                    informacijeDodajZahtjev = true;
                else
                    informacijeDodajZahtjev = false;
                int red;
                if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
                    red = dataGridView1.SelectedCells[0].RowIndex;
                else
                    return;

                DateTime pocetak;
                DateTime.TryParse("15." + dataGridView1.Rows[red].Cells["pocetak"].Value.ToString(), out pocetak);
                int staroTrajanje;
                int.TryParse(dataGridView1.Rows[red].Cells["trajanje"].Value.ToString(), out staroTrajanje);
                int staroProduljenjeTrajanje;
                int.TryParse(dataGridView1.Rows[red].Cells["produljenje_trajanje2"].Value.ToString(), out staroProduljenjeTrajanje);
                //DateTime isticeDatum;
                //DateTime.TryParse(dataGridView1.Rows[red].Cells["isticedat"].Value.ToString(), out isticeDatum);

                string mobitel;
                mobitel = dataGridView1.Rows[red].Cells["mobitel"].Value.ToString();
                int produljenjeTrajanje = 0;
                if (cmbTrajanje.SelectedIndex > -1)
                    int.TryParse(cmbTrajanje.Items[cmbTrajanje.SelectedIndex].ToString(), out produljenjeTrajanje);

                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE pretplate SET ");
                DateTime pocetakPlus = pocetak.AddMonths(staroTrajanje);
                if (DateTime.Compare(DateTime.Today, pocetakPlus) > 0)
                {
                    // istekla osnova pretplata
                    pocetakPlus = pocetakPlus.AddMonths(staroProduljenjeTrajanje);
                    if (DateTime.Compare(DateTime.Today, pocetakPlus) > 0)
                    {
                        // pretplata je skroz istekla
                        if (dateTimePicker1.Value.Year != DateTime.Now.Year || dateTimePicker1.Value.Month != DateTime.Now.Month)
                        {
                            sb.Append("pocetak = '");
                            sb.Append(dateTimePicker1.Value.ToString("yyyy-MM-15"));
                            sb.Append("', ");
                        }
                        else
                            sb.Append("pocetak = DATE_FORMAT(now(), '%Y-%c-15'), ");

                        sb.Append("osnovno_trajanje = ");
                        sb.Append(produljenjeTrajanje);
                        sb.Append(", ");
                        sb.Append("trajanje = ");
                        sb.Append(produljenjeTrajanje);
                        sb.Append(", ");
                        produljenjeTrajanje = 0;
                    }
                    else
                    {
                        // pretplata sa starim produzenje jos NIJE istekla
                        // moram omoguciti novo produljenje
                        staroTrajanje += staroProduljenjeTrajanje;
                        sb.Append(" trajanje = ");
                        sb.Append(staroTrajanje);
                        sb.Append(", ");
                    }
                }
                else
                {
                    // osnovna pretpla NIJE jos istekla
                }

                sb.Append("produljenje_trajanje = ");
                sb.Append(produljenjeTrajanje);
                sb.Append(", izmjena =  NOW() WHERE mobitel = '");
                sb.Append(mobitel);
                sb.Append("' AND pid = ");
                sb.Append(pid);
                odbcCommand = new OdbcCommand(sb.ToString(), odbcConn);
                try
                {
                    Status("Spajam se na bazu...");
                    if (odbcConn.State != ConnectionState.Open)
                        odbcConn.Open();
                    int rez = odbcCommand.ExecuteNonQuery();
                    if (rez > 0)
                    {
                        MessageBox.Show("Produljene pretplate uspjesno");
                        Status("Pretplatnik " + ime.ToUpper() + " " + prezime.ToUpper() + " je ažuriran");
                    }
                    else
                    {
                        MessageBox.Show("Produljene pretplate nije uspjelo");
                        Status("Pretplatnik " + ime.ToUpper() + " " + prezime.ToUpper() + " nije uspješno ažuriran");

                    }
                }
                catch (Exception err) { MessageBox.Show(err.Message, "Greška"); }
                finally
                {
                    DodajInformacije(informacijeDodajZahtjev);
                    if (odbcConn.State == ConnectionState.Open)
                        odbcConn.Close();
                }

            }
            else
                MessageBox.Show("Molimo popunite sva polja ispravno", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void AzurirajZahtjevNeplatise(int mjeseci)
        {
            int red;
            if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
                red = dataGridView1.SelectedCells[0].RowIndex;
            else
                return;
            string mobitel;
            mobitel = dataGridView1.Rows[red].Cells["mobitel"].Value.ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE pretplate SET ");
            sb.Append("pocetak = DATE_ADD(pocetak, INTERVAL ");
            sb.Append(mjeseci);
            sb.Append(" MONTH) ");
            sb.Append("WHERE mobitel = '");
            sb.Append(mobitel);
            sb.Append("' AND pid = ");
            sb.Append(pid);
            odbcCommand = new OdbcCommand(sb.ToString(), odbcConn);
            try
            {
                Status("Spajam se na bazu...");
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                int rez = odbcCommand.ExecuteNonQuery();
                if (rez > 0)
                {
                    MessageBox.Show("Produljene pretplate uspjesno");
                    Status("Pretplatnik " + ime.ToUpper() + " " + prezime.ToUpper() + " je ažuriran");
                }
                else
                {
                    MessageBox.Show("Produljene pretplate nije uspjelo");
                    Status("Pretplatnik " + ime.ToUpper() + " " + prezime.ToUpper() + " nije uspješno ažuriran");

                }
            }
            catch (Exception err) { MessageBox.Show(err.Message, "Greška"); }
            finally
            {
                if (odbcConn.State == ConnectionState.Open)
                    odbcConn.Close();
            }
        }
        private void DodajInformacije(bool dodajZahtjev)
        {
            int mID = 0;
            if (cmbUredaji.Text.Trim() != "")
            {
                mID = MobilniUredaji.GetID(cmbUredaji.Text);
                if (mID == 0 && cmbUredaji.Text.Trim() != "")
                    MessageBox.Show("Ureðaj nije naðen u bazi");
            }
            int zahtjev = 0;
            if (dodajZahtjev)
            {
                if (rdbNovaPretplata.Checked)
                    zahtjev = 1;
                else if (rdbSimpaPrijelaz.Checked)
                    zahtjev = 2;
                else if (rdbProduljenje.Checked)
                    zahtjev = 3;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO informacije (zahtjev, mid, uid, dodano) values (");
            sb.Append(zahtjev);
            sb.Append(", ");
            sb.Append(mID);
            sb.Append(", ");
            sb.Append(ZaposlenikID);
            sb.Append(", NOW()) ");       // produljenje_trajanje, izmjena
            odbcCommand = new OdbcCommand(sb.ToString(), odbcConn);
            Status("Spajam se na bazu...");
            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                int rez = odbcCommand.ExecuteNonQuery();
                if (rez > 0)
                    Status("Informacije dodane u bazu");
                else
                    Status("Informacije nisu dodane u bazu");
            }
            catch { MessageBox.Show("Mobilni ureðaj ili zahtjev nije dodan u bazu informacija, stanje na kraju dana možda neæe biti toèno", "Greška"); Status("Mobilni ureðaj ili zahtjev nije dodan u bazu informacija, stanje na kraju dana možda neæe biti toèno"); }
            finally
            {
                if (odbcConn.State == ConnectionState.Open)
                    odbcConn.Close();
            }
        }
        private bool ObrisiZahtjev()
        {
            int rez = 0;
            int red;
            if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
                red = dataGridView1.SelectedCells[0].RowIndex;
            else
                return false;

            string mobitel;
            mobitel = dataGridView1.Rows[red].Cells["mobitel"].Value.ToString();

            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM pretplate WHERE  mobitel = '");
            sb.Append(mobitel);
            sb.Append("' AND pid = ");
            sb.Append(pid);
            odbcCommand = new OdbcCommand(sb.ToString(), odbcConn);
            try
            {
                Status("Spajam se na bazu...");
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                rez = odbcCommand.ExecuteNonQuery();
                if (rez > 0)
                {
                    MessageBox.Show("Pretplata obrisana");
                    Status("Pretplata obrisana");
                }
                else
                {
                    MessageBox.Show("Pretplata nije obrisana");
                    Status("Pretplata nije obrisana");

                }
            }
            catch (Exception err) { MessageBox.Show(err.Message, "Greška"); }
            finally
            {
                if (odbcConn.State == ConnectionState.Open)
                    odbcConn.Close();
            }
            if (rez > 0)
                return true;
            else
                return false;
        }

        private void PretplataIstice(DateTime istice, out int zaMjeseci, out int zaDana)
        {
            //TODO: malo urediti kod 
            int god = DateTime.Now.Year;
            int mje = DateTime.Now.Month;
            int dan = DateTime.Now.Day;

            int mjeIstice = 0;
            int danDodatni = 0;
            int mjesec = 0;
            for (int god2 = god; god2 <= istice.Year; god2++)
            {
                if (god2 == DateTime.Now.Year && god2 != istice.Year)
                {
                    mje = DateTime.Now.Month;
                    mjeIstice = 12;
                }
                else if (god2 == istice.Year)
                {
                    if (god2 == DateTime.Now.Year)
                        mje = DateTime.Now.Month;
                    else
                        mje = 1;
                    mjeIstice = istice.Month;
                }
                else
                {
                    mje = 1;
                    mjeIstice = 12;
                }
                for (int mje2 = mje; mje2 <= mjeIstice; mje2++)
                {
                    if (god2 == DateTime.Now.Year && mje2 == DateTime.Now.Month)
                    {
                        if (DateTime.Now.Day < 15)
                            danDodatni += 15 - DateTime.Now.Day;
                        else
                            danDodatni += DateTime.DaysInMonth(god2, mje2) - 15;
                    }
                    else if (god2 == istice.Year && mje2 == istice.Month)
                        if (DateTime.Now.Day < 15)
                            mjesec ++;
                        else
                            danDodatni += istice.Day;
                    else
                        mjesec++; 
                }
            }
            zaMjeseci = mjesec;
            zaDana = danDodatni;
        }
        private bool MyValidate()
        {
            int rez = 0;
            if (txtMobitel.Text.Trim() == "" || txtMobitel.Text.Length > 11)
            {
                errorProvider1.SetError(txtMobitel, "Unesite broj mobilnog telefona, maksimalno 11 brojeva");
                rez++;
            }
            else
                errorProvider1.SetError(txtMobitel, "");
            if (cmbUredaji.Text != "")
            {
                if (MobilniUredaji.GetID(cmbUredaji.Text) == 0)
                {
                    rez++;
                    errorProvider1.SetError(cmbUredaji, "Mobilni ureðaj imenom " + cmbUredaji.Text + " nije naðen u bazi, ako niste namjeravali dodati ureðaj ostavite ovo polje prazno");
                }
            }
            else
                errorProvider1.SetError(cmbUredaji, "");
            if (rez == 0)
                return true;
            else
                return false;
        }

        private void stripNoviZahtjev_Click(object sender, EventArgs e)
        {
            grpPrikaz.Visible = false;
            grpZahtjev.Visible = true;
            btnAzuriraj.Visible = true;
            if (e != null)
            {
                rdbNovaPretplata.Enabled = true;
                rdbSimpaPrijelaz.Enabled = true;
                rdbProduljenje.Enabled = false;
                rdbNovaPretplata.Checked = true;
                txtMobitel.Enabled = true;
                txtMobitel.Text = "";
                stripZahtjevDodan.Text = "";
                dateTimePicker1.Enabled = true;
            }
        }
        private void stripPrikaz_Click(object sender, EventArgs e)
        {
            grpPrikaz.Visible = true;
            grpZahtjev.Visible = false;
            btnAzuriraj.Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.RowIndex < dataGridView1.Rows.Count - 1)
            {
                string imeColumne = dataGridView1.Columns[e.ColumnIndex].Name;

                if (imeColumne == "produzi") // azuriraj dugme
                {
                    string mobitel = dataGridView1.Rows[e.RowIndex].Cells["mobitel"].Value.ToString();
                    if (mobitel.StartsWith("09"))
                        mobitel = mobitel.Substring(2, mobitel.Length - 2);
                    txtMobitel.Text = mobitel;
                    txtMobitel.Enabled = false;
                    rdbNovaPretplata.Enabled = false;
                    rdbSimpaPrijelaz.Enabled = false;
                    rdbProduljenje.Enabled = true;
                    rdbProduljenje.Checked = true;
                    rdbProduljenje.Checked = true;
                    if (dataGridView1.Rows[e.RowIndex].Cells["isticedat"].Value.ToString() != "Isteklo")
                    {
                        dateTimePicker1.Enabled = false;
                    }
                    if (dataGridView1.Rows[e.RowIndex].Cells["produljenje_trajanje"].Value.ToString() != "0")
                        rdbProduljenje.Enabled = false;
                    else
                        rdbProduljenje.Enabled = true;

                    DateTime created;
                    DateTime.TryParse(dataGridView1.Rows[e.RowIndex].Cells["created"].Value.ToString(), out created);
                    stripZahtjevDodan.Text = "Dodano u bazu: " + created.ToString("dd.MMMM.yyyy u HH:mm");
                    stripNoviZahtjev_Click(stripNoviZahtjev, null);
                }
                else if (imeColumne == "obrisi") // obrisi dugme
                {
                    if (PrijavljenJe)
                    {
                        InputBox frm = new InputBox();
                        DialogResult rez = frm.ShowDialog();
                        if (rez == DialogResult.OK)
                        {
                            if (ObrisiZahtjev())
                                ShowPretplate();
                        }
                    }
                }
                else if (imeColumne == "dodaj") // produzi pretplatu za x mjeseci zbog trenutno iskljucivanja
                {
                    if (PrijavljenJe)
                    {
                        int mjeseci = 0;
                        InputBox frm = new InputBox("Preduži ugovor za x mjeseci", "U sluèaju ponovog prikljuèenja, produži ugovor");
                        DialogResult rez = frm.ShowDialog(this);
                        if (rez == DialogResult.OK)
                        {
                            int.TryParse(frm.Tekst, out mjeseci);
                            if (mjeseci != 0)
                                AzurirajZahtjevNeplatise(mjeseci);
                            else
                                MessageBox.Show("Neispravan unos mjeseci, pretplata nije ažurirana", "Greška");
                        }
                    }
                }
            }
        }

        private void stripMeduspremnik_Click(object sender, EventArgs e)
        {
            //PreuzimanjeClipboard();
            Meduspremnik frm = new Meduspremnik();
            DialogResult rez = frm.ShowDialog();
            if (rez == DialogResult.OK)
            {
                meduspremnik = true;
                stripNoviZahtjev_Click(null, null);
                txtMobitel.Enabled = false;
                rdbNovaPretplata.Enabled = false;
                rdbProduljenje.Enabled = false;
                rdbSimpaPrijelaz.Enabled = false;
                cmbTrajanje.Enabled = false;
                cmbUredaji.Enabled = false;
                dateTimePicker1.Enabled = false;
                stripNoviZahtjev.Enabled = false;
                stripPrikaz.Enabled = false;
                btnAzuriraj.Text = "&Preuzmi"; 

                TextBoxNumbers txt = null;
                CheckBox chk = null;
                DateTime pocetak = DateTime.Today;
                int trajanje = 0;
                foreach (Control control in frm.panel1.Controls)
                {
                    txt = control as TextBoxNumbers;
                    if (txt != null)    // TextBox
                    {
                        if (txt.Name.StartsWith ("mob"))
                            txtMobitel.Text = txt.Text;
                        else if (txt.Name.StartsWith ("pre"))
                        {
                            int.TryParse(txt.Text, out trajanje);
                            cmbTrajanje.Items.Clear();
                            cmbTrajanje.Items.Add(trajanje);
                            cmbTrajanje.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        chk = control as CheckBox;
                        if (chk != null)
                        {
                            if (DateTime.Today.Day < 15)
                            {
                                if (chk.Checked == true)
                                    pocetak = DateTime.Today;
                                else
                                    pocetak = DateTime.Today.Subtract(new TimeSpan(DateTime.Today.Day + 1, 0, 0, 0));
                            }
                            else
                                pocetak = DateTime.Today;
                            dateTimePicker1.Value = pocetak;
                            NoviZahtjev();
                        }
                    }
                }
                this.Close();
            }
        }
    }
}