using hackweek_backend.dtos;
using hackweek_backend.DTOs;
using hackweek_backend.Models;

namespace hackweek_backend.Services.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDto>> GetGroups();
        Task<GroupDto?> GetGroupById(int id);
        Task UpdateGroup(int id, GroupDtoUpdate request);

        Task<GroupDto?> GetGroupByUser(int idUser);
        Task<IEnumerable<GroupDto>> GetGroupsByProposition(int idProposition);
        Task<IEnumerable<GroupDto>> GetGroupsOnQueue();
        Task<IEnumerable<GroupDto>> GetGroupsToRate(int idUser);
        Task<IEnumerable<GroupDto>> GetGroupsDone();
    }
}