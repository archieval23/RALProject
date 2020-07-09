<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RALReport.aspx.cs" Inherits="RALProject.Web.Reports.RALReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="RALReport" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div style = "overflow: visible;" > 
                <rsweb:ReportViewer ID="RALListReportViewer" runat="server" Width="100%" Height="1000">
                </rsweb:ReportViewer>
            </ div >
        </div>
    </form>
</body>
</html>
