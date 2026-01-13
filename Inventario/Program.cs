using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace NombreDeTuProyecto
{
    internal class Program
    {
        static string connectionString = "Server=localhost\\SQLEXPRESS;Database=InventarioDB;Trusted_Connection=True;TrustServerCertificate=True";


        static void Main(string[] args)
        {
            while (true)
            {
                
                Console.WriteLine("\n----Inventario----");
                Console.WriteLine("1. Ver productos");
                Console.WriteLine("2. Agregar productos");
                Console.WriteLine("3. Atualizar producto");
                Console.WriteLine("4. Eliminar producto");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opcion: ");
                string opcion = Console.ReadLine();
                
                switch (opcion)
                {
                    case "1": VerProductos(); break;
                    case "2": AgregarProducto(); break;
                    case "3": ActualizarStock(); break;
                    case "4": EliminarProducto(); break;
                    case "6": return;
                    default:
                        Console.WriteLine("Opcion no valida."); break;
                        
                }
                

            }
       
        }
        static void VerProductos()
        {
            Console.Clear();
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string consulta = "SELECT Id, Nombre, Precio, Stock FROM Productos";
                SqlCommand comando = new SqlCommand(consulta, conexion);

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    Console.WriteLine("\nID | NOMBRE | PRECIO | STOCK");
                    while (lector.Read())
                    {
                        Console.WriteLine($"{lector["Id"]}  | {lector["Nombre"]} | {lector["Precio"]} |{lector["Stock"]}");  
                    }
                }
            }
            
        }
        static void AgregarProducto()
        {
            Console.Clear();
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine(); 
            Console.Write("Precio: ");
            decimal precio = decimal.Parse(Console.ReadLine());
            Console.Write("Stock: ");
            int stock = int.Parse(Console.ReadLine());

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string query = "INSERT INTO Productos (Nombre, Precio, Stock) VALUES (@Nombre, @Precio, @Stock)";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Nombre", nombre);
                comando.Parameters.AddWithValue("@Precio", precio);
                comando.Parameters.AddWithValue("@Stock", stock);

                comando.ExecuteNonQuery();
                Console.WriteLine("Producto agregado exitosamente.");
            }
        }
        static void ActualizarStock()
        {
            Console.Write("Id del producto a editar: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Nuevo Stock: ");
            int nuevoStock = int.Parse(Console.ReadLine());

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string query = "UPDATE Productos SET Stock = @stock Where id = @id";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@stock", nuevoStock);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
                Console.WriteLine("Stock actualizado.");

            }
        }
        static void EliminarProducto()
        {
            Console.Write("ID ddel producto a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string query = "DELETE FROM Productos WHERE Id = @id";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
                Console.WriteLine("Producto eliminado.");
            }
        }



    }
}