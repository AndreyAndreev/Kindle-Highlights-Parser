using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class HighlightsLibrary
    {
        private readonly List<Highlight> _highlights;

        public HighlightsLibrary(IEnumerable<Highlight> highlights)
        {
            _highlights = new List<Highlight>(highlights);
        }

        public IEnumerable<string> GetBookTitles()
        {
            return _highlights.GroupBy(x => x.Book)
                .Select(x => x.Key)
                .OrderBy(x => x);
        }

        public IEnumerable<Tuple<string, int>> GetBookTitlesWithHighlightCounts()
        {
            return _highlights.GroupBy(x => x.Book)
                .Select(x => new Tuple<string, int>(x.Key, x.Count()))
                .OrderBy(x => x.Item1);
        }

        public IEnumerable<Highlight> GetHighlightsForBookTitle(string title)
        {
            return _highlights.Where(x => x.Book == title).OrderBy(x => x.Page);
        }
    }
}