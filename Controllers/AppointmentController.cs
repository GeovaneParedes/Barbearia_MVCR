using Microsoft.AspNetCore.Mvc;
using Barbearia_MVC.Models;

namespace Barbearia_MVC.Controllers
{
    public class AppointmentController : Controller
    {
        // 1. LISTAR: Exibe a agenda de atendimentos do dia
        // Rota: /Appointment
        [HttpGet]
        public IActionResult Index()
        {
            // Simulação de dados vindos do banco
            // Em um cenário real, você usaria o Entity Framework com .Include() para trazer os dados do Cliente, Barbeiro e Serviço
            var appointments = new List<Appointment>();
            
            return View(appointments);
        }

        // 2. CRIAR AGENDAMENTO NORMAL (Tela): Formulário para marcar um horário futuro
        // Rota: /Appointment/Create
        [HttpGet]
        public IActionResult Create()
        {
            // Dica: Aqui você buscaria as listas de Customers, Barbers e Services no banco 
            // e passaria para a View popular os campos de seleção (Select/Dropdown).
            return View();
        }

        // 3. CRIAR AGENDAMENTO NORMAL (Ação): Salva um agendamento padrão feito com antecedência
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int customerId, int barberId, int serviceId, DateTime dateTime)
        {
            try
            {
                // Invoca o construtor do Model que já valida se a data não está no passado
                var appointment = new Appointment(customerId, barberId, serviceId, dateTime);

                // TODO: Salvar 'appointment' no banco de dados via DbContext
                
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        // 4. ATENDIMENTO DE RUA / BALCÃO (Ação): Executa a sua regra de negócio sem precisar de formulário completo
        // Rota: /Appointment/CreateExpress
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateExpress(int barberId, int serviceId)
        {
            try
            {
                // Usa o método estático que você criou para gerar o agendamento do cliente padrão "Walk-in"
                // Ele já nasce com o ID do cliente como 1, data atual (DateTime.Now) e Status "Completed"
                var expressAppointment = Appointment.CreateWalkIn(barberId, serviceId);

                // TODO: Salvar 'expressAppointment' no banco de dados. 
                // Como ele já nasce concluído, o próximo passo lógico na View seria empurrar esse ID para a tela de pagamento.

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // 5. CANCELAR AGENDAMENTO
        [HttpPost]
        public IActionResult Cancel(int id)
        {
            // TODO: Buscar o agendamento no banco pelo ID
            // var appointment = _context.Appointments.Find(id);
            
            // Simulação:
            Appointment appointment = null!; // Substituir pela busca real

            if (appointment != null) => appointment.Cancel();
                // _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
