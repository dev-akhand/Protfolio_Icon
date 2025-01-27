using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Protfolio_Icon
{
    public class ErrorHandleMiddleWare
    {

        private readonly RequestDelegate _next;
        private readonly IConfiguration configuration;
        public ErrorHandleMiddleWare(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            this.configuration = configuration;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                var NumberFormat = CultureInfo.CreateSpecificCulture("en-US");
                NumberFormat.NumberFormat.CurrencyNegativePattern = 1;
                Thread.CurrentThread.CurrentUICulture = NumberFormat;
                Thread.CurrentThread.CurrentCulture = NumberFormat;
                if (context.Request.Path.Value == "/Home/Error")
                {
                    context.Response.StatusCode = 500;
                }
                await _next(context);
                if (context.Response.StatusCode != 200)
                {
                    var response = context.Response;
                    if (response.StatusCode == 404)
                    {
                        response.Redirect("/Home/Error404", true);
                    }
                }
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                if ((context.Request.Cookies["UserId"] == null || string.IsNullOrEmpty(context.Request.Cookies["UserId"])) || (context.Request.Cookies["AccountId"] == null) || string.IsNullOrEmpty(context.Request.Cookies["AccountId"]))
                {
                    context.Response.Redirect("/login", false);
                }
                else
                {
                    var userId = context.Request.Cookies["UserId"];
                    //ErrorLog errorLog = new ErrorLog();
                    //var hostname = response.HttpContext.Request.Host.Value;
                    //errorLog.URL = hostname + response.HttpContext.Request.Path.Value;
                    //errorLog.ErrorMessage = error?.Message;
                    //errorLog.StackTrace = error?.StackTrace;
                    //errorLog.UserId = userId;
                    //errorLog.AccountId = context.Request.Cookies["AccountId"];
                    //AddLog(errorLog);
                    //EmailHelperNew helper = new EmailHelperNew(configuration);
                    //helper.ExceptionMail(errorLog);
                    response.Redirect("/Home/Error", true);
                }
            }
        }
        //private void AddLog(ErrorLog errorLog)
        //{
        //    try
        //    {
        //        _errorLogRepository.Save(errorLog).Wait();
        //        var basePath = (Environment.CurrentDirectory + "\\" ?? "C:\\inetpub\\wwwroot\\swordfish\\") + "Logs\\";
        //        var logpath = basePath + DateTime.Today.ToString("dd-MM-yyyy") + ".txt";
        //        if (!Directory.Exists(basePath))
        //        {
        //            Directory.CreateDirectory(basePath);
        //        }
        //        if (!File.Exists(logpath))
        //        {
        //            var fs = File.Create(logpath);
        //            fs.Close();
        //        }
        //        File.AppendAllText(logpath, JsonSerializer.Serialize(errorLog), Encoding.UTF8);


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}
