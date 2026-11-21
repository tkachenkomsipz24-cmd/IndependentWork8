namespace IndependentWork8;

public class TagManager
{
    private readonly Dictionary<string, HashSet<string>> _articleTags = new();

    public bool AddTag(string articleId, string tag)
    {
        if (!_articleTags.TryGetValue(articleId, out HashSet<string>? tags))
        {
            tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            _articleTags[articleId] = tags;
        }

        return tags.Add(tag);
    }

    public bool RemoveTag(string articleId, string tag) =>
        _articleTags.TryGetValue(articleId, out HashSet<string>? tags) && tags.Remove(tag);

    public HashSet<string> GetTags(string articleId) =>
        _articleTags.TryGetValue(articleId, out HashSet<string>? tags)
            ? new HashSet<string>(tags, StringComparer.OrdinalIgnoreCase)
            : new HashSet<string>(StringComparer.OrdinalIgnoreCase);

    public HashSet<string> GetAllUniqueTags()
    {
        var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (HashSet<string> tags in _articleTags.Values)
            result.UnionWith(tags);

        return result;
    }

    public HashSet<string> FindCommonTags(string articleId1, string articleId2)
    {
        HashSet<string> result = GetTags(articleId1);
        result.IntersectWith(GetTags(articleId2));
        return result;
    }

    public HashSet<string> FindTagsOnlyInFirst(string articleId1, string articleId2)
    {
        HashSet<string> result = GetTags(articleId1);
        result.ExceptWith(GetTags(articleId2));
        return result;
    }
}
