using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace Vistas
{
    public interface IReportView
    {
        ReportViewer Reporte { get; set; }
        Label Rotulo { get; set; }
    }
}
