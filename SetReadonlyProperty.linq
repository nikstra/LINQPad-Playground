<Query Kind="Program" />

void Main()
{
	var f = new Foo();

	f.TestStubSetProperty(f => f.Bar, 42);
	f.TestStubSetProperty(f => f.Baz, "Hello world!");
	
	Console.WriteLine(f.Bar);
	Console.WriteLine(f.Baz);
}

// Define other methods, classes and namespaces here
public class Foo
{
	public int Bar { get; }
	public string Baz { get; }
}

internal static class TestExtensions
{
	public static void TestStubSetProperty<TSource, TProperty>(
		this TSource @this,
		Expression<Func<TSource, TProperty>> propertyExpression,
		TProperty value)
	{
		var propertyInfo = GetPropertyInfo(propertyExpression);
		if(propertyInfo.CanWrite)
		{
			propertyInfo.SetValue(@this, value);
		}
		else
		{
			var fieldInfo = GetAutoPropertyBackingFieldInfo(
				typeof(TSource), propertyInfo.Name);
			fieldInfo.SetValue(@this, value);
		}
	}
	
	private static PropertyInfo GetPropertyInfo<TSource, TProperty>(
		Expression<Func<TSource, TProperty>> propertyExpression)
	{
		var body = propertyExpression.Body as MemberExpression
			?? throw new ArgumentException("Not a member.", nameof(propertyExpression));
		return body.Member as PropertyInfo
			?? throw new ArgumentException("Not a property.", nameof(propertyExpression));
	}
	
	private static FieldInfo GetAutoPropertyBackingFieldInfo(Type type, string propertyName) =>
		type.GetField($"<{propertyName}>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance)
			?? throw new MissingFieldException($"No backing field found for auto property: {propertyName}.");
}