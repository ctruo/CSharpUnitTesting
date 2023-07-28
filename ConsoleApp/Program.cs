using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    //forked: https://gist.github.com/define-private-public/d05bc52dd0bed1c4699d49e2737e80e7
    public class RegressionTestExampleApp
    {
        public static void Main(string[] args)
        {
            var httpServer = new HttpServer();

            httpServer.Start();
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