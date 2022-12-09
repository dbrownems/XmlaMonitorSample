Sample for reading Trace Events from a Power BI Premium XMLA Endpoint

Power BI Premium XMLA endpoints don't support server-wide tracing, so you have to use a database-scoped trace.  AMO creates server traces, so won't work.  But using Adomd.net you can create and subscribe to the trace. 

This is acomplished by adding a "Catalog" property to your AdomdCommand, eg

```
var cmd = (AdomdCommand)con.CreateCommand();
cmd.Properties.Add(new AdomdProperty("Catalog", db));
```

To subscribe to the trace open an AdomdDataReader with the Subscribe command.  The DataReader will return trace events until the AdomdCommand is Canceled.

See generally [XMLA Tracing](https://learn.microsoft.com/en-us/analysis-services/multidimensional-models-scripting-language-assl-xmla/monitoring-traces-xmla?view=asallproducts-allversions)
