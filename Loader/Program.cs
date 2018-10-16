using System;
using System.Collections.Generic;
using System.Configuration;
using UpdateNewVersion;

namespace Loader
{
    /// <summary>
    /// This program will update application version from FTP server.
    /// It will take the file from the server and will update them in local.
    /// How to use?
    /// 3 files neede:
    /// 1. Loader.exe
    /// 2. Loader.config - need to declare on ftp server details, exclude extension files, application name
    /// 3. UpdateNewVersion.dll
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            UpdateManager.WriteToConsole("Execute Loader",Color.YELLOW);
            string appName = ConfigurationManager.AppSettings["appName"];
            FtpDetails ftp = new FtpDetails()
            {
                Address = ConfigurationManager.AppSettings["FtpAddress"],
                User = ConfigurationManager.AppSettings["FtpUser"],
                Password = ConfigurationManager.AppSettings["FtpPass"],
                ExcludeExtention = new List<string>(ConfigurationManager.AppSettings["excludeExtention"].Split(','))
            };

            UpdateManager.WriteToConsole($"Application Name = {appName}",Color.YELLOW);
            UpdateManager.WriteToConsole($"FTP server = {ftp.Address}",Color.YELLOW);
            UpdateManager mnger = UpdateManager.Instance;
            bool success = mnger.UpdateAssemblyVersion(ftp, appName);
            if (success)
                UpdateManager.WriteToConsole("Version updated successfully!", Color.GREEN);
            else
                UpdateManager.WriteToConsole("Version update failed!", Color.RED);
        }
    }
}
