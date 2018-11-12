using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2_Analisis
{
    public class Metodos
    {
        //crea las variables de mediciones
        int a = 0, c = 0;
        //crea el random
        Random r = new Random();
        //Lista con los objetos

        public List<Disenador> disenadores { get; set; }
        public List<Ubicacion> ubicaciones { get; set; }
        public List<Seccion> secciones { get; set; }
        public List<Trabajo> trabajos { get; set; }

        //datos usados para generarlos aleatoriamente
        //datos disenador
        static List<String> nombres = new List<String> { "Angel", "Jason", "Marco", "Pedro", "Pablo", "Luis", "Jessica", "Sarah", "Raquel", "Pepe" };
        static List<int> repeticiones = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }; //TRABAJA EN PARALELO CON nombres PARA AGREGARLES UN NUMERO.
        static List<int> salarios = new List<int> { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };

        //datos Ubicacion

        //datos seccion
        static List<int> precios = new List<int> { 500, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000 };
        static List<int> horarios = new List<int> { 1, 2, 3, 4 };

        //datos de horarios
        static List<int> diurnos = new List<int> { 1, 2, 0 };
        static List<int> nocturnos = new List<int> { 3, 4, 0 };

        public Metodos()
        {
            disenadores = new List<Disenador>();
            ubicaciones = new List<Ubicacion>();
            secciones = new List<Seccion>();
            trabajos = new List<Trabajo>();
        }

        //Metodos
        public void generadorDisenador(int cant)
        {
            Random r = new Random();

            for (int i = 1; i <= cant; i++)
            {
                int n = r.Next(0, 10);//NOMBRE
                int s = r.Next(0, 10);//SALARIO
                int diu = r.Next(0, 3);//DIURNO
                int noc = r.Next(0, 3);//NOCTURNO
                disenadores.Add(new Disenador(i, (nombres[n] + repeticiones[n]), salarios[s], diurnos[diu], nocturnos[noc]));
                repeticiones[n] += 1;
            }

        }
        public void imprimirD()
        {
            for (int i = 0; i < disenadores.Count; i++)
            {
                Console.WriteLine("Nombre: " + disenadores[i].Nombre + " ID: " + disenadores[i].Id + " Salario:" + disenadores[i].Salario + " Diurno:" + disenadores[i].Diurno + " Nocturno:" + disenadores[i].Nocturno);
            }
        }

        public void generadorUbicacion(int cant)
        {
            Random r = new Random();

            for (int i = 1; i <= cant; i++)
            {
                int diu = r.Next(0, 3);//DIURNO
                int noc = r.Next(0, 3);//NOCTURNO
                ubicaciones.Add(new Ubicacion(i, "Piso " + i, diurnos[diu], nocturnos[noc]));
            }

        }
        public void imprimirU()
        {
            for (int i = 0; i < ubicaciones.Count; i++)
            {
                Console.WriteLine("Nombre: " + ubicaciones[i].Nombre + " ID: " + ubicaciones[i].Id + " Diurno:" + ubicaciones[i].Diurno + " Nocturno:" + ubicaciones[i].Nocturno);
            }
        }

        
        public void generadorSeccion(int cant)
        {
            Random r = new Random();

            for (int i = 1; i <= cant; i++)
            {
                int p = r.Next(0, 3);//PRECIO
                int h = r.Next(0, 4);//HORARIO
                secciones.Add(new Seccion(i, precios[p], horarios[h]));
            }

        }

      
        public void imprimirS()
        {
            for (int i = 0; i < secciones.Count; i++)
            {
                Console.WriteLine("Nombre: " + secciones[i].Nombre + " ID: " + secciones[i].Id + " Precio:" + secciones[i].Precio + " Horario:" + secciones[i].Horario);
            }
        }

        public void generadorTrabajos(int cant)
        {
            Random r = new Random();

            for (int i = 1; i <= cant; i++)
            {
                List<Disenador> d = new List<Disenador>();

                int s = r.Next(0, secciones.Count); // indice sesion  
                int u = r.Next(0, ubicaciones.Count);//UBICACION

                int horario = secciones[s].Horario;  // horario tiene el horario de la sesion que salio al azar
                int cantDisenadores = r.Next(1, 6);
                for (int j = 0; j < cantDisenadores; j++)
                {
                    int indDisenador = r.Next(0, disenadores.Count);//genera random el disenador
                    if (horario == 1)  // 7am a 4pm
                    {
                        if ((disenadores[indDisenador].Diurno == 1) || (disenadores[indDisenador].Diurno == 2))
                        {
                            d.Add(disenadores[indDisenador]);
                        }
                        else
                        {
                            j--;
                        }
                    }
                    else if (horario == 2) // 7am a 11am
                    {
                        if (disenadores[indDisenador].Diurno == 2)
                        {
                            d.Add(disenadores[indDisenador]);
                        }
                        else
                        {
                            j--;
                        }
                    }
                    else if (horario == 3)  // 7pm a 4am
                    {
                        if (disenadores[indDisenador].Nocturno == 3 || disenadores[indDisenador].Nocturno == 4)
                        {
                            d.Add(disenadores[indDisenador]);
                        }
                        else
                        {
                            j--;
                        }

                    }
                    else if (horario == 4)  // 7:00pm a 11:00pm
                    {
                        if (disenadores[indDisenador].Nocturno == 4)
                        {
                            d.Add(disenadores[indDisenador]);
                        }
                        else
                        {
                            j--;
                        }
                    }

                }
                trabajos.Add(new Trabajo(i, d, ubicaciones[u], secciones[s]));

            }

        }
        public void imprimirT(List<Trabajo> trabajos)
        {
            foreach (Trabajo reco in trabajos)//recorre los trabajos
            {
                Console.WriteLine("Id: " + reco.Id + " PrecioTotal: " + precioTotal(reco));
                Console.WriteLine("     Seccion:" + reco.Seccion.Nombre + " Seccion Horario:" + reco.Seccion.Horario + " Precio:" + reco.Seccion.Precio);
                Console.WriteLine("          Ubicacion:" + reco.Ubicacion.Nombre + " Diurno:" + reco.Ubicacion.Diurno + " Nocturno:" + reco.Ubicacion.Nocturno);
                foreach (Disenador d in reco.Disenadores)
                {
                    Console.WriteLine("              Disenador:" + d.Nombre + " Nocturno:" + d.Nocturno + " Diurno:" + d.Diurno + " Salario" + d.Salario);
                    Console.WriteLine("");
                }
            }
        }
        public void imprimirT()
        {
            foreach (Trabajo reco in trabajos)//recorre los trabajos
            {
                Console.WriteLine("Id: " + reco.Id + " PrecioTotal: " + precioTotal(reco));
                Console.WriteLine("     Seccion:" + reco.Seccion.Nombre + " Seccion Horario:" + reco.Seccion.Horario + " Precio:" + reco.Seccion.Precio);
                Console.WriteLine("          Ubicacion:" + reco.Ubicacion.Nombre + " Diurno:" + reco.Ubicacion.Diurno + " Nocturno:" + reco.Ubicacion.Nocturno);
                foreach (Disenador d in reco.Disenadores)
                {
                    Console.WriteLine("              Disenador:" + d.Nombre + " Nocturno:" + d.Nocturno + " Diurno:" + d.Diurno + " Salario" + d.Salario);
                }
            }
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

        public List<Trabajo> RamificacionyPoda(Trabajo t)
        {
            var tiempo = Stopwatch.StartNew();
            int a = 0, c = 0;
            Trabajo anterior = new Trabajo();
            List<Trabajo> trabajos = new List<Trabajo>();
            Trabajo aux = t;a++;
            while (aux != null)
            {
                c++;
                trabajos.Add(aux);
                anterior = aux;
                aux = aux.izq;
                a += 3;
            }
            c++;

            Console.WriteLine("**Algoritmo de Ramificacion y Poda**");
            Console.WriteLine("Asignaciones" + a);
            Console.WriteLine("Comparaciones " + c);
            Console.WriteLine("Tiempo "+ tiempo.Elapsed.TotalMilliseconds);
            return trabajos;
        }


        public List<Trabajo> Mutacion(int n, Trabajo raiz)
        {
            var tiempo = Stopwatch.StartNew();
            Trabajo Padre1;
            Trabajo Padre2;
            Trabajo temp = raiz, anterior, preanterior;a++;
            preanterior = temp;a++;
            temp = temp.izq;a++;
            anterior = temp;a++;
            temp = temp.izq;a++;
            while (temp != null) {
                c++;
                preanterior = anterior;a++;
                anterior = temp;a++;
                temp = temp.izq;a++;
            }
            c++;
            if (anterior.der != null) {
                c++;
                preanterior = anterior.der;a++;
            }
            Padre1 = anterior;a++;
            Padre2 = preanterior;a++;

            // ciclo para encontar al menor o sea al primer padre 

          

            List<Trabajo> padres = new List<Trabajo> {Padre1,Padre2};
            a++;
            for (int i = 0; i < n; i++) {// for para hacer las n generaciones 
                c++;
                Console.WriteLine("Generacion: " + i);
                padres = mutar(padres);
                a++;
            }
            c++;
            Console.WriteLine("Asignaciones: " +a);
            Console.WriteLine("Comparaciones: " + c);
            Console.WriteLine("Tiempo " + tiempo.Elapsed.TotalMilliseconds);
            return padres;
        }

        public List<Trabajo> mutar(List<Trabajo> padres) {
            Trabajo t1 = creaTrabajo(padres);
            Trabajo t2 = creaTrabajo(padres);
            padres.Add(t1);a++;
            padres.Add(t2);a++;
            // impresion de los dos padres y los dos hijos
            imprimirT(padres);

            List<int> precioTrabajo = new List<int> { precioTotal(padres[0]), precioTotal(padres[1]), precioTotal(t1), precioTotal(t2) };a++;
            precioTrabajo.Sort();a++;

            

            List<Trabajo> mejoresDeLaGen = new List<Trabajo>();
            a++;
            foreach (Trabajo t in padres) {
                c++;
                if (precioTotal(t) == precioTrabajo[0] || precioTotal(t) == precioTrabajo[1])
                {
                    c++;
                    mejoresDeLaGen.Add(t);a++;
                    if (mejoresDeLaGen.Count() == 2) {
                        c++;
                        break;
                    }
                }
                a++;
            }
            c++;
            Console.WriteLine("Los mejores de la generacion fueron: ");
            imprimirT(mejoresDeLaGen);
            return mejoresDeLaGen;





        }
        public Trabajo creaTrabajo(List<Trabajo> padres)
        {
            
            int s = r.Next(0, 2);//NUMERO RANDOM PARA SECCION
            int u = r.Next(0, 2);//NUMERO RANDOM PARA UBICACION
            int dis = r.Next(0, 2); //NUMERO RANDOM PARA CANTIDAD DE DISENADORES
            a += 3;
            //nuevo trabajo
            Trabajo t1 = new Trabajo();

            // se agrega seccion y ubicacion y la cantidad de jugadores de cada padre random
            t1.Seccion = padres[s].Seccion;
            t1.Ubicacion = padres[u].Ubicacion;
            int cantDisenadores = padres[dis].Disenadores.Count();
            a += 3;

            int horario = t1.Seccion.Horario;a++;//horario predefido para disenadores
            List<Disenador> d = new List<Disenador>();//lista donde se guardan los disenadores del nuevo trabajo


            a++;
            for (int j = 0; j < cantDisenadores; j++)
            {
                c++;
                int indDisenador = r.Next(0, disenadores.Count);//genera random el disenador
                if (horario == 1)  // 7am a 4pm
                {
                    c++;
                    if ((disenadores[indDisenador].Diurno == 1) || (disenadores[indDisenador].Diurno == 2))
                    {
                        c++;
                        d.Add(disenadores[indDisenador]);a++;
                    }
                    else
                    {
                        c++;
                        j--;a++;
                    }
                }
                else if (horario == 2) // 7am a 11am
                {
                    c++;
                    if (disenadores[indDisenador].Diurno == 2)
                    {
                        c++;
                        d.Add(disenadores[indDisenador]);a++;
                    }
                    else
                    {
                        c++;
                        j--;a++;
                    }
                }
                else if (horario == 3)  // 7pm a 4am
                {
                    c++;
                    if (disenadores[indDisenador].Nocturno == 3 || disenadores[indDisenador].Nocturno == 4)
                    {
                        c++;
                        d.Add(disenadores[indDisenador]);a++;
                    }
                    else
                    {
                        c++;
                        j--;a++;
                    }

                }
                else if (horario == 4)  // 7:00pm a 11:00pm
                {
                    c++;
                    if (disenadores[indDisenador].Nocturno == 4)
                    {
                        c++;
                        d.Add(disenadores[indDisenador]);a++;
                    }
                    else
                    {
                        c++;
                        j--;a++;
                    }
                }

            }
            c++;
            t1.Disenadores = d;a++;
            return t1;
        }


    }
}
