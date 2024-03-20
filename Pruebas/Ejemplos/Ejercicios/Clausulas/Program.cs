using System;
using System.Collections.Generic;

namespace Clausulas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Vocales closure dicc");
            var palabra = "aasfkjhfweEESIDFSINWOUASNDJHRUWBSDuusdusiwoo";
            var res = Vocales4(palabra);
            Console.WriteLine(res());
            Console.WriteLine(res());
            Console.WriteLine(res());
            Console.WriteLine(res());
            Console.WriteLine(res());

            Console.WriteLine();

            Console.WriteLine("Vocales closure lista de arrays");
            var res1 = Vocales(palabra);
            Console.WriteLine(res1());
            Console.WriteLine(res1());
            Console.WriteLine(res1());
            Console.WriteLine(res1());
            Console.WriteLine(res1());

            Console.WriteLine();

            Console.WriteLine("Vocales closure lista de enteros");
            var res2 = Vocales1(palabra);
            Console.WriteLine(res2());
            Console.WriteLine(res2());
            Console.WriteLine(res2());
            Console.WriteLine(res2());
            Console.WriteLine(res2());

            Console.WriteLine();

            Console.WriteLine("ESTRUCTURAS DE CONTROL");

            Console.WriteLine("While con array de actions body");

            int i = 0; int j = 0;
            BucleWhile(() => i < 3, new Action[] {
                () => BucleWhile(() => j < 5,
                new Action[] {
                    () => Console.Write("[{0} {1}]", i, j),
                    () => j++
                }),
                () => i++, () => j = 0, () => Console.WriteLine()
            });

            Console.WriteLine("\nWhile Loop con clausuras");
            i = 0; // The i variable is used in both closures (condition and body)
            WhileLoop(() => i < 10, () => { Console.Write(i + " "); i++; });
            Console.WriteLine();

            Console.WriteLine("\nRepeat Loop con clausuras");
            j = 10;
            RepeatUntilLoop(() => j < 0, () => { Console.Write(j + " "); j--; });
            Console.WriteLine();

            Console.WriteLine("\nDoWhile con clausuras");
            int k = 5;
            DoWhileLoop(() => k < 10, () => { Console.Write(k + " "); k++; });
            Console.WriteLine();

            Console.WriteLine("\nFor con clausuras");
            int l = 0;
            ForLoop(() => l < 10, () => Console.Write(l + " "), () => l++);
            Console.WriteLine();

            Console.WriteLine();

            Console.WriteLine("EJERCICIO ===========> Uso del Loop while");

            Console.WriteLine("While");
            int[,] a = new int[3, 4] {
                            {0, 1, 2, 3} ,
                            {4, 5, 6, 7} ,
                            {8, 9, 10, 11} ,
                        };
            int row = 0;
            int column = 0;
            //while (row < 3)
            //{
            //    while (column < 4)
            //    {
            //        Console.Write("{0} ", a[row, column]);
            //        column++;
            //    }
            //    Console.WriteLine();
            //    column = 0;
            //    row++;
            //}
            //Console.WriteLine();
            Console.WriteLine("Loop While");
            WhileLoop(() => row < 3, () =>
            {
                WhileLoop(() => column < 4, () =>
                {
                    Console.Write("{0} ", a[row, column]);
                    column++;
                });
                Console.WriteLine();
                column = 0;
                row++;
            });


            Console.WriteLine();

            Console.WriteLine("Fibonacci cierre");
            var fi = FiboCierre();
            Console.WriteLine(fi());
            Console.WriteLine(fi());
            Console.WriteLine(fi());
            Console.WriteLine(fi());
            Console.WriteLine(fi());
            Console.WriteLine(fi());
            Console.WriteLine(fi());
            Console.WriteLine(fi());
            Console.WriteLine(fi());

            Console.WriteLine();

            // SUMATORIO

            Console.WriteLine("\nSumatorio Clausura");
            var suma = SumatorioClausura(10);
            for (int o = 0; o < 10; o++)
            {
                Console.WriteLine(suma());
            }

            Console.WriteLine("\nSumatorios normal");
            var suma1 = Sumatorio(10);
            Console.WriteLine(suma1);
            suma1 = Sumatorio(20);
            Console.WriteLine(suma1);


            Console.WriteLine();


            //CONTADOR
            Console.WriteLine("\n Contador con clausura");
            var sumar5 = ContadorConClausura(5);
            var sumar10 = ContadorConClausura(10);

            sumar5(); // Imprime 5
            sumar5(); // Imprime 10
            sumar10(); // Imprime 10
            sumar5(); // Imprime 15

            Console.WriteLine();
            //
            Console.WriteLine("\n Contador con clausura Version2");
            var conta = CrearContador();
            int valorContador = 0;
            for (int ii = 0; ii < 5; ii++)
            {
                valorContador = conta();
            }
            Console.WriteLine($"[Contador Clausura] Valor actual del contador: {valorContador}");

            Console.WriteLine();

            Console.WriteLine("Entero clausura");
            //ENTERO
            var entero = Entero()(10);
            Console.WriteLine(entero()); // salida: 10
            entero = Entero()(20);
            Console.WriteLine(entero()); // salida: 20

            Console.WriteLine();

            Console.WriteLine("Cierres si queremos modificar el estado manualmente");
            //¿Y si se necesitan varias clausuras para manipular el estado?

            //set recibe el nuevo valor y no devuelve nada.
            Action<int> SetValor;
            //get no recibe nada y devuelve un valor.
            Func<int> GetValor;

            CrearConstructor(5, out SetValor, out GetValor);
            Console.WriteLine($"[Clausuras] Almacenado: {GetValor()}");

            SetValor(20);

            Console.WriteLine($"[Clausuras] Almacenado: {GetValor()}");

            Console.WriteLine();

            Console.WriteLine("ALEATORIO");

            Console.WriteLine("ALEATORIO primera vversion");
            var alea = Aleatorio(50);
            Console.WriteLine(alea());
            Console.WriteLine(alea());
            Console.WriteLine(alea());
            Console.WriteLine(alea());
            Console.WriteLine(alea());
            Console.WriteLine(alea());
            Console.WriteLine(alea());

            Console.WriteLine("ALEATORIO segunda version");
            alea = Aleatorio_v1(50);
            Console.WriteLine(alea());
            Console.WriteLine(alea());
            Console.WriteLine(alea());
            Console.WriteLine(alea());
            Console.WriteLine(alea());
            Console.WriteLine(alea());


            // Prueba de la versión 2
            Console.WriteLine("\nPrueba de la versión 2:");
            Action<int> set;
            Func<int> get;
            Aleatorio_v2(10, out set, out get);
            for (int iiii = 0; iiii < 15; iiii++)
            {
                Console.WriteLine(get());
            }

            // Reiniciar el generador y modificar el valor inicial en la versión 2
            set(200);
            Console.WriteLine("\nGenerador reiniciado con valor inicial 200:");
            for (int im = 0; im < 15; im++)
            {
                Console.WriteLine(get());
            }

            // Prueba de la versión con reinicio manual y modificación del valor inicial (Apartado B)
            Console.WriteLine("\nPrueba de la versión 3:");
            Action<int> setValor;
            Func<int> getValor;
            CrearConstructor(10, out setValor, out getValor);

            // Obtener valor inicial
            int valorInicial = getValor();
            Console.WriteLine("Valor inicial: " + valorInicial);

            // Modificar valor inicial
            setValor(20);
            Console.WriteLine("Nuevo valor inicial: " + getValor());

            // Generar números aleatorios
            for (int iii = 0; iii < 5; iii++)
            {
                Console.WriteLine(Aleatorio_v3(ref setValor, ref getValor));
            }

            Console.WriteLine("Prueba la version Aleatorio veersion 4");
            // Versión 4
            Action<int> Modify;
            Action Reset;
            //get no recibe nada y devuelve un valor
            Func<int> GetValorGenerador;
            Aleatorio_v4(50, out Reset, out GetValorGenerador, out Modify);
            int val = GetValorGenerador();
            while (val != 0)
            {
                Console.WriteLine(val);
                val = GetValorGenerador();
            }
            Console.WriteLine(val);
            Console.WriteLine("RESET");
            Reset();
            val = GetValorGenerador();
            while (val != 0)
            {
                Console.WriteLine(val);
                val = GetValorGenerador();
            }
            Console.WriteLine(val);
            Console.WriteLine("MODIFY 450");
            Modify(450);
            val = GetValorGenerador();
            while (val != 0)
            {
                Console.WriteLine(val);
                val = GetValorGenerador();
            }
            Console.WriteLine(val);
        }

        public static Func<int> Vocales4(string cadena)
        {
            IDictionary<char, int> dicc = new Dictionary<char, int>();
            string cadena1 = cadena.ToLower();
            foreach (var i in cadena1)
            {
                if ("aeiou".Contains(i))
                {

                    if (!dicc.ContainsKey(i))
                    {
                        dicc[i] = 0;
                    }
                    dicc[i]++;
                }
            }
            // Crear la clausura
            List<char> vowels = new List<char>("aeiou");
            int indexCounter = 0;

            return () =>
            {
                // Devolver el recuento correspondiente y luego reiniciar si se alcanza el final
                char vowel = vowels[indexCounter];
                int count = dicc.ContainsKey(vowel) ? dicc[vowel] : 0;
                indexCounter = (indexCounter + 1) % 5; // Para volver al inicio cuando se alcanza la última vocal
                return count;
            };
        }

        public static Func<int> Vocales(string cadena)
        {
            IList<int> list = new List<int>(new int[5]);
            string cadena1 = cadena.ToLower();
            foreach (var item in cadena1)
            {
                if ("aeiou".Contains(item))
                {
                    int indice = "aeiou".IndexOf(item);
                    list[indice]++;
                }
            }
            // Creamos la clausura
            int cont = 0;
            return () =>
            {
                int index = list[cont];
                cont = (cont + 1) % 5;
                return index;
            };
        }

        public static Func<int> Vocales1(string cadena)
        {
            IList<int> list = new List<int>();
            string cadena1 = cadena.ToLower();
            int contadorA = 0;
            int contadorE = 0;
            int contadorI = 0;
            int contadorO = 0;
            int contadorU = 0;
            foreach (var i in cadena1)
            {
                if (i.Equals('a')) { contadorA++; }
                else if (i.Equals('e')) { contadorE++; }
                else if (i.Equals('i')) { contadorI++; }
                else if (i.Equals('o')) { contadorO++; }
                else if (i.Equals('u')) { contadorU++; }
                else { }
            }
            list.Add(contadorA);
            list.Add(contadorE);
            list.Add(contadorI);
            list.Add(contadorO);
            list.Add(contadorU);

            // Creacion de la clausuara
            int index = 0;
            return () =>
            {
                int cont = list[index];
                index = (index + 1) % 5;
                return cont;
            };
        }
        //ESTRUCTURAS DE CONTROL

        // Simula el while
        public static void WhileLoop(Func<bool> condition, Action body)
        {
            if (condition())
            {
                body();
                WhileLoop(condition, body); // recursion to iterate
            }
        }

        public static void BucleWhile(Func<bool> condition, Action[] bodies)
        {
            if (condition())
            {
                foreach (var x in bodies)
                    x();
                BucleWhile(condition, bodies);
            }
        }

        // Simula el while de Pascal (hasta que no sea cierta no para)
        public static void RepeatUntilLoop(Func<bool> condition, Action body)
        {
            if (!condition())
            {
                body();
                RepeatUntilLoop(condition, body);
            }
        }

        // Simula el doWhile 
        public static void DoWhileLoop(Func<bool> condition, Action body)
        {
            if (condition())
            {
                body();
                DoWhileLoop(condition, body);
            }
        }

        // Simula el for
        public static void ForLoop(Func<bool> cond, Action body, Action inc)
        {
            if (cond())
            {
                body();
                inc();
                ForLoop(cond, body, inc);
            }
        }

        // Fibonacci normal. con recursividad
        public static int Fibo(int numero)
        {
            return numero <= 2 ? 1 : Fibo(numero - 2) + Fibo(numero - 1);
        }
        // Fibonacci cierre. con recursividad
        static Func<int> FiboCierre1()
        {
            int n = -2;
            return () =>
            {
                n++;
                return n <= 2 ? 1 : Fibo(n - 2) + Fibo(n - 1);
            };
        }

        // FIbonacci con cierre
        public static Func<int> FiboCierre()
        {
            int first = 0, second = 1;
            bool isFirst = true; // etiqueta para indicar si es el primer número de la secuencia

            return () =>
            {
                if (isFirst)
                {
                    isFirst = false; // Desactiva la etiqueta después de la primera secuencia
                    return 1;
                }

                int n = first + second;
                first = second;
                second = n;
                return n;
            };
        }


        // Ejercicio Sumatorio
        //        sumatorio de todos los múltiplos de 3 o 5 que se
        //encuentren por debajo de un nº concreto, por ejemplo 1000

        public static int Sumatorio(int n)
        {
            int sumatorio = 0;
            for (int i = 0; i < n; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    sumatorio += i;
                }
            }
            return sumatorio;
        }
        // Ejercicio Sumatorio
        //        sumatorio de todos los múltiplos de 3 o 5 que se
        //encuentren por debajo de un nº concreto, por ejemplo 1000
        public static Func<int> SumatorioClausura(int n)
        {
            return () =>
            {
                int sumatorio = 0;
                for (int i = 0; i < n; i++)
                {
                    if (i % 3 == 0 || i % 5 == 0)
                    {
                        sumatorio += i;
                    }
                }
                return sumatorio;
            };

        }

        // Ejercicio Contador
        public static Action ContadorConClausura(int incremento)
        {
            int contador = 0;
            return () =>
            {
                contador += incremento;
                Console.WriteLine(contador);
            };
        }

        public static Func<int> CrearContador()
        {
            //Se define el estado
            int contador = 0;

            //Se devuelve la clausura

            return () => ++contador;

        }

        // Ejerccio Entero
        public static Func<int, Func<int>> Entero()
        {
            int entero = 0;

            return (n) =>
            {
                entero = n;

                return () => entero;
            };
        }

        // Suma de enteros de una lista con clausura
        public static Func<int, int> Suma(List<int> numeros)
        {
            int resultado = 0;
            return (int num) =>
            {
                resultado += num;
                if (num == numeros[numeros.Count - 1])
                {
                    return resultado;
                }
                else
                {
                    return 0;
                }
            };
        }

        // Crear constructor con clausuras
        public static void CrearConstructor<T>(T valor, out Action<T> set, out Func<T> get)
        {
            //Se define el estado
            T _valor = valor;

            //Funciones a definir

            set = valor => _valor = valor;

            get = () => _valor;

        }

        /* Examen 21/22

       Ejercicio 1 (A – 1,50 puntos).

           Dado un valor inicial, impleméntese una clausura que, en cada invocación,
           devuelva un número aleatorio inferior al anterior devuelto.Una vez llegue al valor
           cero y lo devuelva, el generador se reiniciará al valor inicial de forma automática.

           (B – 1,00 punto).

           Cree una versión del anterior que permita tanto reiniciar el generador de forma manual
           como modificar el valor inicial.


           Añádase código en el método Main para probar ambas versiones.

        */

        public static Func<int> Aleatorio(int valor)
        {
            Random r = new Random();
            int num = valor;
            return () =>
            {
                num = r.Next(0, num);
                return num;
            };
        }

        public static Func<int> Aleatorio_v1(int valor)
        {
            Random r = new Random();
            int num = valor;
            return () =>
            {
                if (num == 0)
                {
                    num = valor;
                }
                num = r.Next(0, num);
                return num;

            };
        }
        public static void Aleatorio_v2(int valor, out Action<int> set, out Func<int> get)
        {
            Random r = new Random();
            int num = valor;
            set = (newValor) => { num = newValor; };
            get = () =>
            {
                if (num == 0)
                {
                    num = valor;
                }
                num = r.Next(0, num);
                return num;
            };
        }

        public static int Aleatorio_v3(ref Action<int> setValor, ref Func<int> getValor)
        {
            int valorActual = getValor();
            if (valorActual == 0)
            {
                // Reiniciar generador
                setValor(getValor());
            }
            Random r = new Random();
            int numeroAleatorio = r.Next(0, valorActual);
            setValor(numeroAleatorio);
            return numeroAleatorio;
        }

        public static void Aleatorio_v4(int inicial, out Action reset, out Func<int> get, out Action<int> modify)
        {
            Random random = new Random();
            int _valor = inicial;
            reset = () => _valor = inicial;
            get = () => _valor = random.Next(0, _valor);
            modify = newValue => _valor = newValue;
        }
    }
}
