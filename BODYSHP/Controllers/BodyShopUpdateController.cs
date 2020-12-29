using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BODYSHPBLL.ImplBLL;
using BODYSHPDAL.ImplDAL;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using OfficeOpenXml;
using Newtonsoft.Json;

namespace BODYSHP.Controllers
{
    public class BodyShopUpdateController : ApiController
    {
        [HttpPost]
        public string UpdateBodyShop(UpdateStatusBLL obj)
        {
            return UpdateStatusDAL.UpdateStatus(obj,"");
        }

        [HttpPost]
        public async Task<HttpResponseMessage> PostData()
        {
            string location = "";
            string fileName = "";
            
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var root = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["PhotoUrl"]);
            //LogService("Local Path:-"+ root.ToString());
            var PhotoUrlServer = System.Configuration.ConfigurationManager.AppSettings["PhotoUrlServer"];
            //LogService("PhotoUrlServer:-" + PhotoUrlServer.ToString());
            Directory.CreateDirectory(root); 
            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            
            try
            {
                for (int i = 0; i < result.FileData.Count; i++)
                {

                    fileName = result.FileData[i].Headers.ContentDisposition.FileName;
                    string fileserial = result.FileData[i].Headers.ContentDisposition.Name.Trim('"');
                    //LogService("fileName:-" + fileName);
                    if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                    {
                        fileName = fileName.Trim('"');
                    }
                    if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                    {
                        fileName = Path.GetFileName(fileName);

                        Match regex = Regex.Match(root, @"(.+) \((\d+)\)\.\w+");

                        if (regex.Success)
                        {
                            fileName = regex.Groups[1].Value;
                        }
                    }
                    
                    try
                    {
                        string fileType = Path.GetExtension(fileName);
                        string filename = DateTime.Now.Ticks.ToString();
                        //LogService("fileName" + fileName);
                        string ExlName = filename + fileType;
                        string str;
                        str = filename.Substring(filename.Length - 1, 1);
                        string ImageWithPath = string.Format("{0}{1}", filename + str, fileType);
                        PhotoUrlServer = PhotoUrlServer + ImageWithPath;
                        //LogService("PhotoUrlServer :-" + PhotoUrlServer);
                        location = Path.Combine(root, ImageWithPath);
                        //LogService("location :-" + location);
                        File.Move(result.FileData[i].LocalFileName, Path.Combine(root, ImageWithPath));
                    }
                    catch (Exception e)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "2");
                    }
                }
                var model = result.FormData["data"];
                UpdateStatusBLL obj = new UpdateStatusBLL();
                obj = JsonConvert.DeserializeObject<UpdateStatusBLL>(model);
                //function calling for insertion of excel Data into DataBase table
                if (result.FileData.Count==0)
                {
                    //LogService("No File Attached :-");
                    UpdateStatusDAL.UpdateStatus(obj, obj.PhotoUrl);
                }
                else
                { 
                    UpdateStatusDAL.UpdateStatus(obj, PhotoUrlServer);
                    //LogService("File Attached :-");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, e.Message);

            }
            return Request.CreateResponse(HttpStatusCode.Accepted, "0");

        }
        private void LogService(string content)
        {
            var root = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["PhotoUrl"]);
            //FileStream fs = new FileStream(root + "BodyShopUploadPhotoLog.txt", FileMode.OpenOrCreate, FileAccess.Write);

            FileStream fs = new FileStream("C:\\BodyShopUploadPhotoLog.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(string.Format(content, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
            sw.Flush();
            sw.Close();
        }

    }
}