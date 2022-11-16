﻿using MicroservicesLearning.PlatformService.Models;

namespace MicroservicesLearning.PlatformService.Data
{
    public interface IPlatformRepository
    {
        bool SaveChanges();

        IEnumerable<Platform> GetAllPlatforms();

        Platform GetPlatformById(int id);

        void CreatePlatform(Platform platform);
    }
}
