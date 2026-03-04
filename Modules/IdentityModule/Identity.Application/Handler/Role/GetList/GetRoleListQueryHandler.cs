using Common.Application.MediatR.Messaging;
using Common.Domain;
using Identity.Application.Role.Queries;
using Identity.Domain.Repository;

namespace Identity.Application.Role.Handlers;

public class GetRoleListQueryHandler(IRoleRepository _roleRepository)
: IQueryHandler<GetRoleListQuery, List<RoleDto>>
{
    public async Task<Result<List<RoleDto>>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
    {
        var queryable = await _roleRepository.GetQueryableAsync();
        var roles = queryable.ToList();

        var roleDtos = roles.Select(r => new RoleDto(
            Id: r.Id,
            RoleName: r.RoleName,
            Description: r.Description
        )).ToList();

        return roleDtos;
    }
}
