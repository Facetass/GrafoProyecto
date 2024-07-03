using System;
using System.Collections.Generic;

namespace ProyectoGrafo
{
    public class Grafo
    {
        public List<Vertice> ListaAdyc = new List<Vertice>();

        public string AgregarVertice(Ciudad nuevaCiudad)
        {
            ListaAdyc.Add(new Vertice(nuevaCiudad));
            return "Nuevo vértice creado";
        }

        public string AgregarArista(string CiudadOrigen, string CiudadDestino, int costo)
        {
            int indiceOrigen = ObtenerIndiceCiudad(CiudadOrigen);
            int indiceDestino = ObtenerIndiceCiudad(CiudadDestino);

            if (indiceOrigen != -1 && indiceDestino != -1)
            {
                ListaAdyc[indiceOrigen].AgregarArista(indiceDestino, costo);
                return "Arista agregada correctamente";
            }
            else
            {
                return "Las ciudades ingresadas no existen en el grafo";
            }
        }

        public void DFS(int indiceVerticeInicial, List<string> resultado)
        {
            bool[] visitado = new bool[ListaAdyc.Count];
            DFSUtil(indiceVerticeInicial, visitado, resultado);
        }

        private void DFSUtil(int indiceVertice, bool[] visitado, List<string> resultado)
        {
            visitado[indiceVertice] = true;
            resultado.Add(ListaAdyc[indiceVertice].infoCiudad());

            NodoLista temp = ListaAdyc[indiceVertice].ListaEnlaces.inicio;
            while (temp != null)
            {
                if (!visitado[temp.vertexNum])
                {
                    DFSUtil(temp.vertexNum, visitado, resultado);
                }
                temp = temp.next;
            }
        }

        public void BFS(int indiceVerticeInicial, List<string> resultado)
        {
            bool[] visitado = new bool[ListaAdyc.Count];
            Queue<int> cola = new Queue<int>();

            visitado[indiceVerticeInicial] = true;
            cola.Enqueue(indiceVerticeInicial);

            while (cola.Count > 0)
            {
                int verticeActual = cola.Dequeue();
                resultado.Add(ListaAdyc[verticeActual].infoCiudad());

                NodoLista temp = ListaAdyc[verticeActual].ListaEnlaces.inicio;
                while (temp != null)
                {
                    if (!visitado[temp.vertexNum])
                    {
                        visitado[temp.vertexNum] = true;
                        cola.Enqueue(temp.vertexNum);
                    }
                    temp = temp.next;
                }
            }
        }


        public List<string> BusquedaTopologica(int indiceCiudadInicio)
        {
            List<string> resultado = new List<string>();
            bool[] visitado = new bool[ListaAdyc.Count];
            Stack<int> pila = new Stack<int>();

            // Función recursiva para realizar el recorrido DFS
            void DFS(int v)
            {
                visitado[v] = true;
                foreach (var arista in ListaAdyc[v].ListaEnlaces.MostrarDatosColeccion())
                {
                    string[] partes = arista.Split(',');
                    int vecino = int.Parse(partes[0]); // Analizar el número del vértice
                    if (!visitado[vecino])
                        DFS(vecino);
                }
                pila.Push(v);
            }

            DFS(indiceCiudadInicio);

            // Desapila los elementos de la pila y agrega al resultado
            while (pila.Count > 0)
            {
                int v = pila.Pop();
                resultado.Add(ListaAdyc[v].info.NomCiudad);
            }

            return resultado;
        }


        public string[] Dijkstra(int origen, int destino)
        {
            int n = ListaAdyc.Count;
            float[] distancias = new float[n];
            bool[] visitado = new bool[n];
            int[] previo = new int[n];

            for (int i = 0; i < n; i++)
            {
                distancias[i] = float.MaxValue;
                visitado[i] = false;
                previo[i] = -1;
            }

            distancias[origen] = 0;
            List<int> cola = new List<int> { origen };

            while (cola.Count > 0)
            {
                int u = cola[0];
                int minIndex = 0;
                for (int i = 1; i < cola.Count; i++)
                {
                    if (distancias[cola[i]] < distancias[u])
                    {
                        u = cola[i];
                        minIndex = i;
                    }
                }
                cola.RemoveAt(minIndex);

                if (visitado[u])
                    continue;

                visitado[u] = true;

                var adyacentes = ListaAdyc[u].ObternerPosicion();
                foreach (var v in adyacentes)
                {
                    float peso = ListaAdyc[u].ListaEnlaces.devolvercosto(v);
                    if (distancias[u] + peso < distancias[v])
                    {
                        distancias[v] = distancias[u] + peso;
                        previo[v] = u;
                        if (!visitado[v] && !cola.Contains(v))
                        {
                            cola.Add(v);
                        }
                    }
                }
            }

            if (distancias[destino] == float.MaxValue)
            {
                // No hay camino desde el origen al destino
                return new string[] { "No hay camino desde el origen al destino." };
            }

            // Reconstruir el camino en términos de nombres (o representaciones) de los nodos
            Stack<int> camino = new Stack<int>();
            for (int at = destino; at != -1; at = previo[at])
            {
                camino.Push(at);
            }

            List<string> recorrido = new List<string>();
            while (camino.Count > 0)
            {
                int nodo = camino.Pop();
                recorrido.Add(ListaAdyc[nodo].infoCiudad()); // Asumiendo que infoCiudad() devuelve el nombre de la ciudad
            }

            return recorrido.ToArray();
        }


        public string[] MostrarAristasVertice(int posiVertice, ref string msg)
        {
            string[] salida = null;

            if (posiVertice >= 0 && posiVertice <= (ListaAdyc.Count-1))
            {
                salida = ListaAdyc[posiVertice].MuesraAristas();
                msg = "Correcto";
            }
            else
            {
                msg = "La posición del vértice no existe en la Lista de Adyacencia";
            }

            return salida;
        }

        public List<string> MostrarAristasVertice2(int posiVertice, ref string mensaje)
        {
            List<string> salida = new List<string>();

            if (posiVertice >= 0 && posiVertice < ListaAdyc.Count)
            {
                NodoLista temp = ListaAdyc[posiVertice].ListaEnlaces.inicio;

                while (temp != null)
                {
                    salida.Add($"Vértice destino: {ListaAdyc[temp.vertexNum].info.NomCiudad} ,  posición enlace a:  [{temp.vertexNum}] , costo: {temp.costo}");
                    temp = temp.next;
                }

                mensaje = "Correcto";
            }
            else
            {
                mensaje = "La posición del vértice no existe en la Lista de Adyacencia";
            }

            return salida;
        }

        public string[] MostrarVertices()
        {
            string[] vertices = new string[ListaAdyc.Count];
            for (int i = 0; i < ListaAdyc.Count; i++)
            {
                vertices[i] = ListaAdyc[i].infoCiudad();
            }
            return vertices;
        }

        public int ObtenerIndiceCiudad(string nombreCiudad)
        {
            for (int i = 0; i < ListaAdyc.Count; i++)
            {
                if (ListaAdyc[i].info.NomCiudad.Equals(nombreCiudad, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return -1; // Retorna -1 si no se encuentra la ciudad
        }
    }
}
