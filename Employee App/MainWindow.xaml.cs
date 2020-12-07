using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Employee_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Employee> employee = new ObservableCollection<Employee>();
        public MainWindow()
        {
            InitializeComponent();

            FullTimeEmployee em1 = new FullTimeEmployee("Jess", "Walsh", 50000);
            FullTimeEmployee em2 = new FullTimeEmployee("Joe", "Murphy", 100000);

            PartTimeEmployee em3 = new PartTimeEmployee("John", "Smith", 15, 10);
            PartTimeEmployee em4 = new PartTimeEmployee("Jane", "Jones", 13, 15);
        }
    }
}
