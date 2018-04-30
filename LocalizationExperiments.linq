<Query Kind="Program">
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
	CultureInfo.CurrentCulture = new CultureInfo("sv-SE");
	Console.WriteLine(CultureInfo.CurrentCulture.IsDigraph("ny"));

	CultureInfo.CurrentCulture = new CultureInfo("pl-PL");
	Console.WriteLine(CultureInfo.CurrentCulture.IsDigraph("dź"));
	Console.WriteLine(CultureInfo.CurrentCulture.IsDigraph("dz"));

	var hu = new CultureInfo("hu-HU");
	Console.WriteLine(String.Compare("Ny", "NY", true, hu));
	Console.WriteLine(String.Compare("ny", "NY", true, hu));
	Console.WriteLine(String.Compare("ny", "Ny", true, hu));	
	Console.WriteLine(String.Compare("ny", "nY", true, hu));

	CultureInfo.CurrentCulture = hu;
	Console.WriteLine(StringInfo.ParseCombiningCharacters("ny").Length);
	
	Console.WriteLine(CultureInfo.CurrentCulture.IsDigraph("ny"));
	
	var di = DigraphsFactory.Create("hu-HU");
	Console.WriteLine(di.LanguageCode + " contains:");
	foreach(string s in (string[])di)
	{
		Console.Write(" " + s);
	}
	Console.WriteLine();
	string[] d2 = di;
	d2.GetType().Name.Dump();
}

// Define other methods and classes here
public static class LocalizationExtensions
{
	public static bool IsDigraph(this CultureInfo @this, string digraphA)
	{
		@this = @this ?? CultureInfo.CurrentCulture;

		var digraphDictionary = new Dictionary<string, string[]>
		{
			//{ "sv-SE", null },
			{ "hu-HU", new [] { "cs", "zs", "gy", "ly", "ny", "ty", "dz", "sz" } },
			{ "it-IT", new [] { "sc", "ch", "gh", "gl", "gn" } },
			{ "pl-PL", new [] { "ch", "cz", "dz", "dź", "dż", "rz", "sz" } }
		};

		if(digraphDictionary.TryGetValue(@this.Name, out string[] digraphs))
		{
			return digraphs?
				.Where(d => string.Compare(d, digraphA, @this,
					CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0)
				.Any() ?? false;
		}
		
		return false;
	}

	public static bool IsDigraph2(this CultureInfo @this, string digraphA)
	{
		@this = @this ?? CultureInfo.CurrentCulture;
		
		string[] digraphs = DigraphsFactory.Create(@this.Name);
		return digraphs
			.Where(d => string.Compare(d, digraphA, @this,
				CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0)
			.Any();
		
		
	}
}

public static class DigraphsFactory
{
	private static readonly Dictionary<string, Func<DigraphsBase>> _types =
		new Dictionary<string, Func<DigraphsBase>>
	{
		{ "hu-HU", () => new HungarianDigraphs()},
		{ "it-IT", () => new ItalianDigraphs()},
		{ "pl-PL", () => new PolishDigraphs()}
	};

	public static DigraphsBase Create(string languageCode)
	{
		return _types.ContainsKey(languageCode)
			? _types[languageCode]()
			: null;
	}
}

public abstract class DigraphsBase
{
//	protected static Dictionary<string, Func<DigraphsBase>> _types = new Dictionary<string, Func<DigraphsBase>>
//	{
//		{ "hu-HU", () => new HungarianDigraphs()},
//		{ "it-IT", () => new ItalianDigraphs()},
//		{ "pl-PL", () => new PolishDigraphs()}
//	};
	public string LanguageCode { get; protected set; }
	public string[] Digraphs { get; protected set; }
	//public static DigraphsBase Create(string languageCode) { return _types[languageCode](); }
	public static implicit operator string[] (DigraphsBase d) { return d.Digraphs; }
}

public class HungarianDigraphs : DigraphsBase
{
	public HungarianDigraphs()
	{
		LanguageCode = "hu-HU";
		Digraphs = new [] { "cs", "zs", "gy", "ly", "ny", "ty", "dz", "sz" };
	}
}

public class ItalianDigraphs : DigraphsBase
{
	public ItalianDigraphs()
	{
		LanguageCode = "it-IT";
		Digraphs = new [] { "sc", "ch", "gh", "gl", "gn" };
	}
}

public class PolishDigraphs : DigraphsBase
{
	public PolishDigraphs()
	{
		LanguageCode = "pl-PL";
		Digraphs = new [] { "ch", "cz", "dz", "dź", "dż", "rz", "sz" };
	}
}
