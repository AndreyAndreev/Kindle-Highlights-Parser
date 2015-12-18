using System;
using System.Collections.Generic;
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
    }
}
