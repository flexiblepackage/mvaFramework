using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.DataVisualization.Charting.Utilities;
using System.IO;

using MVAFW.Analysis;

namespace MVAQCDVT
{
    public partial class DataDisplay : Form
    {
        private double[][] datas;
        private double[][] scaledDatas;
        private int[] channels;
        private string[] cycleX;            //used to distinguish if it is the cycle analysis.

        public DataDisplay(double[][] datas, double[][] scaledDatas, int[] channels)
        {
            InitializeComponent();

            this.datas = datas;
            this.scaledDatas = scaledDatas;
            this.channels = channels;            
        }

        public DataDisplay(double[][] datas, string[] cycleX, int[] channels)
        {
            InitializeComponent();

            this.datas = datas;
            this.cycleX = cycleX;
            this.channels = channels;
        }


        private void display()
        {
            plotGraph();
            if (channels.Length != 1)
            {
                plotMultipleGraph();
            }
            else if (channels.Length == 1) //only show one chart, because there is only one channel.
            {
                this.splitContainer1.Panel2Collapsed = true;
            }
        }

        private void plotMultipleGraph()
        {
            flp_chart.Controls.Clear();
            for (int channel = 0; channel < channels.Length; channel++)
            {
                double average, max, min, rms, std, p2p;
                int nofc = 0;

                
                Chart chart = new Chart();
                chart.Size = new System.Drawing.Size(700, 300);
                chart.Palette = ChartColorPalette.Pastel;
                
                ChartArea chartArea1 = new ChartArea();
                Legend legend1 = new Legend();

                if (cycleX == null)
                {
                    analysis.getDCAnalysis(datas[channel], scaledDatas[channel], out average, out min, out max, out rms, out std, out p2p, out nofc);
                    legend1.Title = "Average:" + average.ToString("F4") + "\n" +
                                         "Max:" + max.ToString("F4") + "\n" +
                                         "Min:" + min.ToString("F4") + "\n" +
                                         "STD" + std.ToString("F4") + "\n" +
                                         "RMS" + rms.ToString("F4") + "\n";
                    legend1.TitleAlignment = StringAlignment.Near;
                    legend1.TitleFont = new System.Drawing.Font("Verdana", 9);

                    legend1.TitleSeparator = LegendSeparatorStyle.GradientLine;
                }
                chartArea1.CursorY.IsUserEnabled = true;
                chartArea1.CursorY.IsUserSelectionEnabled = true;
                chart.ChartAreas.Add(chartArea1);
                chart.Legends.Add(legend1);
                chart.Palette = ChartColorPalette.Pastel;
                chart.Series.Add("CH" + channels[channel].ToString());
                chart.Series["CH" + channels[channel].ToString()].ChartType = SeriesChartType.Line;
                if (rb_rawdata.Checked == true)
                {
                    if (cycleX == null)
                    {
                        chart.Series[0].Points.DataBindY(datas[channel]);
                    }
                    else
                    {
                        chart.Series[0].Points.DataBindXY(cycleX, datas[channel]);
                    }
                }
                else if (rb_scaledData.Checked == true)
                {
                    chart.Series[0].Points.DataBindY(scaledDatas[channel]);
                }
                chart.Tag = channel;
                flp_chart.Controls.Add(chart);
               
                if (cycleX == null)
                {
                    Chart histoChart = new Chart();
                    histoChart.Size = new System.Drawing.Size(300, 300);

                    ChartArea chartArea2 = new ChartArea();
                    histoChart.ChartAreas.Add(chartArea2);
                    Legend legend2 = new Legend();
                    legend2.Title = "NOFC:" + nofc.ToString() + "\n";
                    legend2.TitleAlignment = StringAlignment.Near;
                    legend2.TitleFont = new System.Drawing.Font("Verdana", 9);
                    histoChart.Legends.Add(legend2);

                    try
                    {
                        histoChart.Series.Add("HistoData");

                        double[] histoDatas = new double[datas[channel].Length];
                        for (int i = 0; i < histoDatas.Length; i++)
                        {
                            histoDatas[i] = (double)(short)datas[channel][i];
                        }
                        histoChart.Series["HistoData"].Points.DataBindY(histoDatas);

                        HistogramChartHelper Hist_Chart = new HistogramChartHelper();
                        Hist_Chart.CreateHistogram(histoChart, "HistoData", "CH" + channels[channel].ToString());
                        flp_chart.Controls.Add(histoChart);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }

        private void plotGraph()
        {
            ch_data.Series.Clear();
            double average, max, min, rms, std, p2p;
            int nofc;

            if (channels.Length != 1 || cycleX!=null)
            {
                this.splitContainer2.Panel2.Controls.Remove((Control)ch_histogram);
                this.ch_data.Dock = DockStyle.Fill;
            }
            for (int channel = 0; channel < channels.Length; channel++)
            {
                
                
                ch_data.Series.Add("CH" + channels[channel].ToString());
                ch_data.Series["CH" + channels[channel].ToString()].ChartType = SeriesChartType.Line;

                if (cycleX==null && channels.Length == 1)
                {
                    analysis.getDCAnalysis(datas[channel], scaledDatas[channel], out average, out min, out max, out rms, out std, out p2p, out nofc);

                    try
                    {
                        double[] histoDatas = new double[datas[channel].Length];
                        for (int i = 0; i < histoDatas.Length; i++)
                        {
                            histoDatas[i] = (double)(short)datas[channel][i];
                        }
                        ch_histogram.Series["histoData"].Points.DataBindY(histoDatas);
                        ch_histogram.Legends["HistoLegend"].Title = "NOFC:" + nofc.ToString() + "\n";
                        HistogramChartHelper Hist_Chart = new HistogramChartHelper();
                        Hist_Chart.CreateHistogram(ch_histogram, "histoData", "CH"+channel.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }

            for (int channel = 0; channel < datas.Length; channel++)
            {
                if (rb_rawdata.Checked == true)
                {
                    if (cycleX == null)
                    {
                        ch_data.Series[channel].Points.DataBindY(datas[channel]);
                    }
                    else
                    {
                        ch_data.Series[channel].Points.DataBindXY(cycleX, datas[channel]);
                    }
                }
                else if (rb_scaledData.Checked == true)
                {
                    ch_data.Series[channel].Points.DataBindY(scaledDatas[channel]);
                }

                if (cycleX == null && datas.Length == 1)
                {
                    analysis.getDCAnalysis(datas[channel], scaledDatas[channel], out average, out min, out max, out rms, out std, out p2p, out nofc);
                    ch_data.Legends["Legend1"].Title = "Average:" + average.ToString("F4") + "\n" +
                                         "Max:" + max.ToString("F4") + "\n" +
                                         "Min:" + min.ToString("F4") + "\n" +
                                         "P2P:" + p2p.ToString("F4") + "\n" +
                                         "STD:" + std.ToString("F4") + "\n" +
                                         "RMS:" + rms.ToString("F4") + "\n";
                    ch_data.Legends["Legend1"].TitleAlignment = StringAlignment.Near;
                    ch_data.Legends["Legend1"].TitleFont = new System.Drawing.Font("Verdana", 9);

                    ch_data.Legends["Legend1"].TitleSeparator = LegendSeparatorStyle.GradientLine;
                }
            }
        }

        private void rb_rawdata_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_scaledData.Checked == true && scaledDatas == null)
            {
                return;
            }
            display();
        }

        private void rb_scaledData_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_scaledData.Checked == true && scaledDatas == null)
            {
                return;
            }
            display();
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            if (sfd_export.ShowDialog() == DialogResult.OK)
            {                
                string filePath = sfd_export.FileName;

                if (rb_rawdata.Checked == true)
                {
                    writeToFile(filePath, datas, chk_header.Checked == true ? true : false);
                }
                else if (rb_scaledData.Checked == true)
                {
                    if (cycleX != null)
                    {
                        writeToFile(filePath, datas, chk_header.Checked == true ? true : false);
                    }
                    else
                    {
                        writeToFile(filePath, scaledDatas, chk_header.Checked == true ? true : false);
                    }
                }
            }
        }

        private void writeToFile(string filePath, double[][] datas, bool chHeader)
        {
            int dataNumber = datas[0].Length;
            StreamWriter sw = new StreamWriter(filePath);

            //header
            if (chHeader == true)
            {
                for (int channel = 0; channel < datas.Length; channel++)
                {
                    sw.Write("CH" + channels[channel] + ",");
                }
                sw.Write(Environment.NewLine);
            }

            for (int dpIndex = 0; dpIndex < dataNumber; dpIndex++)
            {
                for (int channel = 0; channel < datas.Length; channel++)
                {
                    sw.Write(datas[channel][dpIndex] + ",");
                }
                if (cycleX != null)
                {
                    sw.Write(cycleX[dpIndex]);
                }

                sw.Write(Environment.NewLine);
            }

            sw.Close();

            MessageBox.Show("Export done!");
        }
    }
}
