using System.Diagnostics;
using System.Text;
using IndependentWork8;

Console.OutputEncoding = Encoding.UTF8;

static string Join<T>(IEnumerable<T> items) => string.Join(", ", items.OrderBy(x => x));

Console.WriteLine("=== Сценарій 1: FileMetadataCache (Dictionary) ===");
var fileCache = new FileMetadataCache();

fileCache.Add(@"C:\docs\report.docx", new FileMetadata
    { FileName = "report.docx", Size = 24_576, LastModified = new DateTime(2026, 11, 10), Hash = "a1b2c3" });
fileCache.Add(@"C:\docs\photo.png", new FileMetadata
    { FileName = "photo.png", Size = 512_000, LastModified = new DateTime(2026, 11, 15), Hash = "d4e5f6" });

Console.WriteLine($"Записів у кеші: {fileCache.Count}");
Console.WriteLine($"Get: {fileCache.Get(@"C:\docs\report.docx")}");
Console.WriteLine($"Contains (інший регістр шляху): {fileCache.Contains(@"c:\DOCS\REPORT.docx")}");

Console.WriteLine($"Remove photo.png: {fileCache.Remove(@"C:\docs\photo.png")}");
Console.WriteLine($"Contains photo.png після видалення: {fileCache.Contains(@"C:\docs\photo.png")}");

try
{
    fileCache.Get(@"C:\docs\missing.txt");
}
catch (KeyNotFoundException ex)
{
    Console.WriteLine($"KeyNotFoundException: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("=== Сценарій 2: TagManager (Dictionary + HashSet) ===");
var tags = new TagManager();

tags.AddTag("article1", "C#");
tags.AddTag("article1", "LINQ");
tags.AddTag("article1", "dotnet");
bool duplicate = tags.AddTag("article1", "c#"); 

tags.AddTag("article2", "C#");
tags.AddTag("article2", "generics");
tags.AddTag("article2", "dotnet");

tags.AddTag("article3", "python");
tags.AddTag("article3", "dotnet");

Console.WriteLine($"Повторне додавання \"c#\" до article1 вдалося: {duplicate}");
Console.WriteLine($"Усі унікальні теги: {Join(tags.GetAllUniqueTags())}");
Console.WriteLine($"Спільні теги article1 та article2: {Join(tags.FindCommonTags("article1", "article2"))}");
Console.WriteLine($"Теги тільки в article1 (відносно article2): {Join(tags.FindTagsOnlyInFirst("article1", "article2"))}");

tags.RemoveTag("article1", "LINQ");
Console.WriteLine($"Теги article1 після видалення LINQ: {Join(tags.GetTags("article1"))}");

Console.WriteLine();
Console.WriteLine("=== Сценарій 3: SearchIndex (інвертований індекс) ===");
var index = new SearchIndex();

index.AddDocument(1, "LINQ дозволяє фільтрувати колекції в C#");
index.AddDocument(2, "Dictionary і HashSet — узагальнені колекції в C#");
index.AddDocument(3, "HashSet зберігає лише унікальні елементи");

Console.WriteLine($"Search(\"HashSet\")                  -> {Join(index.Search("HashSet"))}");
Console.WriteLine($"Search(\"колекції\")                 -> {Join(index.Search("колекції"))}");
Console.WriteLine($"SearchMultiple(\"HashSet\", \"C\")   -> {Join(index.SearchMultiple("HashSet", "C"))}");
Console.WriteLine($"SearchAny(\"LINQ\", \"унікальні\")   -> {Join(index.SearchAny("LINQ", "унікальні"))}");
Console.WriteLine($"Search(\"Python\") (немає)          -> [{Join(index.Search("Python"))}]");

Console.WriteLine();
Console.WriteLine("=== Сценарій 4: custom IEqualityComparer ===");

var caseInsensitive = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
caseInsensitive["Hello"] = 1;
Console.WriteLine($"Dictionary (OrdinalIgnoreCase)[\"HELLO\"] = {caseInsensitive["HELLO"]}");

var people = new HashSet<Person>(new PersonNameComparer());
bool first = people.Add(new Person { Name = "John", Age = 25 });
bool second = people.Add(new Person { Name = "John", Age = 30 }); 
Console.WriteLine($"Додано John(25): {first}; додано John(30): {second}; у множині: {people.Count}");

var defaultPeople = new HashSet<Person>(); 
defaultPeople.Add(new Person { Name = "John", Age = 25 });
defaultPeople.Add(new Person { Name = "John", Age = 30 });
Console.WriteLine($"Без comparer (за посиланням) у множині: {defaultPeople.Count}");

Console.WriteLine();
Console.WriteLine("=== Порівняння швидкості пошуку ===");
const int N = 50_000;
const int Lookups = 1_000;

var list = Enumerable.Range(0, N).ToList();
var set = new HashSet<int>(list);
var dict = list.ToDictionary(x => x, x => x);
int target = N - 1;

var sw = Stopwatch.StartNew();
for (int i = 0; i < Lookups; i++) list.Contains(target);
sw.Stop();
Console.WriteLine($"List<int>.Contains        : {sw.Elapsed.TotalMilliseconds,10:F4} мс");

sw.Restart();
for (int i = 0; i < Lookups; i++) set.Contains(target);
sw.Stop();
Console.WriteLine($"HashSet<int>.Contains     : {sw.Elapsed.TotalMilliseconds,10:F4} мс");

sw.Restart();
for (int i = 0; i < Lookups; i++) dict.ContainsKey(target);
sw.Stop();
Console.WriteLine($"Dictionary.ContainsKey    : {sw.Elapsed.TotalMilliseconds,10:F4} мс");
Console.WriteLine($"(N = {N:N0} елементів, {Lookups:N0} пошуків останнього елемента)");
