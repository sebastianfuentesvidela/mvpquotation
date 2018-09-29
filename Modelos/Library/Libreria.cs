using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Modelos.Library
{
    class Libreria
    {

        //public static double ParidadMoneda(int piCodigoMoneda, DateTime pxFecha, short piOpcion)
        //{
        //    double ParidadMoneda = 0;
        //    string ltConsulta; DataTable rec;
        //    StringBuilder sbXML;
        //    sbXML = new StringBuilder();
        //    sbXML.Append("exec svc_cle_obt_par_mon '");
        //    sbXML.Append(pxFecha.ToString("yyyyMMdd"));
        //    sbXML.Append("', ");
        //    sbXML.Append(piCodigoMoneda);
        //    sbXML.Append(", ");
        //    sbXML.Append(piOpcion);
        //    // ltConsulta = "exec svc_cle_obt_par_mon '" & Format(pxFecha, "yyyyMMdd") & "', " &
        //    // piCodigoMoneda & ", " & piOpcion
        //    ltConsulta = sbXML.ToString();
        //    sbXML = null;

        //    using (DataBinder db = new DataBinder(dataConnectionString))
        //    {
        //        rec = db.GetRecordset(ltConsulta);
        //        if (db.errlist.Count == 0)
        //        {
        //            if (rec.Rows.Count == 0)
        //            {
        //                success = 3;
        //            }
        //            else
        //            {
        //                success = 0;
        //            }
        //        }
        //        else
        //        {
        //            ///Global.GenericError("Limite.ObtenerListaArtNormsCliente", Information.Err().Number, Information.Err().Description);
        //            success = 4;

        //        }
        //    }
        //    return rec;


        //    string ltConsulta;
        //    int liRespuesta;
        //    VBtoRecordSet lrecValorMoneda;

        //    try
        //    {	// On Error GoTo ManejoError
        //        ParidadMoneda = 0;
        //        StringBuilder sbXML;
        //        sbXML = new StringBuilder();
        //        sbXML.Append("exec svc_cle_obt_par_mon '");
        //        sbXML.Append(pxFecha.ToString("yyyyMMdd"));
        //        sbXML.Append("', ");
        //        sbXML.Append(piCodigoMoneda);
        //        sbXML.Append(", ");
        //        sbXML.Append(piOpcion);
        //        // ltConsulta = "exec svc_cle_obt_par_mon '" & Format(pxFecha, "yyyyMMdd") & "', " &
        //        // piCodigoMoneda & ", " & piOpcion
        //        ltConsulta = sbXML.ToString();
        //        sbXML = null;

        //        lrecValorMoneda = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltConsulta); // , dbOpenSnapshot,
        //        // dbSQLPassThrough)
        //        if (!lrecValorMoneda.EOF)
        //        {
        //            ParidadMoneda = VBtoConverter.ObjectToDouble(lrecValorMoneda.Fields["paridad"].Value);
        //        }
        //        lrecValorMoneda.Close();

        //        return ParidadMoneda;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("modGlobal.ParidadMoneda", Information.Err().Number, Information.Err().Description);
        //    }
        //    return ParidadMoneda;
        //}

    }
}
