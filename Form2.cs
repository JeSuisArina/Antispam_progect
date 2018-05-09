using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Limilabs.Mail;
using Limilabs.Client.IMAP;
using Lab2TechCloud;

namespace antispam
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private string client = Form1.client;
        private string login = Form1.login;
        private string password = Form1.password;
        private int counter;
        private List<string> canonlist = new List<string>();
        private List<string> canonbody;
        private List<string> Body = new List<string>();
        List<double> percentSpam = new List<double>();
        private List<double> values = new List<double>();
        private Dictionary<string, double> tokens= new Dictionary<string, double>();
        Canonize myCanonize = new Canonize();
        Bayes myBayes = new Bayes();

        private void button1_Click(object sender, EventArgs e)
        {
            using (Imap imap = new Imap())
            {

                imap.ConnectSSL("imap." + client);
                imap.Login(login, password);

                CommonFolders folders = new CommonFolders(imap.GetFolders());
                imap.Select(folders.Trash);

                MailBuilder builder = new MailBuilder();
                counter = 0;
                foreach (long uid in imap.Search(Flag.All))
                {
                    IMail email = builder.CreateFromEml(imap.GetMessageByUID(uid));
                    string body = email.Subject + " " + email.Text;

                    canonbody = new List<string>();
                    canonbody = myCanonize.canonize(body);

                   
                    for (int i = 0; i < canonlist.Count; i++)
                    {
                        for (int j = 0; j < canonbody.Count; j++)
                            if (myCanonize.LevenshteinDistance(canonlist[i], canonbody[j]) < 3)
                            {
                                values.Add(percentSpam.ElementAt(i));
                            }
                    }                    

                    double spam = myBayes.CBayes(values);

                    if (spam > 0.3)
                    {
                        listBox2.Items.Add(body + myBayes.CBayes(values));
                    }
                    else
                    {
                        listBox1.Items.Add(body);
                    }
                    values.Clear();
                }
            }            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            using (Imap imap = new Imap())
            {
                imap.ConnectSSL("imap." + client);
                imap.Login(login, password);

                CommonFolders folders = new CommonFolders(imap.GetFolders());
                imap.Select(folders.Spam);

                MailBuilder builder = new MailBuilder();
                counter = 0;
                foreach (long uid in imap.Search(Flag.All))
                {
                    counter++;
                    IMail email = builder.CreateFromEml(imap.GetMessageByUID(uid));
                    string text = email.Subject + " " + email.Text;
                    List<string> canontext = myCanonize.canonize(text);
                    foreach (string row in canontext)
                    {
                        if (row == "")
                            continue;
                        if (!tokens.ContainsKey(row))
                            tokens.Add(row, 1);
                        else
                            tokens[row]++;
                    }
                }

                tokens = tokens.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                for (int i = 0; i < 15; i++)
                {
                    canonlist.Add(tokens.ElementAt(i).Key);
                    percentSpam.Add(Convert.ToDouble(tokens.ElementAt(i).Value) * 100 / Convert.ToDouble(counter));
                }

            }            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }
    }
}

