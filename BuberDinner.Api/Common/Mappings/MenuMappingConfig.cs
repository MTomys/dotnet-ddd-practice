using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Contracts.Menus;
using BuberDinner.Domain.Menu;
using Mapster;
using MenuItem = BuberDinner.Domain.Menu.MenuItem;
using MenuSection = BuberDinner.Domain.Menu.MenuSection;

namespace BuberDinner.Api.Common.Mappings;

public class MenuMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateMenuRequest request, string hostId), CreateMenuCommand>()
            .Map(dest => dest.HostId, src => src.hostId)
            .Map(dest => dest, src => src);

        config.NewConfig<Menu, MenuResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.AverageRating, src => src.AverageRating)
            .Map(dest => dest.HostId, src => src.HostId.Value)
            .Map(dest => dest.DinnerIds, src => src.DinnerIds.Select(dinnerId => dinnerId.Value))
            .Map(dest => dest.MenuReviewIds, src => src.MenuReviewIds.Select(menuId => menuId.Value));
        
        config.NewConfig<MenuSection, MenuSectionResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
        
        config.NewConfig<MenuItem, MenuItemResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}