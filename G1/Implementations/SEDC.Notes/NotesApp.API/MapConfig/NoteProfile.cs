using AutoMapper;
using NotesApp.DataModels;
using NotesApp.InerfaceModels.Models;

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
