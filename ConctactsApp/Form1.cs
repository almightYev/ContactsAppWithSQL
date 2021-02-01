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

namespace ConctactsApp
{
    public partial class Form1 : Form
    {
        SqlConnection connnection = new SqlConnection("Data Source=DESKTOP-G2ISOTR;Initial Catalog=MyConctactsDB;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        void ListOfContacts()
        {
            if(connnection.State == ConnectionState.Closed)
            {
                connnection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connnection;
                cmd.CommandText = "select * from ContactsDB";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "ContactsDB");

                dataGridView1.DataSource = ds.Tables["ContactsDB"];
                dataGridView1.Columns[0].Visible = false;
                connnection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(connnection.State == ConnectionState.Closed)
            {
                connnection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connnection;
                command.CommandText = "insert into ContactsDB(contactName, contactSurname, contactPhone) values('" + txtboxName.Text + "', '" + txtboxSurname.Text + "', '" + txtboxPhone.Text + "')";
                command.ExecuteNonQuery();
                command.Dispose();
                connnection.Close();
                ListOfContacts();

                MessageBox.Show("Contact has been added");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListOfContacts();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (connnection.State == ConnectionState.Closed)
                {
                    connnection.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connnection;
                    cmd.CommandText = "delete from ContactsDB where contactID=@contactID";
                    cmd.Parameters.AddWithValue("@contactID", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    connnection.Close();
                    ListOfContacts();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(connnection.State == ConnectionState.Closed)
            {
                connnection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connnection;
                cmd.CommandText = "update ContactsDB set contactName='"+txtboxName.Text+"', contactSurname='"+txtboxSurname.Text+"', contactPhone='"+txtboxPhone.Text+"' where contactID=@contactID";
                cmd.Parameters.AddWithValue("@contactID", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                connnection.Close();
                ListOfContacts();
                MessageBox.Show("Updated successfully!");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtboxName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtboxSurname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtboxPhone.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }
    }
}
