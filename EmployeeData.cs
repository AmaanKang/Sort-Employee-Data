using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
/// <summary>
/// File Date: 27 Sep. 2020
/// The purpose of this program is to sort the employee data as per the choice of user
/// </summary>
namespace EmployeeData
{
    /// <summary>
    /// EmployeeData Class is the view of Employee class and it gives 6 options to user to sort the employee data in different orders or to exit the application.
    /// It forms an array of the different employees by reading the data from a text file.
    /// </summary>
    class EmployeeData
    {
        private static Employee[] employees;
        private static String[] namesArray;
        private static String name;
        private static int number;
        private static int[] numberArray;
        private static Decimal rate;
        private static Decimal[] ratesArray;
        private static Decimal[] grossArray;
        private static Double hours;
        private static Double[] hoursArray;
        /// <summary>
        /// Main method executes the program by reading the data from text file and displaying the menu option and then, acting upon the user input
        /// </summary>
        /// <param name="args">unused arguments</param>
        public static void Main(string[] args)
        {
            /* 
             * 'employees' array stores the all employees data after reading it from text file
             */
            employees = Read();
            
            if (employees != null)
            {
                /*
                 * 'MenuOption()' method contains the menu display with different options and the action taken as per the user input
                 */
                MenuOption();
            }
            else
            {
                Console.WriteLine("SORRY! The Seleceted file is empty.");
            }
        }
        /// <summary>
        /// Reads the data from text file for employees and validates the data according to data type of the values present in text file
        /// </summary>
        /// <returns>an array of employees</returns>
        public static Employee[] Read()
        {
            /*
             * The length of employees array has been set to a maximum of 100
             */
            Employee[] employees = new Employee[100];
            /*
             * The loop variable works as an index of the employees array
             */
            int loop = 0;
            
            FileStream fileS = null;
            StreamReader streamR = null;
            /* 
             * numberOfLines variable increases everytime when the streamS reads a line in the file, thus it lets us know how many lines are there in the file and 
             * helps in setting the array length to that numberOfLines
             */
            int numberOfLines = 0;
            try
            {
                /*
                 * fileS opens the specified file to read the data from it
                 */
                fileS = new FileStream(@"..\..\employees.txt", FileMode.Open);
                streamR = new StreamReader(fileS);
                /*
                 * while loop will keep reading the data until it reaches the endline of the file or, until the number of lines in the file gets more than the length of array
                 */
                while (!streamR.EndOfStream || numberOfLines <= employees.Length)
                {
                    /* 
                     * When streanS reads a line, then that line is stored in variable 'value' and the number of lines increases
                     */
                    String value = streamR.ReadLine();
                    numberOfLines++;
                    /*
                     * the data array holds four elements which are splitted by ','
                     */
                    String[] data = value.Split(',');
                    /*
                     * the first element in the data array is the name of employee
                     */
                    name = data[0];
                    /*
                     * the second element in data array is the employee number and if it does not come out to be int, then an error message will be printed
                     */
                    if (int.TryParse(data[1], out number) == false)
                    {
                        Console.WriteLine("Cannot convert Employee number to Integer");

                    }
                    /*
                     * the third element in data array is the pay rate and it should be of data type Decimal
                     */
                    if (Decimal.TryParse(data[2], out rate) == false)
                    {
                        Console.WriteLine("Cannot convert hourly rate to decimal");

                    }
                    /*
                     * the fourth element in data array is the number of hours and it should be a double value
                     */
                    if (Double.TryParse(data[3], out hours) == false)
                    {
                        Console.WriteLine("Cannot Convert hours to double");

                    }
                    /*
                     * the employees element at a specific index contains the same data as read in the current line of the text file
                     */
                    employees[loop] = new Employee(name, number, rate, hours);
                    loop++;
                    /*
                     * if the loop reaches the end of the file, then size of the array should be set to the number of lines read in the file
                     */
                    if (streamR.EndOfStream)
                    {
                        Array.Resize<Employee>(ref employees, numberOfLines);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            /*
             * if the read file is not null, then the fileS and streamS can be closed
             */
            try
            {
                if (fileS != null)
                {
                    fileS.Close();
                }if (streamR != null)
                {
                    streamR.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return employees;
        }
        /// <summary>
        /// Displays the menu to the user and asks for an option
        /// When the user makes his choice, then it executes the different methods under the chosen option
        /// </summary>
        /// <returns>true</returns>
        public static bool MenuOption()
        {
            /*
             * the loop keeps on going as long as the user does not select the Exit option
             */
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("*****Employee Data*****");
                Console.WriteLine("1 - Sort by Employee Name(ascending)");
                Console.WriteLine("2 - Sort by Employee Number(ascending)");
                Console.WriteLine("3 - Sort by Employee Pay Rate(descending)");
                Console.WriteLine("4 - Sort by Employee Hours(descending)");
                Console.WriteLine("5 - Sort by Employee Gross Pay(descending)");
                Console.WriteLine("6 - Exit");
                Console.WriteLine("\n Enter Option");
                /*
                 * the choice of the user is stored in 'choice' variable
                 */
                String choice = Console.ReadLine();
                /*
                 * After the user makes a choice, the console window is cleared as the new output is printed
                 */
                Console.Clear();
                Console.WriteLine("  Names              Employee No.     Pay Rate      Hours       Gross Pay");
                Console.WriteLine();
                /*
                 * If the user enters any invalid option, then it displays an error message and directs the user to enter the valid option
                 */
                if ((int.TryParse(choice, out int userChoice) == false) || userChoice < 1 || userChoice > 6)
                {
                    Console.WriteLine("Input invalid. Please enter a value from 1 to 6.");
                }
                /*
                 * If the user enters 1, then the data is sorted as per the ascending ordered of employees names
                 */
                else if (userChoice == 1)
                {
                    /*
                     * namesArray stores the names of all employees
                     */
                    namesArray = new String[employees.Length];
                    for(int i=0;i < namesArray.Length; i++)
                    {
                        namesArray[i] = employees[i].GetName();
                    }
                    SortByName(namesArray);
                    /*
                     * for loops checks if the name of any employee is same as the employee name present at the given index of the sorted namesArray and prints the 
                     * employee data as per sorted names in ascending order
                     */
                    for (int k = 0; k < employees.Length; k++)
                    {
                        for (int i = 0; i < employees.Length; i++)
                        {
                            if (employees[i].GetName() == namesArray[k])
                            {
                                Console.WriteLine(employees[i]);
                            }
                        }
                    }
                }
                /*
                 * If user enters 2, then the employee data is sorted as per the ascending employee number
                 */
                else if (userChoice == 2)
                {
                    /*
                     * numberArray holds the employee numbers of all employees
                     */
                    numberArray = new int[employees.Length];
                    for (int i = 0; i < numberArray.Length; i++)
                    {
                        numberArray[i] = employees[i].GetNumber();
                    }
                    SortByNumber(numberArray);
                    /*
                     * for loops checks if the emp. no. of any employee is same as the employee number present at the given index of the sorted numberArray and prints the 
                     * employee data as per sorted numbers in ascending order
                     */
                    for (int k = 0; k < employees.Length; k++)
                    {
                        for (int i = 0; i < employees.Length; i++)
                        {
                            if (employees[i].GetNumber() == numberArray[k])
                            {
                                Console.WriteLine(employees[i]);
                            }
                        }
                    }
                }
                /*
                 * if the user enters 3, then the data is sorted in descending pay rates
                 */
                else if (userChoice == 3)
                {
                    ratesArray = new decimal[employees.Length];
                    
                    List<Employee> employeeList = new List<Employee>();
                    /*
                     * ratesArray stores all the unsorted employee pay rates
                     */
                    for (int i = 0; i < ratesArray.Length; i++)
                    {
                        ratesArray[i] = employees[i].GetRate();
                    }
                    SortByRateAndPay(ratesArray);
                    /*
                     * for loops checks if the employee pay rate of any employee is same as the employee pay rate present at the given index of the sorted ratesArray 
                     * and adds those sorted employees to the employeeList 
                     */
                    for (int k = 0; k < employees.Length; k++)
                    {
                        for (int i = 0; i < employees.Length; i++)
                        {
                            if (employees[i].GetRate() == ratesArray[k])
                            {
                                employeeList.Add(employees[i]);
                            }  
                            }  
                        }
                    /*
                     * When the sorted employee data in ratesArray is printed, then it prints an employee again after another employee if the pay rate comes out to be same for two employees
                     * Hence if the employee data of the given employee in employeeList comes out to be same as the employee on the third place, then that employee is removed from the list
                     */
                    int index = employeeList.Count - 1;
                    /*
                     * the loop checks from the last element to the first element
                     */
                    while (index > 1)
                    {
                        if (employeeList[index] == employeeList[index - 2])
                        {
                            employeeList.RemoveAt(index);
                            index--;
                        }
                        else
                        {
                            index--;
                        }
                    }
                    
                    foreach(Employee e in employeeList)
                    {
                        Console.WriteLine(e);
                    } 
                }
                /*
                 * If user enters 4, then the data is sorted in descending hours
                 */
                else if (userChoice == 4)
                {
                    hoursArray = new double[employees.Length];
                    List<Employee> employeeList = new List<Employee>();
                    /*
                     * hourArray stores all the unsorted employee hours data
                     */
                    for (int i = 0; i < hoursArray.Length; i++)
                    {
                        hoursArray[i] = employees[i].GetHours();
                    }
                    SortByHours(hoursArray);
                    /*
                     * for loops checks if the hours of any employee is same as the hours present at the given index of the sorted hoursArray
                     * and adds those sorted employees to the employeeList 
                     */
                    for (int k = 0; k < employees.Length; k++)
                    {
                        for (int i = 0; i < employees.Length; i++)
                        {
                            if (employees[i].GetHours() == hoursArray[k])
                            {
                                employeeList.Add(employees[i]);
                            }
                        }
                    }
                    /*
                     * When the sorted employee data in hoursArray is printed, then it prints an employee again after another employee if the hours comes out to be same for two employees
                     * Hence if the employee data of the given employee in employeeList comes out to be same as the employee on the third place, then that employee is removed from the list
                     */
                    int index = employeeList.Count - 1;
                    while (index > 1)
                    {
                        if (employeeList[index] == employeeList[index - 2])
                        {
                            employeeList.RemoveAt(index);
                            index--;
                        }
                        else
                        {
                            index--;
                        }
                    }

                    foreach (Employee e in employeeList)
                    {
                        Console.WriteLine(e);
                    }
                }
                else if (userChoice == 5)
                {
                    grossArray = new decimal[employees.Length];
                    List<Employee> employeeList = new List<Employee>();
                    /*
                     * grossArray stores all the unsorted employee gross pay data
                     */
                    for (int i = 0; i < grossArray.Length; i++)
                    {
                        grossArray[i] = employees[i].GetGross();
                    }
                    SortByRateAndPay(grossArray);
                    /*
                     * for loops checks if the gross pay of any employee is same as the gross pay present at the given index of the sorted grossArray
                     * and adds those sorted employees to the employeeList 
                     */
                    for (int k = 0; k < employees.Length; k++)
                    {
                        for (int i = 0; i < employees.Length; i++)
                        {
                            if (employees[i].GetGross() == grossArray[k])
                            {
                                employeeList.Add(employees[i]);
                            }
                        }
                    }
                    /*
                     * When the sorted employee data in grossArray is printed, then it prints an employee again after another employee if the gross pay comes out to be same for two employees
                     * Hence if the employee data of the given employee in employeeList comes out to be same as the employee on the third place, then that employee is removed from the list
                     */
                    int index = employeeList.Count - 1;
                    while (index > 1)
                    {
                        if (employeeList[index] == employeeList[index - 2])
                        {
                            employeeList.RemoveAt(index);
                            index--;
                        }
                        else
                        {
                            index--;
                        }
                    }

                    foreach (Employee e in employeeList)
                    {
                        Console.WriteLine(e);
                    }

                }
                else
                {
                    break;
                }
            }
            return true;
        }
        /// <summary>
        /// Sorts the names of all the employees by using insertion algorithm and the url for this algorithm is: 
        /// https://dotnetcoretutorials.com/2020/05/10/basic-sorting-algorithms-in-c/
        /// </summary>
        /// <param name="nameArray">array containing all the names</param>
        public static void SortByName(String[] nameArray)
        {
                for (int i = 1; i < nameArray.Length; i++)
                {
                    var value = nameArray[i];
                    int j = i;
                    while ((j > 0) && (nameArray[j-1].CompareTo(value) > 0))
                    {
                        nameArray[j] = nameArray[j-1];
                        j--;
                    }
                    nameArray[j] = value;
                }  
        }
        /// <summary>
        /// Sorts the numbers of all the employees by using insertion algorithm and the url for this algorithm is:
        /// https://dotnetcoretutorials.com/2020/05/10/basic-sorting-algorithms-in-c/
        /// </summary>
        /// <param name="numbersArray">array containing the emp. numbers</param>
        public static void SortByNumber(int[] numbersArray)
        {
            int j = 0;
            while (j < 2)
            {
                for (int i = 0; i < numbersArray.Length; i++)
                {
                    var value = numbersArray[i];
                    var k = i;

                    while (k > 0 && numbersArray[k - 1] > value)
                    {
                        numbersArray[k] = numbersArray[k - 1];
                        k--;

                    }
                    numbersArray[k] = value; 
                }
                j++;
            } 
        }
        /// <summary>
        /// Sorts the gross pay or pay rate of all the employees by using insertion algorithm and the url for this algorithm is:
        /// https://dotnetcoretutorials.com/2020/05/10/basic-sorting-algorithms-in-c/
        /// </summary>
        /// <param name="rateOrPayArray">array containing the pay rate or gross pay</param>
        public static void SortByRateAndPay(Decimal[] rateOrPayArray)
        {
            int j = 0;
            while (j < 2)
            {
                for (int i = 0; i < rateOrPayArray.Length; i++)
                {
                    var value = rateOrPayArray[i];
                    var k = i;

                    while (k > 0 && rateOrPayArray[k - 1] < value)
                    {
                        rateOrPayArray[k] = rateOrPayArray[k - 1];
                        k--;

                    }
                    rateOrPayArray[k] = value;
                }
                j++;
            }
        }
        /// <summary>
        /// Sorts the hours of all the employees by using insertion algorithm and the url for this algorithm is:
        /// https://dotnetcoretutorials.com/2020/05/10/basic-sorting-algorithms-in-c/
        /// </summary>
        /// <param name="hoursArray">array having the hours of employees</param>
        public static void SortByHours(Double[] hoursArray)
        {
            int j = 0;
            while (j < 2)
            {
                for (int i = 0; i < hoursArray.Length; i++)
                {
                    var value = hoursArray[i];
                    var k = i;

                    while (k > 0 && hoursArray[k - 1] < value)
                    {
                        hoursArray[k] = hoursArray[k - 1];
                        k--;

                    }
                    hoursArray[k] = value;
                }
                j++;
            }

        }
        
        }
}
