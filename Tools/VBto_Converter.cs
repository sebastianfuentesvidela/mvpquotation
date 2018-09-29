using System;
//using System.Windows.Forms;
//using System.Drawing;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
//using Microsoft.VisualBasic.Compatibility.VB6;

namespace Tools
{
	/// <summary>
	/// This is a part of the VBto Converter (www.vbto.net). Copyright (C) 2005-2016 StressSoft Company Ltd. All rights reserved.
	/// </summary>
	public class VBtoConverter
	{
        //public static string vbFormat(object Expression, string Style)
        //{
        //    return null;// Support.Format(Expression, Style, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
        //}

		public static double ObjectToDouble(object v)
		{
			double d;

			if (v is DateTime)
			{
				DateTime dt = (DateTime)v;
				d = dt.ToOADate();
				return d;
			}

			if (v is string)
			{
				string s = (string)v;
				d = Conversion.Val(s);
				return d;
			}

			d = Convert.ToDouble(v);
			return d;
		}

        //public static string ObjectToString(object v)
        //{
        //    if (v == null) return "";
        //    if (v is string) return (string)v;
        //    if (v is Color)
        //    {
        //        int iCol = ColorTranslator.ToOle((Color)v);
        //        return iCol.ToString();
        //    }

        //    string s = v.ToString();
        //    return s;
        //}

		public static bool CBool(string s)
		{
			if (s.Length<=0) return false;
			char ch = s[0];
			if (ch=='T' || ch=='t') return true;
			if (ch=='F' || ch=='f') return false;
			return Microsoft.VisualBasic.Conversion.Val(s)!=0;
		}
		public static bool CBool(int d)
		{
			return d!=0;
		}
		public static bool CBool(double d)
		{
			return d!=0;
		}
		public static bool CBool(Object d)
		{
			return (bool)d;
		}

		public static int Int(Decimal dc)
		{
			return (int)Math.Floor(dc);
		}
		public static int Int(double dValue)
		{
			return (int)Math.Floor(dValue);
		}
		public static int Int(string sValue)
		{
			//double d = Convert.ToDouble(sValue);
			double d = double.Parse(sValue);
			return Int(d);
		}


        //public static void PerformClick(Button btn, bool bValue)
        //{
        //    if (bValue) btn.PerformClick();
        //}

        //public static string GetVB6Name(Control Ctrl)
        //{
        //    int lineIdx = Ctrl.Name.LastIndexOf("_");
        //    if (lineIdx == -1) return Ctrl.Name;
        //    return Ctrl.Name.Substring(0, lineIdx);
        //}

        //public static Font SetFontStyle(Font font, FontStyle fs, bool flAdd)
        //{
        //    return new Font(font, flAdd ? (font.Style | fs) : (font.Style & ~fs));
        //}


		public static IntPtr GetByteFromString(String Buf)
		{
			IntPtr pBuf = Marshal.StringToHGlobalAnsi(Buf);
			return pBuf;
		}
		public static void GetStringFromByte(ref String Buf, IntPtr pBuf)
		{
			Buf = Marshal.PtrToStringAnsi(pBuf);
		}

		public static Boolean IsNull(Object Obj)
		{
			return (Obj == null || Obj == DBNull.Value) ? true : false;
		}

		public static int LenB(object obj)
		{
			if (obj is string) return (obj as string).Length * 2;
			if (obj is int || obj is Int32) return 4;
			if (obj is double) return 8;
			if (obj is Int16) return 2;
			if (obj is bool) return 2;
			return 0;
		}

		public static bool IsEmpty(object obj)
		{
			return false;	//? obj == null;
		}


        //public static System.Windows.Forms.Cursor ScreenCursor
        //{
        //    get { return Application.OpenForms[0].Cursor; }
        //    set { Application.OpenForms[0].Cursor = value; }
        //}

        //public static string GetAppTitle()
        //{
        //    return Application.OpenForms[0].Text;
        //}

        //static public void ShowAsMdiChild(Form pMdiChild, Form pMdiParent)
        //{
        //    pMdiChild.MdiParent = pMdiParent;
        //    pMdiChild.Show();
        //}
        //static Form FindMDIContainer()
        //{
        //    foreach (Form f in Application.OpenForms)
        //        if (f.IsMdiContainer) return f;
        //    return null;
        //}
        //static public void ShowAsMdiChild(Form pMdiChild)
        //{
        //    Form pMdiParent = FindMDIContainer();
        //    if (pMdiParent != null) ShowAsMdiChild(pMdiChild, pMdiParent);
        //}

        //public static void ShowPopupMenu(MenuItem menuItem, Control control, Point point)
        //{
        //    ContextMenu newPopup = new ContextMenu();
        //    foreach (MenuItem Menu in menuItem.MenuItems)
        //    {
        //        newPopup.MenuItems.Add(Menu.CloneMenu());
        //    }
        //    newPopup.Show(control, point);
        //}

		public static void ArrayResize<T>(ref T[,] Arr, int Level1, int Level2)
		{
			T[,] result = new T[Level1, Level2];

			if (Arr == null) { Arr = result; return; }

			int PrevLevel1 = Arr.GetLength(0);
			int PrevLevel2 = Arr.GetLength(1);
			for (int i = 0; i < Level1; i++)
			{
				for (int j = 0; j < Level2; j++)
				{
					bool bInside = i < PrevLevel1 && j < PrevLevel2;
					result[i, j] = bInside ? Arr[i, j] : default(T);
				}
			}
			Arr = result;
		}


		// === External Constants: ===
		public const int cdlHelpPartialKey = 261;
		public const int cdlHelpContents = 3;
		public const int cdlPDPrintSetup = 64;
		public const int vbPopupMenuRightButton = 2;


        //public static CheckState CheckState_ByValue(int valueVB6)
        //{
        //    switch (valueVB6)
        //    {
        //        case 0: return CheckState.Unchecked;
        //        case 1: return CheckState.Checked;
        //        case 2: return CheckState.Indeterminate;
        //        //? default: return (CheckState)valueVB6;
        //        default: return CheckState.Unchecked;	// - ???
        //    }
        //}

		public static CompareMethod CompareMethod_ByValue(int valueVB6)
		{
			switch (valueVB6)
			{
				case 0: return CompareMethod.Binary;
				case 1: return CompareMethod.Text;
				case 2: return CompareMethod.Text/*?vbDatabaseCompare*/;
				//? default: return (CompareMethod)valueVB6;
				default: return CompareMethod.Binary;	// - ???
			}
		}
	}
}
