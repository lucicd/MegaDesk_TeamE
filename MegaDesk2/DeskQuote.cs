using System;
using System.Globalization;
using System.IO;

namespace MegaDesk
{
    public class DeskQuote
    {

        private const double FIRST_AREA_THRESHOLD = 1000;
        private const double SECOND_AREA_THRESHOLD = 2000;

        private Desk quotedDesk;

        public DeskQuote(Desk myDesk)
        {
            this.quotedDesk = myDesk;
            this.QuoteDate = DateTime.Now;
        }

        public double QuoteAmount
        {
            get
            {
                return this.BasePrice + this.DrawersAddon +
                    this.MaterialAddon + this.SurfaceAddon +
                    this.RushOrderAddon;
            }
        }

        public DateTime QuoteDate { get; set; }

        public double BasePrice
        {
            get
            {
                return Desk.BASE_PRICE;
            }
        }

        public double SurfaceAddon
        {
            get
            {
                int surfaceArea = this.quotedDesk.Width * this.quotedDesk.Depth;
                if (surfaceArea > Desk.AREA_SURCHARGE_THRESHOLD)
                {
                    return (surfaceArea - Desk.AREA_SURCHARGE_THRESHOLD) *
                        Desk.AREA_SURCHARGE_PER_UNIT;
                }
                else
                {
                    return 0;
                }
            }
            
        }

        public double DrawersAddon
        {
            get
            {
                return this.quotedDesk.NumberOfDrawers * Desk.DRAWER_SURCHARGE;
            }
        }

        public double MaterialAddon
        {
            get
            {
                return (double)this.quotedDesk.SurfaceMaterial; 
            }
        }

        public double RushOrderAddon
        {
            get
            {
                int surfaceArea = this.quotedDesk.Width * this.quotedDesk.Depth;

                int row;

                switch (this.quotedDesk.ProductionTime)
                {
                    case 3:
                        row = 0;
                        break;
                    case 5:
                        row = 1;
                        break;
                    case 7:
                        row = 2;
                        break;
                    default:
                        return 0;
                }

                int col;

                if (surfaceArea < FIRST_AREA_THRESHOLD)
                {
                    col = 0;
                }
                else if (surfaceArea <= SECOND_AREA_THRESHOLD)
                {
                    col = 1;
                }
                else
                {
                    col = 2;
                }

                int[,] rushOrderPrices = GetRushOrder();
                return (double)rushOrderPrices[row, col];
            }
        }

        public int[,] GetRushOrder()
        {
            string data = Properties.Resources.rushOrderPrices;
            string[] lines = data.Split(new string[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries);
            int[,] rushPrices = new int[3,3];
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    rushPrices[row, col] = Convert.ToInt32(lines[row * 3 + col]);
                }
            }
            return rushPrices;
        }
    }
}
