using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please pass at least one file path as an argument");
                return 1;
            }
          
            foreach (string file in args)
            {
                // Check if the file exists
                if (!File.Exists(file))
                {
                    Console.WriteLine("Error: '" + file + "' file not found");
                    return 1;
                }

                // Read the file and count the integers
                var dict = new Dictionary<int, int>();
                int low = 0;
                try
                {
                    using (var sr = File.OpenText(file))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            try
                            {
                                low = int.Parse(line);
                            }
                            catch
                            {
                                throw new Exception("Invalid value encoutered: '" + line + "' is not an int32 value.");
                            }
                      
                            if (dict.ContainsKey(low))
                            {
                                dict[low]++;
                            }
                            else
                            {
                                dict.Add(low, 1);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: Problem occured while reading '" + file + "'. " + e.Message);
                    continue;
                }


                // Find the integer with the lowest count and the lowest integer value in the case of ties.
                foreach (var pair in dict)
                {
                    int key = pair.Key;
                    int value = pair.Value;
                    if (value < dict[low] || (value == dict[low] && key < low))
                    {
                        low = key;
                    }
                }

                // I extract the file name out of the path for a cleaner output that's close to the expected output.
                // But honestly, I think leaving the full path in the output is more informative.
                Console.WriteLine("File: " + file.Substring(file.LastIndexOf("/") + 1) + ", Number: " + low + ", Repeated: " + dict[low]);
            }
            return 0;
        }
    }
}
