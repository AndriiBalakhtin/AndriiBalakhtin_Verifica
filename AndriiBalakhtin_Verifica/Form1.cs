using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AndriiBalakhtin_Verifica
{
    public partial class Form1 : Form
    {
        string connectionString = "server=127.0.0.1;database=magazzino;uid=root";
        public Form1()
        {
            InitializeComponent();
            Connection();
        }

        private void Connection()
        {
           MySqlConnection cnn = new MySqlConnection(connectionString);

            try
            {
                cnn.Open();
                string query = "SELECT * FROM elenco";

                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, cnn))
                {
                    DataSet ds = new DataSet();
                    adpt.Fill(ds);
                    dataGridView.DataSource = ds.Tables[0];
                }

                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBoxSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {            
            DataTable datatable = (dataGridView.DataSource as DataTable);
            string SearcText = txtBoxSearch.Text;

            string filterExpression = string.Format($"Descrizione LIKE '{SearcText}%' OR Produttore LIKE '{SearcText}%' OR Codice_produttore Like '{SearcText}%'", SearcText);
            datatable.DefaultView.RowFilter = filterExpression;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

      

        private void BtnDeny_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridView.CurrentRow.Cells[0].Value.ToString();

            Form2 formModifica = new Form2(id);
            formModifica.ShowDialog();
            Connection();
        }
    }
}