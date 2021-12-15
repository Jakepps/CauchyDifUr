using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;//подключаем ZedGraph

namespace aaaaaaжесть
{
    public partial class Form1 : Form
    {
        public static int n, xP = 0, yP = 0;
        public static double Step;
        public string ME = "Метод Эйлера.";
        public string MRK2 = "Рунге-Кутта 2 порядка.";
        public string MRK4 = "Рунге-Кутта 4 порадка.";
        ZedGraphControl zedGrapgControl1 = new ZedGraphControl();
        public Form1()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            zedGrapgControl1.Location = new Point(10, 30);
            zedGrapgControl1.Name = "text";
            zedGrapgControl1.Size = new Size(500, 500);
            Controls.Add(zedGrapgControl1);
            GraphPane my_Pane = zedGrapgControl1.GraphPane;
            my_Pane.Title.Text = "Результат";
            my_Pane.XAxis.Title.Text = "X";
            my_Pane.YAxis.Title.Text = "Y";

        }
        private void GetSize()
        {
            zedGrapgControl1.Location = new Point(10, 10);
            zedGrapgControl1.Size = new Size(ClientRectangle.Width - 20, ClientRectangle.Height - 20);

        }
        protected override void OnSizeChanged(EventArgs e)
        {
            GetSize();
        }
        static double f1(double x, double y)
        {
            return x * x - 2 * y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear(zedGrapgControl1);
        }
        private void Clear(ZedGraphControl Zed_GraphControl)
        {
            //GraphPane pane = Zed_GraphControl.GraphPane;
            zedGrapgControl1.GraphPane.CurveList.Clear();
            zedGrapgControl1.GraphPane.GraphObjList.Clear();

            zedGrapgControl1.GraphPane.XAxis.Type = AxisType.Linear;
            zedGrapgControl1.GraphPane.XAxis.Scale.TextLabels = null;
            zedGrapgControl1.GraphPane.XAxis.MajorGrid.IsVisible = false;
            zedGrapgControl1.GraphPane.YAxis.MajorGrid.IsVisible = false;
            zedGrapgControl1.GraphPane.YAxis.MinorGrid.IsVisible = false;
            zedGrapgControl1.GraphPane.XAxis.MinorGrid.IsVisible = false;
            zedGrapgControl1.RestoreScale(zedGrapgControl1.GraphPane);

            zedGrapgControl1.AxisChange();
            zedGrapgControl1.Invalidate();
        }

        private void Eiler(ZedGraphControl Zed_GraphControl)
        {
            GraphPane my_Pane = Zed_GraphControl.GraphPane;

            PointPairList list = new PointPairList();

            
                double h = 0.1, x = xP, y = yP;

                for (int i = 0; i < 1000; i++)
                {

                    list.Add(x, y);
                    y += h * f1(x, y); //делаем шаг
                    x += h;
                }


                LineItem d1 = my_Pane.AddCurve("Функция", list, Color.Red, SymbolType.Circle);


                zedGrapgControl1.AxisChange();
                zedGrapgControl1.Invalidate();
            }
 


        private void RungeKutto2(ZedGraphControl Zed_GraphControl)
        {
            GraphPane my_Pane = Zed_GraphControl.GraphPane;

            PointPairList list = new PointPairList();


            double h = 0.1, x = xP, y = yP;


            for (int i = 0; i < 1000; i++)
            {
                list.Add(x, y);
                double d = f1(x, y);
                y = y + (h / 2) * (d + f1(x + h, y + h * d)); //делаем шаг
                x += h;
            }
        

              LineItem myCircle = my_Pane.AddCurve(MRK2, list, Color.Yellow, SymbolType.Circle);
                    zedGrapgControl1.AxisChange();
                    zedGrapgControl1.Invalidate();
            
        }       
           
        private void RungeKutto4(ZedGraphControl Zed_GraphControl)
{
    GraphPane my_Pane = Zed_GraphControl.GraphPane;
    PointPairList list = new PointPairList();
    double h = 0.1, x = xP, y = yP, k1, k2, k3, k4;


    for (int i = 0; i < 1000; i++)
    {
        list.Add(x, y);
        k1 = f1(x, y);
        k2 = f1(x + h / 2, y + (h * k1) / 2);
        k3 = f1(x + h / 2, y + (h * k2) / 2);
        k4 = f1(x + h, y + h * k3);
        y = y + (h / 6) * (k1 + 2 * k2 + 2 * k3 + k4); //делаем шаг
        x += h;
    }

    
        LineItem myCircle = my_Pane.AddCurve(MRK4, list, Color.Green, SymbolType.Circle);
                    zedGrapgControl1.AxisChange();
                    zedGrapgControl1.Invalidate();
    
   
    }
           
        


        private void GriddenOn(GraphPane my_Pane)
        {
            //GraphPane my_Pane = Zed_GraphControl.GraphPane;



            my_Pane.XAxis.MajorGrid.IsVisible = true;
            my_Pane.YAxis.MajorGrid.IsVisible = true;
            my_Pane.YAxis.MinorGrid.IsVisible = true;
            my_Pane.XAxis.MinorGrid.IsVisible = true;
        }


        private void BTN_EM(object sender, EventArgs e)
        {
           
            GriddenOn(zedGrapgControl1.GraphPane);
            Eiler(zedGrapgControl1);

        }

        private void Scale(GraphPane pane)
        {
            //GraphPane pane = Zed_GraphControl.GraphPane;
            // Установим масштаб по умолчанию для оси X
            pane.XAxis.Scale.MinAuto = true;
            pane.XAxis.Scale.MaxAuto = true;

            // Установим масштаб по умолчанию для оси Y
            pane.YAxis.Scale.MinAuto = true;
            pane.YAxis.Scale.MaxAuto = true;

            zedGrapgControl1.AxisChange();
            zedGrapgControl1.Invalidate();
        }
        private void BTN_Scale(object sender, EventArgs e)
        {
            Scale(zedGrapgControl1.GraphPane);
        }

        private void BTN_RK4(object sender, EventArgs e)
        {
            GriddenOn(zedGrapgControl1.GraphPane);

            RungeKutto4(zedGrapgControl1);
        }

        private void BTN_RK2(object sender, EventArgs e)
        {
            GriddenOn(zedGrapgControl1.GraphPane);

            RungeKutto2(zedGrapgControl1);
        }

    }
}


