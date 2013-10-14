using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ytSubscriber.Models
{
    public class SubscriptionItem
    {
        public String Title { get; set; }
        public String Uploader { get; set; }
        public String Description { get; set; }
        public String Link { get; set; }
        public String ThumbnailLink { get; set; }
    }
}
