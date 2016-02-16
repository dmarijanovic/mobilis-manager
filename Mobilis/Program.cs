using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Mobilis
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!(CalculateMD5Hash(Properties.Settings.Default.AktivKorisnik + "mobilisprogram?%$") == Properties.Settings.Default.AktivKod))
            {
                Aktivacija frm = new Aktivacija();
                frm.ShowDialog();
            }
            else
                Application.Run(new Form1());
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

        static public bool JMBG(string mb)
        {
            if (mb.Length == 7)
                return true;
            else if (mb.Length != 13)
                return false;
            #region Teorija
                //Ako je JMBG napisan u formatu ABCDEFGHIJKLM onda je:
                //M = 11 - ( 7*(A+G) + 6*(B+H) + 5*(C+I) + 4*(D+J) + 3*(E+K) + 2*(F+L) ) / 11
                //Zbrojiti 1. i 7., 2. i 8., 3. i 9., 4. i 10., 5. i 11., te 6. i 12. znamenku matiènog broja.
                //Dobivenih 6 suma pomnožiti sa 7, 6, 5, 4, 3 odn. 2 i zbrojiti dobivene umnoške.
                //Navedeni zbroj podijeliti sa 11 i ostatak tog dijeljenja oduzeti od 11.  

            #endregion
            int datum;
            int.TryParse(mb.Substring(0, 2), out datum);
            if (datum > 31)
                return false;
            int.TryParse(mb.Substring(2, 2), out datum);
            if (datum > 12)
                return false;
            int kontrola;
            int mbKon=0;
            try
            {
                if (Convert.ToInt32(mb.Substring(0, 2)) < 33 && Convert.ToInt32(mb.Substring(2, 2)) < 13)
                {
                    int a1 = 
                    kontrola = 7 * (Convert.ToInt32(mb.Substring(0, 1)) + Convert.ToInt32(mb.Substring(6, 1)));
                    kontrola += 6 * (Convert.ToInt32(mb.Substring(1, 1)) + Convert.ToInt32(mb.Substring(7, 1)));
                    kontrola += 5 * (Convert.ToInt32(mb.Substring(2, 1)) + Convert.ToInt32(mb.Substring(8, 1)));
                    kontrola += 4 * (Convert.ToInt32(mb.Substring(3, 1)) + Convert.ToInt32(mb.Substring(9, 1)));
                    kontrola += 3 * (Convert.ToInt32(mb.Substring(4, 1)) + Convert.ToInt32(mb.Substring(10, 1)));
                    kontrola += 2 * (Convert.ToInt32(mb.Substring(5, 1)) + Convert.ToInt32(mb.Substring(11, 1)));
                    kontrola = kontrola % 11;
                    kontrola = Math.Abs(11 - kontrola);
                    mbKon = Convert.ToInt32(mb.Substring(12, 1));
                }
                else
                    kontrola = -1;
            }
            catch { kontrola = -1; }
            if (kontrola == mbKon)
                return true;
            else
                return false;
        }
        static public string GetConnString()
        {
            return "DRIVER={MySQL ODBC 5.1 Driver};SERVER=server;PORT=3306;DATABASE=database;UID=uid;PWD=pwd;OPTION=3";
        }
    }
    class TextBoxTab : TextBox
    {
        public delegate void delTab();
        public event delTab TabStisnut;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            IntPtr znak = new IntPtr(9);
            if (znak.Equals(msg.WParam))
                TabStisnut();

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
    class TextBoxNumbers : TextBox
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyValue < 48 || e.KeyValue > 57)
                if (e.KeyValue != 46 && e.KeyValue != 37 && e.KeyValue != 39 && e.KeyValue != 8 && e.KeyValue != 36 && e.KeyValue != 35)
                    if (e.KeyValue < 96 || e.KeyValue > 105)
                        if (!(e.Shift && (e.KeyValue == 45)))
                            if (!(e.Control && (e.KeyValue == 86 || e.KeyValue == 67)))
                                if (!e.Alt)
                                    e.SuppressKeyPress = true; 
            base.OnKeyDown(e);
        }
    }
    public class ToolStripTextBoxEnter : System.Windows.Forms.ToolStripTextBox
    {
        public delegate void delKeyEnter();
        public event delKeyEnter OnKeyEnter;
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                OnKeyEnter();
            }
            else
                base.OnKeyPress(e);
        }
    }
    static class MobilniUredaji
    {
        static System.Collections.ArrayList lista = new System.Collections.ArrayList();
        public static void NapuniListu()
        {
            System.Data.Odbc.OdbcCommand odbcCommand;
            System.Data.Odbc.OdbcConnection odbcConn = new System.Data.Odbc.OdbcConnection();
            string connectionString = Program.GetConnString();
            odbcConn = new System.Data.Odbc.OdbcConnection(connectionString);


            System.Data.Odbc.OdbcDataReader dr;
            odbcCommand = new System.Data.Odbc.OdbcCommand("SELECT ime, id FROM mobiteli", odbcConn);
            int id;
            lista.Clear();
            try
            {
                if (odbcConn.State != System.Data.ConnectionState.Open)
                    odbcConn.Open();
                dr = odbcCommand.ExecuteReader();

                while (dr.Read())
                {
                    int.TryParse(dr["id"].ToString(), out id);
                    lista.Add(new MobiteliBaza(dr["ime"].ToString(), id));
                }
                dr.Close();

            }
            catch { MessageBox.Show("Mobilni ureðaji nisu uèitani", "Greška"); }
            finally
            {
                if (odbcConn.State == System.Data.ConnectionState.Open)
                    odbcConn.Close();
            }
        }
        public static void Prenesi(ref ComboBox cmb)
        {
            cmb.Items.Clear();
            foreach (MobiteliBaza mobitel in lista)
            {
                cmb.Items.Add(mobitel.ime);
                cmb.AutoCompleteCustomSource.Add(mobitel.ime); 
            }
        }
        public static int GetID(string imeMobitela)
        {
            imeMobitela = imeMobitela.ToLower().Trim();
            foreach (MobiteliBaza mobitel in lista)
            {
                if (mobitel.ime.ToLower() == imeMobitela)
                    return mobitel.id;
            }
            return 0;
        }
        public static string GetIme(int mobitelID)
        {
            foreach (MobiteliBaza mobitel in lista)
            {
                if (mobitel.id == mobitelID)
                    return mobitel.ime;
            }
            return "Nije naðen";
        }
        class MobiteliBaza
        {
            public string ime;
            public int id;
            public MobiteliBaza(string ime, int id)
            {
                this.ime = ime;
                this.id = id;
            }
        }
    }
    public class BaseForm : Form
    {
        protected void Status(string data)
        {
            Form1 frm = (Form1)this.Owner;
            frm.toolStripConnStatus.Text = data;
        }

        protected bool PrijavljenJe
        {
            get
            {
                Form1 frm = (Form1)this.Owner;
                if (frm.PrijavljenJe)
                    return true;
                else
                {
                    MessageBox.Show("Samo prijavljeni zaposlenici mogu dodavati sadržaj", "Prijava", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
        }
        protected bool PristupJe
        {
            get
            {
                Form1 frm = (Form1)this.Owner;
                if (frm.PristupJe != 0)
                    return true;
                else
                {
                    MessageBox.Show("Samo zaposlenik sa pristupom može dodavati sadržaj", "Pristup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
        }
        protected int ZaposlenikID
        {
            get
            {
                Form1 frm = (Form1)this.Owner;
                return frm.ZaposlenikID;
            }
        }
        protected int ZaposlenikPoslovnica
        {
            get
            {
                Form1 frm = (Form1)this.Owner;
                return frm.ZaposlenikPoslovnica;
            }
        }
    }

}