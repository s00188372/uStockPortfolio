using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{

    //Class for Stock properties 
    public class Stock : IComparable
    {

        public string CompanyName { get; set; }
        public string Ticker { get; set; }
        public decimal StockPrice { get; set; }

        public decimal Shares { get; set; }

        public string ImagePath { get; set; }

        public decimal Net { get; set; }

        //setting properties
        public Stock(string companyName, string ticker, decimal stockPrice, decimal shares, string imagePath, decimal net)
        {
            CompanyName = companyName;
            Ticker = ticker;
            StockPrice = stockPrice;
            Shares = shares;
            ImagePath = imagePath;
            Net = net;
           

        }

        public Stock()
        {
        }

        //Comparing company names to sort
        public int CompareTo(object obj)
        {
            Stock that = obj as Stock;
            return this.CompanyName.CompareTo(that.CompanyName);
        }




        /*        public string PriceAdjust
                {
                    get
                    {
                        if (Net > 10000)
                        {
                            return "Red";
                        }
                        else
                        {
                            return "White";
                        }
                    }
                }*/


    }
}
