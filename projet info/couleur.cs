/*
 * Crée par SharpDevelop.
 * Utilisateur: mendu
 * Date: 24/01/2017
 * Heure: 16:35
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;

namespace projet_info
{
	/// <summary>
	/// Description of couleur.
	/// </summary>
	public class couleur
	{
		private Byte Rouge;
		private Byte Vert;
		private Byte Bleu;
		
		public Byte rouge{
			get{return Rouge;}
			set{ Rouge=value;}
		}
		
		public Byte vert{
			get{return Vert;}
			set{ Vert=value;}
		}
		
		public Byte bleu{
			get{return Bleu;}
			set{ Bleu=value;}
		}
		public couleur(Byte R,Byte G, Byte B)
		{
			rouge=R;
			vert=G;
			bleu=B;
		}
		public void Display()
		{
			Console.Write(rouge+"\t"+vert+"\t"+bleu+"\t");
		}
	
	}
}
