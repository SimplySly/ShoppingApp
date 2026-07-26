using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Application.Dto;
using ShoppingApp.Core.Repository;
using ShoppingApp.Core.Utility;

namespace ShoppingApp.Application.AppHandlers.Auth.Roles.GetRoles;

public sealed class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, IEnumerable<AuthRoleDto>>
{
    private readonly IAuthRepository _authRepository;

    public GetRolesQueryHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<Result<IEnumerable<AuthRoleDto>>> Handle(GetRolesQuery query, CancellationToken cancellationToken)
    {
        var dbResult = await _authRepository.GetAllRoles(cancellationToken);
        var result = dbResult.Select(role => new AuthRoleDto(
            role.Id,
            role.Name!));

        return Result.Success(result);
    }
}
