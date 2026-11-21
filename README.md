# Самостійна робота №14

**З дисципліни:** Об'єктно-орієнтоване програмування
**Тема:** `Dictionary<TKey, TValue>` та `HashSet<T>` у бібліотеках .NET
**Виконав:** студент Ткаченко Максим

---

## Опис

Консольний застосунок демонструє сценарії використання `Dictionary` та `HashSet`. Аналіз прикладів з open-source проєктів, порівняльна таблиця й висновки — у файлі [REPORT.md](REPORT.md).

## Структура проєкту

- `FileMetadata.cs`, `FileMetadataCache.cs` — **сценарій 1:** кеш метаданих файлів (`Dictionary<string, FileMetadata>`): `Add`, `Get`, `TryGet`, `Remove`, `Contains`.
- `TagManager.cs` — **сценарій 2:** унікальні теги (`Dictionary<string, HashSet<string>>`): `AddTag`, `RemoveTag`, `GetAllUniqueTags` (`UnionWith`), `FindCommonTags` (`IntersectWith`), `FindTagsOnlyInFirst` (`ExceptWith`).
- `SearchIndex.cs` — **сценарій 3 (додатковий):** інвертований індекс (`Dictionary<string, HashSet<int>>`): `AddDocument`, `Search`, `SearchMultiple`, `SearchAny`.
- `Person.cs` — клас `Person` та `PersonNameComparer : IEqualityComparer<Person>` (custom comparer).
- `Program.cs` — демонстрація всіх сценаріїв, custom comparer і порівняння швидкості пошуку `List` / `HashSet` / `Dictionary`.
- `REPORT.md` — звіт: приклади з open-source проєктів, таблиця, висновки.
- `.gitignore` — виключає службові файли .NET, VS Code та ОС.

## Запуск

```bash
dotnet run
```

## Приклад виводу програми

> ⚠️ Заповніть після запуску на своєму комп'ютері (час у блоці порівняння швидкості залежить від машини).

```
(встав сюди вивід консолі)
```

### Скріншот

(встав скріншот)
