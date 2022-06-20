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
using System.Text.RegularExpressions;
namespace crime_manag
{
    public partial class criminals : Form
    {
        public criminals()
        {
            InitializeComponent();
            ShowCriminals();
            OffNameLal.Text = Login.OffName;
            Reset();
            CountCrimin();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\crime.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowCriminals()

        {
            con.Open();
            string Query = "Select * from CriminalTb1 ";
            SqlDataAdapter adp = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            var ds = new DataSet();
            adp.Fill(ds);
            CriminalDGV.DataSource = ds.Tables[0];

            con.Close();
        }

        private void RecordBtn_Click(object sender, EventArgs e)
        {
            string name = @"^[a-zA-Z]+$";
            string Age = @"^[2-9][0-9]|100+$";
            if (NameTb.Text == "" || AddressTb.Text == "" || ActivityTb.Text == "" || AgeTb.Text == "" || SexTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                if (Regex.IsMatch((String)NameTb.Text, name) && Regex.IsMatch((String)AgeTb.Text, Age))
                        {

                    try
                    {


                        con.Open();
                        SqlCommand cmd = new SqlCommand("insert into criminalTb1(CrName,CrAddress,CrActivites,CrAge,CrSex)values(@CName,@CAddress,@CActivites,@CAge,@CSex)", con);

                        cmd.Parameters.AddWithValue("@CName", NameTb.Text);
                        cmd.Parameters.AddWithValue("@CAddress", AddressTb.Text);
                        cmd.Parameters.AddWithValue("@CActivites", ActivityTb.Text);
                        cmd.Parameters.AddWithValue("@CAge", AgeTb.Text);
                        cmd.Parameters.AddWithValue("@CSex", SexTb.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Criminal Recorded!");
                        con.Close();
                        ShowCriminals();
                        Reset();
                    }
                    catch (Exception Ex)
                    {

                        MessageBox.Show(Ex.Message);
                    } }
            else
            {
                    MessageBox.Show("invalid age or name format");
            }
            }
        }

        int key = 0;
        private void CriminalDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                
                NameTb.Text = CriminalDGV.CurrentRow.Cells[1].Value.ToString();
                AddressTb.Text = CriminalDGV.CurrentRow.Cells[2].Value.ToString();
                ActivityTb.Text = CriminalDGV.CurrentRow.Cells[3].Value.ToString();
                AgeTb.Text = CriminalDGV.CurrentRow.Cells[4].Value.ToString();
                SexTb.Text = CriminalDGV.CurrentRow.Cells[5].Value.ToString();
                if (NameTb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(CriminalDGV.CurrentRow.Cells[0].Value.ToString());
                }

            }
            catch (Exception Ex)

            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void EditTb_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "" || AddressTb.Text == "" || ActivityTb.Text == "" || AgeTb.Text == "" || SexTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update criminalTb1 set CrName=@CName, CrAddress=@CAddress,CrActivites=@CActivites,CrAge=@CAge,CrSex=@CSex Where CrCode=@Ckey", con);
                    cmd.Parameters.AddWithValue("@Ckey", key);
                    cmd.Parameters.AddWithValue("@CName", NameTb.Text);
                    cmd.Parameters.AddWithValue("@CAddress", AddressTb.Text);
                    cmd.Parameters.AddWithValue("@CActivites", ActivityTb.Text);
                    cmd.Parameters.AddWithValue("@CAge", AgeTb.Text);
                    cmd.Parameters.AddWithValue("@CSex", SexTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Are you sure informatin is edited!");
                    con.Close();
                    ShowCriminals();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void Reset() {
            
            NameTb.Text = "";
            AddressTb.Text = "";
            ActivityTb.Text = "";
            AgeTb.Text = "";
            SexTb.Text = "";
            key = 0;
        }
        private void CancelBtn_Click(object sender, EventArgs e)
        {

            if (key==0)
            {
                MessageBox.Show("Please Select a Criminals!!!");
            }
            else
            {
                try
                {
                    con.Open();
                   string deletekey = deletetext.Text;
                    SqlCommand cmd = new SqlCommand("Delete from criminalTb1 Where CrCode=@deletekey", con);
                    cmd.Parameters.AddWithValue("@deletekey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Are you sure informatin is deleted!");
                    con.Close();
                    ShowCriminals();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void AddressTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void crlogBtn_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            dashibored obj = new dashibored();
            obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            cases obj = new cases();
            obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            charges obj = new charges();
            obj.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }
        private void CountCriminals()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From CriminalTb1", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
          
            crminallab.Text = dt.Rows[0][0].ToString() + " Arrested person";
            con.Close();
        }
        private void CountCrimin()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From CriminalTb1", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            crminallab.Text = dt.Rows[0][0].ToString() + " Criminals";
            con.Close();
        }

        private void NameTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}   


