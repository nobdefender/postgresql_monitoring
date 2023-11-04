using AutoMapper;
using Monitoring.Postgresql.Models;

namespace Monitoring.Postgresql.Mappings;

public class UserActionRequestModelToUserActionDbModelMapping : Profile
{
    public UserActionRequestModelToUserActionDbModelMapping()
    {
        CreateMap<UserActionRequestModel, UserActionDbModel>();
    }
}
