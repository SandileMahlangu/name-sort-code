using Xunit;
using FluentAssertions;

namespace Name_sorter_test.Tests
{
    public class Name_sorter_test
    {
        private readonly NameSorter _sorter;

        public Name_sorter_test()
        {
            _sorter = new NameSorter();
        }

        [Fact]
        public void Sort_Names_ReturnsNamesSortedByLastNameThenGivenNames()
        {
            // Arrange
            var names = new List<Name>()
            {
                new Name(new[] { "Marin" }, "Alvarez"),
                new Name(new[] { "Vaughn" }, "Lewis"),
                new Name(new[] { "Adonis" }, "Archer"),
                new Name(new[] { "Shelby", "Nathan" }, "Yoder"),
                new Name(new[] { "Marin" }, "Alvarez"),
                new Name(new[] { "London" }, "Lindsey"),
                new Name(new[] { "Beau", "Tristan" }, "Bentley"),
                new Name(new[] { "Leo" }, "Gardner"),
                new Name(new[] { "Hunter", "Uriah", "Mathew" }, "Clarke"),
                new Name(new[] { "Mikayla" }, "Lopez"),
                new Name(new[] { "Frankie", "Conner" }, "Ritter")
            };
            // Act
            var result = _sorter.Sort(names);

            // Assert
            result.Select(n => n.ToString()).Should().ContainInOrder(
               "Marin Alvarez",
               "Vaughn Lewis",
               "Adonis Archer",
               "Shelby Nathan Yoder",
               "Marin Alvarez",
               "Beau Tristan Bentley",
               "Leo Gardner",
               "Hunter Uriah Mathew Clarke",
               "Mikayla Lopez",
               "Frankie Conner Ritter"
            );

        }

        [Fact]
        public void Sort_EmptyList_ReturnsEmptyList()
        {
            // Arrange
            var names = new List<Name>();

            // Act
            var result = _sorter.Sort(names);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void Sort_SameLastName_DifferentGivenNames_SortsByGivenNames()
        {
            // Arrange
            var names = new List<Name>
            {
                new Name(new[] { "John", "Michael" }, "Smith"),
                new Name(new[] { "John", "Adam" }, "Smith"),
                new Name(new[] { "Alice" }, "Smith")
            };

            // Act
            var result = _sorter.Sort(names);

            // Assert
            result.Select(n => n.ToString()).Should().ContainInOrder(
                "Alice Smith",
                "John Adam Smith",
                "John Michael Smith"
            );
        }
    }

    internal class Name
    {
        private string[] strings;
        private string v;

        public Name(string[] strings, string v)
        {
            this.strings = strings;
            this.v = v;
        }
    }

    internal class FactAttribute : Attribute
    {
    }

    internal class NameSorter
    {
        internal IEnumerable<object> Sort(List<Name> names)
        {
            throw new NotImplementedException();
        }
    }
}