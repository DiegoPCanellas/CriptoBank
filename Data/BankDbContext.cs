using CriptoBank.Domain.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) 
        {
        }

        //Método executado ao gerar o BD
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankDbContext).Assembly);

            modelBuilder.Entity<Cartao>(x =>
            {
                x.HasKey(k => k.CartaoID);
            });
            modelBuilder.Entity<ContaCorrente>(x =>
            {
                x.HasKey(k => k.ContaCorrenteID);
            });
            modelBuilder.Entity<Deposito>(x =>
            {
                x.HasKey(k => k.DepositoID);
            });
            modelBuilder.Entity<Emprestimo>(x =>
            {
                x.HasKey(k => k.EmprestimoID);
            });
            modelBuilder.Entity<Parcela>(x =>
            {
                x.HasKey(k => k.ParcelaID);
            });
            modelBuilder.Entity<Pessoa>(x =>
            {
                x.HasKey(k => k.PessoaID);
            });
            modelBuilder.Entity<TransacaoCripto>(x =>
            {
                x.HasKey(k => k.TransacaoCriptoID);
            });
            modelBuilder.Entity<Transferencia>(x =>
            {
                x.HasKey(k => k.TransferenciaID);
            });
        }

        //Contexto do EF de todos as minhas models
        public DbSet<Cartao> Cartao { get; set; }
        public DbSet<ContaCorrente> ContaCorrente { get; set; }
        public DbSet<Deposito> Deposito { get; set; }
        public DbSet<Emprestimo> Emprestimo { get; set; }
        public DbSet<Parcela> Parcela { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        //public DbSet<Transacao> Transacao { get; set; }
        //public DbSet<TransacaoCripto> TransacaoCripto { get; set; }
        public DbSet<Transferencia> Transferencia { get; set; }
    }
}
