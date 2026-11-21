namespace IndependentWork8;

public class FileMetadataCache
{
    private readonly Dictionary<string, FileMetadata> _cache =
        new(StringComparer.OrdinalIgnoreCase);

    public int Count => _cache.Count;

    public void Add(string path, FileMetadata metadata)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        ArgumentNullException.ThrowIfNull(metadata);

        _cache[path] = metadata;
    }

    public FileMetadata Get(string path)
    {
        if (_cache.TryGetValue(path, out FileMetadata? metadata))
            return metadata;

        throw new KeyNotFoundException($"Метадані для '{path}' відсутні в кеші.");
    }

    public bool TryGet(string path, out FileMetadata? metadata) =>
        _cache.TryGetValue(path, out metadata);

    public bool Remove(string path) => _cache.Remove(path);

    public bool Contains(string path) => _cache.ContainsKey(path);
}
