using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Tools;
using Modelos.Library;
namespace Modelos
{
    public class Limite
    {
        public Limite(String ConexionString)
            : base()
        {
            dataConnectionString = ConexionString;
        }
        private String dataConnectionString;

        //=========================================================

        // variables locales que contienen valores de propiedad
        private int mvarCodigo;
        private int mvarCorrelativo;
        private DateTime mvarFechaOtorgamiento;
        private string mvarNombre = "";
        private int mvarCodigoPais;
        private int mvarCodigoDesarrollo;
        private int mvarCodigoRegion;
        private int mvarCodigoGrupo;
        private string mvarCodigoTipoProducto = "";
        private int mvarCodigoTipoLimite;
        private int mvarCodigoGrupoProducto;
        private string mvarNumeroCliente = "";
        private DateTime mvarFechaVencimiento;
        private double mvarMontoOtorgado;
        private double mvarMontoUtilizado;
        private double mvarMontoDisponible;
        private double mvarMontoReservado;
        private string mvarFormula = "";
        // VBto upgrade warning: mvarEsNormativo As int	OnWrite(bool, int)
        private int mvarEsNormativo;

        public string GlosaAcuerdo = "";
        public int Moneda;
        public bool mfEsNuevo; // Indica si es Nuevo o existente
        // afecta40
        private string mvarPlazoUnidad = "";
        private int mvarPlazoCantidad;
        private int mvarPlazoAbilitado;


        public object BuidInfo()
        {
            object BuidInfo = null;
            // VBto upgrade warning: ArrInfo As object(,)	OnWrite(string(), int(), DateTime(), double())
            object[,] ArrInfo;
            ArrInfo = new object[1 + 1, 20 + 1];

            ArrInfo[0, 0] = "Codigo"; ArrInfo[1, 0] = mvarCodigo;
            ArrInfo[0, 1] = "Correlativo"; ArrInfo[1, 1] = mvarCorrelativo;
            ArrInfo[0, 2] = "FechaOtorgamiento"; ArrInfo[1, 2] = mvarFechaOtorgamiento;
            ArrInfo[0, 3] = "Nombre"; ArrInfo[1, 3] = mvarNombre;
            ArrInfo[0, 4] = "CodigoPais"; ArrInfo[1, 4] = (mvarCodigoPais == 0 ? -1 : mvarCodigoPais);
            ArrInfo[0, 5] = "CodigoDesarrollo"; ArrInfo[1, 5] = (mvarCodigoDesarrollo == 0 ? -1 : mvarCodigoDesarrollo);
            ArrInfo[0, 6] = "CodigoRegion"; ArrInfo[1, 6] = (mvarCodigoRegion == 0 ? -1 : mvarCodigoRegion);
            ArrInfo[0, 7] = "CodigoGrupo"; ArrInfo[1, 7] = (mvarCodigoGrupo == 0 ? -1 : mvarCodigoGrupo);
            ArrInfo[0, 8] = "CodigoTipoProducto"; ArrInfo[1, 8] = (mvarCodigoTipoProducto == string.Empty ? "-1" : mvarCodigoTipoProducto);
            // ArrInfo(0, 9) = "CodigoTipoLimite": ArrInfo(1, 9) = IIf(mvarCodigoTipoLimite = 0, -1, mvarCodigoTipoLimite)
            ArrInfo[0, 10] = "CodigoGrupoProducto"; ArrInfo[1, 10] = (mvarCodigoGrupoProducto == 0 ? -1 : mvarCodigoGrupoProducto);
            ArrInfo[0, 11] = "NumeroCliente"; ArrInfo[1, 11] = (mvarNumeroCliente == string.Empty ? "-1" : mvarNumeroCliente);
            ArrInfo[0, 12] = "FechaVencimiento"; ArrInfo[1, 12] = mvarFechaVencimiento;
            ArrInfo[0, 13] = "MontoOtorgado"; ArrInfo[1, 13] = mvarMontoOtorgado;
            ArrInfo[0, 14] = "MontoUtilizado"; ArrInfo[1, 14] = mvarMontoUtilizado;
            ArrInfo[0, 15] = "MontoDisponible"; ArrInfo[1, 15] = mvarMontoDisponible;
            ArrInfo[0, 16] = "MontoReservado"; ArrInfo[1, 16] = mvarMontoReservado;
            ArrInfo[0, 17] = "Formula"; ArrInfo[1, 17] = mvarFormula;
            ArrInfo[0, 18] = "Normativo"; ArrInfo[1, 18] = mvarEsNormativo;
            ArrInfo[0, 19] = "GlosaAcuerdo"; ArrInfo[1, 19] = GlosaAcuerdo;
            ArrInfo[0, 20] = "Moneda"; ArrInfo[1, 20] = Moneda;

            BuidInfo = ArrInfo;
            return BuidInfo;
        }
        // end afecta

        public DataTable BuscarLimite(ref short suceso, DateTime pxFecha, int piOpt, string psArg)
        {
            // Descripción : Obtiene la lista de limites vigentes según Busqueda
            // Parámetros  : plCodigoLimite, pxFecha
            // Retorno     : 0 OK
            // 3 No existe
            // 4 Error en la base de datos
            // E. laterales: Ninguno
            // 
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================
            string ltConsulta;
            ltConsulta = "exec svc_lim_idv_por_flt '" + pxFecha.ToString("yyyy/mm/dd") + "', " + Convert.ToString(piOpt) + ", '" + Global.qrst(psArg) + "'";
            DataTable rec = new DataTable();
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                rec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count == 0)
                    {
                        suceso = 3;
                    }
                    else
                    {
                        suceso = 0;
                    }
                }
                else
                {
                    suceso = 4;
                    //Global.GenericError("Limite.BuscarLimite", Information.Err().Number, Information.Err().Description);
                }
            }
            return rec;
        }
        public DataTable BuscarLimiteNormativo(ref short suceso, DateTime pxFecha, int piOpt, string psArg)
        {
            // Descripción : Obtiene la lista de limites vigentes según Busqueda
            // Parámetros  : plCodigoLimite, pxFecha
            // Retorno     : 0 OK
            // 3 No existe
            // 4 Error en la base de datos
            // E. laterales: Ninguno
            // 
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================
            string ltConsulta;
            DataTable rec = new DataTable();
            ltConsulta = "exec svc_cle_cns_lim_nor_por_flt '" + pxFecha.ToString("yyyy/mm/dd") + "', " + Convert.ToString(piOpt) + ", '" + Global.qrst(psArg) + "'";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                rec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count == 0)
                    {
                        suceso = 3;
                    }
                    else
                    {
                        suceso = 0;
                    }

                }
                else
                {
                    suceso = 4;
                    //Global.GenericError("Limite.BuscarLimiteNormativo", Information.Err().Number, Information.Err().Description);
                }
            }
            return rec;
        }
        public DataTable BuscarLimitePolitico(ref short suceso, DateTime pxFecha, int piOpt, string psArg)
        {
            // Descripción : Obtiene la lista de limites vigentes según Busqueda
            // Parámetros  : plCodigoLimite, pxFecha
            // Retorno     : 0 OK
            // 3 No existe
            // 4 Error en la base de datos
            // E. laterales: Ninguno
            // 
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================
            string ltConsulta;
            DataTable rec = new DataTable();
            ltConsulta = "exec svc_cle_cns_lim_plc_por_flt '" + pxFecha.ToString("yyyy/mm/dd") + "', " + Convert.ToString(piOpt) + ", '" + Global.qrst(psArg) + "'";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                rec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count == 0)
                    {
                        suceso = 3;
                    }
                    else
                    {
                        suceso = 0;
                    }
                }
                else
                {
                    suceso = 4;
                    //Global.GenericError("Limite.BuscarLimiteNormativo", Information.Err().Number, Information.Err().Description);
                }
            }
            return rec;
        }


        public short Eliminar(int plCodigoLimite, int plCorrelativo, DateTime pxFecha)
        {
            // Descripción : Elimina un limite individual por CodigoLimite,
            // Correlativo y fecha
            // Parámetros  : plCodigoLimite, plCorrelativo, pxFecha
            // Retorno     : 0 OK
            // 3 Error al eliminar
            // E. laterales: Ninguno
            // 
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================
            string ltComando;
            short suceso = 0;
            ltComando = "exec sp_lce_eli_limte_indvd " + Convert.ToString(plCodigoLimite) + "," + Convert.ToString(plCorrelativo) + ",'" + pxFecha.ToString("yyyy/mm/dd") + "'";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                int success = db.ExecuteCommand(ltComando);
                if (db.errlist.Count == 0)
                {
                    suceso = 0;
                }
                else
                {
                    //Global.GenericError("TipoProducto.Eliminar", Information.Err().Number, Information.Err().Description);
                    suceso = 3;

                }

            }
            return suceso;

        }

        public DataTable GetList(ref short suceso, int plCodigoLimite, DateTime pxFecha)
        {
            // Descripción : Obtiene la lista de limites vigentes según estructura
            // Parámetros  : plCodigoLimite, pxFecha
            // Retorno     : 0 OK
            // 3 No existe
            // 4 Error en la base de datos
            // E. laterales: Ninguno
            // 
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================
            string ltConsulta;
            DataTable rec = new DataTable();
            ltConsulta = "exec sp_lce_cna_lst_limte_indvd " + Convert.ToString(plCodigoLimite) + ",'" + pxFecha.ToString("yyyy/mm/dd") + "'";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                rec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count == 0)
                    {
                        suceso = 3;
                    }
                    else
                    {
                        suceso = 0;
                    }
                }
                else
                {
                    suceso = 4;
                    //Global.GenericError("Limite.GetList", Information.Err().Number, Information.Err().Description);
                }
            }
            return rec;
        }

        // VBto upgrade warning: plCodigoLimite As int	OnWrite(VBtoField, int)
        // VBto upgrade warning: plCorrelativo As int	OnWrite(VBtoField, int)
        // VBto upgrade warning: pxFecha As DateTime	OnWrite(VBtoField, DateTime)
        public short Obtener(int plCodigoLimite, int plCorrelativo, DateTime pxFecha)
        {
            // Descripción : Obtiene un limite individual por CodigoLimite,
            // Correlativo y fecha
            // Parámetros  : plCodigoLimite, plCorrelativo, pxFecha
            // Retorno     : 0 OK
            // 3 No existe
            // 4 Error en la BD
            // E. laterales: Ninguno
            // 
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================

            short success;
            string ltConsulta;
            ltConsulta = "exec svc_cle_obt_lim_idv " + Convert.ToString(plCodigoLimite) + "," + Convert.ToString(plCorrelativo) + ",'" + pxFecha.ToString("yyyyMMdd") + "'";

            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataTable rec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count == 0)
                    {
                        success = 3;
                    }
                    else
                    {
                        Codigo = Convert.ToInt32(rec.Rows[0]["cod_limte"]);
                        Correlativo = Convert.ToInt32(rec.Rows[0]["nro_crtvo_limte"]);
                        Nombre = Convert.ToString(rec.Rows[0]["gls_dscrn_limte_indvd"]);
                        FechaOtorgamiento = (DateTime)rec.Rows[0]["fch_otgmt_limte"];
                        if ((short)rec.Rows[0]["cod_pais"] == -1)
                        { CodigoPais = 0; }
                        else
                        {
                            CodigoPais = Convert.ToInt32(rec.Rows[0]["cod_pais"]);
                        }
                        if ((short)rec.Rows[0]["cod_nivel_desrr"] == -1)
                        {
                            CodigoDesarrollo = 0;
                        }
                        else
                        {
                            CodigoDesarrollo = Convert.ToInt32(rec.Rows[0]["cod_nivel_desrr"]);
                        }
                        if ((short)rec.Rows[0]["cod_rgion_geogr"] == -1)
                        {
                            CodigoRegion = 0;
                        }
                        else
                        {
                            CodigoRegion = Convert.ToInt16(rec.Rows[0]["cod_rgion_geogr"]);
                        }
                        if ((short)rec.Rows[0]["cod_grupo_econm"] == -1)
                        {
                            CodigoGrupo = 0;
                        }
                        else
                        {
                            CodigoGrupo = Convert.ToInt32(rec.Rows[0]["cod_grupo_econm"]);
                        }
                        if (rec.Rows[0]["cod_tipo_ctzcn_lcext"] == "-1")
                        {
                            CodigoTipoProducto = string.Empty;
                        }
                        else
                        {
                            CodigoTipoProducto = Convert.ToString(rec.Rows[0]["cod_tipo_ctzcn_lcext"]) + "";
                        }
                        CodigoTipoLimite = Convert.ToInt16(rec.Rows[0]["cod_tipo_limte"]);
                        if (Convert.ToString(rec.Rows[0]["nro_prsna"]).Trim() == "-1")
                        {
                            NumeroCliente = string.Empty;
                        }
                        else
                        {
                            NumeroCliente = Convert.ToString(rec.Rows[0]["nro_prsna"]);
                        }
                        if ((short)rec.Rows[0]["cod_grupo_tipo_ctzcn_lcext"] == -1)
                        {
                            CodigoGrupoProducto = 0;
                        }
                        else
                        {
                            CodigoGrupoProducto = Convert.ToInt16(rec.Rows[0]["cod_grupo_tipo_ctzcn_lcext"]);
                        }
                        EsNormativo = Convert.ToInt32(rec.Rows[0]["pge_rgl_lim"]);

                        FechaVencimiento = (DateTime)rec.Rows[0]["fch_vncto_limte"];
                        MontoOtorgado = Convert.ToDouble((rec.Rows[0]["mnt_otgmt_limte"] == null) ? 0 : rec.Rows[0]["mnt_otgmt_limte"]);
                        MontoUtilizado = Convert.ToDouble((rec.Rows[0]["mnt_utlzd_limte"] == null) ? 0 : rec.Rows[0]["mnt_utlzd_limte"]);
                        MontoDisponible = Convert.ToDouble((rec.Rows[0]["mnt_dispo_limte"] == null) ? 0 : rec.Rows[0]["mnt_dispo_limte"]);
                        MontoReservado = Convert.ToDouble((rec.Rows[0]["mnt_rsrvd_limte"] == null) ? 0 : rec.Rows[0]["mnt_rsrvd_limte"]);
                        GlosaAcuerdo = string.Empty + Convert.ToString(rec.Rows[0]["lim_gls_acu"]);
                        Moneda = Convert.ToInt32((rec.Rows[0]["lim_mon_lim"] == null) ? 13 : rec.Rows[0]["lim_mon_lim"]);
                        success = 0;
                        mfEsNuevo = false;
                    }
                }
                else
                {
                    //Global.GenericError("Limite.Obtener", Information.Err().Number, Information.Err().Description);
                    success = 4;

                }
            }
            return success;
        }
        // VBto upgrade warning: ptRutCliente As string	OnWrite(VBtoField, string, VB.TextBox)
        public short ObtenerLineaCliente(string ptRutCliente)
        {
            // Descripción : Obtiene La Linea Base del Cliente,
            // Parámetros  : ptRutCliente
            // Retorno     : 0 OK
            // 3 No existe
            // 4 Error en la BD
            short success;
            string ltConsulta;
            ltConsulta = "exec svc_cle_cna_lin_bas_cli '" + Global.ConvertirRutNro(ptRutCliente) + "'";

            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataTable rec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count == 0)
                    {
                        success = 3;
                    }
                    else
                    {
                        Codigo = Convert.ToInt32(rec.Rows[0]["cod_limte"]);
                        Correlativo = Convert.ToInt32(rec.Rows[0]["nro_crtvo_limte"]);
                        Nombre = Convert.ToString(rec.Rows[0]["gls_dscrn_limte_indvd"]);
                        FechaOtorgamiento = (DateTime)rec.Rows[0]["fch_otgmt_limte"];
                        if ((short)rec.Rows[0]["cod_pais"] == -1)
                        { CodigoPais = 0; }
                        else
                        {
                            CodigoPais = Convert.ToInt32(rec.Rows[0]["cod_pais"]);
                        }
                        if ((short)rec.Rows[0]["cod_nivel_desrr"] == -1)
                        {
                            CodigoDesarrollo = 0;
                        }
                        else
                        {
                            CodigoDesarrollo = Convert.ToInt32(rec.Rows[0]["cod_nivel_desrr"]);
                        }
                        if ((short)rec.Rows[0]["cod_rgion_geogr"] == -1)
                        {
                            CodigoRegion = 0;
                        }
                        else
                        {
                            CodigoRegion = Convert.ToInt16(rec.Rows[0]["cod_rgion_geogr"]);
                        }
                        if (rec.Rows[0]["cod_tipo_ctzcn_lcext"] == "-1")
                        {
                            CodigoTipoProducto = string.Empty;
                        }
                        else
                        {
                            CodigoTipoProducto = Convert.ToString(rec.Rows[0]["cod_tipo_ctzcn_lcext"]) + "";
                        }
                        CodigoTipoLimite = Convert.ToInt16(rec.Rows[0]["cod_tipo_limte"]);
                        if (Convert.ToString(rec.Rows[0]["nro_prsna"]).Trim() == "-1")
                        {
                            NumeroCliente = string.Empty;
                        }
                        else
                        {
                            NumeroCliente = Convert.ToString(rec.Rows[0]["nro_prsna"]);
                        }
                        if ((short)rec.Rows[0]["cod_grupo_tipo_ctzcn_lcext"] == -1)
                        {
                            CodigoGrupoProducto = 0;
                        }
                        else
                        {
                            CodigoGrupoProducto = Convert.ToInt16(rec.Rows[0]["cod_grupo_tipo_ctzcn_lcext"]);
                        }

                        if ((short)rec.Rows[0]["cod_grupo_econm"] == -1)
                        {
                            CodigoGrupo = 0;
                        }
                        else
                        {
                            CodigoGrupo = Convert.ToInt32(rec.Rows[0]["cod_grupo_econm"]);
                        }

                         MontoOtorgado = (VBtoConverter.IsNull(rec.Rows[0]["mnt_otgmt_limte"]) ? 0D : Convert.ToDouble(rec.Rows[0]["mnt_otgmt_limte"]));
                       FechaVencimiento = (DateTime)rec.Rows[0]["fch_vncto_limte"];
                        MontoUtilizado = (VBtoConverter.IsNull(rec.Rows[0]["mnt_utlzd_limte"]) ? 0D : Convert.ToDouble(rec.Rows[0]["mnt_utlzd_limte"]));
                        MontoDisponible = (VBtoConverter.IsNull(rec.Rows[0]["mnt_dispo_limte"]) ? 0D : Convert.ToDouble(rec.Rows[0]["mnt_dispo_limte"]));
                        MontoReservado = (VBtoConverter.IsNull(rec.Rows[0]["mnt_rsrvd_limte"]) ? 0D : Convert.ToDouble(rec.Rows[0]["mnt_rsrvd_limte"]));
                        Formula = Convert.ToString(rec.Rows[0]["lim_gls_frm_clc"]);
                        Normativo = Convert.ToBoolean((VBtoConverter.IsNull(rec.Rows[0]["Pge_rgl_lim"]) ? 0 : rec.Rows[0]["Pge_rgl_lim"]));
                        GlosaAcuerdo = Convert.ToString(rec.Rows[0]["lim_gls_acu"]);
                        Moneda = (VBtoConverter.IsNull(rec.Rows[0]["lim_mon_lim"]) ? 13 : Convert.ToInt32(rec.Rows[0]["lim_mon_lim"]));

                        success = 0;
                        mfEsNuevo = false;
                    }
                }
                else
                {
                    //Global.GenericError("Limite.ObtenerLineaCliente", Information.Err().Number, Information.Err().Description);
                    success = 4;

                }
            }
            return success;
        }


        public int Codigo
        {
            get
            {
                int Codigo = 0;
                Codigo = mvarCodigo;
                return Codigo;
            }

            set
            {
                mvarCodigo = value;
            }
        }



        public int Correlativo
        {
            get
            {
                int Correlativo = 0;
                Correlativo = mvarCorrelativo;
                return Correlativo;
            }

            set
            {
                mvarCorrelativo = value;
            }
        }



        public DateTime FechaOtorgamiento
        {
            get
            {
                DateTime FechaOtorgamiento = System.DateTime.Now;
                FechaOtorgamiento = mvarFechaOtorgamiento;
                return FechaOtorgamiento;
            }

            set
            {
                mvarFechaOtorgamiento = value;
            }
        }



        public string Nombre
        {
            get
            {
                string Nombre = "";
                Nombre = mvarNombre;
                return Nombre;
            }

            set
            {
                mvarNombre = value;
            }
        }



        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public int CodigoPais
        {
            get
            {
                int CodigoPais = 0;
                CodigoPais = mvarCodigoPais;
                return CodigoPais;
            }

            set
            {
                mvarCodigoPais = value;
            }
        }



        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public int CodigoDesarrollo
        {
            get
            {
                int CodigoDesarrollo = 0;
                CodigoDesarrollo = mvarCodigoDesarrollo;
                return CodigoDesarrollo;
            }

            set
            {
                mvarCodigoDesarrollo = value;
            }
        }



        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public short CodigoRegion
        {
            get
            {
                short CodigoRegion = 0;
                CodigoRegion = (short)mvarCodigoRegion;
                return CodigoRegion;
            }

            set
            {
                mvarCodigoRegion = value;
            }
        }



        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public int CodigoGrupo
        {
            get
            {
                int CodigoGrupo = 0;
                CodigoGrupo = mvarCodigoGrupo;
                return CodigoGrupo;
            }

            set
            {
                mvarCodigoGrupo = value;
            }
        }



        public string CodigoTipoProducto
        {
            get
            {
                string CodigoTipoProducto = "";
                CodigoTipoProducto = mvarCodigoTipoProducto;
                return CodigoTipoProducto;
            }

            set
            {
                mvarCodigoTipoProducto = value;
            }
        }



        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public short CodigoTipoLimite
        {
            get
            {
                short CodigoTipoLimite = 0;
                CodigoTipoLimite = (short)mvarCodigoTipoLimite;
                return CodigoTipoLimite;
            }

            set
            {
                mvarCodigoTipoLimite = value;
            }
        }



        public string NumeroCliente
        {
            get
            {
                string NumeroCliente = "";
                NumeroCliente = mvarNumeroCliente;
                return NumeroCliente;
            }

            set
            {
                mvarNumeroCliente = value;
            }
        }



        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public short CodigoGrupoProducto
        {
            get
            {
                short CodigoGrupoProducto = 0;
                CodigoGrupoProducto = (short)mvarCodigoGrupoProducto;
                return CodigoGrupoProducto;
            }

            set
            {
                mvarCodigoGrupoProducto = value;
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



        public double MontoOtorgado
        {
            get
            {
                double MontoOtorgado = 0;
                MontoOtorgado = mvarMontoOtorgado;
                return MontoOtorgado;
            }

            set
            {
                mvarMontoOtorgado = value;
            }
        }



        public double MontoUtilizado
        {
            get
            {
                double MontoUtilizado = 0;
                MontoUtilizado = mvarMontoUtilizado;
                return MontoUtilizado;
            }

            set
            {
                mvarMontoUtilizado = value;
            }
        }



        public double MontoDisponible
        {
            get
            {
                double MontoDisponible = 0;
                MontoDisponible = mvarMontoDisponible;
                return MontoDisponible;
            }

            set
            {
                mvarMontoDisponible = value;
            }
        }



        public double MontoReservado
        {
            get
            {
                double MontoReservado = 0;
                MontoReservado = mvarMontoReservado;
                return MontoReservado;
            }

            set
            {
                mvarMontoReservado = value;
            }
        }



        public Limite()
            : base()
        {
            mfEsNuevo = false;
        }


        public short Ingresar()
        {
            // Descripción : Ingresa un Límite Individual
            // Parámetros  : Ninguno
            // Retorno     : 0 OK
            // 3 Error al ingresar
            // E. laterales: Ninguno
            // 
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================
            string ltComando;
            string ltCodigoPais = "";
            string ltCodigoDesarrollo = "";
            string ltCodigoRegion = "";
            string ltCodigoGrupo = "";
            string ltCodigoGrupoProducto = "";
            string ltNumeroCliente = "";
            string ltCodigoTipoProducto = "";
            if (CodigoPais == 0)
            {
                ltCodigoPais = "-1";
            }
            else
            {
                ltCodigoPais = CodigoPais.ToString();
            }
            if (CodigoDesarrollo == 0)
            {
                ltCodigoDesarrollo = "-1";
            }
            else
            {
                ltCodigoDesarrollo = CodigoDesarrollo.ToString();
            }
            if (CodigoRegion == 0)
            {
                ltCodigoRegion = "-1";
            }
            else
            {
                ltCodigoRegion = CodigoRegion.ToString();
            }
            if (CodigoGrupo == 0)
            {
                ltCodigoGrupo = "-1";
            }
            else
            {
                ltCodigoGrupo = CodigoGrupo.ToString();
            }
            if (CodigoGrupoProducto == 0)
            {
                ltCodigoGrupoProducto = "-1";
            }
            else
            {
                ltCodigoGrupoProducto = CodigoGrupoProducto.ToString();
            }
            if (NumeroCliente.Trim().Length == 0)
            {
                ltNumeroCliente = "'-1'";
            }
            else
            {
                ltNumeroCliente = "'" + NumeroCliente + "'";
            }
            if (CodigoTipoProducto.Trim().Length == 0)
            {
                ltCodigoTipoProducto = "'-1'";
            }
            else
            {
                ltCodigoTipoProducto = "'" + CodigoTipoProducto + "'";
            }

            short success = 0;
            ltComando = "exec sp_lce_act_limte_indvd " + Convert.ToString(Codigo) + ", " + Convert.ToString(Correlativo) + ",'" + Nombre + "','" + FechaOtorgamiento.ToString("yyyy/mm/dd") + "'," + ltCodigoPais + "," + ltCodigoDesarrollo + "," + ltCodigoRegion + "," + ltCodigoGrupo + "," + ltCodigoTipoProducto + "," + Convert.ToString(CodigoTipoLimite) + "," + ltNumeroCliente + "," + ltCodigoGrupoProducto + ",'" + FechaVencimiento.ToString("yyyy/mm/dd") + "'," + Global.ConvertirComaPorPunto(Convert.ToString(MontoOtorgado)) + "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoUtilizado)) + "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoDisponible)) + "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoReservado));
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
                    //Global.GenericError("Limite.Ingresar", Information.Err().Number, Information.Err().Description);
                    success = 4;

                }

            }
            return success;

        }

        public short Modificar()
        {
            short success = 0;
            // Descripción : Modifica un Límite Individual
            // Parámetros  : Ninguno
            // Retorno     : 0 OK
            // 3 Error al modificar
            // E. laterales: Ninguno
            // 
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================
            string ltComando;
            int liResultado = 0;
            string ltCodigoPais = "";
            string ltCodigoDesarrollo = "";
            string ltCodigoRegion = "";
            string ltCodigoGrupo = "";
            string ltCodigoGrupoProducto = "";
            string ltNumeroCliente = "";
            string ltCodigoTipoProducto = "";
            if (CodigoPais == 0)
            {
                ltCodigoPais = "-1";
            }
            else
            {
                ltCodigoPais = CodigoPais.ToString();
            }
            if (CodigoDesarrollo == 0)
            {
                ltCodigoDesarrollo = "-1";
            }
            else
            {
                ltCodigoDesarrollo = CodigoDesarrollo.ToString();
            }
            if (CodigoRegion == 0)
            {
                ltCodigoRegion = "-1";
            }
            else
            {
                ltCodigoRegion = CodigoRegion.ToString();
            }
            if (CodigoGrupo == 0)
            {
                ltCodigoGrupo = "-1";
            }
            else
            {
                ltCodigoGrupo = CodigoGrupo.ToString();
            }
            if (CodigoGrupoProducto == 0)
            {
                ltCodigoGrupoProducto = "-1";
            }
            else
            {
                ltCodigoGrupoProducto = CodigoGrupoProducto.ToString();
            }
            if (NumeroCliente.Trim().Length == 0)
            {
                ltNumeroCliente = "'-1'";
            }
            else
            {
                ltNumeroCliente = "'" + NumeroCliente + "'";
            }
            if (CodigoTipoProducto.Trim().Length == 0)
            {
                ltCodigoTipoProducto = "'-1'";
            }
            else
            {
                ltCodigoTipoProducto = "'" + CodigoTipoProducto + "'";
            }

            ltComando = "exec sp_lce_act_limte_indvd " + Convert.ToString(Codigo) + ", " + Convert.ToString(Correlativo) + ",'" + Nombre + "','" + FechaOtorgamiento.ToString("yyyy/mm/dd") + "'," + ltCodigoPais + "," + ltCodigoDesarrollo + "," + ltCodigoRegion + "," + ltCodigoGrupo + "," + ltCodigoTipoProducto + "," + Convert.ToString(CodigoTipoLimite) + "," + ltNumeroCliente + "," + ltCodigoGrupoProducto + ",'" + FechaVencimiento.ToString("yyyy/mm/dd") + "'," + Global.ConvertirComaPorPunto(Convert.ToString(MontoOtorgado)) + "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoUtilizado)) + "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoDisponible)) + "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoReservado));
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
                    //Global.GenericError("Limite.Modificar", Information.Err().Number, Information.Err().Description);
                    success = 3;

                }

            }
            return success;

        }
        public short Reconstruir()
        {
            // Descripción : Reconstruye un Límite Individual
            // Parámetros  : Ninguno
            // Retorno     : 0 OK
            // 3 Error al Reconstruir
            String ltComando;
            String numCli = Global.ConvertirRutNro(NumeroCliente.Trim());
            if (NumeroCliente == "-1") numCli = NumeroCliente;
            ltComando = "exec sva_cle_act_nvo_lim_idv " + Convert.ToString(Codigo) + ", " + Convert.ToString(Correlativo) + ",'" + Nombre + "','" + FechaOtorgamiento.ToString("yyyy/mm/dd") + "'," + Convert.ToString(CodigoPais) + "," + Convert.ToString(CodigoDesarrollo) + "," + Convert.ToString(CodigoRegion) + "," + Convert.ToString(CodigoGrupo) + "," + "'" + CodigoTipoProducto + "'" + "," + "'" + numCli + "'" + "," + Convert.ToString(CodigoGrupoProducto) + ",'" + FechaVencimiento.ToString("yyyy/mm/dd") + "'," + Global.ConvertirComaPorPunto(Convert.ToString(MontoOtorgado)) + "," + "'" + Formula + "'," + Convert.ToString(Normativo ? 1 : 0) + "," + "'" + GlosaAcuerdo + "'," + Convert.ToString(Moneda);
            short success = 0;
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
                    //Global.GenericError("Limite.Reconstruir", Information.Err().Number, Information.Err().Description);
                    success = 3;
                }

            }
            return success;
        }
        public short ActualizarReserva()
        {
            // Descripción : Modifica un Límite Individual
            // Parámetros  : Ninguno
            // Retorno     : 0 OK
            // 3 Error al modificar
            // E. laterales: Ninguno
            // 
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================
            string ltComando;
            ltComando = "exec sp_lce_act_reserva_limte_indvd " + Convert.ToString(Codigo) + ", " + Convert.ToString(Correlativo) + ", '" + FechaOtorgamiento.ToString("yyyy/mm/dd") + "', " + Global.ConvertirComaPorPunto(Convert.ToString(MontoDisponible)) + ", " + Global.ConvertirComaPorPunto(Convert.ToString(MontoReservado));
            short success = 0;
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
                    //Global.GenericError("Limite.ActualizarReserva", Information.Err().Number, Information.Err().Description);
                    success = 3;
                }

            }
            return success;
        }

        public short Existe()
        {
            short Existe = 0;
            // Descripción : Verifica la existencia de un Límite Individual
            // Parámetros  : Ninguno
            // Retorno     : 0 Existe
            // 3 No existe
            // 4 Error en la BD
            // E. laterales: Ninguno
            // 
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================
            string ltComando;
            int liResultado;
            // VBto upgrade warning: ltCodigoPais As short --> As int	OnWrite(string)
            int ltCodigoPais;
            string ltCodigoDesarrollo = "";
            string ltCodigoRegion = "";
            string ltCodigoGrupo = "";
            string ltCodigoGrupoProducto = "";
            string ltNumeroCliente = "";
            string ltCodigoTipoProducto = "";

            ltComando = "exec sp_lce_cna_ext_limte_indvd ";
            ltComando += "@cod_limte = " + Convert.ToString(Codigo);
            ltComando += ", @fch_otgmt_limte='" + FechaOtorgamiento.ToString("yyyy/mm/dd") + "'";
            if (CodigoPais == 0)
            {
                ltCodigoPais = Convert.ToInt32("null");
            }
            else
            {
                ltComando += ", @cod_pais=" + Convert.ToString(CodigoPais);
            }
            if (CodigoDesarrollo == 0)
            {
                ltCodigoDesarrollo = "null";
            }
            else
            {
                ltComando += ", @cod_nivel_desrr=" + Convert.ToString(CodigoDesarrollo);
            }
            if (CodigoRegion == 0)
            {
                ltCodigoRegion = "null";
            }
            else
            {
                ltComando += ", @cod_rgion_geogr=" + Convert.ToString(CodigoRegion);
            }
            if (CodigoGrupo == 0)
            {
                ltCodigoGrupo = "null";
            }
            else
            {
                ltComando += ", @cod_grupo_econm=" + Convert.ToString(CodigoGrupo);
            }
            if (CodigoGrupoProducto == 0)
            {
                ltCodigoGrupoProducto = "null";
            }
            else
            {
                ltComando += ", @cod_tipo_ctzcn_lcext=" + Convert.ToString(CodigoGrupoProducto);
            }
            ltComando += ", @cod_tipo_limte=" + Convert.ToString(CodigoTipoLimite);
            if (NumeroCliente.Trim().Length == 0)
            {
                ltNumeroCliente = "null";
            }
            else
            {
                ltComando += ", @nro_prsna='" + NumeroCliente;
            }
            if (CodigoTipoProducto.Trim().Length == 0)
            {
                ltCodigoTipoProducto = "null";
            }
            else
            {
                ltComando += ", @cod_grupo_tipo_ctzcn_lcext='" + CodigoTipoProducto;
            }

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
                        if ((int)rec.Rows[0]["cantidad"] == 0)
                        {
                            success = 3;
                        }
                        else
                        {

                            success = 0;
                        }
                    }


                }
                else
                {
                    //Global.GenericError("Limite.Existe", Information.Err().Number, Information.Err().Description);
                    success = 3;

                }
            }
            return success;
        }


        // VBto upgrade warning: 'Return' As bool	OnWrite(int)
        public bool Normativo
        {
            get
            {
                bool Normativo = false;
                Normativo = Convert.ToBoolean(mvarEsNormativo);
                return Normativo;
            }
            // VBto upgrade warning: vNewValue As bool	OnRead(int)

            set
            {
                mvarEsNormativo = (value ? -1 : 0);
            }
        }



        public int EsNormativo
        {
            get
            {
                int EsNormativo = 0;
                EsNormativo = mvarEsNormativo;
                return EsNormativo;
            }

            set
            {
                mvarEsNormativo = value;
            }
        }


        public string Formula
        {
            get
            {
                string Formula = "";
                Formula = mvarFormula;
                return Formula;
            }

            set
            {
                mvarFormula = value.PadLeft(10);
            }
        }


        // VBto upgrade warning: ptRutCliente As string	OnWrite(VBtoField)
        public DataTable ObtenerListaLineasCliente(ref short  success, string ptRutCliente)
        {
            // Descripción : Obtiene la lista de limites de un cliente
            // Retorno     : 0 OK
            // 3 No existe
            // 4 Error en la base de datos

            string ltConsulta; DataTable rec;
                ltConsulta = "exec svc_cle_cna_lst_lim_nor_cli '" + Global.ConvertirRutNro(ptRutCliente) + "'";
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
                    //Global.GenericError("Limite.ObtenerListaLineasCliente", Information.Err().Number, Information.Err().Description);
                    success = 4;

                }
            }
            return rec;
        }

        public DataTable ObtenerListaArtNormsCliente(ref short success, string ptRutCliente)
        {
            // Descripción : Obtiene la lista de limites de un cliente
            // Retorno     : 0 OK
            // 3 No existe
            // 4 Error en la base de datos
            string ltConsulta; DataTable rec;
            ltConsulta = "exec svc_cle_cna_lst_lim_nor_cli '" + Global.ConvertirRutNro(ptRutCliente) + "'";
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
                    ///Global.GenericError("Limite.ObtenerListaArtNormsCliente", Information.Err().Number, Information.Err().Description);
                    success = 4;

                }
            }
            return rec;
        }



        public short GetPlantilla(string templ)
        {
            short success = 0;
            // Descripción : Obtiene Plantilla segun Tipo
            string ltConsulta; 
            ltConsulta = "exec svc_cle_obt_etu_seg_flt '" + templ + "'";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataTable rec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count == 0)
                    {
                        success = -3;
                    }
                    else
                    {
                        success = Convert.ToInt16(rec.Rows[0][0]);
                    }
                }
                else
                {
                    ///Global.GenericError("Limite.GetPlantilla", Information.Err().Number, Information.Err().Description);
                    success = -4;

                }
            }
            return success;
        }

        /*		// VBto upgrade warning: 'Return' As Decimal	OnWrite(short, Decimal)
                public Decimal CalcFormula()
                {
                    Decimal CalcFormula = 0;
                    // VBto upgrade warning: OK As bool	OnWrite(short)
                    Pais lpaiPais; bool OK;
                    Desarrollo ldesDesarrollo;
                    Patrimonio lpatPatrimonio;
                    lpaiPais = new Pais();
                    ldesDesarrollo = new Desarrollo();
                    lpatPatrimonio = new Patrimonio();
                    int fmx; Decimal curval;
                    // VBto upgrade warning: valorpat As Decimal	OnWrite(double)
                    Decimal valorpat; int liResultado = 0;
                    // VBto upgrade warning: ppai As double	OnWrite(float)
                    // VBto upgrade warning: pgrup As double	OnWrite(float)
                    // VBto upgrade warning: prflux As double	OnWrite(float)
                    double ppai = 0, pgrup = 0; Decimal flanex; double prflux = 0;
                    CalcFormula = 0;
                    curval = Me.MontoOtorgado;
                    OK = Convert.ToBoolean(lpatPatrimonio.Obtener(DateTime.Now, 13));
                    valorpat = Convert.ToDecimal(lpatPatrimonio.Monto);
			
                    if (Strings.UCase(Formula)=="")
                    {
                        CalcFormula = curval;
                    }
                    else if (Strings.UCase(Formula)=="LREL")
                    {
                        CalcFormula = 0;
                        if (CodigoPais!=-1) {
                            liResultado = lpaiPais.Obtener(CodigoPais);
                            if (liResultado==0) {
                                ppai = lpaiPais.PercAdmitido; // 100
                                liResultado = ldesDesarrollo.Obtener(lpaiPais.Desarrollo);
                                if (liResultado==0) {
                                    if (lpaiPais.Desarrollo==-1) {
                                        CalcFormula = 0; // "Definir Categoria"
                                    } else {
                                        pgrup = ldesDesarrollo.PercLimGlobal; // lpaiPais.Desarrollo / 100
                                        CalcFormula = 0.7*valorpat*ppai/100*pgrup/100;
                                    }
                                }
                            }
                        }
                    }
                    else if (Strings.UCase(Formula)=="LMXP")
                    {
                        CalcFormula = 0;
                        if (CodigoPais!=-1) {
                            liResultado = lpaiPais.Obtener(CodigoPais);
                            if (liResultado==0) {
                                if (lpaiPais.MaxPotencial>0) {
                                    CalcFormula = lpaiPais.MaxPotencial;
                                } else {
                                    if (lpaiPais.Desarrollo==-1) {
                                        CalcFormula = 0; // "Definir Categoria"
                                    } else {
                                        ppai = lpaiPais.PercAdmitido; // cboPais.ItemData(cboPais.ListIndex) / 100
                                        flanex = lpaiPais.FlujoExportaciones; // cboPais.ItemData(cboPais.ListIndex) * 8975
                                        liResultado = ldesDesarrollo.Obtener(lpaiPais.Desarrollo);
                                        if (liResultado==0) {
                                            pgrup = ldesDesarrollo.PercLimGlobal; // lpaiPais.Desarrollo / 100
                                            prflux = ldesDesarrollo.PercFlujo; // lpaiPais.Desarrollo / 100
                                        }
                                        CalcFormula = 0.7*valorpat*ppai/100*pgrup/100+(flanex*prflux/100);
                                    }
                                }
                            }
                        }
                    }
                    else if (Strings.UCase(Formula)=="PGLG")
                    {
                        CalcFormula = 0;
                        if (CodigoDesarrollo!=-1) {
                            liResultado = ldesDesarrollo.Obtener(CodigoDesarrollo);
                            if (liResultado==0) {
                                pgrup = ldesDesarrollo.PercLimGlobal; // .codigo / 100
                            }
                            CalcFormula = 0.7*valorpat*pgrup/100;
                        }
                    }
                    else if (Strings.UCase(Formula)=="5PPE")
                    {
                        CalcFormula = 0.05*valorpat;
                    }
                    else if (Strings.UCase(Formula)=="10PPE")
                    {
                        CalcFormula = 0.1*valorpat;
                    }
                    else if (Strings.UCase(Formula)=="30PPE")
                    {
                        CalcFormula = 0.3*valorpat;
                    }
                    else if (Strings.UCase(Formula)=="50PPE")
                    {
                        CalcFormula = 0.5*valorpat;
                    }
                    else 
                    {
                    }
                    lpaiPais = null;
                    ldesDesarrollo = null;
                    lpatPatrimonio = null;

                    return CalcFormula;
                }
        */

        public short ObtenerLineaCategoria(int liCateg)
        {
            // Descripción : Obtiene La Linea Base del Cliente,
            // Parámetros  : ptRutCliente
            // Retorno     : 0 OK
            // 3 No existe
            // 4 Error en la BD
            string ltConsulta;
            ltConsulta = "exec svc_cle_cna_lin_bas_cat_pai " + Convert.ToString(liCateg);
            short success = 0;
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataTable rec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count == 0)
                    {
                        success = 3;
                    }
                    else
                    {
                        Codigo = Convert.ToInt32(rec.Rows[0]["cod_limte"]);
                        Correlativo = Convert.ToInt32(rec.Rows[0]["nro_crtvo_limte"]);
                        Nombre = Convert.ToString(rec.Rows[0]["gls_dscrn_limte_indvd"]);
                        FechaOtorgamiento = (DateTime)rec.Rows[0]["fch_otgmt_limte"];
                        if ((int)rec.Rows[0]["cod_pais"] == -1)
                        {
                            CodigoPais = 0;
                        }
                        else
                        {
                            CodigoPais = Convert.ToInt32(rec.Rows[0]["cod_pais"]);
                        }
                        if ((int)rec.Rows[0]["cod_nivel_desrr"] == -1)
                        {
                            CodigoDesarrollo = 0;
                        }
                        else
                        {
                            CodigoDesarrollo = Convert.ToInt32(rec.Rows[0]["cod_nivel_desrr"]);
                        }
                        if ((int)rec.Rows[0]["cod_rgion_geogr"] == -1)
                        {
                            CodigoRegion = 0;
                        }
                        else
                        {
                            CodigoRegion = Convert.ToInt16(rec.Rows[0]["cod_rgion_geogr"]);
                        }
                        if ((int)rec.Rows[0]["cod_grupo_econm"] == -1)
                        {
                            CodigoGrupo = 0;
                        }
                        else
                        {
                            CodigoGrupo = Convert.ToInt32(rec.Rows[0]["cod_grupo_econm"]);
                        }
                        if (rec.Rows[0]["cod_tipo_ctzcn_lcext"] == "-1")
                        {
                            CodigoTipoProducto = string.Empty;
                        }
                        else
                        {
                            CodigoTipoProducto = Convert.ToString(rec.Rows[0]["cod_tipo_ctzcn_lcext"]) + "";
                        }
                        CodigoTipoLimite = Convert.ToInt16(rec.Rows[0]["cod_tipo_limte"]);
                        if (Convert.ToString(rec.Rows[0]["nro_prsna"]).Trim() == "-1")
                        {
                            NumeroCliente = string.Empty;
                        }
                        else
                        {
                            NumeroCliente = Global.ConvertirNroRut(Convert.ToString(rec.Rows[0]["nro_prsna"])) + "";
                        }
                        if ((int)rec.Rows[0]["cod_grupo_tipo_ctzcn_lcext"] == -1)
                        {
                            CodigoGrupoProducto = 0;
                        }
                        else
                        {
                            CodigoGrupoProducto = Convert.ToInt16(rec.Rows[0]["cod_grupo_tipo_ctzcn_lcext"]);
                        }
                        FechaVencimiento = (DateTime)rec.Rows[0]["fch_vncto_limte"];
                        MontoOtorgado = (VBtoConverter.IsNull(rec.Rows[0]["mnt_otgmt_limte"]) ? 0D : (double)rec.Rows[0]["mnt_otgmt_limte"]);
                        MontoUtilizado = (VBtoConverter.IsNull(rec.Rows[0]["mnt_utlzd_limte"]) ? 0D : (double)rec.Rows[0]["mnt_utlzd_limte"]);
                        MontoDisponible = (VBtoConverter.IsNull(rec.Rows[0]["mnt_dispo_limte"]) ? 0D : (double)rec.Rows[0]["mnt_dispo_limte"]);
                        MontoReservado = (VBtoConverter.IsNull(rec.Rows[0]["mnt_rsrvd_limte"]) ? 0D : (double)rec.Rows[0]["mnt_rsrvd_limte"]);
                        Formula = string.Empty + Convert.ToString(rec.Rows[0]["lim_gls_frm_clc"]);
                        Normativo = Convert.ToBoolean((VBtoConverter.IsNull(rec.Rows[0]["Pge_rgl_lim"]) ? 0 : rec.Rows[0]["Pge_rgl_lim"]));
                        GlosaAcuerdo = string.Empty + Convert.ToString(rec.Rows[0]["lim_gls_acu"]);
                        Moneda = (VBtoConverter.IsNull(rec.Rows[0]["lim_mon_lim"]) ? 13 : (int)rec.Rows[0]["lim_mon_lim"]);
                        mfEsNuevo = false;

                        success = 0;
                    }


                }
                else
                {
                    //Global.GenericError("Limite.ObtenerLineaCategoria", Information.Err().Number, Information.Err().Description);
                    success = 3;

                }
            }
            return success;

        }
        public short ObtenerLineaGrupoEcon(int liGrpEcon)
        {
            // Descripción : Obtiene La Linea Base del Cliente,
            // Parámetros  : ptRutCliente
            // Retorno     : 0 OK
            // 3 No existe
            // 4 Error en la BD
            string ltConsulta;
            ltConsulta = "exec svc_cle_cna_lin_bas_grp_eco " + Convert.ToString(liGrpEcon);
            short success = 0;
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataTable rec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count == 0)
                    {
                        success = 3;
                    }
                    else
                    {

                        Codigo = Convert.ToInt32(rec.Rows[0]["cod_limte"]);
                        Correlativo = Convert.ToInt32(rec.Rows[0]["nro_crtvo_limte"]);
                        Nombre = Convert.ToString(rec.Rows[0]["gls_dscrn_limte_indvd"]);
                        FechaOtorgamiento = (DateTime)rec.Rows[0]["fch_otgmt_limte"];
                        if ((int)rec.Rows[0]["cod_pais"] == -1)
                        {
                            CodigoPais = 0;
                        }
                        else
                        {
                            CodigoPais = Convert.ToInt32(rec.Rows[0]["cod_pais"]);
                        }
                        if ((int)rec.Rows[0]["cod_nivel_desrr"] == -1)
                        {
                            CodigoDesarrollo = 0;
                        }
                        else
                        {
                            CodigoDesarrollo = Convert.ToInt32(rec.Rows[0]["cod_nivel_desrr"]);
                        }
                        if ((int)rec.Rows[0]["cod_rgion_geogr"] == -1)
                        {
                            CodigoRegion = 0;
                        }
                        else
                        {
                            CodigoRegion = Convert.ToInt16(rec.Rows[0]["cod_rgion_geogr"]);
                        }
                        if ((int)rec.Rows[0]["cod_grupo_econm"] == -1)
                        {
                            CodigoGrupo = 0;
                        }
                        else
                        {
                            CodigoGrupo = Convert.ToInt32(rec.Rows[0]["cod_grupo_econm"]);
                        }
                        if (rec.Rows[0]["cod_tipo_ctzcn_lcext"] == "-1")
                        {
                            CodigoTipoProducto = string.Empty;
                        }
                        else
                        {
                            CodigoTipoProducto = Convert.ToString(rec.Rows[0]["cod_tipo_ctzcn_lcext"]) + "";
                        }
                        CodigoTipoLimite = Convert.ToInt16(rec.Rows[0]["cod_tipo_limte"]);
                        if ((rec.Rows[0]["nro_prsna"]).ToString().Trim() == "-1")
                        {
                            NumeroCliente = string.Empty;
                        }
                        else
                        {
                            NumeroCliente = Global.ConvertirNroRut(Convert.ToString(rec.Rows[0]["nro_prsna"])) + "";
                        }
                        if ((int)rec.Rows[0]["cod_grupo_tipo_ctzcn_lcext"] == -1)
                        {
                            CodigoGrupoProducto = 0;
                        }
                        else
                        {
                            CodigoGrupoProducto = Convert.ToInt16(rec.Rows[0]["cod_grupo_tipo_ctzcn_lcext"]);
                        }
                        FechaVencimiento = (DateTime)rec.Rows[0]["fch_vncto_limte"];
                        MontoOtorgado = (VBtoConverter.IsNull(rec.Rows[0]["mnt_otgmt_limte"]) ? 0D : (double)rec.Rows[0]["mnt_otgmt_limte"]);
                        MontoUtilizado = (VBtoConverter.IsNull(rec.Rows[0]["mnt_utlzd_limte"]) ? 0D : (double)rec.Rows[0]["mnt_utlzd_limte"]);
                        MontoDisponible = (VBtoConverter.IsNull(rec.Rows[0]["mnt_dispo_limte"]) ? 0D : (double)rec.Rows[0]["mnt_dispo_limte"]);
                        MontoReservado = (VBtoConverter.IsNull(rec.Rows[0]["mnt_rsrvd_limte"]) ? 0D : (double)rec.Rows[0]["mnt_rsrvd_limte"]);
                        Formula = string.Empty + Convert.ToString(rec.Rows[0]["lim_gls_frm_clc"]);
                        Normativo = Convert.ToBoolean((VBtoConverter.IsNull(rec.Rows[0]["Pge_rgl_lim"]) ? 0 : rec.Rows[0]["Pge_rgl_lim"]));
                        GlosaAcuerdo = string.Empty + Convert.ToString(rec.Rows[0]["lim_gls_acu"]);
                        Moneda = (VBtoConverter.IsNull(rec.Rows[0]["lim_mon_lim"]) ? 13 : (int)rec.Rows[0]["lim_mon_lim"]);
                        mfEsNuevo = false;
                        success = 0;
                    }


                }
                else
                {
                    //Global.GenericError("Limite.ObtenerLineaCategoria", Information.Err().Number, Information.Err().Description);
                    success = 3;

                }
            }
            return success;

        }

        public short ObtenerLineaPais(int liPais)
        {
            short ObtenerLineaPais = 0;
            // Descripción : Obtiene La Linea Base del Pais,
            // Parámetros  : ptRutCliente
            // Retorno     : 0 OK
            // 3 No existe
            // 4 Error en la BD
            string ltConsulta;
            ltConsulta = "exec svc_cle_cna_lin_bas_pai " + Convert.ToString(liPais);
            short success = 0;
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataTable rec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count == 0)
                    {
                        success = 3;
                    }
                    else
                    {


                        Codigo = Convert.ToInt32(rec.Rows[0]["cod_limte"]);
                        Correlativo = Convert.ToInt32(rec.Rows[0]["nro_crtvo_limte"]);
                        Nombre = Convert.ToString(rec.Rows[0]["gls_dscrn_limte_indvd"]);
                        FechaOtorgamiento = (DateTime)rec.Rows[0]["fch_otgmt_limte"];
                        if ((int)rec.Rows[0]["cod_pais"] == -1)
                        {
                            CodigoPais = 0;
                        }
                        else
                        {
                            CodigoPais = Convert.ToInt32(rec.Rows[0]["cod_pais"]);
                        }
                        if ((int)rec.Rows[0]["cod_nivel_desrr"] == -1)
                        {
                            CodigoDesarrollo = 0;
                        }
                        else
                        {
                            CodigoDesarrollo = Convert.ToInt32(rec.Rows[0]["cod_nivel_desrr"]);
                        }
                        if ((int)rec.Rows[0]["cod_rgion_geogr"] == -1)
                        {
                            CodigoRegion = 0;
                        }
                        else
                        {
                            CodigoRegion = Convert.ToInt16(rec.Rows[0]["cod_rgion_geogr"]);
                        }
                        if ((int)rec.Rows[0]["cod_grupo_econm"] == -1)
                        {
                            CodigoGrupo = 0;
                        }
                        else
                        {
                            CodigoGrupo = Convert.ToInt32(rec.Rows[0]["cod_grupo_econm"]);
                        }
                        if (rec.Rows[0]["cod_tipo_ctzcn_lcext"] == "-1")
                        {
                            CodigoTipoProducto = string.Empty;
                        }
                        else
                        {
                            CodigoTipoProducto = Convert.ToString(rec.Rows[0]["cod_tipo_ctzcn_lcext"]) + "";
                        }
                        CodigoTipoLimite = Convert.ToInt16(rec.Rows[0]["cod_tipo_limte"]);
                        if (Convert.ToString(rec.Rows[0]["nro_prsna"]).Trim() == "-1")
                        {
                            NumeroCliente = string.Empty;
                        }
                        else
                        {
                            NumeroCliente = Global.ConvertirNroRut(Convert.ToString(rec.Rows[0]["nro_prsna"])) + "";
                        }
                        if ((int)rec.Rows[0]["cod_grupo_tipo_ctzcn_lcext"] == -1)
                        {
                            CodigoGrupoProducto = 0;
                        }
                        else
                        {
                            CodigoGrupoProducto = Convert.ToInt16(rec.Rows[0]["cod_grupo_tipo_ctzcn_lcext"]);
                        }
                        FechaVencimiento = (DateTime)rec.Rows[0]["fch_vncto_limte"];
                        MontoOtorgado = (VBtoConverter.IsNull(rec.Rows[0]["mnt_otgmt_limte"]) ? 0D : (double)rec.Rows[0]["mnt_otgmt_limte"]);
                        MontoUtilizado = (VBtoConverter.IsNull(rec.Rows[0]["mnt_utlzd_limte"]) ? 0D : (double)rec.Rows[0]["mnt_utlzd_limte"]);
                        MontoDisponible = (VBtoConverter.IsNull(rec.Rows[0]["mnt_dispo_limte"]) ? 0D : (double)rec.Rows[0]["mnt_dispo_limte"]);
                        MontoReservado = (VBtoConverter.IsNull(rec.Rows[0]["mnt_rsrvd_limte"]) ? 0D : (double)rec.Rows[0]["mnt_rsrvd_limte"]);
                        Formula = string.Empty + Convert.ToString(rec.Rows[0]["lim_gls_frm_clc"]);
                        Normativo = Convert.ToBoolean((VBtoConverter.IsNull(rec.Rows[0]["Pge_rgl_lim"]) ? 0 : rec.Rows[0]["Pge_rgl_lim"]));
                        GlosaAcuerdo = string.Empty + Convert.ToString(rec.Rows[0]["lim_gls_acu"]);
                        Moneda = (VBtoConverter.IsNull(rec.Rows[0]["lim_mon_lim"]) ? 13 : (int)rec.Rows[0]["lim_mon_lim"]);
                        ObtenerLineaPais = 0;
                        mfEsNuevo = false;
                    }


                }
                else
                {
                    //Global.GenericError("Limite.ObtenerLineaPais", Information.Err().Number, Information.Err().Description);
                    success = 3;

                }
            }
            return success;
        }


        // afecta40
        public string PlazoUnidad
        {
            get
            {
                string PlazoUnidad = "";
                PlazoUnidad = mvarPlazoUnidad;
                return PlazoUnidad;
            }

            set
            {
                mvarPlazoUnidad = value;
            }
        }


        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public short PlazoCantidad
        {
            get
            {
                short PlazoCantidad = 0;
                PlazoCantidad = (short)mvarPlazoCantidad;
                return PlazoCantidad;
            }

            set
            {
                mvarPlazoCantidad = value;
            }
        }


        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public short PlazoAbilitado
        {
            get
            {
                short PlazoAbilitado = 0;
                PlazoAbilitado = (short)mvarPlazoAbilitado;
                return PlazoAbilitado;
            }

            set
            {
                mvarPlazoAbilitado = value;
            }
        }



        public int ReservaLineaCot(int nroCot)
        {
            int ReservaLineaCot = 0;
            // Descripción : Anula Reserva de un Límite Capitulo 3b5
            // Parámetros  : Ninguno
            // Retorno     : >0 OK
            // -3 Error al AnularReserva3b5
            // =============================================
            string ltComando;
            short suceso = 0;
            ltComando = "exec sva_bth_apl_rsv_ctz_lim_idv " + Convert.ToString(nroCot);
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                int success = db.ExecuteCommand(ltComando);
                if (db.errlist.Count == 0)
                {
                    suceso = (short)success;
                }
                else
                {
                    //Global.GenericError("Limite.ReservaLineaCot", Information.Err().Number, Information.Err().Description);
                    suceso = -3;

                }

            }
            return suceso; 
        }

        public short AnularReservaCot(int nroCot)
        {
            // Descripción : Anula Reserva de un Límite Capitulo 3b5
            // Parámetros  : Ninguno
            // Retorno     : >0 OK
            // -3 Error al AnularReserva3b5
            // =============================================
            string ltComando;
            short suceso = 0;
            ltComando = "exec sva_cle_anc_rsv_lim_ctz_ctz " + Convert.ToString(nroCot);
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                int success = db.ExecuteCommand(ltComando);
                if (db.errlist.Count == 0)
                {
                    suceso = (short)success;
                }
                else
                {
                    //Global.GenericError("Limite.AnularReservaCot", Information.Err().Number, Information.Err().Description);
                    suceso = -3;

                }

            }
            return suceso; 
        }


        // end afecta



    }
}