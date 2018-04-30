<Query Kind="Program" />

void Main()
{
	var q1 = new Queue<string>(new [] { "A" });
	q1.Enqueue("B");
	q1.Add("C");
	foreach(var s in q1)
		Console.WriteLine(s);
	
	var q2 = new Queue<int> { 1, 2, 3 };
	foreach(var s in q2)
		Console.WriteLine(s);

	var s1 = new Stack<int> { 1, 2, 3 };
	foreach(var s in s1)
		Console.WriteLine(s);

	var ll = new LinkedList<int> { 1, 2, 3 };
	foreach(var s in ll)
		Console.WriteLine(s);

	//InitializationExtensions.Add((Queue<int>)null, 4);
}

// Define other methods and classes here
static class InitializationExtensions
{
	public static void Add<T>(this Queue<T> queue, T value)
	{
		if(queue == null) throw new ArgumentNullException();
		queue.Enqueue(value);
	}

	public static void Add<T>(this Stack<T> stack, T value)
	{
		if(stack == null) throw new ArgumentNullException();
		stack.Push(value);
	}

	public static void Add<T>(this LinkedList<T> list, T value)
	{
		if(list == null) throw new ArgumentNullException();
		list.AddLast(value);
	}
}