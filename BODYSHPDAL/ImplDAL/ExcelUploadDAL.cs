using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using BODYSHPDAL.DbContext;
using BODYSHPBLL.ImplBLL;
using System.IO;

namespace CCAPPDAL.ImplDAL
{
    public class ExcelUploadDAL
    {
        public static void InsertData(DataTable info, SessionDataBLL obj)
        {


            using (var context = new BSSDBEntities())
            {

                var dt1 = context.TblJobCardHdrs.ToList();

                foreach (DataRow Dr in info.Rows)
                {

                    string JobCardNo = Dr["Job Card No."].ToString().Trim();
                    if (JobCardNo != null && JobCardNo!= string.Empty)
                    {
                        
                        //LogService("JobCardNo: " + JobCardNo);
                        //LogService("************************: " + DateTime.Now.ToString());

                        string DateAndTime1 = Convert.ToString(Dr["Date & Time"]);
                        //LogService("DateAndTime1: " + DateAndTime1);
                        //LogService("************************: " + DateTime.Now.ToString());

                        DateTime DateAndTime = Convert.ToDateTime(DateAndTime1);
                        //LogService("DateAndTime: " + DateAndTime);
                        //LogService("************************: " + DateTime.Now.ToString());

                        //DateTime DateAndTime = Convert.ToDateTime(Dr["Date & Time"]);
                        string CustomerName = Convert.ToString(Dr["Customer Name"]);
                        long PhoneNo = Convert.ToInt64(Dr["Phone & Mobile No."]);
                        string CustomerCategory = Convert.ToString(Dr["Customer Catg."]);
                        string PSFStatus = Convert.ToString(Dr["PSF Status"]);
                        string RegistrationNo = Convert.ToString(Dr["Registration"]);
                        string Model = Convert.ToString(Dr["Model"]);
                        string SA = Convert.ToString(Dr["S.A"]);
                        string Technician = Convert.ToString(Dr["Technician"]);
                        string Service = Convert.ToString(Dr["Service"]);
                        string PromisedDate = Convert.ToString(Dr["Promised Dt."]);
                        //LogService("PromisedDate: " + PromisedDate);
                        //LogService("************************: " + DateTime.Now.ToString());

                        string RevPromisedDate = Convert.ToString(Dr["Rev. Promised Dt."]);// Dr["Rev. Promised Dt."].ToString();
                        //LogService("RevPromisedDate: " + RevPromisedDate);
                        //LogService("************************: " + DateTime.Now.ToString());

                        //DateTime RevDATE = Convert.ToDateTime(Dr["Rev. Promised Dt."]);
                        string ReadyDateTime = Convert.ToString(Dr["Ready Date & Time"]);
                        //LogService("ReadyDateTime: " + ReadyDateTime);
                        //LogService("************************: " + DateTime.Now.ToString());

                        var Check = context.TblJobCardHdrs.Where(x => x.JobCardNo == JobCardNo && x.DealerID==obj.DealerID).FirstOrDefault();

                        if (Check == null)
                        {
                            
                            TblJobCardHdr dt = new TblJobCardHdr
                            {
                                JobCardNo= JobCardNo,
                                DateAndTime= DateAndTime,
                                CustomerName= CustomerName,
                                PhoneNo= PhoneNo,
                                CustomerCategory= CustomerCategory,
                                PSFStatus= PSFStatus,
                                RegistrationNo= RegistrationNo,
                                Model= Model,
                                SA= SA,
                                Technician= Technician,
                                Service= Service,
                                PromisedDate= PromisedDate,
                                RevPromisedDate= RevPromisedDate,
                                ReadyDateTime = ReadyDateTime,
                                AccountID=obj.AccountId,
                                DealerID=obj.DealerID,
                                
                                CreatedBy= obj.UserId,
                                CreationDate=DateTime.Now
                            
                            };
                            context.Entry(dt).State = EntityState.Added;
                            context.SaveChanges();
                            
                           
                            
                            InsertIntoTblDtl(JobCardNo, obj);
                            
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }


        public static void InsertIntoTblDtl(string JobCardNo, SessionDataBLL obj)
        {

            using (var context = new BSSDBEntities())
            {
                var JobCardId = context.TblJobCardHdrs.Where(x => x.JobCardNo == JobCardNo &&x.DealerID==obj.DealerID).Select(x => x.JobCardId).FirstOrDefault();

                var Check = context.TblJobCardDtls.Where(x => x.JobCardId == JobCardId).FirstOrDefault();

                if (Check == null)
                {
                    TblJobCardDtl tb = new TblJobCardDtl
                    {
                        JobCardId=JobCardId,
                        StatusID=17,
                        IsDeleted = false,
                        CreatedBy =obj.UserId,
                        CreationDate=DateTime.Now,
                        //ModifiedBy=obj.UserId,
                        //ModifiedDate=DateTime.Now

                    };

                    context.Entry(tb).State = EntityState.Added;
                    context.SaveChanges();
                }
               
            }
        }

        private static void LogService(string content)
        {
            //FileStream fs = new FileStream("E:\\BodyshopLog.txt", FileMode.OpenOrCreate, FileAccess.Write);
            FileStream fs = new FileStream("C:\\PublishBodyShop\\Log\\BodyshopLog.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(string.Format(content, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
            sw.Flush();
            sw.Close();
        }


        public static void InsertIntoTblContractor(DataTable info, SessionDataBLL obj)
        {


            using (var context = new BSSDBEntities())
            {

                

                foreach (DataRow Dr in info.Rows)
                {

                    string ContractorName = Dr["ContractorName"].ToString().Trim();
                    string ContractorCode = Convert.ToString(Dr["ContractorCode"]);
                    if (ContractorCode != null && ContractorCode != string.Empty && ContractorName != null && ContractorName != string.Empty)
                    {
                                                
                         
                        
                        
                        string ContractorAddress = Convert.ToString(Dr["ContractorAddress"]);
                        string PhoneNo = Convert.ToString(Dr["PhoneNo"]);

                        var Check = context.tblContractorMasters.Where(x => x.ContractorCode== ContractorCode && x.DealerID == obj.DealerID).FirstOrDefault();

                        if (Check == null)
                        {

                            tblContractorMaster dt = new tblContractorMaster
                            {
                                ContractorCode=ContractorCode,
                                ContractorName=ContractorName,
                                IsDeleted=false,
                                Address=ContractorAddress,
                                AccountID = obj.AccountId,
                                DealerID = obj.DealerID,
                                Phone= PhoneNo,
                                CreatedBy = Convert.ToString(obj.UserId),
                                CreationDate = DateTime.Now

                            };
                            context.Entry(dt).State = EntityState.Added;
                            context.SaveChanges();



                           

                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }

    }
}
