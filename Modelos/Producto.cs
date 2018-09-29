using System;
using Microsoft.VisualBasic;
using System.Data;
using Modelos.Library;
using Tools;

namespace Modelos
{
    public class Producto
	{

		//=========================================================

		private int mvarCodigo; // copia local
		private double mvarCuenta;
		private string mvarNombre = ""; // copia local
		private int mvarCodigoBancomex;
		private string mvarCodigoFamilia = "";
		private bool mfEsNuevo;
        
        private String dataConnectionString;

        public Producto(String ConexionString)
            : base()
        {
            dataConnectionString = ConexionString;
            mfEsNuevo = false;
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

        public DataTable GetListFamilia(ref short suceso, string ptFamilia)
		{
			// Descripción : Obtiene la lista de productos LCE que pertenecen
			// a una familia
			// Parámetros  : precProducto, ptFamilia
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
            ltConsulta = "exec sp_lce_cna_lst_cod_ctzcn_fml '" + ptFamilia + "'";
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
                    DataRow headrow = rec.NewRow();
                    headrow["cod_ctzcn_lcext"] = "0";
                    headrow["gls_dscrn_ctzcn_lcext"] = " No Definido ";
                    headrow["cod_centa_cntbl"] = "0";
                    headrow["cod_tipo_ctzcn_lcext"] = "";

                    rec.Rows.Add(headrow);

                    DataView dv = rec.DefaultView;
                    dv.Sort = "cod_ctzcn_lcext asc";
                    DataTable sortedDT = dv.ToTable();
                    return sortedDT;

                }
                else
                {
                    suceso = 4;
                    //modLCEData.GenericError("ProductoLCE.GetListFamilia", Information.Err().Number, Information.Err().Description);
                }
            }
            return rec;
		}
      
        //public short Existe(int piCodigo)
        //{
        //    short Existe = 0;
        //    // Descripción : Verifica la existencia de un ProductoLCE
        //    // Parámetros  : piCodigo
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

        //        ltComando = "exec sp_lce_cna_cod_ctzcn_lcext "+Convert.ToString(piCodigo);
        //        lrecConsulta = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltComando); // ,                        dbOpenSnapshot, dbSQLPassThrough)
        //        if (lrecConsulta.BOF && lrecConsulta.EOF) {
        //            // No hay registro
        //            Existe = 3;
        //        } else {
        //            Existe = 0;
        //        }

        //        return Existe;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("ProductoLCE.Existe", Information.Err().Number, Information.Err().Description);
        //        Existe = 4;

        //    }
        //    return Existe;
        //}

        //public short GetList(ref VBtoRecordSet precProducto)
        //{
        //    short GetList = 0;
        //    // Descripción : Obtiene la lista de productos LCE
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

        //        ltConsulta = "exec sp_lce_cna_lst_cod_ctzcn_lce ";

        //        precProducto = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltConsulta); // , dbOpenSnapshot,
        //        // dbSQLPassThrough)

        //        if (precProducto.BOF && precProducto.EOF) {
        //            GetList = 3;
        //        } else {
        //            GetList = 0;
        //        }

        //        return GetList;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("ProductoLCE.GetList", Information.Err().Number, Information.Err().Description);
        //        GetList = 4;

        //    }
        //    return GetList;
        //}

        //// VBto upgrade warning: piCodigo As short --> As int	OnWrite(VBtoField, int)
        public short Obtener(int piCodigo)
        {
            // Descripción : Obtiene un ProductoLCE por codigo
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
            ltConsulta = "exec sp_lce_cna_cod_ctzcn_lcext " + Convert.ToString(piCodigo);

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
                        Codigo = Convert.ToInt32(rec.Rows[0]["cod_ctzcn_lcext"]);
                        Cuenta = VBtoConverter.ObjectToDouble(rec.Rows[0]["cod_centa_cntbl"]);
                        Nombre = Convert.ToString(rec.Rows[0]["gls_dscrn_ctzcn_lcext"]) + "";
                        CodigoBancomex = Convert.ToInt32(rec.Rows[0]["cod_tipo_oprcn_comex"]);
                        CodigoFamilia = Convert.ToString(rec.Rows[0]["cod_tipo_ctzcn_lcext"]); // cod_fmlia_ctzcn_lcext")
                        mfEsNuevo = false;
                        success = 0;
                    }
                }
                else
                {
                    //modLCEData.GenericError("ProductoLCE.Obtener", Information.Err().Number, Information.Err().Description);
                    success = 4;

                }
            }
            return success;

        }

        //public short Guardar()
        //{
        //    short Guardar = 0;
        //    // Descripción : Guarda un ProductoLCE
        //    // Parámetros  : Ninguno
        //    // Retorno     : 0 OK
        //    // 3 Error al guardar
        //    // 4 Cuenta contable ya fue asignada
        //    // E. laterales: Ninguno
        //    // 
        //    // =============================================
        //    // Declaración de constantes/variables locales
        //    // =============================================
        //    string ltComando = "";
        //    VBtoRecordSet lrecCantidad;
        //    int liResultado = 0;
        //    VBtoRecordSet lrecConsulta;

        //    try
        //    {	// On Error GoTo ManejoError

        //        // ltComando = "select count (*) cantidad " &
        //        // "   from ta_codgo_ctzcn_lcext " &
        //        // "   where cod_centa_cntbl = " & Cuenta
        //        // 
        //        // Set lrecCantidad = GetRecordset(gbasAlce, ltComando,
        //        // dbOpenSnapshot, dbSQLPassThrough)
        //        // If lrecCantidad("cantidad") >= 2 Then
        //        // Guardar = 4
        //        // Exit Function
        //        // Else
        //        if (mfEsNuevo) {
        //            // Asignación del código
        //            // ltComando = "select max(cod_ctzcn_lcext) maximo " &
        //            // "   from ta_codgo_ctzcn_lcext"
        //            ltComando = "sp_slc_max_ta_codgo_ctzcn_lcext_cctzcn";
        //            lrecConsulta = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltComando); // ,                                dbOpenSnapshot, dbSQLPassThrough)
        //            if (lrecConsulta.BOF && lrecConsulta.EOF) {
        //                Guardar = 4;
        //                return Guardar;
        //            } else {
        //                if (VBtoConverter.IsNull(lrecConsulta.Fields["maximo"])) {
        //                    Codigo = 1;
        //                } else {
        //                    Codigo = Convert.ToInt32(lrecConsulta.Fields["maximo"]) + 1;
        //                }
        //            }
        //        }
        //        ltComando = "exec sp_lce_act_cod_ctzcn_lcext "+Convert.ToString(Codigo)+", "+Convert.ToString(Cuenta)+", '"+Nombre+"',"+Convert.ToString(CodigoBancomex)+",'"+CodigoFamilia+"'";
        //        // liResultado = gbasAlce_ExecuteSQL(ltComando)
        //        modAjuste32.gbasAlce_Execute(ltComando, ref liResultado);

        //        if (liResultado==1) {
        //            Guardar = 0;
        //            mfEsNuevo = false;
        //        } else {
        //            Guardar = 3;
        //        }
        //        // End If
        //        return Guardar;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("ProductoLCE.Guardar", Information.Err().Number, Information.Err().Description);
        //        Guardar = 3;

        //    }
        //    return Guardar;
        //}

        //public void CrearNuevo()
        //{

        //    mfEsNuevo = true;

        //    Codigo = 0;
        //    Cuenta = 0;
        //    Nombre = string.Empty;
        //    CodigoBancomex = 0;
        //    CodigoFamilia = string.Empty;
        //}
        //public short Eliminar(int piCodigo)
        //{
        //    short Eliminar = 0;
        //    // Descripción : Elimina un ProductoLCE por Codigo
        //    // Parámetros  : piCodigo
        //    // Retorno     : 0 OK
        //    // 3 Error al eliminar
        //    // 5 No se puede eliminar (el Producto está asociado
        //    // a un Tipo de Producto)
        //    // E. laterales: Ninguno
        //    // 
        //    // =============================================
        //    // Declaración de constantes/variables locales
        //    // =============================================
        //    string ltComando;
        //    int liResultado = 0;
        //    string ltConsulta = "";

        //    try
        //    {	// On Error GoTo ManejoError

        //        // *** Pendiente considerar que el Producto está asociado
        //        // a un Tipo de Producto ***

        //        ltComando = "exec sp_lce_eli_cod_ctzcn_lcext "+Convert.ToString(piCodigo);
        //        // liResultado = gbasAlce_ExecuteSQL(ltComando)
        //        modAjuste32.gbasAlce_Execute(ltComando, ref liResultado);

        //        if (liResultado==1) {
        //            Eliminar = 0;
        //        } else {
        //            Eliminar = 3;
        //        }

        //        return Eliminar;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("ProductoLCE.Eliminar", Information.Err().Number, Information.Err().Description);
        //        Eliminar = 3;

        //    }
        //    return Eliminar;
        //}

		public string Nombre
		{
			set
			{
				// se usa cuando se asigna un valor a una propiedad, en el lado izquierdo de la asignación.
				// Syntax: X.Nombre = 5
				if (Strings.Trim(value).Length>80) {
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






        public int Codigo
		{
			set
			{
				// se usa cuando se asigna un valor a una propiedad, en el lado izquierdo de la asignación.
				// Syntax: X.Codigo = 5
				mvarCodigo = value;
			}
		// VBto upgrade warning: 'Return' As short	OnWrite(int)

			get
			{
                int Codigo = 0;
				// se usa cuando se asigna un valor a una propiedad, en el lado derecho de la asignación.
				// Syntax: Debug.Print X.Codigo
				Codigo = mvarCodigo;
				return Codigo;
			}
		}






		public double Cuenta
		{
			get
			{
					double Cuenta = 0;
				Cuenta = mvarCuenta;
				return Cuenta;
			}

			set
			{
				// Dim liRespuesta As Integer
				// Dim lcueCuenta As New Cuenta

				// If mfEsNuevo Then
				mvarCuenta = value;
				// Else
				// liRespuesta = lcueCuenta.Existe(vData)
				// If liRespuesta = 0 Then
				// mvarCuenta = vData
				// Else
				// Err.Raise 30003, "ProductoLCE.Cuenta", "Cuenta Contable no existe"
				// End If
				// Set lcueCuenta = Nothing
				// End If
			}
		}


		// VBto upgrade warning: 'Return' As short	OnWrite(int)
        public int CodigoBancomex
		{
			get
			{
                int CodigoBancomex = 0;
				CodigoBancomex = mvarCodigoBancomex;
				return CodigoBancomex;
			}

			set
			{
				mvarCodigoBancomex = value;
			}
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



	}
}