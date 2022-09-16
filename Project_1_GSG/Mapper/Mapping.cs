using AutoMapper;
using Project_1_GSG.Models;
using Project_1_GSG.ModelView;
using System;

namespace Project_1_GSG.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CsvView, CsvModelView>().ReverseMap();
        }
    }
}
