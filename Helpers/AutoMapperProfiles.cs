using AutoMapper;
using CostRegApp2.Data;
using CostRegApp2.DTOs;

namespace CostRegApp2.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Costs, CostDto>();
            CreateMap<Income, IncomeDto>();
            CreateMap<CostPlans, CostPlansDto>();
        }
    }
}
