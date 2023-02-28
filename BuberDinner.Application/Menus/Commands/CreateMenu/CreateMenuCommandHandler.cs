using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu;
using MediatR;
using ErrorOr;

namespace BuberDinner.Application.Menus.Commands.CreateMenu;

public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, ErrorOr<Menu>>
{
    private readonly IMenuRepository _menuRepository;

    public CreateMenuCommandHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<ErrorOr<Menu>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        var menu = Menu.Create(
            HostId.Create(request.HostId),
            request.Name,
            request.Description,
            request.Sections.ConvertAll(section => MenuSection.Create(
                section.Name,
                section.Description,
                section.MenuItems.ConvertAll(item => MenuItem.Create(item.Name, item.Description)))));
        
        _menuRepository.Add(menu);

        return menu;
    }
}