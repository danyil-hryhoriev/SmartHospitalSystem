﻿using System;
using System.Runtime.Serialization;

namespace SmartHospitalSystem.Api.Requests
{
    /// <summary>
    /// Update bed request
    /// </summary>
    [DataContract]
    public class UpdateBedRequest
    {
        /// <summary>
        /// Gets or set department Id
        /// </summary>
        [DataMember]
        public string DepartmentId { get; set; }

        /// <summary>
        /// Gets or set bed id
        /// </summary>
        [DataMember]
        public string BedId { get; set; }

        /// <summary>
        /// Gets or set bed name
        /// </summary>
        [DataMember]
        public string BedName { get; set; }

        /// <summary>
        /// Gets or set patient id
        /// </summary>
        [DataMember]
        public string PatientId { get; set; }
    }
}
