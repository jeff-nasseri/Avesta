using Avesta.Attribute.Controller;
using Avesta.Attribute.Qraph;
using Avesta.Constant;
using Avesta.Controller;
using Avesta.Controller.API;
using Avesta.Controller.MVC;
using Avesta.Data.Model;
using Avesta.Exceptions;
using Avesta.Services.Graph;
using Avesta.Share.Extensions;
using Avesta.Share.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using SystemException = Avesta.Exceptions.SystemException;

namespace Avesta.Graph.Controller
{

    public interface IGraphAPIController<TId, TEntity> where TId : class
     where TEntity : BaseEntity<TId>
    {
        Task<IActionResult> Execaute(string where = "i=>i"
            , string select = "i=>i"
            , string orderBy = "i=>i.CreatedDate"
            , string? navigationPropertyPath = null
            , int? page = null
            , int perPage = Pagination.PerPage);



        Task<IActionResult> Execaute(string where = "i=>i"
            , string select = "i=>i"
            , string orderBy = "i=>i.CreatedDate"
            , bool includeAllPath = true
            , int? page = null
            , int perPage = Pagination.PerPage);
    }




    [AvestaAPIController]
    public class GraphAPIController<TId, TEntity> : AvestaBaseAPIController, IGraphAPIController<TId, TEntity>
        where TId : class
        where TEntity : BaseEntity<TId>
    {

        readonly IEntityGraphService<TId, TEntity> _entityGraphService;
        public GraphAPIController(IEntityGraphService<TId, TEntity> entityGraphService)
        {
            _entityGraphService = entityGraphService;
        }


        [HttpGraphNode]
        public async Task<IActionResult> Execaute(string where = "i=>i"
            , string select = "i=>i"
            , string orderBy = "i=>i.CreatedDate"
            , string? navigationPropertyPath = null
            , int? page = null
            , int perPage = Pagination.PerPage)
        {
            try
            {
                var result = await _entityGraphService.GraphQuery(where: where
                    , select: select
                    , orderBy: orderBy
                    , navigationPropertyPath: navigationPropertyPath
                    , page: page
                    , perPage: perPage);

                return base.Ok(result);
            }
            catch (SystemException exception)
            {
                var errMsg = ErrorManager.GetErrorMessageByCode(exception.Code);
                ErrorManager.LogExceptionToFile(exception);
                return base.BadRequest(errMsg);
            }
        }



        [HttpGraphNode]
        public async Task<IActionResult> Execaute(string where = "i=>i"
            , string select = "i=>i"
            , string orderBy = "i=>i.CreatedDate"
            , bool includeAllPath = true
            , int? page = null
            , int perPage = Pagination.PerPage)
        {
            try
            {
                var result = await _entityGraphService.GraphQuery(includeAllPath: includeAllPath
                    , where: where
                    , select: select
                    , orderBy: orderBy
                    , page: page
                    , perPage: perPage);

                return base.Ok(result);
            }
            catch (SystemException exception)
            {
                var errMsg = ErrorManager.GetErrorMessageByCode(exception.Code);
                ErrorManager.LogExceptionToFile(exception);
                return base.BadRequest(errMsg);
            }
        }


    }


}
