<Query Kind="Program" />

void Main()
{
	var strategy = new Strategy(new List<IStrategyFactory>
	{
		new StrategyTwoFactory2(),
		new StrategyOneFactory(),
		new StrategyTwoFactory(),
		new StrategyOneFactory2(),
		new StrategyThreeFactory()
	});
	
	for(var i = 0; i < 5; i++)
	{
		var providers = strategy.Execute((Strategies)i);
		var result = string.Join(",", providers.Select(p => p.GetValue()));
		
		if(!string.IsNullOrEmpty(result))
			Console.WriteLine(result);
	}
}

// Define other methods, classes and namespaces here
interface IStrategy
{
	IEnumerable<IProvider> Execute(Strategies strategy);
}

interface IProvider
{
	string GetValue();
}

interface IStrategyFactory
{
	bool AppliesTo(Strategies strategy);
	IProvider Create();
}

enum Strategies
{
	None,
	One,
	Two,
	Three
}

class Strategy : IStrategy
{
	private IList<IStrategyFactory> _factories;
	
	public Strategy(IList<IStrategyFactory> factories) => _factories = factories;
	
	public IEnumerable<IProvider> Execute(Strategies strategy) =>
		_factories.Where(s => s.AppliesTo(strategy)).Select(s => s.Create());
}

class OneProvider : IProvider
{
	public string GetValue() => "1";
}

class StrategyOneFactory : IStrategyFactory
{
	public bool AppliesTo(Strategies strategy) => strategy == Strategies.One;
	
	public IProvider Create() => new OneProvider();
}

class OneProvider2 : IProvider
{
	public string GetValue() => "1";
}

class StrategyOneFactory2 : IStrategyFactory
{
	public bool AppliesTo(Strategies strategy) => strategy == Strategies.One;
	
	public IProvider Create() => new OneProvider2();
}

class TwoProvider : IProvider
{
	public string GetValue() => "2";
}

class StrategyTwoFactory : IStrategyFactory
{
	public bool AppliesTo(Strategies strategy) => strategy == Strategies.Two;
	
	public IProvider Create() => new TwoProvider();
}

class TwoProvider2 : IProvider
{
	public string GetValue() => "2";
}

class StrategyTwoFactory2 : IStrategyFactory
{
	public bool AppliesTo(Strategies strategy) => strategy == Strategies.Three;
	
	public IProvider Create() => new TwoProvider2();
}

class ThreeProvider : IProvider
{
	public string GetValue() => "3";
}

class StrategyThreeFactory : IStrategyFactory
{
	public bool AppliesTo(Strategies strategy) => strategy == Strategies.Three;
	
	public IProvider Create() => new ThreeProvider();
}
