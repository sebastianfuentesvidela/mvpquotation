using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using Microsoft.VisualBasic;


namespace Tools
{
	public class Global
	{

		//=========================================================
	    public const string SISTEMA = "Control Límites Exterior";
		public const string MODULO_NAME = "Módulo Operador";

		// *** Monedas
		public const int DOLAR = 13;
		public const int PESO = 999; // 0

		// *** Países
		public const int BRASIL = 220;

		// *** Tipos de consulta
		public const int VER = 1;
		public const int Buscar = 2;
		public const int SIMULACION = 3;
		public const int BUSCARVIGENTES = 4;
		public const int BUSCARVIGENTESVENCIDOS = 8;

		// *** Estados Cotizacion
		public const int COTIZADA = 1;
		public const int ANULADA = 2;
		public const int CURSADA = 3;
		public const int VIGENTE = 4;
		// Global Const VENCIDA_NO_CONFIRMADA = 5
		public const int CURSADA_VENCIDA = 5;
		public const int APROBADA = 6;
		public const int RECHAZADA = 7;
		public const int COTIZADA_VENCIDA = 8;
		public const int VENCIDA_PAGADA = 9;

		public static int SALIDA;
		public static DateTime PASOFECHA;
		public static string TRASPASO = "";
		public static int PASOCATEGORIA;


		// *** Variables globales ***
		public static int giConsulta;
		public static int giListar;
		public static string gtCodigoEncontrado = "";
		public static bool gfResultadoBusqueda;
		public static bool gfSimulado;
		public static int giTipoCliente;
		public static int glNumeroCotizacion;
		public static int glCotizacion;
		public static int giTipoConsulta;
		public static int glCodigoPlantilla;
		public static int glCodigoPlantillaAnterior;
		public static Decimal glMontoAfecta;
		public static int glCorrelativo;
        //public static int [,]glHistoria = new int[30 + 1, 3 + 1];
		public static int giIndice;
		public static string gtRutOperador = "";
		// *** Constantes para distinguir Form abierta ***
		public const int FORM_AGENCIA = 1;
		public const int FORM_PAIS = 2;
		public const int FORM_PRODUCTO = 3;
		public const int FORM_TIPO = 4;
		public const int FORM_GRUPO = 5;
		public const int FORM_CLIENTE = 6;
		public const int FORM_RESTRICCION = 7;
		public const int FORM_FAMILIA = 8;
		public const int FORM_RANGO = 9;
		public const int FORM_RESTRICCION1 = 10;
		public const int FORM_NIVEL_DESARROLLO = 11;

#if defUse_NormalizaNombre
		public static string NormalizaNombre(string ptNombre)
		{
			string NormalizaNombre = "";
			// Dim ltApellidoPaterno As String
			// Dim ltApellidoMaterno As String
			// Dim ltNombres As String

			if (ptNombre.Length>60) {
				// ltApellidoPaterno = StrConv(Trim$(Left$(ptNombre, 30)), vbProperCase)
				// ltApellidoMaterno = StrConv(Trim$(Mid$(ptNombre, 31, 30)), vbProperCase)
				// ltNombres = StrConv(Trim$(Mid$(ptNombre, 61)), vbProperCase)
				cStringBuilder sbXML;
				sbXML = new cStringBuilder();
				sbXML.Append(ref Encoding.Default.GetBytes(Strings.Trim(Strings.Left(ptNombre, 30))));
				sbXML.Append(" ");
				sbXML.Append(ref Encoding.Default.GetBytes(Strings.Trim(Strings.Mid(ptNombre, 31, 30))));
				sbXML.Append(", ");
				sbXML.Append(ref Encoding.Default.GetBytes(Strings.Trim(Strings.Mid(ptNombre, 61))));
				NormalizaNombre = sbXML.Resolve;
				sbXML = null;
				// NormalizaNombre = ltApellidoPaterno & " " & ltApellidoMaterno & ", " & ltNombres
			} else {
				NormalizaNombre = string.Empty;
			}

			return NormalizaNombre;
		}
#endif

        public static string qrst(String texto)
        {
            return texto.Replace("'", "''");
        }
        public static string ConvertirPuntoPorComa(string ptNumero)
        {
            string ConvertirPuntoPorComa = "";
            int liPosicion = 0;
            // VBto upgrade warning: liLargo As string	OnWrite(int)
            string liLargo;
            string ltNumeroConvertido = "";

            liLargo = Convert.ToString(Strings.Trim(ptNumero).Length);

            if (Convert.ToDouble(liLargo) == 0)
            {
                ltNumeroConvertido = ptNumero;
            }
            else
            {
                liPosicion = Strings.InStr(ptNumero, ".", CompareMethod.Text/*?*/);
                if ((liPosicion == 0) || (liPosicion == null))
                {
                    ltNumeroConvertido = ptNumero;
                }
                else
                {
                    ltNumeroConvertido = Strings.Left(ptNumero, liPosicion - 1) + "," + Strings.Mid(ptNumero, liPosicion + 1, Convert.ToInt32(liLargo) - liPosicion);
                }
            }
            ConvertirPuntoPorComa = ltNumeroConvertido;

            return ConvertirPuntoPorComa;
        }
        public static string ConvertirComaPorPunto(string ptNumero)
        {
            string ConvertirComaPorPunto = "";
            int liPosicion = 0;
            // VBto upgrade warning: liLargo As string	OnWrite(int)
            string liLargo;
            string ltNumeroConvertido = "";

            liLargo = Convert.ToString(Strings.Trim(ptNumero).Length);

            if (Convert.ToDouble(liLargo) == 0)
            {
                ltNumeroConvertido = ptNumero;
            }
            else
            {
                liPosicion = Strings.InStr(ptNumero, ",", CompareMethod.Text/*?*/);
                if ((liPosicion == 0) || (liPosicion == null))
                {
                    ltNumeroConvertido = ptNumero;
                }
                else
                {
                    ltNumeroConvertido = Strings.Left(ptNumero, liPosicion - 1) + "." + Strings.Mid(ptNumero, liPosicion + 1, Convert.ToInt32(liLargo) - liPosicion);
                }
            }
            ConvertirComaPorPunto = ltNumeroConvertido;
            return ConvertirComaPorPunto;
        }


		// VBto upgrade warning: ptNumeroPersona As string	OnWrite(string, VBtoField)
		public static string ConvertirNroRut(string ptNumeroPersona)
		{
			string ConvertirNroRut = "";
			string ltDigito;
			string ltRut;
			int i;
			// ltRut = vbNullString

			StringBuilder sbXML = new StringBuilder();
			for(i=1; i<=ptNumeroPersona.Length; i++) {
				if (Strings.InStr("0123456789kK", Strings.Mid(ptNumeroPersona, i, 1), CompareMethod.Text/*?*/)>0) {
					sbXML.Append(Strings.Mid(ptNumeroPersona, i, 1));
					// ltRut = ltRut & Mid$(ptNumeroPersona, i, 1)
				}
			}
			ltRut = sbXML.ToString(); // ltEspacios & " "
			sbXML = null;

			while (Strings.Left(ltRut, 1)=="0") {
				ltRut = Strings.Mid(ltRut, 2);
			}
			ltDigito = Strings.Right(ltRut, 1);
			ltRut = Strings.Left(ltRut, ltRut.Length-1);

			// ltDigito = Right$(ptNumeroPersona, 1)
			// ltRut = CLng(Left$(ptNumeroPersona, 9))
			ConvertirNroRut = ltRut+"-"+ltDigito;
			return ConvertirNroRut;
		}

		// VBto upgrade warning: ptNumeroPersona As string	OnWrite(string, VBtoField, VB.TextBox)
		public static string ConvertirRutNro(string ptNumeroPersona)
		{
            if (ptNumeroPersona == null) return string.Empty;
			string ConvertirRutNro = "";
			string ltDigito;
			string ltRut;
			int i;
			// ltRut = vbNullString
			StringBuilder sbXML;
			sbXML = new StringBuilder();
			for(i=1; i<=ptNumeroPersona.Length; i++) {
				if (Strings.InStr("0123456789kK", Strings.Mid(ptNumeroPersona, i, 1), CompareMethod.Text/*?*/)>0) {
					sbXML.Append(Strings.Mid(ptNumeroPersona, i, 1));
					// ltRut = ltRut & Mid$(ptNumeroPersona, i, 1)
				}
			}
			ltRut = sbXML.ToString(); // ltEspacios & " "
			sbXML = null;
			while (Strings.Left(ltRut, 1)=="0") {
				ltRut = Strings.Mid(ltRut, 2);
			}
			if (ltRut.Length==0) { ConvertirRutNro = string.Empty; return ConvertirRutNro; }
			ltDigito = Strings.UCase(Strings.Right(ltRut, 1));
			ltRut = Strings.Left(ltRut, ltRut.Length-1);
			ConvertirRutNro = Strings.Right("000000000"+ltRut, 9)+ltDigito;
			return ConvertirRutNro;
		}

		// VBto upgrade warning: 'Return' As Variant --> As string
		public static string DiaSemana(DateTime pxFecha)
		{
			string DiaSemana = "";
			string ltDiaSemana = "";

			switch (pxFecha.DayOfWeek) {

                case DayOfWeek.Sunday:
				{
					ltDiaSemana = "Domingo";
					break;
				}
                case DayOfWeek.Monday:
				{
					ltDiaSemana = "Lunes";
					break;
				}
                case DayOfWeek.Tuesday:
				{
					ltDiaSemana = "Martes";
					break;
				}
                case DayOfWeek.Wednesday:
				{
					ltDiaSemana = "Miércoles";
					break;
				}
                case DayOfWeek.Thursday:
				{
					ltDiaSemana = "Jueves";
					break;
				}
                case DayOfWeek.Friday:
				{
					ltDiaSemana = "Viernes";
					break;
				}
                case DayOfWeek.Saturday:
				{
					ltDiaSemana = "Sábado";
					break;
				}
				default: {
					// Error
					ltDiaSemana = " ";
					break;
				}
			} //end switch
			DiaSemana = ltDiaSemana;

			return DiaSemana;
		}

	}
}