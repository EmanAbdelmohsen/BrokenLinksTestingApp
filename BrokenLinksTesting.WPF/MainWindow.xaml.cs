using BrokenLinksTesting.WPF.Common;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BrokenLinksTesting.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string url;

        public MainWindow()
        {
            InitializeComponent();
        }

        void webBrowser1_DocumentNavigated(object sender, NavigationEventArgs e)
        {
            //detect all links found in the navigated page html content
            var links = WebPageLinkChecker.GetAllLinksFromPageUrl(url);

            foreach (var link in links)
            {
                //update list box
                InsertLinkToListBox1(link);
            }
        }

        void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            ListBoxItem lbi = (ListBoxItem)(sender as ListBox).SelectedItem;

            var bug = Links.Where(l => l.Id == lbi.TabIndex).FirstOrDefault();

            BugReportPage bugReport = new BugReportPage(bug.RequestDate, bug.URL, bug.ResponseCode, bug.ResponseDetails);

            bugReport.Show();
            Close();
        }

        /// <summary>
        /// Browser navigation method
        /// </summary>
        /// <param name="pageUrl"> url to be browsed through the WebBrowser</param>
        void Browse(string pageUrl)
        {
            url = pageUrl;

            browser.Navigate(new Uri(pageUrl));

            browser.Navigated += new NavigatedEventHandler(webBrowser1_DocumentNavigated);
        }

        void InsertLinkToListBox1(Link link)
        {
            // Capture url and request body. This url is not root url
            Dispatcher.Invoke(() =>
            {
                var newItem = new ListBoxItem
                {
                    TabIndex = link.Id,
                    Content = $"{DateTime.Now.ToShortTimeString()}   {link.RequestMethod}   {link.ResponseCode}   {link.URL}   {LinkTypeEnum.Web_URL}"
                };

                listBox1.Items.Add(newItem);
            });
        }
    }
}
