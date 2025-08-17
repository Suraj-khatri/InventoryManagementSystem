<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ReportViewerWebForm.aspx.cs" Inherits="ReportViewerForMvc.ReportViewerWebForm" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="margin: 0px; padding: 0px;">
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                <Scripts>
                    <asp:ScriptReference Assembly="ReportViewerForMvc" Name="ReportViewerForMvc.Scripts.PostMessage.js" />
                </Scripts>
            </asp:ScriptManager>
            <div class="box" runat="server" id="ReportBox" style="overflow: hidden">
                <div class="box-body" style="overflow: auto">
                    <rsweb:ReportViewer SizeToReportContent="true" ID="ReportViewer1" Width="100%" EnableTelemetry="False" AsyncRendering="False" ViewStateMode="Enabled" ShowToolBar="true" runat="server" ShowPrintButton="false"></rsweb:ReportViewer>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
