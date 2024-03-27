using Infrastructures.Contexts;
using Infrastructures.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Services;

public class AddressManager(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    #region GetAddresses
    public async Task<AddressEntity> GetAddressAsync(string UserId)
    {
        var addressEntity = await _context.Address.FirstOrDefaultAsync(x => x.UserId == UserId);
        return addressEntity!;
    }
    #endregion

    #region CreateAddresses
    public async Task<bool> CreateAddressAsync(AddressEntity entity)
    {
        _context.Address.Add(entity);
        await _context.SaveChangesAsync();
        return true;
    }
    #endregion

    #region UpdateAddresses
    public async Task<bool> UpdateAddressAsync(AddressEntity entity)
    {
        var existing = await _context.Address.FirstOrDefaultAsync(x => x.UserId == entity.UserId);
        if (existing != null)
        {
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        return false;
    }
    #endregion
}
