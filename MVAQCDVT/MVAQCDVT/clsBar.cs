using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace MVAFW
{

    /// <summary>
    /// Draws a bar in a DatGridView cell.
    /// </summary>
    /// <remarks></remarks>
    public class Bar
    {

        /// <summary>
        /// Draws a glass bar into a cell
        /// </summary>
        /// <param name="oColor">Color of the bar.</param>
        /// <param name="e">CellPainting event's parameters</param>
        /// <remarks></remarks>
        public static void PintaDegradado(System.Drawing.Color oColor, DataGridViewCellPaintingEventArgs e)
        {
            Bar.PintaDegradado(oColor, e, -1);
        }

        /// <summary>
        /// Draws a partial glass bar into a cell
        /// </summary>
        /// <param name="oColor">Color of the bar.</param>
        /// <param name="e">CellPainting event's parameters</param>
        /// <param name="iPorcentaje">Percent value to represent.</param>
        /// <remarks></remarks>
        public static void PintaDegradado(System.Drawing.Color oColor, DataGridViewCellPaintingEventArgs e, int iPorcentaje)
        {
            System.Drawing.Color[] aCol = { oColor };
            int[] aPor = { iPorcentaje };
            if (iPorcentaje == -1)
            {
                int[] aPorN = {
				
			};
                Bar.PintaDegradado(aCol, e, aPorN);
            }
            else
            {
                Bar.PintaDegradado(aCol, e, aPor);
            }
        }

        /// <summary>
        /// Draws a partial glass bar into a cell with target indicator.
        /// </summary>
        /// <param name="oColor">Color of the bar.</param>
        /// <param name="e">CellPainting event's parameters</param>
        /// <param name="iPorcentaje">Percent value to represent.</param>
        /// <param name="iObjetivo">Target to show.</param>
        /// <param name="oColorObjetivo">Target indicator color.</param>
        /// <remarks></remarks>
        public static void PintaDegradado(System.Drawing.Color oColor, DataGridViewCellPaintingEventArgs e, int iPorcentaje, int iObjetivo, System.Drawing.Color oColorObjetivo)
        {
            System.Drawing.Color[] aCol = {
			oColor,
			oColorObjetivo
		};
            int[] aPor = {
			iPorcentaje,
			iObjetivo
		};
            Bar.PintaDegradado(aCol, e, aPor);
        }

        /// <summary>
        /// Does the hard job
        /// </summary>
        /// <param name="aColores">Color array to use.</param>
        /// <param name="e">CellPainting event's parameters</param>
        /// <param name="aPorcentajes">Percent array to show. It can be one or two. 
        /// First shows bar percent and second shows target percent. 
        /// If only one percent is passed and it's cero, it will fill all cell width with the glass bar.</param>
        /// <remarks></remarks>

        private static void PintaDegradado(System.Drawing.Color[] aColores, DataGridViewCellPaintingEventArgs e, int[] aPorcentajes)
        {
            //   Declares brushes and color array
            System.Drawing.Drawing2D.LinearGradientBrush oPin1 = null;
            System.Drawing.Drawing2D.LinearGradientBrush oPin2 = null;
            System.Drawing.Drawing2D.LinearGradientBrush oPinO = null;
            System.Drawing.Color oColor = aColores[0];

            //   Handles any exception
            try
            {
                //   Gets cell rectangle
                Rectangle oCelda = new Rectangle(e.CellBounds.X - 1, e.CellBounds.Y - 1, e.CellBounds.Width, e.CellBounds.Height);

                //   Debugs percent array
                for (int iC = 0; iC <= aPorcentajes.Length - 1; iC++)
                {
                    if (aPorcentajes[iC] > 100)
                        aPorcentajes[iC] = 100;
                }

                //   Declares rectangles.
                Rectangle oRect1 = default(Rectangle);
                // Glass upper rect.
                Rectangle oRect2 = default(Rectangle);
                // Glass lower rect.
                Rectangle oObj = default(Rectangle);
                // Target rect.
                Rectangle oFond = default(Rectangle);
                // Background rect.
                Rectangle oCuad = default(Rectangle);
                // Border rect.
                //   Declares percent variable
                int iPorcentaje = 0;
                bool bPor = false;

                //   Detects if percent(s) is/are passed
                //   One percent indicates only value percent.
                //   Two percents indicate value and target percent.
                if (aPorcentajes.Length > 0)
                {
                    bPor = true;
                    iPorcentaje = aPorcentajes[0];


                    if (iPorcentaje > 0)
                    {
                        //   Gets rectangles and brushes for percent indicator.
                        oRect1 = new Rectangle(oCelda.X + 4, oCelda.Y + 4,(int) Math.Round(((oCelda.Width - 7) * iPorcentaje * 0.01) + 0.49),(int)Math.Round((decimal)(oCelda.Height - 8) / 2));
                        if (oRect1.Width > oCelda.Width - 7)
                            oRect1.Width = oCelda.Width - 7;
                        oRect2 = new Rectangle(oCelda.X + 4, oRect1.Bottom - 1, oRect1.Width, (oCelda.Height - 6) - oRect1.Height);
                        oFond = new Rectangle(oCelda.X + 4, oCelda.Y + 4, oCelda.Width - 7, oCelda.Height - 7);
                        oPin1 = new System.Drawing.Drawing2D.LinearGradientBrush(oRect1, Color.White, Color.FromArgb(180, oColor), System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                        oPin2 = new System.Drawing.Drawing2D.LinearGradientBrush(oRect2, oColor, Color.FromArgb(70, oColor), System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                    }


                    if (aPorcentajes.Length > 1)
                    {
                        //   Get rectangles and brushes for target indicator
                        int iObj = aPorcentajes[1];
                        int iPos =(int)(oCelda.X + 4 + Math.Round(((oCelda.Width - 7) * iObj * 0.01) + 0.49));
                        int iIni = iPos - 20;
                        if (iIni < oCelda.X + 4)
                            iIni = oCelda.X + 4;
                        oObj = new Rectangle(iIni, oCelda.Y + 2, iPos - iIni, oCelda.Height - 4);
                        oPinO = new System.Drawing.Drawing2D.LinearGradientBrush(oObj, System.Drawing.Color.FromArgb(0, aColores[1]), aColores[1], System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
                    }

                    //   Get border rectangle
                    oCuad = new Rectangle(oCelda.X + 3, oCelda.Y + 3, oCelda.Width - 6, oCelda.Height - 6);

                }
                else
                {
                    //   Gets rectangles and brushes to fill cell (no percents)
                    oRect1 = new Rectangle(oCelda.X + 1, oCelda.Y + 1, oCelda.Width - 1,(int)Math.Round((decimal)oCelda.Height / 2));
                    oRect2 = new Rectangle(oCelda.X + 1, oRect1.Bottom - 1, oCelda.Width - 1, oCelda.Height - oRect1.Height);
                    oFond = new Rectangle(oCelda.X + 1, oCelda.Y + 1, oCelda.Width - 1, oCelda.Height);
                    oPin1 = new System.Drawing.Drawing2D.LinearGradientBrush(oRect1, Color.White, Color.FromArgb(180, oColor), System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                    oPin2 = new System.Drawing.Drawing2D.LinearGradientBrush(oRect2, oColor, Color.FromArgb(70, oColor), System.Drawing.Drawing2D.LinearGradientMode.Vertical);

                }

                //   Paints cell background
                e.PaintBackground(e.CellBounds, true);
                if (bPor)
                {
                    e.Graphics.DrawRectangle(Pens.DimGray, oCuad);
                }

                //   Paints glass bar
                if (oPin1 != null)
                {
                    e.Graphics.FillRectangle(Brushes.White, oFond);
                    e.Graphics.FillRectangle(oPin1, oRect1);
                    e.Graphics.FillRectangle(oPin2, oRect2);
                }

                //   Paints target indicator
                if (oPinO != null)
                {
                    e.Graphics.FillRectangle(oPinO, oObj);
                }

                //   Paints cell contents
                e.PaintContent(oCelda);
                e.Paint(oCelda, DataGridViewPaintParts.Border);
                e.Handled = true;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);

            }
            finally
            {
                //   Dispose all resources
                if (oPin1 != null)
                {
                    oPin1.Dispose();
                    oPin1 = null;
                }
                if (oPin2 != null)
                {
                    oPin2.Dispose();
                    oPin2 = null;
                }
                if (oPinO != null)
                {
                    oPinO.Dispose();
                    oPinO = null;
                }
            }
        }

    }
}
