using System;
using System.Collections.Generic;
using System.IO;

namespace Project1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Clear console for better UI experience
            Console.Clear();

            string inFile = "";
            List<Employee> employees = new List<Employee>();
            double average = 0;
            double total = 0;
            double max = Double.NegativeInfinity;
            double min = Double.PositiveInfinity;
            string summaryFileName = "salarySummary.txt";


            // Ask the user for their name
            welcomeUser();

            //     Ask the user for the file to be read
            inFile = getInputFile();

            //     Read the data file line by line to get the individual pieces of data
            try {
                using(StreamReader inStream = new StreamReader(inFile)) {
                    string line = "";
                    while ((line = inStream.ReadLine()) != null) {
                        employees.Add(new Employee(line));
                        Employee.count++;
                    }
                }
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
            
            //     Print each employees pay to the console and calculate stats
            foreach(Employee e in employees) {
                Console.WriteLine($"{e.firstName} {e.lastName}: {e.getPaycheckAmount().ToString("C2")}");
                total += e.getPaycheckAmount();
                max = setMax(max,e.getPaycheckAmount());
                min = setMin(min,e.getPaycheckAmount());
            }

            average = total / Employee.count;

            try {
                //     Create a summary report named salarySummary.txt contains
                //         Number of Employees
                //         Total Payroll
                //         Average Pay
                //         Maximum Pay
                //         Minimum Pay
                writeSummary(summaryFileName,Employee.count, total, average, max, min);
            }
            catch(Exception ex) {
                errorLog($"Exception from Summary File Write: {ex.Message}");
            }
            
            //     Output a message letting the user know the report was successfully written
            Console.WriteLine($"\nSummary successfully written to {summaryFileName}");
            //     End the program
        }

        // Write the summary to a given textfile. 
        private static void writeSummary(string summaryFileName, int count, double total, double average, double max, double min)
        {
            using(StreamWriter file = new StreamWriter(summaryFileName)){
                file.WriteLine("Payroll Summary");
                file.WriteLine("---------------");
                // This is the max number of checks to cut. Make sure there are not more than this many checks
                file.WriteLine($"Number of Employees: {count}");
                // This is the total expenditure for employee payroll. Ensure this amount is in payroll account prior to check disbursement
                file.WriteLine($"Total Payouts: {total.ToString("C2")}");
                // Average check amount. Make sure this isn't overly high or low
                file.WriteLine($"Average Payout: {average.ToString("C2")}");
                // Max check amount. Make sure errors dont occur and employee gets paid too much
                file.WriteLine($"Maximum Payout: {max.ToString("C2")}");
                // Min check amount. Make sure $0 checks are not sent to employee
                file.WriteLine($"Minimum Payout: {min.ToString("C2")}");
            }
        }

        // Find the Maximum value out of all values given
        private static double setMax(params double[] values)
        {
            double output = Double.NegativeInfinity;
            foreach(double v in values) {
                if(v > output){
                    output = v;
                }
            }
            
            return output;
        }

        // Find the minimum value out of all values given
        private static double setMin(params double[] values)
        {
            double output = Double.PositiveInfinity;
            foreach(double v in values) {
                if(v < output){
                    output = v;
                }
            }
            
            return output;
        }

        // Prompt user for input filename. Optionally, you can disable the existance check when calling recursively 
        private static string getInputFile(bool checkExists = true) {
            string userInput;
            string output;
            string intermediate;

            //     If the user file name provided does not end in ".csv" then your code should add ".csv" to the file name
            Console.WriteLine("CSV Input file (default: payroll.csv): ");
            userInput = Console.ReadLine();

            if(userInput == null) {
                errorLog("Filename cannot be null");
                intermediate = getInputFile(false);
            }
            else if(userInput == ""){
                intermediate = "payroll.csv";
            }
            else {
                intermediate = checkFileExtention(userInput);
            }
            
            if(checkExists){
                if(checkFileExists(intermediate)) {
                    output = intermediate;
                }
                else {
                    errorLog($"File does not exist: {intermediate}");
                    output = getInputFile();
                }
            }
            else {
                output = intermediate;
            }

            return output;
        }

        // Log error. Currently, this gives the user output on the console with bright colors to draw attention. In future, could get adjusted to log to file. 
        private static void errorLog(string input)
        {         
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            
            Console.WriteLine($"{input}");

            Console.ResetColor();
            Console.WriteLine();
        }

        // Ensure file has proper extension on it. If not, add the extension
        private static string checkFileExtention(string input)
        {
            string output;
            if (input.EndsWith(".csv")){
                output = input;
            }
            else {
                output = input + ".csv";
            }

            return output;
        }

        // Check if file exists on filesystem in current folder
        private static bool checkFileExists(string input)
        {
            bool output;
            if(File.Exists(input)) {
                output = true;
            }
            else {
                output = false;
            }
            
            return output;
        }

        // Get username and welcome user
        private static void welcomeUser()
        {
            int w = Console.WindowWidth;
            string userName = "";
            string welcomeMessage = "Welcome to PROJECT 1 Payroll Processing";
            string prettyfier = "";
            for(int i = 0; i < ((w/2) - welcomeMessage.Length); i++) {
                prettyfier += "-";
            }

            while(userName == "") {
                Console.WriteLine("Please enter your name: ");
                userName = Console.ReadLine();
                if(userName == null){
                    userName = "";
                }
            }
            

            Console.WriteLine($"{prettyfier} {welcomeMessage} {prettyfier}");
            Console.WriteLine($"Welcome {userName}!\n");
        }

    }

}