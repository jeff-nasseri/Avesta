using AutoMapper;
using Avesta.Data.Entity.Model;
using Avesta.Share.Model;
using Avesta.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avesta.Services;
using Avesta.Repository.EntityRepository.Graph;

namespace Avesta.Services.Graph
{

    public class EntityGraphService<TId, TEntity> : BaseEntityService, IEntityGraphService<TId, TEntity>
        where TId : class
        where TEntity : BaseEntity<TId>
    {

        readonly IGraphRepository<TEntity, TId> _graphRepository;
        public EntityGraphService(IGraphRepository<TEntity, TId> graphRepository)
        {
            _graphRepository = graphRepository;
        }




        public async Task<IEnumerable<dynamic>> GraphQuery(string navigationPropertyPath
           , string where
           , string select
           , string orderBy
           , int? page = null
           , int perPage = Pagination.PerPage
       , bool track = false)
        {
            var result = await _graphRepository.GraphQuery(navigationPropertyPath, where, select, orderBy, page, perPage, track);
            return result;
        }


        public async Task<IEnumerable<dynamic>> GraphQuery(bool includeAllPath
            , string where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = Pagination.PerPage
        , bool track = false)
        {
            var result = await _graphRepository.GraphQuery(includeAllPath, where, select, orderBy, page, perPage, track);
            return result;
        }

        public async Task<IEnumerable<dynamic>> GraphQuery(IQueryable<TEntity> entities
            , string where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = Pagination.PerPage
        , bool track = false)
        {
            var result = await _graphRepository.GraphQuery(entities, where, select, orderBy, page, perPage, track);
            return result;
        }







    }






}
