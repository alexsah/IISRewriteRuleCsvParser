using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewriteRuleCsvParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = @"url.csv";
            string outputFile = @"RewriteRules.config";

            if (!System.IO.File.Exists(inputFile))
            {
                Console.WriteLine("URL.csv not found - please place with executable and run again");
                Console.WriteLine("Press any key to exit.");
                System.Console.ReadKey(); 
                Environment.Exit(0);
            }

            string[] lines = System.IO.File.ReadAllLines(inputFile);

            //Start a new rule file
            System.IO.File.WriteAllText(outputFile, "<rules>\r\n    <clear />\r\n");

            foreach (string line in lines)
            {
                String[] values = line.Split(',');
                var originalUrl = values[0];
                var redirectUrl = values[1];
                var originalDomain = originalUrl.Substring(0, originalUrl.IndexOf('/', 8));
                var redirectName = "";

                if (originalUrl.StartsWith("http://"))
                    redirectName = originalUrl.Substring(7);
                else
                    redirectName = originalUrl;    
                
                var requestUri = originalUrl.Replace(originalDomain, "");
                if (originalDomain.StartsWith("http://"))
                    originalDomain = originalDomain.Substring(7);

                if (originalDomain.StartsWith("www"))
                    originalDomain = originalDomain.Substring(3);

                System.IO.File.AppendAllText(outputFile, "\r\n    <!-- " + originalUrl + " -->\r\n");
                System.IO.File.AppendAllText(outputFile, "    <rule name=\"" + redirectName + "\" enabled=\"true\" patternSyntax=\"Wildcard\" stopProcessing=\"true\">\r\n");
                System.IO.File.AppendAllText(outputFile, "        <match url=\"*\" />\r\n        <conditions logicalGrouping=\"MatchAll\" trackAllCaptures=\"false\">\r\n");
                System.IO.File.AppendAllText(outputFile, "            <add input=\"{HTTP_HOST}\" pattern=\"*" + originalDomain + "\" />\r\n");
                System.IO.File.AppendAllText(outputFile, "            <add input=\"{HTTP_URL}\" pattern=\"" + requestUri + "\" />\r\n        </conditions>\r\n");
                System.IO.File.AppendAllText(outputFile, "        <action type=\"Redirect\" url=\"" + redirectUrl + "\" appendQueryString=\"false\" />\r\n    </rule>\r\n");

            }

            System.IO.File.AppendAllText(outputFile, "</rules>");

            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();

        }
    }
}
