﻿using System.Threading.Tasks;
using SmartHospitalSystem.Core.Interfaces.Repositories;
using SmartHospitalSystem.Core.Models;

namespace SmartHospitalSystem.Core.Interfaces.Repositories
{
    /// <summary>
    /// Manages users in the database
    /// </summary>
    public interface IUserRepository: IRepositoryBase<UserProfile>
    {
        /// <summary>
        /// Returns model of user by user login
        /// </summary>
        /// <param name="login">Login of existing user</param>
        /// <returns></returns>
        Task<UserProfile> FindByLogin(string login);


        /// <summary>
        /// Returns model of user by email
        /// </summary>
        /// <param name="email">Email of existing user</param>
        /// <returns></returns>
        Task<UserProfile> FindByEmail(string email);
    }
}
