using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Xml;

namespace Mobilis
{
    public partial class Form1 : Form
    {
        OdbcCommand odbcCommand;
        OdbcConnection odbcConn = new OdbcConnection();
        DateTime zadnjeOsvjezenje;
        string zaposlenikIme;
        int zaposlenikID;
        int zaposlenikPristup;
        int zaposlenikPoslovnica;
        int redovaPoStranici = 30;
        int trenutnaStranica = -1;
        int queryImaStranica;
        int updateNo = 0;
        string verMinStart;
        byte pagingID = 0;
        System.Collections.ArrayList posloviceID = new System.Collections.ArrayList();
        public Form1()
        {
            InitializeComponent();
            string connectionString = Program.GetConnString();
            odbcConn = new System.Data.Odbc.OdbcConnection(connectionString);

            try
            {
                stripPogledMod.SelectedIndex = Properties.Settings.Default.PogledIndex;
                stripPretragaVrsta.SelectedIndex = Properties.Settings.Default.PretragaIndex;
                this.WindowState = Properties.Settings.Default.MainWindowsState;
            }
            catch { }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            GetZadnjiUpdateBroj();
            PrijaviSe(true);
            timer1.Enabled = true;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyValue == 83)           // control + s, forma transparentna
            {
                this.Opacity = 0.4;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyValue == 27)                    // Esc, minimizira pprogram
            {
                //this.WindowState = FormWindowState.Minimized;
                notifyIcon1.Visible = true;
                this.Hide();
            }
            else if (e.KeyValue == 114)                   // F3, prebacuje na textbox pretragu
            {
                stripPretragaTekst.SelectAll();
                stripPretragaTekst.Focus();
            }
            else if (e.KeyValue == 116)                  // F5, osvjezava pogled
            {
                this.trenutnaStranica = -1;
                stripPogledOsvjezi_Click(null, null);
            }
            else if (e.KeyValue == 117)                // F6, osvjezava pretragu
                stripPretragaOsvjezi_Click(null, null);
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyValue == 83)
            {
                this.Opacity = 1;
                e.SuppressKeyPress = true;
            }
        }

        private void PokaziPretplatnike(byte tip)
        {
            StringBuilder sb = new StringBuilder();
            OdbcDataReader dr;
            string sqlSelect = "SELECT pretplatnici.*, zaposlenici.username AS zaposlenik, poslovnice.ime AS poslovnica ";
            sb.Append("FROM pretplatnici ");
            sb.Append("LEFT JOIN zaposlenici ON pretplatnici.zid = zaposlenici.id ");
            sb.Append("LEFT JOIN poslovnice ON zaposlenici.pid = poslovnice.id ");
            if (tip == 1)   // pokazujem roðendane
            {
                sb.Append("WHERE mbg LIKE DATE_FORMAT(DATE_ADD(NOW(),INTERVAL 2 DAY), '%d%m%') ");
                sb.Append("OR ");
                sb.Append("mbg LIKE DATE_FORMAT(DATE_ADD(NOW(),INTERVAL 1 DAY), '%d%m%') ");
                sb.Append("OR ");
                sb.Append("mbg LIKE DATE_FORMAT(NOW(), '%d%m%') ");
            }
            if (stripPogledZaPoslovnice.SelectedIndex > 0)
            {
                if (tip == 0)
                    sb.Append("WHERE ");
                else if (tip == 1)
                    sb.Append("AND ");
                sb.Append("zaposlenici.pid = ");
                sb.Append(posloviceID[stripPogledZaPoslovnice.SelectedIndex]);

            }
            sb.Append(PokaziPretplatnikeBrojZapisa("SELECT COUNT(pretplatnici.id) " + sb.ToString()));
            odbcCommand = new OdbcCommand(sqlSelect + sb.ToString(), odbcConn);

            toolStripConnStatus.Text = "Spajam se na bazu...";
            Application.DoEvents();
            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                dr = odbcCommand.ExecuteReader();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("ime", "Ime");
                dataGridView1.Columns.Add("prezime", "Prezime");
                dataGridView1.Columns.Add("ulica", "Ulica i broj");
                dataGridView1.Columns.Add("grad", "Grad");
                dataGridView1.Columns.Add("pb", "PB");
                dataGridView1.Columns.Add("mbg", "MB/MBG");
                dataGridView1.Columns.Add("zaposlenik", "Zaposlenik");
                dataGridView1.Columns.Add("poslovnica", "Poslovnica");
                DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();
                DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();
                DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
                col1.HeaderText = "Osobni";
                col1.Name = "azuriraj";
                col2.HeaderText = "Pretplate";
                col2.Name = "pretplate";
                col3.HeaderText = "Napomena";
                col3.Name = "info";
                dataGridView1.Columns.Add(col1);
                dataGridView1.Columns.Add(col2);
                dataGridView1.Columns.Add(col3);
                dataGridView1.Columns.Add("id", "id");
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Rows.Clear();
                while (dr.Read())
                {
                    DataGridViewRow red = dataGridView1.Rows[dataGridView1.Rows.Add()];
                    red.Cells[0].Value = dr["ime"].ToString();
                    red.Cells[1].Value = dr["prezime"].ToString();
                    red.Cells[2].Value = dr["ulica"].ToString();

                    red.Cells[3].Value = dr["grad"].ToString();
                    red.Cells[4].Value = dr["pb"].ToString();
                    red.Cells[5].Value = dr["mbg"].ToString();
                    red.Cells[6].Value = dr["Zaposlenik"].ToString();
                    red.Cells[7].Value = dr["Poslovnica"].ToString();

                    red.Cells[8].Value = "Ažuriraj";
                    red.Cells[9].Value = "Pretplate";
                    red.Cells[10].Value = dr["info"].ToString();
                    red.Cells[11].Value = dr["id"].ToString();
                }
                if(tip == 1)
                    toolStripConnStatus.Text = "Pokazujem roðendane u roku 3 dana";
                else
                    toolStripConnStatus.Text = "Naredba izvršena uspješno";

            }
            catch { MessageBox.Show("Dobivanje liste pretplatnika neuspješno", "Greška"); }
            finally
            {
                if (odbcConn.State == ConnectionState.Open)
                    odbcConn.Close();
            }
        }
        private void PokaziPretplatnikePoDatumu(int tip)
        {
            StringBuilder sb = new StringBuilder();
            OdbcDataReader dr;
            int addMjeseci = stripPogledMod.SelectedIndex;
            //selectQueria = "SELECT SQL_CALC_FOUND_ROWS pretplatnici.*, zaposlenici.username AS zaposlenik, poslovnice.ime AS poslovnica ";
            sb.Append("SELECT ");
            if (this.trenutnaStranica == -1)
                sb.Append("SQL_CALC_FOUND_ROWS ");
            sb.Append("pretplatnici.*, zaposlenici.username AS zaposlenik, poslovnice.ime AS poslovnica ");
            sb.Append("FROM pretplatnici ");
            sb.Append("LEFT JOIN zaposlenici ON pretplatnici.zid = zaposlenici.id ");
            sb.Append("LEFT JOIN poslovnice ON zaposlenici.pid = poslovnice.id ");
            sb.Append("INNER JOIN pretplate ON pretplate.pid = pretplatnici.id ");
            sb.Append("WHERE ");
            sb.Append("DATE_ADD(pretplate.pocetak, INTERVAL pretplate.trajanje + pretplate.produljenje_trajanje MONTH) ");
            if (tip == 0)
            {

                sb.Append("BETWEEN DATE_FORMAT(now(), '%Y-%c-15') AND ");
                sb.Append("DATE_ADD(DATE_FORMAT(now(), '%Y-%c-15'), INTERVAL ");
                sb.Append(addMjeseci);
                sb.Append(" MONTH) ");
            }
            else if (tip == 1)
                sb.Append(" <= NOW() ");
            if (stripPogledZaPoslovnice.SelectedIndex > 0)
            {
                sb.Append("AND zaposlenici.pid = ");
                sb.Append(posloviceID[stripPogledZaPoslovnice.SelectedIndex]);
                sb.Append(" ");
            }            
            sb.Append ("GROUP BY pretplatnici.id ");
            sb.Append("LIMIT ");
            if (this.trenutnaStranica != -1)
            {
                sb.Append(trenutnaStranica * redovaPoStranici);
                sb.Append(", ");
                sb.Append(redovaPoStranici);
            }
            else
                sb.Append(redovaPoStranici);

            //sb.Append(PokaziPretplatnikeBrojZapisa("SELECT COUNT(pretplatnici.id) " + sb.ToString()));
            odbcCommand = new OdbcCommand(sb.ToString(), odbcConn);
            toolStripConnStatus.Text = "Spajam se na bazu...";
            Application.DoEvents();

            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                dr = odbcCommand.ExecuteReader();
                if (this.trenutnaStranica == -1)
                    PokaziPretplatnikeBrojZapisa();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("ime", "Ime");
                dataGridView1.Columns.Add("prezime", "Prezime");
                dataGridView1.Columns.Add("ulica", "Ulica i broj");
                dataGridView1.Columns.Add("grad", "Grad");
                dataGridView1.Columns.Add("pb", "PB");
                dataGridView1.Columns.Add("mbg", "MB/MBG");
                dataGridView1.Columns.Add("zaposlenik", "Zaposlenik");
                dataGridView1.Columns.Add("poslovnica", "Poslovnica");
                DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();
                DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();
                DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
                col1.HeaderText = "Osobni";
                col1.Name = "azuriraj";
                col2.HeaderText = "Pretplate";
                col2.Name = "pretplate";
                col3.HeaderText = "Napomena";
                col3.Name = "info";
                dataGridView1.Columns.Add(col1);
                dataGridView1.Columns.Add(col2);
                dataGridView1.Columns.Add(col3);
                dataGridView1.Columns.Add("id", "id");
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Rows.Clear();
                while (dr.Read())
                {
                    DataGridViewRow red = dataGridView1.Rows[dataGridView1.Rows.Add()];
                    red.Cells[0].Value = dr["ime"].ToString();
                    red.Cells[1].Value = dr["prezime"].ToString();
                    red.Cells[2].Value = dr["ulica"].ToString();

                    red.Cells[3].Value = dr["grad"].ToString();
                    red.Cells[4].Value = dr["pb"].ToString();
                    red.Cells[5].Value = dr["mbg"].ToString();
                    red.Cells[6].Value = dr["zaposlenik"].ToString();
                    red.Cells[7].Value = dr["poslovnica"].ToString();

                    red.Cells[8].Value = "Ažuriraj";
                    red.Cells[9].Value = "Pretplate";
                    red.Cells[10].Value = dr["info"].ToString();
                    red.Cells[11].Value = dr["id"].ToString();
                }
                toolStripConnStatus.Text = "Naredba izvršena uspješno";
            }
            catch { MessageBox.Show("Dobivanje liste pretplatnika neuspješno", "Greška"); }
            finally
            {
                if (odbcConn.State == ConnectionState.Open)
                    odbcConn.Close();
            }
        }
        private void PokaziPretraga()
        {
            //Ime
            //Prezime
            //Ime i prezime
            //Grad
            //Ulica i kuèni broj
            //JMBG
            //Mobilni broj
            //Napomene
            int odabir = stripPretragaVrsta.SelectedIndex;
            StringBuilder sb = new StringBuilder();
            string selectQueria;
            OdbcDataReader dr;
            selectQueria = "SELECT pretplatnici.*, zaposlenici.username AS zaposlenik, poslovnice.ime AS poslovnica ";
            sb.Append("FROM pretplatnici ");
            sb.Append("LEFT JOIN zaposlenici ON pretplatnici.zid = zaposlenici.id ");
            sb.Append("LEFT JOIN poslovnice ON zaposlenici.pid = poslovnice.id ");
            if (odabir == 6)       // mobilni broj
            {

                sb.Append ("INNER JOIN pretplate ON pretplatnici.id = pretplate.pid WHERE ");
                sb.Append("pretplate.mobitel LIKE '");
                sb.Append(stripPretragaTekst.Text.Trim());
                sb.Append("%' ");
                if (stripPogledZaPoslovnice.SelectedIndex > 0)
                {
                    sb.Append("AND zaposlenici.pid = ");
                    sb.Append(posloviceID[stripPogledZaPoslovnice.SelectedIndex]);
                    sb.Append(" ");
                }            
                sb.Append ("GROUP BY pretplatnici.id");
            }
            else
            {
                sb.Append("WHERE ");
                if (odabir == 0)
                {
                    sb.Append("pretplatnici.ime = '");
                    sb.Append(stripPretragaTekst.Text.Trim());
                    sb.Append("' ");
                }
                else if (odabir == 1)
                {
                    sb.Append("pretplatnici.prezime = '");
                    sb.Append(stripPretragaTekst.Text.Trim());
                    sb.Append("' ");
                }
                else if (odabir == 2)
                {
                    int indexOf = stripPretragaTekst.Text.IndexOf(" ");
                    string ime = stripPretragaTekst.Text.Substring(0, indexOf);
                    string prezime = stripPretragaTekst.Text.Substring(indexOf, stripPretragaTekst.Text.Length - indexOf);
                    sb.Append("pretplatnici.ime = '");
                    sb.Append(ime.Trim());
                    sb.Append("' AND pretplatnici.prezime = '");
                    sb.Append(prezime.Trim());
                    sb.Append("' ");
                }
                else if (odabir == 3)
                {
                    sb.Append("pretplatnici.grad = '");
                    sb.Append(stripPretragaTekst.Text.Trim());
                    sb.Append("' ");
                }
                else if (odabir == 4)
                {
                    sb.Append("pretplatnici.ulica LIKE '%");
                    sb.Append(stripPretragaTekst.Text.Trim());
                    sb.Append("%' ");
                }
                else if (odabir == 5)
                {
                    sb.Append("pretplatnici.mbg LIKE '");
                    sb.Append(stripPretragaTekst.Text.Trim());
                    sb.Append("%' ");
                }
                else if (odabir == 7)       // napomene
                {
                    sb.Append("pretplatnici.info LIKE '%");
                    sb.Append(stripPretragaTekst.Text.Trim());
                    sb.Append("%' ");
                }
                if (stripPogledZaPoslovnice.SelectedIndex > 0)
                {
                    sb.Append("AND zaposlenici.pid = ");
                    sb.Append(posloviceID[stripPogledZaPoslovnice.SelectedIndex]);
                    sb.Append(" ");
                }            
            }

            sb.Append(PokaziPretplatnikeBrojZapisa("SELECT COUNT(pretplatnici.id) " + sb.ToString()));
            odbcCommand = new OdbcCommand(selectQueria + sb.ToString(), odbcConn);
            toolStripConnStatus.Text = "Spajam se na bazu...";
            Application.DoEvents();

            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                dr = odbcCommand.ExecuteReader();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("ime", "Ime");
                dataGridView1.Columns.Add("prezime", "Prezime");
                dataGridView1.Columns.Add("ulica", "Ulica i broj");
                dataGridView1.Columns.Add("grad", "Grad");
                dataGridView1.Columns.Add("pb", "PB");
                dataGridView1.Columns.Add("mbg", "MB/MBG");
                dataGridView1.Columns.Add("zaposlenik", "Zaposlenik");
                dataGridView1.Columns.Add("poslovnica", "Poslovnica");
                DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();
                DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();
                DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
                col1.HeaderText = "Osobni";
                col1.Name = "azuriraj";
                col2.HeaderText = "Pretplate";
                col2.Name = "pretplate";
                col3.HeaderText = "Napomena";
                col3.Name = "info";
                dataGridView1.Columns.Add(col1);
                dataGridView1.Columns.Add(col2);
                dataGridView1.Columns.Add(col3);
                dataGridView1.Columns.Add("id", "id");
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Rows.Clear();
                while (dr.Read())
                {
                    DataGridViewRow red = dataGridView1.Rows[dataGridView1.Rows.Add()];
                    red.Cells[0].Value = dr["ime"].ToString();
                    red.Cells[1].Value = dr["prezime"].ToString();
                    red.Cells[2].Value = dr["ulica"].ToString();

                    red.Cells[3].Value = dr["grad"].ToString();
                    red.Cells[4].Value = dr["pb"].ToString();
                    red.Cells[5].Value = dr["mbg"].ToString();
                    red.Cells[6].Value = dr["zaposlenik"].ToString();
                    red.Cells[7].Value = dr["poslovnica"].ToString();

                    red.Cells[8].Value = "Ažuriraj";
                    red.Cells[9].Value = "Pretplate";
                    red.Cells[10].Value = dr["info"].ToString();
                    red.Cells[11].Value = dr["id"].ToString();
                }
                toolStripConnStatus.Text = "Naredba izvršena uspješno";
            }
            catch { MessageBox.Show("Dobivanje liste pretplatnika neuspješno", "Greška"); }
            finally
            {
                if (odbcConn.State == ConnectionState.Open)
                    odbcConn.Close();
            }
        }
        private string PokaziPretplatnikeBrojZapisa(string query)
        {
            string limit = "";
            if (this.trenutnaStranica == -1)
            {
                this.trenutnaStranica = 0;
                int brojZapisa = 0;
                int brojStranica = 0;
                object rez;
                odbcCommand = new OdbcCommand(query, odbcConn);
                stripIdiNaStranicu.Items.Clear();
                try
                {
                    if (odbcConn.State != ConnectionState.Open)
                        odbcConn.Open();
                    rez = odbcCommand.ExecuteScalar();
                    if (rez != null)
                        int.TryParse(rez.ToString(), out brojZapisa);
                    else
                        brojZapisa = 0;
                    if (brojZapisa > redovaPoStranici)
                    {
                        brojStranica = brojZapisa / redovaPoStranici;
                        if (brojZapisa % redovaPoStranici != 0)
                            brojStranica++;
                        limit = " LIMIT " + (trenutnaStranica * redovaPoStranici) + ", " + redovaPoStranici;
                    }
                    else
                        brojStranica = 1;
                    ToolStripItem ithem;
                    for (int i = 0; i < brojStranica; i++)
                    {
                        if (i <= 29)
                        {
                            ithem = new ToolStripButton("Stranica: " + (i + 1));
                            ithem.AutoToolTip = false;
                            ithem.Name = Convert.ToString(i);
                            ithem.Click += new EventHandler(stripIdiNaStranicu_Click);
                            stripIdiNaStranicu.Items.Add(ithem);
                        }
                        else
                        {
                            ithem = new ToolStripButton("Idi na...");
                            ithem.AutoToolTip = false;
                            ithem.Name = Convert.ToString(i);
                            ithem.Click += new EventHandler(stripIdiNaStranicu_Click);
                            stripIdiNaStranicu.Items.Add(ithem);
                            break;
                        }

                    }
                    queryImaStranica = brojStranica;
                }
                catch { limit = " LIMIT 0,20"; MessageBox.Show("Dobivanje broja zapise nije uspjelo", "Greška"); }
                finally
                {
                    //if (odbcConn.State == ConnectionState.Open)
                    //    odbcConn.Close();
                }
            }
            else
                limit = " LIMIT " + (trenutnaStranica * redovaPoStranici) + ", " + redovaPoStranici;
            stripStranicaInfo.Text = "Stranica:  " + (trenutnaStranica + 1) + " od " + queryImaStranica;
            return limit;
        }
        private string PokaziPretplatnikeBrojZapisa()
        {
            string limit = "";
            if (this.trenutnaStranica == -1)
            {
                this.trenutnaStranica = 0;
                int brojZapisa = 0;
                int brojStranica = 0;
                object rez;
                odbcCommand = new OdbcCommand("SELECT FOUND_ROWS()", odbcConn);
                stripIdiNaStranicu.Items.Clear();
                try
                {
                    //if (odbcConn.State != ConnectionState.Open)
                    //    odbcConn.Open();
                    rez = odbcCommand.ExecuteScalar();
                    if (rez != null)
                        int.TryParse(rez.ToString(), out brojZapisa);
                    else
                        brojZapisa = 0;
                    if (brojZapisa > redovaPoStranici)
                    {
                        brojStranica = brojZapisa / redovaPoStranici;
                        if (brojZapisa % redovaPoStranici != 0)
                            brojStranica++;
                        limit = " LIMIT " + (trenutnaStranica * redovaPoStranici) + ", " + redovaPoStranici;
                    }
                    else
                        brojStranica = 1;
                    ToolStripItem ithem;
                    for (int i = 0; i < brojStranica; i++)
                    {
                        if (i <= 29)
                        {
                            ithem = new ToolStripButton("Stranica: " + (i + 1));
                            ithem.AutoToolTip = false;
                            ithem.Name = Convert.ToString(i);
                            ithem.Click += new EventHandler(stripIdiNaStranicu_Click);
                            stripIdiNaStranicu.Items.Add(ithem);
                        }
                        else
                        {
                            ithem = new ToolStripButton("Idi na...");
                            ithem.AutoToolTip = false;
                            ithem.Name = Convert.ToString(i);
                            ithem.Click += new EventHandler(stripIdiNaStranicu_Click);
                            stripIdiNaStranicu.Items.Add(ithem);
                            break;
                        }

                    }
                    queryImaStranica = brojStranica;
                }
                catch { limit = " LIMIT 0,20"; MessageBox.Show("Dobivanje broja zapise nije uspjelo", "Greška"); }
                finally
                {
                    //if (odbcConn.State == ConnectionState.Open)
                    //    odbcConn.Close();
                }
            }
            else
                limit = " LIMIT " + (trenutnaStranica * redovaPoStranici) + ", " + redovaPoStranici;
            stripStranicaInfo.Text = "Stranica:  " + (trenutnaStranica + 1) + " od " + queryImaStranica;
            return limit;
        }

        private void PokaziInformacije(DateTime start, DateTime end, int tip, string sqlIN, string imena)
        {
            // tip(0) = zahtjebi, tip(1) = mobiteli
            OdbcDataReader dr;
            StringBuilder sb = new StringBuilder();
            string limit;
            limit = ""; //PokaziPretplatnikeBrojZapisa("SELECT COUNT(id) FROM pretplatnici");
            sb.Append("SELECT *, ");
            if (tip == 0)
                sb.Append("COUNT(zahtjev) as zbroj ");
            else if (tip == 1)
                sb.Append("COUNT(mid) as zbroj ");
            sb.Append("FROM informacije WHERE dodano BETWEEN '");
            sb.Append(start.ToString("yyyy-MM-dd"));
            sb.Append(" 00:00:00' AND '");
            sb.Append(end.ToString("yyyy-MM-dd"));
            sb.Append(" 23:59:59' ");
            if (tip == 0)
                sb.Append("AND zahtjev > 0 ");
            else if (tip == 1)
                sb.Append("AND mid > 0 ");

            if (sqlIN.IndexOf(",") > -1)   // ima vise korisnika korisiti IN naredbu
            {
                sb.Append(" AND uid IN(");
                sb.Append(sqlIN);
                sb.Append(") ");
            }
            else
            {
                sb.Append(" AND uid =");
                sb.Append(sqlIN);
                sb.Append(" ");
            }
            sb.Append("GROUP BY ");
            if (tip == 0)
                sb.Append("zahtjev");
            else if (tip == 1)
                sb.Append("mid");


            odbcCommand = new OdbcCommand(sb.ToString() + limit, odbcConn);
            toolStripConnStatus.Text = "Spajam se na bazu...";
            Application.DoEvents();
            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                dr = odbcCommand.ExecuteReader();
                dataGridView1.Columns.Clear();
                if (tip == 0)
                    dataGridView1.Columns.Add("zahtjev", "Zahtjev");
                else if (tip == 1)
                    dataGridView1.Columns.Add("mobitel", "Mobitel");

                dataGridView1.Columns.Add("zbroj", "Ukupno");
                dataGridView1.Columns.Add("zaposlenik", "Zaposlenik");
                DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();
                col1.HeaderText = "Pokazi";
                col1.Name = "pokazi";
                dataGridView1.Columns.Add(col1);
                dataGridView1.Rows.Clear();
                int zahtjev;
                int mobitelID;
                while (dr.Read())
                {
                    DataGridViewRow red = dataGridView1.Rows[dataGridView1.Rows.Add()];
                    if (tip == 0)
                    {
                        int.TryParse(dr["zahtjev"].ToString(), out zahtjev);
                        if (zahtjev == 1)
                            red.Cells["zahtjev"].Value = "Zahtjev za zasnivanje pretplatnièkog odnosa";
                        else if (zahtjev == 2)
                            red.Cells["zahtjev"].Value = "Zahtjev za Simpa prijelaz";
                        else if (zahtjev == 3)
                            red.Cells["zahtjev"].Value = "Zahtjev za produljenje obaveznog trajanja pretplatnièkog odnosa";
                    }
                    else if (tip == 1)
                    {
                        int.TryParse(dr["mid"].ToString(), out mobitelID);
                        red.Cells["mobitel"].Value = MobilniUredaji.GetIme(mobitelID);
                    }
                    red.Cells["zbroj"].Value = dr["zbroj"].ToString();
                    red.Cells["zaposlenik"].Value = imena;
                    red.Cells["pokazi"].Value = "Pokaži";
                }
                toolStripConnStatus.Text = "Naredba izvršena uspješno";
            }
            catch { MessageBox.Show("Dobivanje liste pretplatnika neuspješno", "Greška"); }
            finally
            {
                if (odbcConn.State == ConnectionState.Open)
                    odbcConn.Close();
            }
            toolStripConnStatus.Text = "Pokazujem od " + start.ToString("dd.MMMM.yyyy") + " do " + end.ToString("dd.MMMM.yyyy");
        }
        private void PokaziInformacijeUkupno(int tip)
        {
            // tip = 3, pretplata po zaposlenicima
            StringBuilder sb = new StringBuilder();
            OdbcDataReader dr;
            string limit;
            sb.Append("SELECT ");
            if (tip == 3 || tip == 4)
                sb.Append("zaposlenici.username AS ime, ");
            if (tip == 1 || tip == 3)
            {
                sb.Append("COUNT(pretplatnici.id) AS ukupno, poslovnice.ime AS poslovnica FROM pretplatnici ");
                sb.Append("LEFT JOIN zaposlenici ON zaposlenici.id = pretplatnici.zid ");
            }
            else if (tip == 2 || tip == 4)
            {
                sb.Append("COUNT(pretplate.id) AS ukupno, poslovnice.ime AS poslovnica FROM pretplate ");
                sb.Append("LEFT JOIN zaposlenici ON zaposlenici.id = pretplate.zid ");
            }
            sb.Append("LEFT JOIN poslovnice ON zaposlenici.pid = poslovnice.id ");

            if (tip == 1 || tip == 2)
                sb.Append("GROUP BY poslovnice.id ");
            else if (tip == 3 || tip == 4)
                sb.Append("GROUP BY zaposlenici.id ");

            limit = ""; // PokaziPretplatnikeBrojZapisa("SELECT COUNT(pretplatnici.id) FROM pretplatnici LEFT JOIN zaposlenici ON pretplatnici.zid = zaposlenici.id LEFT JOIN poslovnice ON zaposlenici.pid = poslovnice.id");
            odbcCommand = new OdbcCommand(sb.ToString() + limit, odbcConn);
            dataGridView1.Columns.Clear();


            if (tip == 3 || tip == 4)
                dataGridView1.Columns.Add("ime", "Ime");
            dataGridView1.Columns.Add("poslovnica", "Poslovnica");

            if (tip == 1 || tip == 3)
                dataGridView1.Columns.Add("ukupno", "Ukupno pretplatnika");
            else if (tip == 2 || tip == 4)
                dataGridView1.Columns.Add("ukupno", "Ukupno pretplata");
            dataGridView1.Rows.Clear();
            toolStripConnStatus.Text = "Spajam se na bazu...";
            Application.DoEvents();
            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                dr = odbcCommand.ExecuteReader();
                while (dr.Read())
                {
                    DataGridViewRow red = dataGridView1.Rows[dataGridView1.Rows.Add()];
                    if (tip == 3 || tip == 4)
                        red.Cells["ime"].Value = dr["ime"].ToString();
                   
                    red.Cells["poslovnica"].Value = dr["poslovnica"].ToString();
                    red.Cells["ukupno"].Value = dr["ukupno"].ToString();

                }
                toolStripConnStatus.Text = "Naredba izvršena uspješno";
            }
            //catch { MessageBox.Show("Dobivanje liste pretplatnika neuspješno", "Greška"); }
            finally
            {
                if (odbcConn.State == ConnectionState.Open)
                    odbcConn.Close();
            }
        }
        private void GetNetPostavke()
        {
            OdbcDataReader dr;
            odbcCommand = new OdbcCommand("SELECT * FROM postavke", odbcConn);
            toolStripConnStatus.Text = "Spajam se na bazu...";
            Application.DoEvents();
            int netUpdateNo;
            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                dr = odbcCommand.ExecuteReader();

                while (dr.Read())
                {
                    if (dr["ime"].ToString() == "updno")
                    {
                        int.TryParse(dr["vrijednost"].ToString(), out netUpdateNo);
                        if (netUpdateNo > updateNo)
                            stripStatusNoviUpdate.Visible = true;
                    }
                    else if (dr["ime"].ToString() == "minstart")
                    {
                        verMinStart = dr["vrijednost"].ToString();
                    }
                    else if (dr["ime"].ToString() == "poslovnice")
                    {
                        ParsePoslovnice(dr["vrijednost"].ToString());
                    }

                }
                toolStripConnStatus.Text = "Naredba izvršena uspješno";
            }
            catch { MessageBox.Show("Dobivanje net postavki nije uspjelo", "Greška"); }
            finally
            {
                //if (odbcConn.State == ConnectionState.Open)
                //    odbcConn.Close();
            }
        }
        private void ParsePoslovnice(string data)
        {
            stripPogledZaPoslovnice.Items.Clear();
            posloviceID.Clear();
            posloviceID.Add("0");
            stripPogledZaPoslovnice.Items.Add("Sve poslovnice");
            string[] poslovnice = data.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (poslovnice.Length > 0)
            {
                string[] odvoji;
                for (int i = 0; i < poslovnice.Length; i++)
                {
                    odvoji = poslovnice[i].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (odvoji.Length > 0)
                    {
                        stripPogledZaPoslovnice.Items.Add(odvoji[0]);
                        posloviceID.Add(odvoji[1]);
                        if (Properties.Settings.Default.PogledZaPoslovnice == i)
                            stripPogledZaPoslovnice.SelectedIndex = i;
                    }
                }
            }
        }

        private void GetZadnjiUpdateBroj()
        {
            string putanja = Application.StartupPath;
            if (!putanja.EndsWith("\\"))
                putanja += "\\";
            putanja += "upd.xml";
            if (!File.Exists(putanja))
                return;


            XmlTextReader reader = new XmlTextReader(putanja);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Update")
                {
                    int.TryParse(reader.GetAttribute("verzija"), out updateNo);
                    break;
                }
            }
            reader.Close();
        }

        private void PrijaviSe(bool useLocal)
        {
            if (useLocal)
            {
                string[] podaci = Properties.Settings.Default.Zaposlenik.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (podaci.Length == 4)
                {
                    zaposlenikIme = podaci[0];
                    int.TryParse(podaci[1], out zaposlenikID);
                    int.TryParse(podaci[2], out zaposlenikPristup);
                    int.TryParse(podaci[3], out zaposlenikPoslovnica);
                }
            }
            else
            {
                Prijava frm = new Prijava();
                DialogResult rez = frm.ShowDialog(this);
                if (rez == DialogResult.OK)
                {
                    if (frm.IDZaposlenika > 0)
                    {
                        zaposlenikID = frm.IDZaposlenika;
                        zaposlenikIme = frm.Ime;
                        zaposlenikPristup = frm.Pristup;
                        zaposlenikPoslovnica = frm.Poslovnica;
                        Properties.Settings.Default.Zaposlenik = zaposlenikIme + ";" + zaposlenikID + ";" + zaposlenikPristup + ";" + zaposlenikPoslovnica;
                        Properties.Settings.Default.Save();
                    }
                }
            }
            if (zaposlenikID > 0)
            {
                stripOdjavaPrijava.Text = "&Odjavi se";
                stripImePrijavljenog.Text = "Prijavljen pod: " + zaposlenikIme;
            }
        }

        #region PadajuciIzbornik
        // glavni padajuci izbornik
        private void stripOdjavaPrijava_Click(object sender, EventArgs e)
        {
            if (zaposlenikID == 0)
            {
                PrijaviSe(false);
            }
            else
            {
                DialogResult rez = MessageBox.Show("Potvrdi odjavu", "Odjava", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (rez == DialogResult.Yes)
                {
                    zaposlenikID = 0;
                    zaposlenikIme = "";
                    zaposlenikPristup = 0;
                    stripOdjavaPrijava.Text = "&Prijavi se";
                    stripImePrijavljenog.Text = "Niste prijavljeni";
                    toolStripConnStatus.Text = "Odjava uspješna";
                    Properties.Settings.Default.Zaposlenik = "";
                    Properties.Settings.Default.Save();
                }
            }
        }
        private void stripIzlaz_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void stripIzlazSaOdjavom_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Zaposlenik = "";
            Properties.Settings.Default.Save();
            Application.Exit();
        }
        private void stripInformacijePrikazi_Click(object sender, EventArgs e)
        {
            int tip;
            ToolStripMenuItem ithem = (ToolStripMenuItem)sender;
            if (ithem.Name == "stripInformacijeZahtjevi")
                tip = 0;
            else
                tip = 1;
            Informacije frm = new Informacije();
            DialogResult rez = frm.ShowDialog(this);
            if (rez == DialogResult.OK)
            {
                string sqlListaID;
                string sqlListaImena;
                frm.ZaposlenikSQLINIDLista(out sqlListaID, out sqlListaImena);
                PokaziInformacije(frm.Pocetak, frm.Kraj, tip, sqlListaID, sqlListaImena);
            }

        }
        private void stripStatistikaPo_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem odabir = (ToolStripMenuItem)sender;
            int iOdabir;
            int.TryParse(odabir.Tag.ToString(), out iOdabir);
            PokaziInformacijeUkupno(iOdabir);
        }
        private void stripPoslovnice_Click(object sender, EventArgs e)
        {
            Form frm = new Poslovnice();
            frm.ShowDialog(this);

        }
        private void stripZaposlenici_Click(object sender, EventArgs e)
        {
            Form frm = new Zaposlenici();
            frm.ShowDialog(this);
        }
        private void stripMobiteli_Click(object sender, EventArgs e)
        {
            Mobiteli frm = new Mobiteli();
            frm.ShowDialog(this);
        }
        private void stripOprogramu_Click(object sender, EventArgs e)
        {
            Oprogramu frm = new Oprogramu();
            frm.ShowDialog();
        }
        private void stripUpdate_Click(object sender, EventArgs e)
        {
            DialogResult rez = MessageBox.Show("Provjeri dostupnost novih komponenti programa, pritiskom na dugme OK glavni program æe se zatvoriti i pokreniti AutoUpdate program koji æe automatski preuzeti i ažurirati sve dostupne komponente, tijekom rada AutoUpdate programa nemojte pokreæati glavnu aplikaciju dok nedobijete poruku ili se ona sama ne pokrene", "Pokreni AutoUpdate", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (rez == DialogResult.OK)
            {
                string putanja = Application.StartupPath;
                if (!putanja.EndsWith("\\"))
                    putanja += "\\";
                putanja += "AutoUpdate.exe";
                if (File.Exists(putanja))
                {
                    System.Diagnostics.Process process = System.Diagnostics.Process.Start(putanja);
                    if (process != null)
                        Application.Exit();
                }
                else
                    MessageBox.Show("AutoUpdate nije pronaðen", "Mobilis");
            }
        }
        private void stripSiteMobilis_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://wwww.mobilis.hr", null);
            }
            catch { }
        }
        private void stripSiteThis_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.mobilis.hr/stranice/mobilisprogram/", null);
            }
            catch { }
        }
        private void stripPomoc_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.mobilis.hr/stranice/mobilisprogrampomoc/", null);
            }
            catch { }
        }        
        #endregion

        #region OstaliIzbornici
        // Ostali strip izbornici
        private void stripIdiNaStranicu_Click(object sender, EventArgs e)
        {
            ToolStripItem ithem = (ToolStripItem)sender;
            int.TryParse(ithem.Name, out this.trenutnaStranica);
            if (this.trenutnaStranica >= 30)
            {
                InputBox frm = new InputBox("Idi na stranicu", "Unesi broj stranice");
                DialogResult rez = frm.ShowDialog(this);
                if (rez == DialogResult.OK)
                {
                    int odabranaStranica;
                    int.TryParse(frm.Tekst, out odabranaStranica);
                    if (odabranaStranica > 0 && odabranaStranica <= this.queryImaStranica)
                        this.trenutnaStranica = odabranaStranica - 1;
                    else
                        return;
                }
                else
                    return;
            }
            if (pagingID == 0)
                stripPogledOsvjezi_Click(null, null);
            else if (pagingID == 1)
                stripPretragaOsvjezi_Click(null, null);
        }
        private void stripStranicaSljedeca_ButtonClick(object sender, EventArgs e)
        {
            if (this.trenutnaStranica + 1 < this.queryImaStranica)
            {
                this.trenutnaStranica++;
                if (pagingID == 0)
                    stripPogledOsvjezi_Click(null, null);
                else if (pagingID == 1)
                    stripPretragaOsvjezi_Click(null, null);
            }
        }
        private void stripPretplatnici_Click(object sender, EventArgs e)
        {
            Pretplatink frm = new Pretplatink();
            DialogResult rez = frm.ShowDialog(this);
            if (rez == DialogResult.OK && frm.PretplatnikID > 0)
            {
                Form frmPretplate = new Pretplate(frm.Ime, frm.Prezime, frm.PretplatnikID, frm.MBG);
                frmPretplate.ShowDialog(this);
            }
        }
        private void stripPretragaTekst_Enter(object sender, EventArgs e)
        {
            stripPretragaTekst.SelectAll();
        }
        private void stripPretragaTekst_Click(object sender, EventArgs e)
        {
            stripPretragaTekst.SelectAll();
        }
        private void stripPretragaTekst_OnKeyEnter()
        {
            if (stripPretragaTekst.Text.Trim() != "")
                stripPretragaOsvjezi_Click(stripPretragaOsvjezi, null);
        }
        private void stripPretragaTekst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)   // key up
            {
                if (stripPretragaVrsta.SelectedIndex > 0)
                    stripPretragaVrsta.SelectedIndex--;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyValue == 40)  // key down
            {
                if (stripPretragaVrsta.SelectedIndex + 1 < stripPretragaVrsta.Items.Count)
                    stripPretragaVrsta.SelectedIndex++;
                e.SuppressKeyPress = true;
            }
        }
        private void stripPogledOsvjezi_Click(object sender, EventArgs e)
        {
            try
            {
                TimeSpan razlika = DateTime.Now.Subtract(zadnjeOsvjezenje);
                TimeSpan min = new TimeSpan(0, 0, 5);
                if (razlika.CompareTo(min) == 1)
                {
                    pagingID = 0;
                    zadnjeOsvjezenje = DateTime.Now;
                    stripPogledOsvjezi.Enabled = false;
                    stripPretragaOsvjezi.Enabled = false;
                    stripStranicaSljedeca.Enabled = false;
                    if (sender != null)
                        this.trenutnaStranica = -1;
                    if (stripPogledMod.SelectedIndex == 0)      // pokazi sve pretplatnike
                        PokaziPretplatnike(0);
                    else if (stripPogledMod.SelectedIndex == 13)     // pokazi rodendane
                        PokaziPretplatnikePoDatumu(1);
                    else if (stripPogledMod.SelectedIndex == 14)     // pokazi rodendane
                        PokaziPretplatnike(1);
                    else if (stripPogledMod.SelectedIndex >= 1 && stripPogledMod.SelectedIndex <= 12)
                        PokaziPretplatnikePoDatumu(0);
                    stripPogledOsvjezi.Enabled = true;
                    stripPretragaOsvjezi.Enabled = true;
                    stripStranicaSljedeca.Enabled = true;

               }
               else
                   toolStripConnStatus.Text = "5 sekundi izmeðu osvježenja";
           }
           catch { }
        }
        private void stripPretragaOsvjezi_Click(object sender, EventArgs e)
        {
            if (stripPretragaTekst.Text.Trim() != "")
            {
                TimeSpan razlika = DateTime.Now.Subtract(zadnjeOsvjezenje);
                TimeSpan min = new TimeSpan(0, 0, 5);
                if (razlika.CompareTo(min) == 1)
                {
                    pagingID = 1;
                    zadnjeOsvjezenje = DateTime.Now;
                    stripPogledOsvjezi.Enabled = false;
                    stripPretragaOsvjezi.Enabled = false;
                    stripStranicaSljedeca.Enabled = false;
                    if (sender != null)
                        this.trenutnaStranica = -1;
                    PokaziPretraga();
                    stripPogledOsvjezi.Enabled = true;
                    stripPretragaOsvjezi.Enabled = true;
                    stripStranicaSljedeca.Enabled = true;
                }
            }
        }
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            this.Show();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            MobilniUredaji.NapuniListu();
            GetNetPostavke();
            stripPogledOsvjezi_Click(stripPogledOsvjezi, null);
        } 
        #endregion

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.RowIndex < dataGridView1.Rows.Count - 1)
            {

                string imeColumne = dataGridView1.Columns[e.ColumnIndex].Name;
                if (imeColumne == "pretplate")
                {
                    string ime = dataGridView1.Rows[e.RowIndex].Cells["ime"].Value.ToString();
                    string prezime = dataGridView1.Rows[e.RowIndex].Cells["prezime"].Value.ToString();
                    string mbg = dataGridView1.Rows[e.RowIndex].Cells["mbg"].Value.ToString();
                    int id;
                    int.TryParse(dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                    Form frm = new Pretplate(ime, prezime, id, mbg);
                    frm.ShowDialog(this);
                }
                else if (imeColumne == "azuriraj")
                {
                    int id;
                    int.TryParse(dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                    Pretplatink frm = new Pretplatink(id);
                    DialogResult rez = frm.ShowDialog(this);
                    if (rez == DialogResult.OK && frm.PretplatnikID > 0)
                    {
                        Form frmPretplate = new Pretplate(frm.Ime, frm.Prezime, frm.PretplatnikID, frm.MBG);
                        frmPretplate.ShowDialog(this);
                    }
                }
            }

        }

        public bool PrijavljenJe
        {
            get
            {
                if (zaposlenikID > 0)
                    return true;
                else
                    return false;
            }
        }
        public int PristupJe
        {
            get { return zaposlenikPristup; }

        }
        public int ZaposlenikID
        {
            get { return zaposlenikID; }
        }
        public int ZaposlenikPoslovnica
        {
            get { return zaposlenikPoslovnica; }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (stripPogledMod.SelectedIndex > -1)
                Properties.Settings.Default.PogledIndex = stripPogledMod.SelectedIndex;
            if (stripPretragaVrsta.SelectedIndex > -1)
                Properties.Settings.Default.PretragaIndex = stripPretragaVrsta.SelectedIndex;
            if (FormWindowState.Maximized == this.WindowState )
                Properties.Settings.Default.MainWindowsState = this.WindowState;
            else
                Properties.Settings.Default.MainWindowsState = FormWindowState.Normal;
            if (stripPogledZaPoslovnice.SelectedIndex > -1)
                Properties.Settings.Default.PogledZaPoslovnice = stripPogledZaPoslovnice.SelectedIndex;

            Properties.Settings.Default.Save();
        }

        //private void striptxtMaxRedovaPoStranici_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyValue == 13)
        //    {
        //        int temp;
        //        int.TryParse(striptxtMaxRedovaPoStranici.Text, out  temp);
        //        if (temp > 0)
        //        {
        //            redovaPoStranici = temp;
        //            stripMaximalnoRedovaPoStranici.Text = "Redova: " + redovaPoStranici;
        //            stripMaximalnoRedovaPoStranici.HideDropDown();

        //        }
        //    }
        //}

    }

}