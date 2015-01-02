using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace ProjetInfo_GuidePrenoms
{
    class Program
    {
        struct entite
        {
            public int annee;
            public string prenom;
            public int nbDeFoisDonne;
            public int rang;
        }
        // Chargement du fichier 
        public static void copieFichierDansTableau (string fichierSource)
        {
            string[] lignes = System.IO.File.ReadAllLines(fichierSource);
            entite[] entites = new entite[lignes.Length-1];  // -1 car on prend pas la première ligne du fichier 
            for (int i = 1; i < lignes.Length; i++)
            {
                string[] ligneDecoupee = lignes[i].Split();
                entites[i-1].annee = int.Parse(ligneDecoupee[0]);   // i-1 car on n'a pas pris la première ligne du fichier..
                entites[i-1].prenom = ligneDecoupee[1];             // .. et sinon on dépassera de notre tableau
                entites[i-1].nbDeFoisDonne = int.Parse(ligneDecoupee[2]);
                entites[i-1].rang = int.Parse(ligneDecoupee[3]);
            }
        }

        static void Main(string[] args)
        {
            copieFichierDansTableau(@"C:\Users\LVH2\Documents\ENSC\PROG\PROJET");
            Console.WriteLine("{0}",entites[88].prenom); // comment faire pour récuperer le tableau entites ? 
            Console.ReadLine();
        }
    }
}
