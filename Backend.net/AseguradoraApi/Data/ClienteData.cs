using AseguradoraApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace AseguradoraApi.Data
{
    public class ClienteData
    {
        private readonly string conexion; // Cadena de conexión para la base de datos.

        public ClienteData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!; // Obtiene la cadena de conexión de la configuración.
        }

        // Método para obtener una lista de clientes.
        public async  Task<List<Cliente>> Lista()
        {
            List<Cliente> lista = new List<Cliente>(); // Lista para almacenar los clientes obtenidos.

            // Crea una conexión SQL utilizando la cadena de conexión.
            using (var con = new SqlConnection(conexion))
            {

                await  con.OpenAsync(); // Abre la conexión.
                SqlCommand cmd = new SqlCommand("sp_listaClientes", con); // Crea un comando SQL para ejecutar el procedimiento almacenado.
                cmd.CommandType = CommandType.StoredProcedure; 

                // Ejecuta el comando de manera asíncrona.
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    // Lee los datos obtenidos.
                    while (await reader.ReadAsync())
                    {
                        // Agrega un nuevo cliente a la lista.
                        lista.Add(new Cliente
                        {
                            IdCliente = Convert.ToInt32(reader["IdCliente"]),
                            DocumentoIdentidad = reader["DocumentoIdentidad"].ToString(),
                            PrimerNombre = reader["PrimerNombre"].ToString(),
                            SegundoNombre = reader["SegundoNombre"].ToString(),
                            PrimerApellido = reader["PrimerApellido"].ToString(),
                            SegundoApellido = reader["SegundoApellido"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Email = reader["Email"].ToString(),
                            FechaNacimiento = reader["FechaNacimiento"].ToString(),
                            ValorSeguro = Convert.ToDecimal(reader["ValorSeguro"]),
                            Observaciones = reader["Observaciones"].ToString()
                            

                        });
                    }
                }
            }
            return lista;
        }

        // Método para obtener un cliente específico por su ID.
        public async Task<Cliente> Obtener(int idCliente)
        {
            Cliente cliente = new Cliente(); // Crea un cliente vacío para almacenar los datos obtenidos.

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_obtenerCliente", con);
                cmd.Parameters.AddWithValue("@IdCliente", idCliente); // Agrega el parámetro necesario para el procedimiento almacenado.
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        cliente = new Cliente
                        {
                            IdCliente = Convert.ToInt32(reader["IdCliente"]),
                            DocumentoIdentidad = reader["DocumentoIdentidad"].ToString(),
                            PrimerNombre = reader["PrimerNombre"].ToString(),
                            SegundoNombre = reader["SegundoNombre"].ToString(),
                            PrimerApellido = reader["PrimerApellido"].ToString(),
                            SegundoApellido = reader["SegundoApellido"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Email = reader["Email"].ToString(),
                            FechaNacimiento = reader["FechaNacimiento"].ToString(),
                            ValorSeguro = Convert.ToDecimal(reader["ValorSeguro"]),
                            Observaciones = reader["Observaciones"].ToString()
                        };
                    }
                }
            }
            return cliente;
        }

        // Método para crear un nuevo cliente en la base de datos.
        public async Task<bool> Crear(Cliente cliente)
        {
            bool respuesta = true; // Variable para almacenar el resultado de la operación. Se inicializa en true, asumiendo éxito.

            using (var con = new SqlConnection(conexion))
            {
                // Agrega los parámetros al comando utilizando los valores del cliente.
                SqlCommand cmd = new SqlCommand("sp_crearCliente", con);
                cmd.Parameters.AddWithValue("@DocumentoIdentidad", cliente.DocumentoIdentidad);
                cmd.Parameters.AddWithValue("@PrimerNombre", cliente.PrimerNombre);
                cmd.Parameters.AddWithValue("@SegundoNombre", cliente.SegundoNombre);
                cmd.Parameters.AddWithValue("@PrimerApellido", cliente.PrimerApellido);
                cmd.Parameters.AddWithValue("@SegundoApellido", cliente.SegundoApellido);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
                cmd.Parameters.AddWithValue("@FechaNacimiento", cliente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@ValorSeguro", cliente.ValorSeguro);
                cmd.Parameters.AddWithValue("@Observaciones", cliente.Observaciones);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    // Si el número de filas afectadas es mayor que 0, la respuesta es true (éxito), de lo contrario, es false (fallo).
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true: false;
                }
                catch
                {
                    // Si ocurre una excepción, establece la respuesta en false (fallo).
                    respuesta = false;
                }
            }
            return respuesta;
        }

        // Método para actualizar los datos de un cliente en la base de datos.
        public async Task<bool> Editar(Cliente cliente)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_editarCliente", con);
                cmd.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);
                cmd.Parameters.AddWithValue("@DocumentoIdentidad", cliente.DocumentoIdentidad);
                cmd.Parameters.AddWithValue("@PrimerNombre", cliente.PrimerNombre);
                cmd.Parameters.AddWithValue("@SegundoNombre", cliente.SegundoNombre);
                cmd.Parameters.AddWithValue("@PrimerApellido", cliente.PrimerApellido);
                cmd.Parameters.AddWithValue("@SegundoApellido", cliente.SegundoApellido);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
                cmd.Parameters.AddWithValue("@FechaNacimiento", cliente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@ValorSeguro", cliente.ValorSeguro);
                cmd.Parameters.AddWithValue("@Observaciones", cliente.Observaciones);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    // Si el número de filas afectadas es mayor que 0, la respuesta es true (éxito), de lo contrario, es false (fallo).
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    // Si ocurre una excepción, establece la respuesta en false (fallo).
                    respuesta = false;
                }
            }
            return respuesta;
        }

        // Método para eliminar un cliente de la base de datos utilizando su ID.
        public async Task<bool> Eliminar(int id)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_eliminarCliente", con);
                // Agrega el parámetro 'IdCliente' al comando con el valor del ID proporcionado.
                cmd.Parameters.AddWithValue("@IdCliente", id);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    // Si el número de filas afectadas es mayor que 0, la respuesta es true (éxito), de lo contrario, es false (fallo).
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    // Si ocurre una excepción, establece la respuesta en false (fallo).
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}
