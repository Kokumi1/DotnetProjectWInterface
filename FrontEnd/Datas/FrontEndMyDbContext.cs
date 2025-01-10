
using FrontEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace FrontEnd.Datas
{
    public class FrontEndMyDbContext : DbContext
    {
        public FrontEndMyDbContext()
        {
            
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBFrontEnd;");
        }

        public DbSet<Operation> Operations { get; set; }
        public DbSet<ClientParticulier> ClientsParticuliers { get; set; }
        public DbSet<ClientProfessionnel> ClientsProfessionnels { get; set; }
        
        public DbSet<CompteBancaire> ComptesBancaires { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Adresse> Adresses { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurer l'héritage TPH (Table-Per-Hierarchy) pour Client
            modelBuilder.Entity<Client>()
                .HasDiscriminator<string>("TypeClient") // Colonne pour distinguer les types de clients
                .HasValue<ClientParticulier>("Particulier")
                .HasValue<ClientProfessionnel>("Professionnel");

            modelBuilder.Entity<ClientProfessionnel>()
                .HasOne(cp => cp.AdresseSiege)
                .WithMany()
                .HasForeignKey(c => c.AdresseSiegeId)
                .OnDelete(DeleteBehavior.Restrict); // Désactive la cascade

            modelBuilder.Entity<Client>()
                .HasOne(c => c.Adresse)
                .WithMany()
                .HasForeignKey(c => c.AdresseId)
                .OnDelete(DeleteBehavior.Restrict); // Désactive la cascade

            // Configurer la relation One-to-Many entre Client et CompteBancaire
            modelBuilder.Entity<CompteBancaire>()
                .HasOne<Client>(cb => cb.Client)  // Une relation un-à-plusieurs avec Client
                .WithMany(c => c.Comptes)         // Un client peut avoir plusieurs comptes
                .HasForeignKey(cb => cb.ClientId) // Clé étrangère ClientId dans CompteBancaire
                .OnDelete(DeleteBehavior.Restrict); // Pas de suppression en cascade

            // Configurer la relation One-to-Many entre CompteBancaire et CarteBancaire
            modelBuilder.Entity<CarteBancaire>()
                .HasOne(cb => cb.CompteBancaire) // Une carte appartient à un compte bancaire
                .WithMany(c => c.Cartes)         // Un compte bancaire peut avoir plusieurs cartes
                .HasForeignKey(cb => cb.CompteBancaireId) // Clé étrangère CompteBancaireId dans CarteBancaire
                .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade: si un compte est supprimé, ses cartes le seront aussi


            modelBuilder.Entity<Adresse>().HasData(
                new Adresse
                {
                    Id = 1,
                    Libelle = "10 avenue des Champs",
                    Complement = "Bâtiment B",
                    CodePostal = "75008",
                    Ville = "Paris"
                }
            );
            modelBuilder.Entity<ClientParticulier>().HasData(
                new ClientParticulier
                {
                    Id = 1,
                    Nom = "Dupont",
                    Prenom = "Jean",
                    Email = "jean.dupont@example.com",
                    DateNaissance = new DateTime(1985, 5, 12),
                    Sexe = "Homme",
                    AdresseId = 1 // Liaison avec l'adresse via sa clé primaire
                }
            );

            modelBuilder.Entity<CompteBancaire>().HasData(
                new CompteBancaire
                {
                    Id = 1,
                    NumeroCompte = "FR7612345678901234567890123",
                    DateOuverture = new DateTime(2020, 1, 1),
                    Solde = 1000.00m,
                    ClientId = 1 // Lien avec le client ayant l'Id 1
                }
            );
            modelBuilder.Entity<CarteBancaire>().HasData(
                new CarteBancaire
                {
                    Id = 1,
                    NumeroCarte = "1234-5678-9876-5432",
                    CompteBancaireId = 1 // Lien avec un compte bancaire existant
                },
                new CarteBancaire
                {
                    Id = 2,
                    NumeroCarte = "2345-6789-8765-4321",
                    CompteBancaireId = 1 // Lien avec un autre compte bancaire existant
                }
            );
        }
    }
}
