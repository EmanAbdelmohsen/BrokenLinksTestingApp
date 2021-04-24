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


        void btn_browse_click(object sender, RoutedEventArgs e)
        {
            url = txtbx1.Text;

            if (string.IsNullOrEmpty(url))
                return;

            //clear all previous links to prepare for the new list related to the current URL
            UiUtility.Links.Clear();

            //navigate url
            browser.Navigate(new Uri(url));

            browser.Navigated += new NavigatedEventHandler(webBrowser1_DocumentNavigated);
        }

        void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            ListBoxItem lbi = (ListBoxItem)(sender as ListBox).SelectedItem;

            var bug = UiUtility.Links.Where(l => l.Id == lbi.TabIndex).FirstOrDefault();

            BugReportPage bugReport = new BugReportPage(bug.RequestDate, bug.URL, bug.ResponseCode, bug.ResponseDetails);

            //redirect to bug report page
            frame1.Navigate(bugReport);
            frame1.Visibility = Visibility.Visible;
        }

        void webBrowser1_DocumentNavigated(object sender, NavigationEventArgs e)
        {
            //detect all links found in the navigated page html content
            var links = WebPageLinkChecker.GetAllLinksFromPageUrl(url);

            foreach (var link in links)
            {
                //insert link to the global links list
                UiUtility.Links.Add(link);

                //update UI with the detected link
                UiUtility.InsertLinkToMainWindowListBox1(link);
            }
        }
    }
}
