using BurgerApp.DataAccess.Interfaces;
using BurgerApp.Domain;
using BurgerApp.Dto.Dtos;
using BurgerApp.Dto.ViewModels;
using BurgerApp.Mapper;
using BurgerApp.Services.Interfaces;

namespace BurgerApp.Services
{
    public class LocationService : ILocationService
    {
        private readonly IRepository<Location> _locationRepository;
        public LocationService(IRepository<Location> locationRepository)
        {
            _locationRepository = locationRepository;
        }
        public LocationDto GetLocation(int locationId)
        {
            return _locationRepository.GetById(locationId).Map();
        }
        public List<LocationDto> GetAllLocations()
        {
            return _locationRepository.GetAll().Select(x => x.Map()).ToList();
        }
        public void AddLocation(CreateLocationVM createLocation)
        {
            var newLocation = new Location
            {
                Name = createLocation.Name,
                Address = createLocation.Address,
                OpensAt = createLocation.OpensAt,
                ClosesAt = createLocation.ClosesAt
            };
            _locationRepository.Add(newLocation);
        }

        public void EditLocation(EditLocationVM editLocation, int locationId)
        {
            var location = _locationRepository.GetById(locationId);
            if (location != null)
            {
                location.Name = editLocation.Name;
                location.Address = editLocation.Address;
                location.OpensAt = editLocation.OpensAt;
                location.ClosesAt = editLocation.ClosesAt;

                _locationRepository.Update(location);
            }
            else
            {
                throw new Exception("Location not found");
            }
        }

        public void RemoveLocation(int id)
        {
            _locationRepository.Delete(id);
        }
    }
}
