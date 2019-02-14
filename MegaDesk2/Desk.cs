using System;
using System.Globalization;

namespace MegaDesk
{
    public enum Materials
    {
        Oak = 200,
        Laminate = 100,
        Pine = 50,
        Rosewood = 300 ,
        Veneer = 125
    }

    /*
    public struct DeskStruct
    {
        string CustomerName;
        int Width;
        int Depth;
        int NumberOfDrawers;
        Materials SurfaceMaterial;
        int ProductionTime;
    }
    */

    public class Desk
    {
        public Desk()
        {
            this.Quote = new DeskQuote(this);
        }

        public Desk(string fileRec)
        {
            string[] split = fileRec.Split('\t');
            this.Quote = new DeskQuote(this);
            this.CustomerName = split[0];
            CultureInfo provider = CultureInfo.InvariantCulture;
            string strDate = split[1];
            DateTime dt;
            if (DateTime.TryParse(strDate, out dt))
            {
                this.Quote.QuoteDate = dt;
            }
            this.Width = Convert.ToInt32(split[2]);
            this.Depth = Convert.ToInt32(split[3]);
            this.SurfaceMaterial = (Materials)Convert.ToInt32(split[4]);
            this.NumberOfDrawers = Convert.ToInt32(split[5]);
            this.ProductionTime = Convert.ToInt32(split[6]);
        }

        public const int MIN_WIDTH = 24;
        public const int MAX_WIDTH = 96;
        public const int MIN_DEPTH = 12;
        public const int MAX_DEPTH = 48;

        public const double BASE_PRICE = 200;
        public const double AREA_SURCHARGE_PER_UNIT = 1;
        public const int AREA_SURCHARGE_THRESHOLD = 1000;
        public const double DRAWER_SURCHARGE = 50;

        public string CustomerName { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }

        public int SurfaceArea
        {
            get
            {
                return this.Width * this.Depth;
            }
        }

        public int NumberOfDrawers { get; set; }
        public Materials SurfaceMaterial { get; set; }
        public int ProductionTime { get; set; }
        public DeskQuote Quote { get; set; }

        public string SurfaceMaterialDescr()
        {
            return this.SurfaceMaterial.ToString();
        }

        public string MakeFileRecord()
        {
            return String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}",
                this.CustomerName,
                this.Quote.QuoteDate.ToString("dd-MMM-yy"),
                this.Width,
                this.Depth,
                (int)this.SurfaceMaterial,
                this.NumberOfDrawers,
                this.ProductionTime);
        }

        public string[] GridRow()
        {
            string[] row = {
                this.CustomerName,
                String.Format("{0,10:dd-MMM-yy}", this.Quote.QuoteDate),
                this.Width.ToString(),
                this.Depth.ToString(),
                this.SurfaceArea.ToString(),
                this.SurfaceMaterialDescr(),
                this.NumberOfDrawers.ToString(),
                this.ProductionTime.ToString() + " days",
                String.Format("{0,10:$0.00}", this.Quote.QuoteAmount)
            };
            return row;
        }
    }
}
