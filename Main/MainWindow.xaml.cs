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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UsersEntities db = new UsersEntities();
        public MainWindow()
        {
            InitializeComponent();
        }


        //Login button -- Will query for user and continue if found
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            //Query for user
            var query = from s in db.Users
                        where s.Username == tbxLogin.Text && s.Password == tbxPassword.Password
                        select s;


            //Logic to identify if query has found something
            if (query.Any())
            {
                this.Hide();
                MainProg main = new MainProg();
                main.Show();
                
            }
            else
            {
                //Invalid Username or Password
                MessageBox.Show("Invalid Username or password");
            }
        }


        //Will Eit program
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
