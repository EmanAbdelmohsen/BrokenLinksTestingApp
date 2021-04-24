using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BrokenLinksTesting.WPF.Common
{
    public static class UiUtility
    {
        /// <summary>
        /// List of all detected links for the browser
        /// </summary>
        public static List<Link> Links = new List<Link>();

        public static void InsertLinkToMainWindowListBox1(Link link)
        {
            //iterate through the application active windows to get the MainWindow form
            //use Dispatcher to update UI from another thread other than the UI thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        var newItem = new ListBoxItem
                        {
                            TabIndex = link.Id,
                            Content = $"{DateTime.Now.ToShortTimeString()}   {link.RequestMethod}   {link.ResponseCode}   {LinkTypeEnum.REST_Api}      {link.URL}"
                        };

                        //update listBox1
                        (window as MainWindow).listBox1.Items.Add(newItem);

                        break;
                    }
                }
            });
        }
    }
}
