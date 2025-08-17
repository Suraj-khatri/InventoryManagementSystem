using Dapper;
using InventoryManagementSystem.Domain.RoleSetupVM;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace InventoryManagementSystem.Infrastructure.Filter
{
    public class UserAuthorize : AuthorizeAttribute
    {
        public string Role { get; set; }
        public int? MenuId { get; set; }
        public UserAuthorize()
        {
            MenuId = 0;
        }
        public UserAuthorize(int menuId)
        {
            this.MenuId = menuId;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["AuthId"] == null || !HttpContext.Current.Request.IsAuthenticated)
            {

                filterContext.Result = new RedirectResult(FormsAuthentication.LoginUrl + "?ReturnUrl="
                    + filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.RawUrl
                    ));
            }
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            using (var context = new SqlConnection(ConfigurationManager.ConnectionStrings["JUINOAPIEntities"].ConnectionString))
            {

                var param = new DynamicParameters();
                param.Add("@AuthId", Convert.ToInt32(HttpContext.Current.Session["AuthId"]));
                var assignedRole = HttpContext.Current.Session["AssignRole"]?.ToString();

                if (MenuId != null && MenuId != 0)
                {
                    var userId = Convert.ToInt32(HttpContext.Current.Session["AuthId"]);
                    var parameters = new { userId = userId, menuId = MenuId };
                    var result = context.Query(
                        "dbo.procHasAccess",
                        parameters,
                        commandType: System.Data.CommandType.StoredProcedure
                    ).ToList();

                    if (!result.Any())
                    {

                        //if (filterContext.HttpContext.Request.IsAjaxRequest())
                        //{
                        //    filterContext.Result = new JsonResult
                        //    {
                        //        Data = new { redirect = true, url = "/Error/index" },
                        //        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        //    };
                        //}

                        filterContext.Result = new RedirectResult("/Error/index");

                    }



                }



            }
        }
    }
}
