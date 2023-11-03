using Monitoring.Postgresql.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Monitoring.Postgresql.Mappings;

public class UserActionRequestModelToUserActionDbModelMapping : Profile
{
    public UserActionRequestModelToUserActionDbModelMapping()
    {
        CreateMap<UserActionRequestModel, UserActionDbModel>()
            .ForMember(x => x.Id, m => m.Ignore())
            .ForMember(m => m.ProductId, m => m.MapFrom(x => x.Id))
            .ForMember(m => m.ChangeDate, m => m.MapFrom(x => DateTime.UtcNow));
    }
}
