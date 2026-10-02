using Microsoft.AspNetCore.Mvc;
using Barbearia_MVC.Models;

namespace Barbearia_MVC.Controllers
{
    public class PaymentController : Controller
    {
        // 1. LISTAR: Exibe o histórico de faturamento e comissões da barbearia
        // Rota: /Payment
        [HttpGet]
        public IActionResult Index()
        {
            // Simulação de histórico de pagamentos realizados
            var payments = new List<Payment>();
            
            return View(payments);
        }

        // 2. REGISTRAR PAGAMENTO (Tela): Abre o caixa para receber de um atendimento concluído
        // Rota: /Payment/Checkout/5
        [HttpGet]
        public IActionResult Checkout(int appointmentId)
        {
            // TODO: Buscar o agendamento no banco incluindo as relações (.Include) de Barber e Service
            // var appointment = _context.Appointments
            //     .Include(a => a.Barber)
            //     .Include(a => a.Service)
            //     .FirstOrDefault(a => a.Id == appointmentId);

            Appointment appointment = null!; // Substituir pela busca real

            if (appointment == null) return NotFound();

            // Passamos o agendamento para a View para que o caixa veja o nome do cliente, 
            // o serviço feito e o valor padrão a ser cobrado.
            return View(appointment);
        }

        // 3. REGISTRAR PAGAMENTO (Ação): Processa a entrada do dinheiro e gera a comissão
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessPayment(int appointmentId, decimal amountPaid, string paymentMethod)
        {
            try
            {
                // 1. Busca o agendamento com os dados do barbeiro carregados (necessário para a comissão)
                Appointment appointment = null!; // Substituir pela busca real (Ex: via DbContext)

                if (appointment == null) return NotFound();

                // 2. Instancia o modelo Payment. O construtor roda a regra de negócio:
                // amountPaid * appointment.Barber.CommissionRate
                var newPayment = new Payment(appointment, amountPaid, paymentMethod);

                // 3. Atualiza o status do agendamento para concluído, caso ainda não esteja
                appointment.Complete();

                // TODO: Salvar o newPayment e atualizar o appointment no banco de dados
                // _context.Payments.Add(newPayment);
                // _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Checkout");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Checkout");
            }
        }

        // 4. ESTORNAR PAGAMENTO (Ação): Caso ocorra um erro de digitação ou cancelamento de última hora
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Refund(int id)
        {
            try
            {
                // TODO: Buscar o pagamento no banco de dados
                Payment payment = null!; // Substituir pela busca real

                if (payment == null) return NotFound();

                // Dispara a regra de negócio de estorno isolada no Model
                payment.Refund();

                // TODO: _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
