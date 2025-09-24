namespace name_sorter.Models
{
    public class PersonName
    {
        //get full name and last name
        public string FullName { get; }
        public string LastName { get; }
        public List<string> GivenNames { get; }

        private PersonName(string fullName, string lastName, List<string> givenNames)
        {
            FullName = fullName;
            LastName = lastName;
            GivenNames = givenNames;
        }
        //parse a raw person name into a PersonName object
        public static bool TryParse(string raw, out PersonName? name)
        {
            name = null;
            if (string.IsNullOrWhiteSpace(raw))
                return false;

            var parts = raw.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2 || parts.Length > 4)
                return false;

            var lastName = parts[^1];
            var givenNames = parts.Take(parts.Length - 1).ToList();

            name = new PersonName(raw.Trim(), lastName, givenNames);
            return true;
        }
    }
}
