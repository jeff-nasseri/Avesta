# Data layer of simple crud in Avesta framework web api
Avesta.Repository & Avesta.Data is the main libraries you can use them for design your data layer of your application. Currently Avesta only support Entity Framework Core as ORM.

### Lest start designing a simple data layer !
When you want to create various entity model in your data layer ,First of all you need to create a class of that model you want ,For example I want to create a basic Car Model in my data layer :
```csharp
public class Car
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Weight { get; set; }
    public int MaxSpeed { get; set; }    
}
```
The class Car above is a entity model in Efcore and I describe an id for it because as you know we need a unique number for each entity we store in our database. So what's next and how Avesta help me in creating data layer ?

Here is the code you must write in your application when you use Avesta as your framework :
```csharp
public class Car : BaseEntity<int>
{
    public string Name { get; set; }
    public int Weight { get; set; }
    public int MaxSpeed { get; set; }    
}
```
Actually Avesta help you to make an integer id for your model beside of the some usefull properties ,Lets see what is inside of the Avesta BaseEntity : 
```csharp
 public class BaseEntity<T> where T : class
 {
    public BaseEntity()
    {
        CreatedDate = DateTime.UtcNow;
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public T ID { get; set; }
    public bool IsLock { get; set; } = false;
    public DateTime? ModifiedDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
 }
```
The (T) is a generic type you can use it for set the type of id.
There is better way if you use BaseEntity<string> and make your id as UUID ,Because that's make a better system security for some kind of database attack like sql injection !.

* Tips : You can use buit in BaseEntity<string> in Avesta ,The counter assign a number for your entities automaticlly.

```csharp
    public class BaseEntity : BaseEntity<string>
    {
        public BaseEntity() : base()
        {
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Counter { get; set; }
    }
```

* Tips : There is some useful properties in BaseEntity ,We will go forture about that in this toturial

So That's it ,We write our first model entity with the little help of Avesta.
Now we must write our DbContext and register it in our application
[Create DbContent using Avesta](./CreateDBContext.md)

[Avesta.Data.Model Reference](./../../Reference/Avesta.Data/)

