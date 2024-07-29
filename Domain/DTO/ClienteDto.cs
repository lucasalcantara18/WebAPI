using Domain.Enumerations;

namespace Domain.Dto
{
    public record ClienteDto(string cpf, string usuario, string senha, string nome, string celular, string email, string endereco, string sexo);
}
