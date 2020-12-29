using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BODYSHPDAL.DbContext;
using BODYSHPBLL.ImplBLL;

namespace BODYSHPDAL.ImplDAL
{
    public class GridViewDAL
    {
        public static List<CompleteDetailsModel> GetData(SessionDataBLL Obj)
        {
            //List<SpGetCompleteDetails_Result> ListData = null;
            using( var dbContext=new BSSDBEntities())
            {
                var ReqData = dbContext.SpGetCompleteDetails(Obj.DealerID,Obj.AccountId);
                List<CompleteDetailsModel> ListData = ReqData.Select(x => new CompleteDetailsModel()
                {
                    JobCardId=x.JobCardId,
                    JobCardNo = x.JobCardNo,
                    CustomerCategory = x.CustomerCategory,
                    CustomerName = x.CustomerName,
                    DateAndTime = x.DateAndTime,
                    Model = x.Model,
                    PhoneNo = x.PhoneNo,
                    PromisedDate = x.PromisedDate,
                    SA = x.SA,
                    Service = x.Service,
                    RegistrationNo = x.RegistrationNo,
                    PSFStatus = x.PSFStatus,
                    Technician = x.Technician,
                    DealerID = x.DealerID,
                    CreatedBy = x.CreatedBy,
                    Id = x.Id,
                    No_Of_Panel = x.No_Of_Panel,
                    PanelReplaced = x.PanelReplaced,
                    Status = x.Status,
                    StatusID = x.StatusID,
                    ModifiedDate_Formatted = x.ModifiedDate_Formatte,
                    Contractor_Id = x.Contractor_Id,
                    ContractorName = x.ContractorName,
                    InsCompId = x.InsuranceCompanyId,
                    PaymentMode=x.PaymentMode,
                    Glass=x.NoOfGlass,
                    PhotoUrl=x.PhotoUrl
                }).ToList();

                return ListData;
            }

            
        }

        public static List<JobcardDetails> GetDetailForJobcard(string JobcardNo)
        {
            //List<SpGetCompleteDetails_Result> ListData = null;
            using (var dbContext = new BSSDBEntities())
            {
                var ReqData = dbContext.sp_GetDetailForJobcard(JobcardNo).ToList();
                if (ReqData.Count != 0)
                {
                    List<JobcardDetails> ListData = ReqData.Select(x => new JobcardDetails()
                    {
                        JobCardId = x.JobCardId,
                        JobCardNo = x.JobCardNo,
                        CustomerCategory = x.CustomerCategory,
                        CustomerName = x.CustomerName,
                        DateAndTime = x.DateAndTime,
                        Model = x.Model,
                        PhoneNo = x.PhoneNo,
                        PromisedDate = x.PromisedDate,
                        SA = x.SA,
                        Service = x.Service,
                        RegistrationNo = x.RegistrationNo,
                        PSFStatus = x.PSFStatus,
                        Technician = x.Technician,
                        DealerID = x.DealerID,
                        CreatedBy = x.CreatedBy,
                        No_Of_Panel = x.No_Of_Panel,
                        PanelReplaced = x.PanelReplaced,
                        StatusID = x.StatusID,
                        ContractorName = x.ContractorName,
                        PaymentMode = x.PaymentMode,
                        PhotoUrl = x.PhotoUrl,
                        NoOfGlass = x.NoOfGlass,
                        ContractorCode = x.ContractorCode,
                        AccountID = x.AccountID,
                        ContractorId = x.ContractorId,
                        CreationDate = x.CreationDate,
                        InsuranceCompanyId = x.InsuranceCompanyId,
                        ModifiedDate = x.ModifiedDate,
                        MogifiedBy = x.MogifiedBy,
                        PhotoId = x.PhotoId,
                        ReadyDateTime = x.ReadyDateTime,
                        RevPromisedDate = x.RevPromisedDate,
                        StatusAddedDate = x.StatusAddedDate,
                        StatusCode = x.StatusCode,
                        StatusModifiedDate = x.StatusModifiedDate,
                        StatusName = x.StatusName,
                        CompanyName = x.CompanyName
                    }).ToList();
                    return ListData;
                }
                else { return null; }
               
            }


        }
    }
}
