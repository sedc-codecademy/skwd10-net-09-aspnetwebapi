using AutoMapper;
using NotesApp.DataModels;
using SEDC.Notes.InerfaceModels.Models;

namespace NotesApp.API.MapConfig
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            // source => destination
            CreateMap<NoteModel, NoteDto>();
        }
    }
}
