using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlockedOrNot
{
    public partial class Form1 : Form
    {
        #region Checking CONSTANTS
        const string ARABIC_CHECK = "هل أنت مستخدم جديد في فيس بوك؟";
        const string ARABIC_NOT_FOUND = "لم يتم العثور على الصفحة";
        const string ARABIC_HOME = "الصفحة الرئيسية";
        const string ENGLISH_CHECK = "New to Facebook?";
        const string ENGLISH_NOT_FOUND = "Page Not Found";
        const string ENGLISH_HOME = "Home";

        #endregion
        bool clicked = false;
        const string EMAIL = "thewhiteknight9@hotmail.com";
        const string PASSWORD = "AppForTest";
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("mbasic.facebook.com");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.ReadyState == WebBrowserReadyState.Complete)
            {
                //TODO: Login To Facebook Using Test Account
                if (webBrowser1.DocumentText.Contains(ENGLISH_CHECK)||webBrowser1.DocumentText.Contains(ARABIC_CHECK))
                {
                    var elems = webBrowser1.Document.GetElementsByTagName("input");
                    foreach (HtmlElement elem in elems)
                    {
                        if (elem.GetAttribute("name") == "login")
                        {
                            elem.InvokeMember("click");
                        }
                        else if ((elem.GetAttribute("name") == "email"))
                        {
                            elem.InnerText = EMAIL;
                        }
                        else if ((elem.GetAttribute("name") == "pass"))
                        {
                            elem.InnerText = PASSWORD;
                        }
                    }
                }
                    //TODO: Check If The User Blocked You Or The Account Was Deactivated.
                else if (clicked && (webBrowser1.DocumentText.Contains(ENGLISH_NOT_FOUND) || webBrowser1.DocumentText.Contains(ARABIC_NOT_FOUND)))
                {
                    button1.Enabled = true;
                    button2.Enabled = true;
                    lblStatus.Text = "The Account is Deactivated.";
                    label4.Show();
                }
                else if (clicked && (!webBrowser1.DocumentText.Contains(ENGLISH_NOT_FOUND) && !webBrowser1.DocumentText.Contains(ARABIC_NOT_FOUND)))
                {
                    button1.Enabled = true;
                    button2.Enabled = true;
                    lblStatus.Text = "The User Blocked You.";
                    label4.Show();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //TODO: Check If The Page is Loaded Successfully and The URL is Correct
            if ((webBrowser1.DocumentText.Contains(ENGLISH_HOME) || webBrowser1.DocumentText.Contains(ARABIC_HOME)) && System.Uri.IsWellFormedUriString(txtEmail.Text, UriKind.Absolute)&&txtEmail.Text.ToLower().Contains("facebook.com"))
            {
                label4.Hide();
                webBrowser1.Navigate(txtEmail.Text);
                lblStatus.Text = "Loading...";
                clicked = true;
                button1.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                if (!System.Uri.IsWellFormedUriString(txtEmail.Text,UriKind.Absolute) || !txtEmail.Text.ToLower().Contains("facebook.com"))
                {
                    MessageBox.Show("The Account URL is Not Well Formed", "Hey!", MessageBoxButtons.OK);
                }
                else if (!webBrowser1.DocumentText.Contains(ENGLISH_HOME) && !webBrowser1.DocumentText.Contains(ARABIC_HOME))
                {
                    MessageBox.Show("Try Again after a few seconds..Your internet speed is pretty slow.", "Hey!", MessageBoxButtons.OK);
                }
            }
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
