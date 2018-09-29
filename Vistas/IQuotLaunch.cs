using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Vistas
{
    public interface IQuotLaunch
    {
        HiddenField Comanda { get; set; }
        HiddenField Argumento { get ; set; }
        String pageContent { get; set; }
        ImageButton ToolNueva { get; set; }

        ImageButton ToolBusca { get; set; }

        ImageButton ToolSimula { get; set; }

        ImageButton ToolCotiza { get; set; }

        ImageButton ToolCursa { get; set; }

        ImageButton ToolAnula { get; set; }

        ImageButton ToolImprime { get; set; }

        HiddenField PageIdd { get; set; }

        TextBox NumeroCotizacion { get; set; }

        TextBox FechaCotizacion { get; set; }

        TextBox Vigencia { get; set; }

        Button Cotizar { get; set; }
        TextBox FechaCurse { get; set; }
        Button Cursar { get; set; }

        TextBox Estado { get; set; }
        TextBox FechaEstado { get; set; }


        TextBox RutCliente { get; set; }
        Label SinLineaCliente { get; set; }

        TextBox NombreCliente { get; set; }

        TextBox Pais { get; set; }

        TextBox Categoria { get; set; }

        TextBox CasaMatriz { get; set; }

        TextBox Swift { get; set; }

        TextBox NroOperacion { get; set; }

        CheckBox OperPuntual { get; set; }

        CheckBox Nota { get; set; }

        CheckBox CargoCasaMatriz { get; set; }

        DropDownList TipoOperacion { get; set; }

        DropDownList Producto { get; set; }

        TextBox CodigoTipoOperacion { get; set; }

        DropDownList TipoCredito { get; set; }

        DropDownList Moneda { get; set; }

        TextBox MontoOperacion { get; set; }
        
        HiddenField Paridad { get; set; }

        TextBox PorcentajeTolerancia { get; set; }

        TextBox PlazoMaxResidualCtg { get; set; }

        TextBox FechaVctoEstimada { get; set; }

        Label DiaFechaVctoEstimada { get; set; }

        TextBox Monto { get; set; }

        TextBox MontoEquivalente { get; set; }

        Button Simular { get; set; }

        TextBox NroPerSuby { get; set; }
        Label SinLineaSuby { get; set; }

        TextBox NomSuby { get; set; }

        TextBox PaisSuby { get; set; }

        TextBox CategSuby { get; set; }

        TextBox PorcenGarantia { get; set; }

        TextBox MontoGarantia { get; set; }

        DropDownList TipoOperSuby { get; set; }

        DropDownList ProdSuby { get; set; }

        DropDownList MoneComision { get; set; }

        TextBox MontoComisiones { get; set; }

        RadioButtonList GastosPorCuenta { get; set; }

        TextBox TasaPago { get; set; }

        TextBox ValorTasaPago { get; set; }

        TextBox ValorSpreadPago { get; set; }

        TextBox TasaTotalPago { get; set; }

        TextBox TasaPtmo { get; set; }

        TextBox ValorTasaPtmo { get; set; }

        TextBox ValorSpreadPtmo { get; set; }

        TextBox TasaTotalPtmo { get; set; }

        TextBox NombreImportador { get; set; }

        TextBox NombreExportador { get; set; }

        TextBox Mercaderia { get; set; }

        TextBox FechaContraGarantia { get; set; }

        DropDownList Reembolso { get; set; }

        TextBox Observaciones { get; set; }

    }
}
