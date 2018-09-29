using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Vistas;
using Presenter;

public partial class QuotLaunch : System.Web.UI.Page, IQuotLaunch
{
    private PresenterQuotLaunch objPresenter;

    protected void Page_Load(object sender, EventArgs e)
    {


        String cleDBstring = System.Configuration.ConfigurationManager.
                ConnectionStrings["BaseDatosCle"].ConnectionString;
        objPresenter = new PresenterQuotLaunch(cleDBstring);
        
        objPresenter.add(this);
        ///Si Hay Objecion de Puntero a Contenido no iniciar.
        if (!objPresenter.IniciaEdicion()){
                        Response.Redirect("NotAllow.aspx");
        }
        ///si la ventana es llamada mediante POST, evaluar estado
        if (this.IsPostBack)
        {
            if (this.hidcommand.Value == "setcliente")
            {
                objPresenter.RegistraCliente();

                this.hidcommand.Value = string.Empty;

            }
            else if (this.hidcommand.Value == "setsubyacente")
            {
                objPresenter.RegistraSubyacente();

                this.hidcommand.Value = string.Empty;

            }
            else if (this.hidcommand.Value == "cleanedit")
            {
                this.hidcommand.Value = string.Empty;

                if (!objPresenter.CargaControlesNuevo()) return;

                objPresenter.LimpiarFormulario();


            }
            //objPresenter.inicializaStock(iniQuot);
            //objPresenter.presetStock();


        }
        else {
            this.hidcommand.Value = string.Empty;

            if (!objPresenter.CargaControlesNuevo()) return;

            objPresenter.LimpiarFormulario();
        }

    }


    /// Getters y Setters
    public String pageContent { get { return pageMessage.InnerText; } set { pageMessage.InnerText = value; } }
    public HiddenField Comanda { get { return this.hidcommand; } set { this.hidcommand = value; } }
    public HiddenField Argumento { get { return this.hidargument; } set { this.hidargument = value; } }
    public ImageButton ToolNueva { get { return this.toolNueva; } set { this.toolNueva = value; } }

    public ImageButton ToolBusca { get { return this.toolBusca; } set { this.toolBusca = value; } }

    public ImageButton ToolSimula { get { return this.toolSimula; } set { this.toolSimula = value; } }

    public ImageButton ToolCotiza { get { return this.toolCotiza; } set { this.toolCotiza = value; } }

    public ImageButton ToolCursa { get { return this.toolCursa; } set { this.toolCursa = value; } }

    public ImageButton ToolAnula { get { return this.toolAnula; } set { this.toolAnula = value; } }

    public ImageButton ToolImprime { get { return this.toolImprime; } set { this.toolImprime = value; } }

    public HiddenField PageIdd { get { return this.pageIdd; } set { this.pageIdd = value; } }

    public TextBox NumeroCotizacion { get { return this.lblNumeroCotizacion; } set { this.lblNumeroCotizacion = value; } }

    public TextBox FechaCotizacion { get { return this.sprFechaCotizacion; } set { this.sprFechaCotizacion = value; } }

    public TextBox Vigencia { get { return this.sprVigencia; } set { this.sprVigencia = value; } }

    public Button Cotizar { get { return this.botCotizar; } set { this.botCotizar = value; } }
    public TextBox FechaCurse { get { return this.sprFechaCurse; } set { this.sprFechaCurse = value; } }
    public Button Cursar { get { return this.botCursar; } set { this.botCursar = value; } }

    public TextBox Estado { get { return this.lblEstado; } set { this.lblEstado = value; } }
    public TextBox FechaEstado { get { return this.sprFechaEstado; } set { this.sprFechaEstado = value; } }

    public TextBox RutCliente { get { return this.txtRutCliente; } set { this.txtRutCliente = value; } }
    public Label SinLineaCliente { get { return this.lbSinLineaCliente; } set { this.lbSinLineaCliente = value; } }

    public TextBox NombreCliente { get { return this.txtNombreCliente; } set { this.txtNombreCliente = value; } }

    public TextBox Pais { get { return this.lblPais; } set { this.lblPais = value; } }

    public TextBox Categoria { get { return this.lblCategoria; } set { this.lblCategoria = value; } }

    public TextBox CasaMatriz { get { return this.txCasaMatriz; } set { this.txCasaMatriz = value; } }

    public TextBox Swift { get { return this.txSwift; } set { this.txSwift = value; } }

    public TextBox NroOperacion { get { return this.txNroOperacion; } set { this.txNroOperacion = value; } }

    public CheckBox OperPuntual { get { return this.chkOperPuntual; } set { this.chkOperPuntual = value; } }

    public CheckBox Nota { get { return this.chkNota; } set { this.chkNota = value; } }

    public CheckBox CargoCasaMatriz { get { return this.chkCargoCasaMatriz; } set { this.chkCargoCasaMatriz = value; } }

    public DropDownList TipoOperacion { get { return this.cboTipoOperacion; } set { this.cboTipoOperacion = value; } }

    public DropDownList Producto { get { return this.cboProducto; } set { this.cboProducto = value; } }

    public TextBox CodigoTipoOperacion { get { return this.lblCodigoTipoOperacion; } set { this.lblCodigoTipoOperacion = value; } }

    public DropDownList TipoCredito { get { return this.cboTipoCredito; } set { this.cboTipoCredito = value; } }

    public DropDownList Moneda { get { return this.cboMoneda; } set { this.cboMoneda = value; } }

    public HiddenField Paridad { get { return this.parMoneda; } set { this.parMoneda = value; } }
    public TextBox MontoOperacion { get { return this.sprMontoOperacion; } set { this.sprMontoOperacion = value; } }

    public TextBox PorcentajeTolerancia { get { return this.sprPorcentajeTolerancia; } set { this.sprPorcentajeTolerancia = value; } }

    public TextBox PlazoMaxResidualCtg { get { return this.sprPlazoMaxResidualCtg; } set { this.sprPlazoMaxResidualCtg = value; } }

    public TextBox FechaVctoEstimada { get { return this.sprFechaVctoEstimada; } set { this.sprFechaVctoEstimada = value; } }

    public Label DiaFechaVctoEstimada { get { return this.lblDiaFechaVctoEstimada; } set { this.lblDiaFechaVctoEstimada = value; } }

    public TextBox Monto { get { return this.sprMonto; } set { this.sprMonto = value; } }

    public TextBox MontoEquivalente { get { return this.sprMontoEquivalente; } set { this.sprMontoEquivalente = value; } }

    public Button Simular { get { return this.cmdSimular; } set { this.cmdSimular = value; } }

    public TextBox NroPerSuby { get { return this.txNroPerSuby; } set { this.txNroPerSuby = value; } }
    public Label SinLineaSuby { get { return this.lbSinLineaSuby; } set { this.lbSinLineaSuby = value; } }

    public TextBox NomSuby { get { return this.txNomSuby; } set { this.txNomSuby = value; } }

    public TextBox PaisSuby { get { return this.lblPaisSuby; } set { this.lblPaisSuby = value; } }

    public TextBox CategSuby { get { return this.lblCategSuby; } set { this.lblCategSuby = value; } }

    public TextBox PorcenGarantia { get { return this.sprPorcenGarantia; } set { this.sprPorcenGarantia = value; } }

    public TextBox MontoGarantia { get { return this.sprMontoGarantia; } set { this.sprMontoGarantia = value; } }

    public DropDownList TipoOperSuby { get { return this.cboTipoOperSuby; } set { this.cboTipoOperSuby = value; } }

    public DropDownList ProdSuby { get { return this.cboProdSuby; } set { this.cboProdSuby = value; } }

    public DropDownList MoneComision { get { return this.cboMoneComision; } set { this.cboMoneComision = value; } }

    public TextBox MontoComisiones { get { return this.sprMontoComisiones; } set { this.sprMontoComisiones = value; } }

    public RadioButtonList GastosPorCuenta { get { return this.optGastosPorCuenta; } set { this.optGastosPorCuenta = value; } }

    public TextBox TasaPago { get { return this.txtTasaPago; } set { this.txtTasaPago = value; } }

    public TextBox ValorTasaPago { get { return this.sprValorTasaPago; } set { this.sprValorTasaPago = value; } }

    public TextBox ValorSpreadPago { get { return this.sprValorSpreadPago; } set { this.sprValorSpreadPago = value; } }

    public TextBox TasaTotalPago { get { return this.sprTasaTotalPago; } set { this.sprTasaTotalPago = value; } }

    public TextBox TasaPtmo { get { return this.txtTasaPtmo; } set { this.txtTasaPtmo = value; } }

    public TextBox ValorTasaPtmo { get { return this.sprValorTasaPtmo; } set { this.sprValorTasaPtmo = value; } }

    public TextBox ValorSpreadPtmo { get { return this.sprValorSpreadPtmo; } set { this.sprValorSpreadPtmo = value; } }

    public TextBox TasaTotalPtmo { get { return this.sprTasaTotalPtmo; } set { this.sprTasaTotalPtmo = value; } }

    public TextBox NombreImportador { get { return this.txtNombreImportador; } set { this.txtNombreImportador = value; } }

    public TextBox NombreExportador { get { return this.txtNombreExportador; } set { this.txtNombreExportador = value; } }

    public TextBox Mercaderia { get { return this.txtMercaderia; } set { this.txtMercaderia = value; } }

    public TextBox FechaContraGarantia { get { return this.sprFechaContraGarantia; } set { this.sprFechaContraGarantia = value; } }

    public DropDownList Reembolso { get { return this.cboReembolso; } set { this.cboReembolso = value; } }

    public TextBox Observaciones { get { return this.txtObservaciones; } set { this.txtObservaciones = value; } }

    protected void hidcommand_ValueChanged(object sender, EventArgs e)
    {
        hidargument.Value = string.Empty;
        //System.Diagnostics.Debug.WriteLine("INIDELISTA---------------------------------------------");
        //foreach (Control ctlg in this.Controls)
        //{
        //    if (ctlg.Controls.Count > 0)
        //    {
        //        foreach (Control ctl in ctlg.Controls)
        //        {
        //            System.Diagnostics.Debug.WriteLine("public " + ctl.GetType().ToString() + "\t" + ctl.ID);
        //        }
        //    }
        //    else
        //    {
        //        System.Diagnostics.Debug.WriteLine("OUTER--- " + ctlg.GetType().ToString() + "\t" + ctlg.ID);
        //    }
        //}
        //System.Diagnostics.Debug.WriteLine("FINDELISTA---------------------------------------------");

    }
    protected void cboTipoOperacion_SelectedIndexChanged(object sender, EventArgs e)
    {

        objPresenter.RegistraTipoProducto();

    }
    protected void cboTipoOperSuby_SelectedIndexChanged(object sender, EventArgs e)
    {
        objPresenter.RegistraTipoProductoSuby();

    }
    protected void cmdSimular_Click(object sender, EventArgs e)
    {
        objPresenter.EjecutaSimulacion();
        

    }
 
}
