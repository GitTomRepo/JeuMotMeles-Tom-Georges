using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuMotMeles
{
    public class Joueur
    {
        private string name;
        private List<string> findWords;
        private int score;

        public Joueur (string nom)
        {
            this.name = nom;
            this.findWords = null;
            this.score = 0;
        }

        public void Add_Mot (string mot)
        {
            findWords.Add(mot);
        }

        public string ListFindWords ()
        {
            string listContent = "";
            if (this.findWords != null)
            {
                int scaleList = this.findWords.Count;

                for (int i = 0; i < scaleList; i++)
                {
                    listContent += this.findWords[i];
                }
            }
            else
            {
                listContent = "<NullListContent : Class Joueur - ListFindWords";
            }
            
            return listContent;
        }

        public void Add_Score (int val)
        {
            this.score += val;
        }

        public string ToString ()
        {
            return $"Nom : {this.name}\nMots trouvés : {ListFindWords()}\nScore : {this.score}";
        }
    }
}
