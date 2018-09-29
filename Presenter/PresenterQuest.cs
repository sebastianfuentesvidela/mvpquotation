using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;
using System.Data;
using System.Linq;
using System.Globalization;
using Newtonsoft.Json;
using Modelos;
using Modelos.Library;
using Vistas;
using Tools;

namespace Presenter
{
    public class PresenterQuest
    {
        IQuest iView;
        PageComments pageComm;
        private String dataConnectionString;

        public PresenterQuest(String mainConnString)
        {
            dataConnectionString = mainConnString;
            pageComm = new PageComments();
        }
        public void add(IQuest oView)
        {
            iView = oView;
        }
        public String SolveQuest(HttpContext context)
        {
            context.Request.InputStream.Position = 0;
            var jsonString = String.Empty;
            using (var inputStream = new StreamReader(context.Request.InputStream))
            {
                jsonString = inputStream.ReadToEnd();
            }

            JavaScriptSerializer deserializer = new JavaScriptSerializer();
            Dictionary<String, Object> contenido = deserializer.Deserialize<Dictionary<String, Object>>(jsonString);
            String accion = contenido["accion"].ToString();
            if (accion == "paridad")
            {
                int curmone;
                if (!int.TryParse(contenido["pointer"].ToString(), out curmone))
                {
                    pageComm.Add("fail", "Moneda no identificada:" + contenido["pointer"].ToString() + ".");
                }
                else
                {
                    DateTime fecha;
                    if (!DateTime.TryParse(contenido["fecha"].ToString(), out fecha))
                    {
                        pageComm.Add("fail", "Fecha en Malas Condiciones: " + contenido["fecha"].ToString() + ".");
                    }
                    else
                    {
                        Moneda mone = new Moneda(dataConnectionString);
                        Double parity = mone.ParidadMoneda(curmone, fecha, 1);
                        pageComm.Add("paridad", parity.ToString(CultureInfo.CreateSpecificCulture("en-US")));
                    }
                }
                return Modelos.Library.JsonInform.PageCommentsToJson(pageComm);
            }
            else if (accion == "relacionlimitecotizacion")
            {
                short success = 0;
                String[] conte = Tools.CryptoTools.Decrypt(contenido["pointer"].ToString()).Split('\t');
                int codigo = int.Parse(conte[1]);
                int correl = int.Parse(conte[2]);
                String numPersona = conte[3];
                Decimal montoAplica = Decimal.Parse( conte[4], 
                    NumberStyles.Number, new System.Globalization.CultureInfo("en-US"));
                String tipOper = conte[5];
                DateTime fecha = DateTime.Today;
                Dictionary<String, Object> answer = new Dictionary<string, object>();
                answer.Add("fecha", fecha.ToShortDateString());
                answer.Add("targkey", contenido["pointer"].ToString());
                if (codigo == 2)
                {
                    answer.Add("nombre", "Límites Capítulo IIIB5");
                    answer.Add("moneda", "DOLAR AMERICANO");
                }
                else
                {
                    Limite limite = new Limite(dataConnectionString);
                    if (limite.Obtener(codigo, correl, fecha) == 0)
                    {
                        answer.Add("nombre", limite.Nombre);
                        Moneda mon = new Moneda(dataConnectionString);
                        if (mon.Obtener(limite.Moneda) == 0)
                        {
                            answer.Add("moneda", mon.Nombre);
                        }
                    }
                }

                RelacionLimiteCotizacion rela = new RelacionLimiteCotizacion(dataConnectionString);
                DataTable dRel = rela.GetList(ref success, codigo, correl);
                if (success == 0)
                {
                    DataTable infor = rela.PackInfo(dRel, tipOper, numPersona, montoAplica, true);


                    answer.Add("lista", infor);
                }
                else if (success == 3) {
                    answer.Add("vacio", "No Existen Cotizaciones para este Límite");
                }
                String result = JsonConvert.SerializeObject(answer);
                return result;

            }
            else if (accion == "listaclientes")
            {
                short success = 0;
                String numPersona = contenido["numcli"].ToString();
                String nomPersona = contenido["nomcli"].ToString();
                short rowsppage = 14;
                short currpage = Convert.ToInt16(contenido["pointer"].ToString() == string.Empty ? "0" : contenido["pointer"].ToString());
                if (currpage == 0) currpage = 1;
                Persona per = new Persona(dataConnectionString);
                DataTable dt = per.GetList(ref success, 0);
                string filter = (nomPersona != "")? "nom_prsna_etcdo like '%" + nomPersona + "%'": "";
                string sort = "nom_prsna_etcdo ASC";
                IEnumerable<DataRow> qry = dt.Select(filter, sort);
                short rowstotal = (short)qry.ToList().Count();
                DataTable ndt = new DataTable("clientes");
                ndt = dt.Clone();
                int toma = rowsppage;
                int eskip = (rowsppage) * (currpage - 1);
                if (eskip > rowstotal) { currpage--; eskip = (rowsppage) * (currpage - 1); }
                if (qry.Count()>0) ndt = qry.Skip(eskip).Take(toma).CopyToDataTable<DataRow>();
                DataTable pdt = new DataTable("paginacion");
                pdt.Columns.Add("actual", typeof(short));
                pdt.Columns.Add("total", typeof(short));
                pdt.Columns.Add("totalpages", typeof(short));
                int totpages = rowstotal / rowsppage;
                if (totpages * rowsppage < rowstotal) totpages++;
                pdt.Rows.Add(currpage, rowstotal, totpages);
                DataSet nds = new DataSet();
                ndt.TableName = "clientes";
                nds.Tables.Add(ndt); nds.Tables.Add(pdt);
                String result = JsonConvert.SerializeObject(nds);
                return result;

            }
            else
            {
                pageComm.Add("fail", "Accion No Identificada: " + accion + ".");
            }
            return Modelos.Library.JsonInform.PageCommentsToJson(pageComm);
        }

    }
}
