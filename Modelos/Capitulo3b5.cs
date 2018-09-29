using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Tools;
using Modelos.Library;

namespace Modelos
{
   
	public class Capitulo3b5
	{
        public Capitulo3b5(String ConexionString)
            : base()
        {
            dataConnectionString = ConexionString;
        }
        private String dataConnectionString;
        public List<String> errlist;

		//=========================================================
		// sfv v3
		public int CodigoLimite;
		public DateTime FechaOtorgamiento;
		public DateTime FechaVencimiento;
		public double MontoOtorgado15;
		public double MontoUtilizado15;
		// VBto upgrade warning: MontoDisponible15 As double	OnRead(Decimal)
		public double MontoDisponible15;
		// VBto upgrade warning: MontoReservado15 As double	OnWrite(short, Decimal, double)
		public double MontoReservado15;
		public double TotalInteres15;
		public double MontoOtorgado30;
		public double MontoUtilizado30;
		// VBto upgrade warning: MontoDisponible30 As double	OnRead(Decimal)
		public double MontoDisponible30;
		// VBto upgrade warning: MontoReservado30 As double	OnWrite(short, Decimal, double)
		public double MontoReservado30;
		public double TotalInteres30;
		// VBto upgrade warning: MontoOtorgado70 As double	OnRead(Decimal)
		public double MontoOtorgado70;
		public double MontoUtilizado70;
		// VBto upgrade warning: MontoDisponible70 As double	OnWrite(short, Decimal)	OnRead(Decimal)
		public double MontoDisponible70;
		// VBto upgrade warning: MontoReservado70 As double	OnWrite(short, Decimal)	OnRead(Decimal)
		public double MontoReservado70;
		public double TotalInteres70;
		// VBto upgrade warning: MontoOtorgado70Adi As double	OnRead(Decimal)
		public double MontoOtorgado70Adi;
		public double MontoUtilizado70Adi;
		// VBto upgrade warning: MontoDisponible70Adi As double	OnWrite(short, Decimal)
		public double MontoDisponible70Adi;
		// VBto upgrade warning: MontoReservado70Adi As double	OnWrite(short, Decimal)	OnRead(Decimal)
		public double MontoReservado70Adi;
		// VBto upgrade warning: MontoReserva70Pri As double	OnWrite(short, Decimal)
		public double MontoReserva70Pri;
		// VBto upgrade warning: MontoReserva70Sec As double	OnWrite(short, Decimal)
		public double MontoReserva70Sec;
		public double MontoUtiliza70Pri;
		public double MontoUtiliza70Sec;
		public double MontoOtorgadoCap8;
		public double MontoUtilizadoCap8;
		public double MontoDisponibleCap8;
		// VBto upgrade warning: MontoReservadoCap8 As double	OnWrite(short, Decimal)
		public double MontoReservadoCap8;

		// VBto upgrade warning: CodSobBas As byte	OnWrite(short)
		public byte CodSobBas; // 

		// VBto upgrade warning: SumaPrin As double	OnWrite(Decimal)
		public double SumaPrin;
		// VBto upgrade warning: SumaSec As double	OnWrite(Decimal)
		public double SumaSec;
		public string Vigencia = "";
		// VBto upgrade warning: Suma70 As double	OnWrite(Decimal)
		public double Suma70;
		// VBto upgrade warning: Suma30 As double	OnWrite(Decimal)
		public double Suma30;
		// VBto upgrade warning: Suma15 As double	OnWrite(Decimal)
		public double Suma15;
		// VBto upgrade warning: Suma08 As double	OnWrite(Decimal)
		public double Suma08;

		// 



		public short Obtener(DateTime pxFecha)
		{
			short suceso= 0;
			// Descripción : Obtiene el limite capítulo 3.b.5
			// Parámetros  : pxFecha
			// Retorno     : 0 OK
			// 3 No existe
			// 4 Error en la BD
			// E. laterales: Ninguno
			// 
			// =============================================
			// Declaración de constantes/variables locales
			// =============================================
            string ltConsulta;
            DataTable rec = new DataTable();
            ltConsulta = "exec svc_cle_obt_lim_cit_3b5 '" + pxFecha.ToString("yyyyMMdd") + "'";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                rec = db.GetRecordset(ltConsulta);
                if (db.errlist.Count == 0)
                {
                    if (rec.Rows.Count == 0)
                    {
                        suceso = 3;
                    }
                    else
                    {
                        suceso = 0;
                        CodigoLimite = Convert.ToInt32(rec.Rows[0]["cod_limte"]);
                        FechaOtorgamiento = (DateTime)rec.Rows[0]["fch_otgmt"];
                        FechaVencimiento = (DateTime)rec.Rows[0]["fch_vncto"];
                        Vigencia = "VIG";
                        if (FechaVencimiento.ToOADate() < DateTime.Today.ToOADate()) Vigencia = "vnc";

                        MontoOtorgado15 = Convert.ToDouble(rec.Rows[0]["mnt_total_15_prcnt"]);
                        MontoReservado15 = Convert.ToDouble(rec.Rows[0]["mnt_rsrvd_15_prcnt"]);
                        MontoUtilizado15 = Convert.ToDouble(rec.Rows[0]["mnt_utlzd_15_prcnt"]);
                        MontoDisponible15 = MontoOtorgado15 - (MontoReservado15 + MontoUtilizado15); // 0 + lrecLimite("mnt_dispo_15_prcnt")
                        // TotalInteres15 = 0 + lrecLimite("mnt_total_intrs_15_prcnt")

                        MontoOtorgado30 = Convert.ToDouble(rec.Rows[0]["mnt_total_30_prcnt"]);
                        MontoReservado30 = Convert.ToDouble(rec.Rows[0]["mnt_rsrvd_30_prcnt"]);
                        MontoUtilizado30 = Convert.ToDouble(rec.Rows[0]["mnt_utlzd_30_prcnt"]);
                        MontoDisponible30 = MontoOtorgado30 - (MontoReservado30 + MontoUtilizado30); // 0 + lrecLimite("mnt_dispo_30_prcnt")
                        // TotalInteres30 = 0 + lrecLimite("mnt_total_intrs_30_prcnt")

                        MontoOtorgado70 = Convert.ToDouble(rec.Rows[0]["mnt_total_70_prcnt"]);
                        MontoDisponible70 = Convert.ToDouble(rec.Rows[0]["mnt_dispo_70_prcnt"]);
                        MontoUtilizado70 = Convert.ToDouble(rec.Rows[0]["mnt_utlzd_70_prcnt"]);
                        MontoReservado70 = Convert.ToDouble(rec.Rows[0]["mnt_rsrvd_70_prcnt"]);
                        // TotalInteres70 = Convert.ToDouble(lrecLimite("mnt_total_intrs_70_prcnt")

                        MontoOtorgado70Adi = Convert.ToDouble(rec.Rows[0]["mnt_tot_set_adi"]);
                        MontoUtilizado70Adi = Convert.ToDouble(rec.Rows[0]["mnt_uso_set_adi"]);
                        MontoDisponible70Adi = Convert.ToDouble(rec.Rows[0]["mnt_dis_set_adi"]);
                        MontoReservado70Adi = Convert.ToDouble(rec.Rows[0]["mnt_rsv_set_adi"]);
                        MontoReserva70Pri = Convert.ToDouble(rec.Rows[0]["mnt_rsv_set_adi_pri"]);
                        MontoReserva70Sec = Convert.ToDouble(rec.Rows[0]["mnt_rsv_set_adi_sec"]);
                        MontoUtiliza70Pri = Convert.ToDouble(rec.Rows[0]["mnt_uso_set_adi_pri"]);
                        MontoUtiliza70Sec = Convert.ToDouble(rec.Rows[0]["mnt_uso_set_adi_sec"]);
                        CodSobBas = Convert.ToByte((VBtoConverter.IsNull(rec.Rows[0]["cod_sob_bas"]) ? 0 : rec.Rows[0]["cod_sob_bas"]));

                        MontoOtorgadoCap8 = Convert.ToDouble((VBtoConverter.IsNull(rec.Rows[0]["ltn_mnt_tot_cit_och"]) ? 0 : rec.Rows[0]["ltn_mnt_tot_cit_och"]));
                        MontoReservadoCap8 = Convert.ToDouble((VBtoConverter.IsNull(rec.Rows[0]["ltn_mnt_rsv_cit_och"]) ? 0 : rec.Rows[0]["ltn_mnt_rsv_cit_och"]));
                        MontoUtilizadoCap8 = Convert.ToDouble((VBtoConverter.IsNull(rec.Rows[0]["ltn_mnt_uso_cit_och"]) ? 0 : rec.Rows[0]["ltn_mnt_uso_cit_och"]));
                        MontoDisponibleCap8 = MontoOtorgadoCap8 - (MontoReservadoCap8 + MontoUtilizadoCap8);


                    }
                }
                else
                {
                    suceso = 4;
                    errlist.AddRange(db.errlist);
                   // Global.GenericError("Capitulo3b5.Obtener", Information.Err().Number, Information.Err().Description);
                }
            }
            return suceso;

		}
		public short Modificar()
		{
			// Descripción : Modifica un Límite Capitulo 3b5
			// Parámetros  : Ninguno
			// Retorno     : 0 OK
			// 3 Error al modificar
			// E. laterales: Ninguno
			// 
			// =============================================
			// Declaración de constantes/variables locales
			// =============================================
            string ltComando;
            short suceso = 0;
            ltComando = "exec sva_cle_act_cap_3b5 ";
            ltComando += "" + Global.ConvertirComaPorPunto(Convert.ToString(MontoOtorgado15));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(TotalInteres15));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoUtilizado15));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoDisponible15));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoReservado15));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoOtorgado30));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(TotalInteres30));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoUtilizado30));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoDisponible30));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoReservado30));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoOtorgado70));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(TotalInteres70));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoUtilizado70));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoDisponible70));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoReservado70));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoOtorgado70Adi));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoUtilizado70Adi));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoDisponible70Adi));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoReservado70Adi));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoReserva70Pri));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoReserva70Sec));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoUtiliza70Pri));
            ltComando += "," + Global.ConvertirComaPorPunto(Convert.ToString(MontoUtiliza70Sec));

            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                int success = db.ExecuteCommand(ltComando);
                if (db.errlist.Count == 0)
                {
                    suceso = 0;
                }
                else
                {
                    //Global.GenericError("Limite.Modificar", Information.Err().Number, Information.Err().Description);
                    errlist.AddRange(db.errlist);
                    suceso = 3;

                }

            }
            return suceso;
		}

		public DataTable GetList(ref short  suceso)
		{
			// Descripción : Obtiene la lista de limites capitulo 3.b5
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
            DataTable rec = new DataTable();
            ltConsulta = "exec sp_lce_cna_lst_lmtcn_captl_b5 ";
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                rec = db.GetRecordset(ltConsulta);
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
                }
                else
                {
                    suceso = 4;
                    //Global.GenericError("Capitulo3b5.Getlist", Information.Err().Number, Information.Err().Description);
                    errlist.AddRange(db.errlist);
                }
            }
            return rec;
		}
		public short GenerarV3()
		{
			// Descripción : Genera o actualiza los montos otorgados de 30% y 70%
			// a una fecha, según el monto del Patrimonio
			// Version 3.0
			// Parámetros  : pdMonto, pxFecha
			// Retorno     : 0 OK
			// 3 Error en la base de datos
			// E. laterales: Ninguno
			// 
			// =============================================
			// Declaración de constantes/variables locales
			// =============================================
            string ltComando;
            ltComando = "exec sva_cle_gnr_ltn_cit "; // 19032008
            short success = 0;
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                int resultado = db.ExecuteCommand(ltComando);
                if (db.errlist.Count == 0)
                {
                    if (resultado == 1)
                    {
                        success = 0;
                    }
                    else
                    {
                        success = 3;
                    }
                }
                else
                {
                    //Global.GenericError("Capitulo3b5.GenerarV3", Information.Err().Number, Information.Err().Description);
                    errlist.AddRange(db.errlist);
                    success = 3;
                }

            }
            return success;
		}

		public short Generar(double pdMonto, double pdActPond, DateTime pxFecha)
		{
			// Descripción : Genera o actualiza los montos otorgados de 30% y 70%
			// a una fecha, según el monto del Patrimonio
			// Parámetros  : pdMonto, pxFecha
			// Retorno     : 0 OK
			// 3 Error en la base de datos
			// E. laterales: Ninguno
			// 
			// =============================================
			// Declaración de constantes/variables locales
			// =============================================
            string ltComando;
            short suceso = 0;
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                ltComando = "sp_slc_tmosp_exist";
                DataTable prec = db.GetRecordset(ltComando);
                if (db.errlist.Count == 0)
                {
                    if (prec.Rows.Count == 0)
                    {
                        suceso = 2;
                    }
                    else
                    {
                        ltComando = "exec sp_lce_gnr_cap_3b5 " + Global.ConvertirComaPorPunto(Convert.ToString(pdMonto)) + ", " + Global.ConvertirComaPorPunto(Convert.ToString(pdActPond)) + ",'" + pxFecha.ToString("yyyy/mm/dd") + "'";
                        // liResultado = gbasAlce_ExecuteSQL(ltComando)
                        int resultado = db.ExecuteCommand(ltComando);
                        suceso = 0;
                    }
                }
                else
                {
                    // ManejoError:
                    //modLCEData.GenericError("RelacionLimiteCotizacion.Getlist", Information.Err().Number, Information.Err().Description);
                    errlist.AddRange(db.errlist);
                    suceso = 3;

                }
            } 
                return suceso;
		}

		public short AnularReserva3b5(int nroCot)
		{
			// Descripción : Anula Reserva de un Límite Capitulo 3b5
			// Parámetros  : Ninguno
			// Retorno     : >0 OK
			// -3 Error al AnularReserva3b5
			// =============================================
            string ltComando;
            ltComando = "exec sva_cle_anc_rsv_cit_ctz " + Convert.ToString(nroCot);
            short success = 0;
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                int resultado = db.ExecuteCommand(ltComando);
                if (db.errlist.Count == 0)
                {
                        success = (short)resultado;
                }
                else
                {
                    //Global.GenericError("Capitulo3b5.GenerarV3", Information.Err().Number, Information.Err().Description);
                    errlist.AddRange(db.errlist);
                    success = -3;
                }

            }
            return success;
		}


		public short Reservar3b5(int nroCot)
		{
			// Descripción : Anula Reserva de un Límite Capitulo 3b5
			// Parámetros  : Ninguno
			// Retorno     : >0 OK
			// -3 Error al Reservar3b5
			// =============================================
            string ltComando;
            ltComando = "exec sva_bth_apl_rsv_cap_3b5 " + Convert.ToString(nroCot);
            short success = 0;
            using (DataFinder db = new DataFinder(dataConnectionString))
            {
                int resultado = db.ExecuteCommand(ltComando);
                if (db.errlist.Count == 0)
                {
                    success = (short)resultado;
                }
                else
                {
                    //Global.GenericError("Capitulo3b5.Reservar3b5", Information.Err().Number, Information.Err().Description);
                    errlist.AddRange(db.errlist);
                    success = -3;
                }

            }
            return success;

		}




	}
}