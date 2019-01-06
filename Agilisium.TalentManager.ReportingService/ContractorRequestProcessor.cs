using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.ServerUtilities;
using Agilisium.TalentManager.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agilisium.TalentManager.ReportingService
{
    public class ContractorRequestProcessor
    {
        private IServiceRequestService requestService;
        private ISystemSettingsService settingsService;

        public ContractorRequestProcessor(IServiceRequestService requestService, ISystemSettingsService settingsService)
        {
            this.requestService = requestService;
            this.settingsService = settingsService;
        }

        public void ProcessPendingServiceRequests(string templateFilePath)
        {
            try
            {
                string emailTemplateContent = FilesHandler.GetFileContent(templateFilePath);
                List<ServiceRequestDto> requests = requestService.GetAllEmailPendingRequests();

                string emailClientIP = settingsService.GetSystemSettingValue("Email Proxy Server");
                string fromEmailID = settingsService.GetSystemSettingValue("Contractor Request Email Owner");
                string bccEmailID = settingsService.GetSystemSettingValue("Contractor Request Email BCC Email IDs");
                string emailSubject = "New Contractor Request for Agilisium";

                foreach (var request in requests)
                {
                    try
                    {
                        StringBuilder vendorEmail = new StringBuilder(emailTemplateContent);
                        vendorEmail.Replace("__VENDOR_POC_NAME__", request.VendorName);
                        vendorEmail.Replace("__TECHNOLOGY_NAME__", request.RequestedSkill);
                        vendorEmail.Replace("__EMAIL_BODY__", request.EmailMessage);
                        EmailHandler.SendEmail(emailClientIP, fromEmailID,request.VendorEmailID, emailSubject, vendorEmail.ToString(), bccEmailID);
                    }
                    catch (Exception exp)
                    {}
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
