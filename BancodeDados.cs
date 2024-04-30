

using Microsoft.EntityFrameworkCore;

public class BancoDeDados : DbContext
{


    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseMySQL("server=localhost;port=3306;database=ProjetoAPI;user=root;password=152436");
    }

}