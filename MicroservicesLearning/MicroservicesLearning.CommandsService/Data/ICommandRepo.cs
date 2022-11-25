using MicroservicesLearning.CommandsService.Models;

namespace MicroservicesLearning.CommandsService.Data
{
    public interface ICommandRepo
    {
        bool SaveChanges();

        IEnumerable<Command> GetCommandsForPlatform(int platformId);

        Command GetCommand(int platformId, int commandId);

        void CreateCommand(int platformId, Command command);

        bool PlatformExists(int platformId);
    }
}
