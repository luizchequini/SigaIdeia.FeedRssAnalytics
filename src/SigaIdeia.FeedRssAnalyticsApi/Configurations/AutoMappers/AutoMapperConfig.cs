using AutoMapper;
using SigaIdeia.FeedRssAnalytics.Domain.Entities;
using SigaIdeia.FeedRssAnalyticsApi.DTOs;

namespace SigaIdeia.FeedRssAnalyticsApi.Configurations.AutoMappers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Authors, AuthorsDto>().ReverseMap();
            CreateMap<Feed, FeedDto>().ReverseMap();
        }
    }
}
