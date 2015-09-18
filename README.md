# EF-Repository
Entity Framework implementation of a generic repository and specifications using AutoMapper. 
the easy way to use EntityFramework in terms of repositories and organize your queries in an enterprise project. 

**Use nuget**
```
Install-Package Framework.Data.EF
```

####Using without Dependency Injection
```
IRepository<User> repository = new Repository<User>(dbContext);
```
dbContext is your entity framework Context. 
#####Get
1. Using inline specification 
2. 
2. Using specification class

#####Insert

#####Update

#####Delete
