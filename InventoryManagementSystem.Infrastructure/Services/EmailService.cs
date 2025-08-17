using System.Net;
using System;
using System.Net.Mail;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSystem.Data.Models;
using System.Collections.Generic;

namespace InventoryManagementSystem.Infrastructure.Services
{
    public class EmailService
    {
        private static string _mail = ConfigurationManager.AppSettings["Email"].ToString();
        private static string _mailName = ConfigurationManager.AppSettings["EmailName"].ToString();
        private static string _HostAddress = ConfigurationManager.AppSettings["HostAddress"].ToString();

        public static void SendMail(string to, string subject, string body)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            MailAddress mailFrom = new MailAddress(_mail, _mailName);
            MailAddress mailTo = new MailAddress(to, to);
            MailMessage mailMessage = new MailMessage(mailFrom, mailTo);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
        }

        public static void SendMail(string fromEmail, string toEmail, string subject, string body)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            MailMessage mailMessage = new MailMessage(fromEmail, toEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
        }

        public static async Task SendMailAsync(string to, string subject, string body)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            MailAddress mailFrom = new MailAddress(_mail, _mailName);
            MailAddress mailTo = new MailAddress(to, to);
            MailMessage mailMessage = new MailMessage(mailFrom, mailTo);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient();
            await smtpClient.SendMailAsync(mailMessage);
        }
        public static class EmailMessage
        {
            public static string AccountCreated(string fullName, string uname, string password)
            {
                StringBuilder body = new StringBuilder();
                body.AppendFormat("Dear {0},<br/><br/>You are registered as a new user in the Inventory System.<br/>Your details are: -<br/>&nbsp;&nbsp;&nbsp;&nbsp;UserName: {1}<br/>&nbsp;&nbsp;&nbsp;&nbsp;Password: {2}<br/>Please use the following link to Login to Inventory System: {3}<br/><br/><u><strong>Note: Please change your password in format(1 capital letter, 1 special character, 1 numeric digit and at least 6 characters)</strong></u><br/><br/><br/>Kind Regards,<br/>Admin", fullName, uname, password, _HostAddress);
                return body.ToString();
            }
            public static string ForgotPassword(string fullName, string password)
            {
                StringBuilder body = new StringBuilder();
                body.AppendFormat("Dear {0},<br/><br/>Your password has been reset. <br/><br/>Your new password is: {1}<br/>Please use the following link to Login to Inventory Management System: {2}<br/><br/><u><strong>Note: Please change your password in format(1 capital letter, 1 special character, 1 numeric digit and at least 6 characters)</strong></u><br/><br/><br/>Kind Regards,<br/>Admin", fullName, password, _HostAddress);
                return body.ToString();
            }
            public static string ApprovedRequest(string touser, string fromuser)
            {
                StringBuilder body = new StringBuilder();
                body.AppendFormat("Dear {0},<br/><br/>{1} has sent Requisiton for Approval.<br/><br/>Please Verify it and Approve.<br/><br/>Please use the following link to Login to Inventory System: {2}<br/><br/>Kind Regards,<br/>Admin", touser, fromuser, _HostAddress);
                return body.ToString();
            }
            public static string ReceiverDispatchProductDetails(string touser, string fromuser, string data, string disptmes)
            {
                StringBuilder body = new StringBuilder();
                body.AppendFormat("Dear {0},<br/><br/>{1} has dispatched the Following Products.<br/><b>Dispatch Message : </b>{3}<br/>Please Verify it and Acknowledge.<br/>{2} Please use the following link to Login to Inventory System: {4}<br/><br/>Kind Regards,<br/>Admin", touser, fromuser, data, disptmes, _HostAddress);
                return body.ToString();
            }
            public static string SenderDispatchProductDetails(string touser, string fromuser, string data, string branchname, string disptmes)
            {
                StringBuilder body = new StringBuilder();
                body.AppendFormat("Dear {1},<br/><br/>You have dispatched the Following Products to <br/> <b>Branch:</b> {3} <br/> <b>Requester/Receiver :</b> {0} <br/><b>Dispatch Message : </b>{4}<br/> Please Verify it and Confirm.<br/>{2} Please use the following link to Login to Inventory System: {5}<br/><br/>Kind Regards,<br/>Admin", touser, fromuser, data, branchname, disptmes, _HostAddress);
                return body.ToString();
            }

            //for branch
            public static string ApprovalRequestOfProductDetails(string touser, string fromuser, string data, string reqmes)
            {
                StringBuilder body = new StringBuilder();
                body.AppendFormat("Dear {0},<br/><br/>{1} has requested for the Following Products.<br/><b>Requisition Message : </b>{3}<br/> Please Verify it and Approve The Requested Products.<br/>{2} Please use the following link to Login to Inventory System: {4}<br/><br/>Kind Regards,<br/>Admin", touser, fromuser, data, reqmes, _HostAddress);
                return body.ToString();
            }

            //for branch
            public static string ApprovalRequestOfProductDetail(string touser, string fromuser, string data, string reqmes, string frombranch)
            {
                StringBuilder body = new StringBuilder();
                body.AppendFormat("Dear {0},<br/><br/>{1} has transferred the Following Products from <b>{6}</b>.<br/><b>Remarks : </b>{3}<br/> Please Verify it and Acknowledge the transferred Products.<br/>{2} Please use the following link to Login to Inventory System: {4}<br/><br/>Kind Regards,<br/>{5}", touser, fromuser, data, reqmes, _HostAddress, fromuser, frombranch);

                return body.ToString();
            }
            //for corporate office --binod thapa
            public static string ApprovedProductDetails(string touser, string fromuser, string data, string branchname, string reqmes, string approvemes)
            {
                StringBuilder body = new StringBuilder();
                body.AppendFormat("Dear {0},<br/><br/>{1} has requested for the Following Products.<br/> <b>Requesting Branch:</b> {3} <br/> <b>Requesting User :</b> {1} <br/><b>Requisition Message : </b>{4}<br/><b>Approved Message : </b>{5}<br/> Please Verify it and Forward The Requested Products.<br/>{2} Please use the following link to Login to Inventory System: {6}<br/><br/>Kind Regards,<br/>Admin", touser, fromuser, data, branchname, reqmes, approvemes, _HostAddress);
                return body.ToString();
            }
            public static string RejectPlaceRequisition(string touser, string fromuser, string rejectmes)
            {
                StringBuilder body = new StringBuilder();
                body.AppendFormat("Dear {1},<br/><br/>Your Requisition has been rejected by {0} <br/> <b>Rejection Reason :</b> {2}<br/>  Please use the following link to Login to Inventory System: {3}<br/><br/>Kind Regards,<br/>Admin", touser, fromuser, rejectmes, _HostAddress);
                return body.ToString();
            }
            public static string LowStockLevelProductDetails(string user, string data)
            {
                StringBuilder body = new StringBuilder();
                body.AppendFormat("Dear {0},<br/><br/>The Following Products Stock have Low Stock than Re-Order Level.<br/>Please Verify it.<br/>{1} Please use the following link to Login to Inventory System: {2}<br/><br/>Kind Regards,<br/>Admin", user, data, _HostAddress);
                return body.ToString();
            }
            public static string FuelRequestRecommendation(string touser, string fromuser, string data, string reqmes)
            {
                StringBuilder body = new StringBuilder();
                body.AppendFormat("Dear {0},<br/><br/>{1} has requested for the Fuel.<br/><b>Requisition Message : </b>{3}<br/> Please Verify it and Recommend The fuel Request.<br/>{2} Please use the following link to Login to Inventory System: {4}<br/><br/>Kind Regards,<br/>Admin", touser, fromuser, data, reqmes, _HostAddress);
                return body.ToString();
            }
            public static string FuelApprovalRequest(string touser, string fromuser, string data, string reqmes)
            {
                StringBuilder body = new StringBuilder();
                body.AppendFormat("Dear {0},<br/><br/>{1} has recomended for the Approval of Fuel.<br/><b>Recommended Message : </b>{3}<br/> Please Verify it and Approve for the fuel Request.<br/>{2} Please use the following link to Login to Inventory System: {4}<br/><br/>Kind Regards,<br/>Admin", touser, fromuser, data, reqmes, _HostAddress);
                return body.ToString();
            }

            public static string FuelRequestApproved(string touser, string fromuser, string data, string approvedmes)
            {
                StringBuilder body = new StringBuilder();
                body.AppendFormat("Dear {0},<br/><br/>{1} has approved your fuel request.<br/><b>Approval Message : </b>{3}<br/> Please  verify it and collect the token.<br/>{2} Please use the following link to Login to Inventory System: {4}<br/><br/>Kind Regards,<br/>Admin", touser, fromuser, data, approvedmes, _HostAddress);
                return body.ToString();
            }


            public static string FuelRequestRejected(string touser, string fromuser, string data, string rejmes)
            {
                StringBuilder body = new StringBuilder();
                body.AppendFormat("Dear {0},<br/><br/>{1} has Rejected your fuel request.<br/><b>Rejected Message : </b>{3}<br/><br/>{2} Please use the following link to Login to Inventory System: {4}<br/><br/>Kind Regards,<br/>Admin", touser, fromuser, data, rejmes, _HostAddress);
                return body.ToString();
            }
        }
    }
}
