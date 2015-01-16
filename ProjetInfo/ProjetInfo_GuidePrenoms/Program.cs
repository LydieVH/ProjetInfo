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
        // Création de la structure
        public struct entite  
        {
            public int annee;
            public string prenom;
            public int nbDeFoisDonne;
            public int rang;
        }
        
        // Fonction principale
        static void Main(string[] args) 
        {
            entite[] entites;
            copieFichierDansTableau(out entites);
            programme(entites);
            Console.ReadLine();
        }
        
        // Chargement du fichier source dans un tableau de la structure entite.
        // Sortie : Donne la copie du fichier (entites)        
        public static void copieFichierDansTableau(out entite[] entites)   
        {
            Console.WriteLine("Chargement du fichier, veuillez patientez...");
            try
            {
                string[] lignes = System.IO.File.ReadAllLines("prenoms_bordeaux.txt");
                entites = new entite[lignes.Length - 1];                                      // -1 car on ne prend pas la première ligne du fichier 
                for (int i = 1; i < lignes.Length; i++)
                {
                    string[] ligneDecoupee = lignes[i].Split();
                    entites[i - 1].annee = int.Parse(ligneDecoupee[0]);                       // i-1 car on n'a pas pris la première ligne du fichier..
                    entites[i - 1].prenom = ligneDecoupee[1];                                 // .. et sinon on dépassera de notre tableau
                    entites[i - 1].nbDeFoisDonne = int.Parse(ligneDecoupee[2]);
                    entites[i - 1].rang = int.Parse(ligneDecoupee[3]);
                }
            }
            catch                                                                           // Gestion des cas où le fichier ne serait pas au bon emplacement ou au mauvais nom
            {
                Console.Write("/!\\ Veuillez vérifier que le fichier \"prenoms_bordeaux.txt\" ");
                Console.Write("soit dans le même dossier que le fichier .exe de l'application.\nTaper");
                Console.Write("\"ok\" lorsque l'opération est effectuée.");
                while (string.Equals(Console.ReadLine().ToUpper(), "OK") == false)
                {
                    Console.Write("/!\\ Veuillez vérifier que le fichier \"prenoms_bordeaux.txt\" soit ");
                    Console.Write("dans le même dossier que le fichier .exe de l'application. ");
                    Console.Write("\nTaper \"ok\" lorsque l'opération est effectuée.");
                }
                copieFichierDansTableau(out entites);
            }
            Console.WriteLine("Fichier chargé avec succès !");
        }
        
        // Affichage du menu
        public static void menu()  
        {
            Console.Write("Appuyez sur sur la touche entrée pour accéder au menu.");
            Console.ReadLine();
            Console.WriteLine("+----------------------------------------------------------------------------+");
            Console.WriteLine("|                                     MENU                                   |");
            Console.WriteLine("+-----------+----------------------------------------------------------------+");
            Console.WriteLine("| Requête 1 | Donne le nombre de naissances et le rang sur 100 d'un prénom   |");
            Console.WriteLine("|           | quelconque sur une année donnée.                               |");
            Console.WriteLine("|  TAPEZ 1  |                                                                |");
            Console.WriteLine("+-----------+----------------------------------------------------------------+");
            Console.WriteLine("| Requête 2 | Donne le top 10 des prénoms sur une période donnée.            |");
            Console.WriteLine("|           |                                                                |");
            Console.WriteLine("|  TAPEZ 2  |                                                                |");
            Console.WriteLine("+-----------+----------------------------------------------------------------+");
            Console.WriteLine("| Requête 3 | Donne le nombre de naissances et le rang d'un prénom quelconque|");
            Console.WriteLine("|           | sur une période donnée.                                        |");
            Console.WriteLine("|  TAPEZ 3  |                                                                |");
            Console.WriteLine("+-----------+----------------------------------------------------------------+");
            Console.WriteLine("| Requête 4 | Donne la tendance d'un prénom quelconque sur les N dernières   |");
            Console.WriteLine("|           | années.                                                        |");
            Console.WriteLine("|  TAPEZ 4  |                                                                |");
            Console.WriteLine("+-----------+----------------------------------------------------------------+");
            Console.WriteLine("| Requête 5 | Donne le Top 100 d'une année donnée.                           |");
            Console.WriteLine("|           |                                                                |");
            Console.WriteLine("|  TAPEZ 5  |                                                                |");
            Console.WriteLine("+-----------+----------------------------------------------------------------+");
            Console.WriteLine("|  Tapez 0  | Quitter                                                        |");
            Console.WriteLine("+-----------+----------------------------------------------------------------+");
        }
        // Choix de la requête. Entrée : Copie du fichier (entites)
        public static void programme(entite[] entites) 
        {
            menu();
            string cde;
            // Gestion des erreurs de frappe éventuelles
            do
            {
                Console.WriteLine("Entrez le numéro de la requête souhaitée s'il vous plait.");
                cde = Console.ReadLine();
            }
            while (cde != "0" && cde != "1" && cde != "2" && cde != "3" && cde != "4" && cde != "5"); 
            int select = int.Parse(cde);
            switch (select)
            {
                case 0:
                    Console.WriteLine("Au revoir !");
                    Console.WriteLine("Appuyez sur une toucher pour quitter.");
                    break;
                case 1:
                    prenomAnnee(entites);
                    programme(entites);
                    break;
                case 2:
                    top10Periode(entites);
                    programme(entites);
                    break;
                case 3:
                    prenomPeriode(entites);
                    programme(entites);
                    break;
                case 4:
                    prenomTendance(entites);
                    programme(entites);
                    break;
                case 5:
                    top100Annee(entites);
                    programme(entites);
                    break;
            }
        }
        
        // Nombre de naissances et Rang sur 100 d'un prénom sur une année. 
        // Entrée : Copie du fichier (entites)       
        public static void prenomAnnee(entite[] entites) 
        {
            string prenomC = saisirPrenom(entites);
            Console.WriteLine("Sur quelle année souhaitez-vous être renseigné ?");     
            int anChoisie = saisirAnnee(entites);
            int i = (entites[0].annee - anChoisie) * 100;
            int cond1 = i;
            while ((i < cond1 + 100) && (String.Equals(prenomC, entites[i].prenom) == false))
            {
                i++;
            }
            if (i < (entites[0].annee - anChoisie) * 100 + 100)
            {
                if (entites[i].rang == 1)
                {
                    Console.Write("En {0}", entites[i].annee);
                    Console.Write(" {1} {2} ont vu le jour. ", entites[i].nbDeFoisDonne, entites[i].prenom);
                    Console.WriteLine("C'est le prénom le plus donné à Bordeaux cette année là.");
                }
                else
                {
                    Console.Write("En {0}", entites[i].annee);
                    Console.Write(" {0} {1} ont vu le jour. ", entites[i].nbDeFoisDonne, entites[i].prenom);
                    Console.Write("C'est le {0}ème prénom le plus donné à Bordeaux cette année là.\n", entites[i].rang);
                }
            }
            else
                Console.WriteLine("Prenom non répertorié pour cette année.");
        }
        // Donne le top 10 des prénoms sur une période donnée. 
        // Entrée : Copie du fichier (entites)
        public static void top10Periode(entite[] entites)     
        {
            int annee1, annee2;
            saisirPeriode(entites, out annee1, out annee2);
            entite[] classementTopDix = topDix(entites, annee1, annee2);
            Console.WriteLine("Entre {0} et {1}, le top 10 est :", annee1, annee2);
            afficheFichier(classementTopDix);
        }
        // Donne le nombre de naissances et le rang d'un prénom quelconque sur une période donnée. 
        // Entrée : Copie du fichier (entites)
        public static void prenomPeriode(entite[] entites)  
        {
            string prenom = saisirPrenom(entites);
            int annee1, annee2, i = 0;
            saisirPeriode(entites, out annee1, out annee2);
            entite[] classement = classementPeriode(entites, annee1, annee2);
            bool d = false;
            while (i < classement.Length && d == false)
            {
                if (String.Equals(prenom, classement[i].prenom) == true)
                {
                    d = true;
                    Console.Write("Entre {0} et {1}, ", annee1, annee2);
                    Console.Write("{0} {1} ont vu le jour.", classement[i].nbDeFoisDonne, classement[i].prenom);
                    Console.Write(" C'est le {0} prénom le plus donné ", classement[i].rang);
                    Console.WriteLine("sur {0} au total durant cette période.", classement.Length);
                    break;
                }
                i++;
            }
            if (d == false)
                Console.WriteLine("Ce prénom n'est pas répertorié durant cette période.");

        }
        // Donne la tendance d'un prénom sur les N dernières années. 
        // Entrée : Copie du fichier (entites)
        public static void prenomTendance(entite[] entites)
        {
            string prenom = saisirPrenom(entites);
            Console.WriteLine("Sur combien d'année voulez-vous connaitre la tendance de ce prénom ?");
            int N = saisirN(entites);
            // Création des tableaux
            int anneeCharniere = entites[0].annee - N;
            // Tableau de la période souhaitée
            entite[] tab2 = tSpePrenom(entites, prenom, entites[0].annee, anneeCharniere);
            // Tableau du reste
            entite[] tab1 = tSpePrenom(entites, prenom, anneeCharniere - 1, entites[entites.Length - 1].annee);         
            double m1 = moyenne(tab1);            
            double E = ecartType(tab1, m1);            
            double m2 = moyenne(tab2);
            tendance(m1, m2, E, prenom);
        }
        // Donne le Top 100 d'une année donnée. 
        // Entrée : Copie du fichier (entites)
        public static void top100Annee(entite[] entites)
        {
            Console.WriteLine("Sur quelle année souhaitez-vous être renseigné ?");
            int an = saisirAnnee(entites), i = (entites[0].annee - an) * 100;
            afficheTopCentAnnee(entites, i);
        }

        /* Demande et stocke le prenom voulu. 
           Entrée : Copie du fichier (entites). 
           Sortie : Prénom choisi */
        public static string saisirPrenom(entite[] entites)
        {
            Console.WriteLine("Sur quel prénom voulez-vous être renseigné ?");
            // Mise en majuscules du prénom pour correspondre à la liste du fichier
            string prenomChoisi = Console.ReadLine().ToUpperInvariant();
            int rang;
            while (estDejaSaisi(entites, entites.Length - 1, prenomChoisi, out rang) == false)
            {
                Console.Write("Ce prénom n'est pas répertorié, ");
                Console.WriteLine("sur quel autre prénom voulez-vous être renseigné ?");
                prenomChoisi = Console.ReadLine().ToUpperInvariant();
            }
            return prenomChoisi;
        }
        /* Demande et stocke l'année voulue. 
           Entrée : Copie du fichier (entites). 
           Sortie : Année choisie */
        public static int saisirAnnee(entite[] entites)
        {
            try
            {
                int annee = int.Parse(Console.ReadLine());
                // Cas où l'utilisateur rentre une année qui n'est pas répertoriée dans le document chargé
                while (annee > entites[0].annee || annee < entites[entites.Length - 1].annee)
                {
                    Console.Write("Veuillez renseigner une année entre {0} ", entites[entites.Length - 1].annee);
                    Console.WriteLine("et {0} s'il vous plaît.", entites[0].annee);
                    annee = int.Parse(Console.ReadLine());
                }
                return annee;
            }
            // Cas où l'utilisateur ne rentre pas une année
            catch
            {
                Console.Write("Veuillez renseigner une année entre {0} ", entites[entites.Length - 1].annee);
                Console.WriteLine("et {0} s'il vous plaît", entites[0].annee);
                int annee = saisirAnnee(entites);
                return annee;
            }
        }
        /* Demande et stocke la période voulue. 
           Entrée : Copie du fichier (entites). 
           Sortie : Année de début et Année de fin de période */
        public static void saisirPeriode(entite[] entites, out int annee1, out int annee2)
        {
            Console.WriteLine("Quelle est l'année de début de la période étudiée ?");
            annee1 = saisirAnnee(entites);
            Console.WriteLine("Quelle est l'année de fin de la période étudiée ?");
            annee2 = saisirAnnee(entites);
            // Remise dans l'ordre pour la suite du programme
            if (annee2 < annee1)
            {
                int tmp;
                tmp = annee1;
                annee1 = annee2;
                annee2 = tmp;
            }
        }
        /* Extrait les dix prénoms les plus utilisés du tableau entre les années 1 et 2.
           Entrées : Copie du fichier (entites), année de début et année de fin de période. 
           Sortie : Tableau contenant le top10 */
        public static entite[] topDix(entite[] entites, int annee1, int annee2) 
        {
            entite[] classement = classementPeriode(entites, annee1, annee2);
            entite[] topDix = new entite[10];
            int i = 0;
            while (i < 10)
            {
                topDix[i] = classement[i];
                i++;
            }
            return topDix;
        }
        // Affiche le tableau donné en entrée. 
        // Entrée : Tableau du classement à afficher
        public static void afficheFichier(entite[] entites)   
        {
            Console.WriteLine("Rang\tPrénom\t\tNombre");
            int longueur = entites.Length, i = 0;
            // Gestion de l'alignement des colonnes
            while (i < longueur)
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
        /* Donne le classement des prénoms sur une période donnée. 
           Entrées: Copie du fichier (entites), année de début et année de fin de la période. 
           Sortie : Classement des prénoms */
        public static entite[] classementPeriode(entite[] entites, int annee1, int annee2)
        {
            int j, n;
            /* On détermine d'abord combien il y a de prénoms différents
               dans les top 100 de la période donnée (=k) */
            int k = nbPrenomsDiff(entites, annee1, annee2, out j, out n);
            // On crée un nouveau tableau de la bonne taille (=k)
            entite[] entites2 = new entite[k];
            // On rentre les prénoms non-triés avec leurs totaux
            entites2 = rempliClassement(entites, entites2, j, n);
            // On trie le tableau afin d'obtenir le classement
            entites2 = triFusion(entites2, k);
            // On harmonise tous les rangs
            entites2 = reetiquetage(entites2, k);
            return entites2;
        }
        // Demande et stocke le nombre d'années N sur lequel calculer la tendance.
        // Entrée : Copie du fichier (entites)
        public static int saisirN(entite[] entites)
        {
            int dureeMaxAutorisee = entites[0].annee - entites[entites.Length - 1].annee - 1;
            try
            {
                int N = int.Parse(Console.ReadLine());
                // Cas où l'utilisateur rentre une durée non comprise dans le fichier
                while (N < 1 || N > dureeMaxAutorisee) 
                {
                    Console.Write("Veuillez renseigner une durée comprise entre 1 et {0} ", dureeMaxAutorisee);
                    Console.WriteLine("s'il vous plait.");
                    N = int.Parse(Console.ReadLine());
                }
                return N;
            }
            // Cas où l'utilisateur ne rentre pas un nombre
            catch
            {
                Console.Write("Veuillez renseigner une durée comprise entre 1 et {0} ", dureeMaxAutorisee);
                Console.WriteLine("s'il vous plait.");
                int N = saisirN(entites);
                return N;
            }
        }
        /* Créer un tableau des lignes ne concernant que le prénom choisi entre les dates données. 
           Entrées : Copie du fichier (entites), prenom concerné, année de début et année de fin de la période. 
           Sortie : Tableau des données associées au prénom choisi sur la période donnée */
        public static entite[] tSpePrenom(entite[] entites, string prenom, int anneeDebut, int anneeFin)
        {
            entite[] tab = new entite[(anneeDebut - anneeFin) + 1];
            int i = 0, j = 0;
            while (i < entites.Length)
            {
                if (entites[i].annee <= anneeDebut && entites[i].annee >= anneeFin)
                {
                    if (String.Equals(prenom, entites[i].prenom) == true)
                    {
                        tab[j] = entites[i];
                        j++;
                    }
                }
                i++;
            }
            return tab;
        }
        /* Calcule la moyenne du nombre de naissances par année sur une période donnée.
           Entrée : tableau concernant un prénom sur une période. 
           Sortie : moyenne */
        public static double moyenne(entite[] tab)
        {
            int i = 0;
            double moy = 0;
            while (i < tab.Length)
            {
                moy += tab[i].nbDeFoisDonne;
                i++;
            }
            moy = moy / tab.Length;
            return moy;
        }
        /* Calcule l'écart-type du tableau. 
           Entrées : tableau concernant un prénom sur une période, moyenne associée à ce tableau.
           Sortie : Ecart-type */
        public static double ecartType(entite[] tab, double moy)
        {
            double somme = 0.0;
            for (int i = 0; i < tab.Length; i++)
            {
                double delta = tab[i].nbDeFoisDonne - moy;
                somme += delta * delta;
            }
            return Math.Sqrt(somme / (tab.Length - 1));
        }
        /* Calcule et affiche la tendance d'un prénom sur les N dernières années.
           Entrées : Moyenne des N dernières années, moyenne des années précédentes,
                     écart-type d'attribution de ce prénom, le prenom. */
        public static void tendance(double m1, double m2, double E, string prenom)
        {
            double e = m2 - m1;
            if (e <= -(2 * E))
                Console.WriteLine("{0} est à l'abandon.", prenom);
            else if (e > -(2 * E) && e < -E)
                Console.WriteLine("{0} est désuet.", prenom);
            else if (e >= -E && e <= E)
                Console.WriteLine("{0} se maintient.", prenom);
            else if (e > E && e < (2 * E))
                Console.WriteLine("{0} est en vogue.", prenom);
            else
                Console.WriteLine("{0} explose !", prenom);
        }
        /* Affiche le top 100 d'une année donnée. 
           Entrées : Copie du fichier (entites),
                     rang du premier prénom de l'année concernée dans la copie du fichier */
        public static void afficheTopCentAnnee(entite[] entites, int longueur)  
        {
            Console.WriteLine("Rang\tPrénom\t\tNombre");
            int  i = longueur;
            // Gestion de l'alignement des colonnes
            while (i < longueur + 100)
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

        /* Vérifie si le prénom est déjà présent dans la nouvelle liste,
           en indiquant le rang de ce prénom si c'est le cas.
           Entrées : Copie du fichier (entites), rang jusqu'auquel vérifier, le prénom
           Sortie : Vrai si le prénom est déjà dans la liste, sinon Faux. Si vrai, le rang trouvé */
        public static bool estDejaSaisi(entite[] entites, int rgMax, string prenom, out int rgMPrenom)
        {            
            rgMPrenom = 0;
            while (rgMPrenom < rgMax)
            {
                if (String.Equals(entites[rgMPrenom].prenom, prenom) == true)
                    return true;
                rgMPrenom++;
            }
            return false;
        }
        /* Donne le nombre de prénom différents présent dans la copie du fichier, sur une période donnée
           Entrées : Copie du fichier (entites), année de début et année de fin de la période
           Sortie : Nombre de prénoms différents, rang du premier nom de la période, 
                    rang du dernier nom de la période*/
        public static int nbPrenomsDiff(entite[] entites, int annee1, int annee2, out int j, out int n)
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
            // Cet entier sert au dimensionnement d'un nouveau tableau de structure
            return k;
        }
        /* Rempli le nouveau tableau
           Entrées : Copie du fichier (entites), nouveau tableau à remplir, rang du premier nom de la période,
                     rang du dernier nom de la période
           Sortie : Nouveau tableau rempli */
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
        /* Actualise les nouveaux rang du tableau
           Entrées : Tableau à modifier, longueur du tableau
           Sortie : Tableau mis à jour */
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
        /* Algorithme de tri en O(n²) /!\ Moins efficient que triFusion, non utilisé ici. 
           Entrées : Tableau à trier, longueur du tableau
           Sortie : Tableau trié dans l'ordre décroissant pour le nombre de naissances */
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
        /* Algorithme de tri en O(n.log(n))
           Entrées : Tableau à trier, longueur du tableau
           Sortie : Tableau trié dans l'ordre décroissant pour le nombre de naissances */
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
        /* Fonction qui fusionne deux tableaux en une troisième triée (utilisée danle tri fusion)
           Entrées : deux tableaux à fusionner
           Sortie : un tableau trié issu des deux tableaux d'entrée */
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
    }
}
