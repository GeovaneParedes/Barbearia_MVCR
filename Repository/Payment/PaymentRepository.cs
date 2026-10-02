using Microsoft.EntityFrameworkCore;
using Barbearia_MVC.Models;
using Barbearia_MVC.Data;

namespace Barbearia_MVC.Repositories.Payment
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Busca o pagamento carregando o agendamento, o barbeiro envolvido e o serviço prestado
        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _context.Payments
                .Include(p => p.Appointment)
                    .ThenInclude(a => a.Barber)
                .Include(p => p.Appointment)
                    .ThenInclude(a => a.Service)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Lista todo o histórico financeiro da barbearia com os dados dos agendamentos
        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.Payments
                .Include(p => p.Appointment)
                    .ThenInclude(a => a.Barber)
                .Include(p => p.Appointment)
                    .ThenInclude(a => a.Service)
                .ToListAsync();
        }

        // Método poderoso: Filtra os pagamentos de um barbeiro específico entre duas datas
        // Perfeito para fechar a folha de pagamento e ver quanto ele gerou de comissão (BarberCommissionPaid)
        public async Task<IEnumerable<Payment>> GetByBarberAndDateRangeAsync(int barberId, DateTime startDate, DateTime endDate)
        {
            return await _context.Payments
                .Include(p => p.Appointment)
                .Where(p => p.Appointment.BarberId == barberId 
                         && p.PaymentDate.Date >= startDate.Date 
                         && p.PaymentDate.Date <= endDate.Date
                         && p.Status == "Paid") // Apenas pagamentos confirmados (ignora estornos)
                .ToListAsync();
        }

        public async Task AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public void Update(Payment payment)
        {
            _context.Payments.Update(payment);
            _context.SaveChanges();
        }
    }
}
