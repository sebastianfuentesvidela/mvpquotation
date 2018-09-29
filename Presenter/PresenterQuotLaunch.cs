using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Globalization;
using Modelos;
using Modelos.Library;
using Vistas;
using Tools;
using System.Runtime.Serialization.Json;

using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace Presenter
{
    public class PresenterQuotLaunch
    {
        IQuotLaunch iView;
        CotizacionCLE cotizacion;
        PageComments pageComm;
        Dictionary<string, List<String>> errores;
        private String dataConnectionString;
        private SimulacionCLE simu;

        public PresenterQuotLaunch(String mainConnString)
        {
            dataConnectionString = mainConnString;
            cotizacion = new CotizacionCLE(dataConnectionString);
            pageComm = new PageComments();
            errores = new Dictionary<string, List<String>>();

        }
        public void add(IQuotLaunch ObjQuotView)
        {
            iView = ObjQuotView;
            iView.pageContent = string.Empty;
        }
        public void RegistraTipoProducto()
        {
            TipoProducto tipo = new TipoProducto(dataConnectionString);
            iView.TipoOperSuby.Items.Clear();
            iView.ProdSuby.Items.Clear();
            iView.Producto.Items.Clear();
            if (iView.TipoOperacion.SelectedIndex > 0)
            {

                //SetcboProducto
                String fami = iView.TipoOperacion.SelectedValue.Split('-')[0];
                Producto prod = new Producto(dataConnectionString);
                short success = 0;
                DataTable lisprod = prod.GetListFamilia(ref success, fami);
                if (success == 0)
                {
                    iView.Producto.DataTextField = "gls_dscrn_ctzcn_lcext";
                    iView.Producto.DataValueField = "cod_ctzcn_lcext";
                    iView.Producto.DataSource = lisprod;
                    iView.Producto.DataBind();
                }

                short resultado = tipo.Obtener(fami); //', liCodigoPlazo)
                switch (resultado)
                {
                    case 0:
                        iView.Simular.Enabled = true;
                        DataTable lista = tipo.GetListSubyQueComb(ref success);
                        if (success == 0)
                        {
                            iView.TipoOperSuby.DataTextField = "gls_dscrn_tipo_ctzcn_lcext";
                            iView.TipoOperSuby.DataValueField = "cod_tipo_ctzcn_lcext";
                            iView.TipoOperSuby.DataSource = lista;
                            iView.TipoOperSuby.DataBind();
                        }
                        iView.CodigoTipoOperacion.Text = tipo.CodigoFamilia;// '.Codigo
                        if (tipo.TipoCredito != 0)
                        {
                            iView.TipoCredito.SelectedValue = tipo.TipoCredito.ToString();// BuscarCombo(cboTipoCredito, ltipTipoProducto.TipoCredito)
                        }
                        break;
                    default:
                        break;
                }
                //SeteaBarra 2

            }
            else {
                iView.CodigoTipoOperacion.Text = string.Empty;// '.Codigo
            }
        }
        public void RegistraTipoProductoSuby()
        {

            TipoProducto tipo = new TipoProducto(dataConnectionString);
            iView.ProdSuby.Items.Clear();
            if (iView.TipoOperSuby.SelectedIndex > 0)
            {

                //SetcboProducto
                String fami = iView.TipoOperSuby.SelectedValue.Split('-')[0];
                Producto prod = new Producto(dataConnectionString);
                short success = 0;
                DataTable lisprod = prod.GetListFamilia(ref success, fami);
                if (success == 0)
                {
                    iView.ProdSuby.DataTextField = "gls_dscrn_ctzcn_lcext";
                    iView.ProdSuby.DataValueField = "cod_ctzcn_lcext";
                    iView.ProdSuby.DataSource = lisprod;
                    iView.ProdSuby.DataBind();
                }
            }
        }
 
        public void RegistraSubyacente()
        {
            iView.Comanda.Value = string.Empty;
            String ruCliente = iView.Argumento.Value;
            Persona pCliente = new Persona(dataConnectionString);
            iView.SinLineaSuby.Text = string.Empty;

            if (0 == pCliente.Obtener(ruCliente))
            {
                iView.NroPerSuby.Text = Global.ConvertirNroRut(ruCliente);

                iView.NomSuby.Text = pCliente.NombreEstructurado;
                iView.NomSuby.Attributes.Add("data-tag", pCliente.Numero);

                //nroMatr = fCliente.NroCasaMatriz(modGlobal.ConvertirNroRut(modGlobal.gtCodigoEncontrado));
                //if (nroMatr == string.Empty)
                //{
                //    chkCargoMatrizSubyacente.CheckState = CheckState.Unchecked;
                //    chkCargoMatrizSubyacente.Visible = false;
                //    chkCargoMatrizSubyacente.Enabled = false;
                //    txCasaMatrizSuby.Text = string.Empty;
                //    txCasaMatrizSuby.Tag = string.Empty;
                //}
                //else
                //{
                //    chkCargoMatrizSubyacente.CheckState = CheckState.Checked;
                //    chkCargoMatrizSubyacente.Visible = true;
                //    chkCargoMatrizSubyacente.Enabled = false;
                //    q = fMatriz.Obtener(nroMatr);
                //    txCasaMatrizSuby.Text = fMatriz.NombreEstructurado;
                //    txCasaMatrizSuby.Tag = nroMatr;
                //}
                Pais pPais = new Pais(dataConnectionString);
                if (0 == pPais.Obtener(pCliente.CodigoPais))
                {
                    iView.PaisSuby.Text = pPais.Nombre;
                    iView.PaisSuby.Attributes.Add("data-tag", pCliente.CodigoPais.ToString());

                    Categoria pCategoria = new Categoria(dataConnectionString);
                    if (0 == pCategoria.Obtener(pPais.Desarrollo))
                    {
                        iView.CategSuby.Text = pCategoria.Nombre;
                        iView.CategSuby.Attributes.Add("data-tag", pPais.Desarrollo.ToString());
                    }
                    else
                    {
                        iView.CategSuby.Text = "Indefinida";
                        iView.CategSuby.Attributes.Add("data-tag", string.Empty);
                    }
                    iView.Nota.Checked = true;
                    //this.cboTipoOperSuby.Focus();
                }
                Limite llimLimite;
                llimLimite = new Limite(dataConnectionString);
                int liRespuesta = 0;
                liRespuesta = llimLimite.ObtenerLineaCliente(ruCliente);
                if (liRespuesta != 0)
                {
                    pageComm.Add("lineasuby", "Está por generar una Simulación para un Deudor Subyacente Nuevo o Sin Línea " + pCliente.NombreEstructurado);
                    iView.SinLineaSuby.Text = "<-sin linea";

                }
                // 

            }
            else
            {
                pageComm.Add("errordatos", "Ocurrió un error al intentar obtener los datos del Deudor Subyacente.");
            }

            iView.pageContent = Modelos.Library.JsonInform.PageCommentsToJson(pageComm);

        }
 
        public void RegistraCliente()
        {
            iView.Comanda.Value = string.Empty;
            String ruCliente = iView.Argumento.Value;
            Persona pCliente = new Persona(dataConnectionString);
            Persona pMatriz = new Persona(dataConnectionString);
            iView.SinLineaCliente.Text = string.Empty;

            if (0 == pCliente.Obtener(ruCliente))
            {

                iView.RutCliente.Text = Global.ConvertirNroRut(ruCliente);

                iView.NombreCliente.Text = pCliente.NombreEstructurado;
                iView.NombreCliente.Attributes.Add("data-tag", pCliente.Numero);

                Limite llimLimite;
                llimLimite = new Limite(dataConnectionString);
                int liRespuesta = 0;
                liRespuesta = llimLimite.ObtenerLineaCliente(pCliente.Numero);
                if (liRespuesta != 0)
                {

                    //MessageBox.Show("Está por generar una Simulación para un Cliente Nuevo o Sin Línea " + fCliente.NombreEstructurado, "ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pageComm.Add("linea", "Está por generar una Simulación para un Cliente Nuevo o Sin Línea " + pCliente.NombreEstructurado);
                    iView.SinLineaCliente.Text = "<-sin linea";
                    // bOk = EnviaMensajeMail("AVISO", "CLE -> Aviso Vencimiento Línea", MsgVig)
                    // Exit Sub
                }
                // 
                // test lineas
                bool bOk;
                string MsgVig = "";
                MsgVig = pCliente.TieneLineasVigentes();
                if (!("OK" == MsgVig))
                {
                    pageComm.Add("lineas", "Está por generar una Simulación para un Cliente Nuevo o Sin Línea: " + pCliente.NombreEstructurado);
                    iView.SinLineaCliente.Text = "<-sin lineas";
                    //bOk = modMailing.EnviaMensajeMail("AVISO", "CLE -> Aviso Vencimiento Línea", MsgVig);
                    // MsgBox "Testeo Lineas Vigencia: " & MsgVig
                    // ShellExecute 0, vbNullString, "mailto: " & "?subject=Aviso&body=" & MsgVig, vbNullString, vbNullString, vbNormalFocus


                }
                Pais pPais = new Pais(dataConnectionString);
                if (0 == pPais.Obtener(pCliente.CodigoPais))
                {
                    String nroMatr = pCliente.NroCasaMatriz(Global.ConvertirNroRut(ruCliente));
                    if (nroMatr == string.Empty)
                    {
                        iView.CargoCasaMatriz.Checked = false;
                        iView.CargoCasaMatriz.Visible = false;
                        //iView.CargoCasaMatriz.Enabled = false;
                        iView.CasaMatriz.Text = string.Empty;
                        iView.CasaMatriz.Attributes.Add("data-tag", string.Empty);
                    }
                    else
                    {
                        iView.CargoCasaMatriz.Checked = true;
                        iView.CargoCasaMatriz.Visible = true;
                        iView.CargoCasaMatriz.Enabled = false;
                        short q = pMatriz.Obtener(nroMatr);
                        iView.CasaMatriz.Text = pMatriz.NombreEstructurado; ;
                        iView.CasaMatriz.Attributes.Add("data-tag", nroMatr);
                    }
                    //SSTab1.Enabled = true;
                   // iView.Nota.Enabled = true;
                    iView.OperPuntual.Enabled = true;
                    iView.Pais.Text = pPais.Nombre;
                    iView.Pais.Attributes.Add("data-tag", pCliente.CodigoPais.ToString());
                    Categoria pCategoria = new Categoria(dataConnectionString);
                    if (0 == pCategoria.Obtener(pPais.Desarrollo))
                    {
                        iView.Categoria.Text = pCategoria.Nombre;
                        iView.Categoria.Attributes.Add("data-tag", pPais.Desarrollo.ToString());
                    }
                    else
                    {
                        iView.Categoria.Text = "Indefinida";
                        iView.Categoria.Attributes.Add("data-tag", string.Empty);
                    }
                    // ControlaPlazos
                    //SSTab1.SelectedIndex = 0;
                    //cboTipoOperacion.Focus();
                    //iView.Cotizar.Enabled = false;
                }
            }
            else
            {
                pageComm.Add("errordatos", "Ocurrió un error al intentar obtener los datos del Cliente.");
            }

            iView.pageContent = Modelos.Library.JsonInform.PageCommentsToJson(pageComm);



        }
        public bool CargaControlesNuevo()
        {
            TipoProducto tipoProd = new TipoProducto(dataConnectionString);
            short success = 0; 
            DataTable tab = tipoProd.GetList(ref success);

            

            iView.TipoOperacion.Items.Clear();//SelectedIndex = -1;
            if (success == 0)
            {
                iView.TipoOperacion.DataTextField = "gls_dscrn_tipo_ctzcn_lcext";
                iView.TipoOperacion.DataValueField = "Item";
                iView.TipoOperacion.DataSource = tab;
                iView.TipoOperacion.DataBind();
            }
            else if (tipoProd.errlist.Count > 0)
            {
                errores.Add("errores", tipoProd.errlist);

                iView.Comanda.Value = "errores"; // string.Empty;
                iView.pageContent = Errores2Json();

                return false;
            }
           // iView.NumeroCotizacion
            iView.Producto.Items.Clear();//SelectedIndex = -1;
            iView.TipoCredito.SelectedIndex = -1;

            iView.TipoCredito.Items.Clear();
            ControlPlazo control = new ControlPlazo(dataConnectionString);
            DataTable rec = control.GetListTiposCredito(ref success);
            if (success == 0)
            {
                iView.TipoCredito.DataTextField = "Glosa";
                iView.TipoCredito.DataValueField = "Idx";
                iView.TipoCredito.DataSource = rec;
                iView.TipoCredito.DataBind();
            }
            else if (control.errlist.Count > 0)
            {
                errores.Add("errores", control.errlist);
                iView.Comanda.Value = "errores"; // string.Empty;
                iView.pageContent = Errores2Json();
                return false;
            }

            iView.Moneda.Items.Clear();
            Moneda mon = new Moneda(dataConnectionString);
            DataTable recm = mon.GetList(ref success);
            if (success == 0)
            {
                iView.Moneda.DataTextField = "gls_monda";
                iView.Moneda.DataValueField = "cod_monda";
                iView.Moneda.DataSource = recm;
                iView.Moneda.DataBind();
                iView.Moneda.SelectedValue = "13";

                iView.MoneComision.DataTextField = "gls_monda";
                iView.MoneComision.DataValueField = "cod_monda";
                iView.MoneComision.DataSource = recm;
                iView.MoneComision.DataBind();
                iView.MoneComision.SelectedValue = "13";

            }
            else if (mon.errlist.Count > 0)
            {
                errores.Add("errores", mon.errlist);
                iView.Comanda.Value = "errores"; // string.Empty;
                iView.pageContent = Errores2Json();
                return false;
            }


            iView.Reembolso.Items.Clear();
            DataTable recb = cotizacion.GetListCodigoReembolso(ref success);
            if (success == 0)
            {
                iView.Reembolso.DataTextField = "Glosa";
                iView.Reembolso.DataValueField = "Idx";
                iView.Reembolso.DataSource = recb;
                iView.Reembolso.DataBind();
            }
            else if (cotizacion.errlist.Count > 0)
            {
                errores.Add("errores", cotizacion.errlist);
                iView.Comanda.Value = "errores"; // string.Empty;
                iView.pageContent = Errores2Json();
                return false;
            }
            iView.TipoOperSuby.Items.Clear();
            iView.ProdSuby.Items.Clear();

            return true;

        }
  		public void LimpiarFormulario()
		{

  			// Cotizacion
            cotizacion.CrearNuevo();
            iView.PageIdd.Value = Tools.CryptoTools.Encrypt(DateTime.Now.ToString() + "\t(NUEVA)");
            iView.NumeroCotizacion.Text = "(NUEVA)";
			iView.FechaCotizacion.Text = DateTime.Today.ToString("dd/MM/yyyy", new CultureInfo("es-ES"));
			//iView.FechaCurseReal.Text = string.Empty;
			//iView.MontoOriginal.Text = string.Empty;
			//iView.PanAjusta.Visible = false;
			iView.Estado.Text = string.Empty;
			iView.FechaEstado.Text = string.Empty;
			// iView.ControlPlazos.MaxRows = 0
			iView.FechaCurse.Text = string.Empty;
			// Cargar Datos de Cliente
			iView.RutCliente.Text = string.Empty;
            iView.RutCliente.ReadOnly = true;
            iView.SinLineaCliente.Text = string.Empty;
            iView.NombreCliente.Text = string.Empty;
            iView.NombreCliente.Attributes.Add("data-tag", string.Empty);
            iView.NombreCliente.ReadOnly = true;

            //iView.NombreCliente.Tag = string.Empty;
			iView.Pais.Text = string.Empty;
            iView.Pais.ReadOnly = true;
            //iView.Pais.Tag = string.Empty;
            iView.Categoria.Text = string.Empty;
            iView.Categoria.ReadOnly = true;
            iView.CasaMatriz.Text = string.Empty;
            iView.CargoCasaMatriz.Checked = false;
            iView.CargoCasaMatriz.Visible = true;
            iView.CargoCasaMatriz.Enabled= false;
            iView.OperPuntual.Checked = false;
            iView.Nota.Checked = false;
            iView.Nota.Enabled = false;
            //iView.Categoria.Tag = string.Empty;
            //iView.Region.Text = string.Empty;
            //iView.Region.Tag = string.Empty;
            iView.Swift.Text = string.Empty;
            iView.Swift.Enabled = false;
            iView.NroOperacion.Text = string.Empty; 
            iView.NroOperacion.Enabled = false;

            iView.TipoOperacion.SelectedIndex = 0;// -1;
            iView.Producto.Items.Clear(); //.SelectedIndex = -1;
            iView.TipoCredito.SelectedIndex = 0; // -1;
            iView.CodigoTipoOperacion.Text = string.Empty;

            // SUBYACENTE
            iView.NroPerSuby.Text = string.Empty;
            iView.SinLineaSuby.Text = string.Empty;
            iView.NomSuby.Text = string.Empty;
            iView.PaisSuby.Text = string.Empty;
            iView.CategSuby.Text = string.Empty;

			// DEUDOR INDIRECTO
            iView.TipoOperSuby.Items.Clear();
            iView.ProdSuby.Items.Clear();

			iView.PorcenGarantia.Text = "100.00";
            iView.MontoGarantia.Text = "0.00";
			// ANTECEDENTES BASICOS

			iView.PlazoMaxResidualCtg.Text = "0";
            iView.FechaVctoEstimada.Text = DateTime.Today.ToString("dd/MM/yyyy", new CultureInfo("es-ES"));
			// iView.PlazoEfectivoCon.Text = string.Empty
			// iView.PlazoEfectivoSin.Text = string.Empty

			//iView.PlazoTotalEstimado.Text = string.Empty;
            CultureInfo castellano = new CultureInfo("es-CL");
            string sunday = castellano.DateTimeFormat.DayNames[(int)DateTime.Today.DayOfWeek];
            iView.DiaFechaVctoEstimada.Text = sunday;

			//iView.PlazoTotalReal.Text = string.Empty;
			//iView.FechaCurseReal.Text = string.Empty;

			// iView.FechaEmbarque.Text = string.Empty
			// iView.FechaDesembolso.Text = string.Empty

			iView.Moneda.SelectedIndex = 0;
            iView.Paridad.Value = "1";
            iView.MontoOperacion.Text = "0.00";// string.Empty;
            iView.PorcentajeTolerancia.Text = "0";//string.Empty;
            iView.Monto.Text = "0.00";//string.Empty;
            iView.MontoEquivalente.Text = "0.00";//string.Empty;

			// DATOS RENTABILIDAD

			//iView.MontoAfecto.Text = string.Empty;
			//iView.MontoDolares.Text = string.Empty;

            iView.MoneComision.SelectedIndex = 0; // -1;

            iView.MontoComisiones.Text = "0.00";//string.Empty;

			iView.GastosPorCuenta.Items[0].Selected = false;
            iView.GastosPorCuenta.Items[1].Selected = false;

			iView.TasaPago.Text = "TASA FIJA";
            iView.TasaPago.Attributes.Add("data-tag", "0");

            iView.ValorTasaPago.Text = "0.0000";//string.Empty;
            iView.ValorSpreadPago.Text = "0.0000";//string.Empty;
            iView.TasaTotalPago.Text = "0.0000";//string.Empty;

			iView.TasaPtmo.Text = "TASA FIJA";
            iView.TasaPtmo.Attributes.Add("data-tag", "0");
            iView.ValorTasaPtmo.Text = "0.0000";//string.Empty;
            iView.ValorSpreadPtmo.Text = "0.0000";//string.Empty;
            iView.TasaTotalPtmo.Text = "0.0000";//string.Empty;

			// OTROS ANTECEDENTES

			iView.Observaciones.Text = string.Empty;
			iView.NombreImportador.Text = string.Empty;
			iView.NombreExportador.Text = string.Empty;
			iView.Mercaderia.Text = string.Empty;
			// iView.OrigenRecursos.ListIndex = -1
            iView.Reembolso.SelectedIndex = 0;// -1;
			iView.FechaContraGarantia.Text = string.Empty;

            // v4
			//iView.NroCotizacionAjustar.Text = "-";
			//this.iView.OperAjustar.Text = string.Empty;

		} 


        public bool IniciaEdicion()
        {
            if (iView.PageIdd.Value == String.Empty)
            {
                //cotizacion.CrearNuevo();
                LimpiarFormulario();
                //iView.PageIdd.Text = Tools.CryptoTools.Encrypt(DateTime.Now.ToString() + "\t(NUEVA)");
            }
            else {
                String[] cot2Edit = Tools.CryptoTools.Decrypt(iView.PageIdd.Value).Split('\t');
                int edicoty;
                String editando = (cot2Edit.Length != 0) ? cot2Edit[1].ToString() : "(NUEVA)";

                if ((int.TryParse(editando, out edicoty)? 0: edicoty) == 0)
                {

                    cotizacion.CrearNuevo();

                } else {

                    cotizacion.Obtener( edicoty );
                }
                iView.PageIdd.Value = Tools.CryptoTools.Encrypt(DateTime.Now.ToString() + "\t" + editando);

            }
            return true;
            // iObjQuotView.showGrid.Columns.Clear();
            //iObjQuotView.showGrid.AutoGenerateEditButton = true;
        }
        public void EjecutaSimulacion()
        {
            if (!ObtenerSimulador(true))
            {
                iView.Comanda.Value = "faltainfo"; // string.Empty;
                iView.pageContent = Errores2Json();
                return;
            }

            if (!simu.GenerarSimulacion())
            {
                iView.Comanda.Value = "badgenerate"; // string.Empty;

                errores.Add("generando", simu.errlist);
                iView.pageContent = Errores2Json();

                return;
            }

            iView.pageContent = Simul2Json(simu);
            iView.Comanda.Value = "opensimuliz"; // string.Empty;

 
        }
        public bool ObtenerSimulador(bool rechaze)
        {
            string ltMensaje;
            List<string> errs = new List<string>();
            int nroCot = 0;
            nroCot = int.TryParse(iView.NumeroCotizacion.Text, out nroCot) ? nroCot : 0;
            if (nroCot == 0)
            {
                ltMensaje = "Es Nueva";
            }
            else
            {
                ltMensaje = "Es Antigua";
            }

            // Verifica el ingreso de los datos basicos
            if (iView.RutCliente.Text.Trim().Length == 0)
            {
                ltMensaje = "Falta identificar el Cliente";
                if (rechaze)
                {
                    errs.Add(ltMensaje);
                }
            }


            if (Array.Exists((new[] { 0, -1 }), 
                    element => element == iView.TipoOperacion.SelectedIndex))
            {
                ltMensaje = "Falta establecer El Tipo de Operación";
                if (rechaze)
                {
                    errs.Add(ltMensaje);
                }
            }
            if (iView.TipoCredito.SelectedIndex == -1)
            {
                ltMensaje = "Debe elegir un Tipo De Crédito";
                if (rechaze)
                {
                    errs.Add(ltMensaje);
                }
            }
            int test = -1;
            if (!int.TryParse(iView.PlazoMaxResidualCtg.Text, out test) || test == 0)
            {
                ltMensaje = "Falta indicar el Plazo de la Operación";
                if (rechaze)
                {
                    errs.Add(ltMensaje); 
                }
            }

            Decimal parMoneda = Decimal.TryParse(iView.Paridad.Value, NumberStyles.Number,
                CultureInfo.CreateSpecificCulture("en-US"), out parMoneda) ? parMoneda : 0;

            if (parMoneda == 0)
            {
                ltMensaje = "No Existe Paridad para la Moneda de la Operación";
                if (rechaze)
                {
                    errs.Add(ltMensaje);
                }
            }

            if (iView.Moneda.SelectedIndex == -1)
            {
                ltMensaje = "Se necesita identificar la Moneda";
                if (rechaze)
                {
                    errs.Add(ltMensaje);
                }
            }
            double montOp = double.TryParse(iView.MontoOperacion.Text, out montOp) ? montOp : 0;


            //if (montOp==0 && iView.NroCotizacionAjustar.Text.Trim()=="-") {
            if (montOp == 0)
            {
                ltMensaje = "Debe ingresar el Monto que va a afectar Línea";
                if (rechaze)
                {
                    errs.Add(ltMensaje);
                }
                //return false;
            }
            simu = new SimulacionCLE(dataConnectionString);

            simu.tNumeroOperacion = iView.NroOperacion.Text.Trim();
            simu.tNumeroCliente = iView.RutCliente.Text.Trim();
            simu.iCargoCasaMatriz = 0;
            if (iView.CasaMatriz.Attributes["data-tag"] != null && 
                iView.CasaMatriz.Attributes["data-tag"].ToString().Trim() != string.Empty)
                {
                    simu.iCargoCasaMatriz = 1;
                    simu.tNumeroCasaMatriz = iView.CasaMatriz.Attributes["data-tag"].ToString().Trim();
                }

            if (iView.Nota.Checked)
            {
                if (iView.NomSuby.Attributes["data-tag"] == null || iView.NomSuby.Attributes["data-tag"].ToString().Trim() == string.Empty)
                {
                        ltMensaje = "Debe Indicar El Deudor Indirecto, Garante o Subyacente";
                        if (rechaze)
                        {
                            errs.Add(ltMensaje);
                        }
                }
                if (Array.Exists((new[] { 0, -1 }),
                    element => element == iView.TipoOperSuby.SelectedIndex))
                {
                    ltMensaje = "Debe Indicar El Tipo Operacion Subyacente";
                    if (rechaze)
                    {
                        errs.Add(ltMensaje);
                    }
                    //return false;
                }
            }
            if (errs.Count > 0)
            {

                errores.Add("faltainfo", errs);
                return false;

            }

            String famcod = iView.TipoOperacion.SelectedValue;// Items[iView.TipoOperacion.SelectedIndex].ToString();
            famcod = famcod.Split('-')[0];
            simu.tTipoProducto = famcod; // frmLCEDatosSimulacion.TipoOperacion.List(frmLCEDatosSimulacion.TipoOperacion.ListIndex)
            if (iView.Producto.SelectedIndex >= 0)
            {
                simu.iCodigoProducto = int.Parse( iView.Producto.SelectedValue);// Support.GetItemData(iView.Producto, iView.Producto.SelectedIndex);
            }
            else
            {
                simu.iCodigoProducto = 0;
            }
            simu.tCodigoFamilia = iView.CodigoTipoOperacion.Text;
            simu.iCodigoMoneda = int.Parse( iView.Moneda.SelectedValue); // Support.GetItemData(iView.Moneda, iView.Moneda.SelectedIndex);
            //Decimal parMoneda = Decimal.TryParse(iView.Paridad.Value, NumberStyles.Number,
            //    CultureInfo.CreateSpecificCulture("en-US"), out parMoneda) ? parMoneda : 0; ;
            Decimal toleranc = Decimal.TryParse(iView.PorcentajeTolerancia.Text, NumberStyles.Number,
                CultureInfo.CreateSpecificCulture("en-US"), out toleranc) ? toleranc : 0; ;
            Decimal montOpe = Decimal.TryParse(iView.MontoOperacion.Text, NumberStyles.Number,
                CultureInfo.CreateSpecificCulture("en-US"), out montOpe) ? montOpe : 0; ;
            Decimal lMontOrigen;
            lMontOrigen = Decimal.TryParse(iView.Monto.Text, NumberStyles.Number,
                CultureInfo.CreateSpecificCulture("en-US"), out lMontOrigen) ? lMontOrigen : 0;
            lMontOrigen = (montOpe * (1 + toleranc/100)); 
            simu.dMontoOrigen = Convert.ToDouble( lMontOrigen);// tes = double.TryParse(iView.Monto.Text, out tes) ? tes : 0;

            // ojo: simular solo la diferencia entre la cotizacion original y la modificada
            CotizacionCLE oldCot = new CotizacionCLE(dataConnectionString);
            Decimal lMontAplica, lMontGarant; NumberStyles style;
            style = NumberStyles.AllowDecimalPoint;
            //number = Decimal.Parse(value, style);  
//            lMontAplica = Decimal.Parse(iView.MontoEquivalente.Text, NumberStyles.Number); //, out lMontAplica) ? lMontAplica : 0;
            lMontAplica = Decimal.TryParse(iView.MontoEquivalente.Text, NumberStyles.Number, 
                CultureInfo.CreateSpecificCulture("en-US"), out lMontAplica) ? lMontAplica : 0;
            lMontAplica = (montOpe * (1 + toleranc / 100)) * parMoneda;

            Decimal porGar = Decimal.TryParse(iView.PorcenGarantia.Text, NumberStyles.Number,
                CultureInfo.CreateSpecificCulture("en-US"), out porGar) ? porGar : 0; 
            lMontGarant = Decimal.TryParse(iView.MontoGarantia.Text, NumberStyles.Number, 
                CultureInfo.CreateSpecificCulture("en-US"), out lMontGarant) ? lMontGarant : 0;
            lMontGarant = porGar / 100 * lMontAplica;
            simu.iNroCotizacion = 0;
            if (nroCot > 0)
            {
                if (oldCot.Obtener(nroCot) == 0)
                {
                    lMontAplica = Decimal.Parse(iView.MontoEquivalente.Text) 
                        - (Decimal)oldCot.MontoDolares;
                    simu.iNroCotizacion = nroCot;
                }
                else {
                    errores.Add("ObtenerSimulador.1", oldCot.errlist);
                }
                oldCot = null;
            }

            simu.dMontoDolares = (double)lMontAplica; // CvNum(frmCotizacionCLE.MontoEquivalente.Text)
            simu.dMontoAfecta = (double)lMontAplica;
            simu.iCodigoPais =  Convert.ToInt16(iView.Pais.Attributes["data-tag"].ToString().Trim()); // As Integer
            simu.iCodigoCateg = Convert.ToInt16(iView.Categoria.Attributes["data-tag"].ToString().Trim()); // As Integer
            simu.iEsNotaEstruct = iView.Nota.Checked;
            simu.iEsOperPuntual = iView.OperPuntual.Checked;
            if (simu.iEsNotaEstruct)
            {
                famcod = iView.TipoOperSuby.SelectedValue.Split('-')[0]; //.Items[iView.TipoOperSuby.SelectedIndex].ToString();
                //famcod = Strings.Trim(Strings.Left(famcod, Strings.InStr(famcod, " > ", CompareMethod.Text/*?*/) - 1));
                simu.tTipoProdSuby = famcod; // As String
                if (iView.ProdSuby.SelectedIndex >= 0)
                {
                    simu.iCodigoProductoSby = Convert.ToInt32(iView.ProdSuby.SelectedValue); // Support.GetItemData(iView.ProdSuby, iView.ProdSuby.SelectedIndex);
                }
                else
                {
                }
                simu.tNumeroPerSuby = Convert.ToString(iView.NomSuby.Attributes["data-tag"].ToString().Trim()); // As String
                simu.iCodigoPaisSuby = Convert.ToInt16(iView.PaisSuby.Attributes["data-tag"].ToString().Trim()); // As Integer
                simu.iCodigoCategSuby = Convert.ToInt16(iView.CategSuby.Attributes["data-tag"].ToString().Trim()); // As Integer
                float pork;

                simu.sPorcGarantia = (float.TryParse(iView.PorcenGarantia.Text, NumberStyles.Number, new CultureInfo("en-US"), out pork) ? pork : 0) / 100;

                simu.dMontoGarantia = (double)(lMontGarant); // double.Parse(iView.MontoGarantia.Text);

                //if (Strings.Trim(Convert.ToString(iView.CasaMatrizSuby.Tag)) != string.Empty)
                //{
                //    // gSimulacion.iCargoCasaMatrizSuby = 1
                //    // gSimulacion.tNumeroCasaMatrizSuby = lperPersona.Obtener(frmCotizacionCLE.CasaMatrizSuby.Tag)
                //}
                //else
                //{
                //    // gSimulacion.iCargoCasaMatrizSuby = 0
                //}
            }
            else
            {
                simu.sPorcGarantia = 0;
                simu.dMontoGarantia = 0;
            }

            simu.iDiasPlazo = (int)Math.Round(Convert.ToSingle(iView.PlazoMaxResidualCtg.Text));

            short ok; 

            String lblCodigoFamilia_Text = simu.tCodigoFamilia;
            Persona per = new Persona(dataConnectionString);
            simu.nombreCliente = (per.Obtener(simu.tNumeroCliente) == 0) ? per.NombreEstructurado : string.Empty;
            per = null;
            Pais pai = new Pais(dataConnectionString);
            simu.nombrePaisCliente = (pai.Obtener(simu.iCodigoPais) == 0) ? pai.Nombre : string.Empty;
            pai = null;
            TipoProducto tipo = new TipoProducto(dataConnectionString);
            if (tipo.Obtener(simu.tTipoProducto) == 0)
                simu.nombreTipoProducto = tipo.Nombre;
            tipo=null;
            Producto pro = new Producto(dataConnectionString);
            if (pro.Obtener(simu.iCodigoProducto) == 0) 
                simu.nombreProducto = pro.Nombre;
            pro = null;
            Moneda mon = new Moneda(dataConnectionString);
            if (mon.Obtener(simu.iCodigoMoneda) == 0)
                simu.nombreMonedaMonto = mon.Nombre;
            mon = null;
            if (simu.iCargoCasaMatriz == 1)
            {
                Persona matriz = new Persona(dataConnectionString);
                if (matriz.Obtener(simu.tNumeroCasaMatriz) == 0)
                    simu.nombreMatriz = matriz.NombreEstructurado;
                matriz = null;
            }

                String MontoAfecta_Text = simu.dMontoOrigen.ToString();
                String MontoEquivalente_Text = simu.dMontoDolares.ToString();

                if (simu.tNumeroPerSuby != null && simu.tNumeroPerSuby != "")
                {
                    Persona sub = new Persona(dataConnectionString);
                    if (sub.Obtener(simu.tNumeroPerSuby) == 0)
                        simu.nombreDeudorSubyace = sub.NombreEstructurado;
                    sub = null;
                    Pais spai = new Pais(dataConnectionString);
                    if (spai.Obtener(simu.iCodigoPaisSuby) == 0)
                        simu.nombrePaisSubyace= spai.Nombre;
                    spai = null;
                    TipoProducto stipo = new TipoProducto(dataConnectionString);
                    if (stipo.Obtener(simu.tTipoProdSuby) == 0)
                        simu.nombreTipoProductoSubyace= stipo.Nombre;
                    stipo = null;
                    Producto spro = new Producto(dataConnectionString);
                    if (spro.Obtener(simu.iCodigoProductoSby) == 0)
                        simu.nombreProductoSubyace = spro.Nombre;
                    spro = null;

                    String sprPorcenGarantia = simu.sPorcGarantia.ToString();
                    String sprMontoGarantia = simu.dMontoGarantia.ToString();
                }
                else
                {
                    simu.tNumeroPerSuby = string.Empty;

                }



            return true;


        }
        public String Errores2Json()
        {
            String result = JsonConvert.SerializeObject(errores);
            return result;


            //DataContractJsonSerializer ser;
            //MemoryStream ms = new MemoryStream();
            //ser = new DataContractJsonSerializer(typeof(Dictionary<String, List<String>>));
            //ser.WriteObject(ms, errores);
            //byte[] json = ms.ToArray();
            //ms.Close();
            //return Encoding.UTF8.GetString(json, 0, json.Length);
        }

        public String Simul2Json(SimulacionCLE obj)
        {

            String result = JsonConvert.SerializeObject(obj);
            return result;

            //DataContractJsonSerializer ser;
            //MemoryStream ms = new MemoryStream();
            //ser = new DataContractJsonSerializer(typeof(Modelos.SimulacionCLE));
            //ser.WriteObject(ms, obj);
            //byte[] json = ms.ToArray();
            //ms.Close();
            //return Encoding.UTF8.GetString(json, 0, json.Length);
        }

    }
}
