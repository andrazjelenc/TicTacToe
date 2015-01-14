using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace _3x3
{
    class Program
    {
        static void Main(string[] args)
        {
            bool zmaga = false;
            char[] polja = new char[9];
            
            //izbira kdo zacne
            Random rnd = new Random();
            int start = rnd.Next(0, 2);
            if (start == 0)
            {
                //zacne racunalnik
                while (!zmaga) //dokler nekdo ne zmaga
                {
                    int poteza = CPUPlay(polja, 3);
                    if (poteza == 999)
                    {
                        Console.WriteLine("izenaceno!");
                        break;
                    }
                    polja[poteza] = 'C'; //potezo odigra racunalnik
                    zmaga = check(polja, 'C'); //preverimo ali je zmagal
                    izpis(polja); //izpisemo polje
                    if (zmaga == false)
                    {
                        //na potezi je uporabnik
                        int u = UserPlay(polja);
                        if (u == 999)
                        {
                            Console.WriteLine("izenaceno!");
                            break;
                        }
                        polja[u] = 'U';

                        //je uporabnik zmagal
                        zmaga = check(polja, 'U');
                        izpis(polja);
                        if (zmaga == true)
                        {
                            Console.WriteLine("Zmagali ste!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Zmagal je racunalnik!");
                    }
                }
            }
            else
            {
                //zacne uporabnik
                izpis(polja); //zaceten izpis za uporabnika
                while (!zmaga)
                {
                    //izbira zacetnega polja
                    int u = UserPlay(polja);
                    Debug.Print(u.ToString());
                    if (u == 999)
                    {
                        Console.WriteLine("izenaceno!");
                        break;
                    }
                    polja[u] = 'U';
                    zmaga = check(polja, 'U'); //preverimo ali smo zmagali
                    izpis(polja); 
                    if (zmaga == false)
                    {
                        int poteza = CPUPlay(polja, 2);
                        Debug.Print(poteza.ToString());
                        if (poteza == 999)
                        {
                            Console.WriteLine("izenaceno!");
                            break;
                        }

                        polja[poteza] = 'C'; //potezo odigra racunalnik

                        zmaga = check(polja, 'C');
                        izpis(polja);
                        if (zmaga == true)
                        {
                            Console.WriteLine("Zmagal je racunalnik!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Zmagali ste!");
                    }
                }
            }
            Console.ReadLine();
        }

        private static int CPUPlay(char[] polja, int tezavnost)
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
		
        private static int UserPlay(char[] polja)
        {
            //poiscemo prazne prostore
            List<int> free = prosta(polja);

            if (free.Count == 0)
            {
                return 999;
            }

            //uporabnikov vnos
            Console.WriteLine(string.Join(",",free));
            while(true)
            {
                string a = Console.ReadLine();
                int dump;
                bool st = Int32.TryParse(a, out dump);
                if (st)
                {
                    int num = Int32.Parse(a);
                    if (free.Contains(num))
                    {
                        return num;
                    }

                }
               
            }
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

        private static  void izpis(char[] polja)
        {
            //izpisemo trenutno stanje na polju
            char[] backup = new char[9];
            Array.Copy(polja, backup, 9);

            for (int i = 0; i < backup.Length; i++)
            {
                if (backup[i] == default(char))
                {
                    backup[i] = 'X';
                }
            }
            for (int i = 1; i <= backup.Length; i++)
            {
                if (i % 3 == 0)
                {
                    Console.Write(backup[i - 1]);
                    Console.WriteLine();
                }
                else
                {
                    Console.Write(backup[i - 1]);
                }
            }
            Console.WriteLine();
        }
    }
}
