using System;
using Microsoft.VisualBasic;
using System.Data;
using Modelos.Library;
using System.Data.SqlClient;
using Tools;

namespace Modelos
{
	public class Persona
	{

		//=========================================================

		// Datos de Persona
		private string mvarNumero = "";
		private int mvarCodigoTipo;
		private string mvarNumeroDocumento = "";
		private string mvarNombreEstructurado = "";
		private string mvarNombreCompactado = "";
		private int mvarCodigoPais;
		// VBto upgrade warning: mvarCodigoIdiomaOperar As short --> As int	OnWrite(string)
		private int mvarCodigoIdiomaOperar;
		private bool mvarEsJuridica;
		private bool mvarEsRutFicticio;
		private DateTime mvarFechaIngreso;
		private DateTime mvarFechaUltimo;
		private int mvarTipoDocumento;
		private bool mvarEsNombreLegal;

		// Dirección Persona
		private int mvarNumeroDireccion;
		private string mvarCodigoFormatoDireccion = "";
		private bool mvarMarcaEsErronea;
		private DateTime mvarFechaEsErronea;
		private string mvarCodigoComuna = "";
		private string mvarCodigoCiudad = "";
		private string mvarCodigoPostal = "";
		private string mvarGlosaDireccion1 = "";
		private string mvarGlosaDireccion2 = "";
		private bool mvarMarcaVerificada;
		private DateTime mvarFechaVerificacion;
		private DateTime mvarFechaUltimaDireccion;

        private String dataConnectionString;

        public Persona(String ConexionString)
            : base()
        {
            dataConnectionString = ConexionString;
        }
	
        public DataTable GetListLineas(ref short success, string pcNroPersona)
		{
			// Descripción : Obtiene la lista de personas de un Pais Determinado
			// Parámetros  : piPais
			// Retorno     : 0 OK
			// 3 No existe
			// 4 Error en la base de datos
			// E. laterales: Ninguno
			// 
			// =============================================
			// Declaración de constantes/variables locales
			// =============================================
            string ltConsulta; DataTable rec;
            ltConsulta = "exec svc_lce_lst_lin_cli '" + pcNroPersona + "'";
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
                    //modLCEData.GenericError("Persona.GetListLineas", Information.Err().Number, Information.Err().Description);
                    success = 4;

                }
            }
            return rec;

        }
        public Customers GetCustomers(ref short success)
        {
            // Descripción : Obtiene la lista de personas de un Pais Determinado
            // Parámetros  : piPais
            // Retorno     : 0 OK
            // 3 No existe
            // 4 Error en la base de datos
            // E. laterales: Ninguno

            string ltConsulta; DataTable rec;
            ltConsulta = "SELECT TOP 10 [pge_val_cod] AS CustomerId ,[pge_des_val] AS ContactName,[pge_tbl_cod] AS City,[pge_tbl_cod] AS Country FROM [bcle_lim_ext].[dbo].[tcle_pra_gen] AS Customers";
            SqlCommand cmd = new SqlCommand(ltConsulta);
            using (SqlConnection con = new SqlConnection(dataConnectionString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;

                    sda.SelectCommand = cmd;
                    using (Customers dsCustomers = new Customers())
                    {
                        try
                        {
                            sda.Fill(dsCustomers, "DataTable1");
                        }
                        catch (SqlException ex)
                        {
                            string nada;
                            nada = "nada";
                        }
                        finally { }
                        return dsCustomers;
                    }

                }
            }
        }

        public DataTable GetListxPais(ref short success, int piPais)
		{
			// Descripción : Obtiene la lista de personas de un Pais Determinado
			// Parámetros  : piPais
			// Retorno     : 0 OK
			// 3 No existe
			// 4 Error en la base de datos
			// E. laterales: Ninguno
			// 
			// =============================================
			// Declaración de constantes/variables locales
			// =============================================
            string ltConsulta; DataTable rec;
            ltConsulta = "exec svc_lce_lst_cli_pai " + Convert.ToString(piPais);
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
                    //modLCEData.GenericError("Persona.GetListxPais", Information.Err().Number, Information.Err().Description);
                    success = 4;

                }
            }
            return rec;
		}


        public DataTable GetListRestricciones(ref short success, string pcNroPersona)
		{
			// Descripción : Obtiene la lista de personas de un Pais Determinado
			// Parámetros  : piPais
			// Retorno     : 0 OK
			// 3 No existe
			// 4 Error en la base de datos
			// E. laterales: Ninguno
			// 
			// =============================================
			// Declaración de constantes/variables locales
			// =============================================
            string ltConsulta; DataTable rec;
            ltConsulta = "exec svc_cle_obt_rst_cli '" + Global.ConvertirRutNro(pcNroPersona) + "'";
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
                        foreach (DataRow row in rec.Rows)
                        {
                            row["gls_dsc_rst"] = row["gls_dsc_rst"].ToString().Replace("   ", "\r\n"); 
                        }
                    }
                }
                else
                {
                    //modLCEData.GenericError("Persona.GetListRestricciones", Information.Err().Number, Information.Err().Description);
                    success = 4;

                }
            }
            return rec;
		}

        public DataTable GetListRiesgo(ref short success, string pcNroPersona)
		{
			// Descripción : Obtiene la lista de personas de un Pais Determinado
			// Parámetros  : piPais
			// Retorno     : 0 OK
			// 3 No existe
			// 4 Error en la base de datos
			// E. laterales: Ninguno
			// 
			// =============================================
			// Declaración de constantes/variables locales
			// =============================================
            string ltConsulta; DataTable rec;
            ltConsulta = "svc_cle_lst_cla_rie_por_cli '" + Global.ConvertirRutNro(pcNroPersona) + "'";
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
                    //modLCEData.GenericError("Persona.GetListRestricciones", Information.Err().Number, Information.Err().Description);
                    success = 4;

                }
            }
            return rec;
		}

        public DataTable GetList(ref short success,int piTipoPersona)
		{
			// Descripción : Obtiene la lista de personas según código tipo persona
			// Parámetros  : piTipoPersona
			// Retorno     : 0 OK
			// 3 No existe
			// 4 Error en la base de datos
			// E. laterales: Ninguno
			// 
			// =============================================
			// Declaración de constantes/variables locales
			// =============================================
            string ltConsulta; DataTable rec;
		    ltConsulta = "exec sva_cle_lst_per_lst "+Convert.ToString(piTipoPersona);
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
                        rec.Columns.Add("rutcliente");
                        foreach (DataRow r in rec.Rows)
                        {
                            r["RutCliente"] = Global.ConvertirNroRut(r["nro_prsna"].ToString()); 
                        }

                        success = 0;
                    }
                }
                else
                {
                    // GenericError "Persona.GetList", Err.Number, Err.Description
                    success = 4;

                }
            }
            return rec;
		}

        public string TieneLineasVigentes()
		{
			string Msg = "";
            string ltConsulta; DataTable rec;
            ltConsulta = "svc_cle_lst_vig_lim_seg_cli ";
            ltConsulta += " '" + Global.ConvertirRutNro(Numero) + "'";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                rec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count == 0)
                    {
                        Msg = "El Cliente RUT: " + Numero + " " + NombreEstructurado + ", No posee Ningún Limite Aplicable";
                        //success = 3;
                    }
                    else
                    {
                        foreach (DataRow row in rec.Rows) //while (!lrecConsulta.EOF)
                        {
                            if (DateAndTime.DateDiff("d", DateTime.Now, (DateTime)row[0], FirstDayOfWeek.System, FirstWeekOfYear.System) < 10)
                            {
                                // Msg = Msg & vbCrLf & lrecConsulta(0) & ": " & lrecConsulta(1)
                                Msg = "Existen Lineas Del Cliente RUT: " + Numero + " " + NombreEstructurado + " que vencen el " + row[0].ToString() + ".  ";
                            }
                        }
                        if (Msg.Length == 0)
                        {
                            Msg = "OK";
                        }
                    }
                }
                else
                {
                    //modLCEData.GenericError("Persona.TieneLineasVigentes", Information.Err().Number, Information.Err().Description);
                    Msg = "El Cliente RUT: " + Numero + " " + NombreEstructurado + ", No posee Ningún Limite Aplicable";
                }
            }
            return Msg;
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
            ltConsulta = "exec sp_pnj_cnsta_datos_prsna '" + Strings.Trim(ptNumero) + "',''";

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
                        Numero = Global.ConvertirNroRut(ptNumero) + "";
                        if (VBtoConverter.IsNull(rec.Rows[0]["cod_tipo_prsna"]))
                        {
                            CodigoTipo = 1;
                        }
                        else
                        {
                            CodigoTipo = Convert.ToInt32(rec.Rows[0]["cod_tipo_prsna"]);
                        }
                        NumeroDocumento = Convert.ToString(rec.Rows[0]["nro_idtfr_prsna"]) + "";
                        NombreEstructurado = Strings.Trim(Convert.ToString(rec.Rows[0]["nom_prsna_etcdo"]) + "");
                        NombreCompactado = Strings.Trim(Convert.ToString(rec.Rows[0]["nom_prsna_cmpdo"]) + "");
                        CodigoPais = Convert.ToInt32(rec.Rows[0]["cod_pais"]);
                        CodigoIdiomaOperar = Convert.ToString(rec.Rows[0]["cod_idiom"]);
                        if (rec.Rows[0]["flg_prsna_jrdca"] == "0")
                        {
                            EsJuridica = false;
                        }
                        else
                        {
                            EsJuridica = true;
                        }
                        if (rec.Rows[0]["flg_rut_fictc"] == "0")
                        {
                            EsRutFicticio = false;
                        }
                        else
                        {
                            EsRutFicticio = true;
                        }

                        success = 0;
                    }
                }
                else
                {
                    // GenericError "Persona.Obtener", Err.Number, Err.Description
                    success = 4;

                }
            }
            return success;
		}


        public short Guardar()
		{
			// Descripción : Guarda una Persona y su direccion
			// Parámetros  : Ninguno
			// Retorno     : 0 OK
			// 3 Error al guardar la persona
			// 4 Error al guardar la direccion
			// E. laterales: Ninguno
			// 
			// =============================================
			// Declaración de constantes/variables locales
			// =============================================
            int liJuridica;
            int liEsRutFicticio;
            int liEsNombreLegal;
            string ltCodigoTipo = "";
            string ltCodigoPais = "";

            string ltComando;
            short success = 0;
            liJuridica = (EsJuridica ? 1: 0);
            liEsRutFicticio = (EsRutFicticio ? 1: 0);
            liEsNombreLegal = (EsNombreLegal ? 1: 0);
            ltCodigoTipo = ((CodigoTipo == -1)? "null": CodigoTipo.ToString());
            ltCodigoPais = ((CodigoPais == -1)? "null": CodigoPais.ToString());
            Numero = Global.ConvertirRutNro(Numero);
            // bd_persona..
            ltComando = "exec sp_lce_act_datos_prsna '" + Numero + "'," + ltCodigoTipo + "," + ltCodigoPais;
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
                    //modLCEData.GenericError("Persona.Guardar", Information.Err().Number, Information.Err().Description);
                    success = 3;

                }

            }
            return success;
		}

		public void CrearNuevo()
		{

			Numero = string.Empty;
			CodigoTipo = 0;
			NumeroDocumento = string.Empty;
			NombreEstructurado = string.Empty;
			NombreCompactado = string.Empty;
			CodigoPais = 0;
			CodigoIdiomaOperar = Convert.ToString(0);
			EsJuridica = false;
			EsRutFicticio = false;
			FechaIngreso = DateTime.FromOADate(0);
			FechaUltimo = DateTime.FromOADate(0);
			TipoDocumento = 0;
			EsNombreLegal = false;

			NumeroDireccion = 0;
			CodigoFormatoDireccion = string.Empty;
			MarcaEsErronea = false;
			FechaEsErronea = DateTime.FromOADate(0);
			CodigoComuna = string.Empty;
			CodigoCiudad = string.Empty;
			CodigoPostal = string.Empty;
			GlosaDireccion1 = string.Empty;
			GlosaDireccion2 = string.Empty;
			MarcaVerificada = true;
			FechaVerificacion = DateTime.FromOADate(0);
			FechaUltimaDireccion = DateTime.FromOADate(0);
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
            ltComando = "exec sp_lce_eli_prsna '" + ptNumero + "'";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                int success = db.ExecuteCommand(ltComando);
                if (db.errlist.Count == 0)
                {
                    suceso = 0;
                }
                else
                {
                    //modLCEData.GenericError("Persona.Eliminar", Information.Err().Number, Information.Err().Description);
                    suceso = 3;

                }

            }
            return suceso;
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



		// VBto upgrade warning: 'Return' As short	OnWrite(int)
		public int CodigoTipo
		{
			get
			{
                int CodigoTipo = 0;
				CodigoTipo = mvarCodigoTipo;
				return CodigoTipo;
			}

			set
			{
				mvarCodigoTipo = value;
			}
		}



		public string NumeroDocumento
		{
			get
			{
					string NumeroDocumento = "";
				NumeroDocumento = mvarNumeroDocumento;
				return NumeroDocumento;
			}

			set
			{
				mvarNumeroDocumento = value;
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



		public string NombreCompactado
		{
			get
			{
					string NombreCompactado = "";
				NombreCompactado = mvarNombreCompactado;
				return NombreCompactado;
			}

			set
			{
				mvarNombreCompactado = value;
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



		// VBto upgrade warning: 'Return' As string	OnWrite(int)
		public string CodigoIdiomaOperar
		{
			get
			{
					string CodigoIdiomaOperar = "";
				CodigoIdiomaOperar = Convert.ToString(mvarCodigoIdiomaOperar);
				return CodigoIdiomaOperar;
			}
		// VBto upgrade warning: vData As string	OnRead(int)

			set
			{
				mvarCodigoIdiomaOperar = Convert.ToInt32(value);
			}
		}



		public bool EsJuridica
		{
			get
			{
					bool EsJuridica = false;
				EsJuridica = mvarEsJuridica;
				return EsJuridica;
			}

			set
			{
				mvarEsJuridica = value;
			}
		}



		public bool EsRutFicticio
		{
			get
			{
					bool EsRutFicticio = false;
				EsRutFicticio = mvarEsRutFicticio;
				return EsRutFicticio;
			}

			set
			{
				mvarEsRutFicticio = value;
			}
		}









		// VBto upgrade warning: 'Return' As short	OnWrite(int)
		public short NumeroDireccion
		{
			get
			{
					short NumeroDireccion = 0;
				NumeroDireccion = (short)mvarNumeroDireccion;
				return NumeroDireccion;
			}

			set
			{
				mvarNumeroDireccion = value;
			}
		}



		public string CodigoFormatoDireccion
		{
			get
			{
					string CodigoFormatoDireccion = "";
				CodigoFormatoDireccion = mvarCodigoFormatoDireccion;
				return CodigoFormatoDireccion;
			}

			set
			{
				mvarCodigoFormatoDireccion = value;
			}
		}



		public bool MarcaEsErronea
		{
			get
			{
					bool MarcaEsErronea = false;
				MarcaEsErronea = mvarMarcaEsErronea;
				return MarcaEsErronea;
			}

			set
			{
				mvarMarcaEsErronea = value;
			}
		}



		public DateTime FechaEsErronea
		{
			get
			{
					DateTime FechaEsErronea = System.DateTime.Now;
				FechaEsErronea = mvarFechaEsErronea;
				return FechaEsErronea;
			}

			set
			{
				mvarFechaEsErronea = value;
			}
		}



		public string CodigoComuna
		{
			get
			{
					string CodigoComuna = "";
				CodigoComuna = mvarCodigoComuna;
				return CodigoComuna;
			}

			set
			{
				mvarCodigoComuna = value;
			}
		}



		public string CodigoCiudad
		{
			get
			{
					string CodigoCiudad = "";
				CodigoCiudad = mvarCodigoCiudad;
				return CodigoCiudad;
			}

			set
			{
				mvarCodigoCiudad = value;
			}
		}



		public string CodigoPostal
		{
			get
			{
					string CodigoPostal = "";
				CodigoPostal = mvarCodigoPostal;
				return CodigoPostal;
			}

			set
			{
				mvarCodigoPostal = value;
			}
		}



		public string GlosaDireccion1
		{
			get
			{
					string GlosaDireccion1 = "";
				GlosaDireccion1 = mvarGlosaDireccion1;
				return GlosaDireccion1;
			}

			set
			{
				mvarGlosaDireccion1 = value;
			}
		}



		public string GlosaDireccion2
		{
			get
			{
					string GlosaDireccion2 = "";
				GlosaDireccion2 = mvarGlosaDireccion2;
				return GlosaDireccion2;
			}

			set
			{
				mvarGlosaDireccion2 = value;
			}
		}



		public bool MarcaVerificada
		{
			get
			{
					bool MarcaVerificada = false;
				MarcaVerificada = mvarMarcaVerificada;
				return MarcaVerificada;
			}

			set
			{
				mvarMarcaVerificada = value;
			}
		}



		public DateTime FechaVerificacion
		{
			get
			{
					DateTime FechaVerificacion = System.DateTime.Now;
				FechaVerificacion = mvarFechaVerificacion;
				return FechaVerificacion;
			}

			set
			{
				mvarFechaVerificacion = value;
			}
		}



		public DateTime FechaUltimaDireccion
		{
			get
			{
					DateTime FechaUltimaDireccion = System.DateTime.Now;
				FechaUltimaDireccion = mvarFechaUltimaDireccion;
				return FechaUltimaDireccion;
			}

			set
			{
				mvarFechaUltimaDireccion = value;
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



		public DateTime FechaUltimo
		{
			get
			{
					DateTime FechaUltimo = System.DateTime.Now;
				FechaUltimo = mvarFechaUltimo;
				return FechaUltimo;
			}

			set
			{
				mvarFechaUltimo = value;
			}
		}



		public short TipoDocumento
		{
			get
			{
					short TipoDocumento = 0;
				mvarTipoDocumento = TipoDocumento;
				return TipoDocumento;
			}

			set
			{
				mvarTipoDocumento = value;
			}
		}



		public bool EsNombreLegal
		{
			get
			{
					bool EsNombreLegal = false;
				mvarEsNombreLegal = EsNombreLegal;
				return EsNombreLegal;
			}

			set
			{
				mvarEsNombreLegal = value;
			}
		}



		public string NroCasaMatriz(string nro_pers)
		{
            string NroCasaMatriz = string.Empty;
            CasaMatriz loCasaMatriz = new CasaMatriz(dataConnectionString);
            if (loCasaMatriz.Obtener(nro_pers) == 0)
            {
				NroCasaMatriz = loCasaMatriz.Numero;
			}
            loCasaMatriz = null;
			return NroCasaMatriz;
		}

		// VBto upgrade warning: 'Return' As short	OnWrite(int)
        //public short ConsultaVigenciaCliente(string numper, ref string psNombre, ref short piCodEjec, ref string psEjecut, ref string psErrMessage)
        //{
        //    short ConsultaVigenciaCliente = 0;
        //    string nomcli = "", ptNumero; int liResultado; string ltConsulta;
        //    VBtoRecordSet lrecCics; object ltMensaje; bool okCli; string ltComando = ""; int lires;
        //    try
        //    {	// On Error GoTo confnomerror
        //        ptNumero = modGlobal.ConvertirRutNro(numper);
        //        if (ptNumero==string.Empty) return ConsultaVigenciaCliente;
        //        ltConsulta = "exec bcen_cli..Svc_BccBus_Dat001 '"+Strings.Trim(ptNumero)+"',''";
        //        Application.DoEvents();
        //        lrecCics = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltConsulta); // db_ConnectionString
        //        okCli = true;
        //        if (lrecCics.EOF) {
        //            okCli = false;
        //        } else {
        //            if (Strings.Left(Strings.UCase(Convert.ToString(lrecCics.Fields["nombre"].Value)), "CLIENTE NO EXISTE..".Length)=="CLIENTE NO EXISTE..") okCli = false;
        //        }
        //        if (!okCli) {
        //            psNombre = "No existe Cliente";
        //            piCodEjec = 0;
        //            psEjecut = string.Empty;
        //            ConsultaVigenciaCliente = 0;
        //            VBtoConverter.ScreenCursor = Cursors.Default;
        //            return ConsultaVigenciaCliente;
        //        } else {
        //            int stat = 0;
        //            // import client data
        //            psNombre = Strings.Trim(string.Empty+Convert.ToString(lrecCics.Fields["nombre"].Value));
        //            // If Trim$(psNombre) = vbNullString Then
        //            // a = a
        //            // End If
        //            stat = (VBtoConverter.IsNull(lrecCics.Fields["est_cliente"].Value) ? 0 : lrecCics.Fields["est_cliente"].Value);
        //            // If stat = "1" Then
        //            // TotalActivos = TotalActivos + 1
        //            // End If
        //            piCodEjec = Convert.ToInt16((VBtoConverter.IsNull(lrecCics.Fields["cod_ejec"].Value) ? 0 : lrecCics.Fields["cod_ejec"].Value));
        //            if (!(piCodEjec>0)) {
        //                // psEjecut = GetEjecutivo(piCodEjec, Trim$(ptNumero))
        //                // Else
        //                psEjecut = string.Empty; // IIf(IsNull(lrecCics("varios6")), 0, lrecCics("varios6"))
        //            }
        //            ConsultaVigenciaCliente = (short)stat;
        //            VBtoConverter.ScreenCursor = Cursors.Default;
        //            return ConsultaVigenciaCliente;
        //        }
        //    }
        //    catch
        //    {	// confnomerror:
        //        VBtoConverter.ScreenCursor = Cursors.Default;
        //        psErrMessage = Information.Err().Description;
        //        // MsgBox psErrMessage
        //    }
        //    return ConsultaVigenciaCliente;
        //}

	}
}