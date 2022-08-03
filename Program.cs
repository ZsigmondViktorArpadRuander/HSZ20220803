using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace VersenyOnalloFeladato
{
    class Program
    {
        //Versenyzo struktura
        public struct Versenyzo
        {
            public string nev;
            public int pont;
            public string azonosito;
        }

        static void Main(string[] args)
        {
            //Szukseges elemek
            Versenyzo[] versenyzok;
            int versenyzoDb;

            //Versenyzok szamanak bekerese
            versenyzoDb = VersenyzokSzamanakBekerese();

            //Igy mar tudom, mekkora tombre lesz szuksegem >> versenyzoDb
            versenyzok = new Versenyzo[versenyzoDb];

            //Versenyzok adatainak bekerese
            VersenyzokAdatainakBekerese(versenyzok);

            //Versenyzok sorbarendezese pontszam szerint
            RendezesPontszamSzerint(versenyzok);

            //LDAK
            FeladatValaszto(versenyzok);
        }

        static void A(Versenyzo[] versenyzok)
        {
            //Sortores
            Console.WriteLine();

            double atlag = 0;

            foreach (Versenyzo v in versenyzok)
            {
                atlag += v.pont;
            }

            atlag /= versenyzok.Length;

            Console.WriteLine("A versenyzok atlag pontszama: " + atlag.ToString());
        }

        static void D(Versenyzo[] versenyzok)
        {
            //Sortores
            Console.WriteLine();

            if (versenyzok.Length == 1)
            {
                VersenyzoAdatainakKiirasa(versenyzok[0]);
            }
            else if (versenyzok.Length == 2)
            {
                VersenyzoAdatainakKiirasa(versenyzok[0]);
                VersenyzoAdatainakKiirasa(versenyzok[1]);
            }
            else
            {
                for (int i = 0; i <= 2; i++)
                {
                    VersenyzoAdatainakKiirasa(versenyzok[i]);
                }
            }
        }

        static void L(Versenyzo[] versenyzok)
        {
            //Sortores
            Console.WriteLine();

            foreach (Versenyzo v in versenyzok)
            {
                VersenyzoAdatainakKiirasa(v);
            }
        }

        static void RendezesPontszamSzerint(Versenyzo[] versenyzok)
        {
            for (int i = 0; i < versenyzok.Length - 1; i++)
            {
                for (int j = i + 1; j < versenyzok.Length; j++)
                {
                    if (versenyzok[i].pont < versenyzok[j].pont)
                    {
                        Versenyzo seged = versenyzok[i];
                        versenyzok[i] = versenyzok[j];
                        versenyzok[j] = seged;
                    }
                }
            }
        }

        static void VersenyzokAdatainakBekerese(Versenyzo[] versenyzok)
        {
            //Sortores
            Console.WriteLine();

            //Versenyzok adatainak bekerese
            for (int i = 0; i < versenyzok.Length; i++)
            {
                Versenyzo lokalisVersenyzo = new Versenyzo();

                Console.WriteLine((i + 1) + ". VERSENYZO ADATAI: ");
                do
                {
                    Console.Write("\tAdja meg a(z) " + (i + 1) + ". versenyzo nevet: ");
                    lokalisVersenyzo.nev = Console.ReadLine();

                    //lokalisVersenyzo.nev.Length >> megadja a versenyzo nevenek a hosszat (amit beirt a felhasznalo)
                    if (lokalisVersenyzo.nev.Length == 0)
                    {
                        HibauzenetKiirasa("\tAdja meg a versenyzo nevet!");
                    }
                } while (lokalisVersenyzo.nev.Length == 0);

                do
                {
                    Console.Write("\tAdja meg a(z) " + (i + 1) + ". versenyzo pontszamat: ");
                    lokalisVersenyzo.pont = int.Parse(Console.ReadLine());

                    if (lokalisVersenyzo.pont < 0)
                    {
                        HibauzenetKiirasa("\tNem lehet 0-nal kevesebb pontja egy versenyzonek!");
                    }
                } while (lokalisVersenyzo.pont < 0);

                do
                {
                    Console.Write("\tAdja meg a(z) " + (i + 1) + ". versenyzo verseny azonositojat: ");
                    lokalisVersenyzo.azonosito = Console.ReadLine();

                    //lokalisVersenyzo.azonosito.Length >> megadja a versenyzo azonositojanak a hosszat (amit beirt a felhasznalo)
                    if (lokalisVersenyzo.azonosito.Length == 0)
                    {
                        HibauzenetKiirasa("\tAdja meg a versenyzo azonositojat!");
                    }
                } while (lokalisVersenyzo.azonosito.Length == 0);
                
                versenyzok[i] = lokalisVersenyzo;

                //Sortores
                Console.WriteLine();
            }
        }

        static void VersenyzoAdatainakKiirasa(Versenyzo v)
        {
            Console.WriteLine(v.nev + ": pontszama " + v.pont + ", verseny azonositoja " + v.azonosito);
        }

        static void HibauzenetKiirasa(string hibaUzenet)
        {
            //Sortores
            Console.WriteLine();

            //Console betuszine piros lesz
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(hibaUzenet);

            //Console betuszinet alapertelmezettre allitja >> feher
            Console.ResetColor();

            //Sortores
            Console.WriteLine();
        }

        static void FeladatValaszto(Versenyzo[] versenyzok)
        {
            string valasz;
            do
            {
                //Sortores
                Console.WriteLine();

                Console.Write("L / D / A / K : ");
                valasz = Console.ReadLine();

                //Valasz nagybetusse alakitasa
                valasz = valasz.ToUpper();

                switch (valasz)
                {
                    case "L":
                        L(versenyzok);
                        break;

                    case "D":
                        D(versenyzok);
                        break;

                    case "A":
                        A(versenyzok);
                        break;

                    case "K":
                        //Kilpepes
                        break;

                    default: HibauzenetKiirasa("Hibas gomb!"); break;
                }
            } while (valasz != "K");
        }

        static int VersenyzokSzamanakBekerese()
        {
            int db;

            do
            {
                Console.Write("Hany versenyzo adatait szeretne felvinni: ");
                db = Convert.ToInt32(Console.ReadLine());

                if (db < 1)
                {
                    HibauzenetKiirasa("Legalabb 1 versenyzo adatait fel kell toltse!");
                }
            } while (db < 1);

            return db;
        }
    }
}
