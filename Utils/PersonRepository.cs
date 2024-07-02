using opullopaxitS5.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace opullopaxitS5.Utils
{
    public class PersonRepository
    {
        string dbPath;
        private SQLiteConnection conn;
        public string statusMessage { get; set; }

        private void init()
        {
            if (conn is not null)
                return;
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<Persona>();
        }

        public PersonRepository(string path)
        {
            dbPath = path;
            init(); // Asegurar que la conexión se inicializa en el constructor
        }

        public void AddNewPerson(string nombre)
        {
            try
            {
                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("El nombre es requerido");

                // Verificar si el nombre ya existe
                var existingPerson = conn.Table<Persona>().FirstOrDefault(p => p.Nombre == nombre);
                if (existingPerson != null)
                    throw new Exception("El nombre ya existe en la base de datos");

                Persona person = new Persona() { Nombre = nombre };
                conn.Insert(person);
                statusMessage = string.Format("Dato añadido correctamente, Nombre: {0}", nombre);
            }
            catch (Exception ex)
            {
                statusMessage = string.Format("Error al insertar: {0}", ex.Message);
            }
        }

        public List<Persona> GetAllPeople()
        {
            try
            {
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                statusMessage = string.Format("Error al mostrar: {0}", ex.Message);
                return new List<Persona>();
            }
        }

        public void UpdatePerson(int id, string nombre)
        {
            try
            {
                var person = conn.Find<Persona>(id);
                if (person == null)
                    throw new Exception("Persona no encontrada");

                person.Nombre = nombre;
                conn.Update(person);
                statusMessage = string.Format("Dato actualizado correctamente, Nombre: {0}", nombre);
            }
            catch (Exception ex)
            {
                statusMessage = string.Format("Error al actualizar: {0}", ex.Message);
            }
        }

        public void DeletePerson(int id)
        {
            try
            {
                var person = conn.Find<Persona>(id);
                if (person == null)
                    throw new Exception("Persona no encontrada");

                conn.Delete(person);
                statusMessage = string.Format("Dato eliminado correctamente, Nombre: {0}", person.Nombre);
            }
            catch (Exception ex)
            {
                statusMessage = string.Format("Error al eliminar: {0}", ex.Message);
            }
        }
    }
}
