using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.Odbc;

namespace Mobilis
{
    public partial class Zaposlenici : BaseForm
    {
        OdbcCommand odbcCommand;
        OdbcConnection odbcConn = new System.Data.Odbc.OdbcConnection();
        System.Collections.ArrayList poslovniceID = new System.Collections.ArrayList();
        int id = 0;
        bool osvjezi = true;
        private bool imamPoslovnice = false;
        public Zaposlenici()
        {
            InitializeComponent();

            string connectionString = Program.GetConnString();
            odbcConn = new System.Data.Odbc.OdbcConnection(connectionString);
            tabControl1.Click += new EventHandler(tabControl1_Click);
        }
        private void Employees_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            cmbPristup.SelectedIndex = 0;
            GetEmployees();
        }

        private void GetEmployees()
        {
            if (osvjezi)
            {
                OdbcDataReader dr;
                odbcCommand = new OdbcCommand("select zaposlenici.*, poslovnice.ime as poslovnica from zaposlenici left join poslovnice on zaposlenici.pid = poslovnice.id", odbcConn);
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Add("username", "Korisnièko ime");
                dataGridView1.Columns.Add("poslovnica", "Poslovnica");
                dataGridView1.Columns.Add("pristup", "Pristup");
                dataGridView1.Columns.Add("created", "Napravit");
                dataGridView1.Columns.Add("lastlogin", "Zadnji login");
                DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();
                DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();
                col1.HeaderText = "Ažuriraj";
                col1.Name = "azuriraj";
                col2.HeaderText = "Obriši";
                col2.Name = "obrisi";
                dataGridView1.Columns.Add(col1);
                dataGridView1.Columns.Add(col2);
                dataGridView1.Columns.Add("info", "Informacije");
                dataGridView1.Columns.Add("id", "id");
                dataGridView1.Columns["id"].Visible = false;
                int pristup;
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
                        red.Cells[0].Value = dr["username"].ToString();
                        red.Cells[1].Value = dr["poslovnica"].ToString();
                        int.TryParse(dr["pristup"].ToString(), out pristup);
                        red.Cells[2].Value = cmbPristup.Items[pristup].ToString();
                        red.Cells[3].Value = dr["created"].ToString();
                        red.Cells[4].Value = dr["lastlogin"].ToString();
                        red.Cells[5].Value = "Ažuriraj";
                        red.Cells[6].Value = "Obriši";
                        red.Cells[7].Value = dr["info"].ToString();
                        red.Cells["id"].Value = dr["id"].ToString();
                    }
                    dr.Close();
                    Status("Naredba izvršena uspješno");
                    osvjezi = false;
                }
                catch { MessageBox.Show("Dobivanje liste zaposlenika neuspješno", "Greška"); }
                finally
                {
                    if (odbcConn.State == ConnectionState.Open)
                        odbcConn.Close();
                }
            }
        }
        private void GetPoslovnice()
        {
            OdbcDataReader dr;
            odbcCommand = new OdbcCommand("select * from poslovnice", odbcConn);
            Status("Spajam se na bazu...");
            Application.DoEvents();
            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                dr = odbcCommand.ExecuteReader();
                cmbPoslovnice.Items.Clear();
                poslovniceID.Clear();
                while (dr.Read())
                {
                    cmbPoslovnice.Items.Add(dr["ime"].ToString());
                    poslovniceID.Add(dr["id"].ToString());
                }
                if (cmbPoslovnice.Items.Count > 0)
                    cmbPoslovnice.SelectedIndex = 0; 
                imamPoslovnice = true;
                dr.Close();
                Status("Naredba izvršena uspješno");
            }
            catch { MessageBox.Show("Dobivanje liste poslovnica neuspješno", "Greška"); }
            finally
            {
                if (odbcConn.State == ConnectionState.Open)
                    odbcConn.Close();
            }

        }
        private void NoviZaposlenik()
        {
            int pristup = cmbPristup.SelectedIndex;
            int pid = cmbPoslovnice.SelectedIndex;
            if (pid > -1 && pid < poslovniceID.Count)
                int.TryParse(poslovniceID[pid].ToString(), out pid);
            else
                pid = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into zaposlenici (username, password, info, lastlogin, created, pristup, pid) values ('");
            sb.Append(txtUserName.Text.Trim());
            sb.Append("','");
            sb.Append(txtPassword1.Text.Trim());
            sb.Append("','");
            sb.Append(txtInfo.Text.Trim());
            sb.Append("','");
            sb.Append(DateTime.MinValue.ToString("yyyy.MM.dd HH:mm:ss"));
            sb.Append("','");
            sb.Append(DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"));
            sb.Append("',");
            sb.Append(pristup);
            sb.Append(",");
            sb.Append(pid);
            sb.Append(")");
            odbcCommand = new OdbcCommand(sb.ToString(), odbcConn);
            try
            {
                Status("Spajam se na bazu...");
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                int rez = odbcCommand.ExecuteNonQuery();
                if (rez > 0)
                {
                    MessageBox.Show("Zaposlenik dodan u bazu");
                    Status("Zaposlenik " + txtUserName.Text.ToUpper().Trim() + " je dodan u bazu");
                }

            }
            catch (Exception err) { MessageBox.Show(err.Message, "Greška"); }
            finally
            {
                if (odbcConn.State == ConnectionState.Open)
                    odbcConn.Close();
            }
        }
        private bool AzurirajZaposlenika()
        {
            int rez = 0;
            int pristup = cmbPristup.SelectedIndex;
            int pid = cmbPoslovnice.SelectedIndex;
            if (pid < poslovniceID.Count)
                int.TryParse(poslovniceID[pid].ToString(), out pid);
            else
                pid = 0;

            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE zaposlenici SET username = '");
            sb.Append(txtUserName.Text.Trim());
            sb.Append("', ");
            if (txtPassword1.Text.Trim() != "")
            {
                sb.Append(" password = '");
                sb.Append(txtPassword1.Text.Trim());
                sb.Append("', ");
            }
            sb.Append(" info= '");
            sb.Append(txtInfo.Text.Trim());
            sb.Append("', pristup =");
            sb.Append(pristup);
            sb.Append(", pid = ");
            sb.Append(pid);
            sb.Append(" WHERE id =");
            sb.Append(id);
            odbcCommand = new OdbcCommand(sb.ToString(), odbcConn);
            try
            {
                Status("Spajam se na bazu...");
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                rez = odbcCommand.ExecuteNonQuery();
                if (rez > 0)
                {
                    Status("Ažuriranje zaposlenika uspjesno");
                    MessageBox.Show("Ažuriranje zaposlenika uspjesno");
                }
                else
                {
                    Status("Ažuriranje zaposlenika nije uspjesno");
                    MessageBox.Show("Ažuriranje zaposlenika nije uspjesno", "Mobilis");
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
        private bool ObrisiZaposlenika()
        {
            if (id <= 0)
                return false;
            int rez = 0;
            odbcCommand = new OdbcCommand("DELETE FROM zaposlenici WHERE id =" + id, odbcConn);
            Status("Spajam se na bazu...");
            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                rez = odbcCommand.ExecuteNonQuery();
                if (rez > 0)
                {
                    Status("Zaposlenik je obrisan");
                    MessageBox.Show("Zaposlenik je obrisan", "Mobilis");
                }
                else
                {
                    Status("Zaposlenik nije obrisan");
                    MessageBox.Show("Zaposlenik nije obrisan", "Mobilis");
                }

            }
            catch { MessageBox.Show("Zaposlenik nije obrisan", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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


        private void btnNewUser_Click(object sender, EventArgs e)
        {
            if (!(PrijavljenJe && PristupJe))
                return;
            if (MyValidation())
            {
                if (id == 0)
                    NoviZaposlenik();
                else if (id > 0)
                    AzurirajZaposlenika();
            }
            else
                MessageBox.Show("Molimo popunite sva polja ispravno", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                if (!imamPoslovnice)
                    GetPoslovnice();
                if (sender != null)
                {
                    txtUserName.Text = "";
                    txtPassword1.Text = "";
                    txtPassword2.Text = "";
                    txtInfo.Text = "";
                    btnNewUser.Text = "&Napravi";
                    id = 0;
                }
                MyValidation();
            }
            else if (tabControl1.SelectedIndex == 0)
                GetEmployees();
        }

        private void ValidateInput(object sender, EventArgs e)
        {
            MyValidation();
        }
        private bool MyValidation()
        {
            byte rez = 0;
            if (txtUserName.Text.Trim() == "" || txtUserName.Text.Length < 4 || txtUserName.Text.Length > 34)
            {
                errorStats.SetError(txtUserName, "Poslje nesmje biti prazno, mora biti minimalno 4 znaka i maksimalno 32 znaka");
                rez++;
            }
            else
                errorStats.SetError(txtUserName, "");
            if ((txtPassword1.Text.Trim() == "" || txtPassword1.Text.Length < 4 || txtPassword1.Text.Length > 34) && id == 0)
            {
                errorStats.SetError(txtPassword1, "Poslje nesmje biti prazno, mora biti minimalno 4 znaka i maksimalno 32 znaka");
                rez++;
            }
            else
                errorStats.SetError(txtPassword1, "");
            if ((txtPassword2.Text.Trim() != txtPassword1.Text.Trim() || txtPassword2.Text.Trim() == "") && id == 0)
            {
                errorStats.SetError(txtPassword2, "Ponovljena lozinka mora biti jednaka lozinki u polju Lozinka");
                rez++;
            }
            else
                errorStats.SetError(txtPassword2, "");
            if (cmbPoslovnice.SelectedIndex == -1)
            {
                errorStats.SetError(cmbPoslovnice, "Odaberi poslovnicu");
                rez++;
            }
            else
                errorStats.SetError(cmbPoslovnice, "");

            if (rez != 0)
                return false;
            else
                return true;
        }

        private void txtProvjera_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue < 65 || e.KeyValue > 90)
                if (e.KeyValue < 97 || e.KeyValue > 122)
                    if (e.KeyValue < 48 || e.KeyValue > 57)
                    if (e.KeyValue != 46 && e.KeyValue != 37 && e.KeyValue != 39 && e.KeyValue != 8)
                        e.SuppressKeyPress = true; 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.RowIndex < dataGridView1.Rows.Count - 1)
            {
                string imeColumne = dataGridView1.Columns[e.ColumnIndex].Name;
                int.TryParse(dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                if (imeColumne == "obrisi")
                {
                    if (PrijavljenJe && PristupJe)
                    {
                        InputBox frm = new InputBox();
                        DialogResult rez = frm.ShowDialog();
                        if (rez == DialogResult.OK)
                        {
                            osvjezi = ObrisiZaposlenika();
                            GetEmployees();
                        }
                    }
                }
                else if (imeColumne == "azuriraj")
                {
                    if (!imamPoslovnice)
                        GetPoslovnice();
                    btnNewUser.Text = "&Ažuriraj";
                    int pristup;
                    txtUserName.Text = dataGridView1.Rows[e.RowIndex].Cells["username"].Value.ToString();
                    txtInfo.Text = dataGridView1.Rows[e.RowIndex].Cells["info"].Value.ToString();
                    int.TryParse(dataGridView1.Rows[e.RowIndex].Cells["pristup"].Value.ToString(), out pristup);
                    cmbPristup.SelectedIndex = pristup;
                    string poslovnica = dataGridView1.Rows[e.RowIndex].Cells["poslovnica"].Value.ToString();
                    for (int i = 0; i < cmbPoslovnice.Items.Count; i++)
                    {
                        if (cmbPoslovnice.Items[i].ToString() == poslovnica)
                        {
                            cmbPoslovnice.SelectedIndex = i;
                            break;
                        }
                    }
                    int.TryParse(dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                    tabControl1.SelectedIndex = 1;
                }
            }
        }
    }
}