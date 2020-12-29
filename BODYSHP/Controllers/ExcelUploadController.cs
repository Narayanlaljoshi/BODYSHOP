using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using OfficeOpenXml;
using CCAPPDAL.ImplDAL;
using BODYSHPBLL.ImplBLL;

namespace CCAPPUI.Controllers
{
    public class ExcelUploadController : ApiController
    {
        
        //Function for uploading Excel file for Jobcards
        [HttpPost]
        public async Task<HttpResponseMessage> PostData()
        {
            string location = "";
            string fileName = "";
            int k = 0;
            //long UserId = 0;
            // Check if the request contains multipart/form-data.
            string strUniqueId = System.Guid.NewGuid().ToString();
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var root = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ExcelPath"]);

            Directory.CreateDirectory(root);
            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);
           
            

            DataTable tbl = new DataTable();
            

            try
            {
                for (int i = 0; i < result.FileData.Count; i++)
                {

                    fileName = result.FileData[i].Headers.ContentDisposition.FileName;
                    string fileserial = result.FileData[i].Headers.ContentDisposition.Name.Trim('"');
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

                    //Storing file to temporary location in project
                    try
                    {

                        string fileType = Path.GetExtension(fileName);

                        string filename = DateTime.Now.Ticks.ToString(); //Path.GetFileNameWithoutExtension(fileName);


                        string ExlName = filename + fileType;
                        string str;
                        str = filename.Substring(filename.Length - 1, 1);
                        string ImageWithPath = string.Format("{0}{1}", filename + str, fileType);
                        location = Path.Combine(root, ImageWithPath);
                        File.Move(result.FileData[i].LocalFileName, Path.Combine(root, ImageWithPath));
                    }

                    catch (Exception e)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "2");

                    }

                    FileInfo f = new FileInfo(location);
                    Stream s = f.OpenRead();

                    bool hasHeader = true;
                    //open xlsx file            
                    var excel = new ExcelPackage(s);
                    var workbook = excel.Workbook;
                   

                    if (i == 0)
                    {

                        var sheet = excel.Workbook.Worksheets[1];

                        foreach (var firstRowCell in sheet.Cells[1, 1, 1, sheet.Dimension.End.Column])
                        {
                            tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                        }

                        // headers of excel file
                        // headers of excel file
                        if (tbl.Columns.Count == 15)
                        {
                            if (tbl.Columns[1].ColumnName != "Job Card No." || tbl.Columns[2].ColumnName != "Date & Time" || tbl.Columns[3].ColumnName != "Customer Name" || tbl.Columns[4].ColumnName != "Phone & Mobile No." || tbl.Columns[5].ColumnName != "Customer Catg." || tbl.Columns[6].ColumnName != "PSF Status" || tbl.Columns[7].ColumnName != "Registration")
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, "3");
                            }
                            if (tbl.Columns[8].ColumnName != "Model" || tbl.Columns[9].ColumnName != "S.A" || tbl.Columns[10].ColumnName != "Technician" || tbl.Columns[11].ColumnName != "Service" || tbl.Columns[12].ColumnName != "Promised Dt." || tbl.Columns[13].ColumnName != "Rev. Promised Dt." || tbl.Columns[14].ColumnName != "Ready Date & Time")
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, "3");
                            }

                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, "2");
                        }
                        var startRow = hasHeader ? 2 : 1;
                        for (int rowNum = startRow; rowNum <= sheet.Dimension.End.Row; rowNum++)
                        {
                            var wsRow = sheet.Cells[rowNum, 1, rowNum, sheet.Dimension.End.Column];
                            DataRow row = tbl.Rows.Add();
                            //for (k = 0; k < 14;k++)
                            foreach (var cell in wsRow)
                            {

                                if (cell.Start.Column < 14)
                                    row[cell.Start.Column - 1] = cell.Text;
                                else
                                    continue;
                            }
                        }

                    }
                }
                var model = result.FormData["data"];
                
                //var Data = JsonConvert.DeserializeObject<long>(model);
                SessionDataBLL obj= new SessionDataBLL();
                obj= JsonConvert.DeserializeObject<SessionDataBLL>(model);
                
                // function calling for insertion of excel Data into DataBase table
                ExcelUploadDAL.InsertData(tbl, obj);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, e.Message);

            }
            return Request.CreateResponse(HttpStatusCode.Accepted, "0");

        }


        //For Contractor Excel Uplaod
        [HttpPost]
        public async Task<HttpResponseMessage> ContractorData()
        {
            string location = "";
            string fileName = "";
            int k = 0;
            //long UserId = 0;
            // Check if the request contains multipart/form-data.
            string strUniqueId = System.Guid.NewGuid().ToString();
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var root = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ExcelPath"]);

            Directory.CreateDirectory(root);
            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);



            DataTable tbl = new DataTable();


            try
            {
                for (int i = 0; i < result.FileData.Count; i++)
                {

                    fileName = result.FileData[i].Headers.ContentDisposition.FileName;
                    string fileserial = result.FileData[i].Headers.ContentDisposition.Name.Trim('"');
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

                    //Storing file to temporary location in project
                    try
                    {

                        string fileType = Path.GetExtension(fileName);

                        string filename = DateTime.Now.Ticks.ToString(); //Path.GetFileNameWithoutExtension(fileName);


                        string ExlName = filename + fileType;
                        string str;
                        str = filename.Substring(filename.Length - 1, 1);
                        string ImageWithPath = string.Format("{0}{1}", filename + str, fileType);
                        location = Path.Combine(root, ImageWithPath);
                        File.Move(result.FileData[i].LocalFileName, Path.Combine(root, ImageWithPath));
                    }

                    catch (Exception e)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "2");

                    }

                    FileInfo f = new FileInfo(location);
                    Stream s = f.OpenRead();

                    bool hasHeader = true;
                    //open xlsx file            
                    var excel = new ExcelPackage(s);
                    var workbook = excel.Workbook;


                    if (i == 0)
                    {

                        var sheet = excel.Workbook.Worksheets[1];

                        foreach (var firstRowCell in sheet.Cells[1, 1, 1, sheet.Dimension.End.Column])
                        {
                            tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                        }

                        // headers of excel file
                        // headers of excel file
                        if (tbl.Columns.Count == 4)
                        {
                            if (tbl.Columns[0].ColumnName != "ContractorCode" || tbl.Columns[1].ColumnName != "ContractorName" || tbl.Columns[2].ColumnName != "ContractorAddress" || tbl.Columns[3].ColumnName != "PhoneNo")
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, "3");
                            }
                           

                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, "2");
                        }
                        var startRow = hasHeader ? 2 : 1;
                        for (int rowNum = startRow; rowNum <= sheet.Dimension.End.Row; rowNum++)
                        {
                            var wsRow = sheet.Cells[rowNum, 1, rowNum, sheet.Dimension.End.Column];
                            DataRow row = tbl.Rows.Add();
                            //for (k = 0; k < 14;k++)
                            foreach (var cell in wsRow)
                            {  
                                    row[cell.Start.Column - 1] = cell.Text;
                            }
                        }

                    }
                }
                var model = result.FormData["data"];

                //var Data = JsonConvert.DeserializeObject<long>(model);
                SessionDataBLL obj = new SessionDataBLL();
                obj = JsonConvert.DeserializeObject<SessionDataBLL>(model);

                // function calling for insertion of excel Data into DataBase table
                ExcelUploadDAL.InsertIntoTblContractor(tbl, obj);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, e.Message);

            }
            return Request.CreateResponse(HttpStatusCode.Accepted, "0");

        }

        
    }



}