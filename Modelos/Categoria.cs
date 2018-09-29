using System;
using Microsoft.VisualBasic;
using System.Data;
using Modelos.Library;
using Tools;

namespace Modelos
{
	public class Categoria
	{

		//=========================================================

		private int mvarCodigo; // copia local
		private string mvarNombre = ""; // copia local
		private float mvarPercFlujo;
		private float mvarPercLimGlobal;
		private bool mfEsNuevo;

        private String dataConnectionString;

        public Categoria(String ConexionString)
            : base()
        {
            dataConnectionString = ConexionString;
			mfEsNuevo = false;
        }

        //public short Eliminar(int piCodigo)
        //{
        //    short Eliminar = 0;
        //    // Descripción : Elimina un Nivel de Desarrollo por Codigo
        //    // Parámetros  : piCodigo
        //    // Retorno     : 0 OK
        //    // 3 Error al eliminar
        //    // E. laterales: Ninguno
        //    // 
        //    // =============================================
        //    // Declaración de constantes/variables locales
        //    // =============================================
        //    string ltComando;
        //    int liResultado = 0;

        //    try
        //    {	// On Error GoTo ManejoError

        //        ltComando = "exec sp_lce_eli_nivel_desrr "+Convert.ToString(piCodigo);
        //        modAjuste32.gbasAlce_Execute(ltComando, ref liResultado); // 

        //        Eliminar = 0;

        //        return Eliminar;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("NivelDesarrollo.Eliminar", Information.Err().Number, Information.Err().Description);
        //        Eliminar = 3;

        //    }
        //    return Eliminar;
        //}

        //public short Existe(string ptNombre, ref short outcod)
        //{
        //    short Existe = 0;
        //    // Descripción : Verifica la existencia de un nivel de desarrollo
        //    // por nombre
        //    // Parámetros  : ptNombre
        //    // Retorno     : 0 OK
        //    // 3 No existe
        //    // 4 Error en la BD
        //    // E. laterales: Ninguno
        //    // 
        //    // =============================================
        //    // Declaración de constantes/variables locales
        //    // =============================================
        //    string ltComando;
        //    int liResultado;
        //    VBtoRecordSet lrecConsulta;

        //    try
        //    {	// On Error GoTo ManejoError

        //        ltComando = "exec sp_lce_cna_nom_nivel_desrr '"+modAjuste32.qrst(ptNombre)+"'";
        //        lrecConsulta = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltComando); // dbOpenSnapshot, ' dbSQLPassThrough)
        //        if (lrecConsulta.BOF && lrecConsulta.EOF) {
        //            // No hay registro
        //            Existe = 3;
        //            outcod = 0;
        //        } else {
        //            Existe = 0;
        //            outcod = Convert.ToInt16(lrecConsulta.Fields["cod_nivel_desrr"].Value);
        //        }

        //        return Existe;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("NivelDesarrollo.Existe", Information.Err().Number, Information.Err().Description);
        //        Existe = 4;

        //    }
        //    return Existe;
        //}

        //public short Guardar()
        //{
        //    short Guardar = 0;
        //    // Descripción : Guarda un Nivel de Desarrollo
        //    // Parámetros  : Ninguno
        //    // Retorno     : 0 OK
        //    // 3 Error al guardar
        //    // E. laterales: Ninguno
        //    // 
        //    // =============================================
        //    // Declaración de constantes/variables locales
        //    // =============================================
        //    string ltComando = "";
        //    VBtoRecordSet lrecCantidad;
        //    int liResultado = 0;
        //    VBtoRecordSet lrecConsulta;
        //    string ltPercFlujo;
        //    string ltPercLimGlobal;
        //    try
        //    {	// On Error GoTo ManejoError

        //        if (mfEsNuevo) {
        //            // Asignación del código
        //            // ltComando = "select max(cod_nivel_desrr) maximo " &
        //            // "   from ta_nivel_desrr"
        //            ltComando = "sp_slc_ta_nivel_desrr_max_nivdes";
        //            lrecConsulta = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltComando); // , dbOpenSnapshot, ' dbSQLPassThrough)
        //            if (lrecConsulta.BOF && lrecConsulta.EOF) {
        //                Guardar = 4;
        //                return Guardar;
        //            } else {
        //                if (VBtoConverter.IsNull(lrecConsulta.Fields["maximo"].Value)) {
        //                    Codigo = 1;
        //                } else {
        //                    Codigo = lrecConsulta.Fields["maximo"]+1;
        //                }
        //            }
        //        }
        //        ltPercFlujo = PercFlujo.ToString();
        //        ltPercLimGlobal = PercLimGlobal.ToString();
        //        ltComando = "exec sp_lce_act_nivel_desrr "+Convert.ToString(Codigo)+",'"+modAjuste32.qrst(Nombre)+"'"+","+modLCEData.ConvertirComaPorPunto(ltPercFlujo)+","+modLCEData.ConvertirComaPorPunto(ltPercLimGlobal);
        //        // liResultado = gbasAlce_ExecuteSQL(ltComando)
        //        modAjuste32.gbasAlce_Execute(ltComando, ref liResultado);
        //        // liResultado = gbasAlce.RecordsAffected

        //        if (liResultado==1) {
        //            Guardar = 0;
        //            mfEsNuevo = false;
        //            mvarCodigo = Codigo;
        //        } else {
        //            Guardar = 3;
        //        }
        //        return Guardar;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("NivelDesarrollo.Guardar", Information.Err().Number, Information.Err().Description);
        //        Guardar = 3;

        //    }
        //    return Guardar;
        //}

        //public void CrearNuevo()
        //{

        //    mfEsNuevo = true;

        //    Codigo = 0;
        //    Nombre = string.Empty;
        //    PercFlujo = 0;
        //    PercLimGlobal = 0;
        //}

        //public short GetList(ref VBtoRecordSet precDesarrollo)
        //{
        //    short GetList = 0;
        //    // Descripción : Obtiene la lista de niveles de desarrollo
        //    // Parámetros  : Ninguno
        //    // Retorno     : 0 OK
        //    // 3 No existe
        //    // 4 Error en la base de datos
        //    // E. laterales: Ninguno
        //    // 
        //    // =============================================
        //    // Declaración de constantes/variables locales
        //    // =============================================
        //    string ltConsulta;
        //    string ltLlave = "";

        //    try
        //    {	// On Error GoTo ManejoError

        //        ltConsulta = "exec sp_lce_cna_lst_nivel_desrr";

        //        precDesarrollo = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltConsulta); // , dbOpenSnapshot,
        //        // dbSQLPassThrough)

        //        if (precDesarrollo.BOF && precDesarrollo.EOF) {
        //            GetList = 3;
        //        } else {
        //            GetList = 0;
        //        }

        //        return GetList;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("Desarrollo.Getlist", Information.Err().Number, Information.Err().Description);
        //        GetList = 4;

        //    }
        //    return GetList;
        //}
        //// VBto upgrade warning: 'Return' As short	OnWrite(int)
		public short Codigo
		{
			get
			{
					short Codigo = 0;
				// se usa cuando se asigna un valor a una propiedad, en el lado derecho de la asignación.
				// Syntax: Debug.Print X.Codigo
				Codigo = (short)mvarCodigo;
				return Codigo;
			}

			set
			{
				// se usa cuando se asigna un valor a una propiedad, en el lado izquierdo de la asignación.
				// Syntax: X.Codigo = 5
				mvarCodigo = value;
			}
		}




		public string Nombre
		{
			get
			{
					string Nombre = "";
				// se usa cuando se asigna un valor a una propiedad, en el lado derecho de la asignación.
				// Syntax: Debug.Print X.Nombre
				Nombre = mvarNombre;
				return Nombre;
			}

			set
			{
				// se usa cuando se asigna un valor a una propiedad, en el lado izquierdo de la asignación.
				// Syntax: X.Nombre = 5
				mvarNombre = value;
			}
		}


		public float PercFlujo
		{
			get
			{
					float PercFlujo = 0;
				PercFlujo = mvarPercFlujo;
				return PercFlujo;
			}

			set
			{
				mvarPercFlujo = value;
			}
		}


		public float PercLimGlobal
		{
			get
			{
					float PercLimGlobal = 0;
				PercLimGlobal = mvarPercLimGlobal;
				return PercLimGlobal;
			}

			set
			{
				mvarPercLimGlobal = value;
			}
		}



		public short Obtener(int piCodigo)
		{
			// Descripción : Obtiene un nivel de desarrollo por codigo
			// Parámetros  : piCodigo
			// Retorno     : 0 OK
			// 3 No existe
			// 4 Error en la BD
			// E. laterales: Ninguno
			// 
			// =============================================
			// Declaración de constantes/variables locales
			// =============================================
            short success = 0;
            string ltConsulta; DataTable rec;
		
            ltConsulta = "exec sp_lce_cna_nivel_desrr "+Convert.ToString(piCodigo);

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
					    Codigo = Convert.ToInt16(rec.Rows[0]["cod_nivel_desrr"]);
					    Nombre = Convert.ToString(rec.Rows[0]["gls_dscrn_nivel_desrr"])+"";
					    if (VBtoConverter.IsNull(rec.Rows[0]["Nds_por_flj_exp"])) {
						    PercFlujo = 0;
					    } else {
						    PercFlujo = Convert.ToSingle(rec.Rows[0]["Nds_por_flj_exp"]);
					    }
					    if (VBtoConverter.IsNull(rec.Rows[0]["Nds_por_lim_gbl_gpa"])) {
						    PercLimGlobal = 0;
					    } else {
						    PercLimGlobal = Convert.ToSingle(rec.Rows[0]["Nds_por_lim_gbl_gpa"]);
					    }
                        success = 0;
                    }
                }
                else
                {
                    //modLCEData.GenericError("Desarrollo.Obtener", Information.Err().Number, Information.Err().Description);
                    success = 4;

                }
            }
            return success;
		}


        //public short GetListAcuerdo(ref VBtoRecordSet precDesarrollo, int piAcuerdo)
        //{
        //    short GetListAcuerdo = 0;
        //    // Descripción : Obtiene la lista de niveles de desarrollo
        //    // Parámetros  : Id Acuerdo
        //    // Retorno     : 0 OK
        //    // 3 No existe
        //    // 4 Error en la base de datos
        //    // =============================================
        //    string ltConsulta;
        //    string ltLlave = "";

        //    try
        //    {	// On Error GoTo ManejoError

        //        ltConsulta = "exec svc_cle_lst_ado_lim_nvl_des "+Convert.ToString(piAcuerdo);

        //        precDesarrollo = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltConsulta);

        //        if (precDesarrollo.BOF && precDesarrollo.EOF) {
        //            GetListAcuerdo = 3;
        //        } else {
        //            GetListAcuerdo = 0;
        //        }

        //        return GetListAcuerdo;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("Desarrollo.GetListAcuerdo", Information.Err().Number, Information.Err().Description);
        //        GetListAcuerdo = 4;

        //    }
        //    return GetListAcuerdo;
        //}







	}
}