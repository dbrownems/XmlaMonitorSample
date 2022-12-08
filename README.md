Sample for reading Extended Events from a Power BI Premium XMLA Endpoint

Power BI Premium XMLA endpoints don't support server-wide tracing, so you have to use a database-scoped trace.  This is acomplished by adding a "Catalog" property to your AdomdCommand, eg

```
var cmd = (AdomdCommand)con.CreateCommand();
cmd.Properties.Add(new AdomdProperty("Catalog", db));
```

See generally [XMLA Tracing](https://learn.microsoft.com/en-us/analysis-services/multidimensional-models-scripting-language-assl-xmla/monitoring-traces-xmla?view=asallproducts-allversions)
