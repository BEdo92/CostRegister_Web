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
                .ForMember(m => m.CategoryName, opt => opt.MapFrom(source => source.Category.CategoryName))
                .ForMember(m => m.ShopName, opt => opt.MapFrom(source => source.Shop.ShopName));

            CreateMap<Income, IncomeDto>();

            CreateMap<CostPlans, CostPlansDto>()
                .ForMember(m => m.CategoryName, opt => opt.MapFrom(source => source.Category.CategoryName));

            CreateMap<CostDto, Costs>()
                .ForPath(m => m.Category.CategoryName, opt => opt.MapFrom(source => source.CategoryName))
                .ForPath(m => m.Shop.ShopName, opt => opt.MapFrom(source => source.ShopName));

            CreateMap<IncomeDto, Income>();

            CreateMap<CostPlansDto, CostPlans>()
                .ForPath(m => m.Category.CategoryName, opt => opt.MapFrom(source => source.CategoryName));
        }
    }
}
