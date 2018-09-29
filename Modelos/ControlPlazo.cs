using System;
using Microsoft.VisualBasic;
using System.Data;
using System.Collections.Generic;
using Modelos.Library;
using Tools;

namespace Modelos
{
    public class ControlPlazo
	{

		//=========================================================
		// Control de Plazos

		private struct Item
		{
			public string Glosa;
			public int Codigo;
		};

		private Item[] maTiposDeCredito = null;
		private Item[] maTiposDePlazo = null;

		public enum enumModoAcceso
		{
			PorCliente = 1,
			PorCategoria = 2
		};


		private string mtRUTCliente;
		private short miCategoria;
		private short miTipoCredito;
		private short miTipoPlazo;
		private int miDiasMax;

		private string mtTiposCreditoComboGrilla = "";
		private string mtTiposPlazoComboGrilla = "";

        private String dataConnectionString;
        public List<String> errlist = new List<string>();

        public ControlPlazo(String ConexionString)
            : base()
        {
            dataConnectionString = ConexionString;
        }


        public DataTable GetListTiposCredito(ref short suceso)
        {
            // Descripción : Obtiene la lista de tipos de creito para control de plazo
            // Parámetros  : Ninguno
            // Retorno     : 0 OK
            // 3 No existe
            // 4 Error en la base de datos
            // E. laterales: Ninguno
            // 
            // =============================================
            // Declaración de constantes/variables locales
            // =============================================
            string ltConsulta;
            ltConsulta = "exec svc_lce_tip_crd_ctl_plz";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                DataTable rec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count == 0)
                    {
                        suceso = 3;
                    }
                    else
                    {
                        suceso = 0;
                    }
                    return rec;
                }
                else
                {
                    suceso = 4;
                    errlist.AddRange(db.errlist);
                    //modLCEData.GenericError("ControlPlazo.GetListTiposCredito", Information.Err().Number, Information.Err().Description);
                }
            }
            return null;

        }



		public short Categoria
		{
			set
			{
				miCategoria = (short)value;
			}

			get
			{
					short Categoria = 0;
				Categoria = miCategoria;
				return Categoria;
			}
		}



		public string RUTCliente
		{
			set
			{
				mtRUTCliente = value;
			}

			get
			{
					string RUTCliente = "";
				RUTCliente = mtRUTCliente;
				return RUTCliente;
			}
		}



		public short TipoCredito
		{
			set
			{
				miTipoCredito = (short)value;
			}

			get
			{
					short TipoCredito = 0;
				TipoCredito = miTipoCredito;
				return TipoCredito;
			}
		}



		public short TipoPlazo
		{
			set
			{
				miTipoPlazo = (short)value;
			}

			get
			{
					short TipoPlazo = 0;
				TipoPlazo = miTipoPlazo;
				return TipoPlazo;
			}
		}



		public int PlazoMaximo
		{
			set
			{
				miDiasMax = value;
			}

			get
			{
					int PlazoMaximo = 0;
				PlazoMaximo = miDiasMax;
				return PlazoMaximo;
			}
		}



		public string TiposCreditoComboGrilla
		{
			get
			{
					string TiposCreditoComboGrilla = "";
				TiposCreditoComboGrilla = mtTiposCreditoComboGrilla;
				return TiposCreditoComboGrilla;
			}
		}

		public string TiposPlazoComboGrilla
		{
			get
			{
					string TiposPlazoComboGrilla = "";
				TiposPlazoComboGrilla = mtTiposPlazoComboGrilla;
				return TiposPlazoComboGrilla;
			}
		}


        //public ControlPlazo() : base()
        //{
        //    string ltListaCombo = "";
        //    VBtoRecordSet lrec;
        //    int li;
        //    cStringBuilder sbXML;

        //    sbXML = new cStringBuilder();
        //    // ltListaCombo = vbNullString
        //    li = 1;
        //    Array.Clear(maTiposDeCredito, 0, maTiposDeCredito.Length);
        //    if (0==GetListTiposCredito(ref lrec)) {
        //        while (!lrec.EOF) {
        //            Array.Resize(ref maTiposDeCredito, li + 1);
        //            maTiposDeCredito[li].Codigo = Convert.ToInt32(lrec.Fields["idx"].Value);
        //            maTiposDeCredito[li].Glosa = Strings.Trim(Convert.ToString(lrec.Fields["glosa"].Value));
        //            sbXML.Append(ref Strings.Trim(Convert.ToString(lrec.Fields["glosa"].Value)));
        //            sbXML.Append("\t");
        //            // ltListaCombo = ltListaCombo + Trim$(lRec("glosa")) + Chr(9)
        //            lrec.MoveNext();
        //            li += 1;
        //        }
        //        ltListaCombo = sbXML.Resolve;

        //        mtTiposCreditoComboGrilla = ltListaCombo;
        //        lrec.Close();
        //    }
        //    sbXML = null;

        //    sbXML = new cStringBuilder();
        //    // ltListaCombo = vbNullString
        //    li = 1;
        //    Array.Clear(maTiposDePlazo, 0, maTiposDePlazo.Length);
        //    if (0==GetListTiposPlazo(ref lrec)) {
        //        while (!lrec.EOF) {
        //            Array.Resize(ref maTiposDePlazo, li + 1);
        //            maTiposDePlazo[li].Codigo = Convert.ToInt32(lrec.Fields["idx"].Value);
        //            maTiposDePlazo[li].Glosa = Strings.Trim(Convert.ToString(lrec.Fields["glosa"].Value));
        //            sbXML.Append(ref Strings.Trim(Convert.ToString(lrec.Fields["glosa"].Value)));
        //            sbXML.Append("\t");
        //            // ltListaCombo = ltListaCombo + Trim$(lRec("glosa")) + Chr(9)
        //            lrec.MoveNext();
        //            li += 1;
        //        }
        //        ltListaCombo = sbXML.Resolve;
        //        mtTiposPlazoComboGrilla = ltListaCombo;
        //        lrec.Close();
        //    }
        //    sbXML = null;

        //}



        //public short GetList(ref VBtoRecordSet precControlPlazo, enumModoAcceso pModoAcceso)
        //{
        //    short GetList = 0;
        //    // Descripción : Obtiene la lista de estructuras de límites
        //    // Parámetros  : Ninguno
        //    // Retorno     : 0 OK
        //    // 3 No existe
        //    // 4 Error en la base de datos
        //    // E. laterales: Ninguno
        //    // 
        //    // =============================================
        //    // Declaración de constantes/variables locales
        //    // =============================================
        //    string ltConsulta;
        //    string ltLlave = "";
        //    string ltRUTNormalizado = "";

        //    cStringBuilder sbXML;

        //    try
        //    {	// On Error GoTo ManejoError

        //        sbXML = new cStringBuilder();
        //        switch (pModoAcceso) {
					
        //            case enumModoAcceso.PorCategoria:
        //            {
        //                sbXML.Append("exec svc_lce_ctl_pzo_cat ");
        //                sbXML.Append(ref miCategoria);
        //                // ltConsulta = "exec svc_lce_ctl_pzo_cat " & miCategoria
        //                break;
        //            }
        //            case enumModoAcceso.PorCliente:
        //            {
        //                ltRUTNormalizado = modGlobal.ConvertirRutNro(mtRUTCliente);
        //                sbXML.Append("exec svc_lce_ctl_pzo_cli '");
        //                sbXML.Append(ref ltRUTNormalizado);
        //                sbXML.Append("'");
        //                // ltConsulta = "exec svc_lce_ctl_pzo_cli '" & ltRUTNormalizado & "'"
        //                break;
        //            }
        //        } //end switch
        //        ltConsulta = sbXML.Resolve;
        //        sbXML = null;

        //        precControlPlazo = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltConsulta);

        //        if (precControlPlazo.BOF && precControlPlazo.EOF) {
        //            GetList = 3;
        //        } else {
        //            GetList = 0;
        //        }

        //        return GetList;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("ControlPlazo.Getlist", Information.Err().Number, Information.Err().Description);
        //        GetList = 4;

        //    }
        //    return GetList;
        //}


        //public short Guardar(enumModoAcceso pModoAcceso)
        //{
        //    short Guardar = 0;
        //    string ltComando;
        //    int liResultado = 0;
        //    cStringBuilder sbXML;

        //    sbXML = new cStringBuilder();
        //    switch (pModoAcceso) {
				
        //        case enumModoAcceso.PorCategoria:
        //        {
        //            sbXML.Append("exec sva_lce_ctl_pzo_asd_cat ");
        //            sbXML.Append(ref miCategoria);
        //            sbXML.Append(", ");
        //            sbXML.Append(ref miTipoCredito);
        //            sbXML.Append(", ");
        //            sbXML.Append(ref miTipoPlazo);
        //            sbXML.Append(", ");
        //            sbXML.Append(ref miDiasMax);
        //            // ltComando = "exec sva_lce_ctl_pzo_asd_cat " & miCategoria & ", " & miTipoCredito & ", " & miTipoPlazo & ", " & miDiasMax
        //            break;
        //        }
        //        case enumModoAcceso.PorCliente:
        //        {
        //            sbXML.Append("exec sva_lce_ctl_pzo_asd_cli '");
        //            sbXML.Append(ref mtRUTCliente);
        //            sbXML.Append("', ");
        //            sbXML.Append(ref miTipoCredito);
        //            sbXML.Append(", ");
        //            sbXML.Append(ref miTipoPlazo);
        //            sbXML.Append(", ");
        //            sbXML.Append(ref miDiasMax);
        //            // ltComando = "exec sva_lce_ctl_pzo_asd_cli '" & mtRUTCliente & "', " & miTipoCredito & ", " & miTipoPlazo & ", " & miDiasMax
        //            break;
        //        }
        //    } //end switch
        //    ltComando = sbXML.Resolve;
        //    sbXML = null;

        //    try
        //    {	// On Error GoTo ManejoError

        //        modAjuste32.gbasAlce_Execute(ltComando, ref liResultado);

        //        if (liResultado==1) {
        //            Guardar = 0;
        //        } else {
        //            Guardar = 3;
        //        }

        //        return Guardar;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("ControlPlazo.Guardar", Information.Err().Number, Information.Err().Description);
        //        Guardar = 3;

        //    }
        //    return Guardar;
        //}

        //public short Eliminar(enumModoAcceso pModoAcceso)
        //{
        //    short Eliminar = 0;
        //    const int curOnErrorGoToLabel_Default = 0;
        //    const int curOnErrorGoToLabel_ManejoError = 1;
        //    const int curOnErrorGoToLabel_ManejoError = 2;
        //    int vOnErrorGoToLabel = curOnErrorGoToLabel_Default;
        //    try
        //    {
        //        string ltComando;
        //        int liResultado = 0;

        //        cStringBuilder sbXML;

        //        vOnErrorGoToLabel = curOnErrorGoToLabel_ManejoError; /* On Error GoTo ManejoError */

        //        sbXML = new cStringBuilder();
        //        switch (pModoAcceso) {
					
        //            case enumModoAcceso.PorCategoria:
        //            {
        //                sbXML.Append("exec sva_lce_eli_ctl_pzo_cat ");
        //                sbXML.Append(ref miCategoria);
        //                // ltComando = "exec sva_lce_eli_ctl_pzo_cat " & miCategoria
        //                break;
        //            }
        //            case enumModoAcceso.PorCliente:
        //            {
        //                sbXML.Append("exec sva_lce_eli_ctl_pzo_cli '");
        //                sbXML.Append(ref mtRUTCliente);
        //                sbXML.Append("'");
        //                // ltComando = "exec sva_lce_eli_ctl_pzo_cli '" & mtRUTCliente & "'"
        //                break;
        //            }
        //        } //end switch
        //        ltComando = sbXML.Resolve;
        //        sbXML = null;

        //        vOnErrorGoToLabel = curOnErrorGoToLabel_ManejoError; /* On Error GoTo ManejoError */

        //        modAjuste32.gbasAlce_Execute(ltComando, ref liResultado);

        //        if (liResultado>=1) {
        //            Eliminar = 0;
        //        } else {
        //            Eliminar = 3;
        //        }

        //        return Eliminar;

        //    ManejoError: ;
        //        modLCEData.GenericError("ControlPlazo.Eliminar", Information.Err().Number, Information.Err().Description);
        //        Eliminar = 3;


        //    }
        //    catch
        //    {
        //        switch(vOnErrorGoToLabel) {
        //            default:
        //            case curOnErrorGoToLabel_Default:
        //                // ...
        //                break;
        //            case curOnErrorGoToLabel_ManejoError:
        //                //? goto ManejoError;
        //                break;
        //            case curOnErrorGoToLabel_ManejoError:
        //                //? goto ManejoError;
        //                break;
        //        }
        //    }
        //    return Eliminar;
        //}



        //public short GetListTiposPlazo(ref VBtoRecordSet precTiposPlazo)
        //{
        //    short GetListTiposPlazo = 0;
        //    // Descripción : Obtiene la lista de tipos de plazo para control de plazo
        //    // Parámetros  : Ninguno
        //    // Retorno     : 0 OK
        //    // 3 No existe
        //    // 4 Error en la base de datos
        //    // E. laterales: Ninguno
        //    // 
        //    // =============================================
        //    // Declaración de constantes/variables locales
        //    // =============================================
        //    string ltConsulta;
        //    string ltLlave = "";

        //    try
        //    {	// On Error GoTo ManejoError

        //        ltConsulta = "exec svc_lce_lst_tip_pzo_ctl_plz";

        //        precTiposPlazo = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltConsulta);

        //        if (precTiposPlazo.BOF && precTiposPlazo.EOF) {
        //            GetListTiposPlazo = 3;
        //        } else {
        //            GetListTiposPlazo = 0;
        //        }

        //        return GetListTiposPlazo;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("PlazoOperacion.GetListTiposPlazo", Information.Err().Number, Information.Err().Description);
        //        GetListTiposPlazo = 4;

        //    }
        //    return GetListTiposPlazo;
        //}


        //public void InicializaGrilla( /* vaSpread sprPlazos */ )
        //{
        //    string sListaCombo;
        //    int li;
        //    VBtoRecordSet lrec;
        //    cStringBuilder sbXML;


        //    sprPlazos.GrayAreaBackColor = sprPlazos.Parent.BackColor;

        //    // cargar comboboxes de columna 1 (tipo de credito)
        //    // guardar relacion glosa-codigo para cada item en maTiposCredito
        //    sprPlazos.Col = 1;
        //    sprPlazos.Row = 1;
        //    sprPlazos.Col2 = 1;
        //    sprPlazos.Row2 = sprPlazos.MaxRows;
        //    sprPlazos.BlockMode = true;
        //    sbXML = new cStringBuilder();
        //    sListaCombo = string.Empty;
        //    li = 1;
        //    Array.Clear(maTiposDeCredito, 0, maTiposDeCredito.Length);
        //    if (0==GetListTiposCredito(ref lrec)) {
        //        while (!lrec.EOF) {
        //            Array.Resize(ref maTiposDeCredito, li + 1);
        //            maTiposDeCredito[li].Codigo = Convert.ToInt32(lrec.Fields["idx"].Value);
        //            maTiposDeCredito[li].Glosa = Strings.Trim(Convert.ToString(lrec.Fields["glosa"].Value));
        //            sbXML.Append(ref Strings.Trim(Convert.ToString(lrec.Fields["glosa"].Value)));
        //            sbXML.Append("\t");
        //            // sListaCombo = sListaCombo + Trim$(lRec("glosa")) + Chr(9)
        //            lrec.MoveNext();
        //            li += 1;
        //        }
        //        sListaCombo = sbXML.Resolve;
        //        sprPlazos.TypeComboBoxList = sListaCombo;
        //    }
        //    sbXML = null;
        //    sprPlazos.TypeComboBoxList = sListaCombo;

        //    // cargar comboboxes de columna 2 (tipo de plazo)
        //    // guardar relacion glosa-codigo para cada item en maTiposPlazo
        //    sprPlazos.Col = 2;
        //    sprPlazos.Row = 1;
        //    sprPlazos.Col2 = 2;
        //    sprPlazos.Row2 = sprPlazos.MaxRows;
        //    sprPlazos.BlockMode = true;
        //    sbXML = new cStringBuilder();
        //    // sListaCombo = vbNullString
        //    li = 1;
        //    Array.Clear(maTiposDePlazo, 0, maTiposDePlazo.Length);
        //    if (0==GetListTiposPlazo(ref lrec)) {
        //        while (!lrec.EOF) {
        //            Array.Resize(ref maTiposDePlazo, li + 1);
        //            maTiposDePlazo[li].Codigo = Convert.ToInt32(lrec.Fields["idx"].Value);
        //            maTiposDePlazo[li].Glosa = Strings.Trim(Convert.ToString(lrec.Fields["glosa"].Value));
        //            sbXML.Append(ref Strings.Trim(Convert.ToString(lrec.Fields["glosa"].Value)));
        //            sbXML.Append("\t");
        //            // sListaCombo = sListaCombo + Trim$(lRec("glosa")) + Chr(9)
        //            lrec.MoveNext();
        //            li += 1;
        //        }
        //    }
        //    sListaCombo = sbXML.Resolve;
        //    sbXML = null;
        //    sprPlazos.TypeComboBoxList = sListaCombo;
        //    sprPlazos.BlockMode = false;

        //    lrec.Close();
        //}

        //// Obtiene codigo correspondiente a una glosa de Tipos de Credito
        //// Utiliza arreglo maTiposDeCredito cargado previemente en InicializaGrillaPlazos
        //// VBto upgrade warning: 'Return' As short	OnWrite(int)
        //public short CodigoTipoCredito(string tGlosa)
        //{
        //    short CodigoTipoCredito = 0;
        //    int li;

        //    CodigoTipoCredito = -1;
        //    try
        //    {	// On Error GoTo ManejoError
        //        for(li=Information.LBound(maTiposDeCredito, 1); li<=Information.UBound(maTiposDeCredito, 1); li++) {
        //            if (Strings.Trim(tGlosa)==maTiposDeCredito[li].Glosa) {
        //                CodigoTipoCredito = (short)maTiposDeCredito[li].Codigo;
        //                break;
        //            }
        //        } // li
        //        return CodigoTipoCredito;
        //    }
        //    catch
        //    {	// ManejoError:
        //        CodigoTipoCredito = -1;

        //    }
        //    return CodigoTipoCredito;
        //}

        //// Obtiene codigo correspondiente a una glosa de Tipos de Plazo
        //// Utiliza arreglo maTiposDePlazo cargado previemente en InicializaGrillaPlazos
        //// VBto upgrade warning: 'Return' As short	OnWrite(int)
        //public short CodigoTipoPlazo(string tGlosa)
        //{
        //    short CodigoTipoPlazo = 0;
        //    int li;

        //    CodigoTipoPlazo = -1;
        //    try
        //    {	// On Error GoTo ManejoError
        //        for(li=Information.LBound(maTiposDePlazo, 1); li<=Information.UBound(maTiposDePlazo, 1); li++) {
        //            if (Strings.Trim(tGlosa)==maTiposDePlazo[li].Glosa) {
        //                CodigoTipoPlazo = (short)maTiposDePlazo[li].Codigo;
        //                break;
        //            }
        //        } // li
        //        return CodigoTipoPlazo;
        //    }
        //    catch
        //    {	// ManejoError:
        //        CodigoTipoPlazo = -1;

        //    }
        //    return CodigoTipoPlazo;
        //}

        //// Obtiene glosa correspondiente a un codigo de Tipos de Credito
        //// Utiliza arreglo maTiposDeCredito cargado previemente en InicializaGrillaPlazos
        //public string GlosaTipoCredito(int iCodigo)
        //{
        //    string GlosaTipoCredito = "";
        //    int li;

        //    GlosaTipoCredito = "<Indefinido>";
        //    try
        //    {	// On Error GoTo ManejoError
        //        for(li=Information.LBound(maTiposDeCredito, 1); li<=Information.UBound(maTiposDeCredito, 1); li++) {
        //            if (iCodigo==maTiposDeCredito[li].Codigo) {
        //                GlosaTipoCredito = maTiposDeCredito[li].Glosa;
        //                break;
        //            }
        //        } // li
        //        return GlosaTipoCredito;
        //    }
        //    catch
        //    {	// ManejoError:
        //        GlosaTipoCredito = "<Indefinido>";

        //    }
        //    return GlosaTipoCredito;
        //}

        //// Obtiene glosa correspondiente a un codigo de Tipos de Credito
        //// Utiliza arreglo maTiposDeCredito cargado previemente en InicializaGrillaPlazos
        //public string GlosaTipoPlazo(int iCodigo)
        //{
        //    string GlosaTipoPlazo = "";
        //    int li;

        //    GlosaTipoPlazo = "<Indefinido>";
        //    try
        //    {	// On Error GoTo ManejoError
        //        for(li=Information.LBound(maTiposDePlazo, 1); li<=Information.UBound(maTiposDePlazo, 1); li++) {
        //            if (iCodigo==maTiposDePlazo[li].Codigo) {
        //                GlosaTipoPlazo = maTiposDePlazo[li].Glosa;
        //                break;
        //            }
        //        } // li
        //        return GlosaTipoPlazo;
        //    }
        //    catch
        //    {	// ManejoError:
        //        GlosaTipoPlazo = "<Indefinido>";

        //    }
        //    return GlosaTipoPlazo;
        //}

        //public short ListaPlazosAfectados(ref VBtoRecordSet lrec)
        //{
        //    short ListaPlazosAfectados = 0;
        //    string ltConsulta;
        //    string ltLlave = "";
        //    string ltRUTCliente;

        //    try
        //    {	// On Error GoTo ManejoError

        //        ltRUTCliente = Strings.Right(Strings.StrDup(10, "0")+modGlobal.ConvertirRutNro(mtRUTCliente), 10);
        //        // ltRUTCliente = Mid$(ltRUTCliente, 1, Len(ltRUTCliente) - 1)
        //        if (Strings.Trim(ltRUTCliente)==string.Empty) { ListaPlazosAfectados = 3; return ListaPlazosAfectados; }
        //        ltConsulta = "exec svc_lst_tip_ctl_pzo '"+ltRUTCliente+"', "+Convert.ToString(miTipoCredito);

        //        lrec = modAjuste32.GetRecordset(modLCEData.gbasAlce, ltConsulta);

        //        if (lrec.BOF && lrec.EOF) {
        //            ListaPlazosAfectados = 3;
        //        } else {
        //            ListaPlazosAfectados = 0;
        //        }

        //        return ListaPlazosAfectados;

        //    }
        //    catch
        //    {	// ManejoError:
        //        modLCEData.GenericError("ControlPlazo.ListaPlazosAfectados", Information.Err().Number, Information.Err().Description);
        //        ListaPlazosAfectados = 4;

        //    }
        //    return ListaPlazosAfectados;
        //}




	}
}