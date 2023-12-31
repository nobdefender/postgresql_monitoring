﻿using System.Text.Json;
using AutoMapper;
using Monitoring.Posgresql.Infrastructure.Models.Access;
using Monitoring.Posgresql.Infrastructure.Models.TelegramBot;
using Monitoring.Posgresql.Infrastructure.Models.WebAuth;
using Monitoring.Postgresql.Models;
using Monitoring.Postgresql.Models.Action;
using Monitoring.Postgresql.Models.Auth;

namespace Monitoring.Postgresql.Mappings;

public class DbModelsToDTOModelsMapping : Profile
{
    public DbModelsToDTOModelsMapping()
    {
        CreateMap<WebUserDbModel, UpsertWebUserDTO>()
            .ReverseMap()
            .ForMember(dto => dto.EmailAddress, db => db.MapFrom(model => model.EmailAddress.ToLower()));
        CreateMap<TelegramBotUserDbModel, WebUserDTO>().ReverseMap();
        CreateMap<ActionDbModel, ActionDTO>();
        CreateMap<UserActionRequestModel, UserActionDbModel>();
        CreateMap<WebUserDbModel, WebUserDTO>();
    }

    private static TimeSpan? Deserialize(string? timeSpan) =>
        timeSpan is null
            ? null
            : JsonSerializer.Deserialize<TimeSpan?>(timeSpan);

    private static string? Serialize(TimeSpan? timeSpan) =>
        timeSpan?.ToString();
}