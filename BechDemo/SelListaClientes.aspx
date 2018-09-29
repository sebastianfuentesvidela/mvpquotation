<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelListaClientes.aspx.cs" Inherits="SelListaClientes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seleccionar Lista de Clientes</title>
    <script type="text/javascript">
        function SetCliente(point) {
            parent.SetCliente(point)
        }
    </script>
    <style type="text/css">
        body
        {
                background-color: #fcfcfc;
        }
        a:link
        {
            color: #808080;
            text-decoration: none;
        }
        a:visited
        {
            color: #808080;
            text-decoration: none;
        }
        a:hover
        {
            color: #FF0000;
            text-decoration: none;
        }
        a:active
        {
            color: #FFCC66;
        }
        .headmenutext
        {
            font-size: 11px;
            font-family: Verdana, Helvetica, Sans-Serif;
            text-decoration: none;
            font-weight: bold;
        }
        .itemlista
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10px;
            margin-bottom: 4px;
        }
    </style>
</head>
<body>
    <form id="formLisCli1" runat="server" enableviewstate="False">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="Número Cliente" HeaderStyle-CssClass="headmenutext">
                    <ItemTemplate>
                        <a class="itemlista" onclick="javascript:SetCliente('<%# 
                        Eval("nro_prsna") %>');"><%# Eval("RutCliente")%></a>
                    </ItemTemplate>

<HeaderStyle CssClass="headmenutext"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField
                    HeaderText="Nombre o Razón Social" HeaderStyle-CssClass="headmenutext">
                    <ItemTemplate>
                        <a class="itemlista" onclick="javascript:SetCliente('<%# 
                        Eval("nro_prsna") %>');"><%# Eval("nom_prsna_etcdo")%></a>
                    </ItemTemplate>

<HeaderStyle CssClass="headmenutext"></HeaderStyle>
                    </asp:TemplateField>
                <asp:BoundField DataField="gls_pais" ControlStyle-CssClass="itemlista"  
                    HeaderText="País" HeaderStyle-CssClass="headmenutext" >
                
<ControlStyle CssClass="itemlista"></ControlStyle>

<HeaderStyle CssClass="headmenutext"></HeaderStyle>
                </asp:BoundField>
                
            </Columns>
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
