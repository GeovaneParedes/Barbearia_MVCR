using Microsoft.AspNetCore.Mvc;
using Barbearia_MVC.Models;

namespace Barbearia_MVC.Controllers
{
    public class CustomerController : Controller
    {
        // 1. LISTAR: Mostra todos os clientes na tela
        // Rota: /Customer
        [HttpGet]
        public IActionResult Index()
        {
            // Simulação de dados (Excluindo o ID 1 que é o nosso "Walk-in Customer" padrão)
            var customers = new List<Customer>
            {
                new Customer("Michael Jordan", "jordan@email.com", "(555) 0199-2344", "Chicago, IL", "000-12-3456"),
                new Customer("LeBron James", "lebron@email.com", "(555) 0188-4433", "Los Angeles, CA", "000-98-7654")
            };

            return View(customers);
        }

        // 2. CRIAR (Tela): Exibe o formulário em branco
        // Rota: /Customer/Create
        [HttpGet]
        public IActionResult Create() => View();

        // 3. CRIAR (Ação): Recebe o formulário e salva o novo cliente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string name, string email, string phone, string address, string ssn)
        {
            // Instancia o Model usando o construtor que você criou
            var newCustomer = new Customer(name, email, phone, address, ssn);

            // TODO: Salvar no banco de dados aqui (Ex: _context.Customers.Add(newCustomer);)

            return RedirectToAction(nameof(Index));
        }

        // 4. EDITAR (Ação): Atualiza os dados de um cliente existente de forma segura
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, string name, string email, string phone, string address, string ssn)
        {
            // TODO: Buscar o cliente real no banco de dados pelo ID
            Customer customer = null!; // Substituir pela busca real via DbContext

            if (customer == null) return NotFound();

            // Evita que alterem o cadastro fixo do cliente de balcão (ID 1)
            if (id == 1)
            {
                return BadRequest("The default walk-in customer profile cannot be edited.");
            }

            // Usa o método de alteração que você escreveu no seu Model
            customer.UpdateCustomer(name, email, phone, address, ssn);

            // TODO: _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
