using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace crime_manag
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\crime.mdf;Integrated Security=True;Connect Timeout=30");
        public static string OffName;
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Rolecb.SelectedIndex==-1)
            {
                MessageBox.Show("Select a Role First");

            }
            else
            {
                if (Rolecb.SelectedIndex == 0)
                {
                    if (UnameTb.Text == "" || PasswordTb.Text == "")
                    {
                        MessageBox.Show("Enter Admin Name and Password");
                    }
                    //Admin is selected
                   else if (UnameTb.Text == "Meaza" && PasswordTb.Text == "0714") {
                        
                        police obj = new police();
                        
                        obj.Show();
                        this.Hide();
                    
                    }
                    else
                    {
                        MessageBox.Show("Wrong Admin Name or Password");
                        UnameTb.Text = "";
                        PasswordTb.Text = "";
                    }
                }
                else
                {
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From PoliceTb1 where EmpName='" + UnameTb.Text + "'and EmpPas='" + PasswordTb.Text+"'",con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        OffName = UnameTb.Text;
                        criminals obj = new criminals();
                        obj.Show();
                        this.Hide();
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("wrong Officer Name or Password");
                        UnameTb.Text = "";
                        PasswordTb.Text = "";
                    }
                    con.Close();
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
