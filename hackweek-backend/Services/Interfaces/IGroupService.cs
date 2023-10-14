using hackweek_backend.dtos;
using hackweek_backend.DTOs;

namespace hackweek_backend.Services.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDto>> GetGroups();
        Task<GroupDto?> GetGroupById(int id);
        Task UpdateGroup(int id, GroupDtoUpdate request);

        Task<GroupDto?> GetGroupByUser(int idUser);
        Task<IEnumerable<GroupDto>> GetGroupsRanking(string role);
        Task<IEnumerable<GroupDto>> GetGroupsToRate(int idUser);
        Task<IEnumerable<GroupsByDateDTO>> GetAllEventGroupsByDate();
    }
}