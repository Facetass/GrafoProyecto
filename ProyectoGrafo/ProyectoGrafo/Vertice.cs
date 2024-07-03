using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGrafo
{
    public class Vertice
    {
        public Ciudad info = null;
        public ListaAristas ListaEnlaces = new ListaAristas();

        public Vertice(Ciudad datos)
        {
            info = datos;
        }

        public string AgregarArista(int numV2, float cost2)
        {
            return ListaEnlaces.InsertaObjeto1(numV2, cost2);
        }

        public string[] MuesraAristas()
        {
            return ListaEnlaces.MostrarDatosColeccion();
        }

        public string infoCiudad()
        {
            return info.NomCiudad;
        }

        public int[] ObternerPosicion()
        {
            return ListaEnlaces.ObtenerPosi();
        }

        public float DevolverCosto(int aristapos)
        {
            return ListaEnlaces.devolvercosto(aristapos);
        }
    }
}
