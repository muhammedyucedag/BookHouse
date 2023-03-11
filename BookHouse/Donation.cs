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
    public partial class Donation : Form
    {
        int MyUserId = 0;
        public Donation(int userId)
        {
            MyUserId = userId;
            InitializeComponent();
        }

        SqlConnectionClass scc = new SqlConnectionClass();
        private void btnDonate_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bilgilerini Girmiş Olduğunuz Kitabı Bağışlamak İstiyor musunuz? ", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            int typeId = 0;
            SqlCommand commandTypeId = new SqlCommand("select Id from Type where TypeName = @p1", scc.connection());
            commandTypeId.Parameters.AddWithValue("@p1", cmbType.SelectedItem.ToString());
            SqlDataReader dr = commandTypeId.ExecuteReader();
            while (dr.Read())
            {
                typeId = Convert.ToInt32(dr[0]);
            }

            SqlCommand command = new SqlCommand("insert into Donation (name ,publisher,author,pagenumber,date,TypeId,UserId) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", scc.connection());

            command.Parameters.AddWithValue("@p1", txtName.Text);
            command.Parameters.AddWithValue("@p2", txtPublisher.Text);
            command.Parameters.AddWithValue("@p3", txtAuthor.Text);
            command.Parameters.AddWithValue("@p4", Convert.ToInt32(txtPageNumber.Text));
            command.Parameters.AddWithValue("@p5", DateTime.Now.Date);
            command.Parameters.AddWithValue("@p6", typeId);
            command.Parameters.AddWithValue("@p7", MyUserId);
            command.ExecuteNonQuery();
            scc.connection().Close();
            MessageBox.Show("Kitabınız Başarılı Bir Şekilde Bağışlanmıştır", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void Donation_Load(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select typename from type", scc.connection());
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                cmbType.Items.Add(dr[0]);
            }
            scc.connection().Close();
        }

        private void btnHome_Click_1(object sender, EventArgs e)
        {
            Store su = new Store(MyUserId);
            su.Show();
            this.Hide();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAuthor.Clear();
            txtName.Clear();
            txtPageNumber.Clear();
            txtPublisher.Clear();
            cmbType.Items.Remove(cmbType.SelectedItem);
            cmbType.Focus();

        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
