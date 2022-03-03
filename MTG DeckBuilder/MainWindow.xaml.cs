using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using HtmlAgilityPack;

namespace MTG_DeckBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TestFetch(object sender, RoutedEventArgs e)
        {
            HtmlWeb website = new HtmlWeb();
            HtmlDocument document = website.Load($"https://scryfall.com/search?q={txtCardSearch.Text.Replace(" ", "%20")}");
            var dataList = document.DocumentNode.SelectNodes("//div[@class='card-grid-item-card-front']");
            
            foundCards.ColumnDefinitions.Clear();
            if (dataList != null)
            {
                foreach (HtmlNode content in dataList)
                {
                    FindImages(content);
                }
            }
            else
            {
                dataList = document.DocumentNode.SelectNodes("//div[@class='card-image-front']");
                if (dataList != null)
                {
                    foreach(HtmlNode content in dataList)
                    {
                        FindImages(content);
                    }
                }
                else
                {
                    MessageBox.Show("No Cards Found");
                }
            }
        }

        public void FindImages(HtmlNode content)
        {
            string innerHtml = content.InnerHtml.Trim();
            string srcLink = innerHtml.Substring(innerHtml.IndexOf("https"));
            string imgLink = srcLink.Substring(0, srcLink.Length - 2);

            //Creating Image
            Image imgCard = new Image
            {
                Height = 200,
                Width = 300
            };
            
            imgCard.Source = new BitmapImage(new Uri(imgLink));
            imgCard.SetValue(Grid.ColumnProperty, foundCards.ColumnDefinitions.Count);
            imgCard.SetValue(Grid.RowProperty, foundCards.RowDefinitions.Count);
            foundCards.ColumnDefinitions.Add(new ColumnDefinition());
            foundCards.Children.Add(imgCard);
        }
    }
}