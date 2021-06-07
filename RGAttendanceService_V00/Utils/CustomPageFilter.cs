using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace RGAttendanceService_V00.Utils
{
    public class CustomPageFilter : IPageFilter
    {
        public CustomPageFilter(IConfiguration _configuration)
        {

        }
        public void OnPageHandlerSelected(PageHandlerSelectedContext pageContext)
        {
            int a = 0;

        }
        public void OnPageHandlerExecuting(PageHandlerExecutingContext pageContext)
        {
            int a = 0;
        }
        public void OnPageHandlerExecuted(PageHandlerExecutedContext pageContext)
        {
            var result = pageContext.Result;
            if(result is PageResult)
            {
                var page = ((PageResult)result);
                page.ViewData["filterMessage"] = "RGAttendanceService";
            }
        }

    }
}
