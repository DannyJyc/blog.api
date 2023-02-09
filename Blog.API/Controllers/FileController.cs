using Blog.API.Entity;
using Blog.API.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        [HttpPost("ImgUpload")]
        public BaseResult UploadImage()
        {
            var files = this.Request.Form.Files;
            try
            {
                List<string> fileList = new List<string>(); 
                foreach (var item in files)
                {
                    if (item != null)
                    {
                        var strDateTime = DateTime.Now.ToString("yyMMddhhmmssfff"); //取得时间字符串
                        string originFullFileName = item.FileName;
                        string uploadfilename = string.Concat(strDateTime, "_", originFullFileName);
                        var folder = AppsettingHelper.Configuration.GetSection("FileUploadConfig").Value;
                        if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }
                        string file_path = string.Concat(folder, "\\", uploadfilename);
                        #region 图片文件的条件判断

                        //文件后缀
                        var fileExtension = Path.GetExtension(item.FileName);

                        //判断后缀是否是图片
                        //const string fileFilt = ".jpg|.jpeg|.png";
                        if (fileExtension == null)
                        {
                            break;
                            //return Error("上传的文件没有后缀");
                        }

                        //if (!string.IsNullOrEmpty(verFileType) && verFileType.IndexOf(fileExtension.Split(".").Last().ToLower(), StringComparison.Ordinal) <= -1)
                        //{
                        //    return new BaseResult { status = 404, success = false, msg = $"请上传{verFileType}格式的图片" };
                        //    //return Error("请上传jpg、png、gif格式的图片");
                        //}

                        //判断文件大小    
                        long length = item.Length;
                        if (length > 1024 * 1024 * 5) //2M
                        {
                            break;
                            //return Error("上传的文件不能大于2M");
                        }

                        #endregion

                        var strRan = Convert.ToString(new Random().Next(100, 999)); //生成三位随机数

                        //插入图片数据                 
                        using (FileStream fs = System.IO.File.Create(file_path))
                        {
                            item.CopyTo(fs);
                            fs.Flush();
                        }
                        fileList.Add(file_path);
                    }
                }
                return new BaseResult { code = 200, data = fileList };
            }
            catch (Exception ex)
            {
                return new BaseResult { code = 500, message = $"上传失败,原因：{ex.Message}" };
            }
        }
    }
}
