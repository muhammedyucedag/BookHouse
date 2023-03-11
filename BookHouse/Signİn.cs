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
    public partial class Signİn : Form
    {
        int MyUserId = 0;
        public Signİn()

        {
            InitializeComponent();
        }

        SqlConnectionClass scc = new SqlConnectionClass();

        private void lnkSignup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            SignUp signUp = new SignUp();
            signUp.Show();
            this.Hide();

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            SqlCommand command = new SqlCommand($"select * from [User] WHERE CAST(Username as binary) = CAST('{txtUsername.Text}' as binary) and Password = '{txtPassword.Text}'", scc.connection());

            if (command.ExecuteScalar() == null)
            {
                MessageBox.Show("Hatalı Kullanıcı Adı & Şifre Girişi Yapıldı");
                return;
            }
            else
            {
                MyUserId = (int)command.ExecuteScalar();
            }
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                Store st = new Store(MyUserId);
                st.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı T.C. & Şifre Girişi Yapıldı");
            }
            scc.connection().Close();


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Girmiş olduğumuz şifreyi gösterme işlemi
            if (chkPasswordsee.CheckState == CheckState.Checked)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            else if (chkPasswordsee.CheckState == CheckState.Unchecked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        private void btnPurchased_Click(object sender, EventArgs e)
        {
            Purchased prh = new Purchased(MyUserId);
            prh.Show();
            this.Hide();
        }

        private void Signİn_Load(object sender, EventArgs e)
        {

        }
    }
}
