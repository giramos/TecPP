using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Factoria
    {
        public static Persona[] CrearPersonas()
        {
            string[] nombres = { "María", "Juan", "Pepe", "Miguel", "Miguel", "Miguel", "Cristina", "Luis", "Carlos", "Miguel", "Cristina", "María", "Juan" };
            string[] apellidos1 = { "Díaz", "Pérez", "Hevia", "García", "Rodríguez", "Pérez", "Sánchez", "Díaz", "Hevia", "de Paul", "Resurrección", "Hermoso", "Mendez" };
            string[] nifs = { "9876384A", "103478387F", "23476293R", "4837649A", "67365498B", "98673645T", "56837645F", "87666354D", "9376384K", "10007166A", "77791230K", "54367892M", "43212510A" };
            int[] edades = { 15, 26, 37, 18, 39, 60, 21, 72, 23, 43, 29, 63, 30 };
            Persona[] personas = new Persona[nombres.Length];
            for (int i = 0; i < personas.Length; i++)
                personas[i] = new Persona(nombres[i], apellidos1[i], nifs[i], edades[i]);
            return personas;
        }

        public static Angulo[] CrearAngulos()
        {
            Angulo[] angulos = new Angulo[361];
            for (int angulo = 0; angulo <= 360; angulo++)
                angulos[angulo] = new Angulo(angulo / 180.0 * Math.PI);
            return angulos;
        }

        public static Libro[] CrearLibros()
        {
            string[] titulo = { "Biblia", "Don Juan Tenorio", "Pepe y la mascota", "Luis el Taxista", "Carlos III: El imperio", "Miguelin y el hormiguero", "Cristina F", "María de la O", "Juan Bautista" };
            string[] autor = { "Jose Díaz", "Pérez Reverte", "Carlos Hevia", "Juan García", "Omar Rodríguez", "Pilar Pérez", "David Sánchez", "Miriam Díaz", "Nuno Hevia" };
            int[] npa = { 100, 456, 87, 377, 1009, 745, 70, 144, 938 };
            int[] año = { 2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023 };
            string[] genero = { "Drama", "Romantica", "Crimen", "Aventuras", "Accion", "Accion", "Drama", "Accion", "Misterio" };
            Libro[] libros = new Libro[titulo.Length];
            for (int i = 0; i < libros.Length; i++)
                libros[i] = new Libro(titulo[i], autor[i], npa[i], año[i], genero[i]);
            return libros;
        }
    }
}
