// ViewModels/ManagementVM.cs
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Data.SqlClient;
using System.Windows;
using SmartFactory.Helpers;
using SmartFactory.Models;

namespace SmartFactory.ViewModels
{
    public class ManagementVM
    {
        public int Plastic { get; private set; }
        public int Paper { get; private set; }
        public int Can { get; private set; }
        public int PlasticToday { get; private set; }
        public int PaperToday { get; private set; }
        public int CanToday { get; private set; }

        public ISeries[] PieSeries { get; private set; }
        public ISeries[] ColumnSeries { get; private set; }
        public Axis[] XAxes { get; private set; }

        public ManagementVM()
        {
            PieSeries = new ISeries[]
            {
                new PieSeries<double> { Values = new double[] { 0 } },
                new PieSeries<double> { Values = new double[] { 0 } },
                new PieSeries<double> { Values = new double[] { 0 } }
            };

            ColumnSeries = new ISeries[]
            {
                new ColumnSeries<double> { Name = "Category", Values = new double[] { 0 } },
            };

            XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = new string[] { "" },
                    LabelsRotation = 0,
                    LabelsPaint = new SolidColorPaint(SKColors.White),
                    SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                    SeparatorsAtCenter = false,
                    TicksPaint = new SolidColorPaint(new SKColor(255, 255, 255)),
                    TicksAtCenter = true,
                    ForceStepToMin = true,
                    MinStep = 1
                }
            };

        }

        public void get_data_graph()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.CONNSTRING))
                {
                    conn.Open();

                    using (var Pcount = new SqlCommand(Result.RESULT_SELECT_PLASTIC_SUM, conn))
                    {
                        var P_result = Pcount.ExecuteScalar();
                        Plastic = P_result != null ? Convert.ToInt32(P_result) : 0;
                    }

                    using (var Rcount = new SqlCommand(Result.RESULT_SELECT_PAPER_SUM, conn))
                    {
                        var R_result = Rcount.ExecuteScalar();
                        Paper = R_result != null ? Convert.ToInt32(R_result) : 0;
                    }

                    using (var Ccount = new SqlCommand(Result.RESULT_SELECT_CAN_SUM, conn))
                    {
                        var C_result = Ccount.ExecuteScalar();
                        Can = C_result != null ? Convert.ToInt32(C_result) : 0;
                    }

                    using (var Pcount = new SqlCommand(Result.RESULT_SELECT_PLASTIC, conn))
                    {
                        var P_result = Pcount.ExecuteScalar();
                        PlasticToday = P_result != null ? Convert.ToInt32(P_result) : 0;
                    }

                    using (var Rcount = new SqlCommand(Result.RESULT_SELECT_PAPER, conn))
                    {
                        var R_result = Rcount.ExecuteScalar();
                        PaperToday = R_result != null ? Convert.ToInt32(R_result) : 0;
                    }

                    using (var Ccount = new SqlCommand(Result.RESULT_SELECT_CAN, conn))
                    {
                        var C_result = Ccount.ExecuteScalar();
                        CanToday = C_result != null ? Convert.ToInt32(C_result) : 0;
                    }

                    // PieSeries 업데이트
                    PieSeries = new ISeries[]
                    {
                        new PieSeries<double>
                        {
                            Name = "PLASTIC",
                            Values = new double[] { Plastic != 0 ? Plastic : double.NaN },
                            Fill = new SolidColorPaint(SKColor.Parse("#D0E1E9"))
                        },
                        new PieSeries<double>
                        {
                            Name = "PAPER",
                            Values = new double[] { Paper != 0 ? Paper : double.NaN },
                            Fill = new SolidColorPaint(SKColor.Parse("#A1C2D5"))
                        },
                        new PieSeries<double>
                        {
                            Name = "CAN",
                            Values = new double[] { Can != 0 ? Can : double.NaN },
                            Fill = new SolidColorPaint(SKColor.Parse("#5F7290"))
                        }
                    };

                    // ColumnSeries 업데이트
                    ColumnSeries = new ISeries[]
                    {
                        new ColumnSeries<double>
                        {
                            Name = "Plastic",
                            
                            Values = new double[] { PlasticToday },
                            Fill = new SolidColorPaint(SKColor.Parse("#D0E1E9"))
                        },
                        new ColumnSeries<double>
                        {
                            Name = "Paper",
                            Values = new double[] { PaperToday },
                            Fill = new SolidColorPaint(SKColor.Parse("#A1C2D5"))
                        },
                        new ColumnSeries<double>
                        {
                            Name = "Can",
                            Values = new double[] { CanToday },
                            Fill = new SolidColorPaint(SKColor.Parse("#5F7290"))
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터 조회 중 오류가 발생했습니다: " + ex.Message);
            }
        }
    }
}
