namespace Barbearia_MVC.Models
{
    public class Customer 
    {
        public int Id { get; private set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        
        // SSN (Social Security Number) é o equivalente ao CPF nos EUA
        public string Ssn { get; set; } = string.Empty; 

        public Customer(string name, string email, string phone, string address, string ssn) 
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            Ssn = ssn;
        }

        protected Customer() { }

        public void UpdateCustomer(string name, string email, string phone, string address, string ssn) 
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            Ssn = ssn;
        }
    }
}
