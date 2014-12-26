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
        static void Main(string[] args)
        {
            // Chargement du fichier 
            public static void copieFichierDansTableau (string fichierSource)
            {
                try 
                {
                    // Création d'une instance StreamReader pour notre fichier
                    System.Text.Encoding   encoding = System.Text.Encoding.GetEncoding(  "iso-8859-1"  );
                    StreamReader monStreamReader = new StreamReader(fichierSource,encoding); 
                    string ligne = monStreamReader.ReadLine();

                    // Copie du fichier texte dans un tableau 
                    while (ligne != null)
                    {
                        // IL FAUT CHOISIR SI ON FAIT UN TABLEAU DE TABLEAU OU SI ON CREER NOTRE PROPRE STRUCTURE. Structure ??
                    }
                }
            }
        }
    }
}
