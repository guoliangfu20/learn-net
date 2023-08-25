using Newtonsoft.Json;
using SpaceCodeBLL;
using SpaceCodeCommons;
using SpaceCodeModel;
using SpaceCodeWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpaceCodeWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly static SpaceDataBll _spaceDataBll = new SpaceDataBll();


        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 异步获取列表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult GetData(SpaceDataDTO data)
        {
            PageViewModel<SpaceData> pageView = new PageViewModel<SpaceData>();

            if (data.PageIndex == 0) data.PageIndex = 1;
            if (data.PageSize == 0) data.PageSize = 10;
            if (string.IsNullOrEmpty(data.OrderBy)) data.OrderBy = "Id";

            int total = 0;
            pageView.rows = _spaceDataBll.Pagination(data, out total);
            pageView.total = total;
            return Content(JsonConvert.SerializeObject(pageView));
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            ResultDTO<string> result = new ResultDTO<string>();
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var fileExtension = Path.GetExtension(file.FileName);

            var newFile = fileName + fileExtension;

            LogsHelper.Logger($"导入时间:{DateTime.Now}, 文件名称:{newFile}", "导入文件");

            try
            {
                if (!System.IO.File.Exists(Server.MapPath("~/UploadFiles/")))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/UploadFiles"));//创建该文件
                }
                var serverPath = Server.MapPath("~/UploadFiles/" + newFile);
                if (System.IO.File.Exists(serverPath))
                {
                    System.IO.File.Delete(Server.MapPath("~/UploadFiles/" + newFile));
                }
                file.SaveAs(serverPath);
                result.Success = true;
                result.Result = newFile;
                result.Message = "上传成功";
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }

        /// <summary>
        /// 解析文件，并存入数据库
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadData(string fileName, bool change)
        {
            ResultDTO<string> result = new ResultDTO<string>();
            string fullPath = Server.MapPath("~/UploadFiles/" + fileName);
            if (string.IsNullOrEmpty(fileName) || !System.IO.File.Exists(fullPath))
            {
                return Json(new { result = false });
            }
            try
            {
                int total = 0;
                result.Success = _spaceDataBll.AddFromMdb(System.IO.Path.Combine(fullPath), out total, change);
                result.Message = result.Success ? "操作成功" : "插入失败";
                result.Result = total.ToString();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return Json(result);

        }
    }
}