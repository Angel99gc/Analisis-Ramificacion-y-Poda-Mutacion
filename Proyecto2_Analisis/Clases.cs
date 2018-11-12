using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2_Analisis
{
    public class ArbolBinarioOrdenado
    {
        
        public Trabajo raiz;
        static int cant;
        private int altura;

        public ArbolBinarioOrdenado()
        {
            raiz = null;
        }

        public int precioTotal(Trabajo T)
        {
            int total = 0;
            total += T.Seccion.Precio;
            foreach (Disenador d in T.Disenadores)
            {
                total += d.Salario;
            }
            return total;
            
        }

        public void Insertar(Trabajo T)
        {
            if (!Existe(T))
            {
                
                if (raiz == null)
                    raiz = T;
                else
                {
                    Trabajo anterior = null, reco;
                    reco = raiz;
                    while (reco != null)
                    {
                        anterior = reco;
                        if (precioTotal(T) < precioTotal(reco))
                            reco = reco.izq;
                        else
                            reco = reco.der;
                    }
                    if (precioTotal(T) < precioTotal(anterior))
                        anterior.izq = T;
                    else
                        anterior.der = T;
                }
            }
        }

        public bool Existe(Trabajo T)
        {

            Trabajo reco = raiz;
            while (reco != null)
            {
                if (T.Id == reco.Id)
                    return true;
                else
                    if (T.Id > reco.Id)
                    reco = reco.der;
                else
                    reco = reco.izq;
            }
            return false;
        }
        public void insertarTrabajos(List<Trabajo> trabajos) {
            foreach (Trabajo t in trabajos) {
                Insertar(t);
            }
        }

        private void ImprimirEntre(Trabajo reco)
        {
            if (reco != null)
            {
                ImprimirEntre(reco.izq);
                Console.WriteLine("Id: "+reco.Id+" PrecioTotal: "+ precioTotal(reco) );
                Console.WriteLine("     Seccion:" + reco.Seccion.Nombre + " Seccion Horario:" + reco.Seccion.Horario + " Precio:" + reco.Seccion.Precio);
                Console.WriteLine("          Ubicacion:" + reco.Ubicacion.Nombre + " Diurno:" + reco.Ubicacion.Diurno + " Nocturno:" + reco.Ubicacion.Nocturno);
                foreach (Disenador d in reco.Disenadores) {
                    Console.WriteLine("              Disenador:" + d.Nombre + " Nocturno:" + d.Nocturno + " Diurno:" + d.Diurno + " Salario" + d.Salario);
                }
                ImprimirEntre(reco.der);
            }
        }

        public void ImprimirEntre()
        {
            ImprimirEntre(raiz);
            Console.WriteLine();
        }


        private void Cantidad(Trabajo reco)
        {
            if (reco != null)
            {
                cant++;
                Cantidad(reco.izq);
                Cantidad(reco.der);
            }
        }

        public int Cantidad()
        {
            cant = 0;
            Cantidad(raiz);
            return cant;
        }

        private void CantidadNodosHoja(Trabajo reco)
        {
            if (reco != null)
            {
                if (reco.izq == null && reco.der == null)
                    cant++;
                CantidadNodosHoja(reco.izq);
                CantidadNodosHoja(reco.der);
            }
        }

        public int CantidadNodosHoja()
        {
            cant = 0;
            CantidadNodosHoja(raiz);
            return cant;
        }

   


        private void RetornarAltura(Trabajo reco, int nivel)
        {
            if (reco != null)
            {
                RetornarAltura(reco.izq, nivel + 1);
                if (nivel > altura)
                    altura = nivel;
                RetornarAltura(reco.der, nivel + 1);
            }
        }

        public int RetornarAltura()
        {
            altura = 0;
            RetornarAltura(raiz, 1);
            return altura;
        }

        public void MayorValorl()
        {
            if (raiz != null)
            {
                Trabajo anterior = new Trabajo();
                Trabajo reco = raiz;
                while (reco.der != null) { 
                    anterior = reco;
                reco = reco.der;
                }
                Console.WriteLine("Mayor valor del árbol:" + precioTotal(anterior));
            }
        }

        public void BorrarMenor()
        {
            if (raiz != null)
            {
                if (raiz.izq == null)
                    raiz = raiz.der;
                else
                {
                    Trabajo atras = raiz;
                    Trabajo reco = raiz.izq;
                    while (reco.izq != null)
                    {
                        atras = reco;
                        reco = reco.izq;
                    }
                    atras.izq = reco.der;
                }
            }
        }
    }
        public class Disenador
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public int Salario { get; set; }
        public int Diurno { get; set; }
        public int Nocturno { get; set; }

        public Disenador(int id, String nombre, int salario, int diurno, int nocturno)
        {
            Id = id;
            Nombre = nombre;
            Salario = salario;
            Diurno = diurno;
            Nocturno = nocturno;
        }
    }
    public class Ubicacion
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public int Diurno { get; set; }
        public int Nocturno { get; set; }

        public Ubicacion(int id, String nombre, int diurno, int nocturno)
        {
            Id = id;
            Nombre = nombre;
            Diurno = diurno;
            Nocturno = nocturno;
        }

    }
    public class Seccion
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public int Precio { get; set; }
        public int Horario { get; set; }

        public Seccion(int id, int precio, int horario)
        {
            Id = id;
            Precio = precio;
            Horario = horario;
            if (horario == 1)
                Nombre = "7am a 4pm";
            else if (horario == 2)
                Nombre = "7am a 11am";
            else if (horario == 3)
                Nombre = "7pm a 4am";
            else
                Nombre = "7pm a 11pm";
        }
    }
    public class Trabajo
    {
        public int Id { get; set; }
        public List<Disenador> Disenadores { get; set; }
        public Ubicacion Ubicacion { get; set; }
        public Seccion Seccion { get; set; }
        public Trabajo izq { get; set; }
        public Trabajo der { get; set; }

        public Trabajo() {
            Id = 0;
            Disenadores = new List<Disenador>();
            Ubicacion = null;
            Seccion = null;
            izq = null;
            der = null;
        }
        public Trabajo(int id, List<Disenador> disenadores, Ubicacion ubicacion, Seccion seccion)
        {
            Id = id;
            Disenadores = disenadores;
            Ubicacion = ubicacion;
            Seccion = seccion;
            izq = null;
            der = null;
        }
    }
}
