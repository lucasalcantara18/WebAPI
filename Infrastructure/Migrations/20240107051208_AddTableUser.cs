using FluentMigrator;

namespace Infrastructure.Migrations
{
    [Migration(20240107051208)]
    public class AddTableUser : Migration
    {
        public override void Up()
        {
            Create
                .Table("usuario").WithDescription("tabela contendo dados de usuarios")
                .WithColumn("id").AsString().PrimaryKey().WithColumnDescription("identificador unico")
                .WithColumn("cpf").AsString().NotNullable().WithColumnDescription("cpf do usuario")
                .WithColumn("usuario").AsString().NotNullable().WithColumnDescription("nickname do usuario")
                .WithColumn("senha").AsString().Nullable().WithColumnDescription("senha do usuario")
                .WithColumn("nome").AsString().NotNullable().WithColumnDescription("nome do usuario")
                .WithColumn("celular").AsString().NotNullable().WithColumnDescription("numero de celular do usuario")
                .WithColumn("email").AsString().NotNullable().WithColumnDescription("email do usuario")
                .WithColumn("endereco").AsString().NotNullable().WithColumnDescription("endereço do usuario")
                .WithColumn("sexo").AsString().Nullable().WithColumnDescription("sexo");
        }

        public override void Down()
        {
            Delete
                .Table("usuario");
        }
    }
}