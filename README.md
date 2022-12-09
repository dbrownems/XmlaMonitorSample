Sample for reading Trace Events from a Power BI Premium XMLA Endpoint using raw XMLA and Adomd.net.

Tracing is also available using AMO and the [Microsoft.AnalysisServices.Tabular.Trace](https://learn.microsoft.com/en-us/dotnet/api/microsoft.analysisservices.tabular.trace?view=analysisservices-dotnet), and a sample tracing utility that uses it here: [Rui Romano's PbiTracer](https://github.com/RuiRomano/pbitracer)

To Trace in Power BI you must create a database-scoped trace, and this is acomplished by adding a "Catalog" property to your AdomdCommand, eg

```
var cmd = (AdomdCommand)con.CreateCommand();
cmd.Properties.Add(new AdomdProperty("Catalog", db));
```

To subscribe to the trace open an AdomdDataReader with the Subscribe command.  The DataReader will return trace events until the AdomdCommand is Canceled.

See generally [XMLA Tracing](https://learn.microsoft.com/en-us/analysis-services/multidimensional-models-scripting-language-assl-xmla/monitoring-traces-xmla?view=asallproducts-allversions)
