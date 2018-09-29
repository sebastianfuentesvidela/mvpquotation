using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using Modelos.Library;

namespace Modelos
{
    public class RelacionLimiteCotizacion
	{
        public RelacionLimiteCotizacion(String ConexionString)
            : base()
        {
            dataConnectionString = ConexionString;
        }
        private String dataConnectionString;

		//=========================================================

		// variables locales que contienen valores de propiedad
		private int mvarNumeroCotizacion;
		private int mvarCodigoLimite;
		private int mvarCorrelativoLimite;
		private DateTime mvarFechaOtorgamiento;


		public int Eliminar(int plNumeroCotizacion, int plCodigoLimite, int plCorrelativo, DateTime pxFecha)
		{
			// Descripción : Elimina una Relación Limite Cotizacion
			// Parámetros  : plNumeroCotizacion, plCodigoLimite, plCorrelativo, pxFecha
			// Retorno     : 0 OK
			// 3 Error al eliminar
			// E. laterales: Ninguno
			// 
			// =============================================
			// Declaración de constantes/variables locales
			// =============================================
			string ltComando;
            int success = 0;
            ltComando = "exec sp_lce_eli_rel_limte_ctzcn " + Convert.ToString(plNumeroCotizacion) + "," + Convert.ToString(plCodigoLimite) + "," + Convert.ToString(plCorrelativo) + ",'" + pxFecha.ToString("yyyy/mm/dd") + "'";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                success = db.ExecuteCommand(ltComando);
                if (db.errlist.Count == 0)
                {
                    success=0;
                }
                else
                {
                    //modLCEData.GenericError("RelacionLimiteCotizacion.Eliminar", Information.Err().Number, Information.Err().Description);
                    success = 3;

                }

            }
            return success;
		}

		public int EliminarCap3b5(int plNumeroCotizacion, int plCodigoLimite, int plCorrelativo, DateTime pxFecha)
		{
			// Descripción : Elimina afectacion a limite Cap 3b5 a Limite de Cotizacion
			// Parámetros  : plNumeroCotizacion, plCodigoLimite, plCorrelativo, pxFecha
			// Retorno     : 0 OK
			// 3 Error al eliminar
            string ltComando;
            int success = 0;
            ltComando = "exec sp_lce_eli_rel_limte_ctzcn " + Convert.ToString(plNumeroCotizacion) + "," + Convert.ToString(plCodigoLimite) + "," + Convert.ToString(plCorrelativo) + ",'" + pxFecha.ToString("yyyy/mm/dd") + "'";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                success = db.ExecuteCommand(ltComando);
                if (db.errlist.Count == 0)
                {
                    success = 0;
                }
                else
                {
                    //modLCEData.GenericError("RelacionLimiteCotizacion.EliminarCap3b5", Information.Err().Number, Information.Err().Description);
                    success = 3;

                }

            }
            return success;

		}

		public int NumeroCotizacion
		{
			get
			{
					int NumeroCotizacion = 0;
				NumeroCotizacion = mvarNumeroCotizacion;
				return NumeroCotizacion;
			}

			set
			{
				mvarNumeroCotizacion = value;
			}
		}


		public int Guardar()
		{
			// Descripción : Guarda una relación límite-cotización
			// Parámetros  : Ninguno
			// Retorno     : 0 OK
			// 3 Error al guardar
			// E. laterales: Ninguno
			// 
			// =============================================
			// Declaración de constantes/variables locales
			// =============================================
            string ltComando;
            int success = 0;
            int liResultado = 0;
            ltComando = "exec sp_lce_act_relcn_limte_ctzcn " + Convert.ToString(NumeroCotizacion) + "," + Convert.ToString(CodigoLimite) + "," + Convert.ToString(CorrelativoLimite) + ",'" + FechaOtorgamiento.ToString("yyyy/mm/dd") + "'";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                liResultado = db.ExecuteCommand(ltComando);
                if (db.errlist.Count == 0)
                {
                    if (liResultado == 1)
                    {
                        success = 0;
                    }
                    else
                    {
                        success = 3;
                    }
                }
                else
                {
                    //modLCEData.GenericError("RelacionLimiteCotizacion.Guardar", Information.Err().Number, Information.Err().Description);
                    success = 3;

                }

            }
            return success;

		}

		public DataTable GetList(ref short suceso, int plCodigo, int plCorrelativo)
		{
			// Descripción : Obtiene la lista de las cotizaciones que afectan
			// a un límite
			// Parámetros  : plCodigo
			// Retorno     : 0 OK
			// 3 No existe
			// 4 Error en la base de datos
			// E. laterales: Ninguno
			// 
			// =============================================
			// Declaración de constantes/variables locales
			// =============================================
            string ltConsulta;
            ltConsulta = "exec svc_lce_lst_lim_rel_ctz_lim " + Convert.ToString(plCodigo) + "," + Convert.ToString(plCorrelativo);
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataTable precLimite = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (precLimite.Rows.Count == 0)
                    {
                        suceso = 3;
                    }
                    else
                    {
                        suceso = 0;
                    }

                    return precLimite;

                }
                else
                {
                    // ManejoError:
                    //modLCEData.GenericError("RelacionLimiteCotizacion.Getlist", Information.Err().Number, Information.Err().Description);
                    suceso = 4;

                }
            }
            return null;

		}
        public DataTable GetListLimitesAfectados(ref short success, int plNroCot)
		{
			// Descripción : Obtiene la lista de límites 'que afectan a una cotizacion
			// Parámetros  : plNroCot
			// Retorno     : 0 OK
			// 3 No existe
			// 4 Error en la base de datos
			string ltConsulta = "exec svc_lce_lst_lim_rel_ctz_ctz " + Convert.ToString(plNroCot);
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

                    return precCotizacion;

                }
                else
                {
                    // ManejoError:
                    //modLCEData.GenericError("RelacionLimiteCotizacion.GetListLimitesAfectados", Information.Err().Number, Information.Err().Description);
                    success = 4;

                }
            }
            return null;
		}

        public DataTable PackInfo(DataTable dataRel, 
            String tipoOperacion, String numPersona, 
            Decimal montoAplica, bool opSimulando) 
        //private void PoblarGrilla(int codigoPlantilla, int correlativo)
        {
            short liResultado;
            Persona lperPersona = new Persona(dataConnectionString);
            CotizacionCLE lcotCotizacion = new CotizacionCLE(dataConnectionString);
            Producto lproProducto = new Producto(dataConnectionString);
            DataTable dt = new DataTable();

            DataRow dr = null;
                dt.Columns.Add("fondo", System.Type.GetType("System.String"));
                dt.Columns.Add("cotizacion", System.Type.GetType("System.String"));
                dt.Columns.Add("fechaIngreso", System.Type.GetType("System.String"));
                dt.Columns.Add("persona", System.Type.GetType("System.String"));
                dt.Columns.Add("tipo", System.Type.GetType("System.String"));
                dt.Columns.Add("aplica", System.Type.GetType("System.String"));
                dt.Columns.Add("operador", System.Type.GetType("System.String"));
                dt.Columns.Add("vencimiento", System.Type.GetType("System.String"));


            if (dataRel != null) {
                foreach (DataRow row in dataRel.Rows) {
                    //liConsulta = lcotCotizacion.Obtener(Convert.ToInt32(row["nro_ctzcn"]));
                    if (lcotCotizacion.Obtener(Convert.ToInt32(row["nro_ctzcn"])) == 0)
                    {
                        if (Array.Exists((new[] { 1, 3, 6 }), 
                            element => element == lcotCotizacion.CodigoEstado))
                        //    if (lcotCotizacion.CodigoEstado == 1 || lcotCotizacion.CodigoEstado == 3 || lcotCotizacion.CodigoEstado == 6)
                        { // Cotizada, Cursada, Aprobada
                            dr = dt.NewRow();
                            dr["fondo"] = "0"; //Color.FromArgb(220, 220, 220);
                            dr["cotizacion"] = row["nro_ctzcn"].ToString();
                            dr["fechaIngreso"] = lcotCotizacion.FechaIngreso.ToString("dd/MM/yyyy", new CultureInfo("es-ES"));
                            if (lperPersona.Obtener(lcotCotizacion.NumeroPersona) == 0)
                            {
                                dr["persona"] = lperPersona.NombreEstructurado;
                            }
                            else { dr["persona"] = string.Empty; }
                            dr["tipo"] = lcotCotizacion.TipoProducto;
                            dr["aplica"] =Decimal.Parse( row["rlc_mnt_api"].ToString()).ToString(new System.Globalization.CultureInfo("en-US"));
                            //Usuario lperUsuario;
                            //lperUsuario = new Usuario();
                            liResultado = 0; // lperUsuario.ObtieneOperador(modGlobal.ConvertirRutNro(lcotCotizacion.NumeroFuncionario));
                            if (liResultado == 0)
                            {
                                dr["operador"] = "--..--"; // lperUsuario.Nombre;
                            }
                            else { dr["operador"] = string.Empty; }
                            dr["vencimiento"] = lcotCotizacion.FechaVencimiento.ToString("dd/MM/yyyy", new CultureInfo("es-ES"));

                            dt.Rows.Add(dr);
                        }
                    }

                }
            }
            if (opSimulando)
            {
                dr = dt.NewRow();
                dr["fondo"] = "1"; //Color.FromArgb(220, 220, 220);
                dr["cotizacion"] = string.Empty;// row["nro_ctzcn"].ToString();
                dr["fechaIngreso"] = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("es-ES"));
                if (lperPersona.Obtener(numPersona) == 0)
                {
                    dr["persona"] = lperPersona.NombreEstructurado;
                }
                else { dr["persona"] = string.Empty; }

                dr["tipo"] = tipoOperacion;

                dr["aplica"] = Decimal.Parse(montoAplica.ToString()).ToString(new System.Globalization.CultureInfo("en-US")); 
                dr["operador"] = string.Empty;
                dr["vencimiento"] = string.Empty;// lcotCotizacion.FechaVencimiento.ToString();
                dt.Rows.Add(dr);
            }


            dt.AcceptChanges();
            return dt;

        }
        public int CodigoLimite
		{
			get
			{
					int CodigoLimite = 0;
				CodigoLimite = mvarCodigoLimite;
				return CodigoLimite;
			}

			set
			{
				mvarCodigoLimite = value;
			}
		}



		public int CorrelativoLimite
		{
			get
			{
					int CorrelativoLimite = 0;
				CorrelativoLimite = mvarCorrelativoLimite;
				return CorrelativoLimite;
			}

			set
			{
				mvarCorrelativoLimite = value;
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



	}
}