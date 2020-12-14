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
        List<Employee> employees = new List<Employee>();
        ObservableCollection<Employee> filteredEmployees = new ObservableCollection<Employee>();
        public MainWindow()
        {
            InitializeComponent();
            cboxFullTime.IsChecked = true;
            cboxPartTime.IsChecked = true;

            FullTimeEmployee em1 = new FullTimeEmployee("Jess", "Walsh", 50000);
            FullTimeEmployee em2 = new FullTimeEmployee("Joe", "Murphy", 100000);

            PartTimeEmployee em3 = new PartTimeEmployee("John", "Smith", 15, 10);
            PartTimeEmployee em4 = new PartTimeEmployee("Jane", "Jones", 13, 15);

            employees.Add(em1);
            employees.Add(em2);
            employees.Add(em3);
            employees.Add(em4);

            employees.Sort();

            lbxEmployees.ItemsSource = employees;
        }

        private void lbxEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee selectedEmployee = lbxEmployees.SelectedItem as Employee;

            if(selectedEmployee != null)
            {
                tbxFirstName.Text = selectedEmployee.FirstName;
                tbxLastName.Text = selectedEmployee.LastName;

                if(selectedEmployee is FullTimeEmployee)
                {
                    rbFullTime.IsChecked = true;
                    //tbxSalary.Text = selectedEmployee;
                }
                else if (selectedEmployee is PartTimeEmployee)
                {
                    rbPartTime.IsChecked = true;
                    //tbxSalary.Text = selectedEmployee;
                }
            }

        }

        private void cboxFullTime_Checked(object sender, RoutedEventArgs e)
        {
            filteredEmployees.Clear();

            //find out what was selected
            if (cboxFullTime.IsChecked == true && cboxPartTime.IsChecked == true)
            {
                lbxEmployees.ItemsSource = employees;
            }
            else if(cboxFullTime.IsChecked != true && cboxPartTime.IsChecked != true)
            {
                lbxEmployees.ItemsSource = null;
            }
            else
            {
                if (cboxFullTime.IsChecked == true && cboxPartTime.IsChecked == false)
                {
                    foreach(Employee emp in employees)
                    {
                        if(emp is FullTimeEmployee)
                        {
                            filteredEmployees.Add(emp);
                            lbxEmployees.ItemsSource = filteredEmployees;
                        }
                    }
                }
                else if (cboxPartTime.IsChecked == true && cboxFullTime.IsChecked == false)
                {
                    foreach (Employee emp in employees)
                    {
                        if (emp is PartTimeEmployee)
                        {
                            filteredEmployees.Add(emp);
                            lbxEmployees.ItemsSource = filteredEmployees;
                        }
                    }
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbxFirstName.Clear();
            tbxLastName.Clear();
            rbFullTime.IsChecked = false;
            rbPartTime.IsChecked = false;
            tbxSalary.Clear();
            tbxHoursWorked.Clear();
            tbxHourlyRate.Clear();
            tblkCalcMonthlyPay.Text = "";
        }
    }
}
