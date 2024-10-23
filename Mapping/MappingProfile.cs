using AutoMapper;
using Re.DAL.Models;
using Re.PL.Areas.Dashboard.ViewModels;

namespace Re.PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ServiceVmForm, Services>().ReverseMap();
            CreateMap<Services , ServiceVM>();
            CreateMap<Services , SerDelailsVM>();
            //blog 
            CreateMap<BlogVMForm, Blogs>();
            CreateMap<Blogs, BlogVMForm>();
            CreateMap<Blogs, BlogVM>();
            CreateMap<Blogs, BlogDe>();

            //Portfolios
            CreateMap<PortfolioVMForm, Portfolios>().ReverseMap();
            CreateMap<Portfolios, PortfolioVM>();
            CreateMap<Portfolios, PotfolioDetails>();
            CreateMap<Portfolios, PortfolioVM>();


            //Slider
            CreateMap<SliderVMForm, Slider>().ReverseMap();
            CreateMap<Slider, SliderVM>();

            //Items

            CreateMap<ItemVMForm, Items>().ReverseMap();
            CreateMap<Items, ItemVM>();
            CreateMap<Items, ItemDe>();

            CreateMap<Items, ItemVMForm>()
              .ForMember(dest => dest.Portfolios, opt => opt.Ignore());







        }
    }
}
