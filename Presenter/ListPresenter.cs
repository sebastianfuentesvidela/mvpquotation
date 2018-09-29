using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Vistas;
using Modelos;

namespace Presenter
{
    public class ListPresenter
    {

        IListView ilocView;
        private String dataConnectionString;

        public ListPresenter(String mainConnString)
        {
            dataConnectionString = mainConnString;
        }

        public void add(IListView ObjQuotView)
        {
            ilocView = ObjQuotView;
        }
        public void fillGrid(String option)
        {
            if (option == "clientes")
            {
                Persona per = new Persona(dataConnectionString);
                short success = 0;
                DataTable dt = per.GetList(ref success, 0);

                ilocView.grid.DataSource = dt;
                ilocView.grid.DataBind();

            }
        }

    }
}
