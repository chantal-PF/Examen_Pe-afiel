using Examen_Peñafiel.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Common;
using System.Threading;

namespace Examen_Peñafiel.Datos.Repositorio
{
    public class UsuarioRepo : IUsuarioRepo
    {
        private readonly IDbConnection _connection;
        public UsuarioRepo(IDbConnection connection)
        {
            _connection = connection;
        }
        public Usuarios? Crear(Usuarios usuarios)
        {
            _connection.Open();
            var command = _connection.CreateCommand();
            command.CommandText = "INSERT INTO USUARIOS(NOMBRE, APELLIDOS, EMAIL) VALUES (@NOMBRE, @DESCRIPCION,@EMAIL) SELECT SCOPE_IDENTITY()";
            command.CommandType = CommandType.Text;

            var paraNombre = command.CreateParameter();
            paraNombre.ParameterName = "@NOMBRE";
            paraNombre.Value = usuarios.Nombre;

            var paraApellidos = command.CreateParameter();
            paraApellidos.ParameterName = "@APELLIDO";
            paraApellidos.Value = usuarios.Apellido;

            var paraEmail = command.CreateParameter();
            paraEmail.ParameterName = "@EMAIL";
            paraEmail.Value = usuarios.Email;

            command.Parameters.Add(paraNombre);
            command.Parameters.Add(paraApellidos);
            command.Parameters.Add(paraEmail);

            var result = command.ExecuteScalar();

            int nuevoId;
            if (int.TryParse(result.ToString(), out nuevoId))
            {
                usuarios.Id = nuevoId;
            }
            _connection.Close();
            return usuarios;
        }
        public bool Eliminar(int id)
        {
            bool eliminar = false;
            _connection.Open();
            var command = _connection.CreateCommand();
            command.CommandText = "DELETE USUARIOS Where ID = @ID ";
            command.CommandType = CommandType.Text;

            var paramId = command.CreateParameter();
            paramId.ParameterName = "@ID";
            paramId.Value = id;

            command.Parameters.Add(paramId);

            var result = command.ExecuteNonQuery();
            eliminar = result > 0;

            _connection.Close();
            return eliminar;
        }
        public Usuarios? ObtenerPorId(int id)
        {
            Usuarios? result = null;
            _connection.Open();

            var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM USUARIOS WHERE ID = @ID";
            command.CommandType = CommandType.Text;

            var paramId = command.CreateParameter();
            paramId.Value = id;
            paramId.ParameterName = "@Id";

            command.Parameters.Add(paramId);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result = new Usuarios
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                   Apellido = reader.GetString(2),
                   Email = reader.GetString(3),
                };
            }
            _connection.Close();
            return result;
        }

        public List<Usuarios> ObtenerTodas()
        {
            List<Usuarios> usuarios = [];
            _connection.Open();

            var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM USUARIOS";
            command.CommandType = CommandType.Text;

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var usuario = new Usuarios
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Apellido = reader.GetString(2),
                    Email = reader.GetString(3),
                };
                usuarios.Add(usuario);
            }
            _connection.Close();
            return usuarios;
        }
    }
}
