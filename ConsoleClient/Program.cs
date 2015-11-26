using System;
using Domain;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser();
            parser.ParseFile(@"C:\My Clippings.txt");

            var library = new HighlightsLibrary(parser.GetHighlights());

            Console.WriteLine("Books:");
            foreach (var bookTitleWithCount in library.GetBookTitlesWithHighlightCounts())
            {
                Console.WriteLine($"{bookTitleWithCount.Item1} – ({bookTitleWithCount.Item2} highlights)");
            }

            Console.ReadLine();
        }
    }
}
