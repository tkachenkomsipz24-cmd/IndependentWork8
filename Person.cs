namespace IndependentWork8;

public class Person
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }

    public override string ToString() => $"{Name} ({Age})";
}

public class PersonNameComparer : IEqualityComparer<Person>
{
    public bool Equals(Person? x, Person? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null || y is null) return false;

        return x.Name == y.Name;
    }

    public int GetHashCode(Person obj) => obj.Name.GetHashCode();
}
