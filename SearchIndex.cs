using System.Text.RegularExpressions;

namespace IndependentWork8;

public class SearchIndex
{
    private static readonly Regex WordRegex = new(@"[\p{L}\p{N}]+", RegexOptions.Compiled);

    private readonly Dictionary<string, HashSet<int>> _invertedIndex =
        new(StringComparer.OrdinalIgnoreCase);

    public void AddDocument(int docId, string content)
    {
        foreach (Match match in WordRegex.Matches(content))
        {
            string word = match.Value;

            if (!_invertedIndex.TryGetValue(word, out HashSet<int>? docs))
            {
                docs = new HashSet<int>();
                _invertedIndex[word] = docs;
            }

            docs.Add(docId); 
        }
    }

    public HashSet<int> Search(string word) =>
        _invertedIndex.TryGetValue(word, out HashSet<int>? docs)
            ? new HashSet<int>(docs)
            : new HashSet<int>();

    public HashSet<int> SearchMultiple(params string[] words)
    {
        if (words.Length == 0)
            return new HashSet<int>();

        HashSet<int> result = Search(words[0]);
        foreach (string word in words.Skip(1))
        {
            result.IntersectWith(Search(word));
            if (result.Count == 0)
                break; 
        }

        return result;
    }

    public HashSet<int> SearchAny(params string[] words)
    {
        var result = new HashSet<int>();
        foreach (string word in words)
            result.UnionWith(Search(word));

        return result;
    }
}
