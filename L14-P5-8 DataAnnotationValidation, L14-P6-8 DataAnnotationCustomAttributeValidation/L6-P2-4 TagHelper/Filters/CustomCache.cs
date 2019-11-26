using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace L6_P2_4_TagHelper.Filters
{

    public class CustomCacheAttribute : Attribute, IActionFilter
    {
        private readonly int _duration;
        private static Dictionary<string, (DateTime Time, ViewResult Content)> CacheDict { get; set; }
        private string _cacheKey;

        public CustomCacheAttribute(int duration)
        {
            _duration = duration;

            CacheDict = new Dictionary<string, (DateTime, ViewResult)>();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _cacheKey = context.HttpContext.Request.Path.ToString();
            if (CacheDict.ContainsKey(_cacheKey))
            {
                var cachedValue = CacheDict[_cacheKey];
                if ((DateTime.Now - cachedValue.Time).TotalSeconds < _duration)
                    context.Result = cachedValue.Content;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!string.IsNullOrEmpty(_cacheKey))
            {
                if (context.Result is ViewResult result)
                {
                    CacheDict[_cacheKey] = (DateTime.Now, result);
                }
            }
        }
    }
}
