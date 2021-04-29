using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Net;
using Microsoft.Data.Analysis;
using Newtonsoft.Json;
using System.Data;
using ServiceStack;
using ServiceStack.Text;


/*#########################################################################################################################################################################
 * 
 *                                                                       ILLIA SHAKIN S00188372
 *                                                                    YEAR 2 SEMESTER 2 FINAL PROJECT
 *                                                                           STOCK WATCHLIST
 * 
 * 
 * 
 #########################################################################################################################################################################*/

namespace Main
{
    /// <summary>
    /// Interaction logic for MainProg.xaml
    /// </summary>
    public partial class MainProg : Window
    {
        //Initialising List 
        public List<Stock> stocks;
        public MainProg()
        {
            InitializeComponent();
        }


        //Class of variables to store Json data
        public class AlphaVantageData
        {
            public DateTime Timestamp { get; set; }
            public decimal Open { get; set; }

            public decimal High { get; set; }
            public decimal Low { get; set; }

            public decimal Close { get; set; }
            public decimal Volume { get; set; }
        }


        //----------------------------------------------- API ----------------------------------------------------

        //Method to Seacrh for daily time series
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var symbol = tbxTicker.Text;
                var ticker = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey=3R3FA5MSN0CNP0IQ&datatype=csv"
                           .GetStringFromUrl().FromCsv<List<AlphaVantageData>>();

                dtaGrid.ItemsSource = ticker;
            }
            catch
            {
                MessageBox.Show("Invalid Ticker please try again");
            }




        }

        //Method to Seacrh for Weekly time series
        private void btnSearch2_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var symbol = tbxTicker.Text;
                var ticker = $"https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY&symbol={symbol}&apikey=3R3FA5MSN0CNP0IQ&datatype=csv"
                           .GetStringFromUrl().FromCsv<List<AlphaVantageData>>();

                dtaGridWeek.ItemsSource = ticker;
            }
            catch
            {
                MessageBox.Show("Invalid Ticker please try again");
            }
        }

        //Method to Seacrh for Monthly time series
        private void btnSearch3_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var symbol = tbxTicker.Text;
                var ticker = $"https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY&symbol={symbol}&apikey=3R3FA5MSN0CNP0IQ&datatype=csv"
                           .GetStringFromUrl().FromCsv<List<AlphaVantageData>>();

                dtaGridMonth.ItemsSource = ticker;
            }
            catch
            {
                MessageBox.Show("Invalid Ticker please try again");
            }



        }




        //--------------------------------------------------------------------------------------------------------------



        //Initialising Objects on Window Loaded
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            //Creating
            stocks = new List<Stock>();
            Random random = new Random();
            Stock AAPL = new Stock() { Ticker = "AAPL", CompanyName = "Apple", StockPrice = 137, Shares = random.Next(0, 100), ImagePath = "images/Apple.png", Net = 0 };
            Stock TSLA = new Stock() { Ticker = "TSLA", CompanyName = "Tesla", StockPrice = 700, Shares = random.Next(0, 100), ImagePath = "images/Tesla.png", Net = 0 };
            Stock FB = new Stock() { Ticker = "FB", CompanyName = "Facebook", StockPrice = 329, Shares = random.Next(0, 100), ImagePath = "https://1000logos.net/wp-content/uploads/2021/04/Facebook-logo.png", Net = 0 };
            Stock NOK = new Stock() { Ticker = "NOK", CompanyName = "Nokia", StockPrice = 5, Shares = random.Next(0, 100), ImagePath = "https://1000logos.net/wp-content/uploads/2017/03/Nokia-Logo.png", Net = 0 };

            //Adding To list
            stocks.Add(AAPL);
            stocks.Add(TSLA);
            stocks.Add(FB);
            stocks.Add(NOK);

            listBox.ItemsSource = stocks;

            //Sorting
            stocks.Sort();

        }

        //Fill out texboxes on selection change of the listbox
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Stock selectedStock = listBox.SelectedItem as Stock;

            //Importing details into Textboxes
            if(selectedStock != null)
            {
                tbxTicker1.Text = selectedStock.Ticker;
                tbxName.Text = selectedStock.CompanyName;
                tbxPrice.Text = selectedStock.StockPrice + "$".ToString();
                tbxShares.Text = selectedStock.Shares.ToString();
                net.Text = (selectedStock.StockPrice * selectedStock.Shares + "$").ToString();
                tbximg.Text = selectedStock.ImagePath;
            }

        }



        //Button Save -- will allow for changes to be saved to a already existing stock
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Stock slStock = listBox.SelectedItem as Stock;

            //Exception handling to prevent crashing on invalid input
            try
            {
                if (slStock != null)
                {
                    slStock.Ticker = tbxTicker1.Text;
                    slStock.CompanyName = tbxName.Text;
                    slStock.StockPrice = decimal.Parse(tbxPrice.Text);
                    slStock.Shares = decimal.Parse(tbxShares.Text);
                    slStock.ImagePath = tbximg.Text;
                    listBox.Items.Refresh();
                }
            }
            // Most specific:
            catch (Exception c)
            {
                MessageBox.Show($"Invalid Input");
            }


        }

        //Button Exit  -- Will exit program
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Button Remove -- Will remove the selected ticker from listbox
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Stock slStock = listBox.SelectedItem as Stock;

            //Removing and setting inputs empty
            if(slStock != null)
            {
                stocks.Remove(slStock);
                listBox.ItemsSource = null;
                listBox.ItemsSource = stocks;

                tbxTicker1.Text = "";
                tbxName.Text = "";
                tbxPrice.Text = "";
                tbxShares.Text = "";
                net.Text = "";
                tbximg.Text = "";


            }

            //Sorting after removing stock
            stocks.Sort();


        }

        //Button Add -- Will add a new stock
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            //Exception handing to prevent crashes on invalid input
            try
            {
                string ticker = tbxTicker1.Text;
                string companyName = tbxName.Text;
                decimal price = decimal.Parse(tbxPrice.Text);
                decimal shares = decimal.Parse(tbxShares.Text);
                string text = tbximg.Text;

                Stock st = new Stock()
                {
                    Ticker = ticker,
                    CompanyName = companyName,
                    StockPrice = price,
                    Shares = shares,
                    ImagePath = text

                };

                stocks.Add(st);
                stocks.Sort();

                listBox.ItemsSource = null;
                listBox.ItemsSource = stocks;
            }
            // Most specific:
            catch (Exception c)
            {
                MessageBox.Show($"Invalid Input");
            }



        }

    }
}
