<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_Partial.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div>

    </div>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" Height="75%" ProcessingMode="Remote" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" style="height:900px; width:1100px;" ZoomMode="PageWidth" ShowParameterPrompts="False" ShowPromptAreaButton="False" >
            <ServerReport ReportPath='/USDA Reports/Families' ReportServerUrl="bsg-sqlserv/reportserver2012" />
        </rsweb:ReportViewer>
    </form>
</body>
</html>
