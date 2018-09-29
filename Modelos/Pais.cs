using System;
using Microsoft.VisualBasic;
using System.Data;
using Modelos.Library;
using Tools;

namespace Modelos
{
	public class Pais
	{

		//=========================================================

		private int mvarCodigo; // copia local
		private string mvarNombre = ""; // copia local
		private string mvarGentilicio; // copia local
		private int mvarAladi; // copia local
		private int mvarContinente; // 
		private int mvarRegion;
		private int mvarDesarrollo;
		private Decimal mvarFlujoExport;
		private float mvarPercAdmit;
		private Decimal mvarMaxPotencial;
		private bool mfEsNuevo; // Indica si es Nuevo o existente
		public int TotBancos;

        private String dataConnectionString;

        public Pais(String ConexionString)
            : base()
        {
            dataConnectionString = ConexionString;
            mfEsNuevo = false;
        }

		public short Aladi
		{
			set
			{
				// se usa cuando se asigna un valor a una propiedad, en el lado izquierdo de la asignación.
				// Syntax: X.Codigo = 5
				mvarAladi = value;

			}
		// VBto upgrade warning: 'Return' As short	OnWrite(int)

			get
			{
					short Aladi = 0;
				// se usa cuando se asigna un valor a una propiedad, en el lado derecho de la asignación.
				// Syntax: Debug.Print X.Codigo
				Aladi = (short)mvarAladi;

				return Aladi;
			}
		}



		public string Gentilicio
		{
			set
			{
				// se usa cuando se asigna un valor a una propiedad, en el lado izquierdo de la asignación.
				// Syntax: X.Nombre = 5
				mvarGentilicio = value;
			}

			get
			{
					string Gentilicio = "";
				// se usa cuando se asigna un valor a una propiedad, en el lado derecho de la asignación.
				// Syntax: Debug.Print X.Codigo
				Gentilicio = mvarGentilicio;
				return Gentilicio;
			}
		}



		// VBto upgrade warning: piCodigo As short --> As int	OnWrite(int, VBtoField)
		public short Obtener(int piCodigo)
		{
			// Descripción : Obtiene un país por codigo
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
            ltConsulta = "exec svc_cle_obt_pai_por_cod " + Convert.ToString(piCodigo);

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
                        Codigo = Convert.ToInt32(rec.Rows[0]["cod_pais"]);
                        Nombre = Convert.ToString(rec.Rows[0]["gls_pais"]) + "";
                        if (VBtoConverter.IsNull(rec.Rows[0]["gls_gntlc_pais"]))
                        {
                            Gentilicio = string.Empty;
                        }
                        else
                        {
                            Gentilicio = Convert.ToString(rec.Rows[0]["gls_gntlc_pais"]) + "";
                        }
                        Aladi = Convert.ToInt16(rec.Rows[0]["cod_aladi"]);
                        if (VBtoConverter.IsNull(rec.Rows[0]["cod_cntnt"]))
                        {
                            Continente = -1;
                        }
                        else
                        {
                            Continente = Convert.ToInt32(rec.Rows[0]["cod_cntnt"]);
                        }
                        if (VBtoConverter.IsNull(rec.Rows[0]["cod_rgion_geogr"]))
                        {
                            Region = -1;
                        }
                        else
                        {
                            Region = Convert.ToInt32(rec.Rows[0]["cod_rgion_geogr"]);
                        }
                        if (VBtoConverter.IsNull(rec.Rows[0]["cod_nivel_desrr"]))
                        {
                            Desarrollo = -1;
                        }
                        else
                        {
                            Desarrollo = Convert.ToInt32(rec.Rows[0]["cod_nivel_desrr"]);
                        }
                        if (VBtoConverter.IsNull(rec.Rows[0]["Pai_mnt_flj_exp"]))
                        {
                            FlujoExportaciones = 0;
                        }
                        else
                        {
                            FlujoExportaciones = Convert.ToDecimal(rec.Rows[0]["Pai_mnt_flj_exp"]);
                        }
                        if (VBtoConverter.IsNull(rec.Rows[0]["Pai_por_lim_grp"]))
                        {
                            PercAdmitido = 0;
                        }
                        else
                        {
                            PercAdmitido = Convert.ToSingle(rec.Rows[0]["Pai_por_lim_grp"]);
                        }
                        if (VBtoConverter.IsNull(rec.Rows[0]["Pai_mnt_max_pnc"]))
                        {
                            MaxPotencial = 0;
                        }
                        else
                        {
                            MaxPotencial = Convert.ToDecimal(rec.Rows[0]["Pai_mnt_max_pnc"]);
                        }
                        TotBancos = Convert.ToInt32(rec.Rows[0]["TotBancos"]);
                        mfEsNuevo = false;
                        success = 0;
                    }
                }
                else
                {
                    //modLCEData.GenericError("Pais.Obtener", Information.Err().Number, Information.Err().Description);
                    success = 4;

                }
            }
            return success;
		}


        //public short Existe(int piCodigo)
        //{
        //    short Existe = 0;
        //    // Descripción : Verifica la existencia de un país por codigo
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

        //        ltComando = "exec sp_lce_cna_pais "+Convert.ToString(piCodigo);
        //        lrecConsulta = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltComando); // 
        //        // ,           dbOpenSnapshot, ' dbSQLPassThrough)
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
        //        modLCEData.GenericError("Pais.Existe", Information.Err().Number, Information.Err().Description);
        //        Existe = 4;


        //    }
        //    return Existe;
        //}

        //public short GetList(ref VBtoRecordSet precPais)
        //{
        //    short GetList = 0;
        //    // Descripción : Obtiene la lista de paises
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

        //        ltConsulta = "exec svc_cle_cna_lst_pai_vig";

        //        precPais = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltConsulta); // , dbOpenSnapshot,
        //        // dbSQLPassThrough)

        //        if (precPais.BOF && precPais.EOF) {
        //            GetList = 3;
        //        } else {
        //            GetList = 0;
        //        }

        //        return GetList;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("Pais.Getlist", Information.Err().Number, Information.Err().Description);
        //        GetList = 4;

        //    }
        //    return GetList;
        //}
        //public short GetListConOpes(ref VBtoRecordSet precPais)
        //{
        //    short GetListConOpes = 0;
        //    // Descripción : Obtiene la lista de paises
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

        //        ltConsulta = "exec svc_cle_cna_lst_pai_ope";
        //        // ltConsulta = "exec sp_lce_cna_lst_pais"

        //        precPais = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltConsulta); // , dbOpenSnapshot,
        //        // dbSQLPassThrough)

        //        if (precPais.BOF && precPais.EOF) {
        //            GetListConOpes = 3;
        //        } else {
        //            GetListConOpes = 0;
        //        }

        //        return GetListConOpes;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("Pais.GetListCOnOpes", Information.Err().Number, Information.Err().Description);
        //        GetListConOpes = 4;

        //    }
        //    return GetListConOpes;
        //}

		// VBto upgrade warning: 'Return' As short	OnWrite(int)
		public int Codigo
		{
			get
			{
					int Codigo = 0;
				// se usa cuando se asigna un valor a una propiedad, en el lado derecho de la asignación.
				// Syntax: Debug.Print X.Codigo
				Codigo = mvarCodigo;
				return Codigo;
			}

			set
			{
				// se usa cuando se asigna un valor a una propiedad, en el lado izquierdo de la asignación.
				// Syntax: X.Codigo = 5
				if (value<0) {
					Information.Err().Raise(100, null, "El Código no acepta valor negativo", null,null);
				} else {
					mvarCodigo = value;
				}
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



		// VBto upgrade warning: 'Return' As short	OnWrite(int)
        public int Continente
		{
			get
			{
                int Continente = 0;
				Continente = mvarContinente;
				return Continente;
			}

			set
			{
				mvarContinente = value;
			}
		}



		// VBto upgrade warning: 'Return' As short	OnWrite(int)
        public int Region
		{
			get
			{
                int Region = 0;
				Region = mvarRegion;
				return Region;
			}

			set
			{
				mvarRegion = value;
			}
		}



		// VBto upgrade warning: 'Return' As short	OnWrite(int)
        public int Desarrollo
		{
			get
			{
                int Desarrollo = 0;
				Desarrollo = mvarDesarrollo;
				return Desarrollo;
			}

			set
			{
				mvarDesarrollo = value;
			}
		}



		public Decimal FlujoExportaciones
		{
			get
			{
					Decimal FlujoExportaciones = 0;
				FlujoExportaciones = mvarFlujoExport;
				return FlujoExportaciones;
			}

			set
			{
				mvarFlujoExport = value;
			}
		}


		public float PercAdmitido
		{
			get
			{
					float PercAdmitido = 0;
				PercAdmitido = mvarPercAdmit;
				return PercAdmitido;
			}

			set
			{
				mvarPercAdmit = value;
			}
		}


		public void CrearNuevo()
		{

			Codigo = 0;
			Nombre = string.Empty;
			Gentilicio = string.Empty;
			Aladi = 0;
			Continente = -1;
			Region = -1;
			Desarrollo = -1;
			FlujoExportaciones = 0;
			PercAdmitido = 0;
			MaxPotencial = 0;
			mfEsNuevo = true;

		}

        //public short Guardar()
        //{
        //    short Guardar = 0;
        //    // Descripción : Guarda un país
        //    // Parámetros  : Ninguno
        //    // Retorno     : 0 OK
        //    // 3 Error al guardar
        //    // E. laterales: Ninguno
        //    // 
        //    // =============================================
        //    // Declaración de constantes/variables locales
        //    // =============================================
        //    string ltComando;
        //    VBtoRecordSet lrecPais;
        //    VBtoRecordSet lrecConsulta;
        //    int liResultado = 0;
        //    string ltContinente;
        //    string ltRegion;
        //    string ltDesarrollo;
        //    string ltFlujoExport;
        //    string ltMaxPotenc;
        //    string ltPercAdmit;
        //    try
        //    {	// On Error GoTo ManejoError

        //        ltContinente = Continente.ToString();
        //        ltRegion = Region.ToString();
        //        ltDesarrollo = Desarrollo.ToString();
        //        ltFlujoExport = FlujoExportaciones.ToString();
        //        // ltPercFlujo = CStr(PercFlujo)
        //        ltPercAdmit = PercAdmitido.ToString();
        //        ltMaxPotenc = MaxPotencial.ToString();

        //        ltComando = "exec sp_lce_act_datos_pais "+Convert.ToString(Codigo)+", '"+modAjuste32.qrst(Gentilicio)+"', "+ltContinente+","+ltRegion+","+ltDesarrollo+","+modLCEData.ConvertirComaPorPunto(ltFlujoExport)+","+modLCEData.ConvertirComaPorPunto(ltPercAdmit)+","+modLCEData.ConvertirComaPorPunto(ltMaxPotenc);
        //        // liResultado = gbasAlce_ExecuteSQL(ltComando)
        //        modAjuste32.gbasAlce_Execute(ltComando, ref liResultado);

        //        Guardar = 0;
        //        return Guardar;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("Pais.Guardar", Information.Err().Number, Information.Err().Description);
        //        Guardar = 3;

        //    }
        //    return Guardar;
        //}

        //public short Eliminar(int piPais)
        //{
        //    short Eliminar = 0;
        //    // Descripción : Elimina un país por Codigo
        //    // Parámetros  : piPais
        //    // Retorno     : 0 OK
        //    // 3 Error al eliminar
        //    // E. laterales: Ninguno
        //    // 
        //    // =============================================
        //    // Declaración de constantes/variables locales
        //    // =============================================
        //    string ltComando;
        //    VBtoRecordSet lrecPais;
        //    int liResultado = 0;

        //    try
        //    {	// On Error GoTo ManejoError

        //        ltComando = "exec sp_lce_eli_pais "+Convert.ToString(piPais);
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
        //        modLCEData.GenericError("Pais.Eliminar", Information.Err().Number, Information.Err().Description);
        //        Eliminar = 3;

        //    }
        //    return Eliminar;
        //}

        //public short GetListxCategoria(ref VBtoRecordSet precPais, int piCateg)
        //{
        //    short GetListxCategoria = 0;
        //    // Descripción : Obtiene la lista de paises por Codigo Categoria
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

        //        ltConsulta = "exec svc_lce_lst_pai_por_ctg "+Convert.ToString(piCateg);

        //        precPais = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltConsulta); // , dbOpenSnapshot,
        //        // dbSQLPassThrough)

        //        if (precPais.BOF && precPais.EOF) {
        //            GetListxCategoria = 3;
        //        } else {
        //            GetListxCategoria = 0;
        //        }

        //        return GetListxCategoria;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("Pais.GetListxCategoria", Information.Err().Number, Information.Err().Description);
        //        GetListxCategoria = 4;

        //    }
        //    return GetListxCategoria;
        //}



		public Decimal MaxPotencial
		{
			get
			{
					Decimal MaxPotencial = 0;
				MaxPotencial = mvarMaxPotencial;
				return MaxPotencial;
			}

			set
			{
				mvarMaxPotencial = value;
			}
		}

        public DataTable GetListRestricciones(ref short success, int piPais)
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
            ltConsulta = "exec svc_cle_obt_rst_pai " + Convert.ToString(piPais);
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
                            char[] rest = row["gls_dsc_rst"].ToString().ToCharArray();
                            row["gls_dsc_rst"] = row["gls_dsc_rst"].ToString().Replace( "   ", "\r\n");
                        }

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

        public DataTable GetListRiesgo(ref short success, int piPais)
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
            ltConsulta = "exec svc_cle_lst_cla_rie_por_pai " + Convert.ToString(piPais); 
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





	}
}