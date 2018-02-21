<Query Kind="Program" />

void Main()
{
	IEnumerable<int> numberList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

	using(IEnumerator<int> enumerator = numberList.GetEnumerator())
	{
		bool last = !enumerator.MoveNext();
		
		while(!last)
		{
			int current = enumerator.Current;
			last = !enumerator.MoveNext();
			
			if(last)
			{
				Console.WriteLine("{0} is the last element", current);
			}
			else
			{
				Console.WriteLine(current);
			}
		}
	}

	using(IEnumerator<int> enumerator = numberList.GetEnumerator())
	{
		bool last = !enumerator.MoveNext();
		
		while(!last)
		{
			int current = enumerator.Current;
			last = !enumerator.MoveNext();
			
			if(last)
			{
				Console.WriteLine(current);
			}
			else
			{
				Console.Write("{0}, ", current);
			}
		}
	}

	using(IEnumerator<int> enumerator = numberList.GetEnumerator())
	{
		for(bool last = !enumerator.MoveNext(); !last;)
		{
			int current = enumerator.Current;
			last = !enumerator.MoveNext();
			
			Console.Write(current + (last ? Environment.NewLine : ", "));
		}
	}
}
