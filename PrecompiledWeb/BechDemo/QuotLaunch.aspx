<%@ page language="C#" autoeventwireup="true" inherits="QuotLaunch, App_Web_9_qslq2n" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Genera Cotización</title>
    <link href="Contents/jquery.datepick.css" rel="stylesheet" type="text/css" />
    <link href="Contents/quotlaunch.css" rel="stylesheet" type="text/css" />
    <link href="Contents/jquery.modal.min.css" rel="stylesheet" type="text/css" />

    <script src="Scripts/jquery-1.12.4.min.js" type="text/javascript"></script>

    <script src="Scripts/ajax.cdnjs.com.json2.js" type="text/javascript"></script>

    <script src="Scripts/jquery.numeric.min.js" type="text/javascript"></script>

    <script src="Scripts/jquery.plugin.min.js" type="text/javascript"></script>

    <script src="Scripts/jquery.datepick.min.js" type="text/javascript"></script>

    <script src="Scripts/jquery.datepick-es-AR.js" type="text/javascript"></script>

    <script src="Scripts/jquery.modal.min.js" type="text/javascript"></script>

    <script src="Scripts/jquery.tmpl.min.js" type="text/javascript"></script>

    <script src="Scripts/jquery.tmplPlus.min.js" type="text/javascript"></script>

    <script src="Scripts/Pages/quotlaunch.js" type="text/javascript"></script>
<style>
   #spinner
    {
        position: fixed;
        left: 0px;
        top: 0px;
        width: 100%;
        height: 100%;
        z-index: 9999;
        background: url(js/ajax-loader.gif) 50% 50% no-repeat #ede9df;
    }
</style>
<style>
#page_navigation a{
	padding:3px;
	border:1px solid gray;
	margin:2px;
	color:black;
	text-decoration:none
}
.active_page{
	background:darkblue;
	color:white !important;
}
</style>

</head>
<body>
    <form id="form1" runat="server" autocomplete="off" submitdisabledcontrols="True">
    <div class="simuliza" style="width: 100%; background-color: #999999; vertical-align: bottom;">
        <asp:ImageButton ID="ImageButton0" runat="server" ImageUrl="~/Contents/img/nada.gif"
            Height="1px" Width="1px" OnClientClick="return false;" />
        <asp:ImageButton ID="toolNueva" runat="server" ToolTip="Nueva Cotización" BorderColor="#999999"
            ImageUrl="~/Contents/img/nvoelem.gif" Height="20px" Width="62px" BackColor="White"
            BorderStyle="Outset" BorderWidth="1px" OnClientClick="StartFreshOne();" />
        <asp:ImageButton ID="toolBusca" runat="server" ToolTip="Buscar Cotización" AlternateText="Buscar Cotización"
            BorderColor="#999999" ImageUrl="~/Contents/img/busca.gif" Height="20px" Width="22px"
            BackColor="White" BorderStyle="Outset" OnClientClick="alert('BUSCAR no implementado'); return false;" BorderWidth="1px" />
        <asp:ImageButton ID="toolSimula" runat="server" ToolTip="Simular Cotización" AlternateText="Simular Cotización"
            BorderColor="#999966" ImageUrl="~/Contents/img/camben.gif" Height="20px" Width="31px"
            BackColor="White" BorderStyle="Outset" OnClientClick="alert('botón no implementado'); return false;" BorderWidth="1px" />
        <asp:ImageButton ID="toolCotiza" runat="server" ToolTip="Generar Cotización" AlternateText="Generar Cotización"
            BorderColor="#999966" ImageUrl="~/Contents/img/repara.gif" Height="20px" Width="27px"
            BackColor="White" BorderStyle="Outset" OnClientClick="alert('botón no implementado'); return false;" BorderWidth="1px" />
        <asp:ImageButton ID="toolCursa" runat="server" ToolTip="Cursar Cotización" AlternateText="Cursar Cotización"
            BorderColor="#999966" ImageUrl="~/Contents/img/recibir.jpg" Height="20px" Width="24px"
            BackColor="White" BorderStyle="Outset" OnClientClick="alert('botón no implementado'); return false;" BorderWidth="1px" />
        <asp:ImageButton ID="toolAnula" runat="server" ToolTip="Anular Cotización" AlternateText="Anular  Cotización"
            BorderColor="#999966" ImageUrl="~/Contents/img/kill.gif" Height="20px" Width="24px"
            BackColor="White" BorderStyle="Outset" OnClientClick="alert('botón no implementado'); return false;" BorderWidth="1px" />
        <asp:ImageButton ID="toolImprime" runat="server" ToolTip="Imprimir Cotización" AlternateText="Imprimir Cotización"
            BorderColor="#999966" ImageUrl="~/Contents/img/imprime.gif" Height="20px" Width="24px"
            BackColor="White" BorderStyle="Outset" OnClientClick="alert('botón no implementado'); return false;" BorderWidth="1px"
             /><asp:HiddenField ID="hidcommand"
                runat="server" OnValueChanged="hidcommand_ValueChanged" />
        <asp:HiddenField ID="hidargument" runat="server" />
        &nbsp;<asp:HiddenField ID="pageIdd" runat="server"></asp:HiddenField></div>
    <div id="Cotización">
        <label class="titulopanel">
            Cotización</label>
        <br />
        <table runat="server" align="center" frame="box" width="90%">
            <tr>
                <td>
                    Número<br />
                    <asp:TextBox ID="lblNumeroCotizacion" runat="server" Width="92px" BackColor="#FFFFCC"
                        CssClass="centrado" ReadOnly="True">(Nueva)</asp:TextBox>
                </td>
                <td class="simuliza">
                    Fecha Cotizada
                    <br />
                    <asp:TextBox ID="sprFechaCotizacion" class="cotfechas" runat="server" Width="83px"
                        BackColor="White"></asp:TextBox>
                </td>
                <td class="simuliza" nowrap="nowrap">
                    Vigencia<br />
                    <asp:TextBox ID="sprVigencia" CssClass="positive-integer desimuliza" runat="server"
                        Width="50px" BackColor="White" MaxLength="2">7</asp:TextBox>
                    Días
                </td>
                <td>
                    Fecha Curse
                    <br />
                    <asp:TextBox ID="sprFechaCurse" class="cotfechas" runat="server" Width="87px" ReadOnly="True"
                        BackColor="White" Enabled="False"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="botCursar" runat="server" Text="Cursar" Enabled="False" Width="59px"
                        Height="30px" />
                </td>
                <td>
                    Estado<br />
                    <asp:TextBox ID="lblEstado" runat="server" Width="110px" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    Fecha Ultimo Estado
                    <br />
                    <asp:TextBox ID="sprFechaEstado" runat="server" Width="92px" BackColor="#FFFFCC"
                        ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <label class="titulopanel">
            Cliente</label>
        <br />
        <table id="Cliente" class="simuliza" align="center" frame="box" width="90%">
            <tr>
                <td nowrap="nowrap">
                    Número RUT<br />
                    <asp:TextBox ID="txtRutCliente" runat="server" Width="126px" BackColor="#FFFFCC"
                        ReadOnly="True"></asp:TextBox>
                    <input type="button" style="width: 65px;" id="cmdBuscarCliente" name="cmdBuscarCliente"
                        value="Buscar" />
                </td>
                <td colspan="4" nowrap="nowrap">
                    Nombre Cliente&nbsp;<asp:Label ID="lbSinLineaCliente" runat="server" CssClass="rojo"
                        Text=""></asp:Label><br />
                    <asp:TextBox ID="txtNombreCliente" runat="server" Width="490px" BackColor="#FFFFCC"
                        ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    País<br />
                    <asp:TextBox ID="lblPais" runat="server" Width="163px" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    Categoría<br />
                    <asp:TextBox ID="lblCategoria" runat="server" BackColor="#FFFFCC" ReadOnly="True"
                        Width="124px"></asp:TextBox>
                </td>
                <td colspan="3">
                    Casa Matriz<br />
                    <asp:TextBox ID="txCasaMatriz" runat="server" Width="350px" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Swift<br />
                    <asp:TextBox ID="txSwift" runat="server" Width="126px" BackColor="White"></asp:TextBox>
                </td>
                <td>
                    Operación Relacionada<br />
                    <asp:TextBox ID="txNroOperacion" runat="server" Width="126px" BackColor="White"></asp:TextBox>
                </td>
                <td nowrap="nowrap">
                    Alcance<br />
                    <asp:CheckBox ID="chkOperPuntual" runat="server" Text="Operación Puntual" />
                </td>
                <td nowrap="nowrap">
                    Especificación<br />
                    <asp:CheckBox ID="chkNota" runat="server" Text="Doble Imputación" />
                </td>
                <td>
                    Casa Matriz<br />
                    <asp:CheckBox ID="chkCargoCasaMatriz" runat="server" Text="Existe" />
                </td>
            </tr>
        </table>
    </div>
    <div id="Antecedentes">
        <label class="titulopanel">
            Antecedentes Básicos</label>
        <br />
        <table class="simuliza" align="center" frame="box" width="90%">
            <tr>
                <td colspan="2">
                    Tipo de Operación<br />
                    <asp:DropDownList ID="cboTipoOperacion" runat="server" Width="95%" CssClass="sangria"
                        BackColor="White" AutoPostBack="True" OnSelectedIndexChanged="cboTipoOperacion_SelectedIndexChanged">
                        <asp:ListItem Value="A151">A151 - Crisalidas de Pescado a Plazo de Transporte velero </asp:ListItem>
                        <asp:ListItem Value="D41">D41 - Otro Tipo de Operacion</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    Producto<br />
                    <asp:DropDownList ID="cboProducto" runat="server" Width="95%" CssClass="sangria"
                        BackColor="White">
                        <asp:ListItem Value="1151">Producto del Tipo Uno -----------------A151 - Crisalidas de Pescado a Plazo de Transporte velero </asp:ListItem>
                        <asp:ListItem Value="1141">Producto del Tipo Dos -----------------D41 - Otro Tipo de Operacion</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Familia Producto<br />
                    <asp:TextBox ID="lblCodigoTipoOperacion" runat="server" Width="105px" CssClass="sangria centrado"
                        BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    Tipo de Crédito / Inversión<br />
                    <asp:DropDownList ID="cboTipoCredito" runat="server" CssClass="sangria"
                        BackColor="White">
                        <asp:ListItem Value="1151">Crédito tipo uno ----------------- </asp:ListItem>
                        <asp:ListItem Value="1141">Crédito tipo del Tipo Dos ------------</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table id="GeneraDocs" align="center" frame="box" width="90%">
            <caption style="text-align: center; font-size: larger;">
                Generar Documentos Asociados</caption>
            <tr>
                <td>
                </td>
                <td>
                    En Pantalla
                    <br />
                    <a id="restricc" href="javascript:InformeEnIframe('restricciones');">Restricciones al
                        País y al Cliente</a>
                </td>
                <td>
                    En Sub Ventana
                    <br />
                    <a id="infocapitulo" href="javascript:SetInforme3b5();">Estado actual de los márgenes
                        del 30% y 70% del Patrimonio Efectivo (Capítulo III.B.5)</a>
                </td>
                <td>
                    Directo a la Impresora
                    <br />
                    <a id="infocapitulob" href="javascript:printDetails();">Clasificaciones de Riesgo País y Cliente</a>
                </td>
                <td>
                    En Sub Ventana
                    <br />
                    <a id="estadosimulacion" href="javascript:showDetails();">Estado actual de Márgenes
                        y Límites - Simulación (Imprimible)</a>
                </td>
                <td>
                    En Pantalla
                    <br />
                    <a id="verestadosimulacion" href="javascript:MostrarSimulacion();">Volver a Ver la Simulación</a>
                </td>
                <td>
                    <asp:Button ID="botCotizar" Enabled="False" OnClientClick="alert('hasta aqui queda esta demo...');" runat="server" Text="Cotizar"
                        Width="65px" Height="30px" />
                </td>
            </tr>
        </table>
        <table class="simuliza" align="center" frame="box" width="90%">
            <caption style="text-align: center; font-size: larger;">
                Simulación</caption>
            <tr>
                <td style="border: solid 1px grey;" colspan="2">
                    <table align="center">
                        <caption style="text-align: center; font-size: larger;">
                            Monto Operación</caption>
                        <tr>
                            <td colspan="2">
                                Moneda Operación<br />
                                <asp:HiddenField ID="parMoneda" runat="server" Value="1" />
                                <asp:DropDownList ID="cboMoneda" runat="server" CssClass="sangria"
                                    BackColor="White">
                                    <asp:ListItem Value="13">DOLAR AMERICANO</asp:ListItem>
                                    <asp:ListItem Value="999">PESOS CHILENOS</asp:ListItem>
                                    <asp:ListItem Value="998">UNIDADES DE FOMENTO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Monto<br />
                                <asp:TextBox ID="sprMontoOperacion" runat="server" Width="150px" BackColor="White"
                                    MaxLength="15" CssClass="numero numerico2d"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="sprMontoOperacion"
                                    ValidationExpression="^[0-9]{0,12}(\.\d\d)?$|^[0-9]{0,12}(\.\d)?$|^[0-9]{0,12}(\.)?$|^[0-9]{0,12}?$"
                                    Display="Dynamic" ErrorMessage="&lt;-12x.xx!" EnableClientScript="True" runat="server" />
                            </td>
                            <td>
                                Porcentaje Tolerancia<br />
                                <asp:TextBox ID="sprPorcentajeTolerancia" runat="server" Width="80px" CssClass="numero numerico2d"
                                    MaxLength="5" BackColor="White"></asp:TextBox>
                                <span class="titulopanel">%</span>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="sprPorcentajeTolerancia"
                                    ValidationExpression="^[0-9]{0,2}(\.\d\d)?$|^[0-9]{0,2}(\.\d)?$|^[0-9]{0,2}(\.)?$|^[0-9]{0,2}?$"
                                    Display="Dynamic" ErrorMessage="&lt;-xx.xx!" EnableClientScript="True" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="border: solid 1px grey;">
                    <table align="center">
                        <caption style="text-align: center; font-size: larger;">
                            Plazos Operación</caption>
                        <tr>
                            <td>
                                Plazo Total (Curse)<br />
                                <asp:TextBox ID="sprPlazoMaxResidualCtg" runat="server" Width="79px" CssClass="numero numerico0d"
                                    MaxLength="4" BackColor="White"></asp:TextBox><label id="rDias">Días</label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="sprPlazoMaxResidualCtg"
                                    ValidationExpression="^[0-9]{0,4}?$" Display="Dynamic" ErrorMessage="&lt;-entero!"
                                    EnableClientScript="True" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Vencimiento Estimado
                                <br />
                                <asp:TextBox ID="sprFechaVctoEstimada" class="simuliza" runat="server" Width="101px"
                                    BackColor="White"></asp:TextBox>
                                <asp:Label ID="lblDiaFechaVctoEstimada" runat="server" CssClass="titulopanel sangria"
                                    Text="Domingo"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="border: solid 1px grey;">
                <td style="border: solid 1px grey;">
                    Monto Afecto a Linea<br />
                    <asp:TextBox ID="sprMonto" runat="server" Width="185px" Height="26px" CssClass="numero centrado"
                        ReadOnly="True" BackColor="#FFFFCC"></asp:TextBox>
                </td>
                <td style="border: solid 1px grey;" nowrap="nowrap">
                    Monto Equivalente<br />
                    <label class="titulopanel">
                        US$</label><asp:TextBox ID="sprMontoEquivalente" runat="server" Width="146px" Height="26px"
                        ReadOnly="True"    BackColor="#FFFFCC" CssClass="numero centrado"></asp:TextBox>
                </td>
                <td align="center">
                    &nbsp;&nbsp;&nbsp;<asp:Button ID="cmdSimular" runat="server" Text="Simular" Height="27px"
                        Width="183px" OnClick="cmdSimular_Click" OnClientClick="if (AceptaSimular() == 1) { return true; } else {return false;}" />
                </td>
            </tr>
        </table>
    </div>
    <div id="DeudorIndirecto" class="simuliza">
        <label class="titulopanel">
            Deudor Indirecto / Garante</label>
        <br />
        <table align="center" frame="box" width="90%">
            <tr>
                <td nowrap="nowrap">
                    Número RUT<br />
                    <asp:TextBox ID="txNroPerSuby" runat="server" Width="126px" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
                    <input type="button" style="width: 57px;" id="btBuscSuby" name="btBuscSuby" value="Buscar" />
                </td>
                <td colspan="3" nowrap="nowrap">
                    Nombre Deudor<asp:Label ID="lbSinLineaSuby" runat="server" CssClass="rojo" Text=""></asp:Label><br />
                    <asp:TextBox ID="txNomSuby" runat="server" Width="420px" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    País<br />
                    <asp:TextBox ID="lblPaisSuby" runat="server" Width="163px" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    Categoría<br />
                    <asp:TextBox ID="lblCategSuby" runat="server" BackColor="#FFFFCC" ReadOnly="True"
                        Width="177px"></asp:TextBox>
                </td>
                <td>
                    Garantía<br />
                    <asp:TextBox ID="sprPorcenGarantia" runat="server" Width="57px" CssClass="numerico2d"
                        BackColor="White">100.00</asp:TextBox>
                    <span class="titulopanel">%</span>
                </td>
                <td>
                    Monto Garantizado<br />
                    <label class="titulopanel">
                        US$</label><asp:TextBox ID="sprMontoGarantia" runat="server" Width="126px" BackColor="#FFFFCC" CssClass="centrado"
                            ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    Tipo de Operación<br />
                    <asp:DropDownList ID="cboTipoOperSuby" runat="server" Width="95%" CssClass="sangria"
                        BackColor="White" AutoPostBack="True" OnSelectedIndexChanged="cboTipoOperSuby_SelectedIndexChanged">
                        <asp:ListItem Value="A151">A151 - Crisalidas de Pescado a Plazo de Transporte velero </asp:ListItem>
                        <asp:ListItem Value="D41">D41 - Otro Tipo de Operacion</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    Producto<br />
                    <asp:DropDownList ID="cboProdSuby" runat="server" Width="95%" CssClass="sangria"
                        BackColor="White">
                        <asp:ListItem Value="1151">Producto del Tipo Uno -----------------A151 - Crisalidas de Pescado a Plazo de Transporte velero </asp:ListItem>
                        <asp:ListItem Value="1141">Producto del Tipo Dos -----------------D41 - Otro Tipo de Operacion</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div id="OtrosDatos">
        <label class="titulopanel">
            Otros Datos</label>
        <br />
        <table class="simuliza" align="center" frame="box" width="90%">
            <tr>
                <td colspan="2" class="style1">
                    <table align="center">
                        <caption style="text-align: center; font-size: larger;">
                            Comisiones</caption>
                        <tr>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="cboMoneComision" runat="server" CssClass="sangria" 
                                    BackColor="White">
                                    <asp:ListItem Value="13">Dolar Americano</asp:ListItem>
                                    <asp:ListItem Value="999">Peso Chileno</asp:ListItem>
                                    <asp:ListItem Value="998">Unidad de Fomento</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="sprMontoComisiones" class="numerico2d" MaxLength="15" runat="server"
                                    Width="129px" BackColor="White"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="sprMontoComisiones"
                                    ValidationExpression="^[0-9]{0,12}(\.\d\d)?$|^[0-9]{0,12}(\.\d)?$|^[0-9]{0,12}(\.)?$|^[0-9]{0,12}?$"
                                    Display="Dynamic" ErrorMessage="&lt;-12x.xx!" EnableClientScript="True" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td colspan="2">
                    <table align="center">
                        <caption nowrap="nowrap" style="text-align: center; font-size: larger;">
                            Gastos Por Cuenta de</caption>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="optGastosPorCuenta" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Ordenante</asp:ListItem>
                                    <asp:ListItem Value="2">Beneficiario</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="simuliza" align="center" frame="box" width="90%">
            <tr>
                <td colspan="2" class="style1">
                    <table align="center">
                        <caption style="text-align: center; font-size: larger;">
                            Pago Anticipado</caption>
                        <tr>
                            <td colspan="3" nowrap="nowrap">
                                Opciones de Base<br />
                                <asp:TextBox ID="txtTasaPago" runat="server" Width="274px" BackColor="#FFFFCC" CssClass="centrado"
                                    ReadOnly="True"></asp:TextBox>
                                <input type="button" style="width: 57px;" onclick="alert('funcionalidad en construcción')"
                                    id="cmdBuscarTasaPago" name="cmdBuscarTasaPago" value="Buscar" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">
                                Tasa Base<br />
                                <asp:TextBox ID="sprValorTasaPago" runat="server" Width="79px" CssClass="numero numerico4d"
                                    MaxLength="7" BackColor="White"></asp:TextBox>%
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="sprValorTasaPago"
                                    ValidationExpression="^[0-9]{0,2}(\.\d\d\d\d)?$|^[0-9]{0,2}(\.\d\d\d)?$|^[0-9]{0,2}(\.\d\d)?$|^[0-9]{0,2}(\.\d)?$|^[0-9]{0,2}(\.)?$|^[0-9]{0,2}?$"
                                    Display="Dynamic" ErrorMessage="&lt;-xx.xxxx!" EnableClientScript="True" runat="server" />
                            </td>
                            <td nowrap="nowrap">
                                Spread s/Base<br />
                                <span class="titulopanel">+</span>&nbsp;<asp:TextBox ID="sprValorSpreadPago" runat="server"
                                    MaxLength="7" CssClass="numero numerico4d" Width="81px" BackColor="White"></asp:TextBox>%
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="sprValorSpreadPago"
                                    ValidationExpression="^[0-9]{0,2}(\.\d\d\d\d)?$|^[0-9]{0,2}(\.\d\d\d)?$|^[0-9]{0,2}(\.\d\d)?$|^[0-9]{0,2}(\.\d)?$|^[0-9]{0,2}(\.)?$|^[0-9]{0,2}?$"
                                    Display="Dynamic" ErrorMessage="&lt;-xx.xxxx!" EnableClientScript="True" runat="server" />
                            </td>
                            <td colspan="2" nowrap="nowrap">
                                Tasa Descuento<br />
                                <span class="titulopanel">=</span>&nbsp;<asp:TextBox ID="sprTasaTotalPago" CssClass="numero"
                                    runat="server" Width="81px" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>%
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table align="center">
                        <caption style="text-align: center; font-size: larger;">
                            Préstamo</caption>
                        <tr>
                            <td colspan="3" nowrap="nowrap">
                                Opciones de Base<br />
                                <asp:TextBox ID="txtTasaPtmo" runat="server" Width="274px" CssClass="centrado" BackColor="#FFFFCC"
                                    ReadOnly="True"></asp:TextBox>
                                <input type="button" style="width: 57px;" onclick="alert('funcionalidad en construcción')"
                                    id="cmdBuscTasaPtmo" name="cmdBuscTasaPtmo" value="Buscar" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">
                                Tasa Base<br />
                                <asp:TextBox ID="sprValorTasaPtmo" runat="server" Width="81px" CssClass="numero numerico4d"
                                    MaxLength="7" BackColor="White"></asp:TextBox>%
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="sprValorTasaPtmo"
                                    ValidationExpression="^[0-9]{0,2}(\.\d\d\d\d)?$|^[0-9]{0,2}(\.\d\d\d)?$|^[0-9]{0,2}(\.\d\d)?$|^[0-9]{0,2}(\.\d)?$|^[0-9]{0,2}(\.)?$|^[0-9]{0,2}?$"
                                    Display="Dynamic" ErrorMessage="&lt;-xx.xxxx!" EnableClientScript="True" runat="server" />
                            </td>
                            <td nowrap="nowrap">
                                Spread s/Base<br />
                                <span class="titulopanel">+</span>&nbsp;<asp:TextBox ID="sprValorSpreadPtmo" runat="server"
                                    CssClass="numero numerico4d" MaxLength="7" Width="81px" BackColor="White"></asp:TextBox>%
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="sprValorSpreadPtmo"
                                    ValidationExpression="^[0-9]{0,2}(\.\d\d\d\d)?$|^[0-9]{0,2}(\.\d\d\d)?$|^[0-9]{0,2}(\.\d\d)?$|^[0-9]{0,2}(\.\d)?$|^[0-9]{0,2}(\.)?$|^[0-9]{0,2}?$"
                                    Display="Dynamic" ErrorMessage="&lt;-xx.xxxx!" EnableClientScript="True" runat="server" />
                            </td>
                            <td colspan="2" nowrap="nowrap">
                                Tasa Crédito<br />
                                <span class="titulopanel">=</span>&nbsp;<asp:TextBox ID="sprTasaTotalPtmo" runat="server"
                                    CssClass="numero" Width="81px" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>%
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="OtrosAntecedentes">
        <label class="titulopanel">
            Otros Antecedentes</label>
        <br />
        <table align="center" frame="box" width="90%">
            <tr>
                <td>
                    Ordenante
                </td>
                <td>
                    <asp:TextBox ID="txtNombreImportador" runat="server" Width="249px" BackColor="White"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Beneficiario
                </td>
                <td>
                    <asp:TextBox ID="txtNombreExportador" runat="server" Width="247px" BackColor="White"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Mercaderia
                </td>
                <td>
                    <asp:TextBox ID="txtMercaderia" runat="server" Width="245px" BackColor="White"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap">
                    Vencimiento contra Garantía /RU525
                </td>
                <td>
                    <asp:TextBox ID="sprFechaContraGarantia" runat="server" Width="82px" BackColor="White"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Reembolso
                </td>
                <td>
                    <asp:DropDownList ID="cboReembolso" runat="server" CssClass="sangria" BackColor="White">
                        <asp:ListItem Value="1">Sin autorizacion de reembolso</asp:ListItem>
                        <asp:ListItem Value="2">Con Autorización Trecer Banco</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Observaciones
                </td>
                <td>
                    <asp:TextBox ID="txtObservaciones" runat="server" Width="479px" CssClass="sangria"
                        Rows="10" TextMode="MultiLine" BackColor="White"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div  style="display: none;" id="pageMessage" runat="server">
    </div>
    </form>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <div id="forCliente" style="display: none;" class="modal">
        <p>
            &nbsp;<span id="rotulocliente" class="titulopanel">Rotulo</span></p>
        <div id="lapapaya">
            <hr />
            <iframe id="marcoCliente" width="100%" height="400" scrolling="yes" frameborder="0"
                style="background-color: Transparent;"></iframe>
        </div>
    </div>
    <div id="listaClientes" style="display: none; overflow: visible; max-width: 90%; " class="modal">
        <p>
            &nbsp;<span id="rotulocli" class="titulopanel">Rotulo</span></p>
            <hr />
            	<table cellspacing="0" rules="all" border="1" width="100%">
		<tr>
			<th class="headmenutext" scope="col">Número Cliente</th>
			<th class="headmenutext" scope="col">Nombre o Razón Social</th>
			<th class="headmenutext" scope="col">Pa&#237;s</th>
		</tr>
        <tbody id="marcoCli">
                </tbody>
                </table>
             	<div style="float: left;">filtra:<input type="text" 
             	    id="pronomcli" 
             	    onkeypress="   var key; if(window.event) key = window.event.keyCode; else key = e.which; if (key == 13) {go_to_page(1)}" 
             	    style="width: 60px;" size="20" /><input type="button" value="ir" onclick="go_to_page(1);" /></div>
             	<div style="float: right;" id='page_navigation'></div>
        	<input type='hidden' id='current_page' />
        	<input type='hidden' id='show_per_page' value="20" />

    </div>
    <div id="forReserva" style="display: none;  max-width: 70%; nomax-height: 70%; " class="modal">
        <p>
            &nbsp;<span id="Span1" class="titulopanel">Cotizaciones 
                Asociadas al Límite</span></p>
           <hr />
        <div id="listacotiz">
        </div>
    </div>
    <div id="infoPopup" style="display: none; max-width: 90%;" class="modal">
        <p>
            &nbsp;<span id="rotuloinf" class="titulopanel">Rotulo</span><br />
            <div style="text-align: right">
                <a href="javascript:PrintFrame('iframeinforme')">Imprime</a><input type="image" src="Contents/img/print.gif"
                    onclick="PrintFrame('iframeinforme')" /></div>
        </p>
        <hr />
        <div id="contenediv">
            <iframe id="iframeinforme" name="iframeinforme" src="about:blank" width="100%" height="400"
                scrolling="yes" frameborder="0" style="background-color: Transparent;"></iframe>
        </div>
    </div>
    <div id="div_remoto">
    </div>
    <div style="display: none;">
        <img id="calImg" src="Contents/img/CalendarioUp.jpg" alt="Popup" class="trigger" />
    </div>
    <div id="simuliz" style="display: none; overflow: visible; max-width: 90%; nomin-height: 90%; ">
        <p>
            Para <a href="#" rel="modal:close">Cerrar</a> o presione ESC</p>
        <div id="resultado"  style="height: 100%;">
        </div>
    </div>
</body>
</html>
