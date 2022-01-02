using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace LogoKaresz
{
	public partial class Form1 : Form
	{
		void FELADAT()
		{
			mozaik(30,"SFFF");
		}

		int orientáció(int i, int j)
		{
			if (i == 0 && j == 0) { return 90; }
			if (i == 0 && j == 1) { return 270; }
			if (i == 1 && j == 0) { return 0; }
			if (i == 1 && j == 1) { return 180; }
			return -1;
		}

		void Lopva_oldalaz(double d) => Lopva_oldalaz(defaultkaresz, d);
		void Lopva_előre(double d) => Lopva_előre(defaultkaresz, d);
		static void Lopva_oldalaz(Avatar a, double d)
		{
			using (new Átmenetileg(Jobbra, 90))
			using (new Rajzol(false))
				a.Előre(d);
		}
		static void Lopva_előre(Avatar a, double d)
		{
			using (new Rajzol(false))
				a.Előre(d);
		}
		void minta(double méret)
		{
			for (int i = 0; i < 5; i++)
				using (new Átmenetileg(Lopva_előre, i * méret))
					for (int j = 0; j < 2; j++)
					{
						using (new Átmenetileg(Lopva_oldalaz, j * méret))
						using (new Átmenetileg(Balra, orientáció(i % 2, j % 2)))
							alapelem(méret);
					}
		}
		
		void Odavissza(double méret) { Előre(méret); Hátra(méret); }
		void Félnégyzet_sarokból(double a)
		{
			using (new Átmenetileg(Jobbra, 45))
			{
				Előre(a);
				Balra(135);
				Előre(a * Math.Sqrt(2));
				Balra(135);
				Előre(a);
				Balra(90);
			}
		}
		void Félnégyzet_átlóközéppontból(double a)
		{
			using (new Átmenetileg(Jobbra, 90))
			{
				Előre(a*Math.Sqrt(2)/2);
				Balra(135);
				Előre(a);
				Balra(90);
				Előre(a);
				Balra(135);
				Előre(a * Math.Sqrt(2) / 2);
			}
		}
		void alapelem(double mérték)
		{
			Félnégyzet_sarokból(Math.Sqrt(2) * mérték / 2);
			Balra(90);
			Félnégyzet_sarokból(Math.Sqrt(2) * mérték / 2);
			Balra(135);
			Félnégyzet_átlóközéppontból(mérték);
			Balra(135);
		}

		void mozaik(double méret, string s)
		{
            foreach (char k in s)
            {
				Lopva_oldalaz(2 * méret);
                switch (k)
                {
					case 'S':
						break;
					case 'F':
						Lopva_előre(méret);
						break;
					case 'L':
						Lopva_előre(-méret);
						break;
                }
				minta(méret);
            }
			Lopva_oldalaz(-s.Length * 2*méret);
		}
	}
}
