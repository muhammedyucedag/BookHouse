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
    public partial class Store : Form
    {
        int MyUserId=0;
        bool isAdmin = false; // Bool veri tipi "Doğru" veya "Yanlış" olmak üzere iki değer alabilen mantıksal bir veri tipidir. Daha çok kontrol işlemlerinde kullanılır.
        public Store(int userId)
        {
            MyUserId = userId;
            InitializeComponent();

        }

        SqlConnectionClass scc = new SqlConnectionClass();

        private void Store_Load(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select Username,IsAdmin from [User] where Id=@p1", scc.connection());
            command.Parameters.AddWithValue("@p1", MyUserId);
            SqlDataReader dr = command.ExecuteReader();


            while (dr.Read())
            {
                lblkullaniciadi.Text = dr[0].ToString();
                isAdmin = Convert.ToBoolean(dr[1]);
            }
            scc.connection().Close();

            AdminToolStrip.Visible = isAdmin;
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            BuyBook bb = new BuyBook(this.MyUserId);
            bb.Show();
            this.Hide();
        }

        private void btnDonation_Click(object sender, EventArgs e)
        {
            Donation dt = new Donation(MyUserId);
            dt.Show();
            this.Hide();
        }


        private void btnSignin_Click(object sender, EventArgs e)
        {
            Signİn si = new Signİn();
            si.Show();
            this.Hide();
        }

        private void çıkışYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Signİn si = new Signİn();
            si.Show();
            this.Hide();
        }

        private void bağışlananKitaplarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DonationForm df = new DonationForm(MyUserId);
            df.Show();
            this.Hide();
        }

        private void adminİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void kitapEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addBook ab = new addBook();
            ab.Show();
        }

        private void üyeListelemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            memberListing ml = new memberListing();
            ml.Show();
        }

        private void satışListeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
