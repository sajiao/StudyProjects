using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Middleware
{
    public class StopWatch
    {
        public DateTime StartTime { get; private set; } = DateTime.Now;

        public void Start()
        {
            StartTime = DateTime.Now;
        }

        public double GetMillionSeconds()
        {
            return (DateTime.Now - StartTime).TotalMilliseconds;
        }
    }
    /// <summary>
    /// 计时器中间件
    /// </summary>
    public class TimeMiddleware
    {
        private RequestDelegate _next;

        private StopWatch _watch;

        public TimeMiddleware(RequestDelegate next, StopWatch watch)
        {
            _next = next;
            _watch = watch;
        }

        public async Task Invoke(HttpContext context)
        {
            //await context.Response.WriteAsync($"没有权限");
            //return;
            _watch?.Start();
            await _next.Invoke(context);
            var body = context.Response.Body.ToString();
            await context.Response.WriteAsync($"共耗时:{_watch?.GetMillionSeconds()} 毫秒!");
        }

    }
}
