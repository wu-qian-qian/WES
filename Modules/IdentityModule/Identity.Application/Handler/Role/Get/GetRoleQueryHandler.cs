using Common.Application.MediatR.Messaging;
using Common.Domain;
using Identity.Application.Role.Queries;
using Identity.Domain.Repository;

namespace Identity.Application.Role.Handlers;

public class GetRoleQueryHandler(IRoleRepository _roleRepository)
: IQueryHandler<GetRoleQuery, RoleDto>
{
    public async Task<Result<RoleDto>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetAsync(request.Id);
        if (role is null)
        {
            return Result.Error<RoleDto>($"Role with ID {request.Id} not found");
        }

        return new RoleDto(
            Id: role.Id,
            RoleName: role.RoleName,
            Description: role.Description
        );
    }
}
