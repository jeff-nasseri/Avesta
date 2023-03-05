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

namespace Avesta.Repository.Test
{
    [TestFixture(typeof(Student), typeof(ApplicationDbContext))]
    [TestFixture(typeof(Teacher), typeof(ApplicationDbContext))]
    [TestFixture(typeof(School), typeof(ApplicationDbContext))]
    public class CatchEntity<TEntity, TContext> : ServiceResolver<TEntity, TContext>
        where TEntity : BaseEntity
        where TContext : AvestaDbContext
    {

        readonly IRepository<TEntity> _repository;
        readonly TContext _context;
        public CatchEntity()
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
        [TestCaseSource(nameof(Source))]
        public async Task GetByIdAsync_MustReturnValidEntity(string id)
        {
            var result = await _repository.GetById(id);
            Assert.That(result, Is.Not.Null);
        }





        [TestCase("Not_Valid_ID_1")]
        [TestCase("Not_Valid_ID_2")]
        [ExpectedException(typeof(CanNotFoundEntityException))]
        public async Task GetByIdAsyncWithUnvalidId_MustThrowCanNotFoundEntityException(string id)
        {
            await _repository.GetById(id, exceptionRaseIfNotExist: true);
        }



        [TestCase("Not_Valid_ID_1")]
        [TestCase("Not_Valid_ID_2")]
        public async Task GetByIdAsyncWithUnvalidId_WillReturnNull_IfExceptionRaseIsFalse(string id)
        {
            var result = await _repository.GetById(id, exceptionRaseIfNotExist: false);
            Assert.That(result, Is.Null);
        }





        #endregion



        [TestCaseSource(nameof(Source))]
        public async Task GetEntity_MustReturnValidEntity(string id)
        {
            var result = await _repository.GetEntity(e => e.ID == id);
            Assert.That(result, Is.Not.Null);
        }





        [TestCase("Not_Valid_ID_1")]
        [TestCase("Not_Valid_ID_2")]
        [ExpectedException(typeof(CanNotFoundEntityException))]
        public async Task GetEntityWithUnvalidId_MustThrowCanNotFoundEntityException(string id)
        {
            await _repository.GetEntity(e => e.ID == id, exceptionRaseIfNotExist: true);
        }









        static IEnumerable<string> Source()
        {
            var result = new List<string>
            {
                SeedStorage.First(typeof(TEntity)).ID,
                SeedStorage.Last(typeof(TEntity)).ID
            };
            return result;
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


}
