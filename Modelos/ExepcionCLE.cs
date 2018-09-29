using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Tools;
using Modelos.Library;

namespace Modelos
{

    public class ExepcionCLE
    {
        public ExepcionCLE(String ConexionString)
            : base()
        {
            dataConnectionString = ConexionString;
        }
        private String dataConnectionString;

        //=========================================================

        public int NumCotizacion;
        public int AprobadoPor;
        public int NumAprobacion;
        public DateTime FechaAprob;
        public string Comment = "";

        public short Obtener(int plNumCot)
        {
            // Descripción : Obtiene una Exepcion por Numero Cotizacion
            // Parámetros  : plNumCot
            // Retorno     : 0 OK
            // 3 No existe la exepcion
            // 4 Error en la BD
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================
            String ltConsulta = "exec svc_cle_obt_exp_ctz " + Convert.ToString(plNumCot);
            short suceso = 0;
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataTable prec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (prec.Rows.Count == 0)
                    {
                        suceso = 3;
                    }
                    else
                    {
                        NumCotizacion = Convert.ToInt32(prec.Rows[0]["nro_ctzcn"]);
                        AprobadoPor = Convert.ToInt32(prec.Rows[0]["cod_ent_apr"]);
                        NumAprobacion = Convert.ToInt32(prec.Rows[0]["exp_num_apr"]);
                        FechaAprob = (DateTime)prec.Rows[0]["exp_fec_otr"];
                        Comment = Convert.ToString(prec.Rows[0]["exp_gls"]) + "";
                        suceso = 0;
                    }
                }
                else
                {
                    // ManejoError:
                    //modLCEData.GenericError("ExepcionCLE.Obtener", Information.Err().Number, Information.Err().Description);
                    suceso = 4;

                }
                return suceso;
            }
        }
    }
}