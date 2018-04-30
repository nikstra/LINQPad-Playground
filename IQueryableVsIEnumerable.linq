<Query Kind="Program">
  <Connection>
    <ID>c5f54f38-c536-42b4-bcb0-b35dbddda886</ID>
    <Persist>true</Persist>
    <Server>(localdb)\MSSQLLocalDB</Server>
    <Database>EFRespositoryTest</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

// I saw, in a tutorial that the developer was using IEnumerable<T> GetAll() as
// a public method to get all entities from the database. I beleive this is
// a good practice since it doesn't leak DbContext or DbSet to the caller.
//
// But then, when applying the DRY pattern, he reused the GetAll() method
// internally in other methods and then using Where() to filter the results.
// As I suspected, this results in a database query for all entities that
// then will be filtered in memory which seemed like unnecessary load on the
// database. My solution is to use an IQueryable<T> GetAll() method for internal
// use in the class, thus the filtering is done in the database and no
// unnecessary data is beeing sent. The code below is proof of that.
//
// Note: This is a simplified example. The original code was using Include()
// to eagerly load related entities.

void Main()
{
	// 'this' is the DataContext in LINQPad.
	var repo = new PersonRepository(this);

	// Filtering is done in memory.
	foreach(var person in repo.GetAll().Where(p => p.Age > 35))
		person.Dump();

	// Filtering is done with SQL in the database. 
	foreach(var person in repo.GetOlderThan(35))
		person.Dump();
}

// Define other methods and classes here
public class PersonRepository
{
	private UserQuery _context;
	
	public PersonRepository(UserQuery context)
	{
		_context = context;
	}

	public IEnumerable<Persons> GetAll()
	{
		// This method generates the following SQL query:
		// SELECT [t0].[Id], [t0].[Name], [t0].[Age]
		// FROM [Persons] AS [t0]
		return _context.Persons;
	}
	
	public IEnumerable<Persons> GetOlderThan(int age)
	{
		// This method generates the following SQL query:
		// -- Region Parameters
		// DECLARE @p0 Int = 35
		// -- EndRegion
		// SELECT [t0].[Id], [t0].[Name], [t0].[Age]
		// FROM [Persons] AS [t0]
		// WHERE [t0].[Age] > @p0
		return GetAllQueryable()
			.Where(p => p.Age > age);
	}
	
	protected IQueryable<Persons> GetAllQueryable()
	{
		return _context.Persons;
	}
}