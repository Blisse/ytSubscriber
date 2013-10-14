using System;
using System.Collections.ObjectModel;
using System.IO;
using GalaSoft.MvvmLight;
using ytSubscriber.Models;

namespace ytSubscriber.ViewModels
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public const string FileNameDataPropertyKey = "FileNameDataProperty";

        private String _fileName = "";
        public String FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                if (_fileName == value)
                {
                    return;
                }

                RaisePropertyChanging(FileNameDataPropertyKey);
                _fileName = value;
                GetSubscriptionData();
                RaisePropertyChanged(FileNameDataPropertyKey);
            }
        }

        public const string SubscriptionListDataPropertyKey = "SubscriptionListDataProperty";
        private ObservableCollection<SubscriptionItem> _subscriptionList = new ObservableCollection<SubscriptionItem>();
        public ObservableCollection<SubscriptionItem> SubscriptionList
        {
            get
            {
                return _subscriptionList;
            }
            set
            {
                if (_subscriptionList == value)
                {
                    return;
                }

                RaisePropertyChanging(SubscriptionListDataPropertyKey);
                _subscriptionList = value;
                RaisePropertyChanged(SubscriptionListDataPropertyKey);
            }
        }
        
        private void GetSubscriptionData()
        {
            SubscriptionList.Clear();

            var htmlText = File.ReadAllText(FileName);
            var htmlDoc = new HtmlAgilityPack.HtmlDocument
            {
                OptionFixNestedTags = true,
                OptionAutoCloseOnEnd = true
            };

            htmlDoc.LoadHtml(htmlText);

            if (htmlDoc.DocumentNode != null)
            {
                var subscribedVideoNodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='feed-item-main']");

                foreach (var subscribedVideoNode in subscribedVideoNodes)
                {
                    if (subscribedVideoNode != null)
                    {
                        var subItem = new SubscriptionItem();

                        var headerNode = subscribedVideoNode.SelectSingleNode(".//*[contains(@class,'feed-item-header')]");

                        if (headerNode != null)
                        {
                            var headerStringNode = headerNode.SelectSingleNode(".//*[contains(@class,'yt-user-name')]");
                            if (headerStringNode != null)
                            {
                                subItem.Uploader = headerStringNode.InnerText.Trim();
                            }
                        }

                        var contentNode =
                            subscribedVideoNode.SelectSingleNode(".//*[contains(@class,'yt-lockup-content')]");

                        if (contentNode != null)
                        {
                            var titleNode = contentNode.SelectSingleNode(".//*[contains(@class,'yt-uix-tile-link')]");
                            if (titleNode != null)
                            {
                                if (titleNode.Attributes["title"] != null)
                                {
                                    subItem.Title = titleNode.Attributes["title"].Value.Trim();
                                }

                                if (titleNode.Attributes["href"] != null)
                                {
                                    subItem.Link = String.Format("http://www.youtube.com/{0}", titleNode.Attributes["href"].Value);
                                }
                            }

                        }


                        var descriptionStringNode =
                            subscribedVideoNode.SelectSingleNode(".//*[contains(@class,'yt-lockup-description')]");

                        if (descriptionStringNode != null)
                        {
                            subItem.Description = descriptionStringNode.InnerText.Trim();
                        }


                        var thumbnailLinkNode =
                            subscribedVideoNode.SelectSingleNode(".//*[contains(@class,'yt-thumb-clip-inner')]");

                        if (thumbnailLinkNode != null)
                        {
                            var thumbnailLinkImageNode = thumbnailLinkNode.SelectSingleNode("img");
                            if (thumbnailLinkImageNode != null &&
                                thumbnailLinkImageNode.Attributes["data-thumb"] != null)
                            {
                                subItem.ThumbnailLink = "http:" + thumbnailLinkImageNode.Attributes["data-thumb"].Value;
                            }
                        }

                        SubscriptionList.Add(subItem);
                    }
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }
    }
}