# EF-Repository
Entity Framework implementation of a generic repository and specifications using AutoMapper. 
the easy way to use EntityFramework in terms of repositories and organize your queries in an enterprise project. 

**Use nuget**
```
Install-Package Framework.Data.EF
```

####Using
```
IRepository<User> repository = new Repository<User>(dbContext);
```
dbContext is your entity framework Context. 

you can then use the repository like this 
`repository.GetAll()`  
will return an Array of User as IEnumberable (not IQueriable). 

##### using specification Pattern
you can read more about spec pattern in this link https://en.wikipedia.org/wiki/Specification_pattern 

1- Create the spec class 
a simple select by Id class. 
```
public class ById : Specification<User>
{
    public ById(int id)
    {
      this.Expression = u => u.Id == id;
    }
}
```
2- use the Spec in the repository 
`repository.Get(new ById(1));` or 
`repository.First(new ById(1));` or 
`repository.Single(new ById(1));` 

##### using Automapper. 
1- create AutoMapper mapping 
`Mapper.Create<User,UserDto>()`
refere to the link for more details about automapper http://automapper.org/ 

2- use the Mapper in the repository 
`repository.GetAll<UserDto>();` or 
`repository.Get<UserDto>(new ById(1));`

##### Insert 
```
using(new UnitOfWork(DbContext)) 
{
  User usr = repository.Single(new ById(1));
  .... 
  repository.Add(usr);
}
```
##### Update 
```
using(new UnitOfWork(DbContext)) 
{
  User usr = new User();
  .... // do the changes needed in the using block
}
```
##### Delete using Select 
```
using(new UnitOfWork(DbContext)) 
{
 User usr = repository.Single(new ById(1));
 repository.Remove(usr); 
}
```
##### Delete using Spec
```
using(new UnitOfWork(DbContext)) 
{
 repository.Remove(new ById(1)); 
}
```

#### Using Dependency Injection
The Library is ment to be used using Dependency injection, however it can work without it.

You must create a registery for 3 types in your Container 
1- Map IRepository to Repository.
2- Map IUnitOfWorkFactory to UnitOfWorkFactory.
3- Map DbContext to your database Context.

make sure to use the same Scope / LifeTimeManager for all the 3 registry. 

you can then inject IRpository<User> and IUnitOfWorkFactory from your classes constractor 

to create a UnitOfWork you can use the method IUnitOfWorkFactory.Create() in the using block
```
using(_unitOfWorkFactory.Create())
{
 ...
}
```
