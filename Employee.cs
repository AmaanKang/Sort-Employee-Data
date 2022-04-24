using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// I, Amandeep Kaur, 000822179 certify that this material is my original work.  No other person's work has been used without due acknowledgement.
/// 
/// Student Name: Amandeep Kaur
/// Student # 000822179
/// File Date: 27 Sep. 2020
/// The purpose of this program is to store an Employee information
/// </summary>
namespace Lab1
{
    /// <summary>
    /// Class Employee is the model class and stores information for one Employee
    /// </summary>
    /// 
    class Employee
    {
        private String name;
        private int number;
        private Decimal rate;
        private Double hours;
        private Decimal gross;

        /// <summary>
        /// Constructor Employee sets the name, employee number, pay rate, hours worked of Employee to the passed arguments
        /// </summary>
        /// <param name="name">Employee Name</param>
        /// <param name="number">Employee Number</param>
        /// <param name="rate">Pay rate of Employee</param>
        /// <param name="hours">Number of Hours worked per week by an Employee</param>
        /// 
        public Employee(String name,int number,Decimal rate, Double hours)
        {
            this.name = name;
            this.number = number;
            this.rate = rate;
            this.hours = hours;
        }
        /// <summary>
        /// Getter method for Gross Pay amount calculated by multiplying 'hours' with 'pay rate'
        /// </summary>
        /// <returns>Gross Pay amount of Employee</returns>
        /// 
        public Decimal GetGross()
        {
            return (Convert.ToDecimal(GetHours())*GetRate());
        }
        /// <summary>
        /// Getter method for the hours worked
        /// </summary>
        /// <returns>Hours worked of an Employee</returns>
        /// 
        public Double GetHours()
        {
            return hours;
        }

        /// <summary>
        /// Getter method for Employee Name
        /// </summary>
        /// <returns>Name of Employee</returns>
        public String GetName()
        {
            return name;
        }
        /// <summary>
        /// Getter method for The Employee Number
        /// </summary>
        /// <returns>Employee Number</returns>
        public int GetNumber()
        {
            return number;
        }
        /// <summary>
        /// Getter method for Employee Pay rate
        /// </summary>
        /// <returns>Pay rate of an Employee</returns>
        public Decimal GetRate()
        {
            return rate;
        }
        /// <summary>
        /// Overrided method ToString() prints the Employee data in the given format 
        /// </summary>
        /// <returns>a String representation of the Employee data</returns>
        public override String ToString()
        {
            return $" {GetName(),-15}       {GetNumber(),8}       ${GetRate(),6}     {GetHours(),8}        ${GetGross(),7:f}";
        }
        /// <summary>
        /// Setter method for the hours worked by the Employee
        /// </summary>
        /// <param name="hours">Number of hours</param>
        /// <returns>Sets the Number of hours to the passed argument</returns>
        public Double SetHours(Double hours)
        {
            return this.hours = hours;
        }
        /// <summary>
        /// Setter method for the Employee name
        /// </summary>
        /// <param name="name">Name of the Employee</param>
        /// <returns>Sets the Employee nam eto the passed String parameter</returns>
        public String SetName(String name)
        {
            return this.name = name;
        }
        /// <summary>
        /// Setter method for the Employee number
        /// </summary>
        /// <param name="number">Employee Number</param>
        /// <returns>Sets the Employee Number to the passed value</returns>
        public int SetNumber(int number)
        {
            return this.number = number;
        }
        /// <summary>
        /// Sets the Pay rate of the Employee
        /// </summary>
        /// <param name="rate">Pay rate of Employee</param>
        /// <returns>Sets the Employee pay rate to the specified Decimal parameter</returns>
        public Decimal SetRate(Decimal rate)
        {
            return this.rate = rate;
        }
    }
}
