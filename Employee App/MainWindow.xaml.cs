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
            tbxSalary.Clear();
            tbxHoursWorked.Clear();
            tbxHourlyRate.Clear();

            Employee selectedEmployee = lbxEmployees.SelectedItem as Employee;

            if(selectedEmployee != null)
            {
                tbxFirstName.Text = selectedEmployee.FirstName;
                tbxLastName.Text = selectedEmployee.LastName;

                if(selectedEmployee is FullTimeEmployee)
                {
                    FullTimeEmployee selectedFTEmployee = selectedEmployee as FullTimeEmployee;
                    rbFullTime.IsChecked = true;
                    tbxSalary.Text = selectedFTEmployee.Salary.ToString();
                    tblkCalcMonthlyPay.Text = ("€" + selectedFTEmployee.CalculateMonthlyPay().ToString("F"));
                }
                else if (selectedEmployee is PartTimeEmployee)
                {
                    PartTimeEmployee selectedPTEmployee = selectedEmployee as PartTimeEmployee;
                    rbPartTime.IsChecked = true;
                    tbxHoursWorked.Text = selectedPTEmployee.HoursWorked.ToString();
                    tbxHourlyRate.Text = selectedPTEmployee.HourlyRate.ToString();
                    tblkCalcMonthlyPay.Text = ("€" + selectedPTEmployee.CalculateMonthlyPay().ToString("F"));
                }
            }

        }

        private void cboxFullTime_Checked(object sender, RoutedEventArgs e)
        {
            //clears the ObservableCollection
            filteredEmployees.Clear();

            if (cboxFullTime.IsChecked == true && cboxPartTime.IsChecked == true)
            {
                lbxEmployees.ItemsSource = employees;
            }
            else if(cboxFullTime.IsChecked == false && cboxPartTime.IsChecked == false)
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
            //clears every textbox, radio button and tblkCalcMonthlyPay textblock
            tbxFirstName.Clear();
            tbxLastName.Clear();
            rbFullTime.IsChecked = false;
            rbPartTime.IsChecked = false;
            tbxSalary.Clear();
            tbxHoursWorked.Clear();
            tbxHourlyRate.Clear();
            tblkCalcMonthlyPay.Text = "";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //read details from screen
            string firstName = tbxFirstName.Text;
            string lastName = tbxLastName.Text;
            if (rbFullTime.IsChecked == true)
            {
                decimal salary = Convert.ToDecimal(tbxSalary.Text);
                FullTimeEmployee employee = new FullTimeEmployee(firstName, lastName, salary);
                employees.Add(employee);
            }
            else if(rbPartTime.IsChecked == true)
            {
                double hoursWorked = Convert.ToDouble(tbxHoursWorked.Text);
                decimal hourlyRate = Convert.ToDecimal(tbxHourlyRate.Text);
                PartTimeEmployee employee = new PartTimeEmployee(firstName, lastName, hourlyRate, hoursWorked);
                employees.Add(employee);
            }
            //update display manually
            lbxEmployees.ItemsSource = null;
            lbxEmployees.ItemsSource = employees;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //check which employee was selected
            Employee selectedEmployee = lbxEmployees.SelectedItem as Employee;

            if (selectedEmployee != null)
            {
                selectedEmployee.FirstName = tbxFirstName.Text;
                selectedEmployee.LastName = tbxLastName.Text;

                if (selectedEmployee is FullTimeEmployee)
                {
                    FullTimeEmployee selectedFTEmployee = selectedEmployee as FullTimeEmployee;
                    rbFullTime.IsChecked = true;
                    selectedFTEmployee.Salary = Convert.ToDecimal(tbxSalary.Text);
                }
                else if (selectedEmployee is PartTimeEmployee)
                {
                    PartTimeEmployee selectedPTEmployee = selectedEmployee as PartTimeEmployee;
                    rbPartTime.IsChecked = true;
                    selectedPTEmployee.HoursWorked = Convert.ToDouble(tbxHoursWorked.Text);
                    selectedPTEmployee.HourlyRate = Convert.ToDecimal(tbxHourlyRate.Text);
                }
            }
            //update display manually
            lbxEmployees.ItemsSource = null;
            lbxEmployees.ItemsSource = employees;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //check which employee was selected
            Employee selectedEmployee = lbxEmployees.SelectedItem as Employee;

            employees.Remove(selectedEmployee);

            //update display manually
            lbxEmployees.ItemsSource = null;
            lbxEmployees.ItemsSource = employees;
        }
    }
}
