using BuberDinner.Domain.Menu;
using MediatR;
using ErrorOr;

namespace BuberDinner.Application.Menus.Commands.CreateMenu;

public record CreateMenuCommand(
    string HostId,
    string Name,
    string Description,
    List<MenuSectionCommand> Sections) : IRequest<ErrorOr<Menu>>;

public record MenuSectionCommand(
    string Name,
    string Description,
    List<MenuItemCommand> MenuItems);

public record MenuItemCommand(
    string Name,
    string Description);
