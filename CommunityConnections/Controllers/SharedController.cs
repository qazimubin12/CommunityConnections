using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommunityConnections.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        public JsonResult UploadImage()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var file = Request.Files[0];

                var fileName = Path.GetExtension(file.FileName);

                var path = Path.Combine(Server.MapPath("~/Content/themedata/dist/img/"), fileName);
                file.SaveAs(path);
                result.Data = new { Success = true, Path = string.Format("/Content/themedata/dist/img/{0}", fileName) };
                //var newImage = new Image() { Name = fileName };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
                throw;
            }
            return result;
        }

    }
}