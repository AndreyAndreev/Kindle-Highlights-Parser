using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Domain;

namespace UI.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private HighlightsLibrary library;

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            var parser = new Parser();
            parser.ParseFile(@"C:\My Clippings.txt");

            library = new HighlightsLibrary(parser.GetHighlights());

            foreach (var bookTitleWithCount in library.GetBookTitlesWithHighlightCounts())
            {
                var title = $"{bookTitleWithCount.Item1} – ({bookTitleWithCount.Item2} highlights)";

                Books.Items.Add(new Label() {Content = title});
            }
        }

        private void Books_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var titles = library.GetBookTitles();

            var selectedTitle = titles.ElementAt(Books.SelectedIndex);

            var highlights = library.GetHighlightsForBookTitle(selectedTitle);

            Highlights.Text = string.Join("\n\n\n", highlights.Select(x => x.Text));
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!doctype html><html><body>");

            int id = 0;

            sb.AppendLine("<ul id=\"top\">");
            foreach (var bookTitleWithCount in library.GetBookTitlesWithHighlightCounts())
            {
                sb.AppendLine($"<li>{bookTitleWithCount.Item1} – (<a href=\"#id_{id++}\">{bookTitleWithCount.Item2} highlights</a>)</li>");
            }
            sb.AppendLine("</ul>");

            id = 0;
            foreach (var bookTitleWithCount in library.GetBookTitlesWithHighlightCounts())
            {
                var title = $"{bookTitleWithCount.Item1} – ({bookTitleWithCount.Item2} highlights)";

                sb.AppendLine($"<h1 id=\"id_{id++}\">{title}</h1>");

                sb.AppendLine("<ul>");

                foreach (var item in library.GetHighlightsForBookTitle(bookTitleWithCount.Item1))
                {
                    sb.AppendLine($"<li>{item.Text}</li>");
                }

                sb.AppendLine("</ul>");

                sb.AppendLine("<a href=\"#top\">top</a>");
            }

            sb.AppendLine("</body></html>");

            File.WriteAllText("clippings.html", sb.ToString());
        }
    }
}
