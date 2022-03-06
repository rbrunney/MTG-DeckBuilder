using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using HtmlAgilityPack;

namespace MTG_DeckBuilder
{
    public partial class Search : Page
    {
        public Search()
        {
            InitializeComponent();
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            
            //Clearing Rows and Columns
            foundCards.RowDefinitions.Clear();
            foundCards.RowDefinitions.Add(new RowDefinition());


            HtmlWeb website = new HtmlWeb();
            HtmlDocument document = website.Load($"https://scryfall.com/search?q={txtCardSearch.Text.Replace(" ", "%20")}");
            var dataList = document.DocumentNode.SelectNodes("//div[@class='card-grid-item-card-front']");
            int nodeCount = 0;

            if (dataList != null)
            {
                foundCards.ColumnDefinitions.Clear();

                nodeCount = 0;
                foreach (HtmlNode content in dataList)
                {
                    foundCards.ColumnDefinitions.Add(new ColumnDefinition());
                    FindImages(content, nodeCount);
                    if (nodeCount == 3)
                    {
                        nodeCount = -1;
                    }
                    nodeCount++;
                }
            }
            else
            {
                dataList = document.DocumentNode.SelectNodes("//div[@class='card-image-front']");
                if (dataList != null)
                {
                    foundCards.ColumnDefinitions.Clear();

                    nodeCount = 0;
                    foreach (HtmlNode content in dataList)
                    {
                        foundCards.ColumnDefinitions.Add(new ColumnDefinition());
                        FindImages(content, nodeCount);
                        if (nodeCount == 3)
                        {
                            nodeCount = -1;
                        }
                        nodeCount++;
                    }
                }
                else
                {
                    MessageBox.Show("No Cards Found");
                }
            }
        }

        private void FindImages(HtmlNode content, int nodeCount)
        {
            string innerHtml = content.InnerHtml.Trim();
            string srcLink = innerHtml.Substring(innerHtml.IndexOf("https"));
            string imgLink = srcLink.Substring(0, srcLink.Length - 2);

            //Creating Card
            Image imgCard = new Image()
            {
                Height = 300,
                Width = 225,
                Source = new BitmapImage(new Uri(imgLink)),
                Margin = new Thickness(5),
            };

            imgCard.SetValue(Grid.ColumnProperty, nodeCount);
            imgCard.SetValue(Grid.RowProperty, foundCards.RowDefinitions.Count);
            imgCard.Cursor = Cursors.Hand;
            
            imgCard.MouseLeftButtonDown += (s, e) =>
            {
                MessageBox.Show(imgLink);
            };

            if (nodeCount == 3)
            {
                foundCards.RowDefinitions.Add(new RowDefinition());
            }
            
            foundCards.Children.Add(imgCard);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.frmMainFrame.Content = new Decks();
        }
    }
}