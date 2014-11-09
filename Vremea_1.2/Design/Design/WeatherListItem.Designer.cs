namespace WindowsFormsApplication1
{
    partial class WeatherListItem
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent()
        {
            this.Descriere = new System.Windows.Forms.Label();
            this.Temperatura = new System.Windows.Forms.Label();
            this.Umiditate = new System.Windows.Forms.Label();
            this.Vant = new System.Windows.Forms.Label();
            this.Presiune = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Descriere
            // 
            this.Descriere.AutoSize = true;
            this.Descriere.Font = new System.Drawing.Font("Arial", 24F);
            this.Descriere.Location = new System.Drawing.Point(41, 32);
            this.Descriere.Name = "Descriere";
            this.Descriere.Size = new System.Drawing.Size(150, 36);
            this.Descriere.TabIndex = 0;
            this.Descriere.Text = "Descriere";
            // 
            // Temperatura
            // 
            this.Temperatura.AutoSize = true;
            this.Temperatura.Font = new System.Drawing.Font("Arial", 16F);
            this.Temperatura.Location = new System.Drawing.Point(41, 85);
            this.Temperatura.Name = "Temperatura";
            this.Temperatura.Size = new System.Drawing.Size(133, 25);
            this.Temperatura.TabIndex = 1;
            this.Temperatura.Text = "Temperatura";
            // 
            // Umiditate
            // 
            this.Umiditate.AutoSize = true;
            this.Umiditate.Font = new System.Drawing.Font("Arial", 16F);
            this.Umiditate.Location = new System.Drawing.Point(41, 135);
            this.Umiditate.Name = "Umiditate";
            this.Umiditate.Size = new System.Drawing.Size(104, 25);
            this.Umiditate.TabIndex = 2;
            this.Umiditate.Text = "Umiditate";
            // 
            // Vant
            // 
            this.Vant.AutoSize = true;
            this.Vant.Font = new System.Drawing.Font("Arial", 16F);
            this.Vant.Location = new System.Drawing.Point(41, 187);
            this.Vant.Name = "Vant";
            this.Vant.Size = new System.Drawing.Size(54, 25);
            this.Vant.TabIndex = 3;
            this.Vant.Text = "Vant";
            // 
            // Presiune
            // 
            this.Presiune.AutoSize = true;
            this.Presiune.Font = new System.Drawing.Font("Arial", 16F);
            this.Presiune.Location = new System.Drawing.Point(41, 243);
            this.Presiune.Name = "Presiune";
            this.Presiune.Size = new System.Drawing.Size(96, 25);
            this.Presiune.TabIndex = 4;
            this.Presiune.Text = "Presiune";
            // 
            // WeatherListItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.Presiune);
            this.Controls.Add(this.Vant);
            this.Controls.Add(this.Umiditate);
            this.Controls.Add(this.Temperatura);
            this.Controls.Add(this.Descriere);
            this.Name = "WeatherListItem";
            this.Size = new System.Drawing.Size(338, 341);
            this.Load += new System.EventHandler(this.WeatherListItem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label Descriere;
        public System.Windows.Forms.Label Temperatura;
        public System.Windows.Forms.Label Umiditate;
        public System.Windows.Forms.Label Vant;
        public System.Windows.Forms.Label Presiune;
    }
}
