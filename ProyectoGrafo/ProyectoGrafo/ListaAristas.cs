using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGrafo
{
    public class ListaAristas
    {
        internal NodoLista inicio = null;
        private int contElemnts = 0;

        public string InsertaObjeto1(int numV, float cost)
        {
            string mensaje = "";
            NodoLista nuevo = new NodoLista();
            nuevo.vertexNum = numV;
            nuevo.costo = cost;
            if (inicio == null)
            { // no había objetos en la colección
              // es el primero
                inicio = nuevo;
                contElemnts++;
                mensaje = "Primer elemento de la colección";
            }
            else
            { // ya hay objetos en la colección, no se cuantos
              // hay que recorrer esos objetos y dejar una referencia
              // en el último objeto
                NodoLista t = null;
                t = inicio;
                while (t.next != null)
                {
                    t = t.next;
                }
                //cuando llego aquí estoy seguro que t está en el
                //último
                t.next = nuevo;
                contElemnts++;
                mensaje = "Ya no es el primer Elemento";
            }
            return mensaje;
        }
        public List<NodoLista> ObtenerAristas()
        {
            List<NodoLista> aristas = new List<NodoLista>();
            NodoLista z = inicio;
            while (z != null)
            {
                aristas.Add(z);
                z = z.next;
            }
            return aristas;
        }



        public string[] MostrarDatosColeccion()
        {
            string[] cads = new string[contElemnts];
            NodoLista z = null;
            z = inicio;
            int w = 0; // para la localidad del arreglo
            while (z != null)
            {
                cads[w] = $"{z.vertexNum},{z.costo}";
                z = z.next;
                w++;
            }

            return cads;
        }

        public float devolvercosto(int arista)
        {
            NodoLista temp = inicio;

            while(temp != null)
            {
                if (temp.vertexNum == arista)
                {
                    return temp.costo;
                }

                temp = temp.next;
            }

            throw new ArgumentException("El nodo No EXISTE");
        }

        public int[] ObtenerPosi()
        {
            int[] cads = new int[contElemnts];
            int numarista = 0;
            NodoLista z = null;
            z = inicio;
            int w = 0; // para la localidad del arreglo
            while (z != null)
            {
                cads[w] = z.vertexNum;
                z = z.next;
                w++;
                numarista++;
            }

            return cads;
        }

    }
}

