using Avesta.Data.Entity.Model;
using Avesta.Repository.Test.Src.Data.Context;
using Avesta.Repository.Test.Src.Data.Model;
using Avesta.Repository.Test.Src.Storage;
using NUnit.Framework.Internal;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Bson;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Avesta.Share.Extensions;
using Newtonsoft.Json;
using Avesta.Data.Entity.Context;
using Avesta.Repository.EntityRepository;
using Avesta.Exceptions.Entity;

namespace Avesta.Repository.Test
{


    //[TestFixture(typeof(string), typeof(Teacher), typeof(ApplicationDbContext))]
    //public class ReadEntityTest<TId, TEntity, TContext> : ServiceResolver<TId, TEntity, TContext>
    //    where TId : class
    //    where TEntity : BaseEntity<TId>
    //    where TContext : AvestaDbContext
    //{

    //    readonly IEntityRepository<TEntity, TId> _repository;
    //    readonly TContext _context;
    //    public ReadEntityTest()
    //    {
    //        _repository = base.ResolveRepository();
    //        _context = base.Context;
    //    }



    //    #region [- Get By Id -]

    //    [TestCaseSource(nameof(OnlyEntitySource))]
    //    public async Task GetById_MustReturnValidEntity(TId id)
    //    {
    //        var result = await _repository.Get(key: id, includeAllPath: false);
    //        Assert.That(result, Is.Not.Null);
    //    }



    //    [ExpectedException(typeof(CanNotFoundEntityException))]
    //    public async Task GetByIdWithUnvalidId_MustThrowCanNotFoundEntityException(TId id)
    //    {
    //        await _repository.Get(key: id, exceptionRaiseIfNotExist: true);
    //    }


    //    [TestCaseSource(nameof(InvalidIds))]
    //    public async Task GetByIdWithUnvalidId_WillReturnNull_IfExceptionRaseIsFalse(TId id)
    //    {
    //        var result = await _repository.Get(key: id, exceptionRaiseIfNotExist: false);
    //        Assert.That(result, Is.Null);
    //    }



    //    [TestCaseSource(nameof(OnlyEntitySource))]
    //    public async Task GetById_MustTrackEntity(TId id)
    //    {
    //        var result = await _repository.Get(id);
    //        Assert.That(_context.Entry(result).State, Is.Not.EqualTo(EntityState.Detached));
    //    }



    //    [TestCaseSource(nameof(OnlyEntitySource))]
    //    public async Task GetById_MustDetachEntity_IfTrackIsFalse(TId id)
    //    {
    //        var result = await _repository.Get(id, track: false);
    //        Assert.That(_context.Entry(result).State, Is.EqualTo(EntityState.Detached));
    //    }



    //    #endregion


    //    #region [- Get Entity -]

    //    [TestCaseSource(nameof(OnlyEntitySource))]
    //    public async Task GetEntity_MustReturnValidEntity(TId id)
    //    {
    //        var result = await _repository.Get(e => e.Id == id);
    //        Assert.That(result, Is.Not.Null);
    //    }




    //    [TestCaseSource(nameof(InvalidIds))]
    //    [ExpectedException(typeof(CanNotFoundEntityException))]
    //    public async Task GetEntityWithUnvalidId_MustThrowCanNotFoundEntityException(TId id)
    //    {
    //        await _repository.Get(e => e.Id == id, exceptionRaiseIfNotExist: true);
    //    }



    //    [TestCaseSource(nameof(InvalidIds))]
    //    public async Task GetEntityWithUnvalidId_WillReturnNull_IfExceptionRaseIsFalse(TId id)
    //    {
    //        var result = await _repository.Get(e => e.Id == id, exceptionRaiseIfNotExist: false);
    //        Assert.That(result, Is.Null);
    //    }




    //    [TestCaseSource(nameof(OnlyEntitySource))]
    //    public async Task GetEntity_MustTrackEntity(TId id)
    //    {
    //        var result = await _repository.Get(e => e.Id == id, track: true);
    //        Assert.That(_context.Entry(result).State, Is.Not.EqualTo(EntityState.Detached));
    //    }



    //    [TestCaseSource(nameof(OnlyEntitySource))]
    //    public async Task GetEntity_MustDetachEntity_IfTrackIsFalse(TId id)
    //    {
    //        var result = await _repository.Get(e => e.Id == id, track: false);
    //        Assert.That(_context.Entry(result).State, Is.EqualTo(EntityState.Detached));
    //    }




    //    [TestCaseSource(nameof(EntitySourceWithIncludePath))]
    //    public async Task GetEntityWithInclude_MustReturnValidIncludedEntity(TId id, string navigationPropertyPath)
    //    {
    //        var result = await _repository.Get(e => e.Id == id, navigationPropertyPath: navigationPropertyPath);
    //        Assert.That(result, Is.Not.Null);
    //    }




    //    [TestCaseSource(nameof(EntitySourceWithInvalidIncludePath))]
    //    [ExpectedException(typeof(InvalidOperationException))]
    //    public async Task GetEntityWithInvalidInclude_MustThrowInvalidOperationException(TId id, string navigationPropertyPath)
    //    {
    //        await _repository.Get(e => e.Id == id, navigationPropertyPath: navigationPropertyPath);
    //    }

    //    #endregion


    //    #region [- First Or Default -]
    //    [Test]
    //    public async Task First_MustReturnValidEntity()
    //    {
    //        var result = await _repository.First();
    //        Assert.That(result, Is.Not.Null);
    //    }

    //    [Test]
    //    public async Task FirstWithExpression_MustDetachEntity_IfTrackIsFalse()
    //    {
    //        var result = await _repository.First(track: false);
    //        Assert.That(_context.Entry(result).State, Is.EqualTo(EntityState.Detached));
    //    }



    //    [TestCaseSource(nameof(OnlyEntitySource))]
    //    public async Task FirstWithExpression_MustReturnValidEntity(TId id)
    //    {
    //        var result = await _repository.First(e => e.Id == id);
    //        Assert.That(result, Is.Not.Null);
    //    }


    //    [TestCaseSource(nameof(InvalidIds))]
    //    [ExpectedException(typeof(CanNotFoundEntityException))]
    //    public async Task FirstWithInvalidExpression_MustThrowCanNotFoundEntityException_IfExceptionRaiseIsTrue(TId id)
    //    {
    //        await _repository.First(e => e.Id == id, exceptionRaiseIfNotExist: true);
    //    }


    //    [TestCaseSource(nameof(OnlyEntitySource))]
    //    public async Task FirstWithExpression_MustDetachEntity_IfTrackIsFalse(TId id)
    //    {
    //        var result = await _repository.First(e => e.Id == id, track: false);
    //        Assert.That(_context.Entry(result).State, Is.EqualTo(EntityState.Detached));
    //    }
    //    #endregion


    //    #region [- Single Or Default -]

    //    [TestCaseSource(nameof(OnlyEntitySource))]
    //    public async Task Single_MustTrackEntity(TId id)
    //    {
    //        var result = await _repository.First(e => e.Id == id, track: true);
    //        Assert.That(_context.Entry(result).State, Is.Not.EqualTo(EntityState.Detached));
    //    }



    //    [TestCaseSource(nameof(OnlyEntitySource))]
    //    public async Task Single_MustDetachEntity_IfTrackIsFalse(TId id)
    //    {
    //        var result = await _repository.First(e => e.Id == id, track: false);
    //        Assert.That(_context.Entry(result).State, Is.EqualTo(EntityState.Detached));
    //    }




    //    [TestCaseSource(nameof(EntitySourceWithIncludePath))]
    //    public async Task SingleWithInclude_MustReturnValidIncludedEntity(TId id, string navigationPropertyPath)
    //    {
    //        var result = await _repository.First(e => e.Id == id, navigationPropertyPath: navigationPropertyPath);
    //        Assert.That(result, Is.Not.Null);
    //    }




    //    [TestCaseSource(nameof(EntitySourceWithInvalidIncludePath))]
    //    [ExpectedException(typeof(InvalidOperationException))]
    //    public async Task SingleWithInvalidInclude_MustThrowInvalidOperationException(TId id, string navigationPropertyPath)
    //    {
    //        await _repository.First(e => e.Id == id, navigationPropertyPath: navigationPropertyPath);
    //    }
    //    #endregion


    //    #region [- Get All Include All Children -]
    //    [Test]
    //    public async Task GetAllIncludeAllChildren_MustReturnValidEntity()
    //    {
    //        var result = await _repository.GetAll(includeAllPath: true);
    //        Assert.That(result, Is.Not.Null);
    //    }


    //    [Test]
    //    public async Task GetAllIncludeAllChildren_MustReturnValidEntityAndValidIncludedEntity()
    //    {
    //        var result = await _repository.GetAll(includeAllPath: true);
    //        var any = result.ValidateIncludeAllChildren<TEntity, BaseEntity>();
    //        Assert.IsTrue(any);
    //    }


    //    [Test]
    //    public async Task GetAllIncludeAllChildren_MustReturnValidEntityAsNoTracking_IfTrackIsFalse()
    //    {
    //        var result = await _repository.GetAll(track: false);

    //        foreach (var item in result)
    //        {
    //            Assert.That(_context.Entry(item).State == EntityState.Detached);
    //        }
    //    }




    //    [TestCaseSource(nameof(PaginationSource), new object[] { 2, 1 })]
    //    public async Task GetAllIncludeChildren_Pagination_MustReturnValidData(int page, int perPage, string expectedResult)
    //    {
    //        Assert.Warn("There is a tiny problem of pagination in repository. before gathering data you should paginate");
    //    }
    //    #endregion


    //    #region [- Get All -]



    //    [Test]
    //    public async Task GetAllAsync_MustBeMoreThenZero()
    //    {
    //        var result = await _repository.GetAll();
    //        Assert.That(result.Count, Is.GreaterThan(0));
    //    }



    //    [Test]
    //    public async Task GetAllAsync_MustHaveUniqueAndGuidFormattedId()
    //    {
    //        var result = await _repository.GetAll();
    //        foreach (var entity in result)
    //        {
    //            Assert.That(entity.Id.ToString(), Does.Match(@"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$"));
    //        }
    //    }


    //    #endregion


    //    #region [- Test Storage -]


    //    static IEnumerable<TestCaseData> InvalidIds()
    //    {
    //        return new List<TestCaseData>
    //        {
    //            new TestCaseData("INVALID_1"),
    //            new TestCaseData("INVALID_2"),
    //        };
    //    }


    //    static IEnumerable<TestCaseData> PaginationSource(int page, int perPage)
    //    {
    //        var result = SeedStorage.GetJsonOfDataInPage(page, perPage, typeof(TEntity));

    //        return new List<TestCaseData>
    //        {
    //            new TestCaseData(page,perPage,result)
    //        };
    //    }


    //    static IEnumerable<TestCaseData> OnlyEntitySource()
    //    {
    //        var lastId = SeedStorage.LastId(typeof(TEntity));
    //        var firstId = SeedStorage.FirstId(typeof(TEntity));

    //        return new List<TestCaseData>
    //        {
    //            new TestCaseData(lastId),
    //            new TestCaseData(firstId)
    //        };
    //    }


    //    static IEnumerable<TestCaseData> EntitySourceWithIncludePath()
    //    {
    //        var lastId = SeedStorage.LastId(typeof(TEntity));
    //        var firstId = SeedStorage.FirstId(typeof(TEntity));
    //        var path = SeedStorage.GetPath(typeof(TEntity));

    //        return new List<TestCaseData>
    //        {
    //            new TestCaseData(lastId, path),
    //            new TestCaseData(firstId, path)
    //        };
    //    }


    //    static IEnumerable<TestCaseData> EntitySourceWithInvalidIncludePath()
    //    {
    //        var lastId = SeedStorage.LastId(typeof(TEntity));
    //        var firstId = SeedStorage.FirstId(typeof(TEntity));
    //        var path = SeedStorage.GetPath(typeof(TEntity));

    //        return new List<TestCaseData>
    //        {
    //            new TestCaseData(lastId, "InvalidPathOne"),
    //            new TestCaseData(firstId, "InvalidPathTwo")
    //        };
    //    }
    //    #endregion


    //}

}








