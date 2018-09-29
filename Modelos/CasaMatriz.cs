using System;
using Microsoft.VisualBasic;
using System.Data;
using Modelos.Library;
using Tools;

namespace Modelos
{
	public class CasaMatriz
	{

		//=========================================================
		private string mvarChild = "";
		private string mvarNumero = "";
		private string mvarNombreEstructurado = "";

        private String dataConnectionString;

        public CasaMatriz(String ConexionString)
            : base()
        {
            dataConnectionString = ConexionString;
        }

        
        public string Child
		{
			get
			{
					string Child = "";
				Child = mvarChild;
				return Child;
			}

			set
			{
				mvarChild = value;

			}
		}


		public string Numero
		{
			get
			{
					string Numero = "";
				Numero = mvarNumero;
				return Numero;
			}

			set
			{
				mvarNumero = value;
			}
		}


		public string NombreEstructurado
		{
			get
			{
					string NombreEstructurado = "";
				NombreEstructurado = mvarNombreEstructurado;
				return NombreEstructurado;
			}

			set
			{
				mvarNombreEstructurado = value;
			}
		}

		public short Obtener(string ptNumero)
		{
			// Descripción : Obtiene una persona por número
			// Parámetros  : ptNumero
			// Retorno     : 0 OK
			// 3 No existe Persona
			// 4 Error en la BD
			// 5 No existe Direccion
			// E. laterales: Ninguno
			// 
			// =============================================
			// Declaración de constantes/variables locales
			// =============================================
            short success = 0;
            string ltConsulta; DataTable rec;
            ptNumero = Global.ConvertirRutNro(ptNumero);
            // bd_persona..
            ltConsulta = "exec svc_dat_mat_rut_cli '" + Strings.Trim(ptNumero) + "'";

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
                        Child = Strings.Trim(ptNumero);
                        Numero = Global.ConvertirNroRut(Convert.ToString(rec.Rows[0]["nro_prsna_relcn"])) + "";
                        NombreEstructurado = Convert.ToString(rec.Rows[0]["nom_prsna_etcdo"]) + "";
                        success = 0;
                    }
                }
                else
                {
                    // GenericError "CasaMatriz.Obtener", Err.Number, Err.Description
                    success = 4;

                }
            }
            return success;
		}
		public short Eliminar(string ptNumero)
		{
			// Descripción : Elimina una Persona por Numero
			// Parámetros  : ptNumero
			// Retorno     : 0 OK
			// 3 Error al eliminar
			// E. laterales: Ninguno
			// 
			// =============================================
			// Declaración de constantes/variables locales
			// =============================================
            string ltComando;
            short suceso = 0;
            ltComando = "exec sva_lce_eli_cas_mat '" + ptNumero + "'";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                int success = db.ExecuteCommand(ltComando);
                if (db.errlist.Count == 0)
                {
                    suceso = 0;
                }
                else
                {
                    //modLCEData.GenericError("CasaMatriz.Eliminar", Information.Err().Number, Information.Err().Description);
                    suceso = 3;

                }

            }
            return suceso;
		}
		public short Guardar()
		{
			// VBto upgrade warning: lNumero As Variant --> As string
			// VBto upgrade warning: lChild As Variant --> As string

			// Descripción : Guarda una Casa Matriz
			// Parámetros  : Ninguno
			// Retorno     : 0 OK
			// 3 Error al guardar CM
			// 4 Error al guardar CM
			// E. laterales: Ninguno
			// 
			// =============================================
			// Declaración de constantes/variables locales
			// =============================================
            string ltComando;
            short success = 0;
            string lNumero, lChild;	// - "AutoDim"
            lNumero = Global.ConvertirRutNro(Numero);
            lChild = Global.ConvertirRutNro(Child);

            ltComando = "exec sva_dat_mat_cli '" + lChild + "','" + lNumero + "'";

            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                int resultado = db.ExecuteCommand(ltComando);
                if (db.errlist.Count == 0)
                {
                    if (resultado == 1)
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
                    //modLCEData.GenericError("CasaMatriz.Modificar", Information.Err().Number, Information.Err().Description);
                    success = 3;

                }

            }
            return success;
		}
	}
}