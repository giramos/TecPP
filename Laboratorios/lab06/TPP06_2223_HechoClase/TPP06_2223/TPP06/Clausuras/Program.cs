using System;
using System.Collections.Generic;
using System.Linq;

namespace Clausuras
{
    public static class Program
    {
        public static int ContarSi<T>(this IEnumerable<T> coleccion, Predicate<T> condicion)
        {
            int contador = 0;

            foreach (var elemento in coleccion)
                if ( condicion(elemento) )
                    contador++;

            return contador;
        }

        public static int ContarMenoresQue(IEnumerable<int> coleccion, int n)
        {
            int contador = 0;

            foreach (var elemento in coleccion)
                if (elemento < n)
                    contador++;

            return contador;
        }

        static void Main()
        {
            // Contar cuántos números de una colección son menores que N.

            //.Range permite crear colecciones de enteros. Partimos de 0 hasta llegar a 20 ELEMENTOS 
            // En este caso, [0, 19].

            IEnumerable<int> coleccion = Enumerable.Range(0, 20);

            int n = 5;

            //Concepto de Clausura (cierre, closure).

            //Compárese este Predicado con la función definida más arriba "ContarMenoresQue"
            Predicate<int> EsMenor =  numero => numero < n;

            int resultado = coleccion.ContarSi(EsMenor);

            Console.WriteLine($"La colección tiene {resultado} valores menores que {n}");

            // ¿Qué ocurre si cambio el valor de n? ¿Dónde está definida?
            n = 2;
            resultado = coleccion.ContarSi(EsMenor);

            // El Predicado "EsMenor" almacena el valor de n? ¿Qué almacena?
            Console.WriteLine($"La colección tiene {resultado} valores menores que {n}");

           
            LimpiarPantalla();

            
            //Un contador mediante el uso de objetos (POO).
            int iteraciones = 15;
            
            ClaseContador contador = new ClaseContador();
            
            for (int i = 0; i < iteraciones; i++)
                contador.Incrementar();

            Console.WriteLine($"[Contador Objetos] Valor actual del contador: {contador.Valor}");

            
            //Equivalente empleando una clausura

            Func<int> contadorClausula = ClausuraContador.CrearContador();
            
            int valorContador = 0;

            for (int i = 0; i < iteraciones; i++)
                valorContador = contadorClausula();

            Console.WriteLine($"[Contador Clausura] Valor actual del contador: {valorContador}");


            LimpiarPantalla();

            //¿Y si se necesitan varias clausuras para manipular el estado?

            //Un ejemplo simple con un getter y un setter.

            //En POO manipulamos el estado mediante métodos.
            Contenedor<int> contenedor = new Contenedor<int>(5);
            contenedor.SetValor(20);
            Console.WriteLine($"[Objetos] Almacenado: {contenedor.GetValor()}");


            //Sin embargo, mediante clausuras...

            //set recibe el nuevo valor y no devuelve nada.
            Action<int> SetValor;
            //get no recibe nada y devuelve un valor.
            Func<int> GetValor;

            ClausuraContenedor.CrearContador(5, out SetValor, out GetValor);
            Console.WriteLine($"[Cláusuras] Almacenado: {GetValor()}");

            SetValor(20);

            Console.WriteLine($"[Cláusuras] Almacenado: {GetValor()}");


            var res = Ejercicio.Alea();
            Console.WriteLine($"Aleatorio normal {res}");

            var res1 = Ejercicio.AleaCierre();
            Console.WriteLine($"Aleatorio cierre {res1()}");
            Console.WriteLine($"Aleatorio cierre {res1()}");
            Console.WriteLine($"Aleatorio cierre {res1()}");
            Console.WriteLine($"Aleatorio cierre {res1()}");
            Console.WriteLine($"Aleatorio cierre {res1()}");

            var res2 = Ejercicio.AleaCierre(100);
            Console.WriteLine("");
            Console.WriteLine($"Aleatorio cierre sobrecarga {res2()}");
            Console.WriteLine($"Aleatorio cierre sobrecarga {res2()}");
            Console.WriteLine($"Aleatorio cierre sobrecarga {res2()}");
            Console.WriteLine($"Aleatorio cierre sobrecarga {res2()}");
            Console.WriteLine($"Aleatorio cierre sobrecarga {res2()}");

            int j = 0;
            Ejercicio.BucleWhileFuncional(() => j < 10, () =>
            {
                //Cuerpo
                j++;
                Console.WriteLine($"El valor de contador es {j}");
            });

            Console.WriteLine();
            var res3 = Ejercicio.Fibo(20);
            var res4 = Ejercicio.Fibonacci(20);
            var res5 = Ejercicio.FibonacciCierre(20);
            Console.WriteLine($"Fibonacci normal {res4}");
            Console.WriteLine($"Fibonacci {res3()}");
            Console.WriteLine($"Fibonacci {res3()}");
            Console.WriteLine($"Fibonacci {res3()}");
            Console.WriteLine($"Fibonacci cierre {res5()}");
            Console.WriteLine($"Fibonacci cierre {res5()}");
            Console.WriteLine($"Fibonacci cierre  {res5()}");
            Console.WriteLine($"Fibonacci cierre {res5()}");
            Console.WriteLine($"Fibonacci cierre {res5()}");
        }

        static void LimpiarPantalla()
        {
            Console.WriteLine("\nPulse una tecla para continuar al siguiente ejemplo...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
