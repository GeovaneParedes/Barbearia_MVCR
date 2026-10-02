namespace Barbearia_MVC.Models {
    public class Payment {
        public int Id { get; private set; }
        
        // Mantém a regra: Todo pagamento obrigatoriamente pertence a um Agendamento/Atendimento
        public int AppointmentId { get; private set; }
        public Appointment Appointment { get; private set; } = null!;

        public decimal AmountPaid { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public string PaymentMethod { get; private set; } = string.Empty;
        public string Status { get; private set; } = "Paid";

        public Payment(int appointmentId, decimal amountPaid, string paymentMethod)
        {
            if (amountPaid <= 0) 
                throw new ArgumentException("The payment amount must be greater than zero.");

            AppointmentId = appointmentId;
            AmountPaid = amountPaid;
            PaymentMethod = paymentMethod;
            PaymentDate = DateTime.Now;
        }

        protected Payment() { }
    }
}
