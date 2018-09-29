using System;
using Microsoft.VisualBasic;
using System.Data;
using Modelos.Library;
using System.Data.SqlClient;
using Tools;


namespace Modelos
{
	public class GrupoProducto
	{

		//=========================================================


		private int mvarExigibilidad; 
		private int mvarGrupoPadre; 
		private int mvarCodigo; 
		private string mvarNombre = ""; 
		private string mvarTiposenArbol = "";
		private object mvarGruposRamas;
		private bool mfEsNuevo; 

        private String dataConnectionString;

        public GrupoProducto(String ConexionString)
            : base()
        {
            dataConnectionString = ConexionString;
			mfEsNuevo = false;
        }



        ////public short Eliminar(int piCodigo)
        ////{
        ////    // Descripción : Elimina un grupo producto por Codigo
        ////    // Parámetros  : piCodigo
        ////    // Retorno     : 0 OK
        ////    // 3 Error al eliminar
        ////    // E. laterales: Ninguno
        ////    // 
        ////    // =============================================
        ////    // Declaración de constantes/variables locales
        ////    // =============================================
        ////    string ltComando;
        ////    int liResultado = 0;

        ////     /*? On Error Resume Next  */ // GoTo ManejoError

        ////    // *** Pendiente considerar que el grupo tiene tipos asociados

        ////    ltComando = "exec sp_lce_eli_grupo_tipo_ctzcn "+Convert.ToString(piCodigo);
        ////    // liResultado = gbasAlce_ExecuteSQL(ltComando)
        ////    modAjuste32.gbasAlce_Execute(ltComando, ref liResultado);
        ////    if (!(Information.Err().Number==0)) {
        ////        if (Strings.InStr(Information.Err().Description, "Existen Integrantes", CompareMethod.Text/*?*/)>0) {
        ////            MessageBox.Show("No es Posible Eliminar este Grupo de Tipos Operacion"+"\r\n"+"puesto que contiene Tipos De Operacion Asignados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        ////            Eliminar = 4;
        ////            return Eliminar;
        ////        } else { goto ManejoError;
        ////        }
        ////    }
        ////    if (liResultado==1) {
        ////        Eliminar = 0;
        ////    } else {
        ////        Eliminar = 3;
        ////    }

        ////    return Eliminar;

        ////ManejoError: ;
        ////    modLCEData.GenericError("GrupoProducto.Eliminar", Information.Err().Number, Information.Err().Description);
        ////    Eliminar = 3;
        ////    return Eliminar;
        ////}


        ////public short Guardar()
        ////{
        ////    short Guardar = 0;
        ////    // Descripción : Guarda un grupo de tipos de productos
        ////    // Parámetros  : Ninguno
        ////    // Retorno     : 0 OK
        ////    // 3 Error al guardar
        ////    // E. laterales: Ninguno
        ////    // 
        ////    // =============================================
        ////    // Declaración de constantes/variables locales
        ////    // =============================================
        ////    string ltComando = "";
        ////    int liResultado = 0;
        ////    VBtoRecordSet lrecConsulta;

        ////    try
        ////    {	// On Error GoTo ManejoError

        ////        if (mfEsNuevo) {
        ////            // Asignación del código
        ////            // ltComando = "select max(cod_grupo_tipo_ctzcn_lcext) maximo " &
        ////            // "   from ta_grupo_tipo_ctzcn_lcext"
        ////            ltComando = "sp_slc_ta_grupo_tipo_ctzcn_lcext_maxtclcext";
        ////            lrecConsulta = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltComando);

        ////            if (lrecConsulta.BOF && lrecConsulta.EOF) {
        ////                Guardar = 4;
        ////                return Guardar;
        ////            } else {
        ////                if (VBtoConverter.IsNull(lrecConsulta.Fields["maximo"].Value)) {
        ////                    Codigo = 1;
        ////                } else {
        ////                    Codigo = lrecConsulta.Fields["maximo"]+1;
        ////                }
        ////            }
        ////        }
        ////        ltComando = "exec sp_lce_act_grupo_tipo_ctzcn "+Convert.ToString(Codigo)+", '"+modAjuste32.qrst(Nombre)+"', "+Convert.ToString(Exigibilidad)+", "+Convert.ToString(GrupoPadre);
        ////        // liResultado = gbasAlce_ExecuteSQL(ltComando)
        ////        modAjuste32.gbasAlce_Execute(ltComando, ref liResultado);

        ////        if (liResultado==1) {
        ////            Guardar = 0;
        ////            mfEsNuevo = false;
        ////        } else {
        ////            Guardar = 3;
        ////        }

        ////        return Guardar;

        ////    }
        ////    catch
        ////    {	// ManejoError:
        ////        modLCEData.GenericError("GrupoProducto.Guardar", Information.Err().Number, Information.Err().Description);
        ////        Guardar = 3;

        ////    }
        ////    return Guardar;
        ////}


        ////public short Existe(int piCodigo)
        ////{
        ////    short Existe = 0;
        ////    // Descripción : Verifica la existencia de un grupo de
        ////    // tipos de productos por codigo
        ////    // Parámetros  : piCodigo
        ////    // Retorno     : 0 OK
        ////    // 3 No existe
        ////    // 4 Error en la base de datos
        ////    // E. laterales: Ninguno
        ////    // 
        ////    // =============================================
        ////    // Declaración de constantes/variables locales
        ////    // =============================================
        ////    string ltComando;
        ////    int liResultado;
        ////    VBtoRecordSet lrecConsulta;

        ////    try
        ////    {	// On Error GoTo ManejoError

        ////        ltComando = "exec sp_lce_cna_grupo_tipo_ctzcn "+Convert.ToString(piCodigo);
        ////        lrecConsulta = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltComando);

        ////        if (lrecConsulta.BOF && lrecConsulta.EOF) {
        ////            // No hay registro
        ////            Existe = 3;
        ////        } else {
        ////            Existe = 0;
        ////        }

        ////        return Existe;

        ////    }
        ////    catch
        ////    {	// ManejoError:
        ////        modLCEData.GenericError("GrupoProducto.Existe", Information.Err().Number, Information.Err().Description);
        ////        Existe = 4;

        ////    }
        ////    return Existe;
        ////}

        ////public short GetList(ref VBtoRecordSet precGrupoProducto)
        ////{
        ////    short GetList = 0;
        ////    // Descripción : Obtiene la lista de grupos de productos
        ////    // Parámetros  : Ninguno
        ////    // Retorno     : 0 OK
        ////    // 3 No existe
        ////    // 4 Error en la base de datos
        ////    // E. laterales: Ninguno
        ////    // 
        ////    // =============================================
        ////    // Declaración de constantes/variables locales
        ////    // =============================================
        ////    string ltConsulta;
        ////    string ltLlave = "";

        ////    try
        ////    {	// On Error GoTo ManejoError

        ////        ltConsulta = "exec sp_lce_cna_lst_grupo_tpo_ctzcn ";

        ////        precGrupoProducto = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltConsulta); // , dbOpenSnapshot,
        ////        // dbSQLPassThrough)

        ////        if (precGrupoProducto.BOF && precGrupoProducto.EOF) {
        ////            GetList = 3;
        ////        } else {
        ////            GetList = 0;
        ////        }

        ////        return GetList;

        ////    }
        ////    catch
        ////    {	// ManejoError:
        ////        modLCEData.GenericError("GrupoProducto.GetList", Information.Err().Number, Information.Err().Description);
        ////        GetList = 4;

        ////    }
        ////    return GetList;
        ////}

        ////public short GetListHijos(ref VBtoRecordSet precGrupoProducto) { short par = 0; return GetListHijos(ref precGrupoProducto, ref par); }
        ////public short GetListHijos(ref VBtoRecordSet precGrupoProducto, ref short piCodGrupo)
        ////{
        ////    short GetListHijos = 0;
        ////    // Descripción : Obtiene la lista de grupos de productos cuyo flag de padre es el actual
        ////    // Parámetros  : Ninguno
        ////    // Retorno     : 0 OK
        ////    // 3 No existe
        ////    // 4 Error en la base de datos
        ////    // E. laterales: Ninguno
        ////    // 
        ////    // =============================================
        ////    // Declaración de constantes/variables locales
        ////    // =============================================
        ////    string ltConsulta;
        ////    string ltLlave = "";

        ////    try
        ////    {	// On Error GoTo ManejoError
        ////        if (Information.IsNothing(piCodGrupo)) piCodGrupo = Codigo;

        ////        ltConsulta = "exec svc_arb_grp_dat_hij_dpn "+Convert.ToString(piCodGrupo);

        ////        precGrupoProducto = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltConsulta);

        ////        if (precGrupoProducto.BOF && precGrupoProducto.EOF) {
        ////            GetListHijos = 3;
        ////        } else {
        ////            GetListHijos = 0;
        ////        }

        ////        return GetListHijos;

        ////    }
        ////    catch
        ////    {	// ManejoError:
        ////        modLCEData.GenericError("GrupoProducto.GetListHijos", Information.Err().Number, Information.Err().Description);
        ////        GetListHijos = 4;

        ////    }
        ////    return GetListHijos;
        ////}
        ////public short GetLineas(ref VBtoRecordSet precGrupoProducto)
        ////{
        ////    short GetLineas = 0;
        ////    // Descripción : Obtiene la lista de grupos de productos
        ////    // Parámetros  : Ninguno
        ////    // Retorno     : 0 OK
        ////    // 3 No existe
        ////    // 4 Error en la base de datos
        ////    // E. laterales: Ninguno
        ////    // 
        ////    // =============================================
        ////    // Declaración de constantes/variables locales
        ////    // =============================================
        ////    string ltConsulta;
        ////    string ltLlave = "";

        ////    try
        ////    {	// On Error GoTo ManejoError

        ////        ltConsulta = "exec svc_lce_lst_grp_lin_cli";

        ////        precGrupoProducto = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltConsulta); // , dbOpenSnapshot,
        ////        // dbSQLPassThrough)

        ////        if (precGrupoProducto.BOF && precGrupoProducto.EOF) {
        ////            GetLineas = 3;
        ////        } else {
        ////            GetLineas = 0;
        ////        }

        ////        return GetLineas;

        ////    }
        ////    catch
        ////    {	// ManejoError:
        ////        modLCEData.GenericError("GrupoProducto.GetLineas", Information.Err().Number, Information.Err().Description);
        ////        GetLineas = 4;
        ////    }
        ////    return GetLineas;
        ////}

        ////public short GetLineasCtl(ref VBtoRecordSet precGrupoProducto)
        ////{
        ////    short GetLineasCtl = 0;
        ////    // Descripción : Obtiene la lista de grupos de productos
        ////    // Parámetros  : Ninguno
        ////    // Retorno     : 0 OK
        ////    // 3 No existe
        ////    // 4 Error en la base de datos
        ////    // E. laterales: Ninguno
        ////    // 
        ////    // =============================================
        ////    // Declaración de constantes/variables locales
        ////    // =============================================
        ////    string ltConsulta;
        ////    string ltLlave = "";

        ////    try
        ////    {	// On Error GoTo ManejoError

        ////        ltConsulta = "exec svc_lce_lst_grp_lin_inf_ctl";

        ////        precGrupoProducto = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltConsulta); // , dbOpenSnapshot,
        ////        // dbSQLPassThrough)

        ////        if (precGrupoProducto.BOF && precGrupoProducto.EOF) {
        ////            GetLineasCtl = 3;
        ////        } else {
        ////            GetLineasCtl = 0;
        ////        }

        ////        return GetLineasCtl;

        ////    }
        ////    catch
        ////    {	// ManejoError:
        ////        modLCEData.GenericError("GrupoProducto.GetLineasCtl", Information.Err().Number, Information.Err().Description);
        ////        GetLineasCtl = 4;
        ////    }
        ////    return GetLineasCtl;
        ////}

        //public short Obtener(int piCodigo)
        //{
        //    short Obtener = 0;
        //    // Descripción : Obtiene un Grupo de tipos de productos por codigo
        //    // Parámetros  : piCodigo
        //    // Retorno     : 0 OK
        //    // 3 No existe
        //    // 4 Error en la base de datos
        //    // E. laterales: Ninguno
        //    // 
        //    // =============================================
        //    // Declaración de constantes/variables locales
        //    // =============================================
        //    string ltConsulta;
        //    VBtoRecordSet lrecGrupo;

        //    try
        //    {	// On Error GoTo ManejoError

        //        ltConsulta = "exec sp_lce_cna_grupo_tipo_ctzcn "+Convert.ToString(piCodigo);

        //        lrecGrupo = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltConsulta); // , dbOpenSnapshot,
        //        // dbSQLPassThrough)
        //        if (lrecGrupo.BOF && lrecGrupo.EOF) {
        //            Obtener = 3;
        //        } else {
        //            Codigo = Convert.ToInt32(lrecGrupo.Fields["cod_grupo_tipo_ctzcn_lcext"].Value);
        //            Nombre = Convert.ToString(lrecGrupo.Fields["gls_grupo_tipo_ctzcn_lcext"].Value)+"";
        //            Exigibilidad = Convert.ToInt32(lrecGrupo.Fields["grp_cod_exg_lin"].Value);
        //            GrupoPadre = (VBtoConverter.IsNull(lrecGrupo.Fields["grp_cod_grp_pde"].Value) ? 0 : lrecGrupo.Fields["grp_cod_grp_pde"].Value);
        //            TiposenArbol = ObtenerTiposSegunArbol(Codigo);
        //            GruposRamas = ObtenerGrpDependientesArbol(Codigo);
        //            Obtener = 0;
        //            mfEsNuevo = false;
        //        }

        //        lrecGrupo.Close();

        //        return Obtener;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("GrupoProducto.Obtener", Information.Err().Number, Information.Err().Description);
        //        Obtener = 4;
        //    }
        //    return Obtener;
        //}

        //public string ObtenerTiposSegunArbol(int piCodigo)
        //{
        //    string ObtenerTiposSegunArbol = "";
        //    // Descripción : Obtiene la Combinacion de Tipos Operacion Contenidos en si mismo y tambien en todos los Descendientes de Grupo de tipos de productos por codigo
        //    // Parámetros  : piCodigo
        //    // Retorno     : 0 OK
        //    // 3 No existe
        //    // 4 Error en la base de datos
        //    // E. laterales: Ninguno
        //    // 
        //    // =============================================
        //    // Declaración de constantes/variables locales
        //    // =============================================
        //    string ltConsulta;
        //    VBtoRecordSet lrecGrupo;
        //    string ltResult;
        //    try
        //    {	// On Error GoTo ManejoError

        //        ltResult = "|";

        //        ltConsulta = "exec svc_arb_lst_tip_ctz "+Convert.ToString(piCodigo);

        //        lrecGrupo = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltConsulta); // , dbOpenSnapshot,
        //        // dbSQLPassThrough)
        //        if (lrecGrupo.BOF && lrecGrupo.EOF) {
        //            ObtenerTiposSegunArbol = "||";
        //        } else {
        //            while (!lrecGrupo.EOF) {
        //                if (!VBtoConverter.IsNull(lrecGrupo.Fields[0].Value)) {
        //                    ltResult += Convert.ToString(lrecGrupo.Fields[0].Value)+"|";
        //                }
        //                lrecGrupo.MoveNext();
        //            }
        //            ObtenerTiposSegunArbol = ltResult;
        //        }

        //        lrecGrupo.Close();

        //        return ObtenerTiposSegunArbol;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("GrupoProducto.Obtener", Information.Err().Number, Information.Err().Description);
        //        ObtenerTiposSegunArbol = string.Empty;
        //    }
        //    return ObtenerTiposSegunArbol;
        //}
        //// VBto upgrade warning: 'Return' As Variant --> As string
        //public string ObtenerGrpDependientesArbol(int piCodigo)
        //{
        //    string ObtenerGrpDependientesArbol = "";
        //    // Descripción : Obtiene la Lista de Grupos Conponentes de la Rama del Grupo de tipos de productos por codigo
        //    // Parámetros  : piCodigo
        //    // Retorno     : 0 OK
        //    // 3 No existe
        //    // 4 Error en la base de datos
        //    // E. laterales: Ninguno
        //    // 
        //    // =============================================
        //    // Declaración de constantes/variables locales
        //    // =============================================
        //    string ltConsulta;
        //    VBtoRecordSet lrecGrupo;
        //    string ltResult;
        //    try
        //    {	// On Error GoTo ManejoError

        //        ltResult = "|";

        //        ltConsulta = "exec svc_arb_lst_grp_dpn_grp "+Convert.ToString(piCodigo);

        //        lrecGrupo = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltConsulta); // , dbOpenSnapshot,
        //        // dbSQLPassThrough)
        //        if (lrecGrupo.BOF && lrecGrupo.EOF) {
        //            ObtenerGrpDependientesArbol = "||";
        //        } else {
        //            while (!lrecGrupo.EOF) {
        //                if (!VBtoConverter.IsNull(lrecGrupo.Fields[0].Value)) {
        //                    ltResult += Convert.ToString(lrecGrupo.Fields[0].Value)+"|";
        //                }
        //                lrecGrupo.MoveNext();
        //            }
        //            ObtenerGrpDependientesArbol = ltResult;
        //        }

        //        lrecGrupo.Close();

        //        return ObtenerGrpDependientesArbol;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("GrupoProducto.ObtenerGrpDependientesArbol", Information.Err().Number, Information.Err().Description);
        //        ObtenerGrpDependientesArbol = string.Empty;
        //    }
        //    return ObtenerGrpDependientesArbol;
        //}




		public string Nombre
		{
			set
			{
				// se usa cuando se asigna un valor a una propiedad, en el lado izquierdo de la asignación.
				// Syntax: X.Nombre = 5
				if (Strings.Trim(value).Length>30) {
					Information.Err().Raise(30001, null, "String demasiado largo", null,null);
				} else {
					mvarNombre = value;
				}
			}

			get
			{
					string Nombre = "";

				// se usa cuando se asigna un valor a una propiedad, en el lado derecho de la asignación.
				// Syntax: Debug.Print X.Nombre
				Nombre = mvarNombre;
				return Nombre;
			}
		}

		public void CrearNuevo()
		{

			mfEsNuevo = true;
			Codigo = 0;
			Nombre = string.Empty;
			Exigibilidad = 0;

		}




		public short Codigo
		{
			set
			{
				// se usa cuando se asigna un valor a una propiedad, en el lado izquierdo de la asignación.
				// Syntax: X.Codigo = 5
				mvarCodigo = value;
			}
			get
			{
					short Codigo = 0;
				// se usa cuando se asigna un valor a una propiedad, en el lado derecho de la asignación.
				// Syntax: Debug.Print X.Codigo
				Codigo = (short)mvarCodigo;
				return Codigo;
			}
		}







		// VBto upgrade warning: 'Return' As short	OnWrite(int)
		public short Exigibilidad
		{
			get
			{
					short Exigibilidad = 0;
				Exigibilidad = (short)mvarExigibilidad;
				return Exigibilidad;
			}

			set
			{
				mvarExigibilidad = value;
			}
		}



		// VBto upgrade warning: 'Return' As short	OnWrite(int)
		public short GrupoPadre
		{
			get
			{
					short GrupoPadre = 0;
				GrupoPadre = (short)mvarGrupoPadre;
				return GrupoPadre;
			}

			set
			{
				mvarGrupoPadre = value;
			}
		}



		public string TiposenArbol
		{
			get
			{
					string TiposenArbol = "";
				TiposenArbol = mvarTiposenArbol;
				return TiposenArbol;
			}

			set
			{
				mvarTiposenArbol = value;
			}
		}



		public object GruposRamas
		{
			get
			{
					object GruposRamas = null;
				GruposRamas = mvarGruposRamas;
				return GruposRamas;
			}

			set
			{
				mvarGruposRamas = value;
			}
		}


		// 
		// 
		public bool IsTipInGrupo(string sTipoProd) { string par = ""; return IsTipInGrupo(sTipoProd, par); }
		public bool IsTipInGrupo(string sTipoProd, string sFlag)
		{
			// Descripción : Verifica la xistencia de un Tipo dentro del Grupo
			// si sFlag no es vacio usa ese grupo de busqueda
			// tipos de productos por codigo
			// Parámetros  : sTipoProd,sFlag
			// Retorno     : True si existe
			// False si no existe
			// =============================================
			// Declaración de constantes/variables locales
			// =============================================
            bool success = false;
            string ltComando; DataTable rec;
		    
            	if (Strings.UCase(sFlag)=="TOTPRI")
				{
					ltComando = "exec sva_cle_exi_tip_rel_grp 0, '"+sTipoProd+"', 4, 0";
				}
				else if (Strings.UCase(sFlag)=="70MAIN")
				{
					ltComando = "exec sva_cle_exi_tip_rel_grp 0, '"+sTipoProd+"', 1, 1";
				}
				else if (Strings.UCase(sFlag)=="70ADIC")
				{
					ltComando = "exec sva_cle_exi_tip_rel_grp 0, '"+sTipoProd+"', 1, 2";
				}
				else if (Strings.UCase(sFlag)=="30PERC")
				{
					ltComando = "exec sva_cle_exi_tip_rel_grp 0, '"+sTipoProd+"', 1, 3";
				}
				else if (Strings.UCase(sFlag)=="15PERC")
				{
					ltComando = "exec sva_cle_exi_tip_rel_grp 0, '"+sTipoProd+"', 1, 4";
				}
				else if (Strings.UCase(sFlag)=="810RAN")
				{
					ltComando = "exec sva_cle_exi_tip_rel_grp 0, '"+sTipoProd+"', 1, 5";
				}
				else 
				{
					ltComando = "exec sva_cle_exi_tip_rel_grp "+Convert.ToString(Codigo)+", '"+sTipoProd+"'";
				}

            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                rec = db.GetRecordset(ltComando);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count > 0)
                    {
					    if (int.Parse(rec.Rows[0][0].ToString()) > 0 ) {
                            success = true;
					    }
                    }
                }
                else
                {
                    //modLCEData.GenericError("GrupoProducto.IsTipInGrupo", Information.Err().Number, Information.Err().Description);
                    //success = 4;

                }
            }
            return success;
		}


	}
}