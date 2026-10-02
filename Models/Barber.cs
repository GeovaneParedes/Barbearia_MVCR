namespace Barbearia_MVC.Models {
    public class Barber {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        
        // Percentual de comissao (ex 0.40 para 40%)
        public decimal CommissionRate { get; private set; }
        public bool IsActive { get; private set; } = true;

        public Barber(string name, string phone, decimal commissionRate) {
            if (commissionRate < 0 || commissionRate > 1) {
                throw new ArgumentException("Commission rate must be between 0 and 1.");
            }
            Name = name;
            Phone = phone;
            CommissionRate = commissionRate;
            IsActive = true;
        }

        protected Barber() { }

        public void UpdateCommission(decimal newRate) {
            if (newRate < 0 || newRate > 1) {
                throw new ArgumentException("Commission rate must be between 0 and 1.");
            }
            CommissionRate = newRate;
        }

        public void Deactivate() => IsActive = false
        public void Activate() => IsActive = true;
    }
}