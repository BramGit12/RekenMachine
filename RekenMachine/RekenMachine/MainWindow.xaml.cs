using RekenMachine.Classes;
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

namespace RekenMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Calculator calculator = new Calculator();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GetInput(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string input = btn.Content.ToString();
            calculator.RecieveInput(input);
            Display.Text = calculator.TotalString;
            Result.Text = calculator.result.ToString();
        }
    }
}
