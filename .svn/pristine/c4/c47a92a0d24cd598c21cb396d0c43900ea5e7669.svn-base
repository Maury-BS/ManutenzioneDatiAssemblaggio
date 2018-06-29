using System;
using System.Data;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using Microsoft.Dynamics.BusinessConnectorNet;

public class Enums
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static string getEnum(string enumType, int enumVal)
    {
        Axapta axp;
        axp = new Axapta();
        try
        {
            string sStatus = "";
            axp.Logon("MET", "IT", "AXDEV", "");
            string s = axp.CallStaticClassMethod("NPOSysUtils", "getEnumStr",null).ToString();
            return s;
        }
        catch
        {
            return string.Empty;
        }
    }	
}
