using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace TechJobs.Models
{
    class JobData
    {
        // Static mean it is availiable to all the member of the class. 
        // it is alive without an instance and will live till terminate.
        static List<Dictionary<string, string>> AllJobs = new List<Dictionary<string, string>>();
        // Declare a List of Dictionary AllJobs with string string, 
        static bool IsDataLoaded = false;
        // Initalize boolean IsDataLoaded = false

        public static List<Dictionary<string, string>> FindAll()
        {   // FindAll() methods called LoadData Method
            LoadData();

            // Bonus mission: return a copy
            return new List<Dictionary<string, string>>(AllJobs);
        }

        /*
         * Returns a list of all values contained in a given column,
         * without duplicates. 
         */
        public static List<string> FindAll(string column)
        {   // FindAll Method taking in string column, returning List of <string> type
            LoadData();

            List<string> values = new List<string>();

            foreach (Dictionary<string, string> job in AllJobs)
            {   // aValue here is the Value of the Column "key"
                string aValue = job[column];
                // if the List<string> values does not contain values from aValues. 
                // add aValues to the List<string>
                if (!values.Contains(aValue))
                {
                    values.Add(aValue);
                }
            }

            // Bonus mission: sort results alphabetically
            values.Sort();
            return values;
        }

        /**
         * Search all columns for the given term
         */
        public static List<Dictionary<string, string>> FindByValue(string value)
        {
            // load data, if not already loaded
            LoadData();

            List<Dictionary<string, string>> jobs = new List<Dictionary<string, string>>();
            // Create New List of Dictionary Jobs. Opeing up AllJobs and for each Dictionary pair.
            // if the passed Value is contains in the Value:Key Add the row of AllJobs into jobs
            // once it found the first match and added the row. It break out of the loop and return.
            foreach (Dictionary<string, string> row in AllJobs)
            {

                foreach (string key in row.Keys)
                {
                    string aValue = row[key];

                    if (aValue.ToLower().Contains(value.ToLower()))
                    {
                        jobs.Add(row);

                        // Finding one field in a job that matches is sufficient
                        break;
                    }
                }
            }

            return jobs;
        }

        /**
         * Returns results of search the jobs data by key/value, using
         * inclusion of the search term.
         *
         * For example, searching for employer "Enterprise" will include results
         * with "Enterprise Holdings, Inc".
         */
        public static List<Dictionary<string, string>> FindByColumnAndValue(string column, string value)
        {
            // load data, if not already loaded
            LoadData();

            List<Dictionary<string, string>> jobs = new List<Dictionary<string, string>>();

            foreach (Dictionary<string, string> row in AllJobs)
            {
                string aValue = row[column];

                if (aValue.ToLower().Contains(value.ToLower()))
                {
                    jobs.Add(row);
                   
                }
            }

            return jobs;
        }

        /*
         * Load and parse data from job_data.csv
         */
        private static void LoadData()
        {
            // if IsDataLoaded = True then do nothing.
            if (IsDataLoaded)
            {
                return;
            }

            List<string[]> rows = new List<string[]>();
            // Read the file and import it into var reader
            using (StreamReader reader = File.OpenText("Models/job_data.csv"))
            {   // This method return an interger and -1 if there is no char left to read.
                while (reader.Peek() >= 0)
                {   // Peek, if >=0 Read the line and pass it into 'line' type string cursor at the end
                    string line = reader.ReadLine();
                    string[] rowArrray = CSVRowToStringArray(line);// call
                    // if it is not an empty rowArray add it to rows
                    if (rowArrray.Length > 0)
                    {
                        rows.Add(rowArrray);
                    }// keep adding all the line into string[] rows list until end of doc
                }
            }
            // remove the top row "column name" from the list
            string[] headers = rows[0];
            rows.Remove(headers);

            // Parse each row array into a more friendly Dictionary
            foreach (string[] row in rows)
            {   // there are 5 item in string[] Rows = to string[] header 
                Dictionary<string, string> rowDict = new Dictionary<string, string>();
                // adding them to dictionay in pair
                for (int i = 0; i < headers.Length; i++)
                {
                    rowDict.Add(headers[i], row[i]);
                }
                AllJobs.Add(rowDict);
            }

            IsDataLoaded = true;
        }

        /*
         * Parse a single line of a CSV file into a string array
         * Creating the method and Passing Variable straight through.
         * It is better to pass because it is only used during computation and nowhere else.
         */
        private static string[] CSVRowToStringArray(string row, char fieldSeparator = ',', char stringSeparator = '\"')
        {   // initialize !isbetweenquotes and ValueBuilder type Stringbuilder. a mutable string 
            // that can be appended rapidly vs copy and cocatenate to new string.
            bool isBetweenQuotes = false;
            StringBuilder valueBuilder = new StringBuilder();
            List<string> rowValues = new List<string>();

            // Loop through the row string one char at a time
            foreach (char c in row.ToCharArray())
            {   // if c is = , AND is NOT between quotes  Add the content of valueBuilder to rowValue
                if ((c == fieldSeparator && !isBetweenQuotes))
                {
                    rowValues.Add(valueBuilder.ToString());
                    valueBuilder.Clear();
                }
                else
                {   // if c is = " then flip isbetweenquotes 
                    if (c == stringSeparator)
                    {
                        isBetweenQuotes = !isBetweenQuotes;
                    }
                    else
                    {   // if it is not an , or " apppend the char to value builder
                        valueBuilder.Append(c);
                    }
                }
            }

            // Add the final value
            // the line end with empty space there is no , to trigger Adding  the final valueBuilder
            rowValues.Add(valueBuilder.ToString());
            valueBuilder.Clear();

            return rowValues.ToArray();
        }
    }
}
