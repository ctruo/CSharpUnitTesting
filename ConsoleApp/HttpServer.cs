using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    //forked: https://gist.github.com/define-private-public/d05bc52dd0bed1c4699d49e2737e80e7
    public class HttpServer
    {
        public static HttpListener listener;
        public static string url = "http://localhost:8000/";
        public static int pageViews = 0;
        public static int requestCount = 0;
        public static string pageDataTemplate =
            //"<!DOCTYPE>" +
            "<html>" +
            "  <head>" +
            "    <title>Regression Testing Example</title>" +
            "  </head>" +
            "  <body>" +
            "    <p>Page Views: {0}</p>" +
            "    <form method=\"post\" action=\"shutdown\">" +
            "      <input type=\"submit\" value=\"Shutdown\" {1}>" +
            "    </form>" +
            "  </body>" +
            "</html>";


        public void Start()
        {
            // Create a Http server and start listening for incoming connections
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine($"Listening for connections on {url}");

            // Handle requests
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            // Close the listener
            listener.Close();
        }

        public async Task HandleIncomingConnections()
        {
            bool runServer = true;

            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Block here until we hear an incoming connection
                HttpListenerContext ctx = await listener.GetContextAsync();

                // Peel out the requests and response objects
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                Console.Write(this.LogHttpRequest(req));

                // If `shutdown` url requested w/ POST, then shutdown the server after serving the page
                if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/shutdown"))
                {
                    Console.WriteLine("Shutdown requested");
                    runServer = false;
                }

                // TODO: refactor to incrementPageViews()
                // Make sure we don't increment the page views counter if `favicon.ico` is requested
                if (req.Url.AbsolutePath != "/favicon.ico")
                    pageViews += 1;

                // TODO: refactor to writeHttpResponse()
                // Write the response info
                string disableSubmit = !runServer ? "disabled" : "";
                byte[] data = Encoding.UTF8.GetBytes(String.Format(pageDataTemplate, pageViews, disableSubmit));
                resp.ContentType = "text/html";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;

                // Write out to the response stream (asynchronously), then close it
                await resp.OutputStream.WriteAsync(data, 0, data.Length);
                resp.Close();
            }
        }

        public string LogHttpRequest(HttpListenerRequest request)
        {
            var sb = new StringBuilder();

            // Print out some info about the request
            sb.AppendLine($"Request #: {++requestCount}");
            sb.AppendLine(request.Url.ToString());
            sb.AppendLine(request.HttpMethod);
            sb.AppendLine(request.UserHostName);
            sb.AppendLine(request.UserAgent);
            sb.AppendLine();

            return sb.ToString();
        }

        // Approach:
        //      Discuss regression test pyramid
        //      Discuss functional design of example project
        //      Create his git repo
        //      Create test folders and projects
        //      Implement tests using TDD with the TODO refactors, committing nicely as we go

        // TESTS:
        // ----------
        // Unit:
        //      - log message contains proper fields
        //      - response always has minimum properties set (ContentType, ContentEncoding, ContentLength)
        //      - favicon request does not increment page views
        // Integration:
        //      - logger: writes to file system when enabled
        //      - controller: increments page views in key-value-store database (text file on filesystem)
        //      - controller: overwrites shutdown time in key-value-store database (text file on filesystem)
        //      - webapi: default route returns valid response
        //      - webapi: /shutdown route shuts down http listener
        // E2E:
        //      - "Page Views" loads value from database on startup
        //      - "Page Views" increments with each browser / page refresh
        //      - "Shutdown" button is disabled after clicked
        //      - service is unavailable after shutdown is clicked
    }
}
