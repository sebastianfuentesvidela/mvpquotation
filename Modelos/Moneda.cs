using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Data;
using Modelos.Library;
using Tools;

namespace Modelos
{
    public class Moneda
    {

        //=========================================================

        // variables locales que contienen valores de propiedad
        private int mvarCodigo; // copia local
        private string mvarSimbolo = ""; // copia local
        private string mvarNombre = ""; // copia local
        private string mvarMarcaNacional = ""; // copia local
        private int mvarCodigoBcoCentral; // copia local
        private bool mfEsNuevo; // Indica si es Nuevo o existente

        private String dataConnectionString;
        public List<String> errlist = new List<string>();
        public Moneda(String ConexionString)
            : base()
        {
            dataConnectionString = ConexionString;
        }
        public double ParidadMoneda(int piCodigoMoneda, DateTime pxFecha, short piOpcion)
        {
            double paridadMoneda = 0;
            string ltConsulta; DataTable rec;
            StringBuilder sbXML;
            sbXML = new StringBuilder();
            sbXML.Append("exec svc_cle_obt_par_mon '");
            sbXML.Append(pxFecha.ToString("yyyyMMdd"));
            sbXML.Append("', ");
            sbXML.Append(piCodigoMoneda);
            sbXML.Append(", ");
            sbXML.Append(piOpcion);
            // ltConsulta = "exec svc_cle_obt_par_mon '" & Format(pxFecha, "yyyyMMdd") & "', " &
            // piCodigoMoneda & ", " & piOpcion
            ltConsulta = sbXML.ToString();
            sbXML = null;

            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                rec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count == 0)
                    {
                        paridadMoneda = 0;
                    }
                    else
                    {
                        paridadMoneda = double.Parse(rec.Rows[0]["paridad"].ToString());

                    }
                }
                else
                {
                    //modLCEData.GenericError("modGlobal.ParidadMoneda", Information.Err().Number, Information.Err().Description);
                    errlist.AddRange(db.errlist);
                    paridadMoneda = 0;

                }
            }
            return paridadMoneda;
        }


        public DataTable GetList(ref short success)
        {
            // Descripción : Obtiene la lista de monedas
            // Parámetros  : Ninguno
            // Retorno     : 0 OK
            // 3 No existe
            // 4 Error en la base de datos
            // E. laterales: Ninguno
            // 
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================
            string ltConsulta; DataTable rec;
            ltConsulta = "exec svc_cle_lst_mon";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                rec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count == 0)
                    {
                        errlist.Add("No hay Monedas en la BBDD");
                        success = 3;
                    }
                    else
                    {
                        success = 0;
                    }
                }
                else
                {
                    //modLCEData.GenericError("Moneda.Getlist", Information.Err().Number, Information.Err().Description);
                    errlist.AddRange(db.errlist);
                    success = 4;

                }
            }
            return rec;

        }

        // VBto upgrade warning: 'Return' As short	OnWrite(int)
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



        public string Simbolo
        {
            get
            {
                string Simbolo = "";
                Simbolo = mvarSimbolo;
                return Simbolo;
            }

            set
            {
                if (Strings.Trim(value).Length > 10)
                {
                    Information.Err().Raise(Constants.vbObjectError + 101, null, "El simbolo es demasiado largo (máx: 10).", null, null);
                }
                else
                {
                    mvarSimbolo = value;
                }

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
                if (Strings.Trim(value).Length > 50)
                {
                    Information.Err().Raise(Constants.vbObjectError + 101, null, "El nombre es demasiado largo (máx: 50).", null, null);
                }
                else
                {
                    mvarNombre = value;
                }

            }
        }



        // VBto upgrade warning: piCodigo As short --> As int	OnWrite(int, VBtoField)
        public short Obtener(int piCodigo)
        {
            // Descripción : Obtiene una moneda por codigo
            // Parámetros  : piCodigo
            // Retorno     : 0 OK
            // 3 No existe la moneda
            // 4 Error en la BD
            // E. laterales: Ninguno
            // 
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================
            short success;
            string ltConsulta;
            String codigo = piCodigo.ToString();
            int largo = codigo.Trim().Length;
            StringBuilder sbXML;
            sbXML = new StringBuilder();
            for (int i = 1; i <= 5 - largo; i++)
            {
                sbXML.Append(" ");
            } // i
            String espacios = sbXML.ToString(); // ltEspacios & " "
            sbXML = null;
            codigo = espacios + codigo;

            // Inicialización de @datos2
            String ltResultado = string.Empty;

            ltConsulta = "exec sp_lce_cna_monda '" + codigo + "','" + ltResultado + "'";

            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataTable rec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count == 0)
                    {
                        errlist.Add("No existe la Moneda con el Codigo Buscado");
                        success = 3;
                    }
                    else
                    {
                        Codigo = piCodigo;
                        Simbolo = Convert.ToString(rec.Rows[0]["cod_smblo_monda"]) + "";
                        Nombre = Convert.ToString(rec.Rows[0]["gls_monda"]) + "";
                        MarcaNacional = Convert.ToString(rec.Rows[0]["mrc_ncnal_extrj"]) + "";
                        CodigoBcoCentral = Convert.ToInt32(rec.Rows[0]["cod_monda_bcoct"]);
                        success = 0;
                        mfEsNuevo = false;
                    }
                }
                else
                {
                    //modLCEData.GenericError("Moneda.Obtener", Information.Err().Number, Information.Err().Description);
                    errlist.AddRange(db.errlist);
                    success = 4;

                }
            }
            return success;
        }

        public string MarcaNacional
        {
            get
            {
                string MarcaNacional = "";
                MarcaNacional = mvarMarcaNacional;
                return MarcaNacional;
            }

            set
            {
                mvarMarcaNacional = value;
            }
        }



        public int CodigoBcoCentral
        {
            get
            {
                int CodigoBcoCentral = 0;
                CodigoBcoCentral = mvarCodigoBcoCentral;
                return CodigoBcoCentral;
            }

            set
            {
                mvarCodigoBcoCentral = value;
            }
        }



    }
}