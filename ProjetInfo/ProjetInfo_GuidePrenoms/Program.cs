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
        public struct entite
        {
            public int annee;
            public string prenom;
            public int nbDeFoisDonne;
            public int rang;
        }
        // Chargement du fichier 
        public static void copieFichierDansTableau (out entite[] entites)
        {
            try
            {
                string[] lignes = System.IO.File.ReadAllLines("prenoms_bordeaux.txt");
                entites = new entite[lignes.Length-1];  // -1 car on prend pas la première ligne du fichier 
                for (int i = 1; i < lignes.Length; i++)
                {
                    string[] ligneDecoupee = lignes[i].Split();
                    entites[i-1].annee = int.Parse(ligneDecoupee[0]);   // i-1 car on n'a pas pris la première ligne du fichier..
                    entites[i-1].prenom = ligneDecoupee[1];             // .. et sinon on dépassera de notre tableau
                    entites[i-1].nbDeFoisDonne = int.Parse(ligneDecoupee[2]);
                    entites[i-1].rang = int.Parse(ligneDecoupee[3]);
                }
            }
            catch
            {
                Console.WriteLine("/!\\ Veuillez vérifier que le fichier \"prenoms_bordeaux.txt\" soit dans le même dossier que le fichier .exe de l'application.\nTaper \"ok\" lorsque l'opération est effectuée.");
                while (string.Equals(Console.ReadLine().ToUpper(), "OK") == false)
                    Console.WriteLine("/!\\ Veuillez vérifier que le fichier \"prenoms_bordeaux.txt\" soit dans le même dossier que le fichier .exe de l'application.\nTaper \"ok\" lorsque l'opération est effectuée.");
                copieFichierDansTableau(out entites);
            }
        }

        public static void afficheFichier(entite[] entites) 
        {
            int longueur = entites.Length, i = 0;
            while (i < longueur)    // Gestion de l'alignement des colonnes
            {
                if (entites[i].prenom.Length > 7) 
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", entites[i].annee, entites[i].prenom, entites[i].nbDeFoisDonne, entites[i].rang);
                    i++;
                }
                else
                {
                    Console.WriteLine("{0}\t{1}\t\t{2}\t{3}", entites[i].annee, entites[i].prenom, entites[i].nbDeFoisDonne, entites[i].rang);
                    i++;
                }
            }
        } 


        static void Main(string[] args)
        {
            entite[] entites;
            copieFichierDansTableau(out entites);
            afficheFichier(entites);
 
            Console.ReadLine();
        }
    }
}
