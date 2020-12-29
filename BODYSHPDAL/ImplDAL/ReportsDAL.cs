using BODYSHPBLL.ImplBLL;
using BODYSHPDAL.DbContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BODYSHPDAL.ImplDAL
{
    public class ReportsDAL
    {
        public static List<SpGetDateWiseDetails_Result> GetData(DateWiseReportBLL obj)
        {
            List<SpGetDateWiseDetails_Result> StatusResult = null;
            using (var dbContext = new BSSDBEntities())
            {
                var ReqData = dbContext.SpGetDateWiseDetails(obj.StartDate, obj.EndDate, obj.DealerID, obj.AccountId);
                StatusResult = ReqData.ToList();
            }
            return StatusResult;

        }

        public static List<SpStatusWiseDtl_Result> GetData(StatusWiseBLL obj)
        {
            List<SpStatusWiseDtl_Result> StatusResult = null;
            using (var dbContext = new BSSDBEntities())
            {
                var ReqData = dbContext.SpStatusWiseDtl(obj.StatusID, obj.DealerID, obj.AccountId);
                StatusResult = ReqData.ToList();
            }
            return StatusResult;

        }

        public static List<SpContractorWiseDtl_Result> GetContractorWiseDtl(ContractorModel obj)
        {
            List<SpContractorWiseDtl_Result> StatusResult = null;
            using (var dbContext = new BSSDBEntities())
            {
                var ReqData = dbContext.SpContractorWiseDtl(obj.Contractor_Id, obj.DealerID, obj.AccountID);
                StatusResult = ReqData.ToList();
            }
            return StatusResult;

        }

        public static List<ContractorWiseReportModel> GetContractorWiseReport(ContractorModel obj)
        {

            using (var dbContext = new BSSDBEntities())
            {
                List<ContractorWiseReportModel> ReportResult = new List<ContractorWiseReportModel>();
                List<ContractorWiseReportModel> FinalResult = new List<ContractorWiseReportModel>();
                var ReqData = dbContext.SP_ContractorWiseReport(obj.DealerID, obj.AccountID).ToList();
                SessionDataBLL DataObj = new SessionDataBLL
                {
                    DealerID = obj.DealerID,
                    AccountId = obj.AccountID
                };

                var StatusList = StatusMasterDAL.GetData(DataObj);
                foreach (var item in ReqData)
                {
                    if (ReportResult.Where(x => x.Contractor_Id == item.Contractor_Id).FirstOrDefault() == null)
                    {
                        ReportResult.Add(new ContractorWiseReportModel
                        {
                            Contractor_Id = item.Contractor_Id,
                            ContractorName = item.ContractorName,

                            StatusList = ReqData.Where(x => x.Contractor_Id == item.Contractor_Id).Select(x => new StatusList
                            {
                                StatusCode = x.StatusCode,
                                StatusID = x.StatusID,
                                StatusName = x.StatusName,
                                JobCards = x.JobCards
                            }).ToList()



                        });
                    }

                }

                foreach (var result in ReportResult)
                {
                    int Total = 0;
                    List<StatusList> SL = new List<StatusList>();
                    foreach (var pt in StatusList)
                    {
                        
                        var Check = result.StatusList.Where(x => x.StatusID == pt.StatusID).FirstOrDefault();

                        int? JobCards = 0;
                        string StatusName = pt.StatusName;
                        string StatusCode = pt.StatusCode;
                        long? StatusID = 0;
                        if (Check != null)
                        {

                            SL.Add(new BODYSHPBLL.ImplBLL.StatusList
                            {
                                JobCards = Check.JobCards,
                                StatusName = Check.StatusName,
                                StatusCode = Check.StatusCode,
                                StatusID = Check.StatusID
                            });

                        }
                        else
                        {
                            SL.Add(new BODYSHPBLL.ImplBLL.StatusList
                            {
                                JobCards = JobCards,
                                StatusName = StatusName,
                                StatusCode = StatusCode,
                                StatusID = StatusID
                            });
                        }

                        
                    }
                    FinalResult.Add(new ContractorWiseReportModel
                    {
                        ContractorName = result.ContractorName,
                        Contractor_Id = result.Contractor_Id,
                        Total = Total,
                        StatusList = SL
                    });
                }

                List<StatusList> VerticalCount = new List<StatusList>();

                int RowTotal = 0;
                for (int i = 0; i < FinalResult[0].StatusList.Count; i++)
                {
                    int ColumnTotal = 0;

                    for (int j = 0; j < FinalResult.Count; j++)
                    {
                        ColumnTotal = ColumnTotal + FinalResult[j].StatusList[i].JobCards.Value;
                    }
                    VerticalCount.Add(new StatusList
                    {
                        JobCards = ColumnTotal
                    });
                }
                FinalResult.Add(new ContractorWiseReportModel
                {
                    ContractorName = "Total",
                    Contractor_Id = 0,
                    Total=0,
                    StatusList = VerticalCount
                });
                foreach (var result in FinalResult)
                {
                    for (int i = 0; i < result.StatusList.Count; i++)
                    {
                        result.Total = result.Total + result.StatusList[i].JobCards;
                    }
                }

                




                return FinalResult;
            }


        }

        public static List<MonthWiseReport> GetMonthWiseReport(MonthWiseInput obj)
        {
            List<MonthWiseReport> Result = null;
            using (var dbContext = new BSSDBEntities())
            {
                var ReqData = dbContext.SP_ConsolidatedReport(obj.DealerID, obj.AccountID, obj.Days).ToList();
                Result = ReqData.Select(x => new MonthWiseReport {
                    ContractorName=x.ContractorName,
                    DateAndTime=x.DateAndTime.Value.ToString("dd/MM/yyyy"),
                    JobCardNo=x.JobCardNo,
                    Model=x.Model,
                    CustomerName=x.CustomerName,
                    ModifiedDate=x.ModifiedDate,
                    No_of_Panel=x.No_of_Panel,
                    Panel_Replaced=x.Panel_Replaced,
                    PhoneNo=x.PhoneNo,
                    PromisedDate=x.PromisedDate,
                    RegistrationNo=x.RegistrationNo,
                    Service=x.Service,
                    StatusName=x.StatusName,
                    CompanyName=x.CompanyName,
                    NoOfGlass=x.NoOfGlass,
                    PaymentMode=x.PaymentMode,
                    PhotoUrl=x.PhotoUrl
                }).ToList();
            }
            return Result;
        }
        
        public static string SendEmail(ReportInput Obj)
        {
            var root = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ExcelPath"]);

            var StatusList = StatusMasterDAL.GetData(Obj.SessionData);
            Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook wb = xla.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet ws = wb.ActiveSheet;
            
            ws.Cells[1, 1] = "Group Name";
            ws.Cells[1, 1].Font.Bold = true;
            for (int i = 0; i < StatusList.Count; i++)
            {
                ws.Cells[1, i+2] = StatusList[i].StatusName;
                ws.Cells[1, i + 2].Font.Bold = true;
                //ws.Cells[1, i + 2].Text.Align=
            }
            ws.Cells[1, StatusList.Count+2] = "Total";
            ws.Cells[1, StatusList.Count+2].Font.Bold = true;
            int cell = 2;
            foreach (var item in Obj.ContractorWiseReport)
            {
                int j = 1;
                ws.Cells[cell, j] =item.ContractorName;
                j++;
                foreach (var Status in item.StatusList)
                {
                    ws.Cells[cell, j] = Status.JobCards;
                    j++;
                }
                ws.Cells[cell, j] = item.Total;
            cell++;
            }
            string file = "GroupWiseReport_" + DateTime.Now.ToString("MM-dd-yyyy_hh_mm_ss") + ".xlsx";
            string ExportPath = root+"" + file;
            wb.SaveAs(ExportPath);
            wb.Close();
            Marshal.ReleaseComObject(wb);

            xla.Quit();
            Marshal.FinalReleaseComObject(xla);

            // Function calling for Email
            MailAddressCollection toEmail = new MailAddressCollection();
            MailAddressCollection ccEmail = new MailAddressCollection();
            MailAddressCollection frmEmail = new MailAddressCollection();
            using (var DbContext = new BSSDBEntities())
            {
                var EmailList = DbContext.TblEmailMasters.Where(x => x.AccountId == Obj.SessionData.AccountId && x.DealerId == Obj.SessionData.DealerID).ToList();
                foreach (var id in EmailList) {
                    toEmail.Add(id.Email_ID);
                }
            }
            string MailBody = ReportsDAL.GenerateHtml(Obj.SessionData);
            //ccEmail.Add("narayanjoshi25cs@gmail.com");
            //toEmail.Add("narayan.joshi@phoenixtech.consulting");
            string MailStatus=Email.sendEmail("BodyShop_Report_"+DateTime.Now.ToString("MM-dd-yyyy"), "reportsbodyshop@gmail.com", toEmail, ccEmail, ExportPath, MailBody);

            return MailStatus;

        }

        public static string GenerateHtml(SessionDataBLL Obj)
        {
            

            using (var dbContext = new BSSDBEntities())
            {
                var ReqData = dbContext.SP_StatusWiseCompleteReport(Obj.DealerID, Obj.AccountId).ToList();
                string Html = "<!DOCTYPE html><html><head><style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style></head><body><br /> <label>Please find attachment for group wise report. Status wise report is Shown below :-</label><br /><br /><table><thead><tr><th>Status Name:</th><th>No. Of JobCards</th></tr></thead>";
                Html = Html + "<tbody>";

                foreach (var data in ReqData)
                {
                    Html = Html + "<tr><td>"+ data .StatusName+ "</td><td>"+ data .NoOfJobCards+ "</td></tr>";

                }
                Html = Html + "</tbody></table><br /><br />";
                Html = Html + "<label>Regards,</label><br /><label>BodyShop Application</label><br /><br />";
                Html = Html + "<label>This is an auto generated mail, Do not reply</label><br /></body></html>";
                return Html;
            }
        }

        public static List<ModelAndFuelTypeStatus> MonthAndFuelTypeStatus(FilterData Obj)
        {
            using (var Context = new BSSDBEntities())
            {
                List<ModelAndFuelTypeStatus> ModelAndFuelTypeStatus = null;
                var ReqData = Context.sp_ModelAndFuelTypeStatus(Obj.DealerID, Obj.AccountId, Obj.Month, Obj.Year).ToList();
                if (ReqData.Count != 0)
                {

                    ModelAndFuelTypeStatus = ReqData.Select(x => new ModelAndFuelTypeStatus
                    {
                        GrandTotal = x.GrandTotal == null ? 0 : x.GrandTotal,
                        Model = x.Model
                    }).ToList();
                    ModelAndFuelTypeStatus.Add(new BODYSHPBLL.ImplBLL.ModelAndFuelTypeStatus {
                        Model="Total",
                        GrandTotal= ReqData.Sum(x=>x.GrandTotal)
                    });
                    return ModelAndFuelTypeStatus;
                }
                else {
                    return ModelAndFuelTypeStatus;
                }
            }
        }
        public static List<PanelWiseStatus> PanelWiseStatus(FilterData Obj)
        {
            using (var Context = new BSSDBEntities())
            {
                List<PanelWiseStatus> PanelWiseStatus = null;
                var ReqData = Context.sp_PanelWiseStatus(Obj.DealerID, Obj.AccountId, Obj.Month, Obj.Year).ToList();
                if (ReqData.Count != 0)
                {
                    PanelWiseStatus = ReqData.Select(x => new PanelWiseStatus
                    {
                       Model=x.Model,
                       PanelReplaced=x.PanelReplaced,
                       Total=x.Total
                    }).ToList();
                   
                    return PanelWiseStatus;
                }
                else
                {
                    return PanelWiseStatus;
                }
            }
        }
    }
}
