using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Storage.Constant.EndPoints
{
    public class CrudController : BaseController
    {
        public const string Create = "create";
        public const string UpdateOrCreate = "update_or_create";
        public const string Edit = "edit";
        public const string Detail = "Detail";
        public const string Delete = "delete";
        public const string SoftDelete = "soft_delete";
        public const string GetAll = "get_all";
        public const string GetAsViewModel = "get_as_view_model";
        public const string Get = "get";
        public const string Query = "query";
        public const string GetAllWithChildren = "get_all_include_mapped_children";
        public const string GetAllWithSpecificChildren = "get_all_include_specific_mapped_children";
        public const string GetWithChildren = "get_include_mapped_children";
        public const string GetWithSpecificChildren = "get_include_specific_mapped_children";
        public const string GetAllByParentId = "get_all_by_property_name";
    }
}
