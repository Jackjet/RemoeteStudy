using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moblie
{
    public class DynamicInformationEntity
    {
        public string Title { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }

        public DynamicInformationEntity() { }
        public DynamicInformationEntity(string title, int count, string description, string imgUrl)
        {
            this.Title = title;
            this.Count = count;
            this.Description = description;
            this.ImgUrl = imgUrl;
        }
    }
}