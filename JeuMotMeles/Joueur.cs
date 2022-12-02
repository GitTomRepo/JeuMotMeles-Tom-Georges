using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuMotMeles
{
    public class Joueur 
    {
        // Declaration de toutes les variables de classe
        private string name;
        private List<string> findWords;
        private int score;

        public Joueur (string nom) // Constructeur de la classe Joueur
        {
            // Toutes les variables sont initialisees à une valeur par défault
            this.name = nom;
            this.findWords = null;
            this.score = 0;
        }

        /// <summary>
        /// Fonction qui ajoute un mot dans la liste des mots trouves
        /// </summary>
        /// <param name="mot"> Mot a ajouter a la liste de mots trouves </param>
        public void Add_Mot (string mot)
        {
            findWords.Add(mot); // Ajout de mot dans la liste des mots trouves
        }

        /// <summary>
        /// Fonction qui parcours toute la liste des mots trouves
        /// </summary>
        /// <returns> Retourn les mots trouves sur une ligne </returns>
        public string ListFindWords ()
        {
            string listContent = "";
            if (this.findWords != null) // Liste des mots trouves non null
            {
                int scaleList = this.findWords.Count;

                for (int i = 0; i < scaleList; i++)
                {
                    listContent += this.findWords[i];
                }
            }
            else // Liste des mots trouves null
            {
                listContent = "<NullListContent : Class Joueur - ListFindWords";
            }
            
            return listContent;
        }

        /// <summary>
        /// Fonction qui permet de mettre a jour le score du joueur
        /// </summary>
        /// <param name="val"> Valeur a ajouter au score actuel </param>
        public void Add_Score (int val)
        {
            this.score += val;
        }

        /// <summary>
        /// Fonction qui met en forme les informations de la classe Joueur
        /// </summary>
        /// <returns> Chaine de caracteres avec : nom - mots trouves - score </returns>
        public string ToString ()
        {
            return $"Nom : {this.name}\nMots trouvés : {ListFindWords()}\nScore : {this.score}"; // Formatage de la chaine de caractere sur 3 lignes
        }
    }
}
