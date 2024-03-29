﻿using System.Linq;
using AutoMapper;
using SmartHospitalSystem.Api.Requests;
using SmartHospitalSystem.Api.Responses;
using SmartHospitalSystem.Core.Models;

namespace SmartHospitalSystem.Api.Mapper
{
    /// <summary>
    /// Standert mapper profile
    /// </summary>
    public class StandartProfile : Profile
    {
        /// <summary>
        /// Ctor for profile
        /// </summary>
        public StandartProfile()
        {
            // departments
            CreateMap<CreateDepartmentRequest, DepartmentModel>();
            CreateMap<UpdateDepartmentRequest, DepartmentModel>();

            CreateMap<DepartmentModel, CreateDepartmentResponse>();
            CreateMap<DepartmentModel, UpdateDepartmentResponse>();
            CreateMap<DepartmentModel, DepartmentResponse>();

            // beds
            CreateMap<CreateBedRequest, BedModel>();
            CreateMap<UpdateBedRequest, BedModel>();

            CreateMap<BedModel, CreateBedResponse>();
            CreateMap<BedModel, UpdateBedResponse>();
            CreateMap<BedModel, BedResponse>();

            // users
            CreateMap<CreateUserRequest, UserProfile>();
            CreateMap<UpdateUserRequest, UserProfile>();

            CreateMap<UserProfile, CreateUserResponse>();
            CreateMap<UserProfile, UpdateUserResponse>();
            CreateMap<UserProfile, UserResponse>();

            // patients
            CreateMap<CreateVisitRequest, VisitModel>();
            CreateMap<VisitModel, VisitReponse>();
            CreateMap<PatientModel, PatientResponse>();

            // procedures
            CreateMap<CreateProcedureRequest, ProcedureModel>();
            CreateMap<UpdateProcedureRequest, ProcedureModel>();

            CreateMap<ProcedureModel, ProcedureResponse>();
            CreateMap<ProcedureModel, CreateProcedureResponse>();
            CreateMap<ProcedureModel, UpdateProcedureResponse>();
        }
    }
}
