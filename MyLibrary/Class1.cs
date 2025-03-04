using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;

namespace MyLibrary
{
    public class JsonXmlProcessor
    {
        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public List<string> Skills { get; set; }
        }

        public void ProcessJsonAndXml(string filePath)
        {
            string newJsonFilePath = "newdata.json";
            string xmlFilePath = "output.xml"; 
            string newXmlFilePath = "newdata.xml";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not Found: " + filePath);
                return;
            }

            Person person = null;
            try
            {
                string jsonData = File.ReadAllText(filePath);
                person = JsonSerializer.Deserialize<Person>(jsonData);

                Console.WriteLine($"{person.Name}'s Skills:");
                foreach (string skill in person.Skills)
                {
                    Console.WriteLine($"- {skill}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            // Adding a new person
            Person newPerson = new Person
            {
                Name = "Stephen Owoamanam",
                Age = 128,
                Skills = new List<string> { "React", "Golang", "Rust" }
            };

            List<Person> people = new List<Person> { person, newPerson };
            File.WriteAllText(newJsonFilePath, JsonSerializer.Serialize(people, new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine("\nNew person added and saved to newdata.json");

            // Convert JSON to XML
            XElement xml = new XElement("People",
                people.ConvertAll(p =>
                    new XElement("Person",
                        new XElement("Name", p.Name),
                        new XElement("Age", p.Age),
                        new XElement("Skills", string.Join(", ", p.Skills))
                    )
                )
            );
            xml.Save(xmlFilePath);
            Console.WriteLine("\nJSON converted to XML and saved as output.xml");

            // Create a different XML file with different data
            XElement newXml = new XElement("Employees",
                new XElement("Employee",
                    new XElement("ID", 101),
                    new XElement("Name", "Mark Spencer"),
                    new XElement("Department", "IT"),
                    new XElement("Salary", 90000)
                ),
                new XElement("Employee",
                    new XElement("ID", 102),
                    new XElement("Name", "Sarah Connor"),
                    new XElement("Department", "Finance"),
                    new XElement("Salary", 85000)
                )
            );
            newXml.Save(newXmlFilePath);
            Console.WriteLine("\nDifferent XML file saved as newdata.xml");
        }
    }
}
