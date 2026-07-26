using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Application.Dto;

namespace ShoppingApp.Application.AppHandlers.Auth.Roles.GetRoles;

public record GetRolesQuery() : IQuery<IEnumerable<AuthRoleDto>>
{
}
