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
    public partial class Purchased : Form
    {
        int MyUserId = 0;
        public Purchased(int userId)
        {
            
            MyUserId = userId;
            InitializeComponent();
        }

        SqlConnectionClass scc = new SqlConnectionClass();
        private void btnHome_Click(object sender, EventArgs e)
        {
            Store st = new Store(this.MyUserId);
            st.Show();
            this.Hide();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void SatinAlimGecmisi()
        {
            DataSet daset = new DataSet();
            // satın alınan kitapları listeleme işlemi 
            
            SqlDataAdapter adtr = new SqlDataAdapter($"select Name,Author,Publisher,PageNumber,Price ,TypeName,P.CreationTime from Books as B inner join Purchased as P on b.BooksId=p.Books inner join Type as T on t.Id=b.TypeId and p.UserId={MyUserId}", scc.connection());
            
            adtr.Fill(daset, "Purchased");
            dataGridView1.DataSource = daset.Tables["Purchased"];
            scc.connection().Close();

        }   
        private void Purchased_Load(object sender, EventArgs e)
        {
            SatinAlimGecmisi();
        }
    }
}
