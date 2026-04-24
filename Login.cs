using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Dclinic__system
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            UpassTb.Multiline = false;
            UpassTb.UseSystemPasswordChar = true;
            UpassTb.PasswordChar = '*';
        }

        ConnectionString MyConnection = new ConnectionString();

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UnameTb.Text) || string.IsNullOrWhiteSpace(UpassTb.Text))
            {
                MessageBox.Show("Please enter username and password.");
                return;
            }

            try
            {
                using (MySqlConnection Con = MyConnection.GetCon())
                {
                    Con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM UserTbl WHERE Uname=@uname AND Upass=@upass", Con);
                    cmd.Parameters.AddWithValue("@uname", UnameTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@upass", UpassTb.Text.Trim());
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 1)
                    {
                        Appointment App = new Appointment();
                        App.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Wrong username or password.");
                        UnameTb.Text = "";
                        UpassTb.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminLogin log = new AdminLogin();
            log.Show();
            this.Hide();
        }
    }
}
