using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WinForm_GeoJson2Map_Sample.Properties;

//詳しくはhttps://qiita.com/Ichihai1415/items/8447eb43b3e7c9be4e1a
namespace WinForm_GeoJson2Map_Sample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MapImg.MouseWheel += new MouseEventHandler(MapImg_MouseWheel);//←これを追加
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            MapImg.Size = ClientSize;
            DrawMap();
        }

        private void DrawMapB_Click(object sender, EventArgs e)
        {
            DrawMap();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            json = JObject.Parse(Resources.JsonText);//Parse()の中はFile.ReadAllTextとかResource.とかGeoJSONのstring
            DrawMap();
        }

        JObject json;//読み込みとパースを1回だけに
        Bitmap canvas;//後で別のvoidでさらに描画するとき用(同じvoidでさらに描画するならvoid内でもok)
        double LatSta = 25;//緯度の始点(=地図の南端)
        double LatEnd = 50;//緯度の始点(=地図の北端)
        double LonSta = 125;//経度の始点(=地図の西端)
        double LonEnd = 150;//経度の始点(=地図の東端)
        double ZoomW = 0;//横のズーム倍率(画像横ピクセル÷表示する経度の範囲)
        double ZoomH = 0;//縦のズーム倍率(画像縦ピクセル÷表示する緯度の範囲)

        /// <summary>
        /// 地図を一気に全部描画します。
        /// </summary>
        private void DrawMap()
        {
            canvas = new Bitmap(MapImg.Width, MapImg.Height);//pictureBoxの幅と高さ
            ZoomW = canvas.Width / (LonEnd - LonSta);//経度の差×幅ズーム倍率=画像幅だから幅ズーム倍率=画像幅÷経度の差
            ZoomH = canvas.Height / (LatEnd - LatSta);//同じように高さズーム倍率=画像高さ÷緯度の差
            Graphics g = Graphics.FromImage(canvas);
            g.Clear(Color.FromArgb(120, 120, 255));//背景色
            GraphicsPath Maps = new GraphicsPath();
            Maps.StartFigure();
            foreach (JToken json_1 in json.SelectToken("features"))//各地域のデータにする処理
            {
                if ((string)json_1.SelectToken("geometry.type") == "Polygon")//地域が1つのPolygonでできている場合
                {
                    List<Point> points = new List<Point>();
                    foreach (JToken json_2 in json_1.SelectToken($"geometry.coordinates[0]"))
                        points.Add(new Point((int)(((double)json_2.SelectToken("[0]") - LonSta) * ZoomW), (int)((LatEnd - (double)json_2.SelectToken("[1]")) * ZoomH)));
                    if (points.Count > 2)
                        Maps.AddPolygon(points.ToArray());
                }
                else//地域が2つ以上のPolygonでできている場合(MultiPolygon)
                {
                    foreach (JToken json_2 in json_1.SelectToken($"geometry.coordinates"))
                    {
                        List<Point> points = new List<Point>();
                        foreach (JToken json_3 in json_2.SelectToken("[0]"))
                            points.Add(new Point((int)(((double)json_3.SelectToken("[0]") - LonSta) * ZoomW), (int)((LatEnd - (double)json_3.SelectToken("[1]")) * ZoomH)));
                        if (points.Count > 2)
                            Maps.AddPolygon(points.ToArray());
                    }
                }
            }
            g.FillPath(new SolidBrush(Color.FromArgb(150, 150, 150)), Maps);//RGB(150, 150, 150)で塗りつぶし
            g.DrawPath(new Pen(Color.FromArgb(255, 255, 255), 1), Maps);//RGB(255, 255, 255)で太さ1の線を描画(太さ1なら, 1はいらない)
            //太さ変えるならZoomに応じて変わるようにした方がいい(↓みたいに)軽さ優先なら1で
            //g.DrawPath(new Pen(Color.FromArgb(255, 255, 255), (float)Math.Log10(Zoom / 5)), Maps);
            g.Dispose();
            MapImg.Image = canvas;
        }

        /// <summary>
        /// 地図を地域ごとに描画します。
        /// </summary>
        private void DrawMap2()
        {
            canvas = new Bitmap(MapImg.Width, MapImg.Height);
            ZoomW = canvas.Width / (LonEnd - LonSta);
            ZoomH = canvas.Height / (LatEnd - LatSta);
            Graphics g = Graphics.FromImage(canvas);
            g.Clear(Color.FromArgb(120, 120, 255));
            //GraphicsPath Maps = new GraphicsPath();
            //Maps.StartFigure();
            foreach (JToken json_1 in json.SelectToken("features"))
            {
                GraphicsPath Maps = new GraphicsPath();//GraphicsPathを地域ごとに作り描画
                Maps.StartFigure();
                if ((string)json_1.SelectToken("geometry.type") == "Polygon")
                {
                    List<Point> points = new List<Point>();
                    foreach (JToken json_2 in json_1.SelectToken($"geometry.coordinates[0]"))
                        points.Add(new Point((int)(((double)json_2.SelectToken("[0]") - LonSta) * ZoomW), (int)((LatEnd - (double)json_2.SelectToken("[1]")) * ZoomH)));
                    if (points.Count > 2)
                        Maps.AddPolygon(points.ToArray());
                }
                else
                {
                    foreach (JToken json_2 in json_1.SelectToken($"geometry.coordinates"))
                    {
                        List<Point> points = new List<Point>();
                        foreach (JToken json_3 in json_2.SelectToken("[0]"))
                            points.Add(new Point((int)(((double)json_3.SelectToken("[0]") - LonSta) * ZoomW), (int)((LatEnd - (double)json_3.SelectToken("[1]")) * ZoomH)));
                        if (points.Count > 2)
                            Maps.AddPolygon(points.ToArray());
                    }
                }
                //↓はfeatures[].properties.nameに"福"が含まれていれば黄色にする例
                //データによってはfeatures.properties.nameじゃないことがあるので要確認
                if (((string)json_1.SelectToken("properties.name")).Contains("福"))
                    g.FillPath(new SolidBrush(Color.FromArgb(255, 255, 0)), Maps);
                else
                    g.FillPath(new SolidBrush(Color.FromArgb(150, 150, 150)), Maps);
                g.DrawPath(new Pen(Color.FromArgb(255, 255, 255), 1), Maps);
            }
            //g.FillPath(new SolidBrush(Color.FromArgb(150, 150, 150)), Maps);
            //g.DrawPath(new Pen(Color.FromArgb(255, 255, 255), 1), Maps);
            g.Dispose();
            MapImg.Image = canvas;
        }

        private void MapImg_MouseEnter(object sender, EventArgs e)
        {
            MapImg.Focus();
        }

        Point ClickPoint;//クリックしたところ

        private void MapImg_MouseDown(object sender, MouseEventArgs e)
        {
            ClickPoint = e.Location;
        }

        private void MapImg_MouseUp(object sender, MouseEventArgs e)
        {
            Point UpPoint = e.Location;
            int DiffX = UpPoint.X - ClickPoint.X;//右にやれば+
            int DiffY = UpPoint.Y - ClickPoint.Y;//下にやれば+
            LatSta += DiffY / ZoomH;//緯度の差×縦ズーム倍率=画像高さだから下に動かす緯度×縦ズーム倍率=下に動かした分よって下に動かす緯度(下に動かすと最低緯度が高くなるから+=)=下に動かした分÷縦ズーム倍率
            LatEnd += DiffY / ZoomH;//上端も同じように
            LonSta -= DiffX / ZoomW;//Latと同じように(右に動かすと最低経度が低くなるから+=)
            LonEnd -= DiffX / ZoomW;//右端も同じように
            DrawMap();
        }
        /*
        private void MapImg_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)//前に転がしたとき
            {
                if (LatEnd - LatSta > 2 && LonEnd - LonSta > 2)//0以下だと正常に表示できない ↓の変化量に合わせて調整
                {
                    LatSta += 1;//範囲を上下左右1度ずつ狭くする
                    LatEnd -= 1;//変化値が固定だと詳しいところまで拡大したとき微調整が難しくなる
                    LonSta += 1;//LatのほうではMath.Log10(LatEnd - LatSta + 1) 、LonのほうではMath.Log10(LonEnd - LonSta + 1) がおすすめ
                    LonEnd -= 1;//↑これだとy=log10(x+1)のグラフのように変化(y:狭くする量(度),x:緯度差・経度差)
                }
            }
            else//後ろに転がしたとき
            {
                if (LatEnd - LatSta < 30 && LonEnd - LonSta < 30)//縮小制限を作っとく
                {
                    LatSta -= 1;//範囲を上下左右1度ずつ広くする
                    LatEnd += 1;//変化の勢いは↑と同じ
                    LonSta -= 1;
                    LonEnd += 1;
                }
            }
            DrawMap();
        }*/

        Point MousePoint;//マウス座標
        private void MapImg_MouseMove(object sender, MouseEventArgs e)
        {
            MousePoint = e.Location;
        }

        private void MapImg_MouseWheel(object sender, MouseEventArgs e)
        {
            double UpPercent = (double)(MapImg.Height - MousePoint.Y) / MapImg.Height;//カーソルが上にある割合(上端で0、下端で1)
            double LeftPercent = (double)(MapImg.Width - MousePoint.X) / MapImg.Width;//左にある割合　(double)しないとintに丸められて0になる
            /*
            if (e.Delta > 0)
            {
                if (LatEnd - LatSta > 2 && LonEnd - LonSta > 2)
                {//           ↓×0.5が中心だから2に  ↓複雑なのでスルーしてok(間違えてるかも)
                    LatSta += 2 * (UpPercent);//上に近い=値が小さいほど下端は上がる
                    LatEnd -= 2 * (1 - UpPercent);//下にある割合
                    LonSta += 2 * (1 - LeftPercent);//右にある割合 右に近い=値が大きいほど右端は上がる
                    LonEnd -= 2 * (LeftPercent);//上と同じように2をMath.Log10(LatEnd - LatSta + 1)に変えてもok
                }
            }
            else
            {
                if (LatEnd - LatSta < 30 && LonEnd - LonSta < 30)
                {
                    LatSta -= 2 * (UpPercent);//↑と同じ
                    LatEnd += 2 * (1 - UpPercent);
                    LonSta -= 2 * (1 - LeftPercent);
                    LonEnd += 2 * (LeftPercent);
                }
            }
            */
            if (e.Delta > 0)//2の代わりに拡大率に応じて変化用が変わるやつに
            {
                if (LatEnd - LatSta > 2 && LonEnd - LonSta > 2)
                {//           ↓×0.5が中心だから2に  ↓複雑なのでスルーしてok(間違えてるかも)
                    LatSta += Math.Log10(LatEnd - LatSta + 1) * (UpPercent);//上に近い=値が小さいほど下端は上がる
                    LatEnd -= Math.Log10(LatEnd - LatSta + 1) * (1 - UpPercent);//下にある割合
                    LonSta += Math.Log10(LonEnd - LonSta + 1) * (1 - LeftPercent);//右にある割合 右に近い=値が大きいほど右端は上がる
                    LonEnd -= Math.Log10(LonEnd - LonSta + 1) * (LeftPercent);//上と同じように2をMath.Log10(LatEnd - LatSta + 1)に変えてもok
                }
            }
            else
            {
                if (LatEnd - LatSta < 30 && LonEnd - LonSta < 30)
                {
                    LatSta -= Math.Log10(LatEnd - LatSta + 1) * (UpPercent);//↑と同じ
                    LatEnd += Math.Log10(LatEnd - LatSta + 1) * (1 - UpPercent);
                    LonSta -= Math.Log10(LonEnd - LonSta + 1) * (1 - LeftPercent);
                    LonEnd += Math.Log10(LonEnd - LonSta + 1) * (LeftPercent);
                }
            }
            DrawMap();
        }
    }
}
