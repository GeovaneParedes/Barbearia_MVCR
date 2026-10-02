using Microsoft.AspNetCore.Mvc;
using Barbearia_MVC.Models;

namespace Barbearia_MVC.Controllers
{
    public class ServiceController : Controller 
    {
        [HttpGet]
        public IActionResult Index() 
        {
            // Note que seu construtor original exige 3 parâmetros: (name, description, price)
            var services = new List<Service> 
            {
                new Service("Haircut", "Standard haircut", 30.00m),
                new Service("Beard", "Trimming and shaving", 20.00m),
                new Service("Combo", "Haircut and Beard package", 45.00m)
            };
            return View(services);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Coleta os valores do formulário HTML individualmente
        public IActionResult Create(string name, string description, decimal price) 
        {
            try 
            {
                // Instancia o modelo usando o construtor blindado com as regras de negócio
                var newService = new Service(name, description, price);

                // TODO: Adicionar o newService ao banco de dados aqui
                
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex) 
            {
                // Se o preço for negativo, a exceção disparada no Model é capturada aqui e exibida na View
                ModelState.AddModelError(string.Empty, ex.Message) => View();
            }
        }
    }
}
