using Microsoft.EntityFrameworkCore;
using Barbearia_MVC.Models;

namespace Barbearia_MVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Mapeamento das suas 5 tabelas no Banco de Dados
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Barber> Barbers { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;

        protected override void OnModelCreating(ModelCreatingCollection modelCreatingCollection)
        {
            base.OnModelCreating(modelCreatingCollection);

            // =========================================================================
            // CONFIGURAÇÕES DA ENTIDADE: SERVICE
            // =========================================================================
            modelCreatingCollection.Entity<Service>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Price).HasPrecision(18, 2); // Evita arredondamento de dinheiro
                
                // Informa ao Entity Framework que os campos com 'private set' devem ser mapeados
                entity.Property(s => s.Name).HasField("_name").UsePropertyAccessMode(PropertyAccessMode.Property);
            });

            // =========================================================================
            // CONFIGURAÇÕES DA ENTIDADE: BARBER
            // =========================================================================
            modelCreatingCollection.Entity<Barber>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.CommissionRate).HasPrecision(5, 2); // Ex: 0.40
            });

            // =========================================================================
            // CONFIGURAÇÕES DA ENTIDADE: CUSTOMER
            // =========================================================================
            modelCreatingCollection.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Ssn).HasMaxLength(11).IsRequired(); // Regra do SSN americano
            });

            // =========================================================================
            // CONFIGURAÇÕES DA ENTIDADE: APPOINTMENT (Relacionamentos Triplos)
            // =========================================================================
            modelCreatingCollection.Entity<Appointment>(entity =>
            {
                entity.HasKey(a => a.Id);

                // Relacionamento com Customer
                entity.HasOne(a => a.Customer)
                    .WithMany()
                    .HasForeignKey(a => a.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict); // Impede deletar o cliente se houver agendamentos

                // Relacionamento com Barber
                entity.HasOne(a => a.Barber)
                    .WithMany()
                    .HasForeignKey(a => a.BarberId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Relacionamento com Service
                entity.HasOne(a => a.Service)
                    .WithMany()
                    .HasForeignKey(a => a.ServiceId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // =========================================================================
            // CONFIGURAÇÕES DA ENTIDADE: PAYMENT
            // =========================================================================
            modelCreatingCollection.Entity<Payment>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.AmountPaid).HasPrecision(18, 2);
                entity.Property(p => p.BarberCommissionPaid).HasPrecision(18, 2);

                // Relacionamento de 1 para 1 (Todo pagamento pertence obrigatoriamente a um agendamento)
                entity.HasOne(p => p.Appointment)
                    .WithMany()
                    .HasForeignKey(p => p.AppointmentId)
                    .OnDelete(DeleteBehavior.Cascade); // Se o agendamento sumir por completo, remove o pagamento
            });
        }
    }
}
