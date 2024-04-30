using Microsoft.EntityFrameworkCore;

public static class FornecedorApi
{

    public static void MapFornecedorApi(this WebApplication app)
    {

        var group = app.MapGroup("/fornecedor");


        group.MapGet("/", async (BancoDeDados db) =>
            await db.Fornecedores.ToListAsync()
        );

        group.MapPost("/", async (Fornecedor fornecedor, BancoDeDados db) =>
        {
            db.Fornecedores.Add(fornecedor);
            await db.SaveChangesAsync();

            return Results.Created($"/fornecedor/{fornecedor.Id}", fornecedor);
        }
        );

        group.MapPut("/{id}", async (int id, Fornecedor fornecedorAlterado, BancoDeDados db) =>
        {
            var fornecedor = await db.Fornecedores.FindAsync(id);
            if (fornecedor is null)
            {
                return Results.NotFound();
            }

            fornecedor.Nome = fornecedorAlterado.Nome ?? fornecedor.Nome;
            fornecedor.ProdutoFornecido = fornecedorAlterado.ProdutoFornecido ?? fornecedor.ProdutoFornecido;

            await db.SaveChangesAsync();

            return Results.NoContent();
        }
        );

        group.MapDelete("/{id}", async (int id, BancoDeDados db) =>
        {
            if (await db.Fornecedores.FindAsync(id) is Fornecedor fornecedor)
            {
                db.Fornecedores.Remove(fornecedor);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        }
        );
    }
}