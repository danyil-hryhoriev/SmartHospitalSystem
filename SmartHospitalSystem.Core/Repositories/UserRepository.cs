﻿using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using SmartHospitalSystem.Core.Interfaces.Configurations;
using SmartHospitalSystem.Core.Interfaces.Repositories;
using SmartHospitalSystem.Core.Models;
using SmartHospitalSystem.Core.Repositories;

namespace SmartHospitalSystem.Core.Repositories
{
    /// <summary>
    /// Manages users in the database
    /// </summary>
    public class UserRepository: MongoRepositoryBase<UserProfile>, IUserRepository
    {
        /// <summary>
        /// Constructor of UserRepository
        /// </summary>
        /// <param name="settings"></param>
        public UserRepository(IDbConfiguration settings) : base(settings)
        {
        }

        /// <summary>
        /// Collection name where will be stored users
        /// </summary>
        public override string CollectionName => "users";

        /// <summary>
        /// Returns model of user by user login
        /// </summary>
        /// <param name="login">Login of existing user</param>
        /// <returns></returns>
        public async Task<UserProfile> FindByLogin(string login)
        {
            var filter = new BsonDocument("login", login);
            var cursor = await MongoDatabase.GetCollection<UserProfile>(CollectionName)
                .FindAsync(filter);

            return await cursor.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Returns model of user by email
        /// </summary>
        /// <param name="email">Email of existing user</param>
        /// <returns></returns>
        public async Task<UserProfile> FindByEmail(string email)
        {
            var filter = new BsonDocument("login", email);
            var cursor = await MongoDatabase.GetCollection<UserProfile>(CollectionName)
                .FindAsync(filter);

            return await cursor.FirstOrDefaultAsync();
        }
    }
}
