using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2_Analisis
{
    
    class Program
    {

        


        static void Main(string[] args)
        {
            Metodos met = new Metodos();
            ArbolBinarioOrdenado abo = new ArbolBinarioOrdenado();
            met.generadorDisenador(20);
            met.imprimirD();
            met.generadorUbicacion(20);
            met.imprimirU();
            met.generadorSeccion(20);
            met.imprimirS();
            met.generadorTrabajos(10);  // cantidad de nodos para el arbol
            met.imprimirT();
            abo.insertarTrabajos(met.trabajos);
            abo.ImprimirEntre();
 
            List<Trabajo> trabajos = met.RamificacionyPoda(abo.raiz);
            Console.WriteLine();
            Console.WriteLine("ES RAMIFICACION Y PODA");
            met.imprimirT(trabajos);
            Console.WriteLine();
            Console.WriteLine("ESTAS SON LAS MUTACIONES:");
            List<Trabajo> baratos = met.Mutacion(1000,abo.raiz);

            Console.ReadLine();
        }
        
    }
}
