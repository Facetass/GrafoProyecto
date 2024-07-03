using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGrafo
{
    public class NodoLista
    {
        //posicion del vertice en la lista de adyacencia a la que tiene un enlace
        //no se pone cero porque existe el vertice en la posicon [0]
        public int vertexNum = 0;


        //costo para llevar a ese vertice
        public float costo { get; set; }

        //enlace de lista ligada
        public NodoLista next = null;

        public bool visitado { get; set; } = false;
        public float costoAcumulado { get; set; } = float.MaxValue; // Inicializar en infinito
    }
}
