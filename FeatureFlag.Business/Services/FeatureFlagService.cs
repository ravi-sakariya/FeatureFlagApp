using FeatureFlag.Database;
using FeatureFlag.Domain.Entity;
using FeatureFlag.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureFlag.Business.Services
{
    public class FeatureFlagService : IFeatureFlagService
    {
        private readonly FeatureFlagDbContext _db;

        public FeatureFlagService(FeatureFlagDbContext db)
        {
            _db = db;
        }

        public async Task<List<FeatureFlag.Domain.Entity.FeatureFlag>> GetAll()
        {
            return await _db.FeatureFlags.ToListAsync();
        }

        public async Task<Domain.Entity.FeatureFlag> GetByName(string name)
        {
            var flag = await _db.FeatureFlags.FirstOrDefaultAsync(f => f.Name == name);
            return flag;
        }

        public async Task<bool> IsEnabled(string flagName, string userId, List<string> userGroups)
        {
            var flag = _db.FeatureFlags.FirstOrDefault(f => f.Name == flagName); 
            
            if (flag == null) return false; 
            
            if (flag.TargetUsers.Contains(userId)) 
                return true; 
            
            if (userGroups.Any(g => flag.TargetGroups.Contains(g))) 
                return true; 
            
            bool enabled = flag.Enabled;

            return enabled;
        }

        public async Task Create(FeatureFlag.Domain.Entity.FeatureFlag featureFlag)
        {
            _db.FeatureFlags.Add(featureFlag);
            await _db.SaveChangesAsync();
        }

        public async Task<FeatureFlag.Domain.Entity.FeatureFlag> Update(FeatureFlag.Domain.Entity.FeatureFlag featureFlag)
        {
            var flag = await GetByName(featureFlag.Name);

            if (flag == null) return null;

            flag.Enabled = featureFlag.Enabled;

            flag.TargetUsersCsv = featureFlag.TargetUsersCsv;
            flag.TargetGroupsCsv = featureFlag.TargetGroupsCsv;

            await _db.SaveChangesAsync();

            return flag;
        }

        public async Task Delete(string flagName)
        {
            var flag = await GetByName(flagName);

            _db.FeatureFlags.Remove(flag);

            await _db.SaveChangesAsync();
        }
    }
}
