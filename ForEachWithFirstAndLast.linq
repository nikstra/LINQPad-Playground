<Query Kind="Program" />

// https://stackoverflow.com/a/10735834
void Main()
{
	Console.WriteLine("Generic:");

	var genericNumberList = Enumerable.Range(0, 5).ToList();
	foreach(var item in genericNumberList.ForEachWithFirstAndLast())
	{
		var (value, first, last) = item;
		if(first)
		{
			Console.WriteLine($"{value} is the first item.");
		}
		else if(last)
		{
			Console.WriteLine($"{value} is the last item.");
		}
		else
		{
			Console.WriteLine(value);
		}
	}

	Console.WriteLine(Environment.NewLine + "Non-generic:");
	
	var numberList = new ArrayList() { 0, 1, 2, 3, 4 };
	foreach(var item in numberList.ForEachWithFirstAndLast())
	{
		var (value, first, last) = item;
		if(first)
		{
			Console.WriteLine($"{value} is the first item.");
		}
		else if(last)
		{
			Console.WriteLine($"{value} is the last item.");
		}
		else
		{
			Console.WriteLine(value);
		}
	}
}

// Define other methods, classes and namespaces here
public static class IEnumerableExtensions
{
	public static IEnumerable<(object, bool, bool)> ForEachWithFirstAndLast(this IEnumerable @this) =>
		ForEachWithFirstAndLast<object>(@this.Cast<object>());

	public static IEnumerable<(T, bool, bool)> ForEachWithFirstAndLast<T>(this IEnumerable<T> @this)
	{
		using(var enumerator = @this.GetEnumerator())
		{
			bool isFirst = false;
			if(enumerator.MoveNext())
			{
				isFirst = true;
				T last = enumerator.Current;
				while(enumerator.MoveNext())
				{
					// here, "last" is a non-final value; do something with "last"
					yield return (last, isFirst, false);
					isFirst = false;
					last = enumerator.Current;
				}
				// here, "last" is the FINAL one; do something else with "last"
				yield return (last, false, true);
			}
		}
	}
	
	//public static IEnumerable<(object, bool, bool)> ForEachWithFirstAndLast(this IEnumerable @this)
	//{
	//	var enumerator = @this.GetEnumerator();
	//	using(enumerator as IDisposable)
	//	{
	//		bool isFirst = false;
	//		if(enumerator.MoveNext())
	//		{
	//			isFirst = true;
	//			object last = enumerator.Current;
	//			while(enumerator.MoveNext())
	//			{
	//				// here, "last" is a non-final value; do something with "last"
	//				yield return (last, isFirst, false);
	//				isFirst = false;
	//				last = enumerator.Current;
	//			}
	//			// here, "last" is the FINAL one; do something else with "last"
	//			yield return (last, false, true);
	//		}
	//	}
	//}
}
