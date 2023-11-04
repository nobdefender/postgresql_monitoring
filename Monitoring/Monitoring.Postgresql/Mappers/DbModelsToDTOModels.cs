using System.Text.Json;
using AutoMapper;
using Monitoring.Posgresql.Infrastructure.Models.Access;
using Monitoring.Posgresql.Infrastructure.Models.Auth;
using Monitoring.Postgresql.Models;
using Monitoring.Postgresql.Models.Action;
using Monitoring.Postgresql.Models.Auth;

namespace Monitoring.Postgresql.Mappers;

public class DbModelsToDTOModels : Profile
{
    public DbModelsToDTOModels()
    {
        CreateMap<UserDbModel, UpsertUserDTO>()
            .ReverseMap()
            .ForMember(dto => dto.EmailAddress, db => db.MapFrom(model => model.EmailAddress.ToLower()));
        CreateMap<UserDbModel, UserDTO>().ReverseMap();
        CreateMap<ActionDbModel, ActionDTO>();
        CreateMap<UserActionRequestModel, UserActionDbModel>();
    }

    private static TimeSpan? Deserialize(string? timeSpan) =>
        timeSpan is null
            ? null
            : JsonSerializer.Deserialize<TimeSpan?>(timeSpan);

    private static string? Serialize(TimeSpan? timeSpan) =>
        timeSpan?.ToString();
}