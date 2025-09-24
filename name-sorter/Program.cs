using name_sorter.Models;

namespace name_sorter
{
    class Program
    {
        static void Main(string[] args)
        {
            //check if arguments are passed to the program
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: name-sorter <path-to-input-file>");
                return;
            }

            //intialize input path to the 1st argument
            string inputPath = args[0];
            //check if file exist
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Error: File not found at '{inputPath}'");
                return;
            }

            var rawNames = File.ReadAllLines(inputPath)
                               .Where(line => !string.IsNullOrWhiteSpace(line));

            //create output file
            var sorted = NameSorter.SortNames(rawNames);

            //var sorter = new NameSorter();
            //var sorted = sorter.SortNames(rawNames);

            const string outputFile = "sorted-names-list.txt";

            using (var writer = new StreamWriter(outputFile))
            {
                foreach (var person in sorted)
                {
                    Console.WriteLine(person.FullName);
                    writer.WriteLine(person.FullName);
                }
            }
            //display completion message on a console
            Console.WriteLine($"\nSorted list has been populated on {outputFile}");
        }
    }
}


