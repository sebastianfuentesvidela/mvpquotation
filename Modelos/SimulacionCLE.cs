using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Tools;
using Modelos.Library;

namespace Modelos
{
    public class SimulacionCLE
    {
        public int iNroCotizacion;
        public string tNumeroCliente;
        public string nombreCliente;
        public string tNumeroCasaMatriz;
        public string nombreMatriz;
        // tNumeroCasaMatrizSuby As String
        public short iCodigoPais;
        public string nombrePaisCliente;
        public short iCodigoCateg;
        public string nombreCategoriaCliente;
        public string tTipoProducto;
        public string nombreTipoProducto;
        public string tCodigoFamilia;
        public int iCodigoProducto;
        public string nombreProducto;
        public short iCargoCasaMatriz;
        // iCargoCasaMatrizSuby As Integer
        // VBto upgrade warning: iEsNotaEstruct As short --> As CheckState
        public bool iEsNotaEstruct;
        // VBto upgrade warning: iEsOperPuntual As short --> As CheckState
        public bool iEsOperPuntual;
        // VBto upgrade warning: dMontoDolares As double	OnWrite(Decimal)
        public double dMontoDolares;
        public int iCodigoMoneda;
        public string nombreMonedaMonto;
        public string tNumeroPerSuby;
        public string nombreDeudorSubyace;
        public short iCodigoPaisSuby;
        public string nombrePaisSubyace;
        public short iCodigoCategSuby;
        public string nombreCategoriaSubyace;
        public string tTipoProdSuby;
        public string nombreTipoProductoSubyace;
        public int iCodigoProductoSby;
        public string nombreProductoSubyace;
        public double dMontoOrigen;
        public double dMontoAfecta;
        public double dMontoGarantia;
        public double dMontoAfe70;
        public String personaAfe70;
        public float sPorcGarantia;
        public bool bSubyacenteAsume70;


        public String lblDiasPlazo;
        public String clrDiasPlazo;
        public int iDiasPlazo;
        public String lblDiasMaximo;
        public String clrDiasMaximo;
        public String messageBox;
        public int iPlazoMinimo; // minimo de plazos maximos
        public bool iPlazoRestringido; // si hay bloqueos

        public short iFlagCapitulo3b5;
        public Capitulo3b5 lcapCapitulo3b5;
        public DataTable lrecLimites;
        public DataTable listaafectados;
        //public object alstLim;
        public string tPlzAcotado;
        public string tPlzRestringido;
        public int iExedNorm;
        public string tExedNormMsg;
        public int iExedPoli;
        public string tExedPoliMsg;

        public bool bPideExcepcion;
        public bool bPuedeCotizar;
        public bool bPuedeCursar;
        public string tNumeroOperacion;
        public DataTable restricCliente;
        public DataTable riesgoCliente;
        public DataTable restricPais;
        public DataTable riesgoPais;


        public List<string> errlist;
        public Dictionary<string, string> comments;
        private String dataConnectionString;

        public SimulacionCLE(String ConexionString)
            : base()
        {
            dataConnectionString = ConexionString;
            errlist = new List<string>();
            comments = new Dictionary<string, string>();
        }

        public bool GenerarSimulacion()
        {
            bool success = true;
            string msgc, msgp;

            int liResultado;
            string ltComando;
            string ltMensaje = "";
            Decimal SumaPrin, SumaSec, oto1, oto2;
            Decimal grp1, grp2, resv_base, resv_adic;
            Decimal dispo_base, dispo_adic;

            this.iFlagCapitulo3b5 = 0;

            // sfv  v4 Capitulo 3B5

            TipoProducto tipoprod = new TipoProducto(dataConnectionString);
            GrupoProducto grupprod = new GrupoProducto(dataConnectionString);
            Capitulo3b5 cap3b5 = new Capitulo3b5(dataConnectionString);

            string tip70 = this.tTipoProducto;
            Decimal mont70 = Convert.ToDecimal(this.dMontoAfecta);
            String per70 = this.tNumeroCliente;
            this.bSubyacenteAsume70 = false;
            if (1 == tipoprod.ObtenerModoAfectacion(this.tTipoProducto, this.tTipoProdSuby, "sbyasume70"))
            {
                tip70 = this.tTipoProdSuby;
                mont70 = Convert.ToDecimal(this.dMontoGarantia);
                this.bSubyacenteAsume70 = true;
                per70 = this.tNumeroPerSuby;
            }
            this.dMontoAfe70 = (double)mont70;
            this.personaAfe70 = per70;
            // .
            liResultado = cap3b5.Obtener(DateTime.Now);
            if (liResultado == 0)
            {
                // margen  70 Limite Global
                if (grupprod.IsTipInGrupo(tip70, "70MAIN"))
                {
                    // If chkMargenAdicional.Value = True Then
                    SumaPrin = mont70;
                    SumaSec = 0;
                    if (grupprod.IsTipInGrupo(tip70, "70ADIC"))
                    {
                        SumaSec = mont70;
                    }
                    grp1 = Convert.ToDecimal(cap3b5.MontoReserva70Pri) + Convert.ToDecimal(cap3b5.MontoUtiliza70Pri) + SumaPrin;
                    grp2 = Convert.ToDecimal(cap3b5.MontoReserva70Sec) + Convert.ToDecimal(cap3b5.MontoUtiliza70Sec) + SumaSec;
                    oto1 = Convert.ToDecimal(cap3b5.MontoOtorgado70); oto2 = Convert.ToDecimal(cap3b5.MontoOtorgado70Adi);
                    resv_base = Convert.ToDecimal(cap3b5.MontoReservado70); resv_adic = Convert.ToDecimal(cap3b5.MontoReservado70Adi);

                    if (oto1 - grp1 >= 0)
                    {
                        resv_base += mont70;
                        resv_adic += 0;
                        dispo_base = oto1 - grp1;
                        dispo_adic = oto2;
                    }
                    else if (oto1 - (grp1 - grp2) >= 0)
                    {
                        if (oto1 - grp1 < 0)
                        {
                            resv_base += (SumaPrin - SumaSec);
                            resv_adic += SumaSec;
                            dispo_base = 0;
                            dispo_adic = oto2 - (grp1 - oto1);
                        }
                        else
                        {
                            resv_base += (SumaPrin - SumaSec);
                            resv_adic += SumaSec;
                            dispo_base = 0;
                            dispo_adic = oto2 + oto1 - grp1;
                        }
                    }
                    else
                    {
                        resv_base += (SumaPrin - SumaSec);
                        resv_adic += SumaSec;
                        dispo_base = oto1 - grp1;
                        dispo_adic = oto2 + oto1 - grp1;
                    }
                    cap3b5.MontoReserva70Pri += (double)SumaPrin;
                    cap3b5.MontoReserva70Sec += (double)SumaSec;
                    cap3b5.MontoReservado70 = (double)resv_base;
                    cap3b5.MontoReservado70Adi = (double)resv_adic;
                    cap3b5.MontoDisponible70 = (double)dispo_base;
                    cap3b5.MontoDisponible70Adi = (double)dispo_adic;
                    cap3b5.SumaPrin = (double)SumaPrin;
                    cap3b5.SumaSec = (double)SumaSec;
                    cap3b5.Suma70 = (double)SumaPrin;
                    // Se Afecta margen del 70% del patrimonio efectivo (siempre)
                    this.iFlagCapitulo3b5 += 1;
                    // cap3b5.MontoReservado70 = cap3b5.MontoReservado70 + gSimulacion.dMontoDolares
                    // cap3b5.MontoDisponible70 = cap3b5.MontoOtorgado70 - (cap3b5.MontoUtilizado70 + cap3b5.MontoReservado70)

                    if (cap3b5.SumaSec > 0)
                    {

                    }
                }
                // margen 30 Limite Grupal
                if (grupprod.IsTipInGrupo(tip70, "30PERC"))
                {
                    // If chkAfecta30.Value = True Then
                    // Afecta margen del 30% del patrimonio efectivo
                    // gSimulacion.iFlagCapitulo3b5 = 2
                    this.iFlagCapitulo3b5 += 10;
                    cap3b5.MontoReservado30 += (double)mont70;
                    cap3b5.MontoDisponible30 = cap3b5.MontoOtorgado30 - (cap3b5.MontoUtilizado30 + cap3b5.MontoReservado30);
                    cap3b5.Suma30 = (double)mont70;

                }
                // margen 15 Operaciones Tipo G
                if (grupprod.IsTipInGrupo(tip70, "15PERC"))
                {
                    // If chkAfecta15.Value = True Then
                    // Afecta margen del 15% del patrimonio efectivo
                    this.iFlagCapitulo3b5 += 100;
                    cap3b5.MontoReservado15 += (double)mont70;
                    cap3b5.MontoDisponible15 = cap3b5.MontoOtorgado15 - (cap3b5.MontoUtilizado15 + cap3b5.MontoReservado15);
                    cap3b5.Suma15 = (double)mont70;

                    // sfv
                }
                // Cap 8 1o/11 RAN
                if (grupprod.IsTipInGrupo(tip70, "810RAN"))
                {
                    // If chkAfecta15.Value = True Then
                    // Afecta margen del 37.5% del patrimonio efectivo
                    this.iFlagCapitulo3b5 += 1000;
                    cap3b5.MontoReservadoCap8 += (double)mont70;
                    cap3b5.MontoDisponibleCap8 = cap3b5.MontoOtorgadoCap8 - (cap3b5.MontoUtilizadoCap8 + cap3b5.MontoReservadoCap8);
                    cap3b5.Suma08 = (double)mont70;

                    // sfv
                }
            }
            else if (liResultado == 3)
            {
                ltMensaje = "No existen márgenes del 30% y 70% Aplicables" + "\r" + "Informe al administrador del sistema";
                comments.Add("Capitulo3b5", ltMensaje);
            }
            else
            {
                ltMensaje = "Error al obtener los márgenes del 30% y 70%";
                errlist.AddRange(cap3b5.errlist);
                success = false;
            }
            this.lcapCapitulo3b5 = cap3b5;


            // Limites individuales
            StringBuilder sb = new StringBuilder();
            sb.Append("exec sva_cle_sim_lim_idv_ctz '");
            sb.Append(Global.ConvertirRutNro(this.tNumeroCliente));
            sb.Append("', '");
            sb.Append(Global.ConvertirRutNro(this.tNumeroCasaMatriz));
            sb.Append("', ");
            sb.Append(Convert.ToString(this.iCodigoPais));
            sb.Append(", ");
            sb.Append(Convert.ToString(this.iCodigoCateg));
            sb.Append(", '");
            sb.Append(this.tTipoProducto);
            sb.Append("', ");
            sb.Append(Convert.ToString(this.iCodigoProducto));
            sb.Append(", '");
            sb.Append(Global.ConvertirRutNro(this.tNumeroPerSuby));
            sb.Append("', '");
            sb.Append(this.tTipoProdSuby);
            sb.Append("', ");
            sb.Append(Convert.ToString(this.iCodigoPaisSuby));
            sb.Append(", ");
            sb.Append(Convert.ToString(this.iCodigoCategSuby));
            sb.Append(", ");
            sb.Append(Convert.ToString(this.iCargoCasaMatriz));
            sb.Append(", ");
            sb.Append((this.iEsNotaEstruct ? 1 : 0));
            sb.Append(", ");
            sb.Append((this.iEsOperPuntual ? 1 : 0));
            sb.Append(", ");
            sb.Append(Convert.ToString(this.dMontoAfecta).Replace(",", "."));
            sb.Append(", ");
            sb.Append(Convert.ToString(this.iDiasPlazo));
            sb.Append(", ");
            sb.Append(Convert.ToString(this.sPorcGarantia).Replace(",", "."));
            sb.Append(", ");
            sb.Append(Convert.ToString(this.dMontoGarantia).Replace(",", "."));
            ltComando = sb.ToString();

            msgc = string.Empty; msgp = string.Empty; this.iPlazoMinimo = -1;
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataSet dset = db.GetDataSet(ltComando);
                if (db.errlist.Count == 0)
                {
                    this.lrecLimites = dset.Tables[0];
                    foreach (DataRow row in this.lrecLimites.Rows)
                    {

                        if ((short)row["afe_plz_htd"] == 2)
                        {
                            msgc += "\r\n" + "Plazo Impedido por -> " + row["gls_dscrn_limte_indvd"].ToString();
                            this.iPlazoRestringido = true;
                            if (this.iPlazoMinimo < 0 || this.iPlazoMinimo > (int)row["afe_plz_can"])
                            {
                                this.iPlazoMinimo = 0;
                            }
                        }
                        else if ((short)row["afe_plz_htd"] == 1)
                        {
                            if ((int)row["delta_plazo"] < 0)
                            {
                                msgp += "\r\n" + "Plazo Restringido: Maximo = " + row["afe_plz_can"].ToString() + " días -> " + row["gls_dscrn_limte_indvd"].ToString();
                            }
                            if (this.iPlazoMinimo < 0 || this.iPlazoMinimo > (int)row["afe_plz_can"])
                            {
                                this.iPlazoMinimo = (int)row["afe_plz_can"];
                            }
                        }
                    }
                }
                else
                {
                    errlist.AddRange(db.errlist);
                    success = false;
                }
            }
            this.tPlzRestringido = msgc;
            this.tPlzAcotado = msgp;
            //this.lrecLimites.Requery();

            cap3b5 = null;
            grupprod = null;
            tipoprod = null;
            PresentaSimulacion();

            return success;
        }

        private void PresentaSimulacion()
        {
            string rotulo;
            if (this.iPlazoMinimo == -1)
            {
                this.lblDiasMaximo = "-";
            }
            else
            {
                this.lblDiasMaximo = Convert.ToString(this.iPlazoMinimo);
            }
            this.lblDiasPlazo = Convert.ToString(this.iDiasPlazo);
            if (this.iPlazoRestringido)
            {
                this.clrDiasMaximo = "#FF0000"; //.ForeColor = Color.Red;
                this.lblDiasMaximo = "NO HAB.";
                comments.Add("operestringida", "La Operacion esta Restringida por -Plazo No Habilitado- para este Producto/Cliente");

            }
            else
            {
                this.clrDiasMaximo = "#000000";
            }
            if (this.tPlzRestringido.Length > 0 || this.tPlzAcotado.Length > 0)
            {
                this.clrDiasPlazo = "#FF0000";
                if (!this.iPlazoRestringido)
                {
                    comments.Add("plazorestringido", "El plazo definido para la Operacion sobrepasa las Condiciones Definidas para este Producto/Cliente");
                }
            }
            else
            {
                this.clrDiasPlazo = "#000000";
            }
            // sfv  v3 Capitulo 3B5






            //DataTable listaafectados
            //List<string[]> stringList = null;

            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add("BackColor", System.Type.GetType("System.String"));
            dt.Columns.Add("Severidad", System.Type.GetType("System.String"));
            dt.Columns.Add("Nombre", System.Type.GetType("System.String"));
            dt.Columns.Add("Acuerdo", System.Type.GetType("System.String"));
            dt.Columns.Add("Moneda", System.Type.GetType("System.String"));
            dt.Columns.Add("Otorgado", System.Type.GetType("System.String"));
            dt.Columns.Add("Utilizado", System.Type.GetType("System.String"));
            dt.Columns.Add("Reservado", System.Type.GetType("System.String"));
            dt.Columns.Add("Disponible", System.Type.GetType("System.String"));
            dt.Columns.Add("Excedido", System.Type.GetType("System.String"));
            dt.Columns.Add("Vigencia", System.Type.GetType("System.String"));
            dt.Columns.Add("CodigoLimite", System.Type.GetType("System.String"));
            dt.Columns.Add("ReferenciaLimite", System.Type.GetType("System.String"));
            dt.Columns.Add("SumaTotal", System.Type.GetType("System.String"));
            dt.Columns.Add("Formula", System.Type.GetType("System.String"));
            dt.Columns.Add("LimKey", System.Type.GetType("System.String"));


            string moned = "US$";

            Capitulo3b5 lcapCapitulo3b5;
            lcapCapitulo3b5 = this.lcapCapitulo3b5;
            // margen  70 Limite Global
            char[] cFlagCapitulo3b5 = ("0000" + this.iFlagCapitulo3b5.ToString()).ToCharArray();
            if (cFlagCapitulo3b5[cFlagCapitulo3b5.Length - 1] == '1')
            //if (Strings.Right("000" + Convert.ToString(this.iFlagCapitulo3b5), 1) == "1")
            {
             Decimal mont70 = Convert.ToDecimal(this.dMontoAfecta);
                dr = dt.NewRow();
                dr["BackColor"] = "#CCCCCC"; //Color.FromArgb(220, 220, 220);
                dr["Severidad"] = "NOR"; // CAP3B5"
                dr["Nombre"] = "GLOBAL (70%) Cap. 3B5";
                dr["Acuerdo"] = string.Empty; // acuerdo
                dr["Moneda"] = "  US$";
                dr["Otorgado"] = lcapCapitulo3b5.MontoOtorgado70.ToString(new
                    System.Globalization.CultureInfo("en-US"));// string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoOtorgado70));
                dr["Utilizado"] = lcapCapitulo3b5.MontoUtilizado70.ToString(new
                    System.Globalization.CultureInfo("en-US")); // sprLimites.Text = string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoUtilizado70));
                dr["Reservado"] = lcapCapitulo3b5.MontoReservado70.ToString(new
                    System.Globalization.CultureInfo("en-US")); // string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoReservado70));
                dr["Disponible"] = lcapCapitulo3b5.MontoDisponible70.ToString(new
                    System.Globalization.CultureInfo("en-US")); // sprLimites.Text = string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoDisponible70));
                dr["Excedido"] = string.Empty;
                if (lcapCapitulo3b5.MontoDisponible70 < 0)
                {
                    dr["Excedido"] = "Excedido";
                }
                dr["Vigencia"] = lcapCapitulo3b5.Vigencia;
                dr["CodigoLimite"] = lcapCapitulo3b5.CodigoLimite.ToString();// Strings.Right("000" + Convert.ToString(lcapCapitulo3b5.CodigoLimite), 3);
                dr["ReferenciaLimite"] = "1";
                dr["SumaTotal"] = lcapCapitulo3b5.SumaPrin.ToString(new
                    System.Globalization.CultureInfo("en-US")); //  Text = string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.SumaPrin));
                dr["LimKey"] = Tools.CryptoTools.Encrypt(DateTime.Now.ToString()
                    + "\t" + lcapCapitulo3b5.CodigoLimite.ToString()
                    + "\t1"  
                    + "\t" + this.personaAfe70
                    + "\t" + this.dMontoAfe70.ToString(new
                        System.Globalization.CultureInfo("en-US"))
                    + "\t" + this.tTipoProducto
                        );
                dt.Rows.Add(dr);

                if (lcapCapitulo3b5.SumaSec > 0)
                {

                    dr = dt.NewRow();
                    dr["BackColor"] = "#CCCCCC"; //Color.FromArgb(220, 220, 220);
                    dr["Severidad"] = "NOR"; // CAP3B5"
                    dr["Nombre"] = "ADICIONAL (70%) Cap. 3B5";
                    dr["Acuerdo"] = string.Empty; // acuerdo
                    dr["Moneda"] = "  US$";
                    dr["Otorgado"] = lcapCapitulo3b5.MontoOtorgado70Adi.ToString(new
                        System.Globalization.CultureInfo("en-US"));// string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoOtorgado70));
                    dr["Utilizado"] = lcapCapitulo3b5.MontoUtilizado70Adi.ToString(new
                        System.Globalization.CultureInfo("en-US")); // sprLimites.Text = string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoUtilizado70));
                    dr["Reservado"] = lcapCapitulo3b5.MontoReservado70Adi.ToString(new
                        System.Globalization.CultureInfo("en-US")); // string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoReservado70));
                    dr["Disponible"] = lcapCapitulo3b5.MontoDisponible70Adi.ToString(new
                        System.Globalization.CultureInfo("en-US")); // sprLimites.Text = string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoDisponible70));
                    dr["Excedido"] = string.Empty;
                    if (lcapCapitulo3b5.MontoDisponible70Adi < 0)
                    {
                        dr["Excedido"] = "Excedido";
                    }
                    dr["Vigencia"] = lcapCapitulo3b5.Vigencia;
                    dr["CodigoLimite"] = lcapCapitulo3b5.CodigoLimite.ToString();// Strings.Right("000" + Convert.ToString(lcapCapitulo3b5.CodigoLimite), 3);
                    dr["ReferenciaLimite"] = "1";
                    dr["SumaTotal"] = lcapCapitulo3b5.SumaSec.ToString(new
                        System.Globalization.CultureInfo("en-US")); //  Text = string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.SumaPrin));
                    dr["LimKey"] = Tools.CryptoTools.Encrypt(DateTime.Now.ToString()
                        + "\t" + lcapCapitulo3b5.CodigoLimite.ToString()
                    + "\t1"
                    + "\t" + this.personaAfe70
                    + "\t" + this.dMontoAfe70.ToString(new
                        System.Globalization.CultureInfo("en-US"))
                    + "\t" + this.tTipoProducto
                        );

                    dt.Rows.Add(dr);
                }
            }
            // margen 30 Limite Grupal
            if (cFlagCapitulo3b5[cFlagCapitulo3b5.Length - 2] == '1')
            //if (("000" + this.iFlagCapitulo3b5.ToString()).PadRight(2).PadLeft(1) == "1")
            //if (Strings.Left(Strings.Right("000" + Convert.ToString(this.iFlagCapitulo3b5), 2), 1) == "1")
            {

                dr = dt.NewRow();
                dr["BackColor"] = "#CCCCCC"; //Color.FromArgb(220, 220, 220);
                dr["Severidad"] = "NOR"; // CAP3B5"
                dr["Nombre"] = "GRUPAL (30%) (D, F, G) Cap. 3B5";
                dr["Acuerdo"] = string.Empty; // acuerdo
                dr["Moneda"] = "  US$";
                dr["Otorgado"] = lcapCapitulo3b5.MontoOtorgado30.ToString(new
                    System.Globalization.CultureInfo("en-US"));// string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoOtorgado70));
                dr["Utilizado"] = lcapCapitulo3b5.MontoUtilizado30.ToString(new
                    System.Globalization.CultureInfo("en-US")); // sprLimites.Text = string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoUtilizado70));
                dr["Reservado"] = lcapCapitulo3b5.MontoReservado30.ToString(new
                    System.Globalization.CultureInfo("en-US")); // string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoReservado70));
                dr["Disponible"] = lcapCapitulo3b5.MontoDisponible30.ToString(new
                    System.Globalization.CultureInfo("en-US")); // sprLimites.Text = string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoDisponible70));
                dr["Excedido"] = string.Empty;
                if (lcapCapitulo3b5.MontoDisponible30 < 0)
                {
                    dr["Excedido"] = "Excedido";
                }
                dr["Vigencia"] = lcapCapitulo3b5.Vigencia;
                dr["CodigoLimite"] = lcapCapitulo3b5.CodigoLimite.ToString();// Strings.Right("000" + Convert.ToString(lcapCapitulo3b5.CodigoLimite), 3);
                dr["ReferenciaLimite"] = "2";
                dr["SumaTotal"] = lcapCapitulo3b5.Suma30.ToString(new
                    System.Globalization.CultureInfo("en-US")); //  Text = string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.SumaPrin));
                dr["LimKey"] = Tools.CryptoTools.Encrypt(DateTime.Now.ToString()
                    + "\t" + lcapCapitulo3b5.CodigoLimite.ToString()
                    + "\t2"  + "\t" + tNumeroCliente
                    + "\t" + this.dMontoAfecta.ToString(new
                        System.Globalization.CultureInfo("en-US"))
                    + "\t" + this.tTipoProducto
                        );

                dt.Rows.Add(dr);



            }
            // margen 15 Operaciones Tipo G
            if (cFlagCapitulo3b5[cFlagCapitulo3b5.Length - 3] == '1')
            //if (("000" + this.iFlagCapitulo3b5.ToString()).PadRight(3).PadLeft(1) == "1")
            //if (Strings.Left(Strings.Right("000" + Convert.ToString(this.iFlagCapitulo3b5), 3), 1) == "1")
            {

                dr = dt.NewRow();
                dr["BackColor"] = "#CCCCCC"; //Color.FromArgb(220, 220, 220);
                dr["Severidad"] = "NOR"; // CAP3B5"
                dr["Nombre"] = "OPERACIONES TIPO G Cap. 3B5";
                dr["Acuerdo"] = string.Empty; // acuerdo
                dr["Moneda"] = "  US$";
                dr["Otorgado"] = lcapCapitulo3b5.MontoOtorgado15.ToString(new
                    System.Globalization.CultureInfo("en-US"));// string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoOtorgado70));
                dr["Utilizado"] = lcapCapitulo3b5.MontoUtilizado15.ToString(new
                    System.Globalization.CultureInfo("en-US")); // sprLimites.Text = string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoUtilizado70));
                dr["Reservado"] = lcapCapitulo3b5.MontoReservado15.ToString(new
                    System.Globalization.CultureInfo("en-US")); // string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoReservado70));
                dr["Disponible"] = lcapCapitulo3b5.MontoDisponible15.ToString(new
                    System.Globalization.CultureInfo("en-US")); // sprLimites.Text = string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoDisponible70));
                dr["Excedido"] = string.Empty;
                if (lcapCapitulo3b5.MontoDisponible15 < 0)
                {
                    dr["Excedido"] = "Excedido";
                }
                dr["Vigencia"] = lcapCapitulo3b5.Vigencia;
                dr["CodigoLimite"] = lcapCapitulo3b5.CodigoLimite.ToString();// Strings.Right("000" + Convert.ToString(lcapCapitulo3b5.CodigoLimite), 3);
                dr["ReferenciaLimite"] = "3";
                dr["SumaTotal"] = lcapCapitulo3b5.Suma15.ToString(new
                    System.Globalization.CultureInfo("en-US")); //  Text = string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.SumaPrin));
                dr["LimKey"] = Tools.CryptoTools.Encrypt(DateTime.Now.ToString()
                    + "\t" + lcapCapitulo3b5.CodigoLimite.ToString()
                    + "\t3"  + "\t" + tNumeroCliente
                    + "\t" + this.dMontoAfecta.ToString(new
                        System.Globalization.CultureInfo("en-US"))
                    + "\t" + this.tTipoProducto
                        );
                dt.Rows.Add(dr);

            }
            // capitulo 8-10/11 RAN
            if (cFlagCapitulo3b5[cFlagCapitulo3b5.Length - 4] == '1')
            //    if (("0000" + this.iFlagCapitulo3b5.ToString()).PadRight(4).PadLeft(1) == "1")
            //if (Strings.Left(Strings.Right("0000" + Convert.ToString(this.iFlagCapitulo3b5), 4), 1) == "1")
            {

                dr = dt.NewRow();
                dr["BackColor"] = "#CCCCCC"; //Color.FromArgb(220, 220, 220);
                dr["Severidad"] = "NOR"; // CAP3B5"
                dr["Nombre"] = "CAPITULO 8-10/11 RAN";
                dr["Acuerdo"] = string.Empty; // acuerdo
                dr["Moneda"] = "  US$";
                dr["Otorgado"] = lcapCapitulo3b5.MontoOtorgadoCap8.ToString(new
                    System.Globalization.CultureInfo("en-US"));// string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoOtorgado70));
                dr["Utilizado"] = lcapCapitulo3b5.MontoUtilizadoCap8.ToString(new
                    System.Globalization.CultureInfo("en-US")); // sprLimites.Text = string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoUtilizado70));
                dr["Reservado"] = lcapCapitulo3b5.MontoReservadoCap8.ToString(new
                    System.Globalization.CultureInfo("en-US")); // string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoReservado70));
                dr["Disponible"] = lcapCapitulo3b5.MontoDisponibleCap8.ToString(new
                    System.Globalization.CultureInfo("en-US")); // sprLimites.Text = string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoDisponible70));
                dr["Excedido"] = string.Empty;
                if (lcapCapitulo3b5.MontoDisponibleCap8 < 0)
                {
                    dr["Excedido"] = "Excedido";
                }
                dr["Vigencia"] = lcapCapitulo3b5.Vigencia;
                dr["CodigoLimite"] = lcapCapitulo3b5.CodigoLimite.ToString();// Strings.Right("000" + Convert.ToString(lcapCapitulo3b5.CodigoLimite), 3);
                dr["ReferenciaLimite"] = "3";
                dr["SumaTotal"] = lcapCapitulo3b5.Suma08.ToString(new
                    System.Globalization.CultureInfo("en-US")); //  Text = string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.SumaPrin));
                dr["LimKey"] = Tools.CryptoTools.Encrypt(DateTime.Now.ToString()
                    + "\t" + lcapCapitulo3b5.CodigoLimite.ToString()
                    + "\t4"  + "\t" + tNumeroCliente
                    + "\t" + this.dMontoAfecta.ToString(new
                        System.Globalization.CultureInfo("en-US"))
                    + "\t" + this.tTipoProducto
                        );
                dt.Rows.Add(dr);
            }
            if (lrecLimites != null)
            {
                foreach (DataRow row in this.lrecLimites.Rows)
                {
                    dr = dt.NewRow();
                    if ((int)row["pge_rgl_lim"] > 0)
                    {
                        dr["Severidad"] = "NOR";
                        dr["BackColor"] = "#DDDDDD"; //Color.FromArgb(220, 220, 220);
                    }
                    else
                    {
                        dr["Severidad"] = "POL";
                        dr["BackColor"] = "#FFFFFF"; //Color.FromArgb(220, 220, 220);
                    }
                    rotulo = row["gls_dscrn_limte_indvd"].ToString();
                    //if (rotulo.Length > 55) rotulo = rotulo.Substring(0, 52) + "...";

                    dr["Nombre"] = rotulo;
                    dr["Acuerdo"] = row["lim_gls_acu"].ToString(); // acuerdo
                    dr["Moneda"] = row["moneda"].ToString();
                    dr["Otorgado"] = Decimal.Parse(row["mnt_otgmt_limte"].ToString()).ToString(new
                        System.Globalization.CultureInfo("en-US"));// string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoOtorgado70));
                    dr["Utilizado"] = Decimal.Parse(row["mnt_utlzd_limte"].ToString()).ToString(new
                        System.Globalization.CultureInfo("en-US")); // sprLimites.Text = string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoUtilizado70));
                    dr["Reservado"] = Decimal.Parse(row["nuevo_resv"].ToString()).ToString(new System.Globalization.CultureInfo("en-US")); // string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoReservado70));
                    dr["Disponible"] = Decimal.Parse(row["nuevo_dispo"].ToString()).ToString(new
                        System.Globalization.CultureInfo("en-US")); // sprLimites.Text = string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.MontoDisponible70));
                    dr["Excedido"] = string.Empty;
                    if (Decimal.Parse(row["nuevo_dispo"].ToString()) < 0)
                    {
                        dr["Excedido"] = "Excedido";
                    }
                    dr["Vigencia"] = row["status"];
                    dr["CodigoLimite"] = row["cod_limte"].ToString();// Strings.Right("000" + Convert.ToString(lcapCapitulo3b5.CodigoLimite), 3);
                    dr["ReferenciaLimite"] = row["nro_crtvo_limte"].ToString();
                    dr["SumaTotal"] = Decimal.Parse(row["tot_afe"].ToString()).ToString(new
                        System.Globalization.CultureInfo("en-US")); //  Text = string.Empty + Convert.ToString(modAjuste32.CvCom(lcapCapitulo3b5.SumaPrin));
                    dr["Formula"] = row["lim_gls_frm_clc"].ToString();
                    dr["LimKey"] = Tools.CryptoTools.Encrypt(DateTime.Now.ToString()
                        + "\t" + row["cod_limte"].ToString()
                        + "\t" + row["nro_crtvo_limte"].ToString()
                        + "\t" + tNumeroCliente
                        + "\t" + Decimal.Parse(this.dMontoAfecta.ToString(), NumberStyles.Number ).ToString(new
                        System.Globalization.CultureInfo("en-US"))
                    + "\t" + this.tTipoProducto
                        );

                    dt.Rows.Add(dr);


                }
            }
            PruebaCotizar();

            this.lcapCapitulo3b5 = null;
            this.lrecLimites = null;

            dt.AcceptChanges();

            listaafectados = dt;

            short ok = 0;

            Persona per = new Persona(dataConnectionString);
            Pais pai = new Pais(dataConnectionString);

            restricCliente = per.GetListRestricciones(ref ok, tNumeroCliente);
            riesgoCliente = per.GetListRiesgo(ref ok, tNumeroCliente);
            restricPais = pai.GetListRestricciones(ref ok, iCodigoPais);
            riesgoPais = pai.GetListRiesgo(ref ok, iCodigoPais);

            per = null;
            pai = null;

        }
        public bool PruebaCotizar()
        {
            bool success = false;
            Decimal rDispo;	// - "AutoDim"

            // Dim NroCot  As Long
            int totNorm = 0, totPoli = 0; string sTb;
            string strLim = "", moned, strMsg, strMsgp;

            //// sfv  v3 Capitulo 3B5
            //Capitulo3b5 lcapCapitulo3b5;
            //lcapCapitulo3b5 = modGlobal.gSimulacion.lcapCapitulo3b5;
            moned = "US$";
            // margen  70 Limite Global
            StringBuilder sbXML;
            sbXML = new StringBuilder();
            sbXML.Append(" ");
            sbXML.Append("\r\n");
            sbXML.Append("\t");
            sbXML.Append(" ->");
            // sTb = " " & vbCrLf & vbTab & " ->"
            sTb = sbXML.ToString();

            string flg;
            sbXML = new StringBuilder();
            sbXML.Append("0000");
            sbXML.Append(iFlagCapitulo3b5);
            flg = sbXML.ToString();

            sbXML = new StringBuilder();

            StringBuilder sbXML1 = new StringBuilder();

            char[] cFlagCapitulo3b5 = ("0000" + this.iFlagCapitulo3b5.ToString()).ToCharArray();
            if (cFlagCapitulo3b5[cFlagCapitulo3b5.Length - 1] == '1')
            //if (("000" + this.iFlagCapitulo3b5.ToString()).PadRight(1) == "1")
            //if (Strings.Right(flg, 1) == "1")
            {
                if (lcapCapitulo3b5.MontoDisponible70 < 0)
                {
                    totNorm += 1;
                    sbXML.Append("\r\n");
                    sbXML.Append("·");
                    sbXML.Append("GLOBAL (70%) Cap. 3B5");
                    sbXML.Append(sTb);
                    sbXML.Append(" Excedido: ");
                    sbXML.Append(moned);
                    sbXML.Append(" ");
                    sbXML.Append(lcapCapitulo3b5.MontoDisponible70
                        .ToString("0,000.00", new CultureInfo("en-US")));
                    // strMsg = strMsg & vbCrLf & "·" & strLim & sTb & " Excedido: " & moned & " " & FormatNumber(lcapCapitulo3b5.MontoDisponible70, 2, vbTrue, vbTrue, vbTrue)
                }
                if (lcapCapitulo3b5.SumaSec > 0)
                {
                    strLim = "ADICIONAL (70%) Cap. 3B5";
                    if (lcapCapitulo3b5.MontoDisponible70Adi < 0)
                    {
                        totNorm += 1;
                        sbXML.Append("\r\n");
                        sbXML.Append("·");
                        sbXML.Append("ADICIONAL (70%) Cap. 3B5");
                        sbXML.Append(sTb);
                        sbXML.Append(" Excedido: ");
                        sbXML.Append(moned);
                        sbXML.Append(" ");
                        sbXML.Append(lcapCapitulo3b5.MontoDisponible70Adi
                            .ToString("0,000.00", new CultureInfo("en-US")));
                        // strMsg = strMsg & vbCrLf & "·" & strLim & sTb & " Excedido: " & moned & " " & FormatNumber(lcapCapitulo3b5.MontoDisponible70Adi, 2, vbTrue, vbTrue, vbTrue)
                    }
                }
            }
            // margen 30 Limite Grupal
            if (cFlagCapitulo3b5[cFlagCapitulo3b5.Length - 2] == '1')
            //if (("000" + this.iFlagCapitulo3b5.ToString()).PadRight(2).PadLeft(1) == "1")
            //if (Strings.Left(Strings.Right(flg, 2), 1) == "1")
            {
                strLim = "GRUPAL (30%) (D, F, G) Cap. 3B5";
                if (lcapCapitulo3b5.MontoDisponible30 < 0)
                {
                    totNorm += 1;
                    sbXML.Append("\r\n");
                    sbXML.Append("·");
                    sbXML.Append("GRUPAL (30%) (D, F, G) Cap. 3B5");
                    sbXML.Append(sTb);
                    sbXML.Append(" Excedido: ");
                    sbXML.Append(moned);
                    sbXML.Append(" ");
                    sbXML.Append(lcapCapitulo3b5.MontoDisponible30
                        .ToString("0,000.00", new CultureInfo("en-US")));
                }
            }
            // margen 15 Operaciones Tipo G
            if (cFlagCapitulo3b5[cFlagCapitulo3b5.Length - 3] == '1')
            //if (("000" + this.iFlagCapitulo3b5.ToString()).PadRight(3).PadLeft(1) == "1")
            //if (Strings.Left(Strings.Right(flg, 3), 1) == "1")
            {
                strLim = "OPERACIONES TIPO G Cap. 3B5";
                if (lcapCapitulo3b5.MontoDisponible15 < 0)
                {
                    totNorm += 1;
                    sbXML.Append("\r\n");
                    sbXML.Append("·");
                    sbXML.Append("OPERACIONES TIPO G Cap. 3B5");
                    sbXML.Append(sTb);
                    sbXML.Append(" Excedido: ");
                    sbXML.Append(moned);
                    sbXML.Append(" ");
                    sbXML.Append(lcapCapitulo3b5.MontoDisponible15
                        .ToString("0,000.00", new CultureInfo("en-US")));
                }

            }
            // capitulo 8 10/11 RAN
            if (cFlagCapitulo3b5[cFlagCapitulo3b5.Length - 4] == '1')
            //if (("0000" + this.iFlagCapitulo3b5.ToString()).PadRight(4).PadLeft(1) == "1")
            //if (Strings.Left(Strings.Right(flg, 4), 1) == "1")
            {
                strLim = "CAPITULO 8-10/11 RAN";
                if (lcapCapitulo3b5.MontoDisponibleCap8 < 0)
                {
                    totNorm += 1;
                    sbXML.Append("\r\n");
                    sbXML.Append("·");
                    sbXML.Append("CAPITULO 8-10/11 RAN");
                    sbXML.Append(sTb);
                    sbXML.Append(" Excedido: ");
                    sbXML.Append(moned);
                    sbXML.Append(" ");
                    sbXML.Append(lcapCapitulo3b5.MontoDisponibleCap8
                        .ToString("0,000.00", new CultureInfo("en-US")));
                }

            }

            // Limites individuales
            string msgc = string.Empty;
            string msgp = string.Empty;

            iPlazoMinimo = -1;
            //lrecConsulta = modGlobal.gSimulacion.lrecLimites;
            if (lrecLimites != null)
            {
                foreach (DataRow row in this.lrecLimites.Rows)
                {

                    moned = row["moneda"].ToString();
                    strLim = row["gls_dscrn_limte_indvd"].ToString();
                    rDispo = Convert.ToDecimal(row["nuevo_dispo"]);
                    if ((double)rDispo < 0)
                    {
                        if ((int)row["pge_rgl_lim"] > 0)
                        {
                            totNorm += 1;
                            sbXML.Append("\r\n");
                            sbXML.Append("·");
                            sbXML.Append(strLim);
                            sbXML.Append(sTb);
                            sbXML.Append(" Excedido: ");
                            sbXML.Append(moned);
                            sbXML.Append(" ");
                            sbXML.Append(rDispo.ToString("0,000.00", new CultureInfo("en-US")));
                            // strMsg = strMsg & vbCrLf & "·" & strLim & sTb & " Excedido: " & moned & " " & FormatNumber(rDispo, 2, vbTrue, vbTrue, vbTrue)
                        }
                        else
                        {
                            totPoli += 1;
                            sbXML1.Append("\r\n");
                            sbXML1.Append("·");
                            sbXML1.Append(strLim);
                            sbXML1.Append(sTb);
                            sbXML1.Append(" Excedido: ");
                            sbXML1.Append(moned);
                            sbXML1.Append(" ");
                            sbXML1.Append(rDispo.ToString("0,000.00", new CultureInfo("en-US")));
                            // strMsgp = strMsgp & vbCrLf & "·" & strLim & sTb & " Excedido: " & moned & " " & FormatNumber(rDispo, 2, vbTrue, vbTrue, vbTrue)
                        }
                    }
                    if ((short)row["afe_plz_htd"] == 2)
                    {
                        msgc += "\r\n" + "·" + strLim + sTb + "PLAZO IMPEDIDO DE OPERAR";
                        iPlazoRestringido = true;
                        if (iPlazoMinimo < 0 || iPlazoMinimo > (int)row["afe_plz_can"])
                        {
                            iPlazoMinimo = 0;
                        }
                    }
                    else if ((short)row["afe_plz_htd"] == 1)
                    {
                        if ((int)row["delta_plazo"] < 0)
                        {
                            msgp += "\r\n" + "·" + strLim + sTb + "PLAZO RESTRINGIDO (Días): Máximo = " + row["afe_plz_can"].ToString() + "; Propuesto = " + iDiasPlazo.ToString() + "; Margen = " + row["delta_plazo"].ToString();
                        }
                        if (iPlazoMinimo < 0 || iPlazoMinimo > (int)row["afe_plz_can"])
                        {
                            iPlazoMinimo = Convert.ToInt32(row["afe_plz_can"]);
                        }
                    }

                }
            }
            strMsg = sbXML.ToString();
            sbXML = null;
            strMsgp = sbXML1.ToString();
            sbXML1 = null;

            iExedNorm = totNorm;
            tExedNormMsg = strMsg;
            iExedPoli = totPoli;
            tExedPoliMsg = strMsgp;
            tPlzRestringido = msgc;
            tPlzAcotado = msgp;

            if (totNorm > 0)
            {
                bPuedeCotizar = false;
                success = false;
            }
            else
            {
                bPuedeCotizar = true;
                success = true;
            }
            if (tPlzRestringido.Length > 0 || tPlzAcotado.Length > 0 || (totPoli > 0))
            {
                bPideExcepcion = true;
            }
            else
            {
                bPideExcepcion = false;
            }
            return success = false;
        }

    }
}
