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
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        SqlConnectionClass scc = new SqlConnectionClass();

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            // aynı kullanıcı adını bir daha almamak üzere yazılmış olan bir kod satırı.
            // count kodu kullanıcıların sayısını hesaplar
            SqlCommand countUserCommand = new SqlCommand("select count(*) as sonuc from [User] where Username=LOWER(@p1)", scc.connection());
            countUserCommand.Parameters.AddWithValue("@p1", txtUsername.Text);
            int countUser = 0;

            SqlDataReader dr = countUserCommand.ExecuteReader();    

            while (dr.Read()) 
            {
                countUser = Convert.ToInt32(dr["sonuc"]);
            }
            if (countUser == 0)
            {
                SqlCommand command = new SqlCommand("insert into [User] (Name,Surname,Username,Email,PhoneNumber,Password,IsAdmin) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", scc.connection());
                command.Parameters.AddWithValue("@p1", txtName.Text);
                command.Parameters.AddWithValue("@p2", txtSurname.Text);
                command.Parameters.AddWithValue("@p3", txtUsername.Text);
                command.Parameters.AddWithValue("@p4", txtEmail.Text);
                command.Parameters.AddWithValue("@p5", mskPhoneNumber.Text);    
                command.Parameters.AddWithValue("@p6", txtPassword.Text);
                command.Parameters.AddWithValue("@p7", false); // IsAdmin Otomatik False geliyor.
                command.ExecuteNonQuery();
                scc.connection().Close();

                MessageBox.Show("Kaydınız Gerçekleşmiştir Şifreniz" + txtPassword.Text, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Signİn signİn = new Signİn();
                signİn.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Girmiş Olduğunuz Kullanıcı Adı Daha Önceden Alınmıştır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Signİn signİn = new Signİn();
            signİn.Show();
            this.Hide();
        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }
    }
}
