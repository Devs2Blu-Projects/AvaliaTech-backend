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
        private readonly IGlobalService _globalService;

        public GroupService(DataContext context, IGlobalService globalService)
        {
            _context = context;
            _globalService = globalService;
        }

        public async Task<IEnumerable<GroupDto>> GetGroups()
        {
            return await _context.Groups
                .Include(g => g.Proposition).Include(g => g.GroupRatings)
                .Select(g => new GroupDto(g)).ToListAsync();
        }

        public async Task<GroupDto?> GetGroupById(int id)
        {
            var group = await _context.Groups
                .Include(g => g.Proposition).Include(g => g.GroupRatings)
                .FirstOrDefaultAsync(g => g.Id == id);

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
            var group = await _context.Groups
                .Include(g => g.Proposition).Include(g => g.GroupRatings)
                .FirstOrDefaultAsync(g => g.UserId == idUser);

            if (group == null) return null;

            return new GroupDto(group);
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsRanking(string role)
        {
            var currentEvent = await _globalService.GetCurrentEvent() ?? throw new Exception($"Evento atual não selecionado!");

            if (!currentEvent.IsClosed) return Enumerable.Empty<GroupDto>();
            if ((role != UserRoles.Admin) || (!currentEvent.IsPublic)) return Enumerable.Empty<GroupDto>();

            var propositionIds = await _context.Propositions
                .Where(p => p.EventId == currentEvent.Id)
                .Select(p => p.Id).ToListAsync();

            return await _context.Groups
                .Where(g => propositionIds.Contains(g.PropositionId ?? -1))
                .Include(g => g.Proposition).Include(g => g.GroupRatings)
                .OrderByDescending(g => g.FinalGrade)
                .Select(g => new GroupDto(g)).ToListAsync();
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsToRate(int idUser)
        {
            var currentEvent = await _globalService.GetCurrentEvent() ?? throw new Exception($"Evento atual não selecionado!");

            if (currentEvent.IsClosed) return Enumerable.Empty<GroupDto>();

            var ratedGroupIdList = await _context.Ratings.Where(r => r.UserId == idUser).Select(r => r.GroupId).ToListAsync();

            return await _context.Groups
                .Include(g => g.Proposition)
                .Where(g => (!ratedGroupIdList.Contains(g.Id)) && (DateTime.Now == currentEvent.StartDate.AddDays(g.DateOffset)))
                .Select(g => new GroupDto(g)).ToListAsync();
        }

        public async Task<IEnumerable<GroupsByDateDTO>> GetAllEventGroupsByDate()
        {
            EventModel? currentEvent = await _globalService.GetCurrentEvent() ?? throw new Exception($"Evento atual não selecionado!");

            DateTime startDate = currentEvent.StartDate.Date;
            DateTime endDate = currentEvent.EndDate.Date;

            List<GroupsByDateDTO> groupsByDate = new List<GroupsByDateDTO>();

            for (DateTime dt = startDate; dt <= endDate; dt.AddDays(1))
            {
                groupsByDate.Add(new GroupsByDateDTO
                {
                    Date = dt,
                });
            }

            var groups = await _context.Groups
                .Include(g => g.Proposition)
                .Where(g => g.EventId == currentEvent.Id)
                .OrderBy(g => g.DateOffset)
                .ToListAsync();

            foreach (var group in groups)
            {
                groupsByDate[(int)group.DateOffset].Groups.Add(new GroupDto(group));
            }

            return groupsByDate;
        }
    }
}