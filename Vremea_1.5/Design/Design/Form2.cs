using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Xml;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        private string oras;
        public string gradeinitiale;
        public float grade;
        public float gradeK = 0;
        public float gradeF;


        public class Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }

        public class Sys
        {
            public int population { get; set; }
        }

        public class City
        {
            public int id { get; set; }
            public string name { get; set; }
            public Coord coord { get; set; }
            public string country { get; set; }
            public int population { get; set; }
            public Sys sys { get; set; }
        }

        public class Temp
        {
            public double day { get; set; }
            public double min { get; set; }
            public double max { get; set; }
            public double night { get; set; }
            public double eve { get; set; }
            public double morn { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class List
        {
            public int dt { get; set; }
            public Temp temp { get; set; }
            public double pressure { get; set; }
            public int humidity { get; set; }
            public List<Weather> weather { get; set; }
            public double speed { get; set; }
            public int deg { get; set; }
            public int clouds { get; set; }
            public double rain { get; set; }
        }

        public class RootObject
        {
            public string cod { get; set; }
            public double message { get; set; }
            public City city { get; set; }
            public int cnt { get; set; }
            public List<List> list { get; set; }
        }
        public class WeatherData
        {
            public Coord coord { get; set; }
            public Sys sys { get; set; }
            public Weather[] weather { get; set; }
            public City city { get; set; }
            public Temp temp{ get; set; }
            public List[] list { get; set; }
        }


        public Form2(string str_value)
        {
            InitializeComponent();
            oras = str_value;
            timer1.Start();

        }

        public Form2()
        {
            InitializeComponent();
            
        }


        Rectangle button1;


        protected override void OnPaint(PaintEventArgs e)
        {
            Font font = new Font("Arial", 16);
            Graphics g = this.CreateGraphics();
            System.Drawing.Drawing2D.LinearGradientBrush myBrush1 = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle,Color.Red, Color.Yellow, System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal);
            g.FillRectangle(myBrush1, ClientRectangle);
            System.Drawing.Drawing2D.LinearGradientBrush myBrush2 = new System.Drawing.Drawing2D.LinearGradientBrush(button1, Color.Purple, Color.Yellow, System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal);
            g.FillRectangle(myBrush2, button1);
            g.DrawString("BACK", Font, Brushes.Black, new Point(55, 520));
            myBrush1.Dispose();
            base.OnPaint(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Font font = new Font("Arial", 16);
            System.Drawing.Drawing2D.LinearGradientBrush myBrush2 = new System.Drawing.Drawing2D.LinearGradientBrush(button1, Color.Purple, Color.Yellow, System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal);
            System.Drawing.Drawing2D.LinearGradientBrush myBrush3 = new System.Drawing.Drawing2D.LinearGradientBrush(button1, Color.Yellow, Color.Purple, System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal);
            if (button1.Contains(e.Location))
            {
                g.FillRectangle(myBrush3, button1);
                g.DrawString("BACK", Font, Brushes.Black, new Point(55, 520));
            }
            else if (!button1.Contains(e.Location))
            {
                g.FillRectangle(myBrush2, button1);
                g.DrawString("BACK", Font, Brushes.Black, new Point(55, 520));
            }
            
            base.OnMouseMove(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (button1.Contains(e.Location))
                timer2.Start();
            base.OnMouseClick(e);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button1 = new Rectangle(35, 500, 70, 50);
            label4.Text = oras;
            
            try
            {
                
                WebClient webclient = new WebClient();
                webclient.Encoding = Encoding.UTF8;
                string json = webclient.DownloadString("http://api.openweathermap.org/data/2.5/forecast/daily?q=" + oras + "&mode=json&units=metric&cnt=3");
                WeatherData data = JsonConvert.DeserializeObject<WeatherData>(json);
                gradeinitiale = (Math.Round((decimal)data.list[0].temp.day, 2)).ToString();
                grade = Convert.ToSingle(gradeinitiale);
                weatherListItem1.Temperatura.Text = (Math.Round((decimal)data.list[0].temp.day, 2)).ToString() + "°C";
                weatherListItem1.Presiune.Text = "Pressure: " + data.list[0].pressure.ToString();
                weatherListItem1.Vant.Text = "Wind: " + data.list[0].speed.ToString() + " km/h";
                weatherListItem1.Umiditate.Text = "Humidity: " + data.list[0].humidity.ToString() + "%";
                
                weatherListItem1.Descriere.Text = data.list.ElementAt(0).weather[0].description;

                weatherListItem2.Temperatura.Text = (Math.Round((decimal)data.list[1].temp.day, 2)).ToString() + "°C";
                weatherListItem2.Presiune.Text = "Pressure: " + data.list[1].pressure.ToString();
                weatherListItem2.Vant.Text = "Wind: " + data.list[1].speed.ToString() + " km/h";
                weatherListItem2.Umiditate.Text = "Humidity: " + data.list[1].humidity.ToString() + "%";
               
                weatherListItem2.Descriere.Text = data.list[1].weather[0].description;
              
                
               
                weatherListItem3.Temperatura.Text = (Math.Round((decimal)data.list[2].temp.day, 2)).ToString() + "°C";
                weatherListItem3.Presiune.Text = "Pressure: " + data.list[2].pressure.ToString();
                weatherListItem3.Vant.Text = "Wind: " + data.list[1].speed.ToString() + " km/h";
                weatherListItem3.Umiditate.Text = "Humidity: " + data.list[2].humidity.ToString() + "%";
                
                weatherListItem3.Descriere.Text = data.list[2].weather[0].description;

                switch (data.list.ElementAt(0).weather[0].main)
                {
                    case "Clear":
                        pictureBox1.BackgroundImage= WindowsFormsApplication1.Properties.Resources.art_clear;

                        break;
                    case "Clouds":
                       
                        pictureBox1.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clouds;
                        break;
                    case "Mist":
                        pictureBox1.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clouds;
                        break;
                    case "Fog":
                        pictureBox1.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clouds;
                        break;
                    case "light_clouds":
                        pictureBox1.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clouds;
                        break;
                    case "Drizzle":
                        pictureBox1.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_light_rain;
                        break;
                    case "Rain":
                        pictureBox1.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_light_rain;
                        break;
                    case "Snow":
                        pictureBox1.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_light_rain;
                        break;
                    case "Storm":
                        pictureBox1.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_light_rain;
                        break;
                }

                switch (data.list.ElementAt(1).weather[0].main)
                {
                    case "Clear":
                        pictureBox2.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clear;

                        break;
                    case "Clouds":
                        pictureBox2.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clouds;
                        break;
                    case "Mist":
                        pictureBox2.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clouds;
                        break;
                    case "Fog":
                        pictureBox2.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clouds;
                        break;
                    case "light_clouds":
                        pictureBox2.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clouds;
                        break;
                    case "Drizzle":
                        pictureBox2.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_light_rain;
                        break;
                    case "Rain":
                        pictureBox2.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_light_rain;
                        break;
                    case "Snow":
                        pictureBox2.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_light_rain;
                        break;
                    case "Storm":
                        pictureBox2.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_light_rain;
                        break;
                }
                switch (data.list.ElementAt(2).weather[0].main)
                {
                    case "Clear":
                        pictureBox3.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clear;

                        break;
                    case "Clouds":
                        
                        pictureBox3.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clouds;
                        break;
                    case "Mist":
                        pictureBox3.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clouds;
                        break;
                    case "Fog":
                        pictureBox3.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clouds;
                        break;
                    case "light_clouds":
                        pictureBox3.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clouds;
                        break;
                    case "Drizzle":
                        pictureBox3.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_light_rain;
                        break;
                    case "Rain":
                        pictureBox3.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_light_rain;
                        break;
                    case "Snow":
                        pictureBox3.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_light_rain;
                        break;
                    case "Storm":
                        pictureBox3.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_light_rain;
                        break;
                }
                
            }

            catch
            {

            }
        }

        private void weatherListItem1_Load(object sender, EventArgs e)
        {

        }

        private void weatherListItem3_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity += .05;
            if (this.Opacity == 1)
            {
                timer1.Stop();
            }
        }


        private Point mouseOffset;
        private bool isMouseDown1 = false;       

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset1;
            int yOffset1;

            if (e.Button == MouseButtons.Left)
            {
                xOffset1 = -e.X - SystemInformation.FrameBorderSize.Width;
                yOffset1 = -e.Y - SystemInformation.CaptionHeight - SystemInformation.FrameBorderSize.Height;
                mouseOffset = new Point(xOffset1, yOffset1);
                isMouseDown1 = true;
            }
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown1)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown1 = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Opacity -= .05;
            if (this.Opacity == 0)
            {
                timer2.Stop();
                Form1 form1 = new Form1(label4.Text);
                form1.Show();
                this.Hide();
                
            }
        }



    }
}
