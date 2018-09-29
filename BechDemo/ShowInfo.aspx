<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowInfo.aspx.cs" Inherits="ShowInfo" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Informe</title>
</head>
<body>
    <form id="formInfo" runat="server" enableviewstate="True">
    <asp:Label ID="rotuloReporte" 
            runat="server" Text="Titulin del Reportin"></asp:Label><br />
    <rsweb:ReportViewer ID="ReportViewer1"  runat="server" Width="100%" 
        BackColor="#FFFBF7" ShowPrintButton="True"
                        BorderWidth="0px" Height="520" 
        ShowPageNavigationControls="False" DocumentMapWidth="100%" 
        ShowDocumentMapButton="False" 
        ShowRefreshButton="False" AsyncRendering="True" EnableTheming="False" 
        ShowCredentialPrompts="False" ShowFindControls="False" 
        ShowParameterPrompts="False" ShowPromptAreaButton="False" 
        ShowZoomControl="False"  >
    </rsweb:ReportViewer>
    </form>
</body>
</html>
