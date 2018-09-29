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
using Vistas;
using Presenter;

public partial class SelListaClientes : System.Web.UI.Page, IListView
{
    private ListPresenter objPresenter;

    protected void Page_Load(object sender, EventArgs e)
    {
        String cleDBstring = System.Configuration.ConfigurationManager.
                ConnectionStrings["BaseDatosCle"].ConnectionString;
        objPresenter = new ListPresenter(cleDBstring);

        objPresenter.add(this);
        objPresenter.fillGrid("clientes");

    }
    public GridView grid
    { get { return this.GridView1; } set { this.GridView1 = value; } }

}
