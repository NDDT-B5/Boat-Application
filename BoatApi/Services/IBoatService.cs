namespace BoatApi.Services;

using BoatApi.Dtos;

public interface IBoatService
{
	Task<BoatDto> CreateBoatAsync(CreateBoatDto command);
	Task<BoatDto?> GetBoatByIdAsync(Guid id);
	Task<IEnumerable<BoatDto>> GetAllBoatsAsync();
	Task<bool> UpdateBoatAsync(Guid id, UpdateBoatDto command);
	Task<bool> DeleteBoatAsync(Guid id);
}