/*###############################################################################################
 * Luke Sweeney, S00197749, 10/12/2020
 * CA2, Program that displays employees in a listbox and has filter functionality as well as add,
 * delete, update functionality.
 ################################################################################################*/
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
        //create one list and one ObservableCollection of type Employee
        List<Employee> employees = new List<Employee>();
        ObservableCollection<Employee> filteredEmployees = new ObservableCollection<Employee>();
        public MainWindow()
        {
            InitializeComponent();
            //makes both checkboxes checked upon initialization
            cboxFullTime.IsChecked = true;
            cboxPartTime.IsChecked = true;

            //create objects
            FullTimeEmployee em1 = new FullTimeEmployee("Jess", "Walsh", 50000);
            FullTimeEmployee em2 = new FullTimeEmployee("Joe", "Murphy", 100000);
            PartTimeEmployee em3 = new PartTimeEmployee("John", "Smith", 15, 10);
            PartTimeEmployee em4 = new PartTimeEmployee("Jane", "Jones", 13, 15);

            //add objects to list
            employees.Add(em1);
            employees.Add(em2);
            employees.Add(em3);
            employees.Add(em4);

            //sort employees by LastName(uses IComparible)
            employees.Sort();
        }

        private void lbxEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //clears each tbx each time a new employee is selected
            tbxSalary.Clear();
            tbxHoursWorked.Clear();
            tbxHourlyRate.Clear();

            //check which employee is selected
            Employee selectedEmployee = lbxEmployees.SelectedItem as Employee;

            //check to see if there is an employee selected
            if(selectedEmployee != null)
            {
                //display employee's name
                tbxFirstName.Text = selectedEmployee.FirstName;
                tbxLastName.Text = selectedEmployee.LastName;

                //check if selected employee is FullTimeEmployee or PartTimeEmployee
                if(selectedEmployee is FullTimeEmployee)
                {
                    //puts selected employee in new employee of correct derived class (FullTimeEmployee) so you can call the correct properties (Salary)
                    FullTimeEmployee selectedFTEmployee = selectedEmployee as FullTimeEmployee;
                    //checks fulltime radio button
                    rbFullTime.IsChecked = true;
                    //displays FullTimeEmployee's Salary 
                    tbxSalary.Text = selectedFTEmployee.Salary.ToString();
                    //displays employees monthly pay by calling the class method
                    tblkCalcMonthlyPay.Text = ("€" + selectedFTEmployee.CalculateMonthlyPay().ToString("F"));
                }
                else if (selectedEmployee is PartTimeEmployee)
                {
                    //puts selected employee in new employee of correct derived class (PartTimeEmployee) so you can call the correct properties (HoursWorked, HourlyRate)
                    PartTimeEmployee selectedPTEmployee = selectedEmployee as PartTimeEmployee;
                    //checks part time radio button
                    rbPartTime.IsChecked = true;
                    //display PartTimeEmployee's HoursWorked and HourlyRate
                    tbxHoursWorked.Text = selectedPTEmployee.HoursWorked.ToString();
                    tbxHourlyRate.Text = selectedPTEmployee.HourlyRate.ToString();
                    //displays employees monthly pay by calling the class method
                    tblkCalcMonthlyPay.Text = ("€" + selectedPTEmployee.CalculateMonthlyPay().ToString("F"));
                }
            }

        }

        private void cboxFullTime_Checked(object sender, RoutedEventArgs e)
        {
            //clears the ObservableCollection
            filteredEmployees.Clear();

            //check if both check boxes are checked, if not, move onto else statement
            if (cboxFullTime.IsChecked == true && cboxPartTime.IsChecked == true)
            {
                //display employees in listbox
                lbxEmployees.ItemsSource = employees;
            }
            else
            {
                //check if FullTime id checked and PartTime is not checked
                if (cboxFullTime.IsChecked == true && cboxPartTime.IsChecked == false)
                {
                    //loop through all the employees in employees list
                    foreach (Employee emp in employees)
                    {
                        //check if employee is a FullTimeEmployee
                        if(emp is FullTimeEmployee)
                        {
                            //add FullTimeEmployees employees to ObservableCollection
                            filteredEmployees.Add(emp);
                        }
                    }
                }
                //check if PartTime id checked and FullTime is not checked
                else if (cboxPartTime.IsChecked == true && cboxFullTime.IsChecked == false)
                {
                    //loop through all the employees in employees list
                    foreach (Employee emp in employees)
                    {
                        //check if employee is a PartTimeEmployee
                        if (emp is PartTimeEmployee)
                        {
                            //add PartTimeEmployees employees to ObservableCollection
                            filteredEmployees.Add(emp);
                        }
                    }
                }
                //display filterdEmployees
                lbxEmployees.ItemsSource = filteredEmployees;
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
            //read inputs from textboxes and put them into variables
            string firstName = tbxFirstName.Text;
            string lastName = tbxLastName.Text;

            //checks if FullTime radio button is checked
            if (rbFullTime.IsChecked == true)
            {
                //reads salary input and places it in a variable
                decimal salary = Convert.ToDecimal(tbxSalary.Text);
                //make a new employee and put it into employees list
                FullTimeEmployee employee = new FullTimeEmployee(firstName, lastName, salary);
                employees.Add(employee);
            }
            //checks if PartTime radio button is checked
            else if (rbPartTime.IsChecked == true)
            {
                //reads hoursWorked and hourlyRate inputs and places them in a variables
                double hoursWorked = Convert.ToDouble(tbxHoursWorked.Text);
                decimal hourlyRate = Convert.ToDecimal(tbxHourlyRate.Text);
                //make a new employee and put it into employees list
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

            //check if an employee is selected
            if (selectedEmployee != null)
            {
                //sets the employees name to the input in the textboxes
                selectedEmployee.FirstName = tbxFirstName.Text;
                selectedEmployee.LastName = tbxLastName.Text;

                //check if employee is a FullTimeEmployee
                if (selectedEmployee is FullTimeEmployee)
                {
                    //puts selected employee in new employee of correct derived class (FullTimeEmployee) so you can call the correct properties (Salary)
                    FullTimeEmployee selectedFTEmployee = selectedEmployee as FullTimeEmployee;
                    //sets the Salary to the input in the textbox 
                    selectedFTEmployee.Salary = Convert.ToDecimal(tbxSalary.Text);
                }
                //check if employee is a PartTimeEmployee
                else if (selectedEmployee is PartTimeEmployee)
                {
                    //puts selected employee in new employee of correct derived class (PartTimeEmployee) so you can call the correct properties (HoursWorked, HourlyRate)
                    PartTimeEmployee selectedPTEmployee = selectedEmployee as PartTimeEmployee;
                    //sets the HoursWorked and HourlyRate to the input in the textboxes 
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

            //removes selected employee from employees list
            employees.Remove(selectedEmployee);

            //update display manually
            lbxEmployees.ItemsSource = null;
            lbxEmployees.ItemsSource = employees;
        }
    }
}
