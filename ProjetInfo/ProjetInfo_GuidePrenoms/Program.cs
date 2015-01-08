﻿using System;
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
        public static void copieFichierDansTableau(out entite[] entites)                    // Chargement du fichier
        {
            Console.WriteLine("Chargement du fichier, veuillez patientez...");
            try
            {
                string[] lignes = System.IO.File.ReadAllLines("prenoms_bordeaux.txt");
                entites = new entite[lignes.Length-1];                                      // -1 car on prend pas la première ligne du fichier 
                for (int i = 1; i < lignes.Length; i++)
                {
                    string[] ligneDecoupee = lignes[i].Split();
                    entites[i-1].annee = int.Parse(ligneDecoupee[0]);                       // i-1 car on n'a pas pris la première ligne du fichier..
                    entites[i-1].prenom = ligneDecoupee[1];                                 // .. et sinon on dépassera de notre tableau
                    entites[i-1].nbDeFoisDonne = int.Parse(ligneDecoupee[2]);
                    entites[i-1].rang = int.Parse(ligneDecoupee[3]);
                }
            }
            catch                                                                           // Gestion des cas où le fichier ne serait pas au bon emplacement ou au mauvais nom
            {
                Console.WriteLine("/!\\ Veuillez vérifier que le fichier \"prenoms_bordeaux.txt\" soit dans le même dossier que le fichier .exe de l'application.\nTaper \"ok\" lorsque l'opération est effectuée.");
                while (string.Equals(Console.ReadLine().ToUpper(), "OK") == false)
                    Console.WriteLine("/!\\ Veuillez vérifier que le fichier \"prenoms_bordeaux.txt\" soit dans le même dossier que le fichier .exe de l'application.\nTaper \"ok\" lorsque l'opération est effectuée.");
                copieFichierDansTableau(out entites);
            }
            Console.WriteLine("Fichier chargé avec succès !");
        }
        public static void afficheFichier(entite[] entites)                                 // Affichage du tableau pour vérification du chargement du fichier 
        {
            Console.WriteLine("Rang\tPrénom\t\tNombre");
            int longueur = entites.Length, i = 0;
            while (i < longueur)                                                            // Gestion de l'alignement des colonnes
            {
                if (entites[i].prenom.Length > 7) 
                {
                    Console.WriteLine("{0}\t{1}\t{2}", entites[i].rang, entites[i].prenom, entites[i].nbDeFoisDonne);
                    i++;
                }
                else
                {
                    Console.WriteLine("{0}\t{1}\t\t{2}", entites[i].rang, entites[i].prenom, entites[i].nbDeFoisDonne);
                    i++;
                }
            }
        } 
        public static void menu()                                                           // Affichage du MENU
        {
            Console.Write("Appuyez sur sur la touche entrée pour accéder au menu.");
            Console.ReadLine();
            Console.WriteLine("+-----------------------------------------------------------------------------+");
            Console.WriteLine("|                                     MENU                                    |");
            Console.WriteLine("+-----------+-----------------------------------------------------------------+");
            Console.WriteLine("| Requête 1 | Donne le nombre de naissances et le rang sur 100 d'un prénom    |");
            Console.WriteLine("|           | quelconque sur une année donnée.                                |");
            Console.WriteLine("|  TAPEZ 1  |                                                                 |");
            Console.WriteLine("+-----------+-----------------------------------------------------------------+");
            Console.WriteLine("| Requête 2 | Donne le top 10 des prénoms sur une période donnée.             |");
            Console.WriteLine("|           |                                                                 |");
            Console.WriteLine("|  TAPEZ 2  |                                                                 |");
            Console.WriteLine("+-----------+-----------------------------------------------------------------+");
            Console.WriteLine("| Requête 3 | Donne le nombre de naissances et le rang d'un prénom quelconque |");
            Console.WriteLine("|           | sur une période donnée.                                         |");
            Console.WriteLine("|  TAPEZ 3  |                                                                 |");
            Console.WriteLine("+-----------+-----------------------------------------------------------------+");
            Console.WriteLine("| Requête 4 | Donne la tendance d'un prénom quelconque sur les N dernières    |");
            Console.WriteLine("|           | années.                                                         |");
            Console.WriteLine("|  TAPEZ 4  |                                                                 |");
            Console.WriteLine("+-----------+-----------------------------------------------------------------+");
            Console.WriteLine("| Requête 5 | Donne la liste complète d'une année donnée.                     |");
            Console.WriteLine("|           |                                                                 |");
            Console.WriteLine("|  TAPEZ 5  |                                                                 |");
            Console.WriteLine("+-----------+-----------------------------------------------------------------+");
            Console.WriteLine("|  Tapez 0  | Quitter                                                         |");
            Console.WriteLine("+-----------+-----------------------------------------------------------------+");
        }
        public static void programme(entite[] entites)                                      // Choix de la requête
        {
            menu();
            string commande;
            do
            {
                Console.WriteLine("Entrez le numéro de la requête souhaitée s'il vous plait.");
                commande = Console.ReadLine();
            }
            while (commande != "0" && commande != "1" && commande != "2" && commande != "3" && commande != "4" && commande != "5"); // Gestion des erreurs de frappe éventuelles
            int select = int.Parse(commande);
            switch (select)
            {
                case 0 :
                    Console.WriteLine("Au revoir !");
                    Console.WriteLine("Appuyez sur une toucher pour quitter.");
                    break;
                case 1 :
                    requeteA(entites);
                    programme(entites);
                    break;
                case 2 :
                    requeteB(entites);                    
                    programme(entites);
                    break;
                case 3 :
                    requeteC(entites);
                    programme(entites);
                    break;
                case 4 :
                    // requeteD;
                    programme(entites);
                    break;
                case 5 : 
                    // requeteE;
                    programme(entites);
                    break;
            }
        }
        public static string saisirPrenom ()                                                //(entite[] entites)
        {
            Console.WriteLine("Sur quel prénom voulez-vous être renseigné ?");
            string prenomChoisi = Console.ReadLine().ToUpperInvariant();                    // Mise en majuscules du prénom pour correspondre à la liste du fichier
            return prenomChoisi;
        }
        public static int saisirAnnee(entite[] entites)
        {
            try
            {
                int annee = int.Parse(Console.ReadLine());
                while (annee > entites[0].annee || annee < entites[entites.Length - 1].annee)
                {
                    Console.WriteLine("Veuillez renseigner une année entre {0} et {1} s'il vous plaît", entites[entites.Length - 1].annee, entites[0].annee);
                    annee = int.Parse(Console.ReadLine());                
                }
                return annee;
            }
            catch
            {
                Console.WriteLine("Veuillez renseigner une année entre {0} et {1} s'il vous plaît", entites[entites.Length - 1].annee, entites[0].annee);
                int annee  = saisirAnnee(entites);
                return annee;
            }
        }
        public static void saisirPeriode(entite[] entites, out int annee1, out int annee2)
        {
            Console.WriteLine("Quelle est l'année de début de la période étudiée ?");
            annee1 = saisirAnnee(entites);
            Console.WriteLine("Quelle est l'année de fin de la période étudiée ?");
            annee2 = saisirAnnee(entites);
            if (annee2 < annee1)
            {
                int tmp;
                tmp = annee1;
                annee1 = annee2;
                annee2 = tmp;
            }
        }
        public static int trouveNombrePrenomsDifferents(entite[] entites, int annee1, int annee2, out int j, out int n)
        {
            j = (entites[0].annee - annee2) * 100;
            n = (1 + annee2 - annee1) * 100;
            int h = 1, i = 0, k = n;
            while (i < n)
            {
                while (h < n - i)
                {
                    if (String.Equals(entites[j + i].prenom, entites[j + i + h].prenom) == true)
                    {
                        k--;
                        break;
                    }
                    h++;
                }
                h = 1;
                i++;
            }
            return k;
        }
        public static entite[] rempliClassement(entite[] entites, entite[] entites2, int j, int n)
        {
            int i = 0, h = j, rang;
            while (h < j + n)
            {
                if ((h > j + 99) && estDejaSaisi(entites2, i, entites[h].prenom, out rang) == true)
                {
                    entites2[rang].nbDeFoisDonne += entites[h].nbDeFoisDonne;
                    h++;
                    continue;
                }
                entites2[i] = entites[h];
                i++;
                h++;
            }
            return entites2;
        }
        public static entite[] reetiquetage(entite[] entites2, int k)
        {
            int i = 0;
            while (i < k)
            {
                entites2[i].rang = i + 1;
                i++;
            }
            return entites2;
        }
        public static entite[] classementPrenomsPeriode(entite[] entites, int annee1, int annee2) 
        {           
            int j, n;
            int k = trouveNombrePrenomsDifferents(entites, annee1, annee2, out j, out n);   // On détermine d'abord combien il y a de prénoms différents dans les top 100 de la période donnée (=k)
            entite[] entites2 = new entite[k];                                              // On crée un nouveau tableau de la bonne taille (=k)
            entites2 = rempliClassement(entites, entites2, j, n);                           // On rentre les prénoms non-triés avec leurs totaux
            entites2 = triFusion(entites2, k);                                              // On trie le tableau afin d'obtenir le classement
            entites2 = reetiquetage(entites2, k);                                           // On harmonise tous les rangs
            return entites2;
        }
        public static entite[] triNaif(entite[] entites, int longueurListe)
        {
            int i = 0, j = 1;
            entite tmp;
            while (i < longueurListe - 1)
            {
                while (j < longueurListe - i)
                {
                    if (entites[i].nbDeFoisDonne < entites[i + j].nbDeFoisDonne)
                    {
                        tmp = entites[i];
                        entites[i] = entites[i + j];
                        entites[i + j] = tmp;
                    }
                    j++;
                }
                j = 1;
                i++;
            }
            return entites;
        }
        public static entite[] triFusion(entite[] entites, int longueurListe)
        {
            entite[] listeFinale = new entite[longueurListe];
            if (longueurListe == 1)
            {
                listeFinale[0] = entites[0];
                return listeFinale;
            }
            int m = longueurListe / 2, i = 0;
            entite[] liste1 = new entite[m], liste2 = new entite[longueurListe - m];
            while (i < m)
            {
                liste1[i] = entites[i];
                i++;
            }
            i = 0;
            while (i < longueurListe - m)
            {
                liste2[i] = entites[i + m];
                i++;
            }
            liste1 = triFusion(liste1, liste1.Length);
            liste2 = triFusion(liste2, liste2.Length);
            listeFinale = fusionne(liste1, liste2);
            return listeFinale;
        }
        public static entite[] fusionne(entite[] entites1, entite[] entites2)
        {
            int longueurliste1 = entites1.Length, longueurliste2 = entites2.Length;
            entite[] listeTri = new entite[longueurliste1 + longueurliste2];
            int a = 0, b = 0, i = 0;
            while (a < longueurliste1 && b < longueurliste2)
            {
                if (entites1[a].nbDeFoisDonne > entites2[b].nbDeFoisDonne)
                {
                    listeTri[i] = entites1[a];
                    a++;
                    i++;
                }
                else
                {
                    listeTri[i] = entites2[b];
                    b++;
                    i++;
                }
            }
            while (a < longueurliste1)
            {
                listeTri[i] = entites1[a];
                a++;
                i++;
            }
            while (b < longueurliste2)
            {
                listeTri[i] = entites2[b];
                b++;
                i++;
            }
            return listeTri;
        }
        public static entite[] topDix(entite[] entites, int annee1, int annee2)
        {
            entite[] classement = classementPrenomsPeriode(entites, annee1, annee2);
            entite[] topDix = new entite[10];
            int i = 0;
            while (i < 10)
            {
                topDix[i] = classement[i];
                i++;
            }
            return topDix;
        }
        public static bool estDejaSaisi(entite[] entites, int rangMaxEntites, string prenom, out int rangMemePrenom)
        {
            rangMemePrenom = 0;
            while (rangMemePrenom < rangMaxEntites)
            {
                if (String.Equals(entites[rangMemePrenom].prenom, prenom) == true)
                    return true;
                rangMemePrenom++;
            }
            return false;
        }
        public static void requeteA(entite[] entites)                                       // Nombre de naissance et Rang sur 100 d'un prénom sur une année
        {
            string prenomChoisi = saisirPrenom();
            Console.WriteLine("Sur quelle année souhaitez-vous être renseigné ?");          // pourquoi pas le mettre dans saisir année ?  ca fait tache la
            int anneeChoisie = saisirAnnee(entites);
            int i = (entites[0].annee - anneeChoisie) * 100;
            while ((i < (entites[0].annee - anneeChoisie) * 100 + 100) && (String.Equals(prenomChoisi, entites[i].prenom) == false))
            {
                i++;
            }
            if (i < (entites[0].annee - anneeChoisie) * 100 + 100)
            {
                if (entites[i].rang == 1)
                    Console.WriteLine("En {0}, {1} {2} ont vu le jour. C'est le prénom le plus donné à Bordeaux cette année là", entites[i].annee, entites[i].nbDeFoisDonne, entites[i].prenom, entites[i].rang);
                else
                    Console.WriteLine("En {0}, {1} {2} ont vu le jour. C'est le {3}ème prénom le plus donné à Bordeaux cette année là", entites[i].annee, entites[i].nbDeFoisDonne, entites[i].prenom, entites[i].rang);
            }
            else
                Console.WriteLine("Prenom non répertorié pour cette année.");
        }
        public static void requeteB(entite[] entites)
        {
            int annee1, annee2;
            saisirPeriode(entites, out annee1, out annee2);
            entite[] classementTopDix = topDix(entites, annee1, annee2);
            afficheFichier(classementTopDix);
        }
        public static void requeteC(entite[] entites)
        {
            string prenom = saisirPrenom();
            int annee1, annee2, i = 0;
            saisirPeriode(entites, out annee1, out annee2);
            entite[] classement = classementPrenomsPeriode(entites, annee1, annee2);
            bool d = false;
            while (i < classement.Length && d == false)
            {
                if (String.Equals(prenom, classement[i].prenom) == true)
                {
                    d = true;
                    Console.WriteLine("Entre {0} et {1}, {2} {3} ont vu le jour. C'est le {4} prénom le plus donné sur {5} au total durant cette période.", annee1, annee2, classement[i].nbDeFoisDonne, classement[i].prenom, classement[i].rang, classement.Length);
                    break;
                }
                i++;
            }
            if (d == false)
                Console.WriteLine("Ce prénom n'est pas répertorié durant cette période.");

        }
       /* public static void requeteD (entite[] entites)
        {
            string prenom = saisirPrenom();
            int annee1, annee2;
            saisirPeriode(entites, out annee1, out annee2);
            double m2 = moyenne(entites, annee1, annee2, prenom), m1 = moyenne(entites, entites[entites.Length - 1].annee, annee1 - 1, prenom);
                                                                                            // ecart-type 
                                                                                            // test qualificatif + affichage reponse  
        }        
                                                                                            // Affiche des 100 prenoms d'une année choisie
        public static void requeteE (entite[] entites )
        {
                                                                                            // a faire 
        }
        public static double moyenne (entite[] entites, int annee1, int annee2, string prenom)
        {
                                                                                            // a toi de jouer
        }
        public static double ecartType (entite[] entites, int annee1, int annee2, string prenom)
        {
        }
        public static int esperance (entite[] entites, string prenom)
        {
        }*/
        static void Main(string[] args)
        {
            entite[] entites;
            copieFichierDansTableau(out entites);
            programme(entites);
            Console.ReadLine();
        }
    }
}
