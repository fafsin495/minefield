using System;
using System.Drawing;
using System.Drawing.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMayinTarlasi
{
    public partial class FormMain : MetroFramework.Forms.MetroForm
    {
      


        GameCreate GAME;
        ConsolSlidingPanel GAMEConsol;
        ScoreSystem scoreSystem;
        private string playerName ="";
        private int second = 0;

        
        public FormMain()
        {
            
            GAME = GameCreate.GetInstance();
            scoreSystem = ScoreSystem.GetInstance();
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
           
          
           
            this.StyleManager = mainStyleManager;

            GAMEConsol = new ConsolSlidingPanel(ref pnlConsolMain, 0, 400);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            
            this.Controls.Add(GAME.panelMap);
            GAME.panelMap.BackColor = Color.Black;
        }

        private void btnBaslat_Click(object sender, EventArgs e)
        {
            pnlGameStart.Visible = false;
            GAME.panelMap.Hide();
            this.Refresh();
            #region Oyun zorluk derecesi
           
            if (toggleKolay.CheckState == CheckState.Checked)
            {
                GAME.Start(20, 20, 35); second = 120;
            }
            else if (toggleOrta.CheckState == CheckState.Checked)
            {
                GAME.Start(30, 20, 55);  second = 180;
            }
            else if (toggleZor.CheckState == CheckState.Checked)
            {
                GAME.Start(50, 20, 200); second = 250;
            }
            Refresh();
            #endregion
            this.Width  = (GAME.panelMap.Width + 32)  < 228 ? 228 : (GAME.panelMap.Width + 32);
            Refresh();
            this.Height = GAME.panelMap.Height + 183;
            Refresh();
            WindowPosition();
            Refresh();
            pnlSkorBoard.Visible = true;
            Refresh();
            playerName = (txtBoxTakmaIsim.Text == "" ) ? "-" : txtBoxTakmaIsim.Text;
            Refresh();
            
            GAME.panelMap.Show();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            second--;
            if (second == 0) //zaman bitince
            {
                scoreSystem.Add(playerName, GAME.score);
                timer1.Stop();
                ScoreBoard();
                WindowStartupSettings();    
                return;
            }

            lblSkorDisplay.Text      = ""+GAME.score;
            lblTemizAlanDisplay.Text = ""+GAME.gameBtnEmptyNum;
            lblKalanSureDisplay.Text = ""+second;
            lblMayinSayisiDisplay.Text = ""+ GAME.gameBtnMineNum;

            if (GAME.gameBtnEmptyNum == 0) // Kazandın

            {
               
                scoreSystem.Add(playerName, GAME.score); 
                timer1.Stop();
                MessageBox.Show("TEBRKİLER !! Kazandın :)");
                ScoreBoard();
                WindowStartupSettings();
                return;
            }
            if (GAME.lose) // Kaybettin
            {
                
                scoreSystem.Add(playerName, GAME.score);
                timer1.Stop();
                MessageBox.Show("KAYBETTİN :( ");
                ScoreBoard();
                WindowStartupSettings();
                return;
            }
        }

        private void WindowPosition()
        {
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                                      (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 4);
        }

        private void WindowStartupSettings()
        {
            timer1.Stop();
            GAME.Clear();
            Refresh();
            txtBoxTakmaIsim.Text = "DEFAULT";
            lblSkorDisplay.Text      =
            lblTemizAlanDisplay.Text =
            lblKalanSureDisplay.Text = "0";

            pnlSkorBoard.Visible   = false;
            this.Refresh();
            pnlGameStart.Visible  = true;
            txtBoxTakmaIsim.Focus();
        }


        private void toggleKolay_Click(object sender, EventArgs e)
        {
            toggleKolay.CheckState = CheckState.Checked;
            toggleOrta.CheckState = CheckState.Unchecked;
            toggleZor.CheckState = CheckState.Unchecked;
        }

        private void toggleOrta_Click(object sender, EventArgs e)
        {
            toggleKolay.CheckState = CheckState.Unchecked;
            toggleOrta.CheckState = CheckState.Checked;
            toggleZor.CheckState = CheckState.Unchecked;
        }

        private void toggleZor_Click(object sender, EventArgs e)
        {
            toggleKolay.CheckState = CheckState.Unchecked;
            toggleOrta.CheckState = CheckState.Unchecked;
            toggleZor.CheckState = CheckState.Checked;
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private void txtBxTakmaIsim_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnBaslat_Click(this, new EventArgs());
        }

        private void yeniOyunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowStartupSettings();
        }
        private void HakkındaToolStripMenuItem_Click(object sender, EventArgs e) => MessageBox.Show("Bu Proje Algoritma 2 ödevi kapsamında yapılmıştır");



        private void ScoreBoard()
        {
            FormScoreBoard skrfrm = new FormScoreBoard
            {
                Owner = this,
                StyleManager = StyleManager,
                Theme = Theme,
                Style = Style
            };
            skrfrm.ShowDialog();
        }

        private void skorTablosuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScoreBoard();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            Menu.Show(btnMenu, 0, btnMenu.Height);
        }

        private void txtBoxConsoleInput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
