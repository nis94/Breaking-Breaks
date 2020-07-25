using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bricks_Breaker
{
    public partial class FormBricks : Form
    {
        private int m_Ball_X = 4;
        private int m_Ball_Y = 4;
        private int m_BrokenCounter = 0;

        public FormBricks()
        {
            InitializeComponent();
        }

        private void FormBricks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && player.Left > 0)
            {
                player.Left -= 5;
            }
            else if (e.KeyCode == Keys.Right && player.Right < 360)
            {
                player.Left += 5;
            }
            else
            {
                return;
            }
        }

        private void ballMovement()
        {
            ball.Left += m_Ball_X;
            ball.Top += m_Ball_Y;

            if (ball.Left + ball.Width > ClientSize.Width || ball.Left < 0)
            {
                m_Ball_X = -m_Ball_X;
            }
            if (ball.Top < 0 || ball.Bounds.IntersectsWith(player.Bounds))
            {
                m_Ball_Y = -m_Ball_Y;
            }
        }

            
        private void breakblock()
        {
            foreach(Control ctrl in this.Controls)
            {
                if(ctrl is PictureBox && ctrl.Tag == "block")
                {
                    if(ball.Bounds.IntersectsWith(ctrl.Bounds)==true)
                    {
                        Controls.Remove(ctrl);
                        m_Ball_Y = -m_Ball_Y;
                        m_BrokenCounter++;
                    }
                }
            }
        }

        private void checkEndOfGame()
        {
            if(ball.Top+ball.Height>ClientSize.Height)
            {
                m_TimerBallMovement.Stop();
                if (MessageBox.Show("Game-Over!!!", "Wanna try again?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    m_BrokenCounter = 0;
                    Controls.Remove(ball);
                    Controls.Remove(player);
                    InitializeComponent();
                }
                else
                {
                    Application.Exit();
                }
            }

            if (m_BrokenCounter==24)
            {
                m_TimerBallMovement.Stop();
                if (MessageBox.Show("You win!!!","Wanna rematch?",MessageBoxButtons.YesNo)==DialogResult.Yes)
                {
                    Controls.Remove(ball);
                    Controls.Remove(player);
                    InitializeComponent();
                    InitializeComponent();
                }
                else
                {
                    Application.Exit();
                }
            }
        }


        private void m_TimerBallMovement_Tick(object sender, EventArgs e)
        {
            ballMovement();
            breakblock();
            checkEndOfGame();
        }
    }
}
