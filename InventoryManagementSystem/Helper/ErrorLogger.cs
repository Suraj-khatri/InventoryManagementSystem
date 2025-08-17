using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;

namespace InventoryManagementSystem.Helper
{
    public class ErrorLogger
    {
        public static string ErrorLine { get; set; }
        public static string ErrorMessage { get; set; }
        public static string ErrorType { get; set; }
        public static string ExceptionURL { get; set; }
        public static string IPAddress { get; set; }
        public static string ErrorLocation { get; set; }
        public static string HostAdd { get; set; }
        public static string Username { get; set; }

        public static string LogError(Exception ex)
        {
            var context = HttpContext.Current;
            if (ex == null)
                return "An error occurred. Please try again later.";

            try
            {
                string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                string logFilePath;

                logFilePath = Path.Combine(logDirectory, "error_log.txt");

                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                string filePath = logFilePath;
                //string filePath = HttpContext.Current.Server.MapPath("~/ExceptionDetailsFile/Error.txt");

                string username = HttpContext.Current.Session?["UserId"]?.ToString() ?? "";
                string branchName = HttpContext.Current.Session?["BranchId"]?.ToString() ?? "";
                string errorType = ex.GetType().Name;
                string exceptionUrl = HttpContext.Current?.Request.Url?.ToString() ?? "";
                string errorLocation = ex.Message;
                string hostName = Dns.GetHostName();
                string ipAddress = Dns.GetHostEntry(hostName).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?.ToString() ?? "";

                string logMessage = $"Log Written Date: {DateTime.Now}{Environment.NewLine}" +
                                    $"Error Message: {errorLocation}{Environment.NewLine}" +
                                    $"Username: {username}{Environment.NewLine}" +
                                    $"BranchName: {branchName}{Environment.NewLine}" +
                                    $"Error Type: {errorType}{Environment.NewLine}" +
                                    $"Error Location: {errorLocation}{Environment.NewLine}" +
                                    $"Exception URL: {exceptionUrl}{Environment.NewLine}" +
                                    $"Host Name: {hostName}{Environment.NewLine}" +
                                    $"IP Address: {ipAddress}{Environment.NewLine}" +
                                    $"Exception Stack Trace: {ex.StackTrace}{Environment.NewLine}" +
                                    $"----------------------------------------------------------------{Environment.NewLine}{Environment.NewLine}";

                File.AppendAllText(filePath, logMessage);

                return $"{ex.Message}{Environment.NewLine}{errorType}";
            }
            catch (Exception ee)
            {
                return $"Error occurred while logging: {ee}";
            }
        }

        public static string DispatchedLog(string message)
        {
            var context = HttpContext.Current;
            if (message == null)
                return "An error occurred. Please try again later.";

            try
            {
                string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                string logFilePath;

                logFilePath = Path.Combine(logDirectory, "dispatch_log.txt");

                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                string filePath = logFilePath;
                //string filePath = HttpContext.Current.Server.MapPath("~/ExceptionDetailsFile/Error.txt");

                string username = HttpContext.Current?.Session?["UserId"]?.ToString() ?? "";
                string branchName = HttpContext.Current?.Session?["BranchId"]?.ToString() ?? "";
                string exceptionUrl = HttpContext.Current?.Request.Url?.ToString() ?? "";
                string hostName = Dns.GetHostName();
                string ipAddress = Dns.GetHostEntry(hostName).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?.ToString() ?? "";

                string logMessage = $"Log Written Date: {DateTime.Now}{Environment.NewLine}" +
                                    $"Username: {username}{Environment.NewLine}" +
                                    $"BranchName: {branchName}{Environment.NewLine}" +
                                    $"Exception URL: {exceptionUrl}{Environment.NewLine}" +
                                    $"Host Name: {hostName}{Environment.NewLine}" +
                                    $"IP Address: {ipAddress}{Environment.NewLine}" +
                                    $"Exception Stack Trace: {message}" +
                                    $"----------------------------------------------------------------{Environment.NewLine}{Environment.NewLine}";

                File.AppendAllText(filePath, logMessage);

                return $"";
            }
            catch (Exception ee)
            {
                return $"Error occurred while logging: {ee}";
            }
        }
    }
}