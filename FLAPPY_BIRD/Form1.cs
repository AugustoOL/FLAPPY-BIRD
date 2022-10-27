using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FLAPPY_BIRD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int gravidade = 5;
        int Speed = 10;
        int placar = 0;
        int Recorde = 0;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode ==Keys.Space)
            {
                gravidade = -5;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravidade = 5;
            }
        }

        private void jogo_Tick(object sender, EventArgs e)
        {
            bird.Top += gravidade;
            tuboInferior.Left -= Speed;
            tubosuperior.Left -= Speed;


                if(tuboInferior.Left <0 - tuboInferior.Width)
                {
                    tuboInferior.Left = this.Width + tuboInferior.Width;
                    placar++;
                }

                if (tubosuperior.Left < 0 - tubosuperior.Width)
                {
                    tubosuperior.Left = this.Width + tubosuperior.Width;
                    placar++;

                }
//colisao
            if (bird.Bounds.IntersectsWith(tuboInferior.Bounds) ||
                bird.Bounds.IntersectsWith(tubosuperior.Bounds) ||
                bird.Bounds.IntersectsWith(ground.Bounds) ||
                bird.Top <= 0)
            {
                lbMensagem.Text = "VOCE PERDEU!";
                jogo.Stop();
            }
            lbPlacar.Text = String.Format("PLACAR: {0}", placar);
            Acelerar(); 
        }

        private void Acelerar()
        {
            if (placar > 20) Speed = 15;
            if (placar > 30) Speed = 20;
            if (placar > 40) Speed = 25;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar))
           {
                tuboInferior.Left = this.Width + tuboInferior.Width;
                tubosuperior.Left = this.Width + tubosuperior.Width * 2;
                bird.Top = this.Height / 2;

                if (placar > Recorde)
                {
                    Recorde = placar;
                    lbRecord.Text = String.Format("RECORDE {0}", Recorde);
                    Registro.Gravar("FLAPPY", "Recorde", Recorde.ToString());
                }
                lbMensagem.Text = "Pressione ESC para sair...";
                placar = 0;
                Speed = 10;
                jogo.Start();
            }
            if(e.KeyChar== Convert.ToChar(Keys.Escape))
                Environment.Exit(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Recorde = Int32.Parse(Registro.Ler("FLAPPY", "Recorde"));
            lbRecord.Text = String.Format("RECORDE {0}", Recorde);
        }

    }
}
    
    
      