using System;
using MyLibrary;

class Program
{
    static void Main()
    {
        JsonXmlProcessor processor = new JsonXmlProcessor();
        processor.ProcessJsonAndXml("data.json");
    }
}
