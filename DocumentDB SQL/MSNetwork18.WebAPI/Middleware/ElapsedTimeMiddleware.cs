using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;

namespace MSNetwork18.WebAPI.Middleware
{
    public class ElapsedTimeMiddleware
    {
        public RequestDelegate Next { get; set; }
        public string Environment { get; set; }

        public ElapsedTimeMiddleware(RequestDelegate next, IHostingEnvironment environment)
        {
            Next = next;
            Environment = environment.EnvironmentName;
        }

        public async Task Invoke(HttpContext context)
        {
            Stream originalBody = context.Response.Body;

            try
            {
                Stopwatch requestTime = Stopwatch.StartNew();

                using (MemoryStream memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;
                    await Next(context);

                    if (context.Response.ContentType != null && context.Response.ContentType.Contains("application/json"))
                    {
                        memStream.Position = 0;
                        string responseBody = new StreamReader(memStream).ReadToEnd();

                        context.Response.Headers.Add("X-Network", new[] { Environment });

                        var developmentModel = new
                        {
                            body = JArray.Parse(responseBody),
                            meta_data = new MetaEnvModel()
                            {
                                Environment = Environment,
                                ElapsedMiliseconds = requestTime.ElapsedMilliseconds
                            }
                        };

                        byte[] content = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(developmentModel));

                        memStream.Position = 0;
                        await memStream.WriteAsync(content, 0, content.Length);

                        memStream.Position = 0;
                        await memStream.CopyToAsync(originalBody);
                    }
                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }
    }
}
