<Query Kind="Program" />

void Main()
{
	char[] invalidPathChars = Path.GetInvalidPathChars();
	Console.WriteLine($"Invalid char count: {invalidPathChars.Length}");
	Utilities.PrintChars(invalidPathChars, 5);

	Console.WriteLine();
	
	char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
	Console.WriteLine($"Invalid char count: {invalidFileNameChars.Length}");
	Utilities.PrintChars(invalidFileNameChars, 7);

	Console.WriteLine();

	string path = @"C:\Program Files";
	string file = "some file.txt";
	
	if(path.IndexOfAny(invalidPathChars) == -1)
		Console.WriteLine("Path OK");

	if(file.IndexOfAny(invalidFileNameChars) == -1)
		Console.WriteLine("Filename OK");

}

// Define other methods and classes here

static class Utilities
{
	public static void PrintChars(char[] chars, int columns = 5)
	{
		foreach(var c in chars.Select((value, i) => new { value, i }))
		{
			Console.Write($"0x{Convert.ToUInt32(c.value).ToString("X2")} '{(Char.IsControl(c.value) ? ' ' : c.value )}' ");
			if((c.i + 1) % columns == 0) Console.WriteLine();
		}
		Console.WriteLine();
	}
}