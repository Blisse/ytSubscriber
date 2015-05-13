using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using GalaSoft.MvvmLight;
using ytSubscriber.Models;
using System.Collections;

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

                _fileName = value;
                GetSubscriptionData();
                RaisePropertyChanged();
            }
        }

        public const string SubscriptionListDataPropertyKey = "SubscriptionListDataProperty";
        private Collection<SubscriptionItem> _subscriptionList = new Collection<SubscriptionItem>();
        public Collection<SubscriptionItem> SubscriptionList
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

                _subscriptionList = value;
                RaisePropertyChanged();
            }
        }

        public const string FilteredSubscriptionListDataPropertyKey = "FilteredSubscriptionListDataProperty";
        private ObservableCollection<SubscriptionItem> _filteredSubscriptionList = new ObservableCollection<SubscriptionItem>();
        public ObservableCollection<SubscriptionItem> FilteredSubscriptionList
        {
            get
            {
                return _filteredSubscriptionList;
            }
            set
            {
                if (_filteredSubscriptionList == value)
                {
                    return;
                }

                _filteredSubscriptionList = value;
                RaisePropertyChanged();
            }
        }

        public const string UploaderListDataPropertyKey = "UploaderListDataProperty";
        private ObservableCollection<String> _uploaderList = new ObservableCollection<String>();
        public ObservableCollection<String> UploaderList
        {
            get
            {
                return _uploaderList;
            }
            set
            {
                if (_uploaderList == value)
                {
                    return;
                }

                _uploaderList = value;
                RaisePropertyChanged();
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

            GetUploaderData();

            foreach (var subItem in SubscriptionList)
            {
                FilteredSubscriptionList.Add(subItem);
            } 
        }

        private void GetUploaderData()
        {
            UploaderList.Clear();

            var uploaders = SubscriptionList.Select(x => x.Uploader).Distinct();
            foreach (var uploader in uploaders)
            {
                UploaderList.Add(uploader);
            }
        }

        public void FilterSubscriptionList(IList uploaders)
        {
            if (uploaders != null)
            {
                FilteredSubscriptionList.Clear();

                var filteredList = SubscriptionList.Where(x => !uploaders.Contains(x.Uploader));
                foreach (var subItem in filteredList)
                {
                    FilteredSubscriptionList.Add(subItem);
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