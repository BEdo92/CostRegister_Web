using AutoMapper;
using CostRegApp2.Data;
using CostRegApp2.DTOs;

namespace CostRegApp2.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Costs, CostDto>()
                .ForMember(destination => destination.CategoryName, opt => opt.MapFrom(source => source.Category.CategoryName))
                .ForMember(destination => destination.ShopName, opt => opt.MapFrom(source => source.Shop.ShopName));

            CreateMap<Income, IncomeDto>();

            CreateMap<CostPlans, CostPlansDto>()
                .ForMember(destination => destination.CategoryName, opt => opt.MapFrom(source => source.Category.CategoryName));

            CreateMap<CostDto, Costs>()
                .ForPath(destination => destination.Category.CategoryName, opt => opt.MapFrom(source => source.CategoryName))
                .ForPath(destination => destination.Shop.ShopName, opt => opt.MapFrom(source => source.ShopName));

            CreateMap<IncomeDto, Income>();

            CreateMap<CostPlansDto, CostPlans>()
                .ForPath(destination => destination.Category.CategoryName, opt => opt.MapFrom(source => source.CategoryName));
        }
    }
}
