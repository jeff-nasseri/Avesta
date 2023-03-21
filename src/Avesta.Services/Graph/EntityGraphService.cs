using AutoMapper;
using Avesta.Data.Model;
using Avesta.Repository.EntityRepository.Qraph;
using Avesta.Share.Model;
using Avesta.Storage.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Services.Graph
{

    public class EntityGraphService<TId, TEntity, TModel> : BaseEntityService, IEntityGraphService<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {

        readonly IGraphRepository<TEntity, TId> _graphRepository;
        readonly IMapper _mapper;

        public EntityGraphService(IGraphRepository<TEntity, TId> graphRepository, IMapper mapper)
        {
            _graphRepository = graphRepository;
            _mapper = mapper;
        }




        public async Task<IEnumerable<TModel>> GraphQuery(string navigationPropertyPath
           , string where
           , string select
           , string orderBy
           , int? page = null
           , int perPage = Pagination.PerPage
       , bool track = false)
        {
            var entities = await _graphRepository.GraphQuery(navigationPropertyPath, where, select, orderBy, page, perPage, track);
            var result = _mapper.Map<IEnumerable<TModel>>(entities);
            return result;
        }


        public async Task<IEnumerable<TModel>> GraphQuery(string includeAllPath
            , bool where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = Pagination.PerPage
        , bool track = false)
        {
            var entities = await _graphRepository.GraphQuery(includeAllPath, where, select, orderBy, page, perPage, track);
            var result = _mapper.Map<IEnumerable<TModel>>(entities);
            return result;
        }

        public async Task<IEnumerable<TModel>> GraphQuery(IQueryable<TEntity> entities
            , string where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = Pagination.PerPage
        , bool track = false)
        {
            var data = await _graphRepository.GraphQuery(entities, where, select, orderBy, page, perPage, track);
            var result = _mapper.Map<IEnumerable<TModel>>(data);
            return result;
        }







    }






}
