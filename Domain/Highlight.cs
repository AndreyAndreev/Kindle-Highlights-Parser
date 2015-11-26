using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Domain
{
    public class Highlight
    {
        public string Book { get; set; }

        public string Text { get; set; }

        public int Page { get; set; }

        public DateTime AddedOn { get; set; }

        public string Author { get; set; }

        private readonly string _highlightString;

        public Highlight(string highlight)
        {
            _highlightString = highlight;
        }

        public void Parse()
        {
            var lines = _highlightString.Split(new [] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            string titleAndAuthor = lines[0];
            //todo ParseTitleAndAuthor(titleAndAuthor);
            Book = titleAndAuthor;
            
            string info = lines[1];
            ParseInfo(info);

            if (lines.Length < 3)
                return; // it's a bookmark, not hightlight

            Text = lines[2];
        }

        // Info looks like this:
        // - Your Highlight on page 479 | Location 7331-7335 | Added on Thursday, May 8, 2014 10:04:54 PM
        private void ParseInfo(string info)
        {
            ParsePageNumber(info);
            ParseAddedDate(info);
        }

        private void ParsePageNumber(string info)
        {
            string onPageFindResult = Regex.Match(info, "on page ([0-9]+)").Value;
            if (string.IsNullOrWhiteSpace(onPageFindResult))
                return;

            string pageString = onPageFindResult.Substring(8);
            int pageParsed;

            if (int.TryParse(pageString, out pageParsed))
            {
                Page = pageParsed;
            }
        }

        private void ParseAddedDate(string info)
        {
            string addedOnString = Regex.Match(info, "Added on ([a-zA-Z, 0-9:]+)").Value.Substring(9);
            DateTime addedOnParsed;

            if (DateTime.TryParse(addedOnString, out addedOnParsed))
            {
                AddedOn = addedOnParsed;
            }
        }

        private void ParseTitleAndAuthor(string titleAndAuthor)
        {
            throw new NotImplementedException(); // todo
        }
    }
}