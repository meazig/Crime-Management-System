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
    public partial class dashibored : Form
    {
        public dashibored()
        {
            InitializeComponent();
            CountOfficers();
            CountCases();
            CountCriminals();

        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\crime.mdf;Integrated Security=True;Connect Timeout=30");
        private void CountOfficers(){
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From PoliceTb1", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Offnula.Text = dt.Rows[0][0].ToString();
            con.Close();
}
        private void CountCases()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From CaseTb1", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            casela.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void CountCriminals()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From CriminalTb1", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            arrla.Text = dt.Rows[0][0].ToString();
            crimla.Text = dt.Rows[0][0].ToString()+ " Arrested person";
            con.Close();
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            charges obj = new charges();
            obj.Show();
            this.Hide();
        }

        private void dashibored_Load(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
           criminals obj = new criminals();
           obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
           cases obj = new cases();
           obj.Show();
           this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void crimla_Click(object sender, EventArgs e)
        {

        }
    }
}
