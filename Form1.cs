using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        PictureBox[] p = new PictureBox[9];
        char[] polja = new char[9];
        bool poteza = false;
        bool CPUzmaga = false;
        bool USERzmaga = false;
        int cpuScore = 0;
        int userScore = 0;
        int tieScore = 0;
        int level = -1;

        private int poteza1;

        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < p.Length; i++)
            {
                p[i] = new PictureBox();
                p[i].Click += pic_Click;
                p[i].Enabled = false;                  
            }
            TotalGamesLabel.Text = "Total games: " + (cpuScore + userScore + tieScore);
        }

        void pic_Click(object sender, EventArgs e)
        {
            var box = sender as PictureBox;
            int i = Array.IndexOf(p, box);
            if (poteza == false && polja[i] == default(char))
            {
                //naredi potezo
                p[i].Image = resizeImage(Image.FromFile("X.jpg"), new Size(50, 50));
                polja[i] = 'U';
                List<int> prosta1 = prosta(polja);
                if (prosta1.Count == 0)
                {
                    MessageBox.Show("Izenačeno");
                }
                else
                {
                    poteza = true;
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int x = 150;
            int xBack = 150;
            int y = 60;
            for (int i = 0; i < p.Length; i++)
            {
                if (i != 0)
                x = x + 56;
                if (i == 3 || i == 6)
                {
                    y = y + 56;
                    x = xBack;
                }    
                setUp(p[i], x, y);
                Controls.Add(p[i]);
            }
            
        }

        private static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private static void setUp(PictureBox p, int x, int y)
        {
            p.BackColor = Color.White;
            p.Size = new Size(50, 50);
            p.Location = new Point(x, y);
                
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CPUzmaga = check(polja, 'C');
            USERzmaga = check(polja, 'U');
            if (CPUzmaga == false) play();
            else if (CPUzmaga)
            {
                cpuScore++;
                for (int i = 0; i < p.Length; i++) p[i].Enabled = false;
                CPUscoreLabel.Text = "CPU: " + cpuScore;
                enabler(true);
                timer1.Stop();
                MessageBox.Show("CPU has won!");
            }
            else if (USERzmaga)
            {
                userScore++;
                UserScoreLabel.Text = "User: " + userScore;
            }
        }

        private void play()
        {
            if (poteza == true)
                {
                    poteza1 = CPUPlay(level);
                    //igra računalnik
                    poteza = false;
                    if (poteza1 == 999)
                    {
                        
                        enabler(true);          
                        tieScore++;
                        TieScoreLabel.Text = "Tie: " + tieScore.ToString();
                        MessageBox.Show("Tie");
                        timer1.Stop();
                    }
                    else
                    {
                        polja[poteza1] = 'C';
                        p[poteza1].Image = resizeImage(Image.FromFile("O.jpg"), new Size(50, 50));
                    }
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CPUzmaga = false;
            USERzmaga = false;
            for (int i = 0; i < p.Length; i++)
            {
                p[i].Enabled = true;
                p[i].Image = null;
                polja[i] = '\0';
            }
            
            if (LowRadioButton.Checked) level = 0;
            else if (MediumRadioButton.Checked) level = 1;
            else if (HardRadioButton.Checked) level = 2;
            else if (EvilRadioButton.Checked) level = 3;

            enabler(false);

            Random r = new Random();
            int a = (int)r.Next(0, 2);
            if (a == 0)
            {
                poteza = true;
                play();
            }
            else poteza = false;

            
        }

        private void enabler(bool enable)
        {
            LowRadioButton.Enabled = enable;
            MediumRadioButton.Enabled = enable;
            HardRadioButton.Enabled = enable;
            EvilRadioButton.Enabled = enable;
            PlayButton.Enabled = enable;
        }
        
        /*ANDRAZ JELENC*/
        private static List<int> prosta(char[] polja)
        {
            //preverimo katera polja so prosta in jih vrnemo
            List<int> free = new List<int>();
            for (int i = 0; i < polja.Length; i++)
            {
                if (polja[i] == default(char) && (polja[i] != 'U' || polja[i] != 'C'))
                {
                    free.Add(i);

                }
            }
            return free;
        }

        private int CPUPlay(int tezavnost)
        {
            /*
                funkcija sprejme trenutno pozicijo in tezavnost:
                    0: random
                    1: blocking first attack
                    2: block and attack in next round
                    3: ultimate logic
                vracamo izbrano polje na katerega bo racumalnik postavil svoj znak, ce vrnemo 999 pomeni, da je ni vec prostora za igranje.
            */

            Random rnd = new Random();
            //poiscemo prosta polja
            List<int> free = prosta(polja);

            //vsa polja so polna
            if (free.Count == 0)
            {
                return 999;
            }
            else if (free.Count == 9 || tezavnost == 0) //prva poteza je nakljucna oz. ce je tezavnost 0
            {
                int choosen = rnd.Next(0, free.Count);
                return free[choosen];
            }
            ////////////////////////////////////////////////////////////////////////////////
            //OSNOVNI SISTEM
            ////////////////////////////////////////////////////////////////////////////////

            if (tezavnost >= 2) //napad 1.
            {
                for (int i = 0; i < free.Count; i++)
                {
                    polja[free.ElementAt(i)] = 'C';
                    bool preveri = check(polja, 'C');
                    if (preveri)
                    {
                        return free.ElementAt(i);
                    }
                    polja[free.ElementAt(i)] = default(char);
                }
            }

            if (tezavnost >= 1) //obramba 1.
            {
                for (int i = 0; i < free.Count; i++)
                {
                    polja[free.ElementAt(i)] = 'U';
                    bool preveri = check(polja, 'U');
                    if (preveri)
                    {
                        return free.ElementAt(i);
                    }
                    polja[free.ElementAt(i)] = default(char);
                }
            }

            if (tezavnost == 3)
            {
                ////////////////////////////////////////////////////////////////////////////////
                //NAPREDNI SISTEM PREDVIDEVANJA
                ////////////////////////////////////////////////////////////////////////////////

                int[] stResitev = new int[free.Count];

                for (int i = 0; i < free.Count; i++)
                {
                    int resitev = 0;

                    //ustavimo element na i-to mesto
                    polja[free.ElementAt(i)] = 'C';

                    //naredimo scan prostih mest v drugem koraku
                    List<int> free_nivo2 = prosta(polja);

                    //poiskusimo ustaviti na vsa možna mesta
                    for (int j = 0; j < free_nivo2.Count; j++)
                    {
                        //ustavimo element na i-to mesto
                        polja[free_nivo2.ElementAt(j)] = 'C';

                        bool preveri = check(polja, 'C');
                        if (preveri)
                        {
                            //ce zmagamo v 2. koraku pristejemo moznost zmage
                            resitev++;
                        }
                        polja[free_nivo2.ElementAt(j)] = default(char);
                    }
                    polja[free.ElementAt(i)] = default(char);
                    stResitev[i] = resitev;
                }

                //poiscemo najvecjo verjetnost zmage
                int index = 0; //na katerem indexu je največja številka
                for (int i = 1; i < free.Count; i++)
                {
                    //iscemo najvecjo
                    if (stResitev[i] > stResitev[index])
                    {
                        index = i;
                    }
                }

                ////////////////////////////////////////////////////////////////////////////////
                //ZMAGA V DRUGEM NIVOJU
                ////////////////////////////////////////////////////////////////////////////////
                //ce sta nato 2 mozna zakljucka smo ze zmagali
                if (stResitev[index] >= 2)
                {
                    return free[index];
                }
                else if (free.Contains(4)) //napad na sredo
                {
                    return 4;
                }


                ////////////////////////////////////////////////////////////////////////////////
                //PREVERI DIAGONALO
                ////////////////////////////////////////////////////////////////////////////////

                bool diagonala = false;
                int steviloZasedenihCPU = 0;
                int steviloZasedenihUSER = 0;
                //preverimo če smo mi v sredino in napad diagonalen
                for (int i = 0; i < polja.Length; i++)
                {
                    if (polja[i] == 'C')
                    {
                        steviloZasedenihCPU++;
                    }
                    else if (polja[i] == 'U')
                    {
                        steviloZasedenihUSER++;
                    }
                }
                //ce smo CPU:1, USER:2
                if (steviloZasedenihCPU == 1 && steviloZasedenihUSER == 2)
                {
                    //ali smo diagonalno
                    if (polja[4] == 'C' && polja[0] == 'U' && polja[8] == 'U')
                    {
                        diagonala = true;
                    }
                    else if (polja[4] == 'C' && polja[2] == 'U' && polja[6] == 'U')
                    {
                        diagonala = true;
                    }
                }

                //ce smo napadeno diagonalno se postavimo na rob, ce ne pa na kot
                if (diagonala)
                {
                    //izberemo proste robove
                    List<int> ProstiRobovi = new List<int>();

                    for (int k = 0; k < free.Count; k++)
                    {
                        if (free[k] == 1 || free[k] == 3 || free[k] == 5 || free[k] == 7)
                        {
                            ProstiRobovi.Add(free[k]); //polja prostih robov
                        }

                    }
                    //vsi robovi imajo enako verjetnost, zato izberemo nakjucnega
                    int choosen = rnd.Next(0, ProstiRobovi.Count);
                    return ProstiRobovi[choosen];
                }
                else //nismo diagonalno napadeni, poiscemo tiste z najvecjo verjetnostjo zmage, ce je mozno naj bo kot
                {
                    //obstaja 2. hand napad ?

                    ////////////////////////////////////////////////////////////////////////////////
                    //OBRAMBA V DRUGEM NIVOJU
                    ////////////////////////////////////////////////////////////////////////////////
                    int[] USERstResitev = new int[free.Count];

                    for (int i = 0; i < free.Count; i++)
                    {
                        int USERresitev = 0;

                        //ustavimo element na i-to mesto
                        polja[free.ElementAt(i)] = 'C';

                        //naredimo scan prostih mest v drugem koraku
                        List<int> USERfree_nivo2 = prosta(polja);

                        //poiskusimo ustaviti na vsa možna mesta
                        for (int j = 0; j < USERfree_nivo2.Count; j++)
                        {
                            //ustavimo element na i-to mesto
                            polja[USERfree_nivo2.ElementAt(j)] = 'U';

                            bool preveri = check(polja, 'U');
                            if (preveri)
                            {
                                //ce zmagamo v 2. koraku pristejemo moznost zmage
                                USERresitev++;
                            }
                            polja[USERfree_nivo2.ElementAt(j)] = default(char);
                        }
                        polja[free.ElementAt(i)] = default(char);
                        USERstResitev[i] = USERresitev;
                    }
                    //poiscemo najvecjo verjetnost zmage
                    int USERindex = 0; //na katerem indexu je največja številka
                    for (int i = 1; i < free.Count; i++)
                    {
                        //iscemo najvecjo
                        if (USERstResitev[i] > USERstResitev[USERindex])
                        {
                            USERindex = i;
                        }
                    }
                    if (USERstResitev[USERindex] >= 2)
                    {
                        //zmaga v naslednem koraku
                        return free[USERindex];
                    }

                    //nadaljevanje po kotih
                    List<int> ugodna = new List<int>();
                    for (int k = 0; k < free.Count; k++)
                    {
                        if (stResitev[k] == stResitev[index])
                        {
                            ugodna.Add(free[k]);
                        }

                    }
                    List<int> ZanesliviKoti = new List<int>();
                    //je med ugodnimi mogoče kot ?
                    if (ugodna.Contains(0) || ugodna.Contains(2) || ugodna.Contains(6) || ugodna.Contains(8))
                    {
                        //izberemo kot :)
                        if (ugodna.Contains(0))
                        {
                            ZanesliviKoti.Add(0);
                        }
                        if (ugodna.Contains(2))
                        {
                            ZanesliviKoti.Add(2);
                        }
                        if (ugodna.Contains(6))
                        {
                            ZanesliviKoti.Add(6);
                        }
                        if (ugodna.Contains(8))
                        {
                            ZanesliviKoti.Add(8);
                        }
                        //izberemo enega izmed nakljucnih
                        int choosen = rnd.Next(0, ZanesliviKoti.Count);
                        return ZanesliviKoti[choosen];
                    }
                    else //izberemo nakjucnega
                    {
                        int choosen = rnd.Next(0, ugodna.Count);
                        return ugodna[choosen];
                    }
                }
            }
            return rnd.Next(0, free.Count);
        }

        private static bool check(char[] polja, char igralec)
        {
            //preverimo ali je igralec zmagal, ima kaksno 3 v vrsto.
            if ((polja[0] == igralec && polja[1] == igralec && polja[2] == igralec) || (polja[3] == igralec && polja[4] == igralec && polja[5] == igralec) ||
                (polja[6] == igralec && polja[7] == igralec && polja[8] == igralec) || (polja[0] == igralec && polja[3] == igralec && polja[6] == igralec) ||
                (polja[1] == igralec && polja[4] == igralec && polja[7] == igralec) || (polja[2] == igralec && polja[5] == igralec && polja[8] == igralec) ||
                (polja[0] == igralec && polja[4] == igralec && polja[8] == igralec) || (polja[2] == igralec && polja[4] == igralec && polja[6] == igralec))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
