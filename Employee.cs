using System;

namespace Project1
{
    class Employee
    {
        public static int count { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public double payRate { get; set; }
        public double hoursWorked { get; set; }

        public Employee(string fName, string lName):this(fName,lName,0,0) {

        }

        public Employee(string fName, string lName, double pay):this(fName,lName,pay,0) {

        }

        public Employee(string fName, string lName, double pay, double hours) {
            firstName = fName;
            lastName = lName;
            payRate = pay;
            hoursWorked = hours;
        }

        public Employee(string valueString) {
            string[] input = valueString.Split(",");
            this.firstName = input[0];
            this.lastName = input[1];
            this.payRate = Double.Parse(input[2]);
            this.hoursWorked = Double.Parse(input[3]);
        }

        public double getPaycheckAmount() {
            //     Calculate the pay for each worker
            double output;

            output = payRate * hoursWorked;

            return output;
        }
    }
}