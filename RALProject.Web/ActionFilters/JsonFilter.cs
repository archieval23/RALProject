using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace RALProject.Web.ActionFilters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class JsonFilter : ActionFilterAttribute
    {
        public string Param { get; set; }
        public Type JsonDataType { get; set; }
        public string PropertyName { get; set; }        

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string inputContent = filterContext.RequestContext.HttpContext.Items["jsonInput"] as string;
            if (String.IsNullOrEmpty(inputContent))
            {
                using (var sr = new StreamReader(filterContext.HttpContext.Request.InputStream))
                {
                    inputContent = sr.ReadToEnd();
                    filterContext.RequestContext.HttpContext.Items["jsonInput"] = inputContent;
                }
            }
                        
            JObject parameters = JObject.Parse(inputContent);
            var selectedParameter = parameters[PropertyName].ToString();
            //var obj = JsonConvert.DeserializeObject(inputContent, JsonDataType);
            //var obj = JsonConvert.DeserializeObject<dynamic>(selectedParameter);
            var obj = JsonConvert.DeserializeObject<IList<Dictionary<string, string>>>(selectedParameter);
            filterContext.ActionParameters[Param] = obj;
        }
    }
}
