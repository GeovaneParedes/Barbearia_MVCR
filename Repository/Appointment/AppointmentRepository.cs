using Microsoft.EntityFrameworkCore;
using Barbearia_MVC.Models;
using Barbearia_MVC.Data;

namespace Barbearia_MVC.Repositories.Appointment
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context; // Seu contexto do Entity Framework

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Busca um agendamento trazendo junto os dados do Cliente, Barbeiro e Serviço
        public async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(a => a.Customer)
                .Include(a => a.Barber)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // Lista todos os agendamentos do sistema com seus respectivos relacionamentos
        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                .Include(a => a.Customer)
                .Include(a => a.Barber)
                .Include(a => a.Service)
                .ToListAsync();
        }

        // Filtra a agenda por barbeiro e por uma data específica (ignorando a hora na filtragem)
        public async Task<IEnumerable<Appointment>> GetByBarberAndDateAsync(int barberId, DateTime date)
        {
            return await _context.Appointments
                .Include(a => a.Customer)
                .Include(a => a.Service)
                .Where(a => a.BarberId == barberId && a.DateTime.Date == date.Date)
                .ToListAsync();
        }

        // Adiciona o agendamento (serve tanto para o normal quanto para o Walk-In da rua)
        public async Task AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync(); // Grava fisicamente no banco de dados
        }

        // Atualiza o estado do agendamento (quando você chama .Cancel() ou .Complete() no Model)
        public void Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            _context.SaveChanges();
        }
    }
}
