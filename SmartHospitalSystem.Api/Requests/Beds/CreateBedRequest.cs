﻿using System;
using System.Runtime.Serialization;

namespace SmartHospitalSystem.Api.Requests
{
    /// <summary>
    /// Create bed request
    /// </summary>
    [DataContract]
    public class CreateBedRequest
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

        /// <summary>
        /// Date of unit creation
        /// </summary>
        [DataMember]
        public DateTime CreatedDate { get; set; }   // date and time
    }
}
