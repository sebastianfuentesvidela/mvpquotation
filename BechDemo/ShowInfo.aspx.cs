using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Presenter;
using Vistas;

public partial class ShowInfo : System.Web.UI.Page, IReportView
{
    private PresenterReports objPresenter;

    protected void Page_Load(object sender, EventArgs e)
    {

        String cleDBstring = System.Configuration.ConfigurationManager.
                ConnectionStrings["BaseDatosCle"].ConnectionString;
        objPresenter = new PresenterReports(cleDBstring);

        objPresenter.add(this, HttpContext.Current);
        if (objPresenter.accion == "pepe")
        {
            objPresenter.CorreCustomer(Server.MapPath("~/Reportes/ReportSample.rdlc"));
        }
        else
        {
            objPresenter.CorreEmployees(Server.MapPath("~/Reportes/Report1.rdlc"), Server.MapPath("~/App_data/data.xml"));
        }

        ///Si Hay Objecion de Puntero a Contenido no iniciar.

    }
    public Label Rotulo
    { get { return this.rotuloReporte; } set { this.rotuloReporte = value; } }

    public ReportViewer Reporte
    { get { return this.ReportViewer1; } set { this.ReportViewer1 = value; } }



}
