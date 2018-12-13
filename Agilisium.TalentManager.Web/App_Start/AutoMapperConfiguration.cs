using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Web.Models;
using AutoMapper;

namespace Agilisium.TalentManager.Web.App_Start
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                //x.AddProfile(new DomainToViewModelMappingProfile());
                //x.AddProfile(new ViewModelToDomainMappingProfile());
            });
        }
    }

    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName => "DomainToViewModelMappingProfile";

        public DomainToViewModelMappingProfile()
        {
            CreateMap<DropDownCategoryDto, CategoryModel>();
            CreateMap<DropDownSubCategoryDto, SubCategoryViewModel>();
            CreateMap<EmployeeDto, EmployeeViewModel>();
            CreateMap<PracticeDto, PracticeViewModel>();
            CreateMap<SubPracticeDto, SubPracticeViewModel>();
            CreateMap<ProjectDto, ProjectViewModel>();
            CreateMap<ProjectAllocationDto, AllocationViewModel>();
        }
    }

    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName => "ViewModelToDomainMappingProfile";

        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CategoryModel, DropDownCategoryDto>();
            CreateMap<SubCategoryViewModel, DropDownSubCategoryDto>();
            CreateMap<EmployeeViewModel, EmployeeDto>();
            CreateMap<PracticeViewModel, PracticeDto>();
            CreateMap<SubPracticeViewModel, SubPracticeDto>();
            CreateMap<ProjectViewModel, ProjectDto>();
            CreateMap<AllocationViewModel, ProjectAllocationDto>();
        }
    }
}