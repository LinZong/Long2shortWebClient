//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Long2short;
using Long2shortWebClient.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.IO;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Long2shortWebClient.Controllers
{
    public class lController : Controller
    {
        // GET: /<controller>/

        public bool CreateConvertHandler(out ProcessShortId processShortId)
        {
            try
            {
                StreamReader sr = new StreamReader("MongoConfig.json", Encoding.Default);
                string JsonString = sr.ReadToEnd();
                JObject jObject = (JObject)JsonConvert.DeserializeObject(JsonString);
                processShortId = new ProcessShortId(jObject["Username"].ToString(), jObject["Password"].ToString(), jObject["Database"].ToString(), jObject["Collection"].ToString(), jObject["ServerAddress"].ToString());
                return true;
            }
            catch 
            {
                processShortId = null;
                return false;
            }
            
        }

        public void GetLongURL(string shorturl)
        {
            if (shorturl != null)
            {
                ProcessShortId processShortId;
                if (CreateConvertHandler(out processShortId))
                {
                    string longidArgs;
                    bool status = processShortId.GetLongId(shorturl, out longidArgs);
                    if (status)
                    {
                        Response.Redirect(longidArgs, true);
                    }
                }
                                
            }   
        }

        public IActionResult GetShortURL(GenerateShortURLViewModel generateShortURLData)
        {
            ProcessShortId process;
            bool b = CreateConvertHandler(out process);
            if (!b)
            {
                return View("Error", new ErrorViewModel { ErrorReason = "出现了一些错误,您的短链接并没有生成", RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            var userid = generateShortURLData.UserDefineId;
            var userlongurl = generateShortURLData.LongAddr;
            string shortresult;
            bool GetShortIDStatus = process.GetShortId(userid, userlongurl, out shortresult);
             if (GetShortIDStatus)
             {
                
               return View(new GenerateShortURLViewModel { Status = true, UserDefineId = userid, ShortAddr = "http://"+Request.Host+"/l/"+shortresult, LongAddr = userlongurl });
             }
             else
             {
               return View("Error", new ErrorViewModel { ErrorReason="出现了一些错误,您的短链接并没有生成", RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
             }

        }
    }
}
