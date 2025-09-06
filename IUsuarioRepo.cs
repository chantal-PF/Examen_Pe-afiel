using Examen_Peñafiel.Entidades;
using System.Threading;

namespace Examen_Peñafiel.Datos
{
    public interface IUsuarioRepo
    {
        List<Usuarios> ObtenerTodas();
        Usuarios?  ObtenerPorId(int id);
        Usuarios? Crear(Usuarios usuarios);
        bool Eliminar(int id);

    }
}
