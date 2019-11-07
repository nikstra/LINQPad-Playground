void Main()
{
	IUnitOfWork uow1 = new UnitOfWork(new DbContext(), new RepositoryA(), new RepositoryB());
	IRepository ra = uow1.Repository<RepositoryA>();
	ra.GetType().Name.Dump();
	IUnitOfWork uow2 = new UnitOfWork(new DbContext(), new RepositoryA(), new RepositoryB(), new RepositoryC());
	IRepository rb = uow2.Repository<RepositoryB>();
	rb.GetType().Name.Dump();
	//IRepository rc = uow1.Repository<RepositoryC>();
}

// Define other methods, classes and namespaces here
interface IRepository {}
class RepositoryA : IRepository {}
class RepositoryB : IRepository {}
class RepositoryC : IRepository {}
class DbContext {}

interface IUnitOfWork
{
	T Repository<T>() where T : class, IRepository;
	void Commit();
}

abstract class UnitOfWorkBase : IUnitOfWork
{
	protected IReadOnlyDictionary<Type, object> _repositories;
	protected readonly DbContext _context;
	
	protected UnitOfWorkBase(DbContext context, params object[] repositories)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
		if(repositories == null || !repositories.Any())
		{
			throw new ArgumentException("Parameter is null or empty.", nameof(repositories));
		}

		_repositories = new Dictionary<Type, object>(repositories.ToDictionary(r => r.GetType()));
	}
	
	public T Repository<T>() where T : class, IRepository
	{
		if(_repositories.TryGetValue(typeof(T), out object repository))
		{
			return repository as T ??
				throw new ApplicationException($"Repository not of type {typeof(T).Name}.");
		}

		throw new ApplicationException($"{typeof(T).Name} not registered.");
	}
	
	public abstract void Commit();
}

class UnitOfWork : UnitOfWorkBase
{
	public UnitOfWork(DbContext context, RepositoryA ra)
		: base(context, ra)
	{
	}

	public UnitOfWork(DbContext context, RepositoryA ra, RepositoryB rb)
		: base(context, ra, rb)
	{
	}

	public UnitOfWork(DbContext context, RepositoryA ra, RepositoryB rb, RepositoryC rc)
		: base(context, ra, rb, rc)
	{
	}

	public override void Commit() {}
}
