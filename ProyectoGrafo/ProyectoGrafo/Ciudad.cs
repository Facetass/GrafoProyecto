using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGrafo
{
    public class Ciudad
    {
        public string NomCiudad { get; set; }
        public int Totalhabitantes { get; set; }
        public float SuperficieKm { get; set; }


        public Ciudad(string nomC, int toth, float sup)
        {
            NomCiudad = nomC;
            Totalhabitantes = toth;
            SuperficieKm = sup;
        }
    }
}
