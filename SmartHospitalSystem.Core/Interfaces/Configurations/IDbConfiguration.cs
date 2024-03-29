﻿namespace SmartHospitalSystem.Core.Interfaces.Configurations
{
    /// <summary>
    /// Configuration of DB
    /// </summary>
    public interface IDbConfiguration
    {
        /// <summary>
        /// Connection string
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Database name
        /// </summary>
        string Database { get; }
    }
}
