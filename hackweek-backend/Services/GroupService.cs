using hackweek_backend.Data;
using hackweek_backend.dtos;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;
using System.Data;

namespace hackweek_backend.Services
{
    public class GroupService : IGroupService
    {
        private readonly DataContext _context;

        public GroupService(DataContext context) { _context = context; }

        public async Task<IEnumerable<GroupModel>> GetGroups()
        {
            return await _context.Groups.ToListAsync();
        }
        
        public async Task<GroupModel?> GetGroupById(int id)
        {
            return await _context.Groups.FindAsync(id);
        }

        public async Task UpdateGroup(int id, GroupDtoUpdate request)
        {
            if (request.Id != id) throw new Exception("Id diferente do grupo informado!");

            var group = await _context.Groups.FindAsync(id) ?? throw new Exception($"Grupo não encontrado! ({request.Id})");

            group.Team = request.Team;
            group.ProjectDescription = request.ProjectDescription;
            group.Language = request.Language;
            group.PropositionId = request.PropositionId;

            await _context.SaveChangesAsync();
        }

        public async Task<GroupModel?> GetGroupByUser(int idUser)
        {
            return await _context.Groups.FirstOrDefaultAsync(g => g.UserId == idUser);
        }

        public async Task<IEnumerable<GroupModel>> GetGroupsByProposition(int idProposition)
        {
            return await _context.Groups.Where(g => g.PropositionId == idProposition).ToListAsync();
        }

        public async Task<IEnumerable<GroupModel>> GetGroupsOnQueue()
        {
            return await _context.Groups.Where(g => g.EndTime == null).ToListAsync();
        }

        public async Task<IEnumerable<GroupModel>> GetGroupsToRate(int idUser)
        {
            var ratedGroupIdList = await _context.Ratings.Where(r => r.UserId == idUser).Select(r => r.GroupId).ToListAsync();
            return await _context.Groups.Where(g => (g.StartTime != null) && (!ratedGroupIdList.Contains(g.Id))).ToListAsync();
        }

        public async Task<IEnumerable<GroupModel>> GetGroupsDone()
        {
            return await _context.Groups.Where(g => g.EndTime != null).ToListAsync();
        }
    }
}