# Create DbContext using Avesta Framework
So from the previous toturial after you create your own entity model you must create a DbContext and use your DbContext for data access ,Here is a simple DbContext in Efcore : 
```csharp
 public class MyApplicationDbContext : DbContext
 {
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Car> Model { get; set; }
 }
```
With the little help of Avesta you can extend AvestaDbContext in your application DbContext.
* Tips : We strongly suggest you to extend AvestaDbContext in your db context because it's very important in Repository layer of Avesta ,We will descuss about the repository later .

Here when u arrive MyApplicationDbContext from AvestaDbContext :   
```csharp
 public class MyApplicationDbContext : AvestaDbContext
 {
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Car> Model { get; set; }
 }
```
Lets take a look inside the AvestaDbContext :
```csharp
    public class AvestaDbContext : DbContext
    {
        public AvestaDbContext(DbContextOptions options)
            : base(options)
        {
        }
        protected virtual void OnModelCreating(ModelBuilder modelBuilder)
        {
            //use reflection to write this code
            //modelBuilder.Entity<Like>().HasQueryFilter(u => !u.DeletedDate.HasValue);
            base.OnModelCreating(modelBuilder);
        }
    }
``` 
So AvestaDbContext automaticlly exclude deleted entity in OnModel Creating.
* Tips : must of the functionality in Avesta framework is override able ,So you can change and custome them in your application .
