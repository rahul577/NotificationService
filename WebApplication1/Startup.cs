using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapPost("/NotificationService", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                    new ToastContentBuilder()
                    //.AddArgument("action", "viewConversation")
                    //.AddArgument("conversationId", 9813)
                    .AddText(context.Request.Form["notificationMessage"])
                    .AddText("Click to see options to watch")
                    .SetProtocolActivation(new Uri(context.Request.Form["deepLink"]))
                    .Show(toast =>
               {
                   toast.ExpirationTime = DateTime.Now.AddMinutes(1);
               });
                });
            });

            // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
           // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 6 (or later), then your TFM must be net6.0-windows10.0.17763.0 or greater
        }
    }
}
