<Query Kind="Program" />

void Main()
{
	SpaceTime st = new SpaceTime();
	Console.WriteLine(st.ToString());

	SpaceTime st2 = new SpaceTime(1, 2, 3, DateTime.Now);
	Console.WriteLine(st2.ToString());

	SpaceTime st3 = st2;
	st2.X = 10;
	Console.WriteLine(st3.ToString());

	typeof(SpaceTime).IsValueType.Dump();
}

// Define other methods and classes here
public struct SpaceTime
{
	public SpaceTime(int x, int y, int z, DateTime t)
	{
		X = x;
		Y = y;
		Z = z;
		T = t;
	}
	
	public int X { get; set; }
	public int Y { get; set; }
	public int Z { get; set; }
	public DateTime T { get; set; }
	
	public override string ToString()
	{
		return $"{X}, {Y}, {Z}, {T}";
	}
}