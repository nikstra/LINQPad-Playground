<Query Kind="Program" />

void Main()
{
    var type = typeof(Foo);
    var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
    
    var attributesMap = properties
        .SelectMany(p =>
            Attribute.GetCustomAttributes(p, typeof(PropertyAliasAttribute)),
            (p, a) => new { Alias = (PropertyAliasAttribute)a, Property = p })
        .ToDictionary(x => x.Alias.Name, x => x.Property.Name);

    foreach(var mapping in attributesMap)
    {
        Console.WriteLine($"{mapping.Key} is an alias for {mapping.Value}");
    }
}

// Define other methods, classes and namespaces here
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public class PropertyAliasAttribute : Attribute
{
    public string Name { get; }
    
    public PropertyAliasAttribute(string name) =>
        Name = name;
}

public class FooAttribute : Attribute {}

public class Foo
{
    [PropertyAlias("BarAlias1")]
    [PropertyAlias("BarAlias2")]
    public string Bar { get; set; }

    [Foo]
    [PropertyAlias("BazAlias1")]
    public string Baz { get; set; }
}
