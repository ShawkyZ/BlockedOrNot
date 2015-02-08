using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace BlockedOrNot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void getIDInfo()
        {
            if (System.Uri.IsWellFormedUriString(txtEmail.Text, UriKind.Absolute))
            {
                //TODO: Get The User ID From The URL
                string ID = txtEmail.Text.Substring(txtEmail.Text.IndexOf("facebook.com/")).Replace("facebook.com/", "");
                if (ID.Contains("profile.php"))
                    ID=ID.Replace("profile.php?id=", "");
                Invoke(new Action(()=>
                {
                    label4.Hide();
                    lblStatus.Text = "Loading...";
                    button1.Enabled = false;
                    button2.Enabled = false;
                }));
                //TODO: Get The Facebook Graph Response
                string response = "error";
                try
                {
                    WebClient wb = new WebClient();
                    response = wb.DownloadString("http://graph.facebook.com/" + ID);
                }
                catch { }
                if (response.Contains("error"))
                {
                    Invoke(new Action(() =>
                    {
                        button1.Enabled = true;
                        button2.Enabled = true;
                        lblStatus.Text = "The Account is Deactivated.";
                        label4.Show();
                    }));
                }
                else
                {
                    Invoke(new Action(() =>
                    {
                        button1.Enabled = true;
                        button2.Enabled = true;
                        lblStatus.Text = "The User Blocked You.";
                        label4.Show();
                    }));
                }
            }
            else
            {
                MessageBox.Show("The Account URL is Not Well Formed", "Hey!", MessageBoxButtons.OK);
            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //TODO:Run The Check On Different Thread
            Thread th = new Thread(getIDInfo);
            th.Start();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            GetID gti = new GetID();
            gti.ShowDialog();
            if (GetID.MessageID!=""&&GetID.MessageID!=null)
            {
                txtEmail.Text = "https://facebook.com/profile.php?id="+GetID.MessageID;
                button1_Click(sender, e);
            }
        }

        private void sourceCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/ShawkyZ/BlockedOrNot");
        }
    }
}
