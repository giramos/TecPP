using System;
// ¿Como es la agrupacion fisica? Pues donde va guardado los ficheros, clases, etc.
// En java tanto la logica como lo fisico va en package
// Al proyecto se le llama SOLUCION, y dentro de ella hay tambien varios proyectos de menor tamaño
// ¿Qué es un namespace? Agrupoacion Logica (niveles de ocultacion, protectedf, private, etc.)
// Las librerias son DLL
namespace TPP01.Modelo
{

    /// <summary>
    /// Las clases, por defecto, son internal.
    /// Es decir, accesibles sólo desde el mismo ensamblado...
    /// Modificadores de acceso en C#: https://docs.microsoft.com/es-es/dotnet/csharp/programming-guide/classes-and-structs/access-modifiers
    /// </summary>
    public class Autor // Cambia el nivel de ocultacion de internal a public para que pueda ser accedido desde program
    {

        // Los campos se crean con el modificador "private" por defecto.
        string _nombre; //equivalente a -> private string _nombre;
        string _apellido;

        // Creamos una propiedad de lectura (get) y escritura (set) para encapsular el campo _nombre
        public string Nombre
        {
            get
            {
                return String.IsNullOrEmpty(_nombre) ? "Anónimo" : _nombre;
            }
            set
            {
                _nombre = value;
            }
        }

        // El compilador puede crear campos para las propiedades de forma transparente.

        // ¿Qué ocurre si queremos comprobar en el set que el nuevo valor tenga longitud > 1?
        public string Apellido { get { return _apellido; } set { if (_apellido.Length > 1) _apellido = value; } }


        // En ocasiones, necesitamos propiedades de solo lectura.

        // ¿Cómo se crearía una propiedad de solo escritura?
        public string Iniciales
        {
            get
            {
                return $"{Nombre[0]}.{Apellido[0]}";
            }
        }

        //Fíjate en esta propiedad de sólo lectura y dónde se le puede asignar valor.
        public DateTime CreatedAt { get; }


        /// <summary>
        /// Constructor por defecto.
        /// Si hay otro constructor, hay que declararlo explícitamente.
        /// </summary>
        public Autor()
        {
            CreatedAt = DateTime.Now;
#if DEBUG
            Console.WriteLine("Entrando en el constructor por defecto");
#endif
        }

        /// <summary>
        /// Otro constructor, por eso está declarado el anterior.
        /// Vamos a ver qué pasa si quitamos el otro.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        public Autor(string nombre, string apellido)
        {
            Nombre = nombre;
            Apellido = apellido;
            CreatedAt = DateTime.Now;

#if DEBUG
            Console.WriteLine("Entrando en el constructor con valores: {0}, {1}", nombre, apellido);
#endif
        }


        /// <summary>
        /// Método ToString. ¡OJO al override y la T mayúscula!
        /// </summary>
        /// <returns>Estado del objeto</returns>
        public override string ToString()
        {
            return $"{Nombre} {Apellido} ({Iniciales})";
        }


        /// <summary>
        /// Esto es un finalizador (anteriormente conocido como destructor).
        /// Se implementa únicamente cuando es necesario liberar recursos, conexiones...
        /// Implementarlo vacío supone una pérdida de rendimiento (como en este caso).
        /// Se invoca automáticamente.
        /// </summary>
        ~Autor()
        {
            Console.WriteLine($"Liberación de {ToString()}");
            // Liberación de recursos, conexiones... Más detalle en: https://docs.microsoft.com/es-es/dotnet/csharp/programming-guide/classes-and-structs/finalizers
        }

    }
}
