using System.Collections.Generic;
using System.IO;

namespace ConsoleClient
{
    public class Parser
    {
        private string _fileContent;

        private List<Highlight> _highlights = new List<Highlight>();

        private const string Separator = "==========";

        public void OpenFile(string fileName)
        {
            using (StreamReader stream = File.OpenText(fileName))
            {
                int lineCounter = 0;
                var highlight = new Highlight();

                while (!stream.EndOfStream)
                {
                    string line = stream.ReadLine();
                    lineCounter++;

                    if (line != Separator)
                    {
                        switch (lineCounter)
                        {
                            case 1:
                                highlight.Book = line;
                                break;
                            case 2:
                                highlight.Info = line;
                                break;
                            case 4:
                                highlight.Text = line;
                                break;
                        }
                    }
                    else
                    {
                        highlight.ParseInfo(highlight.Info);
                        _highlights.Add(highlight);
                        highlight = new Highlight();
                        lineCounter = 0;
                    }
                }
            }
        }
    }
}