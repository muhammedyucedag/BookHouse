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
    public partial class memberListing : Form
    {
        public memberListing()
        {
            InitializeComponent();
        }

        SqlConnectionClass scc = new SqlConnectionClass();
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select * from [user] where username like '" + txtUsername.Text + "'", scc.connection());
            SqlDataReader read = command.ExecuteReader();
            while (read.Read())
            {
                txtName.Text = read["name"].ToString();
                txtSurname.Text = read["surname"].ToString();
                txtPassword.Text = read["password"].ToString();
                txtEmail.Text = read["email"].ToString();
                txtId.Text = read["ıd"].ToString();
                mskPhoneNumber.Text = read["phonenumber"].ToString();

                scc.connection().Close();

            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUsername.Text = dataGridView1.CurrentRow.Cells["username"].Value.ToString();

        }
        DataSet daset = new DataSet();
        private void txtUsernameSearch_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["[user]"].Clear();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from [user] where username like '%" + txtUsernameSearch.Text + "%'", scc.connection());
            adtr.Fill(daset, "[user]");
            dataGridView1.DataSource = daset.Tables["[user]"];
            scc.connection().Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Seçmiş Olduğunuz kaydı silmek istiyor musunuz ? ", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            SqlCommand command = new SqlCommand("delete from [user] where username=@username", scc.connection());
            command.Parameters.AddWithValue("@username", dataGridView1.CurrentRow.Cells["username"].Value.ToString());
            command.ExecuteNonQuery();
            scc.connection().Close();
            MessageBox.Show("Silme İşlemi Başarılı Bir Şekilde Gerçekleşti");
            daset.Tables["[user]"].Clear();
            member_Listing();
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void member_Listing()
        {
            SqlDataAdapter adtr = new SqlDataAdapter("select * from [user]", scc.connection());
            adtr.Fill(daset, "[user]");
            dataGridView1.DataSource = daset.Tables["[user]"];
            scc.connection().Close();
        }
        private void memberListing_Load(object sender, EventArgs e)
        {
            member_Listing();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("update [user] set username=@username, email=@email, name=@name, surname=@surname, phonenumber=@phonenumber, password=@password where ıd=@ıd", scc.connection());
            command.Parameters.AddWithValue("@username",txtUsername.Text);
            command.Parameters.AddWithValue("@email", txtEmail.Text);
            command.Parameters.AddWithValue("@name", txtName.Text);
            command.Parameters.AddWithValue("@surname", txtSurname.Text);
            command.Parameters.AddWithValue("@phonenumber", mskPhoneNumber.Text);
            command.Parameters.AddWithValue("@password", txtPassword.Text);
            command.Parameters.AddWithValue("@ıd", txtId.Text);
            command.ExecuteNonQuery();
            scc.connection().Close();
            daset.Tables["[user]"].Clear();
            member_Listing();
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is MaskedTextBox)
                {
                    item.Text = "";
                }
            }
            txtUsername.Focus();
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
