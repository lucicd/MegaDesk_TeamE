using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MegaDesk
{
    public partial class DisplayQuote : Form
    {
        private Desk quote;

        public DisplayQuote(Desk quote)
        {
            InitializeComponent();
            this.quote = quote;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void ShowQuote()
        {
            lblCustomerName.Text = quote.CustomerName;
            lblDeskWidth.Text = quote.Width.ToString();
            lblDeskDepth.Text = quote.Depth.ToString();
            lblSurfaceArea.Text = quote.SurfaceArea.ToString();
            lblNumberOfDrawers.Text = quote.NumberOfDrawers.ToString();
            lblSurfaceMaterial.Text = quote.SurfaceMaterialDescr();
            lblProductionTime.Text = quote.ProductionTime.ToString() + " days";

            lblOrderDate.Text = String.Format("{0,10:dd-MMM-yy}", quote.Quote.QuoteDate);
            lblBasePrice.Text = String.Format("{0,10:$0.00}", quote.Quote.BasePrice);
            lblAreaSurcharge.Text = String.Format("{0,10:$0.00}", quote.Quote.SurfaceAddon);
            lblMaterialSurcharge.Text = String.Format("{0,10:$0.00}", quote.Quote.MaterialAddon);
            lblDrawersSurcharge.Text = String.Format("{0,10:$0.00}", quote.Quote.DrawersAddon);
            lblRushOrderSurcharge.Text = String.Format("{0,10:$0.00}", quote.Quote.RushOrderAddon);
            lblTotalPrice.Text = String.Format("{0,10:$0.00}", quote.Quote.QuoteAmount);
        }

        private void DisplayQuote_Shown(object sender, EventArgs e)
        {
            ShowQuote();
        }

        private void DisplayQuote_FormClosed(object sender, FormClosedEventArgs e)
        {
            var nextForm = (Form)Tag;
            nextForm.Show();
        }
    }
}
