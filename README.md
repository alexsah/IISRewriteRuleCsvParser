IIS Rewrite Rule CSV Parser
=========================

This app allows you to parse a CSV file to create rewrite rules.

See sample_url.csv for a sample csv file.

The format is:
source_url,redirect_url

###How to use:
Place url.csv in the same directory as the executable. Run executable and it will spit out the correct xml format for Rewrite 2.0. I set it up as an external file - however you can use it inside web.config.

Put the following in web.config inside System.WebServer:
```xml  
<rewrite>
    <rules configSource="RewriteRules.config" />
</rewrite>
```

###Links:
http://www.iis.net/downloads/microsoft/url-rewrite
