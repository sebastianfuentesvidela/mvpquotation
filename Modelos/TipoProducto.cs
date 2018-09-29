using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Modelos.Library;
using Tools;

namespace Modelos
{
    public class TipoProducto
    {
        public TipoProducto(String ConexionString)
            : base()
        {
            dataConnectionString = ConexionString;
        }
        private String dataConnectionString;

        //=========================================================


        private string mvarCodigo; // copia local
        private int mvarCodigoPlazo; // copia local
        private string mvarNombre = ""; // copia local
        private string mvarCodigoFamilia = ""; // copia local
        private int mvarTipoCredito;
        private bool mfEsNuevo;

        private int mvarPosicSicomar;
        private int mvarExigeSubyacente;
        private int mvarTipoAfectacion;
        private int mvarEsSaldoLinea;

        public List<String> errlist = new List<String>();

        public short PosicSicomar
        {
            get
            {
                short PosicSicomar = 0;
                PosicSicomar = (short)mvarPosicSicomar;
                return PosicSicomar;
            }

            set
            {
                mvarPosicSicomar = value;
            }
        }

        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public short ExigeSubyacente
        {
            get
            {
                short ExigeSubyacente = 0;
                ExigeSubyacente = (short)mvarExigeSubyacente;
                return ExigeSubyacente;
            }

            set
            {
                mvarExigeSubyacente = value;
            }
        }

        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public short TipoAfectacion
        {
            get
            {
                short TipoAfectacion = 0;
                TipoAfectacion = (short)mvarTipoAfectacion;
                return TipoAfectacion;
            }

            set
            {
                mvarTipoAfectacion = value;
            }
        }

        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public short EsSaldoLinea
        {
            get
            {
                short EsSaldoLinea = 0;
                EsSaldoLinea = (short)mvarEsSaldoLinea;
                return EsSaldoLinea;
            }

            set
            {
                mvarEsSaldoLinea = value;
            }
        }


        public void CrearNuevo()
        {

            mfEsNuevo = true;

            Codigo = string.Empty;
            CodigoPlazo = 0;
            Nombre = string.Empty;
            CodigoFamilia = string.Empty;
            EsSaldoLinea = 0;
            ExigeSubyacente = 0;
            PosicSicomar = 0;
            TipoAfectacion = 0;
        }
        public short Eliminar(string piCodigo)
        {
            // Descripción : Elimina un tipo de producto LCE por Codigo
            // Parámetros  : piCodigo
            // Retorno     : 0 OK
            // 3 Error al eliminar
            // E. laterales: Ninguno
            // 
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================
            string ltComando;
            short suceso = 0;
            ltComando = "exec sp_lce_eli_tipo_ctzcn_lcext '" + piCodigo + "'";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                int success = db.ExecuteCommand(ltComando);
                if (db.errlist.Count == 0)
                {
                    suceso = 0;
                }
                else
                {
                    //modLCEData.GenericError("TipoProducto.Eliminar", Information.Err().Number, Information.Err().Description);
                    errlist.Add("No fue posible eliminar el codigo '" + piCodigo + "'");

                    suceso = 3;

                }

            }
            return suceso;

        }

        public short Guardar()
        {
            // Descripción : Guarda un tipo de producto LCE
            // Parámetros  : Ninguno
            // Retorno     : 0 OK
            // 3 Error al guardar
            // E. laterales: Ninguno
            // 
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================
            string ltComando;
            short success = 0;
            ltComando = "exec sp_lce_act_tipo_ctzcn_lcext '" + Codigo + "'," + Convert.ToString(CodigoPlazo) + ", '" + Nombre + "','" + CodigoFamilia + "'";
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
                        errlist.Add("No fue posible Guardar el Registro codigo '" + Codigo + "'");
                    }
                }
                else
                {
                    errlist.AddRange(db.errlist);
                    //modLCEData.GenericError("TipoProducto.Guardar", Information.Err().Number, Information.Err().Description);
                    success = 3;

                }

            }
            return success;

        }

        public short Existe(string piCodigo)
        {
            // Descripción : Verifica la existencia de un tipo de producto por codigo
            // Parámetros  : piCodigo
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
            ltConsulta = "exec sp_lce_cna_tipo_ctzcn '" + piCodigo + "'";
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
                        success = 0;
                    }
                }
                else
                {
                    //modLCEData.GenericError("TipoProducto.Existe", Information.Err().Number, Information.Err().Description);
                    errlist.AddRange(db.errlist);
                    success = 4;

                }
            }
            return success;

        }

        public short Obtener(string piCodigo)
        {
            // Descripción : Obtiene un tipo de producto LCE por codigo
            // Parámetros  : piCodigo
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
            ltConsulta = "exec svc_cle_obt_tip_ope_por_cod '" + piCodigo + "'";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataTable rec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count == 0)
                    {
                        errlist.Add("No se encontró el regoistro Codigo '" + piCodigo + "'");
                        success = 3;
                    }
                    else
                    {
                        Codigo = rec.Rows[0]["cod_tipo_ctzcn_lcext"].ToString();
                        CodigoPlazo = Convert.ToInt32(rec.Rows[0]["cod_rango_plazo"]);
                        Nombre = rec.Rows[0]["gls_dscrn_tipo_ctzcn_lcext"].ToString();
                        CodigoFamilia = rec.Rows[0]["cod_fmlia_ctzcn_lcext"].ToString();
                        TipoCredito = (rec.Rows[0]["tip_cod_tip_cre"] == DBNull.Value) ? (short)0 : short.Parse(rec.Rows[0]["tip_cod_tip_cre"].ToString());
                        EsSaldoLinea = (rec.Rows[0]["tip_cod_exg_lin"] == DBNull.Value) ? (short)0 : short.Parse(rec.Rows[0]["tip_cod_exg_lin"].ToString());
                        ExigeSubyacente = (rec.Rows[0]["tip_cod_exg_deu_ind"] == DBNull.Value) ? (short)0 : short.Parse(rec.Rows[0]["tip_cod_exg_deu_ind"].ToString());
                        PosicSicomar = (rec.Rows[0]["tip_cod_pos_sic"] == DBNull.Value) ? (short)0 : short.Parse(rec.Rows[0]["tip_cod_pos_sic"].ToString());
                        TipoAfectacion = (rec.Rows[0]["tip_cod_afe_lim"] == DBNull.Value) ? (short)0 : short.Parse(rec.Rows[0]["tip_cod_afe_lim"].ToString());

                        mfEsNuevo = false;
                        success = 0;
                    }
                }
                else
                {
                    //modLCEData.GenericError("TipoProducto.Obtener", Information.Err().Number, Information.Err().Description);
                    errlist.AddRange(db.errlist);
                    success = 4;

                }
            }
            return success;
        }

        public int ObtenerModoAfectacion(string ptCodigoTipo, string ptCodigoTipoSuby, string Look)
        {
            string ltConsulta;
            int success = 0;
            if (ptCodigoTipoSuby == string.Empty) ptCodigoTipoSuby = "-1";
            ltConsulta = "exec svc_cle_cna_mod_afe_por_tip '" + ptCodigoTipo + "', '" + ptCodigoTipoSuby + "'";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataTable rec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count != 0)
                    {
                        if (Look == "sbyasume70")
                        {
                            success = (int)rec.Rows[0][10];
                        }
                    }
                }
                else
                {
                    errlist.AddRange(db.errlist);
                    //modLCEData.GenericError("TipoProducto.ObtenerModoAfectacion", Information.Err().Number, Information.Err().Description);
                }
            }
            return success;
        }

        public DataTable GetList(ref short suceso)
        {
            // Descripción : Obtiene la lista de tipos de productos
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
            ltConsulta = "exec sp_lce_cna_lst_tipo_ctzcn ";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataTable rec = db.GetRecordset(ltConsulta);
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
                    rec.Columns.Add("Rotulo");
                    rec.Columns.Add("Item");
                    foreach (DataRow r in rec.Rows)
                    {
                        r["Rotulo"] = Convert.ToString(r["cod_tipo_ctzcn_lcext"]) + "-" + Convert.ToString(r["gls_dscrn_tipo_ctzcn_lcext"]);
                        r["Item"] = Convert.ToString(r["cod_tipo_ctzcn_lcext"]) + "-" + Convert.ToString(r["cod_fmlia_ctzcn_lcext"]) + "-" + Convert.ToString(r["tip_cod_tip_cre"]);
                    }
                    return rec;
                }
                else
                {
                    suceso = 4;
                    errlist.AddRange(db.errlist);
                    //modLCEData.GenericError("TipoProducto.GetList", Information.Err().Number, Information.Err().Description);
                }
            }
            return null;

        }


        public DataTable GetListGrupo(ref short suceso, int piCodigoGrupo)
        {
            // Descripción : Obtiene la lista de tipos de productos dado un grupo
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
            ltConsulta = "exec sp_lce_cna_lst_tpo_ctzcn_grp " + Convert.ToString(piCodigoGrupo);
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataTable rec = db.GetRecordset(ltConsulta);
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
                    return rec;
                }
                else
                {
                    suceso = 4;
                    errlist.AddRange(db.errlist);

                    //modLCEData.GenericError("TipoProducto.GetListGrupo", Information.Err().Number, Information.Err().Description);
                }
            }
            return null;
        }

        public DataTable GetListSico(ref short suceso) //VBtoRecordSet precTipoProducto)
        {
            // Descripción : Obtiene la lista de tipos de productos
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
            ltConsulta = "exec svc_cle_lst_tip_seg_pos_sic ";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataTable rec = db.GetRecordset(ltConsulta);
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
                    return rec;
                }
                else
                {
                    suceso = 4;
                    errlist.AddRange(db.errlist);

                    //modLCEData.GenericError("TipoProducto.GetListSico", Information.Err().Number, Information.Err().Description);
                }
            }
            return null;
        }

        public string Nombre
        {
            set
            {
                mvarNombre = value;
            }

            get
            {
                string Nombre = "";
                Nombre = mvarNombre;
                return Nombre;
            }
        }






        public string Codigo
        {
            set
            {
                // se usa cuando se asigna un valor a una propiedad, en el lado izquierdo de la asignación.
                // Syntax: X.Codigo = 5
                mvarCodigo = value;
            }

            get
            {
                string Codigo = "";
                // se usa cuando se asigna un valor a una propiedad, en el lado derecho de la asignación.
                // Syntax: Debug.Print X.Codigo
                Codigo = mvarCodigo;
                return Codigo;
            }
        }







        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public int CodigoPlazo
        {
            get
            {
                int CodigoPlazo = 0;
                CodigoPlazo = mvarCodigoPlazo;
                return CodigoPlazo;
            }

            set
            {
                mvarCodigoPlazo = value;

                /*   int liRespuesta = 0;
                // Dim lplaPlazo As Plazo
                // Set lplaPlazo = New Plazo

                if (mfEsNuevo) {
                    mvarCodigoPlazo = value;
                } else {
                    // liRespuesta = lplaPlazo.Existe(vData)
                    if (liRespuesta==0) {
                        mvarCodigoPlazo = value;
                    } else {
                        Information.Err().Raise(30002, null, "Código plazo no existe", null,null);
                    }
                }*/
            }
        }



        public TipoProducto()
            : base()
        {
            mfEsNuevo = false;
        }



        public string CodigoFamilia
        {
            get
            {
                string CodigoFamilia = "";
                CodigoFamilia = mvarCodigoFamilia;
                return CodigoFamilia;
            }

            set
            {
                mvarCodigoFamilia = value;
            }
        }



        // VBto upgrade warning: 'Return' As short	OnWrite(int)
        public short TipoCredito
        {
            get
            {
                short TipoCredito = 0;
                TipoCredito = (short)mvarTipoCredito;
                return TipoCredito;
            }

            set
            {
                mvarTipoCredito = value;
            }
        }



        public DataTable GetListConAfectacion(ref short suceso) // VBtoRecordSet precTipoProducto)
        {
            // Descripción : Obtiene la lista de tipos de productos
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
            ltConsulta = "exec svc_cle_cna_lst_tip_con_afe ";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataTable rec = db.GetRecordset(ltConsulta);
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

                    return rec;
                }
                else
                {
                    suceso = 4;
                    errlist.AddRange(db.errlist);

                    //modLCEData.GenericError("TipoProducto.GetListConAfectacion", Information.Err().Number, Information.Err().Description);
                }
            }
            return null;
        }

        public DataTable GetListSubyQueComb(ref short suceso)//VBtoRecordSet precTipoProducto)
        {
            // Descripción : Obtiene la lista de tipos de productos
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
            ltConsulta = "exec svc_cle_cna_lst_tip_ope_sby '" + Codigo + "'";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataTable rec = db.GetRecordset(ltConsulta);
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
                    return rec;
                }
                else
                {
                    suceso = 4;
                    errlist.AddRange(db.errlist);

                    //modLCEData.GenericError("TipoProducto.GetListSubyQueComb", Information.Err().Number, Information.Err().Description);
                }
            }
            return null;
        }
    }
}