using System;
using System.Collections;
using Modelo;

namespace Parametros
{
    class Program
    {
        private static Random generador = new Random(657346743);
        static void Main()
        {
            //Uso de ref
            Console.WriteLine("Uso de ref\n");
            EjemploRef();

            Console.ReadLine();
            Console.Clear();

            //Uso del out
            Console.WriteLine("Uso de out\n");
            EjemploOut();

            Console.ReadLine();
            Console.Clear();

            //Parámetros por defecto.
            Console.WriteLine("Parámetros por omisión y nombrado explícito:\n");
            int[] numeros = { 1, 2, 2, 3, 4, 5, 6, 7, 7, 8 };
            Console.WriteLine("No repetidos:");

            //¿var?
            var resultado = FiltrarNumeros(numeros);
            foreach (int numero in resultado)
                Console.Write("{0} ", numero);

            Console.WriteLine("\n Repetidos múltiplos de 2:");
            resultado = FiltrarNumeros(numeros, true, 2);
            foreach (int numero in resultado)
                Console.Write("{0} ", numero);

            Console.WriteLine("\nMúltiplos de 3:");

            //Podemos usar el nombrado explícito para pasar los parámetros en cualquier orden.
            //Siempre incluyendo los parámetros obligatorios.
            resultado = FiltrarNumeros(multiplosDe: 3, numeros: numeros);
            foreach (int numero in resultado)
                Console.Write("{0} ", numero);

            Console.ReadLine();
            Console.Clear();

            //EJERCICIO: Crear un método que permita filtrar personas con parámetros opcionales.
            //El método debe recibir un array de Persona -incialízalo con los valores que consideres-.
            Persona[] per = { new Persona("a", "b", "c"), new Persona("f", "b", "c"), new Persona("a", "b", "b"), new Persona("x", "b", "c") };


            var t = FiltrarPersonas(per);
            foreach (var i in t)
                Console.WriteLine(i);

            var tt = FiltrarPersonas(per, nombre: "f", nif: "c");
            foreach (var i in tt)
                Console.WriteLine(i);
        }


        /// <summary>
        /// Ejemplos para el uso de ref
        /// </summary>
        static void EjemploRef()
        {
            int visitasVideo = 1500;
            int posibleAudiencia = 200000;
            Console.WriteLine("Visitas actuales: {0} - Posible audiencia: {1}", visitasVideo, posibleAudiencia);

            Console.WriteLine("Un mes después...");
            Avanzar(ref visitasVideo, ref posibleAudiencia);
            Console.WriteLine("Visitas actuales: {0} - Posible audiencia: {1}", visitasVideo, posibleAudiencia);

            //Veámoslo con objetos.
            double latitud = 43.36029;
            double longitud = -5.84476;
            string nombre = "Oviedo";
            PuntoDeInteres poi = new PuntoDeInteres(latitud, longitud, nombre);
            Console.WriteLine("\nPunto de interés creado: {0}", poi);
            IntentarCambio(poi);
            Console.WriteLine("Intentamos cambiar Oviedo por Asturias:\n {0}", poi);
            Cambiar(ref poi);
            Console.WriteLine("Cambiamos Oviedo por Asturias:\n {0}", poi);
            //¿Qué implicaciones tiene?
        }


        /// <summary>
        /// Al utilizar ref, pasamos los parámetros por referencia (entrada y salida).
        /// Los parámetros ref pueden ser modificados (o no) dentro del método objetivo.
        /// Los parámetros tienen que pasarse ya inicializados.
        /// </summary>
        /// <param name="visitasVideo">Número de visitas, puede ser modificado por referencia</param>
        /// <param name="posibleAudiencia">Posible audencia, puder ser modificado por referencia</param>
        static void Avanzar(ref int visitasVideo, ref int posibleAudiencia)
        {
            if (visitasVideo < posibleAudiencia)
            {
                double porcentaje = generador.NextDouble();
                int aleatorio = (int)(posibleAudiencia * porcentaje);
                visitasVideo += aleatorio;
                posibleAudiencia -= aleatorio;
            }
        }


        static void IntentarCambio(PuntoDeInteres poi)
        {
            //¿Por qué no funciona?
            string nombre = "Asturias"; 
            poi = new PuntoDeInteres(poi.Latitud, poi.Longitud, nombre);
        }

        static void Cambiar(ref PuntoDeInteres poi)
        {
            string nombre = "Asturias";
            poi = new PuntoDeInteres(poi.Latitud, poi.Longitud, nombre);
        }

        /// <summary>
        /// Uso del out
        /// </summary>
        static void EjemploOut()
        {
            int[] numeros = GeneradorAleatorios(50, 1, 30); //50 números en [1,30]
            int minimo, maximo;
            double media;
            CalcularEstadisticos(numeros, out media, out minimo, out maximo);
            Console.WriteLine("Estadísticos: Media = {0} - Máximo = {1} - Mínimo = {2}", media, maximo, minimo);
        }


        /// <summary>
        /// Mediante out pasamos los parámetros por referencia (salida).
        /// Los parámetros out deben ser modificados/inicializados en el método, OBLIGATORIAMENTE.
        /// 
        /// </summary>
        /// <param name="valores">Número de enteros a crear en el array</param>
        /// <param name="media"></param>
        /// <param name="minimo"></param>
        /// <param name="maximo"></param>
        static void CalcularEstadisticos(int[] valores, out double media, out int minimo, out int maximo)
        {
            minimo = valores[0];
            maximo = valores[0];
            double suma = valores[0];
            for (int i = 1; i < valores.Length - 1; i++)
            {
                suma += valores[i];

                if (valores[i] > maximo)
                    maximo = valores[i];

                if (valores[i] < minimo)
                    minimo = valores[i];
            }
            media = suma / valores.Length;
        }


        /// <summary>
        /// Filtra un array de número en base a si son múltiplos de un número y/o están repetidos en el origen.
        /// </summary>
        /// <param name="numeros">Array de enteros</param>
        /// <param name="repetidos">Si en los resultados se devolverán números repetidos (false por defecto)</param>
        /// <param name="multiplosDe"> Se devolverán los múltiplos de este valor (1 por defecto)</param>
        /// <returns></returns>
        public static IList FiltrarNumeros(int[] numeros, bool repetidos = false, int multiplosDe = 1)
        {
            ArrayList resultado = new ArrayList();
            foreach (var actual in numeros)
            {
                if (actual % multiplosDe == 0 && (repetidos || repetidos == resultado.Contains(actual)))
                    resultado.Add(actual);
            }

            //Fíjate en el el tipo de la variable resultado y el tipo de retorno del método.
            //¿Qué está ocurriendo?
            return resultado;
        }


        public static IList FiltrarPersonas(Persona[] personas, string nombre = "a", string nif = "b")
        {
            IList resultado = new ArrayList();
            foreach (var actual in personas)
            {
                if (actual.Nombre == nombre || actual.Nif == nif)
                    resultado.Add(actual);
            }
            return resultado;
        }

        /// <summary>
        /// Método para generar un array con N enteros en un rango [a,b]
        /// </summary>
        /// <param name="cantidad">Número de elementos</param>
        /// <param name="minimo">Valor mínimo</param>
        /// <param name="maximo">Valor máximo (incluido)</param>
        /// <returns></returns>
        static int[] GeneradorAleatorios(int cantidad, int minimo, int maximo)
        {
            int[] aleatorios = new int[cantidad];
            for (int i = 0; i < cantidad; i++)
                aleatorios[i] = generador.Next(minimo, maximo + 1);
            return aleatorios;
        }
    }
}
