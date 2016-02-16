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
    public partial class Poslovnice : BaseForm
    {
        OdbcCommand odbcCommand;
        OdbcConnection odbcConn = new System.Data.Odbc.OdbcConnection();
        int id = 0;
        bool osvjezi = true;
        public Poslovnice()
        {
            InitializeComponent();
            string connectionString = Program.GetConnString();
            odbcConn = new System.Data.Odbc.OdbcConnection(connectionString);
        }
        private void Poslovnice_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            GetPoslovnice();
        }

        private void GetPoslovnice()
        {
            if (osvjezi)
            {
                OdbcDataReader dr;
                odbcCommand = new OdbcCommand("select * from poslovnice", odbcConn);
                Status("Spajam se na bazu...");
                Application.DoEvents();
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                try
                {
                    if (odbcConn.State != ConnectionState.Open)
                        odbcConn.Open();
                    dr = odbcCommand.ExecuteReader();
                    dataGridView1.Columns.Add("ime", "Ime poslovnice");
                    dataGridView1.Columns.Add("created", "Napravito");
                    DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();
                    DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();
                    col1.HeaderText = "Ažuriraj";
                    col1.Name = "azuriraj";
                    col2.HeaderText = "Obriši";
                    col2.Name = "obrisi";
                    dataGridView1.Columns.Add(col1);
                    dataGridView1.Columns.Add(col2);
                    dataGridView1.Columns.Add("info", "Informacije");
                    dataGridView1.Columns.Add("id", "Informacije");
                    dataGridView1.Columns["id"].Visible = false;

                    while (dr.Read())
                    {
                        DataGridViewRow red = dataGridView1.Rows[dataGridView1.Rows.Add()];
                        red.Cells["ime"].Value = dr["ime"].ToString();
                        red.Cells["created"].Value = dr["created"].ToString();
                        red.Cells["azuriraj"].Value = "Ažuriraj";
                        red.Cells["obrisi"].Value = "Obriši";

                        red.Cells["info"].Value = dr["info"].ToString();
                        red.Cells["id"].Value = dr["id"].ToString();
                    }
                    Status("Naredba izvršena uspješno");
                }
                catch { MessageBox.Show("Dobivanje liste poslovnica neuspješno", "Greška"); }
                finally
                {
                    if (odbcConn.State == ConnectionState.Open)
                        odbcConn.Close();
                }
                osvjezi = false;
            }
        }
        private bool NovaPoslovnica()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into poslovnice (ime, created, info) values ('");
            sb.Append(txtImePoslovnice.Text.Trim());
            sb.Append("','");
            sb.Append(DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"));
            sb.Append("','");
            sb.Append(txtInfo.Text.Trim());
            sb.Append("')");
            odbcCommand = new OdbcCommand(sb.ToString(), odbcConn);
            int rez = 0;
            try
            {
                Status("Spajam se na bazu...");
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                rez = odbcCommand.ExecuteNonQuery();
                if (rez > 0)
                {
                    Status("Poslovnica " + txtImePoslovnice.Text.ToUpper().Trim() + " je dodan u bazu");
                    MessageBox.Show("Poslovnica dodana u bazu", "Mobilis");
                }
                else
                {
                    Status("Poslovnica nije dodana u bazu");
                    MessageBox.Show("Poslovnica nije dodana u bazu", "Mobilis");
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
        private bool AzurirajPoslovnicu()
        {
            int rez = 0;
            if (MyValidation() && id > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE poslovnice SET ime = '");
                sb.Append(txtImePoslovnice.Text.Trim());
                sb.Append("', info = '");
                sb.Append(txtInfo.Text.Trim());
                sb.Append("' WHERE id =");
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
                        Status("Ažuriranje poslovnice uspjesno");
                        MessageBox.Show("Ažuriranje poslovnice uspjesno");
                    }
                    else
                    {
                        Status("Ažuriranje poslovnice nije uspjesno");
                        MessageBox.Show("Ažuriranje poslovnice nije uspjesno", "Mobilis");
                    }
                }
                catch (Exception err) { MessageBox.Show(err.Message, "Greška"); }
                finally
                {
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
        private bool ObrisiPoslovnicu()
        {
            if (id <= 0)
                return false;
            int rez = 0;
            odbcCommand = new OdbcCommand("DELETE FROM poslovnice WHERE id =" + id, odbcConn);
            Status("Spajam se na bazu...");
            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                rez = odbcCommand.ExecuteNonQuery();
                if (rez > 0)
                {
                    Status("Poslovnica je obrisana");
                    MessageBox.Show("Poslovnica je obrisana", "Mobilis");
                }
                else
                {
                    Status("Poslovnica nije obrisana");
                    MessageBox.Show("Poslovnica nije obrisana", "Mobilis");
                }

            }
            catch { MessageBox.Show("Poslovnica nije obrisana", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
        private bool MyValidation()
        {
            if (txtImePoslovnice.Text.Trim() == "")
            {
                errorProvider1.SetError(txtImePoslovnice, "Polje nemože biti prazno");
                return false;
            }
            else
            {
                errorProvider1.SetError(txtImePoslovnice, "");
                return true;
            }
        }

        private void btnNewPoslovnica_Click(object sender, EventArgs e)
        {
            if (!(PrijavljenJe && PristupJe))
                return;

            if (MyValidation())
            {
                if (id == 0)
                    osvjezi = NovaPoslovnica();
                else
                    osvjezi = AzurirajPoslovnicu();
            }
            else
                MessageBox.Show("Molimo popunite sva polja ispravno", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
                            osvjezi = ObrisiPoslovnicu();
                            GetPoslovnice();
                        }
                    }
                }
                else if (imeColumne == "azuriraj")
                {
                    txtImePoslovnice.Text = dataGridView1.Rows[e.RowIndex].Cells["ime"].Value.ToString();
                    txtInfo.Text = dataGridView1.Rows[e.RowIndex].Cells["info"].Value.ToString();
                    tabControl1.SelectedIndex = 1;
                }
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                if (sender != null)
                {
                    txtImePoslovnice.Text = "";
                    txtInfo.Text = "";
                    id = 0;
                }
                MyValidation();
            }
            else if (tabControl1.SelectedIndex == 0)
                GetPoslovnice();

        }
    }
}