using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;

// TODO: prvjeravam duplikate (gotovo)

namespace Mobilis
{
    public partial class Mobiteli : BaseForm
    {
        OdbcCommand odbcCommand;
        OdbcConnection odbcConn = new System.Data.Odbc.OdbcConnection();
        System.Collections.ArrayList listaMobitela = new System.Collections.ArrayList();
        bool osvjezi = true;
        int id = 0;
        public Mobiteli()
        {
            InitializeComponent();
            string connectionString = Program.GetConnString();
            odbcConn = new System.Data.Odbc.OdbcConnection(connectionString);

        }
        private void Mobiteli_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            GetMobiteli();
        }

        private void GetMobiteli()
        {
            if (osvjezi)
            {
                OdbcDataReader dr;
                string ime = "";
                odbcCommand = new OdbcCommand("SELECT * from mobiteli", odbcConn);
                Status("Spajam se na bazu...");
                Application.DoEvents();
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                listaMobitela.Clear();
                dataGridView1.Columns.Add("ime", "Ime mobitela");
                dataGridView1.Columns.Add("created", "Dodan u bazu");

                DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();
                DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();

                col1.HeaderText = "Ažuriraj";
                col1.Name = "azuriraj";
                col2.HeaderText = "Obriši";
                col2.Name = "obrisi";
                dataGridView1.Columns.Add(col1);
                dataGridView1.Columns.Add(col2);
                dataGridView1.Columns.Add("id", "id");
                dataGridView1.Columns[4].Visible = false;
                try
                {
                    if (odbcConn.State != ConnectionState.Open)
                        odbcConn.Open();
                    dr = odbcCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        DataGridViewRow red = dataGridView1.Rows[dataGridView1.Rows.Add()];
                        ime = dr["ime"].ToString();
                        red.Cells[0].Value = ime;
                        listaMobitela.Add(ime.ToLower());
                        red.Cells[1].Value = dr["created"].ToString();
                        red.Cells[2].Value = "Ažuriraj";
                        red.Cells[3].Value = "Obriši";
                        red.Cells[4].Value = dr["id"].ToString();

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
        private bool DodajMobitel()
        {
            int rez = 0;
            if (txtImeMobitela.Text.Trim() != "")
            {
                StringBuilder sb = new StringBuilder();

                if (id == 0)
                {
                    if (ProvjeriDuplikate())
                    {
                        DialogResult odabir = MessageBox.Show("Mobitel je naðen u bazi, želiš li nastaviti", "Mobitel u bazi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (odabir == DialogResult.Cancel)
                            return false;
                    }
                    sb.Append("INSERT INTO mobiteli (ime, created) VALUES ('");
                    sb.Append(txtImeMobitela.Text.Trim());
                    sb.Append("', NOW())");
                }
                else
                {
                    sb.Append("UPDATE mobiteli SET ime = '");
                    sb.Append(txtImeMobitela.Text);
                    sb.Append("' WHERE id = ");
                    sb.Append(id);
                }
                odbcCommand = new OdbcCommand(sb.ToString(), odbcConn);
                rez = 0;
                try
                {
                    if (odbcConn.State != ConnectionState.Open)
                        odbcConn.Open();
                    rez = odbcCommand.ExecuteNonQuery();
                    if (rez > 0)
                    {
                        if (id == 0)
                        {
                            listaMobitela.Add(txtImeMobitela.Text.ToLower().Trim());
                            Status("Mobitel dodan u bazu");
                            MessageBox.Show("Mobitel dodan u bazu", "Mobilis");
                        }
                        else
                        {
                            Status("Mobitel je ažuriran");
                            MessageBox.Show("Mobitel je ažuriran", "Mobilis");
                        }
                        txtImeMobitela.Text = "";
                    }
                    else
                    {
                        if (id == 0)
                            Status("Mobitel nije dodan u bazu");
                        else
                            Status("Mobitel nije ažuriran");
                    }
                }
                catch { MessageBox.Show("Mobitel nije ažuriran ", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                finally
                {
                    if (odbcConn.State == ConnectionState.Open)
                        odbcConn.Close();
                }
            }
            else
                errorProvider1.SetError(txtImeMobitela, "Polje nemože biti prazno");
            if (rez > 0)
                return true;
            else
                return false;
        }
        private bool ObrisiMobitel()
        {
            if (id <= 0)
                return false;
            int rez = 0;
            odbcCommand = new OdbcCommand("DELETE FROM mobiteli WHERE id =" + id, odbcConn);
            Status("Spajam se na bazu...");
            try
            {
                if (odbcConn.State != ConnectionState.Open)
                    odbcConn.Open();
                rez = odbcCommand.ExecuteNonQuery();
                if (rez > 0)
                {
                    Status("Mobitel je obrisan");
                    MessageBox.Show("Mobitel je obrisan", "Mobilis");
                }
                else
                {
                    Status("Mobitel nije obrisan");
                    MessageBox.Show("Mobitel nije obrisan", "Mobilis");
                }

            }
            catch { MessageBox.Show("Mobitel nije obrisan", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
        private bool ProvjeriDuplikate()
        {
            string ime = txtImeMobitela.Text.ToLower().Trim();
            foreach (string mobitel in listaMobitela)
            {
                if (mobitel == ime)
                    return true;
            }
            return false;
        }

        private void btnDodajMobitel_Click(object sender, EventArgs e)
        {
            if (!(PrijavljenJe && PristupJe))
                return;
            osvjezi = DodajMobitel();
            if (id > 0)
                tabControl1.SelectedIndex = 0;
        }
        private void btnPonisti_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.RowIndex < dataGridView1.Rows.Count - 1)
            {
                int.TryParse(dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                string imeColumne = dataGridView1.Columns[e.ColumnIndex].Name;
                if (imeColumne == "obrisi")
                {
                    if (PrijavljenJe && PristupJe)
                    {
                        InputBox frm = new InputBox();
                        DialogResult rez = frm.ShowDialog();
                        if (rez == DialogResult.OK)
                        {
                            osvjezi = ObrisiMobitel();
                            GetMobiteli();
                        }
                    }
                }
                else if (imeColumne == "azuriraj")
                {
                    txtImeMobitela.Text = dataGridView1.Rows[e.RowIndex].Cells["ime"].Value.ToString();
                    tabControl1.SelectedIndex = 1;
                }
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                GetMobiteli();
            else if (tabControl1.SelectedIndex == 1)
            {
                id = 0;
                txtImeMobitela.Text = "";
            }
        }

    }
}