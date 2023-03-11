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
    public partial class addBook : Form
    {
        public addBook()
        {
            InitializeComponent();
        }
        SqlConnectionClass scc = new SqlConnectionClass();
        DataSet daset = new DataSet();
        private void Book_Listing()
        {
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Books", scc.connection());
            adtr.Fill(daset, "Books");
            dataGridView1.DataSource = daset.Tables["Books"];
            scc.connection().Close();
        }
        private void addBook_Load(object sender, EventArgs e)
        {
            Book_Listing();

            SqlCommand command = new SqlCommand("select typename from type", scc.connection());
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                cmbType.Items.Add(dr[0]);
            }
            scc.connection().Close();
        }
   
        private void btnAdd_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bilgilerini Girmiş Olduğunuz Kitabı eklemek İstiyor musunuz? ", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            int typeId = 0;
            SqlCommand commandTypeId = new SqlCommand("select Id from Type where TypeName = @p1", scc.connection());
            commandTypeId.Parameters.AddWithValue("@p1", cmbType.SelectedItem.ToString());
            SqlDataReader dr = commandTypeId.ExecuteReader();
            while (dr.Read())

            {
                typeId = Convert.ToInt32(dr[0]);    
            }

            SqlCommand command = new SqlCommand("insert into Books (name,author,publisher,TypeId,pagenumber,price,date) values (@name,@author,@publisher,@typeıd,@pagenumber,@price,@date)", scc.connection());
            command.Parameters.AddWithValue("@name", txtName.Text);
            command.Parameters.AddWithValue("@author", txtAuthor.Text);
            command.Parameters.AddWithValue("@publisher", txtPublisher.Text); 
            command.Parameters.AddWithValue("@typeıd", typeId);
            command.Parameters.AddWithValue("@pagenumber", Convert.ToInt32(txtPageNumber.Text));
            command.Parameters.AddWithValue("@price", txtPrice.Text);
            command.Parameters.AddWithValue("@date", DateTime.Now.ToShortDateString());
            command.ExecuteNonQuery();
            scc.connection().Close();
            MessageBox.Show("Kitabınız Başarılı Bir Şekilde eklenmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            daset.Clear();
            refresh();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Seçmiş Olduğunuz kaydı silmek istiyor musunuz ? ", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            SqlCommand command = new SqlCommand("delete from Books where name=@name", scc.connection());
            command.Parameters.AddWithValue("@name", dataGridView1.CurrentRow.Cells["name"].Value.ToString());
            command.ExecuteNonQuery();
            scc.connection().Close();
            MessageBox.Show("Silme İşlemi Başarılı Bir Şekilde Gerçekleşti");
            daset.Tables["books"].Clear();
            Book_Listing();
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bilgilerini Girmiş Olduğunuz Kitabı Güncellemek İstiyor musunuz? ", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            int typeId = 0;
            SqlCommand commandTypeId = new SqlCommand("select Id from Type where TypeName = @p1", scc.connection());
            commandTypeId.Parameters.AddWithValue("@p1", cmbType.SelectedItem.ToString());
            SqlDataReader dr = commandTypeId.ExecuteReader();
            while (dr.Read())

            {
                typeId = Convert.ToInt32(dr[0]);
            }
            SqlCommand command = new SqlCommand("update books set author=@author, typeıd=@type, pagenumber=@pagenumber, price=@price, publisher=@publisher, name=@name where BooksId=@BooksId", scc.connection());
            command.Parameters.AddWithValue("@author", txtAuthor.Text);
            command.Parameters.AddWithValue("@name", txtName.Text);
            command.Parameters.AddWithValue("@pagenumber", txtPageNumber.Text);
            command.Parameters.AddWithValue("@price", txtPrice.Text);
            command.Parameters.AddWithValue("@publisher", txtPublisher.Text);
            command.Parameters.AddWithValue("@BooksId", txtBooksId.Text);
            command.Parameters.AddWithValue("@type", typeId);
            command.ExecuteNonQuery();
            scc.connection().Close();
            daset.Tables["Books"].Clear();
            Book_Listing();
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
            txtName.Focus();
        }

        private void txtBooksearch_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["Books"].Clear();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Books where name like '%" + txtNamesearch.Text + "%'", scc.connection());
            adtr.Fill(daset, "Books");
            dataGridView1.DataSource = daset.Tables["Books"];
            scc.connection().Close();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dataGridView1.CurrentRow.Cells["name"].Value.ToString();

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            //SqlCommand command = new SqlCommand("select * from Books where name like '" + txtName.Text + "'", scc.connection());
            SqlCommand command = new SqlCommand("select BooksId,Name,Author,Publisher,TypeId,PageNumber,price,Date,TypeName from Books as B inner join Type as T on b.TypeId=t.Id where name like '" + txtName.Text + "'", scc.connection());
            SqlDataReader read = command.ExecuteReader();
            while (read.Read())
            {
                txtName.Text = read["name"].ToString();
                txtAuthor.Text = read["author"].ToString();
                txtPublisher.Text = read["publisher"].ToString();
                txtPageNumber.Text = read["pagenumber"].ToString();
                txtPrice.Text = read["price"].ToString();
                cmbType.Text = read["typename"].ToString();
                txtBooksId.Text = read["BooksId"].ToString();

                scc.connection().Close();

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
            txtName.Focus();

        }
        public void refresh()
        {
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Books", scc.connection());
            adtr.Fill(daset, "Books");
            dataGridView1.DataSource = daset.Tables["Books"];
            scc.connection().Close();
            
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
