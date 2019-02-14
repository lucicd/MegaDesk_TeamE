using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MegaDesk
{
    public partial class ViewAllQuotes : Form
    {
        public ViewAllQuotes()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ViewAllQuotes_FormClosed(object sender, FormClosedEventArgs e)
        {
            var mainMenu = (MainMenu)Tag;
            mainMenu.Show();
        }

        private void ViewAllQuotes_Load(object sender, EventArgs e)
        {
            List<Desk> quotes = Program.Quotes.GetAll();
            foreach (Desk quote in quotes)
            {
                dataGridView1.Rows.Add(quote.GridRow());
            }
        }
    }
}
