using ColaConcurrente;
using ListaGenerica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Hilos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // En el ejemplo HilosPOO, el método Run de cada Expositor no recibía parámetros.
            // De hecho, utilizábamos el propio objeto Expositor para encapsular los datos (valor y nVeces).
            // El/Los parámetro/s lo encapsulábamos como atributo/s de una clase.

            // A continuación veremos los casos desde un enfoque más funcional.

            //Thread recibe en su constructor cualquier Action con 0 o 1 parámetros de tipo object.

            Thread hiloParametrizado = new Thread(ObtenerDatos);

            //En el método Start pasamos el parámetro si es necesario.
            hiloParametrizado.Start("wwww.google.es");
            hiloParametrizado.Join();

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //También podríamos utilizar directamente una expresión lambda:
            Thread hiloSuelto = new Thread(
                     valor =>
                     {
                         Console.WriteLine("El hilo suelto recibe " + valor);
                     }
             );
            hiloSuelto.Start("Exprensión lambda");
            hiloSuelto.Join();

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //¿Y si necesitamos más parámetros? Variables libres.

            //Vamos a crear 4 hilos.
            //Cada hilo debería imprimir un par de valores: 40 y 41, 41 y 42, 42 y 43, 43 y 44...

            //¿Cómo arreglamos esto?

            Thread[] hilos = new Thread[4];
            int numero = 40;

            for (int i = 0; i < 4; i++)
            {
                int copia = numero; // OPCION 1 (variable)
                //Sin parámetro                
                hilos[i] = new Thread(
                    (parametro) => // OPCION 2 (Parametro)
                    //() => // OPCION normal y OPCION 1 (variable) y OPCION 3
                    {
                        //Console.WriteLine($"{numero} {numero + 1} "); // Normal
                        //Console.WriteLine($"{Interlocked.Add(ref numero, 0)} {Interlocked.Add(ref numero, 1)} "); // OPCION 3
                        //Console.WriteLine($"{copia} {copia + 1} "); //OPCION 1 (variable)
                        Console.WriteLine($"{parametro} "); //OPCION 2 (Parametro)
                    }
                    );
                //hilos[i].Start(); // OPCION 1 (variable)
                hilos[i].Start($"{numero} {numero + 1}"); // OPCION 2 (Parametros)
                numero++; // OPCION 1 y OPCION 2
            }

            //foreach (var hilo in hilos)
            //{
            //    hilo.Join();
            //}

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //Ejercicio: Empleando un enfoque funcional, impleméntese el ejercicio Expositor de HilosPOO.

            Console.WriteLine("Ejercicio Expositores con un enfoque funcional");
            Thread[] HilosExpositor = new Thread[4];
            for (int i = 0; i < HilosExpositor.Length; i++)
            {
                HilosExpositor[i] = new Thread(() => EjecutaRun(i + 1, i));
                HilosExpositor[i].Start();
            }

            foreach (var hilo in HilosExpositor)
            {
                hilo.Join();
            }

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // Ejercicio Expositor con un hilo y un Action. Se inicia en el start
            Thread hiloFuncional = new Thread(Run);

            //En el método Start pasamos un argumento (si es necesario).
            hiloFuncional.Start(1);

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // Ejercicio Expositor con array de hilos y Action. Se inicia en el start

            Thread[] hilosfuncAction = new Thread[4];

            for (int i = 0; i < 4; i++)
            {
                //¿Qué estamos enviando en el construtor de cada Thread?
                hilosfuncAction[i] = new Thread(Run);

                //Parámetros opcionales.
                hilosfuncAction[i].Name = "Hilo número: " + i; //Nombre del hilo.
                hilosfuncAction[i].Priority = ThreadPriority.Normal; //Prioridad
            }

            foreach (Thread hilo in hilosfuncAction)
            {
                //Iniciamos el hilo con el método Start de la clase Thread.
                hilo.Start(1);
                //hilo.Join();
            }

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // Ejercicio Expositor con un enfoque funcional. Se usan 4 hilos y parametro. Se inicia en el start
            Thread[] hilosfuncParametro = new Thread[4];

            for (int i = 0; i < 4; i++)
            {
                //¿Qué estamos enviando en el construtor de cada Thread?
                //hilosfunc[i] = new Thread(Run);
                hilosfuncParametro[i] = new Thread(
                     valor =>
                     {
                         Run(valor);
                     }
             );

                //Parámetros opcionales.
                hilosfuncParametro[i].Name = "Hilo número: " + i; //Nombre del hilo.
                hilosfuncParametro[i].Priority = ThreadPriority.Normal; //Prioridad
            }

            foreach (Thread hilo in hilosfuncParametro)
            {
                //Iniciamos el hilo con el método Start de la clase Thread.
                hilo.Start(1);
                //hilo.Join();
            }

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // Ejercicio: //Empleando el ienumerable de palabras clasifica, mediante la creacion manual de hilos, 
            //las palabras en tres colecciones distintas: 
            //    1)Palabras con todas sus letras mayusculas 
            //    2)PAlabras con todas sus letras minusculas 
            //    3)Palabras con letras tanto mayusculas como minusculas.
            //Finalmente comprueba mediante PLinq que cada solucion posee las palabras adecuadas

            var palabras = new string[] { "Hola", "adios", "BUENAS", "ok", "OKI", "Doki", "PEPE", "el", "hijo" };
            List<string> may = new List<string>();
            List<string> min = new List<string>();
            List<string> mix = new List<string>();
            Thread hiloMay = new Thread(() => Clasifica(palabras, may, Tipo.Mayusculas));
            Thread hiloMin = new Thread(() => Clasifica(palabras, min, Tipo.Minusculas));
            Thread hiloMix = new Thread(() => Clasifica(palabras, mix, Tipo.Mixto));
            hiloMay.Start();
            hiloMin.Start();
            hiloMix.Start();
            hiloMay.Join();
            hiloMin.Join();
            hiloMix.Join();

            //Comprobacion con PLINQ
            Console.WriteLine("Lista de mayusculas");
            may.ForEach(Console.WriteLine);
            Console.WriteLine("Lista de minusculas");
            min.ForEach(Console.WriteLine);
            Console.WriteLine("Lista de mayusculas y minusculas");
            mix.ForEach(Console.WriteLine);

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // Ejercicio:  crear dos hilosmanualmente que trabajen con el ienumerable de palabras
            // proporcionado. Ambos hilos haran uso de la misma variable compartida que hará de contador.
            // El primer hilo recorrera el ienumerable y restara 1 al contador por cada palabra de tamaño par.
            // El segundo hilo hara una operación similar, pero sumará 1 por cada palabra de tamaño impar que encuentre.
            // Imprimir mediante este procedimiento la diferencia entre el nº de palabras para y el impar
            int cont = 0;
            Thread hilo1 = new Thread(() => Operacion(palabras, ref cont, true));
            Thread hilo2 = new Thread(() => Operacion(palabras, ref cont, false));
            hilo1.Start();
            hilo2.Start();
            hilo1.Join();
            hilo2.Join();
            Console.WriteLine("La diferencia es: " + cont);

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //Ejercicio: igual que el anterior pero que sea apartir de un array de hilos

            // Crear un array de 4 hilos
            Thread[] hilosClasifica = new Thread[4];

            // Iniciar cada hilo en un bucle
            for (int i = 0; i < hilosClasifica.Length; i++)
            {
                hilosClasifica[i] = new Thread(() => ClasificarPalabras(palabras, i));
                hilosClasifica[i].Start();
            }

            // Esperar a que todos los hilos terminen
            foreach (var hilo in hilosClasifica)
            {
                hilo.Join();
            }

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //Ejercicio: Igual Igual que el de minusculas y mayus

            //string[] palabras = { "Hola", "adios", "BUENAS", "ok", "OKI", "Doki", "PEPE", "el", "hijo" };

            // Crear listas para almacenar las palabras clasificadas
            List<string> MAY = new List<string>();
            List<string> MIN = new List<string>();
            List<string> MIX = new List<string>();

            // Crear un array de hilos para clasificar las palabras
            Thread[] hilosBIS = new Thread[3];

            // Iniciar cada hilo en un bucle
            hilosBIS[0] = new Thread(() => Clasifica(palabras, MAY, Tipo.Mayusculas));
            hilosBIS[1] = new Thread(() => Clasifica(palabras, MIN, Tipo.Minusculas));
            hilosBIS[2] = new Thread(() => Clasifica(palabras, MIX, Tipo.Mixto));

            foreach (var hilo in hilosBIS)
            {
                hilo.Start();
            }

            // Esperar a que todos los hilos terminen
            foreach (var hilo in hilosBIS)
            {
                hilo.Join();
            }

            // Imprimir las palabras clasificadas
            Console.WriteLine("Lista de mayúsculas:");
            MAY.ForEach(Console.WriteLine);
            Console.WriteLine("Lista de minúsculas:");
            MIN.ForEach(Console.WriteLine);
            Console.WriteLine("Lista de mayúsculas y minúsculas:");
            MIX.ForEach(Console.WriteLine);

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // 1º Forma. Funcional con un Action directamente
            Thread hiloX = new Thread(() =>
            {
                Console.WriteLine("Hola");
            });
            hiloX.Start();

            // 2º Forma. Con un metodo quw recibe un parametro
            Thread hiloParam = new Thread(Metodo);
            hiloParam.Start("German");

            // 3º Forma. Con un Action quw recibe invoca un metodo que recibe varios parametros
            Thread hiloParam1 = new Thread(() =>
            {
                Metodo1("German", 13);
            });
            hiloParam1.Start();

            // 4º Forma. Con un metodo pasado por parametro, que 
            Thread hiloParam2 = new Thread(Metodo2);
            hiloParam2.Start(new Persona { nombre = "Jose", edad = 222 });

            // 5º Forma. Con tuplas
            Thread hiloParam3 = new Thread(() =>
            {
                Metodo3(("Cocacola", 233));
            });
            hiloParam3.Start();

            // 6º Forma. Con Diccionario
            Thread hiloParam4 = new Thread(() =>
            {
                Metodo4(new Dictionary<string, int>
                {
                    {"Numero",1 },
                    {"Clave",3 }
                });
            });
            hiloParam4.Start();

            // 7º Forma. Con arrays
            Thread hiloParam5 = new Thread(() =>
            {
                Metodo5(new object[]
                {
                    "Array",2
                });
            });
            hiloParam5.Start();


            // 8º Forma. Varios parametros
            string param1 = "German";
            int param2 = 12;
            string param3 = "c/Teodoro Cuesto, 25, 3º iz.";

            Thread hiloParam6 = new Thread(() =>
            {
                HazAlgo(param1, param2, param3);
            });
            hiloParam6.Start();

            // 9º Forma. Con parametro Action
            int num = 2;
            Thread hiloParam7 = new Thread((res) =>
            {
                Console.WriteLine("Multipicacion * 2 -> " + HazAlgo1(res));
            });
            hiloParam7.Start(num);

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // Ejercicio del contador con palabras pares e impares y MI Lista

            //string[] palabritas = { "Hola", "adios", "BUENAS", "ok", "OKI", "Doki", "PEPE", "el", "hijo" };
            Lista<string> pares = new();
            pares.Añadir("Hola");
            pares.Añadir("adios");
            pares.Añadir("BUENAS");
            pares.Añadir("ok");
            pares.Añadir("OKI");
            pares.Añadir("Doki");
            pares.Añadir("PEPE");
            pares.Añadir("el");
            pares.Añadir("hijo");
            int contador = 0;
            Thread hilosLista = new Thread(() => MPares(pares, ref contador, true));
            Thread hilosLista1 = new Thread(() => MPares(pares, ref contador, false));
            hilosLista.Start();
            hilosLista1.Start();
            hilosLista.Join();
            hilosLista1.Join();
            Console.WriteLine($"{contador}");

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // Ejercicio con mi cola y Clasificacion de palabras minusculas y mayusculas

            string[] palabritas = { "Hola", "adios", "BUENAS", "ok", "OKI", "Doki", "PEPE", "el", "hijo" };
            Cola<string> mi = new();
            Cola<string> ma = new();
            Cola<string> mx = new();

            Thread hilom = new Thread(() => MetodoPalabritas(palabritas, mi, Tipo.Minusculas));
            Thread hiloM = new Thread(() => MetodoPalabritas(palabritas, ma, Tipo.Mayusculas));
            Thread hilox = new Thread(() => MetodoPalabritas(palabritas, mx, Tipo.Mixto));

            hilom.Start();
            hiloM.Start();
            hilox.Start();
            
            hilom.Join();
            hiloM.Join();
            hilox.Join();

            Console.WriteLine(mi.ToString());
            Console.WriteLine(ma.ToString());
            Console.WriteLine(mx.ToString());

            var resMin = palabritas.AsParallel().Where(p => p == p.ToLower());
            Console.WriteLine("PLinq minus");
            foreach (var item in resMin)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine($"\nMisma cantidad de palabras? {resMin.Count() == mi.Count()}");

            var resMay = palabritas.AsParallel().Where(p => p == p.ToUpper());
            Console.WriteLine("PLinq mayus");
            foreach (var item in resMay)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine($"\nMisma cantidad de palabras? {resMay.Count() == ma.Count()}");

            var resMix = palabritas.AsParallel().Where(p => p != p.ToLower() && p != p.ToUpper());
            Console.WriteLine("PLinq mix");
            foreach (var item in resMix)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine($"\nMisma cantidad de palabras? {resMix.Count() == mx.Count()}");

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // EJercicio que crea un hilo que realiza la cuenta de 10 numeros seguidos desde un entero que pasamos

            Thread t = new Thread(Show10Numbers);
            t.Start(7);

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // ejercicio que comparte variables, otro hace una copia y otro pasa parametod
            SharedBoundVariables();
            MaingACopy();
            WithParameters();
        }

        static int global = 1;

        static void SharedBoundVariables()
        {
            int local = global = 1;
            Thread thread1 = new Thread(() => {
                Console.WriteLine("Thread 1. Global {0}, Local {1}.",
                        global, local);
            });
            global = local = 2;
            Thread thread2 = new Thread(() => {
                Console.WriteLine("Thread 2. Global {0}, Local {1}.",
                        global, local);
            });
            thread1.Start();
            thread2.Start();
        }

        static void MaingACopy()
        {
            int local = 1;
            int copy = local;
            Thread thread = new Thread(() => {
                Console.WriteLine("Making a copy {0}.", copy);
            });
            local = 2;
            thread.Start();
        }

        static void WithParameters()
        {
            int local = 1;
            Thread thread = new Thread((parameter) => {
                Console.WriteLine("With parameter {0}.", parameter);
            });
            local = 2;
            thread.Start(local - 1);
        }

        static void Show10Numbers(object from)
        {
            int? fromInt = from as int?;
            if (!fromInt.HasValue)
                throw new ArgumentException("The parameter \"from\" must be an integer");
            for (int i = fromInt.Value; i < 10 + fromInt; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000); // Sleeps one second
            }
        }

        private static void MetodoPalabritas(string[] palabritas, Cola<string> col, Tipo tipo)
        {
            foreach(var i in palabritas)
            {
                switch(tipo)
                {
                    case Tipo.Minusculas:
                        if (i.All(Char.IsLower))
                        {
                            col.Añadir(i);
                        }
                        break;
                    case Tipo.Mayusculas:
                        if (i.All(Char.IsUpper))
                        {
                            col.Añadir(i);
                        }
                        break;
                    case Tipo.Mixto:
                        if(!i.All(Char.IsLower) && !i.All(Char.IsUpper)){
                            col.Añadir(i);
                        }
                        break;
                }
            }
        }

        private static void MPares(Lista<string> pares, ref int contador, bool par)
        {
            foreach(var i in pares)
            {
                if(i.Length % 2 == 0 && par == true)
                {
                    contador--;
                }
                else if((i.Length % 2 != 0 && par == false))
                {
                    contador++;
                }
            }
        }

        static void ClasificarPalabras(string[] palabras, int indice)
        {
            // Determinar si el índice es par o impar
            bool esPar = indice % 2 == 0;

            // Clasificar las palabras según si son pares o no
            foreach (var palabra in palabras)
            {
                if ((palabra.Length % 2 == 0 && esPar) || (palabra.Length % 2 != 0 && !esPar))
                {
                    Console.WriteLine($"Hilo {Thread.CurrentThread.ManagedThreadId}: {palabra} es {(esPar ? "par" : "impar")}");
                }
            }
        }

        private static void Operacion(string[] palabras, ref int cont, bool esPar)
        {
            foreach (var i in palabras)
            {
                if (i.Length % 2 == 0 && esPar)
                {
                    cont--;
                }
                else if (i.Length % 2 != 0 && !esPar)
                {
                    cont++;
                }
            }
        }

        private static void Clasifica(string[] palabras, List<string> lista, Tipo tipo)
        {
            foreach (var i in palabras)
            {
                switch (tipo)
                {
                    case Tipo.Mayusculas:
                        if (i.All(Char.IsUpper))
                        {
                            lista.Add(i);
                        }
                        break;
                    case Tipo.Minusculas:
                        if (i.All(Char.IsLower))
                        {
                            lista.Add(i);
                        }
                        break;
                    case Tipo.Mixto:
                        if (!i.All(Char.IsUpper) && !i.All(Char.IsLower))
                        {
                            lista.Add(i);
                        }
                        break;
                }
            }
        }

        public enum Tipo { Mayusculas, Minusculas, Mixto }
        public static void ObtenerDatos(object valor)
        {
            Console.WriteLine("Obteniendo datos del destino {0}", valor);
            //Simulamos carga de trabajo, fines demostrativos.
            Thread.Sleep(2000);
            Console.WriteLine("Datos obtenidos y almacenados");

        }

        public static void EjecutaRun<T>(int _nVeces, T _objeto)
        {
            int i = 0;
            //¿Qué indica Thread.CurrentThread.ManagedThreadId? El identificador del hilo
            Console.WriteLine("Comienza el hilo ID={0} que escribirá {1} veces cada 2000ms.", Thread.CurrentThread.ManagedThreadId, _nVeces);

            while (i < _nVeces)
            {
                //Dormimos el proceso durante 2000 milisegundos
                //¿Para qué es útil? No para sincronizar. simular si. Nunca para esperar que otro hilo haga algo
                Thread.Sleep(2000);
                Console.WriteLine("Ejecución {0} del hilo [ID={1}; NOMBRE={2}; PRIORIDAD={3}] con valor {4}",
                    i, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name, Thread.CurrentThread.Priority, _objeto);
                i++;
            }
        }

        static int _nVeces = 4;
        public static void Run<T>(T _objeto)
        {
            int i = 0;
            int? ob = _objeto as int?;
            //¿Qué indica Thread.CurrentThread.ManagedThreadId?
            Console.WriteLine("Comienza el hilo ID={0} que escribirá {1} veces cada 2000ms. (mínimo)", Thread.CurrentThread.ManagedThreadId, _nVeces);

            while (i < _nVeces)
            {
                //Dormimos el proceso durante 2000 milisegundos
                //¿Para qué es útil? ¿Y para qué no debe utilizarse?
                Thread.Sleep(2000);
                Console.WriteLine("Ejecución {0} del hilo [ID={1}; NOMBRE={2}; PRIORIDAD={3}] con valor {4}",
                    i, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name, Thread.CurrentThread.Priority, ob);
                i++;
                ob++;

            }
            Console.WriteLine($"Fin del hilo ID = {Thread.CurrentThread.ManagedThreadId}.");
        }

        public static void Metodo(object palabra)
        {
            Console.WriteLine($"Palabra: {palabra}");
        }

        public static void Metodo1(String palabra, int numero)
        {
            Console.WriteLine($"Palabra: {palabra}| Numero: {numero}| Letra: {palabra[0]}");
        }

        public static void Metodo2(object persona)
        {
            Persona person = (Persona)persona;
            Console.WriteLine($"Nombre: {person.nombre}| Edad: {person.edad}");
        }

        public static void Metodo3((string, int) tupla)
        {
            Console.WriteLine($"Palabra: {tupla.Item1}| Int: {tupla.Item2}");
        }

        public static void Metodo4(Dictionary<string, int> dic)
        {
            Console.WriteLine($"Numero: {dic["Numero"]}| Int: {dic["Clave"]}");
        }

        public static void Metodo5(object[] arr)
        {
            string str = (string)arr[0];
            int entero = (int)arr[1];
            Console.WriteLine($"String: {str} y entero: {entero}");
        }

        public static void HazAlgo(string nombre, int edad, string domicilio)
        {
            Console.WriteLine($"Nombre:{nombre} + Edad:{edad} + Domicilio:{domicilio}");
        }

        public static int HazAlgo1(object numero)
        {
            return (int)numero * 2;
        }
    }

    public class Persona
    {
        public string nombre { get; set; }
        public int edad { get; set; }
    }
}


