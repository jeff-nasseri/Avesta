# Avesta Data Repository
Avesta repository is a bunch of functions and classes help you to query and access to the data within a various ways and methods.

In this toturial I will tell you more about how you can use Avesta repository

Please read These toturial before you continue :
 - [Avesta Models](./Avesta.Models.md)
 - [Avesta Db Context](./Avesta.DbContext.md)

The repository of Avesta is based on BaseEntity and AvestaDbContext
First Lets take a look at the signature of Avesta IRepository for get data in IRepository.cs in the Avesta source code :
```csharp
    public interface IRepository<TEntity>
    {
        #region get entity
        Task<TEntity> GetByIdAsync(int id, bool track = true, bool exceptionRaseIfNotExist = false);
        Task<TEntity> GetByIdAsync(object key, bool track = true, bool exceptionRaseIfNotExist = false);
        Task<TEntity> GetEntity(Expression<Func<TEntity, bool>> predicate, bool exceptionRaseIfNotExist);
        Task<TEntity> GetEntity(string navigationPropertyPath, Expression<Func<TEntity, bool>> predicate, bool exceptionRaseIfNotExist);
        Task<TEntity> First(bool exceptionRaseIfNotExist);
        #endregion

        .
        .
        .
    }
```
As you can see there is various way just for get one entity in Avesta Repository. So how we can use this repository in our application?

# How we use Avesta Repository in our services
Here I create a simple service in .NET 5
```csharp
    public interface ICarService
    {
       Task<Car> GetCarById(object id);
    }
    public class CarService : ICarService
    {
       public Task<Car> GetCarById(object id)
       {
            //find the car and then return it
       }
    }
```
Now we want use Avesta repository to get data and complete our service : 
```csharp
    public interface ICarService
    {
       Task<Car> GetCarById(object id);
    }
    public class CarService : ICarService
    {
       readonly IRepository<Car> _carRepository;
       public CarService(IRepository<Car> carRepository)
       {
            _carRepository = carRepository;
       }

       public async Task<Car> GetCarById(object id)
       {
            //find the car and then return it
            var resule = await _carRepsitory.GetByIdAsync(id);
            return result;
       }
    }
```
So we used avesta repository to get car object by id, there is various ways to gather data information using Avesta repository and Avesta services, you will learn more about then !


[Now let's create a simple service and use built in service data access in Avesta](./../ServiceLayer/SimpleUseOfEntityService.md)