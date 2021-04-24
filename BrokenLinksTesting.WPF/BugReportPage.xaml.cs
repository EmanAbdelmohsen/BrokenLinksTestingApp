using System.Windows.Controls;

namespace BrokenLinksTesting.WPF
{
    /// <summary>
    /// Interaction logic for BugReportPage.xaml
    /// </summary>
    public partial class BugReportPage : Page
    {
        public BugReportPage(string bugDate, string url, string statusCode, string responseDetails)
        {
            InitializeComponent();

            //update components with the given info
            lbl_date.Content = bugDate;
            lbl_statusCode.Content = statusCode;
            lbl_url.Content = url;
            lbl_response.Content = responseDetails;
        }
    }
}
