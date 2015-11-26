using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Domain
{
    public class Parser
    {
        private readonly List<Highlight> _highlights = new List<Highlight>();

        private const string Separator = "==========";

        public void ParseFile(string fileName)
        {
            using (StreamReader stream = File.OpenText(fileName))
            {
                var sb = new StringBuilder();

                while (!stream.EndOfStream)
                {
                    string line = stream.ReadLine();

                    if (line != Separator)
                    {
                        sb.AppendLine(line);
                    }
                    else
                    {
                        var highlight = new Highlight(sb.ToString());
                        highlight.Parse();
                        _highlights.Add(highlight);

                        sb.Clear();
                    }
                }
            }
        }

        public IEnumerable<Highlight> GetHighlights()
        {
            return _highlights;
        }
    }
}