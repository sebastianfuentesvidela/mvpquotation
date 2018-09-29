<%@ page language="C#" autoeventwireup="true" inherits="ShowInfo, App_Web_9_qslq2n" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Informe</title>
</head>
<body>
    <form id="form1" runat="server" enableviewstate="True">
    <asp:Label ID="rotuloReporte" 
            runat="server" Text="Titulin del Reportin"></asp:Label>
    <rsweb:ReportViewer ID="ReportViewer1"  runat="server" Width="100%" 
        BackColor="#FFFBF7" ZoomMode="PageWidth" ShowRefreshButton="false"
                        BorderWidth="0px" ShowFindControls="False" Height="650"
                         ShowPromptAreaButton="False" 
        ShowPageNavigationControls="False"  >
    </rsweb:ReportViewer>
    </form>
</body>
</html>
