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
    public partial class BuyBook : Form
    {
        int MyUserId = 0;


        string kullaniciadi;
        public BuyBook(int userId)
        {
            MyUserId = userId;
            InitializeComponent();
        }

        SqlConnectionClass scc = new SqlConnectionClass();
        private void kitaplistele()
        {
            // kitapları listeleme işlemi 
             
            SqlDataAdapter adtr = new SqlDataAdapter("select Name, Author,Publisher,TypeName, PageNumber,Price from books INNER JOIN Type on Books.TypeId=Type.ıd", scc.connection());
            adtr.Fill(daset, "Books");
            dataGridView1.DataSource = daset.Tables["Books"];
            scc.connection().Close();
        }
        private void BuyBook_Load(object sender, EventArgs e)   
        {
            // Kitapları listeleme işlemi
        
            kitaplistele();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Store st = new Store(this.MyUserId);
            st.Show();
            this.Hide();
        }

        private void btnBuy_Click(object sender, EventArgs e )
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Seçmiş Olduğunuz kitabı satın almak istiyor musunuz ? ", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            SqlCommand command = new SqlCommand("insert into Purchased values (@UserId,@Books,@date)", scc.connection());
            command.Parameters.AddWithValue("@UserId", this.MyUserId);
            command.Parameters.AddWithValue("@books", this.lblBookId.Text);
            command.Parameters.AddWithValue("@date", DateTime.Now);

            command.ExecuteNonQuery();
            scc.connection().Close();

            MessageBox.Show("Seçili Olan Kitap Başarılı Bir Şekilde Satın Alındı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBookname.Text = dataGridView1.CurrentRow.Cells["name"].Value.ToString();
        }

        private void txtBookname_TextChanged(object sender, EventArgs e)
        {
            // Kitap tablomuzdaki typeıd Type tablomuzdaki Id Eşittir o yüzden iki tabloyu birleştirip ( INNER JOIN ) daha sağlıklı bir sonu elde ettik 
            SqlCommand command = new SqlCommand("select name,author,pagenumber,publisher,typename,price,booksıd from Books INNER JOIN type on  Books.TypeId = Type.ıd where Name like '" + txtBookname.Text + "'", scc.connection());
            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                txtBookname.Text = read["name"].ToString();
                txtAuthor.Text = read["author"].ToString();
                txtPagenumber.Text = read["pagenumber"].ToString();
                txtPublisher.Text = read["publisher"].ToString();
                txtTypeName.Text = read["typename"].ToString();
                txtPrice .Text = read["price"].ToString();
                lblBookId.Text = read["booksıd"].ToString();
            }
            scc.connection().Close();
        }

        DataSet daset = new DataSet(); // kayıtları geçici olarak tutmaızın için 
        private void txtSearchname_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["Books"].Clear();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Books where name like '%" + txtSearchname.Text + "%'", scc.connection()); // % işaretinin amacı nerede bulursa bulsun bize gösterecek
            adtr.Fill(daset, "Books");
            dataGridView1.DataSource = daset.Tables["Books"];
            scc.connection().Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtAuthor.Clear();
            txtBookname.Clear();
            txtPagenumber.Clear();
            txtPrice.Clear();
            txtPublisher.Clear();
            txtTypeName.Clear();

            txtBookname.Focus();
        }

        private void btnPurchased_Click(object sender, EventArgs e)
        {
            Purchased prd = new Purchased(MyUserId);
            prd.Show();
            this.Hide();
        }
    }
}
