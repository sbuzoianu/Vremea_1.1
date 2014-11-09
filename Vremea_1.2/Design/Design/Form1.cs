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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

        }
       // SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\FM2 Processor\Desktop\vremea merge aci3\merge vremea aici\Design\Design\Database1.mdf;Integrated Security=True;User Instance=True");
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Manita\Desktop\vremea merge aci3.5\vremea merge aci3.5\merge vremea aici\Design\Design\Database1.mdf;Integrated Security=True;User Instance=True");
        SqlCommand cmd;
        SqlDataReader jlk;


        public string oras;
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
            public int type { get; set; }
            public int id { get; set; }
            public double message { get; set; }
            public string country { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class Main
        {
            public double temp { get; set; }
            public double pressure { get; set; }
            public int humidity { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
        }

        public class Wind
        {
            public double speed { get; set; }
            public double deg { get; set; }
        }

        public class Clouds
        {
            public int all { get; set; }
        }

        public class RootObject
        {
            public Coord coord { get; set; }
            public Sys sys { get; set; }
            public List<Weather> weather { get; set; }
            public string @base { get; set; }
            public Main main { get; set; }
            public Wind wind { get; set; }
            public Clouds clouds { get; set; }
            public int dt { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int cod { get; set; }
        }
        public class WeatherData
        {
            public Coord coord { get; set; }
            public Sys sys { get; set; }
            public Weather[] weather { get; set; }
            public Main main { get; set; }
            public Wind wind { get; set; }
        }
        
        string mod;
        string orasDefault1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity += .05;
            if (this.Opacity == 1)
            {
                timer1.Stop();
            }
        }

        Rectangle rect_button1;
        Rectangle rect_button2;
        Rectangle rect_button3;
        Rectangle rect_button4;
        Image button_image1 = WindowsFormsApplication1.Properties.Resources.iconmonstr_x_mark_4_icon_48;
        Image button_image2 = WindowsFormsApplication1.Properties.Resources.iconmonstr_minus_5_icon_48;
        Image button_image3 = WindowsFormsApplication1.Properties.Resources.iconmonstr_help_3_icon_48;
        Image button_image4 = WindowsFormsApplication1.Properties.Resources.search1;
       

        


        protected override void OnPaint(PaintEventArgs e)
        {
            Font font = new Font("Arial", 16);



            e.Graphics.DrawImage(button_image1, rect_button1);
            e.Graphics.DrawImage(button_image2, rect_button2);
            e.Graphics.DrawImage(button_image3, rect_button3);
            e.Graphics.DrawImage(button_image4, rect_button4);

            
            
            base.OnPaint(e);
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (rect_button1.Contains(e.Location))
            {
                button_image1 = WindowsFormsApplication1.Properties.Resources.iconmonstr_x_mark_3_icon_48;
            }
            else if (!rect_button1.Contains(e.Location))
            {
                button_image1 = WindowsFormsApplication1.Properties.Resources.iconmonstr_x_mark_4_icon_48;
            }
            if (rect_button2.Contains(e.Location))
            {
                button_image2 = WindowsFormsApplication1.Properties.Resources.iconmonstr_minus_4_icon_48;
            }
            else if (!rect_button1.Contains(e.Location))
            {
                button_image2 = WindowsFormsApplication1.Properties.Resources.iconmonstr_minus_5_icon_48;
            }
            if (rect_button3.Contains(e.Location))
            {
                button_image3 = WindowsFormsApplication1.Properties.Resources.iconmonstr_help_2_icon_48;
            }
            else if (!rect_button1.Contains(e.Location))
            {
                button_image3 = WindowsFormsApplication1.Properties.Resources.iconmonstr_help_3_icon_48;
            }
            if (rect_button4.Contains(e.Location))
            {
                button_image4 = WindowsFormsApplication1.Properties.Resources.search2;
            }
            else if (!rect_button1.Contains(e.Location))
            {
                button_image4 = WindowsFormsApplication1.Properties.Resources.search1;
            }
            this.Invalidate();
            base.OnMouseMove(e);
        }
    


        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (rect_button1.Contains(e.Location))
                timer2.Start();
            if (rect_button2.Contains(e.Location))
                this.WindowState = FormWindowState.Minimized;
            if (rect_button3.Contains(e.Location))
                MessageBox.Show("When you open the app you can see the weather in the place you are" + '\n' + "If you want to see the weather in other places, type the name of the place in the text box and then press Search!");
            if (rect_button4.Contains(e.Location))
            {
                if (textBox1.TextLength != 0)
                {
                    timer3.Start();
                   
                    
                }
            }
            base.OnMouseClick(e);
        }

        private Point mouseOffset;
        private bool isMouseDown1 = false;
        private bool isMouseDown2 = false;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
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

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown1)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown1 = false;
            }
        }

        private void weatherListItem2_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset2;
            int yOffset2;

            if (e.Button == MouseButtons.Left)
            {
                xOffset2 = -e.X - SystemInformation.FrameBorderSize.Width;
                yOffset2 = -e.Y - SystemInformation.CaptionHeight - SystemInformation.FrameBorderSize.Height;
                mouseOffset = new Point(xOffset2, yOffset2);
                isMouseDown2 = true;
            }
        }

        private void weatherListItem2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown2)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void weatherListItem2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown2 = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Opacity -= .05;
            if (this.Opacity == 0)
            {
                timer2.Stop();
                Application.Exit();
            }
        }

       

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                timer3.Start();
            }
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            float numar = 173.434F;
            gradeK = grade;
            if (radioButton2.Checked == true && gradeK == grade)
            {
                gradeK = gradeK + numar;
                weatherListItem2.Temperatura.Text = Convert.ToString(gradeK) + "°K";
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                weatherListItem2.Temperatura.Text = Convert.ToString(grade) + "°C";

            }
        }



        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            gradeF = grade;

            if (radioButton3.Checked == true && gradeF == grade)
            {
                gradeF = gradeF * 9 / 5 + 32;
                weatherListItem2.Temperatura.Text = Convert.ToString(gradeF) + "°F";
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            this.Opacity -= .05;
            if (this.Opacity == 0)
            {
                timer3.Stop();
                try
                {

                    oras = textBox1.Text;
                    label1.Text = textBox1.Text;
                    textBox1.Clear();
                    WebClient webclient = new WebClient();
                    webclient.Encoding = Encoding.UTF8;
                    string json = webclient.DownloadString("http://api.openweathermap.org/data/2.5/weather?q=" + oras + ",&lang=en&units=metric");
                    WeatherData data = JsonConvert.DeserializeObject<WeatherData>(json);
                    gradeinitiale = (Math.Round((decimal)data.main.temp, 2)).ToString();
                    grade = Convert.ToSingle(gradeinitiale);
                    weatherListItem2.Temperatura.Text = (Math.Round((decimal)data.main.temp, 2)).ToString() + "°C";
                    weatherListItem2.Presiune.Text = "Pressure: " + data.main.pressure.ToString();
                    weatherListItem2.Vant.Text = "Wind: " + data.wind.speed.ToString() + " km/h";
                    weatherListItem2.Umiditate.Text = "Humidity: " + data.main.humidity.ToString() + "%";
                    weatherListItem2.Descriere.Text = data.weather[0].description;


                    switch (data.weather[0].main)
                    {
                        case "Clear":
                            this.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clear;
                            break;
                        case "Clouds":
                            this.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clouds;
                            break;
                        case "Fog":
                            this.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clouds;
                            break;
                        case "light_clouds":
                            this.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clouds;
                            break;
                        case "Drizzle":
                            this.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_light_rain;
                            break;
                        case "Rain":
                            this.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_light_rain;
                            break;
                        case "Snow":
                            this.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_light_rain;
                            break;
                        case "Storm":
                            this.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_light_rain;
                            break;
                    }
                }

                catch
                {

                }
                this.BackgroundImageLayout = ImageLayout.Stretch;
                timer1.Start();
            }
            
        }
        ////////////////  Select Mode
        
        private void Mode()
        {
            string str = "SELECT * FROM Setari WHERE ID='1'";
            con.Open();
            cmd = new SqlCommand(str, con);
            //cmd.ExecuteNonQuery();
            jlk = cmd.ExecuteReader();
              while (jlk.Read())
             {
            mod = Convert.ToString(jlk[1]);
             }

          
            con.Close();

        }
        ////////////// Selectare orasDefault
        private void orasDefault()
        {
            string str = "SELECT * FROM orasDefault WHERE ID='1'";
            con.Open();
            cmd = new SqlCommand(str, con);
            //cmd.ExecuteNonQuery();
            jlk = cmd.ExecuteReader();
            while (jlk.Read())
            {
                orasDefault1 = Convert.ToString(jlk[1]);
            }


            con.Close();

        }
        ////////////// FORM 11111111111  LOOOOOOOOAD

        private void Form1_Load(object sender, EventArgs e)
        {
            Mode();
            if (mod == "1")
                checkBox1.Checked = true;
            else checkBox1.Checked = false;
            
            rect_button1 = new Rectangle(265, 245, 50, 50);
            rect_button2 = new Rectangle(265, 315, 50, 50);
            rect_button3 = new Rectangle(265, 385, 50, 50);
            rect_button4 = new Rectangle(725, 450, 50, 50);
            radioButton1.Checked = true;

            string city = "London";
            string countrycode = "";

            if (checkBox1.Checked == false)
            {
                XmlTextReader reader = new XmlTextReader("http://freegeoip.net/xml/");
                try
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                            if (reader.Name == "CountryCode")
                            {
                                reader.Read();
                                countrycode = reader.Value;
                            }

                        if (reader.NodeType == XmlNodeType.Element)
                            if (reader.Name == "City")
                            {
                                reader.Read();
                                city = reader.Value;
                            }
                    }
                }
                catch { }
            }
            else if (checkBox1.Checked==true)
            {
                orasDefault();
                city = orasDefault1;
            }
            textBox1.Text = city;
            label1.Text = city;

            try
            {
                oras = textBox1.Text;
                textBox1.Clear();
                WebClient webclient = new WebClient();
                webclient.Encoding = Encoding.UTF8;
                string json = webclient.DownloadString("http://api.openweathermap.org/data/2.5/weather?q=" + oras + ",&lang=en&units=metric");
                WeatherData data = JsonConvert.DeserializeObject<WeatherData>(json);
                gradeinitiale = (Math.Round((decimal)data.main.temp, 2)).ToString();
                grade = Convert.ToSingle(gradeinitiale);
                weatherListItem2.Temperatura.Text = (Math.Round((decimal)data.main.temp, 2)).ToString() + "°C";
                weatherListItem2.Presiune.Text = "Pressure: " + data.main.pressure.ToString();
                weatherListItem2.Vant.Text = "Wind: " + data.wind.speed.ToString() + " km/h";
                weatherListItem2.Umiditate.Text = "Humidity: " + data.main.humidity.ToString() + "%";
                weatherListItem2.Descriere.Text = data.weather[0].description;


                switch (data.weather[0].main)
                {
                    case "Clear": 
                        this.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clear;
                       
                        break;
                    case "Clouds": 
                        this.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clouds;
                        break;
                    case "Fog": 
                        this.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clouds;
                        break;
                    case "light_clouds": 
                        this.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_clouds;
                        break;
                    case "Drizzle": 
                        this.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_light_rain;
                        break;
                    case "Rain":
                        this.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_light_rain;
                        break;
                    case "Snow": 
                        this.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_light_rain;
                        break;
                    case "Storm": 
                        this.BackgroundImage = WindowsFormsApplication1.Properties.Resources.art_light_rain;
                        break;
                }
            }

            catch
            {

            }
            this.BackgroundImageLayout = ImageLayout.Stretch;
            timer1.Start();
        
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

           if (checkBox1.Checked == true)
            {
                string str = "UPDATE Setari SET Mode='" + '1' + "'  WHERE ID='1'";
                con.Open();
                cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else if (checkBox1.Checked == false)
            {
                string str = "UPDATE Setari SET Mode='" + '0' + "'  WHERE ID='1'";
                con.Open();
                cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
                con.Close();
            
            }
        }
                
        private void weatherListItem2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength > 2)
            {
                string str = "UPDATE orasDefault SET oras='" + textBox1.Text + "'  WHERE ID='1'";


                con.Open();
                cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
                con.Close();

                timer3.Start();
                
               
            }

        }

        private void weatherListItem2_Load_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
