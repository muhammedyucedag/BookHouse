using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BookHouse
{
    public partial class DonationForm : Form

    {
        int MyUserId = 0;
        public DonationForm(int userId)
        {
            MyUserId = userId;
            InitializeComponent();

        }


        SqlConnectionClass scc = new SqlConnectionClass();

        private void btnSignin_Click(object sender, EventArgs e)
        {
            Store st = new Store(this.MyUserId);
            st.Show();
            this.Hide();
        }

        private void BagislamaGecmisi()
        {
            DataSet daset = new DataSet();
            // Bağışlanan kitapları listeleme işlemi 
            SqlDataAdapter adtr = new SqlDataAdapter($"select name,author,publisher,TypeName,PageNumber,Date from Donation as D inner join Type as T on t.Id=d.TypeId and userıd ={MyUserId}",scc.connection());
            adtr.Fill(daset, "Donation");
            dataGridView1.DataSource = daset.Tables["Donation"];
            scc.connection().Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DonationForm_Load(object sender, EventArgs e)
        {
            BagislamaGecmisi();
        }
    }
}
