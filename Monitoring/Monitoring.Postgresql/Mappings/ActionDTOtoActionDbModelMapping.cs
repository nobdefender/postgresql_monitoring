using AutoMapper;
using Monitoring.Posgresql.Infrastructure.Models.Access;
using Monitoring.Postgresql.Models.Action;

namespace Monitoring.Postgresql.Mappings;

public class ActionDTOtoActionDbModelMapping : Profile
{
    public ActionDTOtoActionDbModelMapping()
    {
        CreateMap<ActionDTO, ActionDbModel>();
    }
}