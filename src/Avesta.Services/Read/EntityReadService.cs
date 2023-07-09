using AutoMapper;
using Avesta.Data.Entity.Model;
using Avesta.Share.Model;
using Avesta.Constant;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Avesta.Share.Utilities;
using Avesta.Repository.EntityRepository.Read;

namespace Avesta.Services.Read
{
    public class ReadEntityService<TId, TEntity, TModel> : BaseEntityService, IReadEntityService<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {


        readonly IReadRepository<TEntity, TId> _readRepository;
        readonly IMapper _mapper;
        public ReadEntityService(IReadRepository<TEntity, TId> readRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }



        public async Task<TModel> First(string navigationPropertyPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
        {
            var entity = await _readRepository.First(navigationPropertyPath, track, exceptionRaiseIfNotExist);
            var result = _mapper.Map<TModel>(entity);
            return result;
        }

        public async Task<TModel> First(bool includeAllPath = false
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
        {
            var entity = await _readRepository.First(includeAllPath, track, exceptionRaiseIfNotExist);
            var result = _mapper.Map<TModel>(entity);
            return result;
        }

        public async Task<TModel> First(IQueryable<TEntity> entities
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
        {
            var entity = await _readRepository.First(entities, track, exceptionRaiseIfNotExist);
            var result = _mapper.Map<TModel>(entity);
            return result;
        }







        public async Task<TModel> First(Expression<Func<TEntity, bool>> search
            , string navigationPropertyPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
        {
            var entity = await _readRepository.First(search, navigationPropertyPath, track, exceptionRaiseIfNotExist);
            var result = _mapper.Map<TModel>(entity);
            return result;
        }
        public async Task<TModel> First(Expression<Func<TEntity, bool>> search
            , bool includeAllPath = false
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
        {
            var entity = await _readRepository.First(search, includeAllPath, track, exceptionRaiseIfNotExist);
            var result = _mapper.Map<TModel>(entity);
            return result;
        }

        public async Task<TModel> First(IQueryable<TEntity> entities
            , Expression<Func<TEntity, bool>> search
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
        {
            var entity = await _readRepository.First(entities, search, track, exceptionRaiseIfNotExist);
            var result = _mapper.Map<TModel>(entity);
            return result;
        }







        public async Task<TModel> Get(TId key
           , string navigationPropertyPath
           , bool track = true
           , bool exceptionRaiseIfNotExist = false)
        {
            var entity = await _readRepository.Get(key, navigationPropertyPath, track, exceptionRaiseIfNotExist);
            var result = _mapper.Map<TModel>(entity);
            return result;
        }

        public async Task<TModel> Get(TId key
             , bool includeAllPath
             , bool track = true
             , bool exceptionRaiseIfNotExist = false)
        {
            var entity = await _readRepository.Get(key, includeAllPath, track, exceptionRaiseIfNotExist);
            var result = _mapper.Map<TModel>(entity);
            return result;
        }

        public async Task<TModel> Get(IQueryable<TEntity> entities
             , TId key
             , bool track = true
             , bool exceptionRaiseIfNotExist = false)
        {
            var entity = await _readRepository.Get(entities, key, track, exceptionRaiseIfNotExist);
            var result = _mapper.Map<TModel>(entity);
            return result;
        }










        public async Task<TModel> Get(Expression<Func<TEntity, bool>> predicate
            , string navigationPropertyPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
        {
            var entity = await _readRepository.Get(predicate, navigationPropertyPath, track, exceptionRaiseIfNotExist);
            var result = _mapper.Map<TModel>(entity);
            return result;
        }

        public async Task<TModel> Get(Expression<Func<TEntity, bool>> predicate
            , bool includeAllPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
        {
            var entity = await _readRepository.Get(predicate, includeAllPath, track, exceptionRaiseIfNotExist);
            var result = _mapper.Map<TModel>(entity);
            return result;
        }


        public async Task<TModel> Get(IQueryable<TEntity> entities
            , Expression<Func<TEntity, bool>> predicate
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
        {
            var entity = await _readRepository.Get(entities, predicate, track, exceptionRaiseIfNotExist);
            var result = _mapper.Map<TModel>(entity);
            return result;
        }













        public async Task<IEnumerable<TModel>> GetAll<TKey>(
            string navigationPropertyPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false
            , object[] keywords = null)
        {
            var entities = await _readRepository.GetAll(navigationPropertyPath, page, perPage, orderBy, orderbyDirection, track);
            entities = await entities.Search<TEntity>(keywords);
            var result = _mapper.Map<IEnumerable<TModel>>(entities);
            return result;
        }

        public async Task<IEnumerable<TModel>> GetAll<TKey>(
             bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false
            , object[] keywords = null)
        {
            var entities = await _readRepository.GetAll(includeAllPath, page, perPage, orderBy, orderbyDirection, track);
            entities = await entities.Search<TEntity>(keywords);
            var result = _mapper.Map<IEnumerable<TModel>>(entities);
            return result;
        }


        public async Task<IEnumerable<TModel>> GetAll(
             bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , bool track = false
            , object[] keywords = null)
                => await GetAll<object>(includeAllPath: includeAllPath, page: page, perPage: perPage, track: track, keywords: keywords);



        public async Task<IEnumerable<TModel>> GetAll<TKey>(IQueryable<TEntity> entities
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false
            , object[] keywords = null)
        {
            var data = await _readRepository.GetAll(entities, page, perPage, orderBy, orderbyDirection, track);
            data = await entities.Search<TEntity>(keywords);
            var result = _mapper.Map<IEnumerable<TModel>>(data);
            return result;
        }







        public async Task<IEnumerable<TModel>> GetByIds<TKey>(IEnumerable<TId> ids
        , string navigationPropertyPath
        , int? page = null
        , int perPage = Pagination.PerPage
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false)
        {
            var entities = await _readRepository.GetByIds(ids, navigationPropertyPath, page, perPage, orderBy, orderbyDirection, track);
            var result = _mapper.Map<IEnumerable<TModel>>(entities);
            return result;
        }

        public async Task<IEnumerable<TModel>> GetByIds<TKey>(IEnumerable<TId> ids
            , bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
        {
            var entities = await _readRepository.GetByIds(ids, includeAllPath, page, perPage, orderBy, orderbyDirection, track);
            var result = _mapper.Map<IEnumerable<TModel>>(entities);
            return result;
        }


        public async Task<IEnumerable<TModel>> GetByIds<TKey>(IQueryable<TEntity> entities
            , IEnumerable<TId> ids
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
        {
            var data = await _readRepository.GetByIds(entities, ids, page, perPage, orderBy, orderbyDirection, track);
            var result = _mapper.Map<IEnumerable<TModel>>(data);
            return result;
        }





        public async Task<IEnumerable<TModel>> Where<TKey>(Expression<Func<TEntity, bool>> search
           , string navigationPropertyPath
           , int? page = null
           , int perPage = Pagination.PerPage
           , Func<TEntity, TKey> orderBy = null
           , OrderByDirection orderbyDirection = OrderByDirection.Ascending
           , bool track = false)
        {
            var entities = await _readRepository.Where(search, navigationPropertyPath, page, perPage, orderBy, orderbyDirection, track);
            var result = _mapper.Map<IEnumerable<TModel>>(entities);
            return result;
        }


        public async Task<IEnumerable<TModel>> Where<TKey>(Expression<Func<TEntity, bool>> search
            , bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
        {
            var entities = await _readRepository.Where(search, includeAllPath, page, perPage, orderBy, orderbyDirection, track);
            var result = _mapper.Map<IEnumerable<TModel>>(entities);
            return result;
        }

        public async Task<IEnumerable<TModel>> Where<TKey>(IQueryable<TEntity> entities
            , Expression<Func<TEntity, bool>> search
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
        {
            var data = await _readRepository.Where(entities, search, page, perPage, orderBy, orderbyDirection, track);
            var result = _mapper.Map<IEnumerable<TModel>>(data);
            return result;
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> where, string navigationPropertyPath)
            => await _readRepository.Count(where, navigationPropertyPath);

        public async Task<int> Count(Expression<Func<TEntity, bool>> where)
            => await _readRepository.Count(where);


        public async Task<int> Count(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> where)
            => await _readRepository.Count(entities, where);
        public async Task<int> Count()
            => await _readRepository.Count();


        public Task<TModel> Search(params Expression[] expressions)
        {
            throw new NotImplementedException();
        }

        public Task<TModel> Search(params object[] keywords)
        {
            throw new NotImplementedException();
        }



    }






}
