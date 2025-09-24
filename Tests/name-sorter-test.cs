using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using name_sorter;
using NameSorter;


namespace name_sorter.Tests
{
    public class NameTests
    {
        [Fact]
        public void Name_Constructor_ValidInput_CreatesName()
        {
          
            var name = new Name(new[] { "John", "Michael" }, "Doe");

            Assert.Equal(new[] { "John", "Michael" }, name.GivenNames);
            Assert.Equal("Doe", name.LastName);
        }

        [Fact]
        public void Name_Constructor_NoGivenNames_ThrowsException()
        {
           
            Assert.Throws<ArgumentException>(() => new Name(new string[0], "Doe"));
        }

        [Fact]
        public void Name_Constructor_TooManyGivenNames_ThrowsException()
        {
         
            Assert.Throws<ArgumentException>(() => 
                new Name(new[] { "One", "Two", "Three", "Four" }, "Doe"));
        }

        [Fact]
        public void Name_ToString_ReturnsCorrectFormat()
        {
           
            var name = new Name(new[] { "John", "Michael" }, "Doe");

            var result = name.ToString();

            Assert.Equal("John Michael Doe", result);
        }
    }

    public class NameParserTests
    {
        //public readonly NameParser _parser;
        private readonly name_sorter.NameParser _parser;

        public NameParserTests()
        {
            _parser = new NameParser();
        }

        [Theory]
        [InlineData("John Doe", new[] { "John" }, "Doe")]
        [InlineData("John Michael Doe", new[] { "John", "Michael" }, "Doe")]
        [InlineData("John Michael David Doe", new[] { "John", "Michael", "David" }, "Doe")]
        public void Parse_ValidNames_ReturnsCorrectName(string input, string[] expectedGivenNames, string expectedLastName)
        {
           
            var result = _parser.Parse(input);

          
            Assert.Equal(expectedGivenNames, result.GivenNames);
            Assert.Equal(expectedLastName, result.LastName);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Parse_InvalidInput_ThrowsArgumentException(string input)
        {
          
            Assert.Throws<ArgumentException>(() => _parser.Parse(input));
        }

        [Theory]
        [InlineData("Single")]
        public void Parse_InsufficientNameParts_ThrowsArgumentException(string input)
        {
           
            Assert.Throws<ArgumentException>(() => _parser.Parse(input));
        }

        [Fact]
        public void Parse_TooManyNameParts_ThrowsArgumentException()
        {
           
            var input = "One Two Three Four Five";

           
            Assert.Throws<ArgumentException>(() => _parser.Parse(input));
        }
    }

    public class NameSorterTests
    {
        private readonly NameSorter _sorter;

        public NameSorterTests()
        {
            _sorter = new NameSorter();
        }

        [Fact]
        public void Sort_Names_ReturnsNamesSortedByLastNameThenGivenNames()
        {
            // Arrange names
            var names = new List<Name>
            {
                new Name(new[] { "Janet" }, "Parsons"),
                new Name(new[] { "Vaughn" }, "Lewis"),
                new Name(new[] { "Adonis", "Julius" }, "Archer"),
                new Name(new[] { "Shelby", "Nathan" }, "Yoder"),
                new Name(new[] { "Marin" }, "Alvarez"),
                new Name(new[] { "London" }, "Lindsey"),
                new Name(new[] { "Beau", "Tristan" }, "Bentley"),
                new Name(new[] { "Leo" }, "Gardner"),
                new Name(new[] { "Hunter", "Uriah", "Mathew" }, "Clarke"),
                new Name(new[] { "Mikayla" }, "Lopez"),
                new Name(new[] { "Frankie", "Conner" }, "Ritter")
            };

            
            var result = _sorter.Sort(names);

          
            var resultList = result.Select(n => n.ToString()).ToList();
            
            Assert.Equal("Marin Alvarez", resultList[0]);
            Assert.Equal("Adonis Julius Archer", resultList[1]);
            Assert.Equal("Beau Tristan Bentley", resultList[2]);
            Assert.Equal("Hunter Uriah Mathew Clarke", resultList[3]);
            Assert.Equal("Leo Gardner", resultList[4]);
            Assert.Equal("Vaughn Lewis", resultList[5]);
            Assert.Equal("London Lindsey", resultList[6]);
            Assert.Equal("Mikayla Lopez", resultList[7]);
            Assert.Equal("Janet Parsons", resultList[8]);
            Assert.Equal("Frankie Conner Ritter", resultList[9]);
            Assert.Equal("Shelby Nathan Yoder", resultList[10]);
        }

        [Fact]
        public void Sort_EmptyList_ReturnsEmptyList()
        {
           
            var names = new List<Name>();

            var result = _sorter.Sort(names);

            Assert.Empty(result);
        }

        [Fact]
        public void Sort_SameLastName_DifferentGivenNames_SortsByGivenNames()
        {
       
            var names = new List<Name>
            {
                new Name(new[] { "Frankie", "Conner" }, "Ritter"),
                new Name(new[] { "John", "Michael" }, "Smith"),
                new Name(new[] { "John", "Adam" }, "Smith"),
                new Name(new[] { "Alice" }, "Smith")
            };

    
            var result = _sorter.Sort(names);

         
            var resultList = result.Select(n => n.ToString()).ToList();
            
            Assert.Equal("Alice Smith", resultList[0]);
            Assert.Equal("John Adam Smith", resultList[1]);
            Assert.Equal("John Michael Smith", resultList[2]);
        }
    }

    public class FileReaderTests
    {
        [Fact]
        public void ReadLines_FileExists_ReturnsLines()
        {
        
            var tempFile = Path.GetTempFileName();
            File.WriteAllLines(tempFile, new[] { "Line 1", "Line 2" });
            var reader = new FileReader();

            try
            {
            
                var result = reader.ReadLines(tempFile);

            
                Assert.Equal(2, result.Count);
                Assert.Equal("Line 1", result[0]);
                Assert.Equal("Line 2", result[1]);
            }
            finally
            {
                File.Delete(tempFile);
            }
        }

        [Fact]
        public void ReadLines_FileNotExists_ThrowsException()
        {
       
            var reader = new FileReader();

            
            Assert.Throws<FileNotFoundException>(() => reader.ReadLines("nonexistent.txt"));
        }
    }

    public class FileWriterTests
    {
        [Fact]
        public void WriteLines_WritesToFile()
        {
          
            var tempFile = Path.GetTempFileName();
            var writer = new FileWriter();
            var lines = new[] { "Test line 1", "Test line 2" };

            try
            {
           
                writer.WriteLines(tempFile, lines);

                
                var writtenLines = File.ReadAllLines(tempFile);
                Assert.Equal(lines, writtenLines);
            }
            finally
            {
                File.Delete(tempFile);
            }
        }
    }
}