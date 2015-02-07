using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BlockedOrNot
{
    public partial class GetID : Form
    {
        public static string MessageID { get; set; }
        public GetID()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TODO: Check If The URL Is Well Formed
            if (System.Uri.IsWellFormedUriString(txtMsg.Text, UriKind.Absolute)&&txtMsg.Text.Contains("messages"))
            {
                //TODO: Get Only The User ID From The URL
                string temp = txtMsg.Text.Substring(txtMsg.Text.IndexOf("messages/")).Replace("messages/","");
                MessageID = Regex.Match(temp, @"\d+").Value;
                this.Hide();
            }
            else
            {
                if (!System.Uri.IsWellFormedUriString(txtMsg.Text, UriKind.Absolute))
                {
                    MessageBox.Show("The Message URL is Not Well Formed", "Hey!", MessageBoxButtons.OK);
                }
            }
        }

        private void GetID_Load(object sender, EventArgs e)
        {

        }
    }
}
