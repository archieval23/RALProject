using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.Common.ErrorHandler
{
    public static class ExceptionMapping
    {
        public static CustomException Map(Exception error)
        {
            var customException = new CustomException();
            var serverMessage = error.InnerException != null ? error.InnerException.ToString() : error.Message;
            var clientMessage = string.Empty;

            //For Debugging            
            //clientMessage = serverMessage;

            foreach (var errorData in OpenJsonFile())
            {
                errorData.Value.ToList().ForEach(e => {
                    if (serverMessage.Contains((string)e["ServerErrorContains"]))
                        clientMessage = (string)e["Message"]; 
                });
            }

            if (!string.IsNullOrEmpty(clientMessage))
                customException.ExceptionHandled = true;

            customException.Message = clientMessage;

            return customException;
        }

        private static JObject OpenJsonFile()
        {
            string currentDir = Environment.CurrentDirectory;
            var jsonFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine("bin", "ErrorHandler", "errordata.json"));
            using (StreamReader reader = new StreamReader(jsonFile))
            {
                var json = reader.ReadToEnd();
                return JObject.Parse(json);
            }
        }
    }
}
