namespace name_sorter.Models
{
    // This class is responsible for collecting names and sorting them using SortNames method
    public static class NameSorter
    {
        public static List<PersonName> SortNames(IEnumerable<string> rawNames)
        {
            var people = new List<PersonName>();
            // Loop through each raw name string
            foreach (var raw in rawNames)
            {
                //parse the raw string into a PersonName object, and add person name to the list
                if (PersonName.TryParse(raw, out PersonName? person))
                {
                    people.Add(person!);
                }
                else
                {
                    Console.Error.WriteLine(
                        $"Warning: Invalid name format '{raw}' - must have 2 to 4 parts");
                }
            }
            
            // Sort the people by last name, then by first name(s)
            return [.. people.OrderBy(p => p.LastName)
                         .ThenBy(p => p.GivenNames.ElementAtOrDefault(0))
                         .ThenBy(p => p.GivenNames.ElementAtOrDefault(1))
                         .ThenBy(p => p.GivenNames.ElementAtOrDefault(2))];
        }
    }
}
