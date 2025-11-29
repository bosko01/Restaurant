using Domain.Interfaces.IReservation;
using Domain.Interfaces.IRestaurant;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ReservationRepository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RestaurantDbContext _context;

        public ReservationRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<Reservation> CreateNewAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);

            return reservation;
        }

        public async Task DeleteAsync(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            var reservations = await _context.Reservations.ToListAsync();

            return reservations;
        }

        public async Task<Reservation?> GetByIdAsync(Guid reservationId)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync<Reservation>();

            return reservation;
        }

        public async Task<Reservation> UpdateAsync(Reservation reservation)
        {
             _context.Update(reservation);

            return reservation;
        }
    }
}
