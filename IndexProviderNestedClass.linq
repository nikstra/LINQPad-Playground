<Query Kind="Program" />

void Main()
{
	var f = new Foo();
	f.Add("Hello");
	f.Add("World");
	f.Index[0].Dump();
	f.Index[1].Dump();
	f.Index[1] = "nikstra";
	f.Index[0].Dump();
	f.Index[1].Dump();
}

// Define other methods and classes here
public class Foo
{
	protected List<string> _list = new List<string>();
	public class IndexProvider
	{
		private Foo _foo;
		public IndexProvider(Foo foo) { _foo = foo; }
		public string this[int v]
		{
			get { return _foo._list[v]; }
			set { _foo._list[v] = value; }
		}
	}

	public IndexProvider Index { get; set; }
	
	public Foo()
	{
		Index = new IndexProvider(this);
	}
	
	public void Add(string s)
	{
		_list.Add(s);
	}
}