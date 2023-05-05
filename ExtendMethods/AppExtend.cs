using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DoAnLapTrinhWebNC.ExtendMethods
{
    public static class AppExtend
    {
        public static void AddStatusCodePage(this IApplicationBuilder app)
        {
            app.UseStatusCodePages(appErorr =>
            {
                appErorr.Run(async context =>
                {
                    var respose = context.Response;
                    var code = respose.StatusCode;

                    var content = @$"<html>
                 <head>
                 <meta charset='UTF8'/>
                 <title>Loi {code}</title>
                 </head>
                 <body>
                    <p style='color: red; font-size: 30px'>
                    Co loi xay ra : {code} - {(HttpStatusCode)code}
                    </p>
                 </body>
                 
                 </html>";

                    await respose.WriteAsync(content);
                });
            });// loi code 400-599
        }
    }
}