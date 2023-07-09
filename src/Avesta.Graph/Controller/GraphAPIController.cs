using Avesta.Attribute.Controller;
using Avesta.Attribute.Graph;
using Avesta.Attribute.Qraph;
using Avesta.Constant;
using Avesta.Controller;
using Avesta.Controller.API;
using Avesta.Controller.MVC;
using Avesta.Data.Entity.Model;
using Avesta.Exceptions;
using Avesta.Services.Graph;
using Avesta.Share.Extensions;
using Avesta.Share.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SystemException = Avesta.Exceptions.SystemException;

namespace Avesta.Graph.Controller
{

    public interface IGraphAPIController<TId, TEntity> where TId : class
     where TEntity : BaseEntity<TId>
    {
        Task<IActionResult> Execute(string? where = "i=>i"
                    , string? select = "i=>i"
                    , string? orderBy = "i=>i.CreatedDate"
                    , string? navigationPropertyPath = null
                    , bool includeAllPath = true
                    , int? page = null
                    , int perPage = Pagination.PerPage);
    }




    [AvestaAPIController]
    [Route("/Avesta/Graph/api")]
    public class GraphAPIController<TId, TEntity> : AvestaBaseAPIController, IGraphAPIController<TId, TEntity>
        where TId : class
        where TEntity : BaseEntity<TId>
    {

        readonly IEntityGraphService<TId, TEntity> _entityGraphService;
        readonly IServiceProvider _provider;
        public GraphAPIController(IEntityGraphService<TId, TEntity> entityGraphService, IServiceProvider provider)
        {
            _entityGraphService = entityGraphService;
            _provider = provider;
        }


        [HttpGraphNode("exe")]
        public async Task<IActionResult> Execute(string? where = "i=>i"
            , string? select = "i=>i"
            , string? orderBy = "i=>i.CreatedDate"
            , string? navigationPropertyPath = null
            , bool includeAllPath = true
            , int? page = null
            , int perPage = Pagination.PerPage)
        {
            try

            {

                if (!string.IsNullOrEmpty(navigationPropertyPath))
                {
                    var result = await _entityGraphService.GraphQuery(where: where
                   , select: select
                   , orderBy: orderBy
                   , navigationPropertyPath: navigationPropertyPath
                   , page: page
                   , perPage: perPage);

                    return base.Ok(result);
                }
                else
                {
                    var result = await _entityGraphService.GraphQuery(where: where
                   , select: select
                   , orderBy: orderBy
                   , includeAllPath: includeAllPath
                   , page: page
                   , perPage: perPage);

                    return base.Ok(result);
                }


            }
            catch (SystemException exception)
            {
                var errMsg = ErrorManager.GetErrorMessageByCode(exception.Code);
                ErrorManager.LogExceptionToFile(exception);
                return base.BadRequest(errMsg);
            }
        }




        [HttpGraphNode("global/exe")]
        public async Task<IActionResult> Global(string typeFullName, string? where = "i=>i"
           , string? select = "i=>i"
           , string? orderBy = "i=>i.CreatedDate"
           , string? navigationPropertyPath = null
           , bool includeAllPath = true
           , int? page = null
           , int perPage = Pagination.PerPage)
        {
            try
            {
                var type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => (t.FullName ?? "").ToLower().Contains(typeFullName.ToLower()));

                if (type == null)
                    throw new SystemException("type is null!", ExceptionConstant.AProblemOccureAtSystem);

                var serviceType = typeof(IEntityGraphService<,>).MakeGenericType(typeof(string), type);
                var service = _provider.GetRequiredService(serviceType) as dynamic;


                if (!string.IsNullOrEmpty(navigationPropertyPath))
                {
                    var result = await service.GraphQuery(where: where
                   , select: select
                   , orderBy: orderBy
                   , navigationPropertyPath: navigationPropertyPath
                   , page: page
                   , perPage: perPage);

                    return base.Ok(result as object);
                }
                else
                {
                    var result = await service.GraphQuery(where: where
                   , select: select
                   , orderBy: orderBy
                   , includeAllPath: includeAllPath
                   , page: page
                   , perPage: perPage);

                    return base.Ok(result as object);
                }


            }
            catch (SystemException exception)
            {
                var errMsg = ErrorManager.GetErrorMessageByCode(exception.Code);
                ErrorManager.LogExceptionToFile(exception);
                return base.BadRequest(errMsg);
            }
            catch (Exception exception)
            {
                ErrorManager.LogExceptionToFile(exception.Message, exception.StackTrace);
                return base.BadRequest(exception.Message);
            }
        }

    }






}
