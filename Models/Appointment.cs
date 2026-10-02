namespace Barbearia_MVC.Models {
    public class Appointment {
        public int Id { get; private set; }

        // Relacionamento com Cliente
        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; } = null!;

        // NOVO: Relacionamento com o Barbeiro
        public int BarberId { get; private set; }
        public Barber Barber { get; private set; } = null!;

        // Relacionamento com Servico
        public int ServiceId { get; private set; }
        public Service Service { get; private set; } = null!;

        // Data e Hora do agendamento
        public DateTime DateTime { get; private set; }

        // Status do agendamento (ex: Pendente, Confirmado, Cancelado)
        public string Status { get; private set; } = "Scheduled";

        public Appointment(int customerId,int barberId, int serviceId, DateTime dateTime) {
            if (dateTime < DateTime.Now) {
                throw new ArgumentException("Appointment date and time cannot be in the past.");
            }
            CustomerId = customerId;
            BarberId = barberId;
            ServiceId = serviceId;
            DateTime = dateTime;
            Status = "Scheduled";
        }

        // NOVO CONSTRUTOR OU MÉTODO ESTÁTICO: Para o cliente que veio da rua
        public static Appointment CreateWalkIn(int barberId, int serviceId)
        {
            // O ID '1' será fixo para o cliente padrão "Walk-in Customer"
            const int walkInCustomerId = 1; 
            
            var appointment = new Appointment();
            appointment.CustomerId = walkInCustomerId;
            appointment.BarberId = barberId;
            appointment.ServiceId = serviceId;
            appointment.DateTime = DateTime.Now; // Hora atual do atendimento
            appointment.Status = "Completed";    // Já nasce concluído porque foi direto ao caixa
            
            return appointment;
        }

        protected Appointment() { }

        // Metodo para remarcar o agendamento
        public void Reschedule(DateTime newDateTime) {
            if (newDateTime < DateTime.Now) {
                throw new ArgumentException("New appointment date and time cannot be in the past.");
            }
            DateTime = newDateTime;
        }

        // Metodo para cancelar o agendamento
        public void Cancel() => Status = "Cancelled";

        // Metodo para concluir o agendamento
        public void Complete() => Status = "Completed";
    }
}

