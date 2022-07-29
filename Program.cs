using Microsoft.AnalysisServices;
using Microsoft.AnalysisServices.AdomdClient;
using System.Xml;
using System.Xml.Linq;

var db = "AdventureWorksCustomerActivityReport";
var xmlaEndpoint = "powerbi://api.powerbi.com/v1.0/myorg/ReportExportTesting";
var constr = $"Data Source={xmlaEndpoint};Initial Catalog={db}";
using var svr = new Microsoft.AnalysisServices.Server();

svr.ServerProperties.Add("Catalog", db);
svr.Connect(constr);
svr.Refresh();


using var con = new AdomdConnection();
con.ConnectionString = constr;
con.SessionID = svr.SessionID;
con.Open();

var cmd = (AdomdCommand)con.CreateCommand();
cmd.Properties.Add(new AdomdProperty("Catalog", db));

var traceId = "MyTrace";

cmd.CommandText = $"""
<Delete xmlns="http://schemas.microsoft.com/analysisservices/2003/engine" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">	
    <Object>		
        <TraceID>{traceId}</TraceID>	
    </Object>
</Delete>
""";

try
{
    cmd.ExecuteNonQuery();
}
catch (Exception ex) when (ex.Message.Contains("already exists"))
{
    Console.Write(ex.Message);
}

cmd.CommandText = $"""
                <Create xmlns="http://schemas.microsoft.com/analysisservices/2003/engine">
                <ObjectDefinition>
                    <Trace>
                        <ID>{traceId}</ID>
                        <Name>{traceId}</Name>
                        <Events>
                            <Event>
                                <EventID>9</EventID>
                                <Columns>
                                    <ColumnID>32</ColumnID>
                                    <ColumnID>1</ColumnID>
                                    <ColumnID>25</ColumnID>
                                    <ColumnID>33</ColumnID>
                                    <ColumnID>2</ColumnID>
                                    <ColumnID>3</ColumnID>
                                    <ColumnID>28</ColumnID>
                                    <ColumnID>36</ColumnID>
                                    <ColumnID>37</ColumnID>
                                    <ColumnID>39</ColumnID>
                                    <ColumnID>40</ColumnID>
                                    <ColumnID>41</ColumnID>
                                    <ColumnID>42</ColumnID>
                                    <ColumnID>43</ColumnID>
                                    <ColumnID>44</ColumnID>
                                    <ColumnID>45</ColumnID>
                                    <ColumnID>46</ColumnID>
                                    <ColumnID>47</ColumnID>
                                    <ColumnID>51</ColumnID>
                                    <ColumnID>52</ColumnID>
                                    <ColumnID>54</ColumnID>
                                    <ColumnID>55</ColumnID>
                                </Columns>
                            </Event>
                            <Event>
                                <EventID>10</EventID>
                                <Columns>
                                    <ColumnID>1</ColumnID>
                                    <ColumnID>2</ColumnID>
                                    <ColumnID>10</ColumnID>
                                    <ColumnID>3</ColumnID>
                                    <ColumnID>4</ColumnID>
                                    <ColumnID>5</ColumnID>
                                    <ColumnID>6</ColumnID>
                                    <ColumnID>22</ColumnID>
                                    <ColumnID>23</ColumnID>
                                    <ColumnID>24</ColumnID>
                                    <ColumnID>25</ColumnID>
                                    <ColumnID>28</ColumnID>
                                    <ColumnID>32</ColumnID>
                                    <ColumnID>33</ColumnID>
                                    <ColumnID>36</ColumnID>
                                    <ColumnID>37</ColumnID>
                                    <ColumnID>39</ColumnID>
                                    <ColumnID>40</ColumnID>
                                    <ColumnID>41</ColumnID>
                                    <ColumnID>42</ColumnID>
                                    <ColumnID>43</ColumnID>
                                    <ColumnID>46</ColumnID>
                                    <ColumnID>47</ColumnID>
                                    <ColumnID>49</ColumnID>
                                    <ColumnID>51</ColumnID>
                                    <ColumnID>52</ColumnID>
                                    <ColumnID>54</ColumnID>
                                    <ColumnID>55</ColumnID>
                                </Columns>
                            </Event>
                        </Events>
                        <Filter>
                            <And>
                                <GreaterOrEqual>
                                    <ColumnID>5</ColumnID>
                                    <Value>1</Value>
                                </GreaterOrEqual>
                                <NotLike>
                                    <ColumnID>37</ColumnID>
                                    <Value>SQL Server Profiler - c4ddfe44-9b50-4036-bef0-b3a995213de3</Value>
                                </NotLike>
                            </And>
                        </Filter>
                    </Trace>
                </ObjectDefinition>
            </Create>
    """;

try 
{
    cmd.ExecuteNonQuery();
}
catch (Exception ex) when (ex.Message.Contains("already exists"))
{
    Console.Write(ex.Message);
}

var subscribeCmd = $"""
    <Subscribe xmlns="http://schemas.microsoft.com/analysisservices/2003/engine"
        xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
        <Object>
            <TraceID>{traceId}</TraceID>
        </Object>
    </Subscribe>
    """;

cmd.CommandText = subscribeCmd;

Task.Run(() =>
{
    Console.WriteLine("Hit any key to cancel");
    Console.ReadKey();
    cmd.Cancel();
   
});
using (AdomdDataReader rdr = cmd.ExecuteReader())
{
    //var vals = new object[rdr.FieldCount];
    //for (int i = 0; i < rdr.FieldCount; i++)
    //{
    //    Console.Write(rdr.GetName(i));
    //    Console.Write("\t");
    //}
    //Console.WriteLine("");

    while (rdr.Read())
    {
        //EventClass      EventSubclass   CurrentTime     StartTime       EndTime Duration        CPUTime 
        Console.WriteLine($"{rdr["EventClass"]} {rdr["EventSubclass"]} {rdr["StartTime"]} {rdr["EndTime"]}");
        
    }
}

return;



