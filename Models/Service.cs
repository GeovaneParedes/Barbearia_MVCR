namespace Barbearia_MVC.Models

public class Service {
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }

    public Service(string name, string description, decimal preco) {
        Name = name;
        Description = description;
        Price = price;
    }

    protected Service() { }

    public void UpdatePrice(decimal newPrice) {
        if (newPrice < 0) {
            throw new ArgumentException("Price cannot be negative.");
        }
        Price = newPrice;
    }
}