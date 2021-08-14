using System.Collections.Generic;

namespace Module.ArtworksSearcher.Helpers
{
    public static class StringHelper
    {
        public static double CalcSimilarityCoefficient(string query, string text)
        {
            query = query.ToLower();
            text = text.ToLower();
            var qTerms = query.Split(' ');
            double coefficient = 1;
            foreach (var term in qTerms)
            {
                if (text.Contains(term))
                    coefficient *= 10;
                else
                    coefficient *= CalcJacquardCoefficient(term, text);
            }
            return coefficient;
        }

        private static double CalcJacquardCoefficient(string a, string b)
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
