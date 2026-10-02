using Microsoft.AspNetCore.Mvc;
using Barbearia_MVC.Models;

namespace Barbearia_MVC.Controllers
{
    public class BarberController : Controller
    {
        // 1. LISTAR: Exibe todos os barbeiros cadastrados
        // Rota: /Barber
        [HttpGet]
        public IActionResult Index()
        {
            // Simulação de dados vindos do banco
            var barbers = new List<Barber>
            {
                new Barber("John Doe", "(67) 99999-1122", 0.40m), // 40% comissão
                new Barber("Alex Smith", "(67) 98888-3344", 0.50m) // 50% comissão
            };

            return View(barbers);
        }

        // 2. CADASTRAR (Tela): Formulário em branco
        // Rota: /Barber/Create
        [HttpGet]
        public IActionResult Create() => View();

        // 3. CADASTRAR (Ação): Salva o novo barbeiro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string name, string phone, decimal commissionRate)
        {
            try
            {
                // Instancia o modelo usando o construtor blindado
                var newBarber = new Barber(name, phone, commissionRate);

                // TODO: Salvar no banco de dados (Ex: _context.Barbers.Add(newBarber);)

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                // Captura se a comissão for menor que 0 ou maior que 1
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        // 4. ATUALIZAR COMISSÃO (Ação): Altera o percentual de ganho de forma segura
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCommission(int id, decimal newRate)
        {
            try
            {
                // TODO: Buscar o barbeiro real no banco de dados pelo ID
                Barber barber = null!; 

                if (barber == null) return NotFound();

                // Usa o método de negócio do seu Model
                barber.UpdateCommission(newRate);
                
                // TODO: _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // 5. DESATIVAR BARBEIRO: Em vez de deletar e perder o histórico financeiro, nós o desativamos
        [HttpPost]
        public IActionResult Deactivate(int id)
        {
            // TODO: Buscar do banco pelo ID
            Barber barber = null!;

            if (barber == null) return NotFound();

            barber.Deactivate(); // Usa a regra do Model
            
            // TODO: _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
