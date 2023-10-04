using hackweek_backend.Models;

namespace hackweek_backend.Services.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupModel>> GetGroups();
        Task<GroupModel?> GetGroupById(int id);
        Task CreateGroup(GroupModel Group);
        Task DeleteGroup(int id);
        Task UpdateGroup(int id, GroupModel Group);

        Task<IEnumerable<GroupModel>> GetGroupsByProposition(int idProposition);
        Task<IEnumerable<GroupModel>> GetGroupsOnQueue();
        Task<IEnumerable<GroupModel>> GetGroupsToRate(int idUser);
        Task<IEnumerable<GroupModel>> GetGroupsDone();
    }
}