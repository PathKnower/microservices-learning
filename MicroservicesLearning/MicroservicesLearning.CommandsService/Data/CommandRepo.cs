using MicroservicesLearning.CommandsService.Models;
using System;

namespace MicroservicesLearning.CommandsService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _dbContext;

        public CommandRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateCommand(int platformId, Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.PlatformId = platformId;
            _dbContext.Commands.Add(command);
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return _dbContext.Commands
                .FirstOrDefault(x => x.PlatformId == platformId && x.Id == commandId);
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _dbContext.Commands
                .Where(x => x.PlatformId == platformId)
                .OrderBy(x => x.Platform.Name);
        }

        public bool PlatformExists(int platformId)
        {
            return _dbContext.Platforms.Any(x => x.Id == platformId);
        }

        public bool SaveChanges()
        {
            return _dbContext.SaveChanges() >= 0;
        }
    }
}
