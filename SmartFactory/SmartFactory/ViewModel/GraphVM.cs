// ViewModels/GraphVM.cs
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace SmartFactory.ViewModels
{
    public class GraphVM
    {
        public int Plastic { get; private set; }
        public int Paper { get; private set; }
        public int Can { get; private set; }

        public ISeries[] PieSeries { get; private set; }
        public ISeries[] ColumnSeries { get; private set; }
        public Axis[] XAxes { get; private set; }

        public GraphVM()
        {
            PieSeries = new ISeries[]
            {
                new PieSeries<double> { Values = new double[] { 0 } },
                new PieSeries<double> { Values = new double[] { 0 } },
                new PieSeries<double> { Values = new double[] { 0 } }
            };

            ColumnSeries = new ISeries[]
            {
                new ColumnSeries<double> { Name = "Category 1", Values = new double[] { 0 } },
            };

            XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = new string[] { "" },
                    LabelsRotation = 0,
                    SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                    TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35))
                }
            };
        }

        public void get_data_graph()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
                {
                    conn.Open();

                    using (var Pcount = new SqlCommand(Models.Result.RESULT_SELECT_PLASTIC, conn))
                    {
                        var P_result = Pcount.ExecuteScalar();
                        Plastic = P_result != null ? Convert.ToInt32(P_result) : 0;
                    }

                    using (var Rcount = new SqlCommand(Models.Result.RESULT_SELECT_PAPER, conn))
                    {
                        var R_result = Rcount.ExecuteScalar();
                        Paper = R_result != null ? Convert.ToInt32(R_result) : 0;
                    }

                    using (var Ccount = new SqlCommand(Models.Result.RESULT_SELECT_CAN, conn))
                    {
                        var C_result = Ccount.ExecuteScalar();
                        Can = C_result != null ? Convert.ToInt32(C_result) : 0;
                    }

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

                    ColumnSeries = new ISeries[]
                    {
                        new ColumnSeries<double>
                        {
                            Name = "Plastic",
                            Values = new double[] { Plastic },
                            Fill = new SolidColorPaint(SKColor.Parse("#D0E1E9"))
                        },
                        new ColumnSeries<double>
                        {
                            Name = "Paper",
                            Values = new double[] { Paper },
                            Fill = new SolidColorPaint(SKColor.Parse("#A1C2D5"))
                        },
                        new ColumnSeries<double>
                        {
                            Name = "Can",
                            Values = new double[] { Can },
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
