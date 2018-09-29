using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Vistas
{
    public interface IListView
    {
        GridView grid { get; set; }
    }
}
