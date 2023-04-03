using Avesta.Attribute.Controller;
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



    public interface IGraphHierarchySrevice
    {
        Task<DataHierarchy> GetHierarchyOfCurrentExecuteableApplication();
    }

    public class GraphHierarchySrevice : IGraphHierarchySrevice
    {

        public async Task<DataHierarchy> GetHierarchyOfCurrentExecuteableApplication()
        {
            await Task.CompletedTask;

            var entities = new List<EntityInformation>();

            var types = Assembly.GetExecutingAssembly().GetAllDrivenTypes<BaseEntity>();

            foreach (var type in types)
            {

                if (entities.Any(e => e.FullName == type.FullName))
                    continue;


                var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static)?.Select(p => new PropertyInformation
                {
                    Name = p.Name,
                    Type = p.GetType(),
                    FullName = p.Name
                }).ToList();
                var entityInfo = new EntityInformation(properties);

                var entity = new EntityInformation(properties)
                {
                    Name = type.Name,
                    FullName = type.FullName,
                    Type = type
                };

                entities.Add(entityInfo);
            }


            var result = new DataHierarchy(entities);
            return result;

        }
    }




    [AvestaMVCController]
    public class AvestaBaseMVCController : AvestaBaseController
    {
        readonly IGraphHierarchySrevice _graphHierarchySrevice;
        public AvestaBaseMVCController(IGraphHierarchySrevice graphHierarchySrevice)
        {
            _graphHierarchySrevice = graphHierarchySrevice;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _graphHierarchySrevice.GetHierarchyOfCurrentExecuteableApplication();
            return View(result);
        }
    }








    public class AvestaGraphMiddleware
    {
        readonly RequestDelegate _next;
        public AvestaGraphMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            await _next(httpContext);


        }
    }





    public class AvestaGraphModel : BaseModel
    {
        public AvestaGraphModel() : base(DateTime.Now)
        {
        }
    }


    public class BasicInformation : AvestaGraphModel
    {
        public string? Name { get; set; }
        public string? FullName { get; set; }
        public Type? Type { get; set; }
    }

    public class DataHierarchy : BasicInformation
    {

        public DataHierarchy(params EntityInformation[] data) : this(data?.ToList())
        {
        }

        public DataHierarchy(IEnumerable<EntityInformation>? entities) : this()
        {
            Entities = entities;
        }

        public DataHierarchy() : base()
        {
        }

        public IEnumerable<EntityInformation>? Entities { get; set; }

    }

    public class EntityInformation : BasicInformation
    {

        public EntityInformation(params PropertyInformation[] data) : this(data?.ToList())
        {
        }

        public EntityInformation(IEnumerable<PropertyInformation>? properties) : this()
        {
            Properties = properties;
        }

        public EntityInformation() : base()
        {
        }


        public IEnumerable<PropertyInformation>? Properties { get; set; }
    }

    public class PropertyInformation : BasicInformation
    {
    }







    public class HttpGraphNodeAttribute : HttpGetAttribute
    {
        public HttpGraphNodeAttribute() : base()
        {
        }

        public HttpGraphNodeAttribute(string template) : base(template)
        {
        }
    }






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






}
