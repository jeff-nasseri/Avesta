using Avesta.Data.Context;
using Avesta.Data.Model;
using Avesta.Repository.EntityRepository;
using Avesta.Repository.Test.Src;
using Avesta.Repository.Test.Src.Data.Context;
using Avesta.Repository.Test.Src.Data.Model;
using Avesta.Repository.Test.Src.Storage;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Builders;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework.Internal.Commands;
using TestResult = NUnit.Framework.Internal.TestResult;
using Avesta.Exceptions.Entity;
using NUnit.Framework.Constraints;
using Microsoft.EntityFrameworkCore;

namespace Avesta.Repository.Test
{
    [TestFixture(typeof(School), typeof(ApplicationDbContext))]
    [TestFixture(typeof(Student), typeof(ApplicationDbContext))]
    [TestFixture(typeof(Teacher), typeof(ApplicationDbContext))]
    [TestFixture(typeof(Teacher_School), typeof(ApplicationDbContext))]
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



        #region [- GetByIdAsync -]

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





        #region [- GetEntity -]

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




    }



}







public class ServiceResolver<TEntity, TContext> : RepositoryResolver<TEntity, TContext>
    where TEntity : BaseEntity
    where TContext : AvestaDbContext
{

}

public class RepositoryResolver<TEntity, TContext> : Program
    where TEntity : BaseEntity
    where TContext : AvestaDbContext
{

    public RepositoryResolver() => Start();



    public TContext Context { get => Builder.GetRequiredService<TContext>(); }

    public IRepository<TEntity> ResolveRepository()
    {
        var repository = Builder.GetRequiredService<IRepository<TEntity>>();
        return repository;
    }

}




public class TestCaseStorage<T> where T : class
{
    public static IEnumerable<TestCaseData> Source<T>() where T : class
    {
        throw new NotImplementedException();
    }
}




[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class ExpectedExceptionAttribute : NUnitAttribute, IWrapTestMethod
{
    private readonly Type _expectedExceptionType;

    public ExpectedExceptionAttribute(Type type)
    {
        _expectedExceptionType = type;
    }

    public TestCommand Wrap(TestCommand command)
    {
        return new ExpectedExceptionCommand(command, _expectedExceptionType);
    }

    private class ExpectedExceptionCommand : DelegatingTestCommand
    {
        private readonly Type _expectedType;

        public ExpectedExceptionCommand(TestCommand innerCommand, Type expectedType)
            : base(innerCommand)
        {
            _expectedType = expectedType;
        }

        public override TestResult Execute(TestExecutionContext context)
        {
            Type caughtType = null;

            try
            {
                innerCommand.Execute(context);
            }
            catch (Exception ex)
            {
                if (ex is NUnitException)
                    ex = ex.InnerException;
                caughtType = ex.GetType();
            }

            if (caughtType == _expectedType)
                context.CurrentResult.SetResult(ResultState.Success);
            else if (caughtType != null)
                context.CurrentResult.SetResult(ResultState.Failure,
                    string.Format("Expected {0} but got {1}", _expectedType.Name, caughtType.Name));
            else
                context.CurrentResult.SetResult(ResultState.Failure,
                    string.Format("Expected {0} but no exception was thrown", _expectedType.Name));

            return context.CurrentResult;
        }
    }
}


