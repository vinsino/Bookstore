using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace Bookstore.Filters
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)//判斷是否已驗證
                return false;
            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {            

            base.OnAuthorization(filterContext); // call --> AuthorizeCore
                                                 //未授權網頁, 導到首頁
            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.HttpContext.Response.Redirect("~/Home/Unauthorized/" +
                filterContext.HttpContext.User.Identity.Name);
                filterContext.Result = new EmptyResult();
                return;
            }
            string loginUser = filterContext.HttpContext.User.Identity.Name;
            Match m = Regex.Match(loginUser, @"\\{0,1}(\d{4})@{0,1}");          //   SECLTD   \7596@
            if (m.Success)
                loginUser = m.Groups[1].ToString(); // 7596
            //-------------------------------------------------------
            if (filterContext.HttpContext.Session["empno"] == null)
            {
                //Get Userinfo
                filterContext.HttpContext.Session["empno"] = loginUser;
                filterContext.HttpContext.Session["empname"] = loginUser;
                
            }

        }


    }
}