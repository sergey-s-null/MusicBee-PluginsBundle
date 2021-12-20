using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Root.Helpers
{
    public static class UriHelper
    {
        public static Uri GetCommonUriPart(Uri first, Uri second)
        {
            if (first.Scheme != second.Scheme)
            {
                throw new ArgumentException($"Schemes of uris are not equals. " +
                                            $"Schemes: {first.Scheme}, {second.Scheme}. " +
                                            $"First Uri: {first}. " +
                                            $"Second Uri: {second}.");
            }
            
            var firstSegments = first.Segments;
            var commonSegmentsCount = GetCommonSegmentsCount(firstSegments, second.Segments);
            
            var commonSegments = firstSegments
                .Take(commonSegmentsCount)
                .ToArray();
            
            return BuildUriFromSegments(commonSegments, first.Scheme);
        }

        public static (Uri Common, IReadOnlyList<Uri> Particulars) SplitOnCommonAndParticulars(IReadOnlyList<Uri> uris)
        {
            if (uris.Count == 0)
            {
                throw new ArgumentException("Uris is empty.");
            }
            
            CheckUrisForEqualsScheme(uris);
            
            var segmentsCollection = uris.Select(x => x.Segments).ToReadOnlyList();

            var commonSegmentsCount = GetCommonSegmentsCount(segmentsCollection);

            var commonSegments = segmentsCollection[0]
                .Take(commonSegmentsCount)
                .ToReadOnlyCollection();
            var common = BuildUriFromSegments(commonSegments, uris[0].Scheme);

            var particulars = segmentsCollection
                .Select(x => x.Skip(commonSegmentsCount).ToReadOnlyCollection())
                .Select(x => BuildUriFromSegments(x))
                .ToReadOnlyList();

            return (common, particulars);
        }

        private static void CheckUrisForEqualsScheme(IReadOnlyList<Uri> uris)
        {
            for (var i = 1; i < uris.Count; i++)
            {
                if (uris[i - 1].Scheme != uris[i].Scheme)
                {
                    throw new ArgumentException($"Schemes of uris are not equals. " +
                                                $"Schemes: {uris[i - 1].Scheme}, {uris[i].Scheme}. " +
                                                $"First Uri: {uris[i - 1]}. " +
                                                $"Second Uri: {uris[i]}.");
                }
            }
        }
        
        private static int GetCommonSegmentsCount(IReadOnlyList<string> firstSegments, IReadOnlyList<string> secondSegments)
        {
            return GetCommonSegmentsCount(new[] { firstSegments, secondSegments });
        }

        private static int GetCommonSegmentsCount(IReadOnlyList<IReadOnlyList<string>> segmentsCollection)
        {
            var minSegmentsCount = segmentsCollection.Select(x => x.Count).Min();
            
            for (var i = 0; i < minSegmentsCount; i++)
            {
                for (var j = 1; j < segmentsCollection.Count; j++)
                {
                    if (segmentsCollection[j - 1][i] != segmentsCollection[j][i])
                    {
                        return i;
                    }
                }
            }

            return 0;
        }
        
        private static Uri BuildUriFromSegments(IEnumerable<string> segments, string? scheme = null)
        {
            var builder = new StringBuilder();
            if (scheme is not null)
            {
                builder.Append(scheme);
                builder.Append("://");
            }

            var isFirst = true;
            foreach (var segment in segments)
            {
                if (isFirst)
                {
                    if (segment == "/" && scheme is not null
                        || segment != "/")
                    {
                        builder.Append(segment);
                    }
                    
                    isFirst = false;
                }
                else
                {
                    builder.Append(segment);
                }
            }

            return new Uri(builder.ToString(), UriKind.RelativeOrAbsolute);
        }
    }
}