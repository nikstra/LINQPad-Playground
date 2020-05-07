<Query Kind="Program" />

void Main()
{
	var f = new Foo();
	IContravariant<Foo> f2 = f;
	IContravariant<FooBar> f3 = f;

	var fb = new FooBar();
	IContravariant<Foo> fb2 = fb;
	IContravariant<FooBar> fb3 = fb;
	
	IProduct<Radio> p = Factory<TV>.Create();
	p.GetType().Dump();
	
	IProduct<Apple> t = Factory<Onion>.Create();
	t.GetType().Dump();

	IProduct<FlatscreenTV> e = Factory<WidescreenTV>.Create();
	e.GetType().Dump();

	IProduct<DABRadio> r = Factory<WidescreenTV>.Create();
	r.GetType().Dump();
}

// Define other methods and classes here
interface IContravariant<in T> {}
class Foo : IContravariant<Foo> {}
class FooBar : Foo {}

interface IProduct<in TProduct> {}

class Electronics : IProduct<Electronics> {}
class Radio : Electronics {}
class AnalogRadio : Radio {}
class DABRadio : Radio {}
class TV : Electronics {}
class WidescreenTV : TV {}
class FlatscreenTV : TV {}

class Produce : IProduct<Produce> {}
class Apple : Produce {}
class Onion : Produce {}

class Factory<T> where T
	: IProduct<T>, new()
{
	public static T Create() => new T();
}