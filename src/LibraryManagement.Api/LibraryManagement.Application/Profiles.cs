using AutoMapper;
using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Data
{
    class Profiles: Profile
    {
        public Profiles()
        {
            CreateMap<Review, CreateReviewRequestModel>().ReverseMap();
        }
    }
}
