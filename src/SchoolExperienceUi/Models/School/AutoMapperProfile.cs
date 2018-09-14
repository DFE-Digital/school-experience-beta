using System.Collections.Generic;
using AutoMapper;
using SchoolExperienceApiDto.School;

namespace SchoolExperienceUi.Models.School
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SchoolExperienceApiDto.School.GetDiaryEntriesResponse.DiaryEvent, DiaryViewModelEvent>()
                .ForMember(dst => dst.Start, opt => opt.MapFrom(src => src.When))
                .ForMember(dst => dst.Title, opt => opt.MapFrom(src => $"{src.CandidateName} ({src.CandidateSubject})"))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.IsFree, opt => opt.MapFrom(src => src.EntryType == SchoolExperienceBaseTypes.DiaryEntryType.Free))
                .ForMember(dst => dst.IsBusy, opt => opt.MapFrom(src => src.EntryType == SchoolExperienceBaseTypes.DiaryEntryType.Booked))
                ;
            CreateMap<SchoolExperienceApiDto.School.GetDiaryEntriesResponse, DiaryViewModel>();
            CreateMap<IEnumerable<SchoolExperienceApiDto.School.GetDiaryEntriesResponse.DiaryEvent>, List<DiaryViewModelEvent>>();
        }
    }
}
