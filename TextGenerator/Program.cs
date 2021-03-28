using System;

namespace TextGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            TextGenerator textGenerator = new TextGenerator();
            textGenerator.SetData("text.txt");
            Console.WriteLine(textGenerator.GetText());
            Console.ReadKey();
        }
    }
}
