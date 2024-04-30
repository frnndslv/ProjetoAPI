
using Microsoft.EntityFrameworkCore;

public static class FuncionarioApi
{

    public static void MapFuncionarioApi(this WebApplication app)
    {

        var group = app.MapGroup("/funcionario");


        group.MapGet("/", async (BancoDeDados db) =>
            await db.Funcionarios.ToListAsync()
        );

        group.MapPost("/", async (Funcionario funcionario, BancoDeDados db) =>
        {
            db.Funcionarios.Add(funcionario);
            await db.SaveChangesAsync();

            return Results.Created($"/funcionario/{funcionario.Id}", funcionario);
        }
        );

        group.MapPut("/{id}", async (int id, Funcionario funcionarioAlterado, BancoDeDados db) =>
        {
            var funcionario = await db.Funcionarios.FindAsync(id);
            if (funcionario is null)
            {
                return Results.NotFound();
            }

            funcionario.Nome = funcionarioAlterado.Nome ?? funcionario.Nome;
            funcionario.Cargo = funcionarioAlterado.Cargo ?? funcionario.Cargo;

            await db.SaveChangesAsync();

            return Results.NoContent();
        }
        );

        group.MapDelete("/{id}", async (int id, BancoDeDados db) =>
        {
            if (await db.Funcionarios.FindAsync(id) is Funcionario funcionario)
            {
                db.Funcionarios.Remove(funcionario);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        }
        );
    }
}