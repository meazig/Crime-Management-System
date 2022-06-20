using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace crime_manag
{
    public partial class police : Form
    {
        public police()
        {
            InitializeComponent();
            ShowPolice();
            Rest();
            CountOffi();
        }
    SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\crime.mdf;Integrated Security=True;Connect Timeout=30");
    private void ShowPolice()

    {
        con.Open();
        string Query = "Select * from PoliceTb1";
        SqlDataAdapter adp = new SqlDataAdapter(Query, con);
        SqlCommandBuilder builder = new SqlCommandBuilder(adp);
        var ds = new DataSet();
        adp.Fill(ds);
        policeDGV.DataSource = ds.Tables[0];

        con.Close();
    }
    private void label4_Click(object sender, EventArgs e)
        {

        }

        private void police_Load(object sender, EventArgs e)
        {

        }
        private void CountOffi()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From CriminalTb1", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            label9.Text = dt.Rows[0][0].ToString() + " Officers";
            con.Close();
        }
            private void DetailTb_TextChanged(object sender, EventArgs e)
        {

        }
        private void Rest()
        {
            NameTb.Text = "";
            AddressTb.Text = "";
            PhoneTb1.Text = "";
            desTb.SelectedIndex= -1;
            PassTb.Text = "";
            AgeTB.Text = "";
            SexTb1.Text = "";
            key = 0;
         
        }
        private void RecordBtn_Click(object sender, EventArgs e)
        {
            string name = @"^[a-zA-Z]+$";
            string Age = @"^[2-9][0-9]|100+$";
            string phone = @"^[+]251[0-9]{9}|09[0-9]{8}";
            if (NameTb.Text == "" || AddressTb.Text == "" || PassTb.Text == "" || AgeTB.Text == "" || SexTb1.Text == "" || desTb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                if (Regex.IsMatch((String)NameTb.Text, name) && Regex.IsMatch((String)AgeTB.Text, Age) && Regex.IsMatch((String)PhoneTb1.Text, phone))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("insert into PoliceTb1(EmpName,EmpAddress,EmpPhone,EmpDes,EmpPas,EmpSex,EmpAge)values(@EName,@EAddress,@Ep,@Eds,@Epass,@ESex,@EAge)", con);

                        cmd.Parameters.AddWithValue("@EName", NameTb.Text);
                        cmd.Parameters.AddWithValue("@EAddress", AddressTb.Text);
                        cmd.Parameters.AddWithValue("@Ep", PhoneTb1.Text);
                        cmd.Parameters.AddWithValue("@Eds", desTb.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Epass", PassTb.Text);
                        cmd.Parameters.AddWithValue("@EAge", AgeTB.Text);
                        cmd.Parameters.AddWithValue("@ESex", SexTb1.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("staff is Recorded!");
                        con.Close();
                        ShowPolice();
                        Rest();


                    }
                    catch (Exception Ex)
                    {

                        MessageBox.Show(Ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("invalid age or name format");
                }
            }
        
            }
        int key = 0;
        private void policeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
                try
                {


                    NameTb.Text = policeDGV.CurrentRow.Cells[1].Value.ToString();
                    AddressTb.Text = policeDGV.CurrentRow.Cells[2].Value.ToString();
                    PhoneTb1.Text = policeDGV.CurrentRow.Cells[3].Value.ToString();
                    desTb.SelectedItem = policeDGV.CurrentRow.Cells[4].Value.ToString();
                    PassTb.Text = policeDGV.CurrentRow.Cells[5].Value.ToString();
                    SexTb1.Text = policeDGV.CurrentRow.Cells[6].Value.ToString();
                    AgeTB.Text = policeDGV.CurrentRow.Cells[7].Value.ToString();
                    if (NameTb.Text == "")
                    {
                        key = 0;
                    }
                    else
                    {
                        key = Convert.ToInt32(policeDGV.CurrentRow.Cells[0].Value.ToString());
                    }

                }
                catch (Exception Ex)

                {
                    MessageBox.Show(Ex.Message);
                }
            
            
            
        }
            private void CancelBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Please Select police officer!!!");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from PoliceTb1 Where EmpCode=@PKey", con);
                    cmd.Parameters.AddWithValue("@PKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Are you sure informatin is deleted!");
                    con.Close();
                    ShowPolice();
                    Rest();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "" || AddressTb.Text == "" || PassTb.Text == "" || AgeTB.Text == "" || SexTb1.Text == "" || desTb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update  PoliceTb1 Set EmpName= @EName,EmpAddress= @EAddress,EmpPhone= @Ep,EmpDes= @Eds,EmpPas=@Epass,EmpSex=@ESex,EmpAge= @EAge where EmpCode=@PKey", con);
                    cmd.Parameters.AddWithValue("@PKey", key);
                    cmd.Parameters.AddWithValue("@EName", NameTb.Text);
                    cmd.Parameters.AddWithValue("@EAddress", AddressTb.Text);
                    cmd.Parameters.AddWithValue("@Ep", PhoneTb1.Text);
                    cmd.Parameters.AddWithValue("@Eds", desTb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Epass", PassTb.Text);
                    cmd.Parameters.AddWithValue("@EAge", AgeTB.Text);
                    cmd.Parameters.AddWithValue("@ESex", SexTb1.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("staff is edited");
                    con.Close();
                    ShowPolice();
                    Rest();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void label7_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            dashibored obj = new dashibored();
            obj.Show();
            this.Hide();
        }

        private void label4_Click_1(object sender, EventArgs e)
        {
            cases obj = new cases();
            obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            criminals obj = new criminals();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            charges obj = new charges();
            obj.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
    }
}
