using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;
using System.Data;
using System.Text;
using Microsoft.Reporting.WebForms;
using Modelos;
using Vistas;

namespace Presenter
{
    public class PresenterReports
    {
        IReportView iView;
        PageComments pageComm;
        private String dataConnectionString;
        private String tipollamada;
        public String accion;
        public PresenterReports(String mainConnString)
        {
            dataConnectionString = mainConnString;
            pageComm = new PageComments();

        }
        public void add(IReportView repView, HttpContext context)
        {
            iView = repView;
            tipollamada = context.Request.HttpMethod.ToUpper();
            accion = context.Request.QueryString["."] == null ? string.Empty : context.Request.QueryString["."].ToString();
            if (tipollamada == "GET")
            {
                accion = context.Request.QueryString["."].ToString();
            }
            else if (accion == string.Empty) {
                context.Request.InputStream.Position = 0;
                var jsonString = String.Empty;
                using (var inputStream = new StreamReader(context.Request.InputStream))
                {
                    jsonString = inputStream.ReadToEnd();
                }

                JavaScriptSerializer deserializer = new JavaScriptSerializer();
                Dictionary<String, Object> contenido = deserializer.Deserialize<Dictionary<String, Object>>(jsonString);
                accion = contenido["iddrequest"].ToString();


            }

        }
        public void CorreAccion(String reportPath)
        {
            if (accion == "pepe")
            {

            }
            else { 
            }
        }
        public void CorreCustomer(String reportPath)
        {
            iView.Rotulo.Text = "Ejemplo de Report Viewer via " + tipollamada; // +" por " + accion;
            Persona perso = new Persona(dataConnectionString);
            iView.Reporte.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            iView.Reporte.LocalReport.ReportPath = reportPath;

            //ReportParameter fecha = new ReportParameter("Fecha",dd.ToString("dd/MM/yyyy"));
            //iView.Reporte.LocalReport.SetParameters(
            //          new ReportParameter[] { fecha });

            short suceso=0;
            Customers dsCustomers = perso.GetCustomers(ref suceso);
            ReportDataSource datasource = new ReportDataSource("Customers_DataTable1", dsCustomers.Tables[0]);
            iView.Reporte.LocalReport.DataSources.Clear();
            iView.Reporte.LocalReport.DataSources.Add(datasource);

        }
        public void CorreEmployees(String reportPath, String dataPath)
        {
            iView.Rotulo.Text = "Ejemplo 2 de Report Viewer via " + tipollamada; // +" por " + accion;
            Persona perso = new Persona(dataConnectionString);

            iView.Reporte.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            iView.Reporte.LocalReport.ReportPath = reportPath;

            DataSet dataSet = new DataSet();
            dataSet.ReadXml(dataPath);


            iView.Reporte.LocalReport.DataSources.Clear();
            iView.Reporte.LocalReport.DataSources.Add(
                new ReportDataSource("DataSet1_DataTable1", dataSet.Tables[0]));


        }

    }
}
