using System;

namespace LambdaSintaxis
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Las expresiones lambda nos permiten declarar métodos/funciones como expresiones.
            *Basándonos en el método Concatenar anterior:
            * 
            * public static string Concatenar(char a, char b) { return String.Format("{0}{1}",a,b);}
            * 
            * 1. Eliminamos hasta el primer paréntesis:
            *    (char a, char b) { return String.Format("{0}{1}",a,b);}
            * 
            * 2. Añadimos el operador => entre los parámetros y el cuerpo:
            *    (char a, char b) => { return String.Format("{0}{1}", a, b); }
            *    
            * 3. Declaramos la expresión con un delegado genérico basándonos en los parámetros y el retorno:*/

            Func<char, char, string> operación = (char a, char b) => { return String.Format("{0}{1}", a, b); };

            // Si la expresisón tiene una única sentencia y es de retorno, podemos eliminar los {} el ; y el return

            Func<char, char, string> operaciónLimpia = (char a, char b) => String.Format("{0}{1}", a, b);

            //Omitiendo tipos:
            operaciónLimpia = (a, b) => String.Format("{0}{1}", a, b);

            Console.WriteLine(operaciónLimpia('l', 'a'));

            // Si no tuviéramos parámetros, usaríamos ()
            //Cuando tenemos un único parámetro, no hace falta el paréntesis:


            Predicate<int> esPar = n => n % 2 == 0;
            Action<string> mostrar;
            int numero = 3;
            string texto = " Si este texto es rojo, " + numero + " es par";
            if (esPar(numero))
            {
                mostrar = (cadena) =>
                {
                    ConsoleColor color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(cadena);
                    Console.ForegroundColor = color;
                }; //Ojo el ;
            }
            else
                mostrar = Console.WriteLine;


            //Por tanto, podemos transformar funciones anteriores a expresiones lambda:

            Func<Func<int, int>, int, int> lambdaDoble = (f, n) => f(f(n));
            //Le pasamos una expresión Lambda como función (f) y un entero (n).
            Console.WriteLine(lambdaDoble(n => n + n, 3));
        }
    }
}
