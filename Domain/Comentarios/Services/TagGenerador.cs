using System.Text;
using Domain.Core.Services;
using Domain.Hilos;
using Domain.Hilos.Models.ValueObjects;
using Domain.Usuarios;
using Domain.Usuarios.Models.ValueObjects;

namespace Domain.Comentarios.Services
{
    

    static public class TagsService
    {
        static private readonly Random _random = new Random();
        static public string GenerarTag() =>  RandomTextBuilderService.BuildRandomString(_random,8);
        static public string GenerarTagUnico(HiloId hiloId, IdentityId usuarioId) => RandomTextBuilderService.BuildRandomString(
            new Random((hiloId.ToString() + usuarioId.ToString()).GetHashCode()), 3);
    }

    static public class DadosService
    {
        static private readonly Random _random = new Random();
        static public int Generar() =>  _random.Next(1, 6);
    }
}