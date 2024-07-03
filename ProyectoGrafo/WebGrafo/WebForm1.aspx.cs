using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ProyectoGrafo;
using Newtonsoft.Json;
using System.Web.UI;
using System.Linq;


namespace WebGrafo
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        Grafo graf1 = null;
        string mensaje = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // Carga de la página por primera vez
                graf1 = new Grafo();
                Session["graf1"] = graf1;
            }
            else
            {
                // Recuperación del grafo desde la sesión en caso de postback
                graf1 = (Grafo)Session["graf1"];
            }

            string graphJson = ConvertGraphToJson();
            ClientScript.RegisterStartupScript(this.GetType(), "graphData", "var graphData = " + graphJson + ";", true);
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            // Agregar ciudad como vértice
            string nombreCiudad = TextBox1.Text;
            int habitantes = Convert.ToInt32(TextBox2.Text); 
            float superficie = Convert.ToSingle(TextBox3.Text); 

            Ciudad nuevaCiudad = new Ciudad(nombreCiudad, habitantes, superficie);
            graf1.AgregarVertice(nuevaCiudad);

            Session["graf1"] = graf1;
            ActualizarDropDownList();

            TextBox8.Text = "Ciudad Agregada";

            Llenar();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            // Agregar arista entre ciudades
            string CiudadOrigen = TextBox4.Text;
            string CiudadDestino = TextBox5.Text;
            int costo = Convert.ToInt32(TextBox6.Text); 

            int indiceOrigen = ObtenerIndiceCiudad(CiudadOrigen);
            int indiceDestino = ObtenerIndiceCiudad(CiudadDestino);

            if (indiceOrigen != -1 && indiceDestino != -1)
            {
                string mensaje = graf1.AgregarArista(CiudadOrigen, CiudadDestino, costo);

                TextBox7.Text = mensaje;
                Session["graf1"] = graf1;
                ActualizarDropDownListAristas();
            }
            else
            {
                TextBox7.Text = "Las ciudades ingresadas no existen en el grafo.";
            }
        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            // Mostrar aristas del vértice seleccionado
            int posicionVertice = DropDownList1.SelectedIndex;
            string[] aristas;

            if (posicionVertice != -1)
            {
                aristas = graf1.MostrarAristasVertice(posicionVertice, ref mensaje);
                DropDownList2.Items.Clear();
                foreach (string w in aristas)
                {
                    DropDownList2.Items.Add(w);
                }
                List<string> aristasConNombre = graf1.MostrarAristasVertice2(posicionVertice, ref mensaje);
                DropDownList3.Items.Clear();
                foreach (string w in aristasConNombre)
                {
                    DropDownList3.Items.Add(w);
                }
            }
            else
            {
                TextBox7.Text = "Debe elegir un vértice de la lista";
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            // Evento del botón para iniciar el DFS desde un vértice seleccionado
            int posicionVertice = DropDownList1.SelectedIndex;
            if (posicionVertice != -1)
            {
                // Llama al método DFS del grafo
                List<string> resultadoDFS = new List<string>();
                graf1.DFS(posicionVertice, resultadoDFS); // Pasa una lista para almacenar el resultado del DFS

                // Actualiza el campo de texto con el resultado del DFS
                TextBox8.Text = "Recorrido DFS:\n";
                foreach (var ciudad in resultadoDFS)
                {
                    TextBox8.Text += ciudad + "\n";
                }
            }
            else
            {
                TextBox7.Text = "Debe elegir un vértice de la lista";
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            // Evento del botón para iniciar el BFS desde un vértice seleccionado
            int posicionVertice = DropDownList1.SelectedIndex;
            if (posicionVertice != -1)
            {
                // Llama al método BFS del grafo
                List<string> resultadoBFS = new List<string>();
                graf1.BFS(posicionVertice, resultadoBFS); // Pasa una lista para almacenar el resultado del BFS

                // Actualiza el campo de texto con el resultado del BFS
                TextBox8.Text = "Recorrido BFS:\n";
                foreach (var ciudad in resultadoBFS)
                {
                    TextBox8.Text += ciudad + "\n";
                }
            }
            else
            {
                TextBox7.Text = "Debe elegir un vértice de la lista";
            }
        }


        protected void Button6_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedIndex >= 0)
            {
                int indice = DropDownList1.SelectedIndex;
                List<string> recorridoTopologico = graf1.BusquedaTopologica(indice);

                // Mostrar el resultado en el TextBox
                TextBox9.Text = string.Join(" -> ", recorridoTopologico);
            }
            else
            {
                TextBox9.Text = "Por favor, selecciona una ciudad de inicio.";
            }
        }


        protected void Button7_Click(object sender, EventArgs e)
        {
            GridView1.DataSource = null;
            string[] cam = graf1.Dijkstra(DropDownList4.SelectedIndex, DropDownList5.SelectedIndex);

                if(cam != null)
                {
                    GridView1.DataSource = cam.Select((ciudad, index) => new { indice = index, Ciudad = ciudad });
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
                
        }


        protected void Button8_Click(object sender, EventArgs e)
        {
            List<object> nodesData = new List<object>();
            List<object> edgesData = new List<object>();

            // Crear nodos
            for (int i = 0; i < graf1.ListaAdyc.Count; i++)
            {
                var vertice = graf1.ListaAdyc[i];
                nodesData.Add(new { id = i, label = vertice.info.NomCiudad });
            }

            // Crear aristas
            for (int i = 0; i < graf1.ListaAdyc.Count; i++)
            {
                var vertice = graf1.ListaAdyc[i];
                foreach (var arista in vertice.ListaEnlaces.MostrarDatosColeccion())
                {
                    string[] partes = arista.Split(',');
                    int destino = int.Parse(partes[0]);
                    float costo = float.Parse(partes[1]);
                    edgesData.Add(new { from = i, to = destino, label = costo.ToString() });
                }
            }

            //Serializar datos a JSON
            var nodesJson = Newtonsoft.Json.JsonConvert.SerializeObject(nodesData);
            var edgesJson = Newtonsoft.Json.JsonConvert.SerializeObject(edgesData);

            //Creamos scripts para inicializar el grafo en vis.js
            string script = $"drawGraph({nodesJson}, {edgesJson});";
            ScriptManager.RegisterStartupScript(this, GetType(), "drawGraph", script, true);
        }

        protected int ObtenerIndiceCiudad(string nombreCiudad)
        {
            // Método para obtener el índice de una ciudad por su nombre
            List<Vertice> vertices = graf1.ListaAdyc;
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].info.NomCiudad == nombreCiudad)
                {
                    return i;
                }
            }
            return -1; // Retorna -1 si no se encuentra la ciudad
        }

        protected void ActualizarDropDownList()
        {
            DropDownList1.Items.Clear();
            List<Vertice> vertices = graf1.ListaAdyc;
            foreach (Vertice vertice in vertices)
            {
                DropDownList1.Items.Add(vertice.infoCiudad());
            }
        }

        protected void ActualizarDropDownListAristas()
        {
            int posicionVertice = DropDownList1.SelectedIndex;
            if (posicionVertice != -1)
            {
                string[] aristas = graf1.MostrarAristasVertice(posicionVertice, ref mensaje);
                DropDownList2.Items.Clear();
                foreach (string arista in aristas)
                {
                    DropDownList2.Items.Add(arista);
                }

                List<string> aristasConNombre = graf1.MostrarAristasVertice2(posicionVertice, ref mensaje);
                DropDownList3.Items.Clear();
                foreach (string arista in aristasConNombre)
                {
                    DropDownList3.Items.Add(arista);
                }
            }
            else
            {
                TextBox7.Text = "Debe elegir un vértice de la lista";
            }
        }


        protected string ConvertGraphToJson()
        {
            var vertices = graf1.ListaAdyc;
            var nodes = new List<object>();
            var links = new List<object>();

            for (int i = 0; i < vertices.Count; i++)
            {
                nodes.Add(new
                {
                    id = i,
                    name = vertices[i].info.NomCiudad,
                    habitantes = vertices[i].info.Totalhabitantes,
                    superficie = vertices[i].info.SuperficieKm
                });

                var aristas = vertices[i].ListaEnlaces.MostrarDatosColeccion();
                foreach (var arista in aristas)
                {
                    var parts = arista.Split(',');
                    int destino = int.Parse(parts[0]);
                    float costo = float.Parse(parts[1]);

                    links.Add(new
                    {
                        source = i,
                        target = destino,
                        value = costo
                    });
                }
            }

            var graphData = new { nodes, links };
            return Newtonsoft.Json.JsonConvert.SerializeObject(graphData);
        }



        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void Llenar() 
        {
            DropDownList4.Items.Clear();
            DropDownList5.Items.Clear();
            foreach (var ciudades in graf1.MostrarVertices())
            {
                DropDownList4.Items.Add(new ListItem(ciudades));
                DropDownList5.Items.Add(new ListItem(ciudades));
            }
        }
    }
}
