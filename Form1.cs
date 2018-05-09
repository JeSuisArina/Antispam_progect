using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace antispam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static public string client;
        static public string login;
        static public string password;

        private void LogInBut_Click(object sender, EventArgs e)
        {
            client = textBox3.Text;
            login = textBox1.Text;
            password = textBox2.Text;

            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }
    }
}
