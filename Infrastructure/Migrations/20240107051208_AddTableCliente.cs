using FluentMigrator;

namespace Infrastructure.Migrations
{
    [Migration(20240107051208)]
    public class AddTableCliente : Migration
    {
        public override void Up()
        {
            Create
                .Table("cliente").WithDescription("tabela contendo dados de clientes")
                .WithColumn("id").AsString().PrimaryKey().WithColumnDescription("identificador unico")
                .WithColumn("cpf").AsString().NotNullable().WithColumnDescription("cpf do cliente")
                .WithColumn("usuario").AsString().NotNullable().WithColumnDescription("nickname do cliente")
                .WithColumn("senha").AsString().Nullable().WithColumnDescription("senha do cliente")
                .WithColumn("nome").AsString().NotNullable().WithColumnDescription("nome do cliente")
                .WithColumn("celular").AsString().NotNullable().WithColumnDescription("numero de celular do cliente")
                .WithColumn("email").AsString().NotNullable().WithColumnDescription("email do cliente")
                .WithColumn("endereco").AsString().NotNullable().WithColumnDescription("endereço do cliente")
                .WithColumn("sexo").AsString().Nullable().WithColumnDescription("sexo");
        }

        public override void Down()
        {
            Delete
                .Table("cliente");
        }
    }
}