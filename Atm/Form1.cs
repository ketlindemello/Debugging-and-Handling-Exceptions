using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atm
{
    
    public partial class Form1 : Form
    {
        Dictionary<string, double> accounts = new Dictionary<string, double>();

        
        public Form1()
        {
            //ExceptionLabel.ForeColor = Color.Red;
            InitializeComponent();


            accounts.Add("40038", 200);
            accounts.Add("30171", 500);
            accounts.Add("87401", 0);
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            VerifyAccount();
                
        }

        public void VerifyAccount()
        {
            
            if (textBoxAccount.TextLength > 0)
            {
                foreach(KeyValuePair<string, double> b in accounts)
                {
                    if (accounts.ContainsKey(textBoxAccount.Text))
                    {
                        double item = (from userAccount in accounts where userAccount.Key == textBoxAccount.Text select userAccount.Value).FirstOrDefault();
                        label5.Text = "$ " + item.ToString();
                        label6.Text = textBoxAccount.Text;
                        label2.Visible = true;
                        label3.Visible = true;
                        textBox1.Visible = true;
                        button2.Visible = true;
                        button1.Visible = true;

                        label7.Text = "";

                    }
                    else
                    {
                        label7.Text = "Account does not exist!";
                        buttonCreate.BackColor = Color.Green;
                    }
                        
                }
            }
            else
            {
                MessageBox.Show("Provide account number.");
            }
        }

        public void UpdateDict(string accountValue, double accounBalance)
        {
            string newAccountValue = accountValue;
            double newBalaceValue = accounBalance;
            accounts[newAccountValue] = newBalaceValue;

            //MessageBox.Show(accounts.ToString());
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {

            ExceptionLabel.Text = "";
            label7.Text = "";
            textBox1.Text = "";
            if(textBoxAccount.TextLength > 0)
            {
                try
                {
                    accounts.Add(textBoxAccount.Text, 0);
                    label5.Text = "$ 0";
                    label6.Text = textBoxAccount.Text;
                    label2.Visible = true;
                    label3.Visible = true;

                    textBox1.Visible = true;
                    button2.Visible = true;
                    button1.Visible = true;
                }
                catch
                {
                    
                    label7.ForeColor = Color.Red; 
                    label7.Text = "Account already exists.";
                }
                
            }
            else
            {
                return;
            }
            


        }

        private double ValidateAmount(TextBox txt)
        {
            double retVal = 0;
            while (double.TryParse(txt.Text, out retVal) == false)
            {
                textBox1.Text = "0.0";
                textBox1.Focus();
            }
            
            return retVal;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExceptionLabel.Text = "";
            
            try
            {
                double depositAmount = ValidateAmount(textBox1);
                BankAccount bankAccount = new BankAccount();
                bankAccount.AccountNumber = textBoxAccount.Text;
                double item = (from userAccount in accounts where userAccount.Key == textBoxAccount.Text select userAccount.Value).FirstOrDefault();
                bankAccount.balance = item;
                bankAccount.Deposit(depositAmount);

                accounts[bankAccount.AccountNumber] = bankAccount.balance;

                double newItem = (from userAccount in accounts where userAccount.Key == textBoxAccount.Text select userAccount.Value).FirstOrDefault();

                label5.Text = "$ " + newItem.ToString();
                textBox1.Clear();

            }
            catch(Exception ex)
            {
                ExceptionLabel.ForeColor = Color.Red;
                ExceptionLabel.Text = ex.Message;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExceptionLabel.Text = "";
            try
            {
                double WithdrawAmount = ValidateAmount(textBox1);
                BankAccount bankAccount = new BankAccount();
                bankAccount.AccountNumber = textBoxAccount.Text;
                double item = (from userAccount in accounts where userAccount.Key == textBoxAccount.Text select userAccount.Value).FirstOrDefault();
                bankAccount.balance = item;
                bankAccount.Withdraw(WithdrawAmount);

                accounts[bankAccount.AccountNumber] = bankAccount.balance;

                double newItem = (from userAccount in accounts where userAccount.Key == textBoxAccount.Text select userAccount.Value).FirstOrDefault();

                label5.Text = "$ " + newItem.ToString();
                textBox1.Clear();

            }
            catch (Exception ex)
            {
                ExceptionLabel.ForeColor = Color.Red;
                ExceptionLabel.Text = ex.Message;
            }
            
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            label3.Visible = false;

            textBox1.Visible = false;
            button2.Visible = false;
            button1.Visible = false;
            label5.Text = "";
            label6.Text = "";
            textBoxAccount.Clear();
            ExceptionLabel.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in accounts)
            {
                sb.AppendFormat("{0}  $ {1}{2}", item.Key, item.Value, Environment.NewLine);
            }

            string result = sb.ToString().TrimEnd();
            MessageBox.Show(result, "Accounts Registered");
        }
    }
}
