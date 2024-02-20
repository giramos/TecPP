using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Modelo;

namespace DelPredefinidosGenericos
{
    /// <summary>
    /// C# Contiene una serie de delegados predefinidos genéricos, véase: Func, Predicate y Action.
    /// </summary>
    class Program
    {
        public static string Concatenar(char a, char b)
        {
            return String.Format("{0}{1}", a, b);
        }

        public static bool TieneLongitudUno(string cadena)
        {
            return cadena.Length == 1;
        }

        public static void Almacena(int valor)
        {
            //<-- Código que simula almacenar el valor en algún lugar.
            Console.WriteLine("LOG: El valor {0} ha sido almacenado", valor);
        }

        static void Main(string[] args)
        {
            //Func Puede recibir entre 0 y 16 parámetros y siempre retorna un valor

            Func<char, char, string> funcion = Concatenar;
            Console.WriteLine( funcion('f', 'u') );

            //Predicate Siempre recibe un único parámetro y siempre devuelve bool.
            //¿Qué alternativa tenemos?

            Predicate<string> funcionPredicado = TieneLongitudUno;
            String cadena = "S";

            if (funcionPredicado(cadena)) //Recordad, para invocar tenemos que usar () con los parámetros necesarios.
                Console.WriteLine(cadena + " tiene longitud 1.");
            else
                Console.WriteLine(cadena + " no tiene longitud 1.");

            //Action recibe entre 0 y 16 parámetros y NO DEVUELVE NADA (void)

            Action<int> funcionAction = Almacena;
            funcionAction(2);

            int valor = 2;

            //Pasamos como primer parámetro una función. AplicaciónDoble ¿Es una función de orden superior?
            int resultado = AplicaciónDoble(Suma, valor);
            Console.WriteLine("Resultado de la aplicación doble: {0}", resultado);

            //Ejemplo de orden superior.
            Persona[] personas = Factoria.CrearPersonas();
            Persona[] adultos = Array.FindAll(personas, EsAdulto);

            adultos.ForEach(Console.WriteLine);


            // Ejercicios

            Libro[] libros = Factoria.CrearLibros();
            int nPag = 100;
            var res = Contar(libros, x => x.NumeroPaginas < nPag);
            Console.WriteLine("\nEJERCICIO CLASE:");
            Console.WriteLine("Cuantos libros tienen menos de {0} paginas : {1}", nPag, res);

            int año = 2018;
            var res1 = Contar(libros, x => x.Año < año);
            Console.WriteLine("\nEJERCICIO CLASE:");
            Console.WriteLine("Cuantos libros tienen se publicaron mas tarde del año {0}: {1}", nPag, res1);

            string palabra = "comotatucucucucucuAOAADOUHAWEIUWFBNASIUHDWioalaeenofaspjghwe";
            var res2 = Contar(palabra, x => "aeiouaAEIOU".Contains(palabra));
            Console.WriteLine("\nEJERCICIO CLASE:");
            Console.WriteLine("Cuantos vocales tiene la siguiente palabra {0}: \n{1}", palabra, res2);
            
            string palabra1 = "comotatucucucucucuAOAADOUHAWEIUWFBNASIUHDWioalaeenofaspjghwe";
            var res3 = Contiene2(palabra1, x => "aeiouaAEIOU".Contains(palabra1));
            Console.WriteLine("\nEJERCICIO CLASE:");
            Console.WriteLine("Cuantos vocales tiene la siguiente palabra {0}: \n{1}", palabra1, res3);
            
            string palabra2 = "aeiojjj";
            var res4 = Contiene2(palabra2, x => "bcdghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ".Contains(palabra2));
            Console.WriteLine("\nEJERCICIO CLASE:");
            Console.WriteLine("Cuantos consonantes tiene la siguiente palabra {0}: \n{1}", palabra2, res4);

            Func<int,int,int> mult = (a,b) => a * b;
            var resMult = mult(3, 2);
            Console.Write("La multiplicacion entre 3 y 2 es {0}", resMult);

            Func<string,int,bool> comp = (a,b) =>
            {
                if (a.Length == b)
                {
                    return true;
                }
                else
                    return false;
            };

            Func<string, int, bool> comp1 = (a, b) => a.Length == b ? true : false;
            var resC1 = comp1("hola", 4);
            Console.WriteLine("\nComprobar cadena1 {0}", resC1);

            Func<int, int, bool> div = (a, b) => a%b == 0 ? true : false;
            var resD = div(3, 4);
            Console.WriteLine("\nSon divisibles {0}", resD);




        }

        public static bool Comprobar(string cadena, int longi)
        {
            if(cadena.Length == longi)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
     

        public static int Suma(int a)
        {
            return a + a;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="funcion"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static int AplicaciónDoble(Func<int, int> funcion, int a)
        {
            // Puedo pasar el resultado de la primera aplicación al mismo método.
            //El Func necesita un int y devuelve un int.
            return funcion(funcion(a));
        }

        private static bool EsAdulto(Persona persona)
        {
            return persona.Edad >= 18;
        }

        Predicate<Persona> adulto = a => a.Edad >= 18;

        public static int Contar<T>(IEnumerable<T> col, Predicate<T> cond)
        {
            int cont=0;
            foreach (T t in col)
            {
                if (cond(t))
                {
                    cont++;
                }
            }
            return cont;
        }

        public static T Contiene<T>(IEnumerable<T> col, Predicate<T> cond)
        {
            foreach (T item in col)
            {
                if (cond(item))
                {
                    return item;
                }
            }
            return default;
        }
        
        public static int Contiene2<T>(IEnumerable<T> col, Predicate<T> cond)
        {
            int i = -1;
            foreach (T item in col)
            {   
                i++;
                if (cond(item))
                {
                    return i;
                }
            }
            return i;
        }

        public int PosicionPrimero<T>(T[] v, Predicate<T> cond)
        {
            for (int i = 0; i < v.Length; i++)
            {
                if (cond(v[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public IEnumerable<T> Filtrar<T>(IEnumerable<T> col, Predicate<T> cond)
        {
            IList<T> list = new List<T>();
            foreach (T item in col)
            {
                if (cond(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public void Mostrar<T>(IEnumerable<T> col, Action<T> act)
        {
            foreach (T item in col) { act(item); }
        }

    }
}
