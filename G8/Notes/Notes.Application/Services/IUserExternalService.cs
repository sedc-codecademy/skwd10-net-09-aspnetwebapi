using Notes.Application.Models.External;


namespace Notes.Application.Services
{
    public interface IUserExternalService
    {
        Task<IEnumerable<ExternalUser>> GetExternalUsers(CancellationToken token = default);
    }
}
