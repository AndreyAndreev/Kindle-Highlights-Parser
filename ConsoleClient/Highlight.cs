using System;
using System.Text.RegularExpressions;

namespace ConsoleClient
{
    public class Highlight
    {
        public string Book { get; set; } 
        public string Info { get; set; }
        public string Text { get; set; }
        public int Page { get; set; }
        public DateTime AddedOn { get; set; }

        public void ParseInfo(string info)
        {
            // Info looks like this:
            // - Your Highlight on page 479 | Location 7331-7335 | Added on Thursday, May 8, 2014 10:04:54 PM


            string pageString = Regex.Match(info, "on page ([0-9]+)").Value.Substring(8);
            int pageParsed;

            if (int.TryParse(pageString, out pageParsed))
            {
                Page = pageParsed;
            }

            string addedOnString = Regex.Match(info, "Added on ([a-zA-Z, 0-9:]+)").Value.Substring(9);
            DateTime addedOnParsed;

            if (DateTime.TryParse(addedOnString, out addedOnParsed))
            {
                AddedOn = addedOnParsed;
            }
        }
    }
}