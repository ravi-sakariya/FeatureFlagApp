using FeatureFlag.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureFlag.Domain.Interfaces
{
    public interface IFeatureFlagService
    {
        Task<List<Entity.FeatureFlag>> GetAll();
        Task<Entity.FeatureFlag> GetByName(string name);
        Task<bool> IsEnabled(string flagName, string userId, List<string> userGroups);
        Task Create(FeatureFlag.Domain.Entity.FeatureFlag featureFlag);
        Task<FeatureFlag.Domain.Entity.FeatureFlag> Update(FeatureFlag.Domain.Entity.FeatureFlag featureFlag);
        Task Delete(string flagName);
    }
}
