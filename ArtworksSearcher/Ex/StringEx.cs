using System.Collections.Generic;

namespace ArtworksSearcher.Ex
{
    public static class StringEx
    {
        public static double CalcSimilarityCoef(string query, string text)
        {
            query = query.ToLower();
            text = text.ToLower();
            var qTerms = query.Split(' ');
            double coef = 1;
            foreach (var term in qTerms)
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

            var setA = new HashSet<char>(a.ToCharArray());
            var setB = new HashSet<char>(b.ToCharArray());
            var aCount = setA.Count;
            var bCount = setB.Count;
            setA.IntersectWith(setB);
            var cCount = setA.Count;
            return (double)cCount / (aCount + bCount - cCount);
        }
    }
}
