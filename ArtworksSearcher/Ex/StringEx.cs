using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworksSearcher.Ex
{
    public static class StringEx
    {
        public static double CalcSimilarityCoef(string query, string text)
        {
            query = query.ToLower();
            text = text.ToLower();
            string[] qTerms = query.Split(' ');
            double coef = 1;
            foreach (string term in qTerms)
            {
                if (text.Contains(term))
                    coef *= 10;
                else
                    coef *= CalcJacquardCoef(term, text);
            }
            return coef;
        }

        private static double CalcJacquardCoef(string a, string b)
        {
            if (a.Length == 0 && b.Length == 0)
                return double.PositiveInfinity;

            HashSet<char> setA = new HashSet<char>(a.ToCharArray());
            HashSet<char> setB = new HashSet<char>(b.ToCharArray());
            int aCount = setA.Count;
            int bCount = setB.Count;
            setA.IntersectWith(setB);
            int cCount = setA.Count;
            return (double)cCount / (aCount + bCount - cCount);
        }
    }
}
