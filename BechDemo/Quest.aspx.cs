using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Presenter;
using Vistas;

public partial class Quest : System.Web.UI.Page, IQuest
{
    private PresenterQuest objPresenter;

    protected void Page_Load(object sender, EventArgs e)
    {

        String cleDBstring = System.Configuration.ConfigurationManager.
                ConnectionStrings["BaseDatosCle"].ConnectionString;
            objPresenter = new PresenterQuest(cleDBstring);

            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(objPresenter.SolveQuest(HttpContext.Current));
            Response.End();

    
    }
}
