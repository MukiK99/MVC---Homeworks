using BurgerApp.Dto.Dtos;
using BurgerApp.Dto.ViewModels;

namespace BurgerApp.Services.Interfaces
{
    public interface ILocationService
    {
        List<LocationDto> GetAllLocations();
        void EditLocation(EditLocationVM editLocation, int locationId);
        LocationDto GetLocation(int locationId);
        void RemoveLocation(int id);
        void AddLocation(CreateLocationVM createLocation);

    }
}
