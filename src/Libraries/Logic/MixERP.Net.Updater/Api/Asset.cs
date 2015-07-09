using MixERP.Net.Common;
using System;

namespace MixERP.Net.Updater.Api
{
    public class Asset
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string State { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime PublishedAt { get; set; }
        public string DownloadUrl { get; set; }


        public static Asset Parse(dynamic toParse)
        {
            return new Asset
            {
                ContentType = toParse[Config.AssetContentTypeSubKey],
                CreatedAt = Conversion.TryCastDate(toParse[Config.CreatedAtKey]),
                DownloadUrl = toParse[Config.AssetDownloadUrlSubKey],
                Id = toParse[Config.AssetIdSubKey],
                Name = toParse[Config.NameKey],
                PublishedAt = Conversion.TryCastDate(toParse[Config.PublishedAtKey]),
                State = toParse[Config.AssetStateSubKey],
            };
        }
    }
}