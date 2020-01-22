using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PjsipDialer
{
    public partial class AccountForm : Form
    {
        public PjsipAccount Account;

        public AccountForm()
        {
            InitializeComponent();
        }

        private bool ValidateForm()
        {
            return !string.IsNullOrWhiteSpace(tbHost.Text) &&
                !string.IsNullOrWhiteSpace(tbUserName.Text) &&
                !string.IsNullOrWhiteSpace(tbPassword.Text);
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            tbHost.Text = Account.Host;
            tbUserName.Text = Account.UserName;
            tbPassword.Text = Account.Password;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            Account.SetConfig(tbHost.Text, tbUserName.Text, tbPassword.Text);
        }

        private void tbHost_TextChanged(object sender, EventArgs e)
        {
            btOK.Enabled = ValidateForm();
        }

        private void tbUserName_TextChanged(object sender, EventArgs e)
        {
            btOK.Enabled = ValidateForm();
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            btOK.Enabled = ValidateForm();
        }
    }
}
