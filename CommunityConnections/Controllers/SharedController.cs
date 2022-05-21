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

                var path = Path.Combine(Server.MapPath("~/Content/template/images/"), file.FileName);
                file.SaveAs(path);
                result.Data = new { Success = true, Path = string.Format("/Content/template/images/{0}", file.FileName) };
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