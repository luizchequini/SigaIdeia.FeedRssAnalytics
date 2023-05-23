using Microsoft.AspNetCore.Mvc;

namespace SigaIdeia.FeedRssAnalyticsApi.Configurations.FiltersAndAtttributes
{
    public class GlobalProducesRonponseTypeAttributes : ProducesResponseTypeAttribute
    {
        public string? ContentType { get; set; }
        public GlobalProducesRonponseTypeAttributes(Type type, int statusCode) : base(type, statusCode)
        {
            if (statusCode == 200 || statusCode == 201 || statusCode == 204)
            {
                ContentType = "application/json";
            }
        }
    }
}
