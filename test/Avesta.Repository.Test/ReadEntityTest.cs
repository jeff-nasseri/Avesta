using Avesta.Data.Context;
using Avesta.Data.Model;
using Avesta.Repository.EntityRepository;
using Avesta.Repository.Test.Src.Data.Context;
using Avesta.Repository.Test.Src.Data.Model;
using Avesta.Repository.Test.Src.Storage;
using NUnit.Framework.Internal;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Avesta.Exceptions.Entity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Bson;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Avesta.Share.Extensions;
using Newtonsoft.Json;

namespace Avesta.Repository.Test
{


    //[TestFixture(typeof(School), typeof(ApplicationDbContext))]
    [TestFixture(typeof(Student), typeof(ApplicationDbContext))]
    //[TestFixture(typeof(Teacher), typeof(ApplicationDbContext))]
    //[TestFixture(typeof(Teacher_School), typeof(ApplicationDbContext))]
    public class ReadEntityTest<TEntity, TContext> : ServiceResolver<TEntity, TContext>
        where TEntity : BaseEntity
        where TContext : AvestaDbContext
    {

        readonly IRepository<TEntity> _repository;
        readonly TContext _context;
        public ReadEntityTest()
        {
            _repository = base.ResolveRepository();
            _context = base.Context;
        }



        #region [- Get By Id -]

        [TestCaseSource(nameof(OnlyEntitySource))]
        public async Task GetById_MustReturnValidEntity(string id)
        {
            var result = await _repository.GetById(id);
            Assert.That(result, Is.Not.Null);
        }




        [TestCase("Not_Valid_ID_1")]
        [TestCase("Not_Valid_ID_2")]
        [ExpectedException(typeof(CanNotFoundEntityException))]
        public async Task GetByIdWithUnvalidId_MustThrowCanNotFoundEntityException(string id)
        {
            await _repository.GetById(id, exceptionRaiseIfNotExist: true);
        }



        [TestCase("Not_Valid_ID_1")]
        [TestCase("Not_Valid_ID_2")]
        public async Task GetByIdWithUnvalidId_WillReturnNull_IfExceptionRaseIsFalse(string id)
        {
            var result = await _repository.GetById(id, exceptionRaiseIfNotExist: false);
            Assert.That(result, Is.Null);
        }



        [TestCaseSource(nameof(OnlyEntitySource))]
        public async Task GetById_MustTrackEntity(string id)
        {
            var result = await _repository.GetById(id);
            Assert.That(_context.Entry(result).State, Is.Not.EqualTo(EntityState.Detached));
        }



        [TestCaseSource(nameof(OnlyEntitySource))]
        public async Task GetById_MustDetachEntity_IfTrackIsFalse(string id)
        {
            var result = await _repository.GetById(id, track: false);
            Assert.That(_context.Entry(result).State, Is.EqualTo(EntityState.Detached));
        }



        #endregion


        #region [- Get Entity -]

        [TestCaseSource(nameof(OnlyEntitySource))]
        public async Task GetEntity_MustReturnValidEntity(string id)
        {
            var result = await _repository.GetEntity(e => e.ID == id);
            Assert.That(result, Is.Not.Null);
        }





        [TestCase("Not_Valid_ID_1")]
        [ExpectedException(typeof(CanNotFoundEntityException))]
        public async Task GetEntityWithUnvalidId_MustThrowCanNotFoundEntityException(string id)
        {
            await _repository.GetEntity(e => e.ID == id, exceptionRaiseIfNotExist: true);
        }



        [TestCase("Not_Valid_ID_1")]
        public async Task GetEntityWithUnvalidId_WillReturnNull_IfExceptionRaseIsFalse(string id)
        {
            var result = await _repository.GetEntity(e => e.ID == id, exceptionRaiseIfNotExist: false);
            Assert.That(result, Is.Null);
        }




        [TestCaseSource(nameof(OnlyEntitySource))]
        public async Task GetEntity_MustTrackEntity(string id)
        {
            var result = await _repository.GetEntity(e => e.ID == id, track: true);
            Assert.That(_context.Entry(result).State, Is.Not.EqualTo(EntityState.Detached));
        }



        [TestCaseSource(nameof(OnlyEntitySource))]
        public async Task GetEntity_MustDetachEntity_IfTrackIsFalse(string id)
        {
            var result = await _repository.GetEntity(e => e.ID == id, track: false);
            Assert.That(_context.Entry(result).State, Is.EqualTo(EntityState.Detached));
        }




        [TestCaseSource(nameof(EntitySourceWithIncludePath))]
        public async Task GetEntityWithInclude_MustReturnValidIncludedEntity(string id, string navigationIncludePath)
        {
            var result = await _repository.GetEntity(navigationIncludePath, e => e.ID == id);
            Assert.That(result, Is.Not.Null);
        }




        [TestCaseSource(nameof(EntitySourceWithInvalidIncludePath))]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task GetEntityWithInvalidInclude_MustThrowInvalidOperationException(string id, string navigationIncludePath)
        {
            await _repository.GetEntity(navigationIncludePath, e => e.ID == id);
        }

        #endregion


        #region [- First Or Default -]
        [Test]
        public async Task FirstOrDefault_MustReturnValidEntity()
        {
            var result = await _repository.FirstOrDefault();
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task FirstOrDefaultWithExpression_MustDetachEntity_IfTrackIsFalse()
        {
            var result = await _repository.FirstOrDefault(track: false);
            Assert.That(_context.Entry(result).State, Is.EqualTo(EntityState.Detached));
        }



        [TestCaseSource(nameof(OnlyEntitySource))]
        public async Task FirstOrDefaultWithExpression_MustReturnValidEntity(string id)
        {
            var result = await _repository.FirstOrDefault(e => e.ID == id);
            Assert.That(result, Is.Not.Null);
        }


        [TestCase("Invalid_Id_1")]
        [TestCase("Invalid_Id_2")]
        [ExpectedException(typeof(CanNotFoundEntityException))]
        public async Task FirstOrDefaultWithInvalidExpression_MustThrowCanNotFoundEntityException_IfExceptionRaiseIsTrue(string id)
        {
            await _repository.FirstOrDefault(e => e.ID == id, exceptionRaiseIfNotExist: true);
        }


        [TestCaseSource(nameof(OnlyEntitySource))]
        public async Task FirstOrDefaultWithExpression_MustDetachEntity_IfTrackIsFalse(string id)
        {
            var result = await _repository.FirstOrDefault(e => e.ID == id, track: false);
            Assert.That(_context.Entry(result).State, Is.EqualTo(EntityState.Detached));
        }
        #endregion


        #region [- Single Or Default -]

        [TestCaseSource(nameof(OnlyEntitySource))]
        public async Task SingleOrDefault_MustTrackEntity(string id)
        {
            var result = await _repository.SingleOrDefault(e => e.ID == id, track: true);
            Assert.That(_context.Entry(result).State, Is.Not.EqualTo(EntityState.Detached));
        }



        [TestCaseSource(nameof(OnlyEntitySource))]
        public async Task SingleOrDefault_MustDetachEntity_IfTrackIsFalse(string id)
        {
            var result = await _repository.SingleOrDefault(e => e.ID == id, track: false);
            Assert.That(_context.Entry(result).State, Is.EqualTo(EntityState.Detached));
        }




        [TestCaseSource(nameof(EntitySourceWithIncludePath))]
        public async Task SingleOrDefaultWithInclude_MustReturnValidIncludedEntity(string id, string navigationIncludePath)
        {
            var result = await _repository.SingleOrDefault(navigationIncludePath, e => e.ID == id);
            Assert.That(result, Is.Not.Null);
        }




        [TestCaseSource(nameof(EntitySourceWithInvalidIncludePath))]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task SingleOrDefaultWithInvalidInclude_MustThrowInvalidOperationException(string id, string navigationIncludePath)
        {
            await _repository.SingleOrDefault(navigationIncludePath, e => e.ID == id);
        }
        #endregion


        #region [- Get All Include All Children -]
        [Test]
        public async Task GetAllIncludeAllChildren_MustReturnValidEntity()
        {
            var result = await _repository.GetAllIncludeAllChildren();
            Assert.That(result, Is.Not.Null);
        }


        [Test]
        public async Task GetAllIncludeAllChildren_MustReturnValidEntityAndValidIncludedEntity()
        {
            var result = await _repository.GetAllIncludeAllChildren();
            var any = result.CheckTheValueOfEntity<TEntity, BaseEntity>();
            Assert.IsTrue(any);
        }


        [Test]
        public async Task GetAllIncludeAllChildren_MustReturnValidEntityAsNoTracking_IfTrackIsFalse()
        {
            var result = await _repository.GetAllIncludeAllChildren(track: false);

            foreach (var item in result)
            {
                Assert.That(_context.Entry(item).State == EntityState.Detached);
            }
        }




        [TestCaseSource(nameof(PaginationSource), new object[] { 2, 1 })]
        public async Task GetAllIncludeChildren_Pagination_MustReturnValidData(int page, int perPage, string expectedResult)
        {
            var take = perPage;
            var skip = (page - 1) * perPage;

            var result = await _repository.GetAllIncludeAllChildren(skip, take, e => e.CreatedDate);
            var json = JsonConvert.SerializeObject(result);
            Assert.That(json, Is.EqualTo(expectedResult));
        }
        #endregion


        #region [- Get All -]



        [Test]
        public async Task GetAllAsync_MustBeMoreThenZero()
        {
            var result = await _repository.GetAllAsync();
            Assert.That(result.Count, Is.GreaterThan(0));
        }



        [Test]
        public async Task GetAllAsync_MustHaveUniqueAndGuidFormattedId()
        {
            var result = await _repository.GetAllAsync();
            foreach (var entity in result)
            {
                Assert.That(entity.ID, Does.Match(@"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$"));
            }
        }


        #endregion








        #region [- Test Storage -]

        static IEnumerable<TestCaseData> PaginationSource(int page, int perPage)
        {
            var result = SeedStorage.GetJsonOfDataInPage(page, perPage, typeof(TEntity));

            return new List<TestCaseData>
            {
                new TestCaseData(page,perPage,result)
            };
        }


        static IEnumerable<TestCaseData> OnlyEntitySource()
        {
            var lastId = SeedStorage.LastId(typeof(TEntity));
            var firstId = SeedStorage.FirstId(typeof(TEntity));

            return new List<TestCaseData>
            {
                new TestCaseData(lastId),
                new TestCaseData(firstId)
            };
        }


        static IEnumerable<TestCaseData> EntitySourceWithIncludePath()
        {
            var lastId = SeedStorage.LastId(typeof(TEntity));
            var firstId = SeedStorage.FirstId(typeof(TEntity));
            var path = SeedStorage.GetPath(typeof(TEntity));

            return new List<TestCaseData>
            {
                new TestCaseData(lastId, path),
                new TestCaseData(firstId, path)
            };
        }


        static IEnumerable<TestCaseData> EntitySourceWithInvalidIncludePath()
        {
            var lastId = SeedStorage.LastId(typeof(TEntity));
            var firstId = SeedStorage.FirstId(typeof(TEntity));
            var path = SeedStorage.GetPath(typeof(TEntity));

            return new List<TestCaseData>
            {
                new TestCaseData(lastId, "InvalidPathOne"),
                new TestCaseData(firstId, "InvalidPathTwo")
            };
        }
        #endregion


    }

}








