using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Data;
using Modelos.Library;
using Tools;

namespace Modelos
{

    public class CotizacionCLE
    {

        //=========================================================
        private String dataConnectionString;

        public enum enumGastosPorCuentaDe
        {
            Ordenante = 1,
            Beneficiario = 2
        };


        // variables locales que contienen valores de propiedad
        private int mvarNumero;
        private string mvarTipoProducto = "";
        private int mvarCodigoProductoLCE;
        private string mvarNumeroPersona = "";
        private int mvarProductoServicio;
        private string mvarNumeroOperacion = "";
        private string mvarNumeroFuncionario = "";
        private int mvarCodigoMoneda;
        private int mvarCodigoEstado;
        private int mvarCodigoTipoTasa;
        private int mvarDiasPlazoTasa;
        private int mvarCodigoMonedaTasa;
        private double mvarValorTasaBase;
        private int mvarCodigoTipoFinanc;
        private double mvarMonto;
        private double mvarMontoDolares;
        private double mvarTasa;
        private int mvarDiasPlazo;
        private DateTime mvarFechaIngreso;
        private int mvarVigencia;
        private DateTime mvarFechaCurse;
        private DateTime mvarFechaCurseOriginal;
        private DateTime mvarFechaVencimiento;
        private double mvarValorComision;
        private double mvarPorcentajeComision;
        private double mvarTasaTransferencia;
        private double mvarIntereses;
        private int mvarAfectaCapitulo3b5;
        private bool mvarFlagCargoCasaMatriz;

        private string mvarGlosa = "";
        private string mvarImportador = "";
        private string mvarExportador = "";
        private DateTime mvarFechaDesembolso;
        private DateTime mvarFechaEmbarque;
        private string mvarMercaderia = "";

        // -----------------------------------------
        private int CodMensajePlazos;
        private int mCodigoTasaPrestamo;
        private string mGlosaTasaPrestamo = "";
        private int miTipoCredito;

        private double mdProcentajeTolerancia; // ctz_por_tlr
        private double mdMontoOperacion; // ctz_mnt_ope
        private int miPlazoMaximoResidualContingente;
        private int miPlazoEfectivoSinUsoFondos;
        private int miPlazoEfectivoConUsoFondos;
        // VBto upgrade warning: miGastosPorCuentaDe As int	OnWrite(enumGastosPorCuentaDe)
        private int miGastosPorCuentaDe;

        private int miCodigoTasaBasePagoAnticipado;
        private double miValorTasaBasePagoAnticipado;
        private double miValorSpreadPagoAnticipado;
        private double miValorTasaPagoAnticipado;
        public string GlosaTasaPagoAnticipado = "";

        private int miCodigoTasaBasePrestamo;
        private double miValorTasaBasePrestamo;
        private double miValorSpreadPrestamo;
        private double miValorTasaPrestamo;
        public string GlosaTasaPrestamo = "";

        private int miCodigoMensajeValidacionPlazos;

        private int miCodigoTipoReembolso;
        private DateTime mFechaContraGarantia;


        // Private mCodigoTasaPrestamo As Long

        // -----------------------------------------

        private bool mfEsNuevo;
        // sfv v3
        private int mvarEsNotaEstructurada;
        private int mvarEsOperacionPuntual;
        private string mvarNumeroPersonaSuby = "";
        private string mvarTipoProductoSuby = "";
        private int mvarCodigoProductoLCESuby;
        private string mvarSwiftBanco = "";
        // Private mvarNumeroOperacion As String

        // 
        private bool mvarFlagCargoCasaMatrizSuby;
        private int mvarCodigoMonedaComision;
        private double mdProcentajeGarantia;
        private double mdMontoGarantizado;
        private int mvarNroCotizacionAjuste;
        private DateTime mvarFechaUltimoEstado;

        public bool SubyAsume70porc;
        public List<string> errlist;

        public CotizacionCLE(String ConexionString)
            : base()
        {
            dataConnectionString = ConexionString;
            errlist = new List<string>();
                mfEsNuevo = false;
        }

        // 
        public Dictionary<String, String> BuidInfo()
        {
            Dictionary<String, String> ArrInfo = new Dictionary<string, string>();
            string ltConsulta, strEstado;

            ltConsulta = "sp_slc_ta_estdo_ctzcn_lcext_all";
            DataTable lrecEstado = new DataFinder(dataConnectionString).GetRecordset(ltConsulta);
            DataView dv = lrecEstado.DefaultView;
            dv.RowFilter = "cod_estdo_ctzcn_lcext = " + mvarCodigoEstado;
            strEstado = Convert.ToString(dv[0][1]);

            ArrInfo.Add("Numero", mvarNumero.ToString());
            ArrInfo.Add("CodigoEstado", Convert.ToString(mvarCodigoEstado) + " (" + strEstado + ")");
            ArrInfo.Add("FechaCurse", mvarFechaCurse.ToString());
            ArrInfo.Add("Vigencia", mvarVigencia.ToString()); // & " (" & nomConti & ")"

            return ArrInfo;
        }


        public DataTable GetList(ref short success)
        {
            success = 0;
            // Descripción : Obtiene una lista de todas las cotizaciones
            // Parámetros  : Ninguno
            // Retorno     : 0 OK
            // 3 No existe
            // 4 Error en la base de datos
            // E. laterales: Ninguno
            // 
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================
            string ltConsulta;
            ltConsulta = "exec sp_lce_cna_lst_ctzcn_lcext";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataTable precCotizacion = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (precCotizacion.Rows.Count == 0)
                    {
                        success = 3;
                    }
                    else
                    {
                    }

                    return precCotizacion;

                }
                else
                {
                    // ManejoError:
                    //modLCEData.GenericError("Cotizacion.Getlist", Information.Err().Number, Information.Err().Description);
                    errlist.AddRange(db.errlist);
                    success = 4;

                }
            }
            return null;
        }

        public void CrearNuevo()
        {
            Numero = 0;
            TipoProducto = string.Empty;
            CodigoProductoLCE = 0;
            NumeroPersona = string.Empty;
            ProductoServicio = (short)IntPtr.Zero;
            NumeroOperacion = Convert.ToString((int)IntPtr.Zero);
            NroCotizacionAjuste = 0;
            NumeroFuncionario = string.Empty;
            CodigoMoneda = 0;
            CodigoEstado = 0;
            CodigoTipoTasa = 0;
            ValorTasaBase = 0;
            CodigoTipoFinanc = 0;
            Monto = 0;
            MontoDolares = 0;
            Tasa = 0;
            DiasPlazo = 0;
            FechaIngreso = DateTime.Now;
            Vigencia = 7;
            // FechaCurse = vbNull
            // FechaDesembolso = 0
            // FechaVencimiento = 0
            ValorComision = 0;
            PorcentajeComision = 0;
            TasaTransferencia = 0;
            Intereses = 0;
            AfectaCapitulo3b5 = 0; // 1       'No afecta
            FlagCargoCasaMatriz = false; // No se realiza el cargo a la casa matriz
            FlagCargoCasaMatrizSuby = false; // No se realiza el cargo a la casa matriz

            Glosa = string.Empty;
            Importador = string.Empty;
            Exportador = string.Empty;
            // FechaEmbarque = 0
            Mercaderia = string.Empty;

            mfEsNuevo = true;
        }
        public bool CanBeCursated(object caller)
        {
            bool CanBeCursated = false;
            short liResultado;
            Capitulo3b5 lcapCapitulo3b5 = new Capitulo3b5(dataConnectionString);
            // VBto upgrade warning: ltipTipoProducto As TipoProducto	OnWrite(string)
            TipoProducto ltipTipoProducto = new TipoProducto(dataConnectionString);
            RelacionLimiteCotizacion lrelLimiteCotizacion = new RelacionLimiteCotizacion(dataConnectionString);
            Limite llimLimite = new Limite(dataConnectionString);
            // VBto upgrade warning: Dispo15 As Decimal	OnWrite(double)
            // VBto upgrade warning: Dispo30 As Decimal	OnWrite(double)
            // VBto upgrade warning: Dispo70 As Decimal	OnWrite(double)
            Decimal Dispo15, Dispo30, Dispo70; string CodTipoOpFam, ltMensaje = "";
            string ltComando = ""; int liLimsNorm; string glsLimsNorm;
            DataTable lrecConsulta;

            CanBeCursated = true;
            liResultado = lcapCapitulo3b5.Obtener(DateTime.Now);
            Dispo15 = Convert.ToDecimal(lcapCapitulo3b5.MontoDisponible15);
            Dispo30 = Convert.ToDecimal(lcapCapitulo3b5.MontoDisponible30);
            Dispo70 = Convert.ToDecimal(lcapCapitulo3b5.MontoDisponible70);
            liResultado = ltipTipoProducto.Obtener(mvarTipoProducto); // , liCodigoPlazo)
            CodTipoOpFam = string.Empty;
            if (liResultado == 0)
            {
                CodTipoOpFam = ltipTipoProducto.CodigoFamilia; // .Codigo
            }
            if (CodTipoOpFam != String.Empty)
            {
                if ((Strings.InStr("BCDEFG", CodTipoOpFam, CompareMethod.Text) > 0 && Dispo70 < 0) || (Strings.InStr("DFG", CodTipoOpFam, CompareMethod.Text/*?*/) > 0 && Dispo30 < 0) || (Strings.InStr("G", CodTipoOpFam, CompareMethod.Text/*?*/) > 0 && Dispo15 < 0))
                {

                    //VBtoConverter.ScreenCursor = Cursors.Default;
                    ltMensaje = "La Cotización Nº" + Convert.ToString(mvarNumero) + " no puede ser Cursada." + "\r" + "Puesto que El Límite Normativo CAP 3B5 Ha Sido Sobrepasado";
                    //TODO	MessageBox.Show(ltMensaje, "Curse", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    CanBeCursated = false;
                }
            }
            liLimsNorm = 0; glsLimsNorm = string.Empty;
            lrecConsulta = lrelLimiteCotizacion.GetListLimitesAfectados(ref liResultado, mvarNumero);
            for (int i = 0; i < lrecConsulta.Rows.Count; i++)//     while (lrecConsulta.Rows.Count!=0)
            {
                //VBtoConverter.ScreenCursor = Cursors.WaitCursor;
                liResultado = llimLimite.Obtener(Convert.ToInt32(lrecConsulta.Rows[i]["cod_limte"].ToString()), Convert.ToInt32(lrecConsulta.Rows[i]["nro_crtvo_limte"].ToString()), (DateTime)lrecConsulta.Rows[i]["fch_otgmt_limte"]);
                //VBtoConverter.ScreenCursor = Cursors.Default;
                if (liResultado == 0)
                {
                    if (Convert.ToBoolean(llimLimite.EsNormativo))
                    {
                        if (llimLimite.MontoDisponible <= 0)
                        {
                            liLimsNorm += 1;
                            glsLimsNorm += "\r\n" + "· " + llimLimite.Nombre;
                        }
                    }
                }
                //lrecConsulta.MoveNext();
            }
            if (liLimsNorm > 0)
            {
                //VBtoConverter.ScreenCursor = Cursors.Default;
                ltMensaje = "La Cotización Nº" + Convert.ToString(mvarNumero) + " no puede ser Cursada." + "\r" + "Puesto que Se ha Sobrepasado Los Límites Normativos: " + glsLimsNorm;
                //TODO MessageBox.Show(ltMensaje, "Curse", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CanBeCursated = false;
            }
            lcapCapitulo3b5 = null;
            ltipTipoProducto = null;
            lrelLimiteCotizacion = null;
            llimLimite = null;

            return CanBeCursated;
        }
        public short Guardar() { return Guardar(null); }
        public short Guardar(ExepcionCLE obExep)
        {
            // Descripción : Ingresa o Modifica una Cotizacion LCE
            // Parámetros  : Ninguno
            // Retorno     : 0 OK
            // 3 Error al guardar
            // E. laterales: Ninguno
            // 
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================
            string ltComando;
            string ltParam;
            string ltFechaCurse = "";

            if (mfEsNuevo)
            {
                Numero = 0;
            }

            if (FechaCurse == DateTime.MinValue) //si viene vacio
            {
                ltFechaCurse = "'" + DateTime.Now.ToString("yyyy/mm/dd") + "'";
            }
            else
            {
                ltFechaCurse = "'" + FechaCurse.ToString("yyyy/mm/dd") + "'";
            }
            // CodMensajePlazos = 0
            NumeroFuncionario = Global.ConvertirRutNro(NumeroFuncionario);
            NumeroPersona = Global.ConvertirRutNro(NumeroPersona);
            NumeroPersonaSuby = Global.ConvertirRutNro(NumeroPersonaSuby);

            ltComando = "exec sva_cle_act_ctz ";

            ltParam = string.Empty;
            AddParam(ref ltParam, Numero); // @nro_ctzcn
            AddParam(ref ltParam, TipoProducto); // @cod_tipo_ctzcn_lcext
            AddParam(ref ltParam, CodigoProductoLCE); // @cod_ctzcn_lcext
            AddParam(ref ltParam, NumeroPersona); // @nro_prsna
            AddParam(ref ltParam, NumeroOperacion); // @nro_oprcn
            AddParam(ref ltParam, NumeroFuncionario); // @nro_fncro
            AddParam(ref ltParam, CodigoMoneda); // @cod_monda
            AddParam(ref ltParam, CodigoEstado); // @cod_estdo_ctzcn
            AddParam(ref ltParam, CodigoTipoTasa); // @cod_tipo_tasa
            AddParam(ref ltParam, CodigoTipoFinanc); // @cod_tipo_fncmt
            AddParam(ref ltParam, Monto); // @mnt_ctzcn
            AddParam(ref ltParam, FechaIngreso); // @fch_ctzcn
            AddParam(ref ltParam, Vigencia); // @can_dias_vgnca_ctzcn
            AddParam(ref ltParam, FechaCurse); // @fch_curse_ctzcn
            AddParam(ref ltParam, FechaDesembolso); // @fch_dsmbs_ctzcn
            AddParam(ref ltParam, FechaVencimiento); // @fch_vncto_ctzcn
            AddParam(ref ltParam, ValorComision); // @vlr_cmson
            AddParam(ref ltParam, CodigoMonedaComision); // @mon_cms
            // AddParam ltParam, PorcentajeComision                    '@vlr_prcje_cmson
            AddParam(ref ltParam, Intereses); // @mnt_intrs_ctzcn
            AddParam(ref ltParam, AfectaCapitulo3b5); // @flg_afcto_captl_3b5
            AddParam(ref ltParam, MontoDolares); // @mnt_ctzcn_usd
            AddParam(ref ltParam, FlagCargoCasaMatriz); // @flg_cargo_casa_matriz
            AddParam(ref ltParam, CodigoMensajeValidacionPlazos); // @pge_cod_msj
            AddParam(ref ltParam, Glosa); // @gls_ctzcn
            AddParam(ref ltParam, Importador); // @nom_impdr
            AddParam(ref ltParam, Exportador); // @nom_expdr
            AddParam(ref ltParam, Mercaderia); // @gls_dscrn_mrcdr_ctzcn
            AddParam(ref ltParam, (CodigoTasaBasePagoAnticipado == 0 ? 0 : 1)); // @pge_tip_tas_ctz
            AddParam(ref ltParam, CodigoTasaBasePagoAnticipado); // @ctz_cod_tas_pag
            AddParam(ref ltParam, ValorTasaBasePagoAnticipado); // @ctz_val_tas_pgo
            AddParam(ref ltParam, ValorSpreadPagoAnticipado); // @ctz_spr_pgo
            AddParam(ref ltParam, GlosaTasaPagoAnticipado); // @ctz_des_tas_pag
            AddParam(ref ltParam, CodigoTasaBasePrestamo); // @ctz_cod_tas_pmo
            AddParam(ref ltParam, ValorTasaBasePrestamo); // @ctz_val_tas_pmo
            AddParam(ref ltParam, ValorSpreadPrestamo); // @ctz_spr_pmo
            AddParam(ref ltParam, GlosaTasaPrestamo); // @ctz_des_tas_pmo
            AddParam(ref ltParam, PlazoMaximoResidualContingente); // @ctz_pzo_mrs_cgt
            AddParam(ref ltParam, PlazoEfectivoSinUsoFondos); // @ctz_pzo_efc_sus_fon
            AddParam(ref ltParam, PlazoEfectivoConUsoFondos); // @ctz_pzo_efc_cus_fon
            AddParam(ref ltParam, CodigoTipoCredito); // @pge_tip_crd
            AddParam(ref ltParam, FechaEmbarque); // @ctz_fec_emb
            AddParam(ref ltParam, GastosPorCuentaDe); // @ctz_cod_rsp_gcm
            AddParam(ref ltParam, MontoOperacion); // @ctz_mnt_ope
            AddParam(ref ltParam, PorcentajeTolerancia); // @ctz_por_tlr
            AddParam(ref ltParam, FechaContraGarantia); // @anx_ctz_fec_gar
            AddParam(ref ltParam, CodigoTipoReembolso); // @pge_cod_ree
            AddParam(ref ltParam, EsNotaEstructurada); // @flg_not_etu
            AddParam(ref ltParam, EsOperacionPuntual); // @flg_ope_pun
            AddParam(ref ltParam, NumeroPersonaSuby); // @nro_pna_suby
            AddParam(ref ltParam, TipoProductoSuby); // @cod_tipo_ctzcn_suby
            AddParam(ref ltParam, CodigoProductoLCESuby); // @cod_ctzcn_suby
            AddParam(ref ltParam, SwiftBanco); // @cod_swift
            // AddParam ltParam, FlagCargoCasaMatrizSuby                   '@flg_car_casa_matriz_subyacente
            AddParam(ref ltParam, PorcentajeGarantia); // @por_gtz
            AddParam(ref ltParam, MontoGarantizado); // @mnt_gtz
            AddParam(ref ltParam, NroCotizacionAjuste); // @num_ctz_aju
            AddParam(ref ltParam, FechaUltimoEstado); // @ctz_fec_ult_est

            if (!(obExep == null))
            {
                AddParam(ref ltParam, obExep.AprobadoPor); // @cod_swift
                AddParam(ref ltParam, obExep.FechaAprob); // @cod_swift
                AddParam(ref ltParam, obExep.NumAprobacion); // @cod_swift
                AddParam(ref ltParam, obExep.Comment); // @cod_swift
            }
            else
            {
                AddParam(ref ltParam, 0); // @cod_swift
                AddParam(ref ltParam, null); // @cod_swift
                AddParam(ref ltParam, 0); // @cod_swift
                AddParam(ref ltParam, ""); // @cod_swift
            }

            ltComando += " " + ltParam;

            short success = 0;
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataTable rec = db.GetRecordset(ltComando);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count == 0)
                    {
                        success = 3;
                    }
                    else
                    {
                        Numero = Convert.ToInt32(rec.Rows[0][0].ToString());
                        mfEsNuevo = false;
                        success = 0;
                    }


                }
                else
                {
                    //modLCEData.GenericError("Cotizacion.Guardar", Information.Err().Number, Information.Err().Description);
                    errlist.AddRange(db.errlist);
                    success = 3;

                }
            }
            return success;


        }

        public short Eliminar(int plNumero)
        {
            // Descripción : Elimina una cotizacion por número
            // Parámetros  : Ninguno
            // Retorno     : 0 OK
            // 3 Error al eliminar
            // E. laterales: Ninguno
            // 
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================
            string ltComando;
            short success = 0;
            ltComando = "exec sp_lce_eli_ctzcn_lcext " + Convert.ToString(plNumero);
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                int resultado = db.ExecuteCommand(ltComando);
                if (db.errlist.Count == 0)
                {
                    if (resultado == 1)
                    {
                        success = 0;
                        mfEsNuevo = false;
                    }
                    else
                    {
                        success = 3;
                    }
                }
                else
                {
                    //modLCEData.GenericError("Cotizacion.Eliminar", Information.Err().Number, Information.Err().Description);
                    errlist.AddRange(db.errlist);
                    success = 3;

                }

            }
            return success;

        }

        // VBto upgrade warning: plNumero As int	OnWrite(int, VBtoField, double)
        public short Obtener(int plNumero)
        {
            // Descripción : Obtiene una cotizacion por número
            // Parámetros  : piCodigo
            // Retorno     : 0 OK
            // 3 No existe la cotizacion
            // 4 Error en la BD
            // E. laterales: Ninguno
            // 
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================
            short success;
            string ltConsulta;
            ltConsulta = "exec svc_cle_obt_ctz " + Convert.ToString(plNumero);
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataSet dset = db.GetDataSet(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (dset.Tables[0].Rows.Count == 0)
                    {
                        success = 3;
                    }
                    else
                    {
                        DataTable dt1 = dset.Tables[0];
                        Numero = Convert.ToInt32(dt1.Rows[0]["nro_ctzcn"].ToString());
                        TipoProducto = Convert.ToString(dt1.Rows[0]["cod_tipo_ctzcn_lcext"].ToString()) + "";
                        CodigoProductoLCE = Convert.ToInt32(Convert.ToString(dt1.Rows[0]["cod_ctzcn_lcext"].ToString()) + "");
                        NumeroPersona = Global.ConvertirNroRut(Convert.ToString(dt1.Rows[0]["nro_prsna"].ToString())) + "";
                        NumeroOperacion = Strings.Trim("" + Convert.ToString(dt1.Rows[0]["ctz_nro_ope_car"].ToString()));

                        String ltNumeroFuncionario = Strings.Trim(Convert.ToString(dt1.Rows[0]["nro_fncro"].ToString()) + "");
                        if (ltNumeroFuncionario.Length == 0)
                        {
                            NumeroFuncionario = string.Empty;
                        }
                        else
                        {
                            NumeroFuncionario = Global.ConvertirNroRut(ltNumeroFuncionario);
                        }

                        CodigoMoneda = Convert.ToInt16(dt1.Rows[0]["cod_monda"].ToString());
                        CodigoEstado = Convert.ToInt16(dt1.Rows[0]["cod_estdo_ctzcn"].ToString());
                        // CodigoTipoTasa = lrecConsulta("cod_tipo_tasa")
                        // CodigoTipoFinanc = lrecConsulta("cod_tipo_fncmt")
                        Monto = VBtoConverter.ObjectToDouble(dt1.Rows[0]["mnt_ctzcn"].ToString());
                        FechaIngreso = (DateTime)dt1.Rows[0]["fch_ctzcn"];
                        Vigencia = Convert.ToInt16(dt1.Rows[0]["can_dias_vgnca_ctzcn"].ToString());
                        FechaCurse = (DateTime)(VBtoConverter.IsNull(dt1.Rows[0]["fch_curse_ctzcn"]) ? (DateTime.MinValue) : (DateTime)dt1.Rows[0]["fch_curse_ctzcn"]);
                        FechaDesembolso = (DateTime)(VBtoConverter.IsNull(dt1.Rows[0]["fch_dsmbs_ctzcn"]) ? (DateTime.MinValue) : (DateTime)dt1.Rows[0]["fch_dsmbs_ctzcn"]);
                        FechaVencimiento = (DateTime)dt1.Rows[0]["fch_vncto_ctzcn"];
                        ValorComision = VBtoConverter.ObjectToDouble(dt1.Rows[0]["vlr_cmson"].ToString());
                        // pz 21/09/2011
                        CodigoMonedaComision = Convert.ToInt16(dt1.Rows[0]["ctz_cod_mon_cms"]);
                        Intereses = VBtoConverter.ObjectToDouble(dt1.Rows[0]["mnt_intrs_ctzcn"].ToString());
                        AfectaCapitulo3b5 = Convert.ToInt16(dt1.Rows[0]["flg_afcto_captl_3b5"].ToString());
                        MontoDolares = VBtoConverter.ObjectToDouble(dt1.Rows[0]["mnt_ctzcn_usd"].ToString());
                        // sfv
                        FlagCargoCasaMatriz = Convert.ToBoolean(dt1.Rows[0]["flg_cargo_casa_matriz"]); // IIf(lrecConsulta("flg_cargo_casa_matriz") = 1, True, False)
                        CodigoMensajeValidacionPlazos = (VBtoConverter.IsNull(dt1.Rows[0]["pge_cod_msj"]) ? 0 : (int)dt1.Rows[0]["pge_cod_msj"]);
                        CodigoTasaBasePagoAnticipado = (VBtoConverter.IsNull(dt1.Rows[0]["pge_tip_tas_ctz"]) ? -1 : (int)dt1.Rows[0]["pge_tip_tas_ctz"]);
                        CodigoTasaBasePagoAnticipado = (VBtoConverter.IsNull(dt1.Rows[0]["ctz_cod_tas_pag"]) ? -1 : (int)dt1.Rows[0]["ctz_cod_tas_pag"]);
                        ValorTasaBasePagoAnticipado = Convert.ToDouble((dt1.Rows[0]["ctz_val_tas_pgo"] == null) ? -1 : dt1.Rows[0]["ctz_val_tas_pgo"]);
                        ValorSpreadPagoAnticipado = Convert.ToDouble((dt1.Rows[0]["ctz_spr_pgo"] == null) ? -1 : dt1.Rows[0]["ctz_spr_pgo"]);
                        GlosaTasaPagoAnticipado = Convert.ToString(dt1.Rows[0]["ctz_des_tas_pag"].ToString()) + "";
                        CodigoTasaBasePrestamo = (VBtoConverter.IsNull(dt1.Rows[0]["ctz_cod_tas_pmo"].ToString()) ? -1 : (int)dt1.Rows[0]["ctz_cod_tas_pmo"]);
                        ValorTasaBasePrestamo = Convert.ToDouble((dt1.Rows[0]["ctz_val_tas_pmo"]== null) ? -1 : dt1.Rows[0]["ctz_val_tas_pmo"]);
                        ValorSpreadPrestamo = Convert.ToDouble((dt1.Rows[0]["ctz_spr_pmo"] == null) ? -1 : dt1.Rows[0]["ctz_spr_pmo"]);
                        GlosaTasaPrestamo = Convert.ToString(dt1.Rows[0]["ctz_des_tas_pmo"].ToString()) + "";
                        PlazoMaximoResidualContingente = (VBtoConverter.IsNull(dt1.Rows[0]["ctz_pzo_mrs_cgt"]) ? 0 : (int)dt1.Rows[0]["ctz_pzo_mrs_cgt"]);
                        PlazoMaximoResidualContingente += (VBtoConverter.IsNull(dt1.Rows[0]["ctz_pzo_efc_sus_fon"]) ? 0 : (int)dt1.Rows[0]["ctz_pzo_efc_sus_fon"]);
                        PlazoMaximoResidualContingente += (VBtoConverter.IsNull(dt1.Rows[0]["ctz_pzo_efc_cus_fon"]) ? 0 : (int)dt1.Rows[0]["ctz_pzo_efc_cus_fon"]);
                        CodigoTipoCredito = Convert.ToInt16((dt1.Rows[0]["pge_tip_crd"] == null) ? -1 : dt1.Rows[0]["pge_tip_crd"]);
                        FechaEmbarque = (DateTime)(VBtoConverter.IsNull(dt1.Rows[0]["ctz_fec_emb"]) ? (DateTime.MinValue) : (DateTime)dt1.Rows[0]["ctz_fec_emb"]);
                        GastosPorCuentaDe = (VBtoConverter.IsNull(dt1.Rows[0]["ctz_cod_rsp_gcm"]) ? (enumGastosPorCuentaDe)(-1) : (enumGastosPorCuentaDe)dt1.Rows[0]["ctz_cod_rsp_gcm"]);
                        MontoOperacion = Convert.ToDouble((dt1.Rows[0]["ctz_mnt_ope"] == null) ? 0.0 : dt1.Rows[0]["ctz_mnt_ope"]);
                        PorcentajeTolerancia = Convert.ToDouble((dt1.Rows[0]["ctz_por_tlr"] == null) ? 0.0 : dt1.Rows[0]["ctz_por_tlr"]);
                        SwiftBanco = Convert.ToString(dt1.Rows[0]["ctz_cod_swf"].ToString());
                        EsOperacionPuntual = Convert.ToInt16((dt1.Rows[0]["ctz_ind_ope_pun"]== null) ? 0 : dt1.Rows[0]["ctz_ind_ope_pun"]);
                        EsNotaEstructurada = Convert.ToInt16((dt1.Rows[0]["ctz_ind_nta_etu"] == null) ? 0 : dt1.Rows[0]["ctz_ind_nta_etu"]);
                        // pz 21/09/2011
                        NroCotizacionAjuste = (VBtoConverter.IsNull(dt1.Rows[0]["ctz_num_ctz_org"].ToString()) ? 0 : (int)dt1.Rows[0]["ctz_num_ctz_org"]);
                        FechaUltimoEstado = (DateTime)(VBtoConverter.IsNull(dt1.Rows[0]["ctz_fec_ult_est"]) ? (DateTime.MinValue) : (DateTime)dt1.Rows[0]["ctz_fec_ult_est"]);

                        //lrecConsulta2 = lrecConsulta.NextRecordset();
                        // Anexo Cotización
                        DataTable dt2 = dset.Tables[1];
                        // lrecConsulta2 = "exec sp_lce_cna_datos_ctzcn " & plNumero
                        // Set lrecConsulta2 = GetRecordset(gbasAlce, lrecConsulta2)
                        Glosa = Strings.Trim(Convert.ToString(dt2.Rows[0]["gls_ctzcn"].ToString()));
                        Importador = Strings.Trim(Convert.ToString(dt2.Rows[0]["nom_impdr"].ToString()));
                        Exportador = Strings.Trim(Convert.ToString(dt2.Rows[0]["nom_expdr"].ToString()));
                        Mercaderia = Strings.Trim(Convert.ToString(dt2.Rows[0]["gls_dscrn_mrcdr_ctzcn"].ToString())) + "";
                        FechaContraGarantia = (DateTime)(VBtoConverter.IsNull(dt2.Rows[0]["anx_ctz_fec_gar"]) ? (DateTime.MinValue) : (DateTime)dt2.Rows[0]["anx_ctz_fec_gar"]);
                        CodigoTipoReembolso = (VBtoConverter.IsNull(dt2.Rows[0]["pge_cod_ree"]) ? -1 : (int)dt2.Rows[0]["pge_cod_ree"]);

                        // lrecConsulta2 = lrecConsulta.NextRecordset();
                        if (dset.Tables.Count > 2) //!(lrecConsulta2 == null))
                        {
                            DataTable dt3 = dset.Tables[2];
                            if (dt3.Rows.Count > 0) //!lrecConsulta2.EOF)
                            {
                                NumeroPersonaSuby = Convert.ToString(dt2.Rows[0]["dei_rut_gar"].ToString());
                                TipoProductoSuby = Strings.Trim(Convert.ToString(dt2.Rows[0]["dei_tip_ctz_cle"].ToString()));
                                CodigoProductoLCESuby = (int)Math.Round(Conversion.Val(Convert.ToString(dt2.Rows[0]["dei_cod_ctz_cle"].ToString())));
                                PorcentajeGarantia = (VBtoConverter.IsNull(dt2.Rows[0]["deu_por_gtz"]) ? 0 : (double)dt2.Rows[0]["deu_por_gtz"]);
                                MontoGarantizado = (VBtoConverter.IsNull(dt2.Rows[0]["deu_mnt_gtz"]) ? 0 : (double)dt2.Rows[0]["deu_mnt_gtz"]);
                            }
                        }
                        mfEsNuevo = false;
                        success = 0;
                    }
                }
                else
                {
                    //modLCEData.GenericError("Cotizacion.Obtener", Information.Err().Number, Information.Err().Description);
                    errlist.AddRange(db.errlist);
                    success = 4;
                }
            }
            return success;
        }

        public int Numero
        {
            get
            {
                int Numero = 0;
                Numero = mvarNumero;
                return Numero;
            }

            set
            {
                mvarNumero = value;
            }
        }




        public int NroCotizacionAjuste
        {
            get
            {
                int NroCotizacionAjuste = 0;
                NroCotizacionAjuste = mvarNroCotizacionAjuste;
                return NroCotizacionAjuste;
            }

            set
            {
                mvarNroCotizacionAjuste = value;
            }
        }



        public int CodigoProductoLCE
        {
            get
            {
                int CodigoProductoLCE = 0;
                CodigoProductoLCE = mvarCodigoProductoLCE;
                return CodigoProductoLCE;
            }

            set
            {
                mvarCodigoProductoLCE = value;
            }
        }



        public string NumeroPersona
        {
            get
            {
                string NumeroPersona = "";
                NumeroPersona = mvarNumeroPersona;
                return NumeroPersona;
            }

            set
            {
                mvarNumeroPersona = value;
            }
        }



        public short ProductoServicio
        {
            get
            {
                short ProductoServicio = 0;
                ProductoServicio = (short)mvarProductoServicio;
                return ProductoServicio;
            }

            set
            {
                mvarProductoServicio = value;
            }
        }



        public string NumeroOperacion
        {
            get
            {
                string NumeroOperacion = "";
                NumeroOperacion = mvarNumeroOperacion;
                return NumeroOperacion;
            }

            set
            {
                mvarNumeroOperacion = value;
            }
        }



        public string NumeroFuncionario
        {
            get
            {
                string NumeroFuncionario = "";
                NumeroFuncionario = mvarNumeroFuncionario;
                return NumeroFuncionario;
            }

            set
            {
                mvarNumeroFuncionario = value;
            }
        }



        public short CodigoMoneda
        {
            get
            {
                short CodigoMoneda = 0;
                CodigoMoneda = (short)mvarCodigoMoneda;
                return CodigoMoneda;
            }

            set
            {
                mvarCodigoMoneda = value;
            }
        }


        public short CodigoMonedaComision
        {
            get
            {
                short CodigoMonedaComision = 0;
                CodigoMonedaComision = (short)mvarCodigoMonedaComision;
                return CodigoMonedaComision;
            }

            set
            {
                mvarCodigoMonedaComision = value;
            }
        }




        public short CodigoEstado
        {
            get
            {
                short CodigoEstado = 0;
                CodigoEstado = (short)mvarCodigoEstado;
                return CodigoEstado;
            }

            set
            {
                mvarCodigoEstado = value;
            }
        }



        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public short CodigoTipoTasa
        {
            get
            {
                short CodigoTipoTasa = 0;
                CodigoTipoTasa = (short)mvarCodigoTipoTasa;
                return CodigoTipoTasa;
            }

            set
            {
                mvarCodigoTipoTasa = value;
            }
        }



        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public short CodigoTipoFinanc
        {
            get
            {
                short CodigoTipoFinanc = 0;
                CodigoTipoFinanc = (short)mvarCodigoTipoFinanc;
                return CodigoTipoFinanc;
            }

            set
            {
                mvarCodigoTipoFinanc = value;
            }
        }



        public double Monto
        {
            get
            {
                double Monto = 0;
                Monto = mvarMonto;
                return Monto;
            }

            set
            {
                mvarMonto = value;
            }
        }



        public double Tasa
        {
            get
            {
                double Tasa = 0;
                Tasa = mvarTasa;
                return Tasa;
            }

            set
            {
                mvarTasa = value;
            }
        }



        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public short DiasPlazo
        {
            get
            {
                short DiasPlazo = 0;
                DiasPlazo = (short)mvarDiasPlazo;
                return DiasPlazo;
            }

            set
            {
                mvarDiasPlazo = value;
            }
        }



        public DateTime FechaIngreso
        {
            get
            {
                DateTime FechaIngreso = System.DateTime.Now;
                FechaIngreso = mvarFechaIngreso;
                return FechaIngreso;
            }

            set
            {
                mvarFechaIngreso = value;
            }
        }



        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public short Vigencia
        {
            get
            {
                short Vigencia = 0;
                Vigencia = (short)mvarVigencia;
                return Vigencia;
            }

            set
            {
                mvarVigencia = value;
            }
        }



        public DateTime FechaCurse
        {
            get
            {
                DateTime FechaCurse = System.DateTime.Now;
                FechaCurse = mvarFechaCurse;
                return FechaCurse;
            }

            set
            {
                mvarFechaCurse = value;
            }
        }



        public DateTime FechaCurseOriginal
        {
            get
            {
                DateTime FechaCurseOriginal = System.DateTime.Now;
                FechaCurseOriginal = mvarFechaCurseOriginal;
                return FechaCurseOriginal;
            }

            set
            {
                mvarFechaCurseOriginal = value;
            }
        }



        public DateTime FechaDesembolso
        {
            get
            {
                DateTime FechaDesembolso = System.DateTime.Now;
                FechaDesembolso = mvarFechaDesembolso;
                return FechaDesembolso;
            }

            set
            {
                mvarFechaDesembolso = value;
            }
        }



        public DateTime FechaUltimoEstado
        {
            get
            {
                DateTime FechaUltimoEstado = System.DateTime.Now;
                FechaUltimoEstado = mvarFechaUltimoEstado;
                return FechaUltimoEstado;
            }

            set
            {
                mvarFechaUltimoEstado = value;
            }
        }




        public DateTime FechaVencimiento
        {
            get
            {
                DateTime FechaVencimiento = System.DateTime.Now;
                FechaVencimiento = mvarFechaVencimiento;
                return FechaVencimiento;
            }

            set
            {
                mvarFechaVencimiento = value;
            }
        }



        public double ValorComision
        {
            get
            {
                double ValorComision = 0;
                ValorComision = mvarValorComision;
                return ValorComision;
            }

            set
            {
                mvarValorComision = value;
            }
        }



        public double PorcentajeComision
        {
            get
            {
                double PorcentajeComision = 0;
                PorcentajeComision = mvarPorcentajeComision;
                return PorcentajeComision;
            }

            set
            {
                mvarPorcentajeComision = value;
            }
        }



        public double TasaTransferencia
        {
            get
            {
                double TasaTransferencia = 0;
                TasaTransferencia = mvarTasaTransferencia;
                return TasaTransferencia;
            }

            set
            {
                mvarTasaTransferencia = value;
            }
        }



        public double Intereses
        {
            get
            {
                double Intereses = 0;
                Intereses = mvarIntereses;
                return Intereses;
            }

            set
            {
                mvarIntereses = value;
            }
        }



        public string Glosa
        {
            get
            {
                string Glosa = "";
                Glosa = mvarGlosa;
                return Glosa;
            }

            set
            {
                mvarGlosa = value;
            }
        }



        public string Importador
        {
            get
            {
                string Importador = "";
                Importador = mvarImportador;
                return Importador;
            }

            set
            {
                mvarImportador = value;
            }
        }



        public string Exportador
        {
            get
            {
                string Exportador = "";
                Exportador = mvarExportador;
                return Exportador;
            }

            set
            {
                mvarExportador = value;
            }
        }



        public DateTime FechaEmbarque
        {
            get
            {
                DateTime FechaEmbarque = System.DateTime.Now;
                FechaEmbarque = mvarFechaEmbarque;
                return FechaEmbarque;
            }

            set
            {
                mvarFechaEmbarque = value;
            }
        }



        public string Mercaderia
        {
            get
            {
                string Mercaderia = "";
                Mercaderia = mvarMercaderia;
                return Mercaderia;
            }

            set
            {
                mvarMercaderia = value;
            }
        }


        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public short AfectaCapitulo3b5
        {
            get
            {
                short AfectaCapitulo3b5 = 0;
                AfectaCapitulo3b5 = (short)mvarAfectaCapitulo3b5;
                return AfectaCapitulo3b5;
            }

            set
            {
                mvarAfectaCapitulo3b5 = value;
            }
        }



        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public short CodigoTipoCredito
        {
            get
            {
                short CodigoTipoCredito = 0;
                CodigoTipoCredito = (short)miTipoCredito;
                return CodigoTipoCredito;
            }

            set
            {
                miTipoCredito = value;
            }
        }



        public double PorcentajeTolerancia
        {
            get
            {
                double PorcentajeTolerancia = 0;
                PorcentajeTolerancia = mdProcentajeTolerancia;
                return PorcentajeTolerancia;
            }

            set
            {
                mdProcentajeTolerancia = value;
            }
        }



        public double PorcentajeGarantia
        {
            get
            {
                double PorcentajeGarantia = 0;
                PorcentajeGarantia = mdProcentajeGarantia;
                return PorcentajeGarantia;
            }

            set
            {
                mdProcentajeGarantia = value;
            }
        }


        public double MontoGarantizado
        {
            get
            {
                double MontoGarantizado = 0;
                MontoGarantizado = mdMontoGarantizado;
                return MontoGarantizado;
            }

            set
            {
                mdMontoGarantizado = value;
            }
        }




        public double MontoOperacion
        {
            get
            {
                double MontoOperacion = 0;
                MontoOperacion = mdMontoOperacion;
                return MontoOperacion;
            }

            set
            {
                mdMontoOperacion = value;
            }
        }




        public double MontoDolares
        {
            get
            {
                double MontoDolares = 0;
                MontoDolares = mvarMontoDolares;
                return MontoDolares;
            }

            set
            {
                mvarMontoDolares = value;
            }
        }



        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public short DiasPlazoTasa
        {
            get
            {
                short DiasPlazoTasa = 0;
                DiasPlazoTasa = (short)mvarDiasPlazoTasa;
                return DiasPlazoTasa;
            }

            set
            {
                mvarDiasPlazoTasa = value;
            }
        }



        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public short CodigoMonedaTasa
        {
            get
            {
                short CodigoMonedaTasa = 0;
                CodigoMonedaTasa = (short)mvarCodigoMonedaTasa;
                return CodigoMonedaTasa;
            }

            set
            {
                mvarCodigoMonedaTasa = value;
            }
        }



        public string TipoProducto
        {
            get
            {
                string TipoProducto = "";
                TipoProducto = mvarTipoProducto;
                return TipoProducto;
            }

            set
            {
                mvarTipoProducto = value;
            }
        }



        public double ValorTasaBase
        {
            get
            {
                double ValorTasaBase = 0;
                ValorTasaBase = mvarValorTasaBase;
                return ValorTasaBase;
            }

            set
            {
                mvarValorTasaBase = value;
            }
        }



        public bool FlagCargoCasaMatriz
        {
            get
            {
                bool FlagCargoCasaMatriz = false;
                FlagCargoCasaMatriz = mvarFlagCargoCasaMatriz;
                return FlagCargoCasaMatriz;
            }

            set
            {
                mvarFlagCargoCasaMatriz = value;
            }
        }



        public bool FlagCargoCasaMatrizSuby
        {
            get
            {
                bool FlagCargoCasaMatrizSuby = false;
                FlagCargoCasaMatrizSuby = mvarFlagCargoCasaMatrizSuby;
                return FlagCargoCasaMatrizSuby;
            }

            set
            {
                mvarFlagCargoCasaMatrizSuby = value;
            }
        }




        public int PlazoMaximoResidualContingente
        {
            get
            {
                int PlazoMaximoResidualContingente = 0;
                PlazoMaximoResidualContingente = miPlazoMaximoResidualContingente;
                return PlazoMaximoResidualContingente;
            }

            set
            {
                miPlazoMaximoResidualContingente = value;
            }
        }



        public int PlazoEfectivoSinUsoFondos
        {
            get
            {
                int PlazoEfectivoSinUsoFondos = 0;
                PlazoEfectivoSinUsoFondos = miPlazoEfectivoSinUsoFondos;
                return PlazoEfectivoSinUsoFondos;
            }

            set
            {
                miPlazoEfectivoSinUsoFondos = value;
            }
        }



        public int PlazoEfectivoConUsoFondos
        {
            get
            {
                int PlazoEfectivoConUsoFondos = 0;
                PlazoEfectivoConUsoFondos = miPlazoEfectivoConUsoFondos;
                return PlazoEfectivoConUsoFondos;
            }

            set
            {
                miPlazoEfectivoConUsoFondos = value;
            }
        }



        public enumGastosPorCuentaDe GastosPorCuentaDe
        {
            get
            {
                enumGastosPorCuentaDe GastosPorCuentaDe = (enumGastosPorCuentaDe)0;
                GastosPorCuentaDe = (enumGastosPorCuentaDe)miGastosPorCuentaDe;
                return GastosPorCuentaDe;
            }

            set
            {
                miGastosPorCuentaDe = (int)value;
            }
        }



        public int CodigoTasaBasePagoAnticipado
        {
            get
            {
                int CodigoTasaBasePagoAnticipado = 0;
                CodigoTasaBasePagoAnticipado = miCodigoTasaBasePagoAnticipado;
                return CodigoTasaBasePagoAnticipado;
            }

            set
            {
                miCodigoTasaBasePagoAnticipado = value;
            }
        }



        public double ValorTasaBasePagoAnticipado
        {
            get
            {
                double ValorTasaBasePagoAnticipado = 0;
                ValorTasaBasePagoAnticipado = miValorTasaBasePagoAnticipado;
                return ValorTasaBasePagoAnticipado;
            }

            set
            {
                miValorTasaBasePagoAnticipado = value;
            }
        }



        public double ValorSpreadPagoAnticipado
        {
            get
            {
                double ValorSpreadPagoAnticipado = 0;
                ValorSpreadPagoAnticipado = miValorSpreadPagoAnticipado;
                return ValorSpreadPagoAnticipado;
            }

            set
            {
                miValorSpreadPagoAnticipado = value;
            }
        }



        public double ValorTasaPagoAnticipado
        {
            get
            {
                double ValorTasaPagoAnticipado = 0;
                ValorTasaPagoAnticipado = miValorTasaPagoAnticipado;
                return ValorTasaPagoAnticipado;
            }

            set
            {
                miValorTasaPagoAnticipado = value;
            }
        }




        public int CodigoTasaBasePrestamo
        {
            get
            {
                int CodigoTasaBasePrestamo = 0;
                CodigoTasaBasePrestamo = miCodigoTasaBasePrestamo;
                return CodigoTasaBasePrestamo;
            }

            set
            {
                miCodigoTasaBasePrestamo = value;
            }
        }



        public double ValorTasaBasePrestamo
        {
            get
            {
                double ValorTasaBasePrestamo = 0;
                ValorTasaBasePrestamo = miValorTasaBasePrestamo;
                return ValorTasaBasePrestamo;
            }

            set
            {
                miValorTasaBasePrestamo = value;
            }
        }



        public double ValorSpreadPrestamo
        {
            get
            {
                double ValorSpreadPrestamo = 0;
                ValorSpreadPrestamo = miValorSpreadPrestamo;
                return ValorSpreadPrestamo;
            }

            set
            {
                miValorSpreadPrestamo = value;
            }
        }



        public double ValorTasaPrestamo
        {
            get
            {
                double ValorTasaPrestamo = 0;
                ValorTasaPrestamo = miValorTasaPrestamo;
                return ValorTasaPrestamo;
            }

            set
            {
                miValorTasaPrestamo = value;
            }
        }



        public int CodigoMensajeValidacionPlazos
        {
            get
            {
                int CodigoMensajeValidacionPlazos = 0;
                CodigoMensajeValidacionPlazos = miCodigoMensajeValidacionPlazos;
                return CodigoMensajeValidacionPlazos;
            }

            set
            {
                miCodigoMensajeValidacionPlazos = value;
            }
        }



        public int CodigoTipoReembolso
        {
            get
            {
                int CodigoTipoReembolso = 0;
                CodigoTipoReembolso = miCodigoTipoReembolso;
                return CodigoTipoReembolso;
            }

            set
            {
                miCodigoTipoReembolso = value;
            }
        }



        public DateTime FechaContraGarantia
        {
            get
            {
                DateTime FechaContraGarantia = System.DateTime.Now;
                FechaContraGarantia = mFechaContraGarantia;
                return FechaContraGarantia;
            }

            set
            {
                mFechaContraGarantia = value;
            }
        }




        // VBto upgrade warning: vParam As object	OnWrite(int, string, double, DateTime, bool, enumGastosPorCuentaDe, object)
        private void AddParam(ref string sText, object vParam)
        {

            string ltSep = "";
            string ltString = "";

            if (sText.Length > 0)
            {
                ltSep = ", ";
            }
            else
            {
                ltSep = string.Empty;
            }


            if (vParam.GetType().ToString() == "String")
            {
                sText += ltSep + "'" + Global.qrst(vParam.ToString()) + "'";
            }
            else if (vParam.GetType().ToString() == "Integer" || vParam.GetType().ToString() == "Long")
            {
                sText += ltSep + vParam.ToString();
            }
            else if (vParam.GetType().ToString() == "Double" || vParam.GetType().ToString() == "Float")
            {
                sText += ltSep + Global.ConvertirComaPorPunto(Double.Parse(vParam.ToString()).ToString("N4"));
            }
            else if (vParam.GetType().ToString() == "Date")
            {
                if (!(Math.Abs(DateAndTime.DateDiff("yyyy", DateAndTime.TimeSerial(0, 0, 0), (DateTime)vParam, FirstDayOfWeek.System, FirstWeekOfYear.System)) > 2))
                {
                    // If IsNull(vParam) Or vParam = CDate(0) Then
                    sText += ltSep + "null";
                }
                else
                {
                    sText += ltSep + "'" + ((DateTime)vParam).ToString("yyyy/mm/dd") + "'";
                }
            }
            else if (vParam.GetType().ToString() == "Boolean")
            {
                sText += ltSep + (Convert.ToBoolean(vParam) ? "1" : "0");
            }
            else if (vParam.GetType().ToString() == "Null")
            {
                sText += ltSep + "Null";
            }
            else
            {
                //TODO MessageBox.Show("Tipo ["+Information.TypeName(vParam)+"] no soportado.");
            }

        }
        public DataTable GetListCodigoReembolso(ref short success)
        {
            // Descripción : Obtiene una lista de todas las cotizaciones
            // Parámetros  : Ninguno
            // Retorno     : 0 OK
            // 3 No existe
            // 4 Error en la base de datos
            // E. laterales: Ninguno
            // 
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================
            string ltConsulta;
            ltConsulta = "exec svc_lce_lst_tip_cod_ree";
            DataTable rec;
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                rec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count == 0)
                    {
                        success = 3;
                    }
                    else
                    {
                        success = 0;
                    }
                }
                else
                {
                    //modLCEData.GenericError("GetListCodigoReembolso.Getlist", Information.Err().Number, Information.Err().Description);
                    errlist.AddRange(db.errlist);
                    success = 4;

                }
            }
            return rec;

        }

        // sfv v3
        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public short EsNotaEstructurada
        {
            get
            {
                short EsNotaEstructurada = 0;
                EsNotaEstructurada = (short)mvarEsNotaEstructurada;
                return EsNotaEstructurada;
            }

            set
            {
                mvarEsNotaEstructurada = value;
            }
        }


        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public short EsOperacionPuntual
        {
            get
            {
                short EsOperacionPuntual = 0;
                EsOperacionPuntual = (short)mvarEsOperacionPuntual;
                return EsOperacionPuntual;
            }

            set
            {
                mvarEsOperacionPuntual = value;
            }
        }



        public string NumeroPersonaSuby
        {
            get
            {
                string NumeroPersonaSuby = "";
                NumeroPersonaSuby = mvarNumeroPersonaSuby;
                return NumeroPersonaSuby;
            }

            set
            {
                mvarNumeroPersonaSuby = value;
            }
        }



        public string TipoProductoSuby
        {
            get
            {
                string TipoProductoSuby = "";
                TipoProductoSuby = mvarTipoProductoSuby;
                return TipoProductoSuby;
            }

            set
            {
                mvarTipoProductoSuby = value;
            }
        }


        public int CodigoProductoLCESuby
        {
            get
            {
                int CodigoProductoLCESuby = 0;
                CodigoProductoLCESuby = mvarCodigoProductoLCESuby;
                return CodigoProductoLCESuby;
            }

            set
            {
                mvarCodigoProductoLCESuby = value;
            }
        }



        public string SwiftBanco
        {
            get
            {
                string SwiftBanco = "";
                SwiftBanco = mvarSwiftBanco;
                return SwiftBanco;
            }

            set
            {
                mvarSwiftBanco = value;
            }
        }


        // 
        public DataTable GetFechaOriginalAjuste(int numCoti, ref short success)
        {

            DataTable rec;
            String ltConsulta = "exec svc_cle_cna_fec_ope_ori " + Convert.ToString(numCoti);
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                rec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count == 0)
                    {
                        success = 3;
                    }
                    else
                    {
                        success = 0;
                    }
                }
                else
                {
                    //modLCEData.GenericError("CotizacionCLE.GetFechaOriginalAjuste", Information.Err().Number, Information.Err().Description);
                    errlist.AddRange(db.errlist);
                    success = 4;
                }
            }
            return rec;

        }
        public bool ExisteOperacionenDia()
        {
            string ltConsulta;
            bool success = false;
            ltConsulta = "exec svc_cle_cna_exi_ope_ori " + Convert.ToString(mvarNumero);
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataTable precCotizacion = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (precCotizacion.Rows.Count == 0)
                    {
                        success = false;
                    }
                    else
                    {
                        success = true;
                    }
                }
                else
                {
                    //modLCEData.GenericError("CotizacionCLE.ExisteOperacionenDia", Information.Err().Number, Information.Err().Description);
                    errlist.AddRange(db.errlist);
                    success = false;

                }
            }
            return success;

        }


    }
}