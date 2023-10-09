using hackweek_backend.Data;
using hackweek_backend.dtos;
using hackweek_backend.DTOs;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;
using System.Data;

namespace hackweek_backend.Services
{
    public class GroupService : IGroupService
    {
        private readonly DataContext _context;

        public GroupService(DataContext context) { _context = context; }

        public async Task<IEnumerable<GroupDto>> GetGroups()
        {
            return await _context.Groups.Select(g => new GroupDto(g)).ToListAsync();
        }
        
        public async Task<GroupDto?> GetGroupById(int id)
        {
            var group = await _context.Groups.FindAsync(id);

            if (group == null) return null;

            return new GroupDto(group);
        }

        public async Task UpdateGroup(int id, GroupDtoUpdate request)
        {
            if (request.Id != id) throw new Exception("Id diferente do grupo informado!");

            var group = await _context.Groups.FindAsync(id) ?? throw new Exception($"Grupo não encontrado! ({request.Id})");

            group.Team = request.Team;
            group.Language = request.Language;
            group.PropositionId = request.PropositionId;
            group.ProjectName = request.ProjectName;
            group.ProjectDescription = request.ProjectDescription;

            await _context.SaveChangesAsync();
        }

        public async Task<GroupDto?> GetGroupByUser(int idUser)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(g => g.UserId == idUser);

            if (group == null) return null;

            return new GroupDto(group);
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsByProposition(int idProposition)
        {
            return await _context.Groups.Where(g => g.PropositionId == idProposition).Select(g => new GroupDto(g)).ToListAsync();
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsOnQueue()
        {
            return await _context.Groups.Where(g => g.EndTime == null).Select(g => new GroupDto(g)).ToListAsync();
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsToRate(int idUser)
        {
            var ratedGroupIdList = await _context.Ratings.Where(r => r.UserId == idUser).Select(r => r.GroupId).ToListAsync();
            return await _context.Groups.Where(g => (g.StartTime != null) && (!ratedGroupIdList.Contains(g.Id))).Select(g => new GroupDto(g)).ToListAsync();
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsDone()
        {
            return await _context.Groups.Where(g => g.EndTime != null).Select(g => new GroupDto(g)).ToListAsync();
        }
    }
}