/*
 * Crée par SharpDevelop.
 * Utilisateur: mendu
 * Date: 24/01/2017
 * Heure: 16:40
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;

namespace projet_info
{
	/// <summary>
	/// Description of Bitmap.
	/// </summary>
	public class Bitmap
	{
		#region propriete attributs
		private int height;
		private int width;
		private couleur[,] tableaudecouleur;
		private Byte[] tableau_de_bytes;
		
		private int entete=14;		
		private int entetebis=40;
		private int tailledeloffset=54;
		public Byte[] Tableau_de_Byte
		{
			get{return tableau_de_bytes;}
			set{tableau_de_bytes=value;}
		}
		public int Height{
			get{return height;}
			set {height=value;}
		}
		public int Width{
			get{return width;}
			set{width=value;}
		}
		public couleur[,] TableauDeCouleur{
			get{return tableaudecouleur;}
			set{tableaudecouleur=value;}
		}
		#endregion
		public Bitmap(string chemin)
		{
			Tableau_de_Byte = System.IO.File.ReadAllBytes(@chemin);
			
			
			Height=recuperer_taille(Tableau_de_Byte,entete+8);
			Width=recuperer_taille(Tableau_de_Byte,entete+4);
			colortab();
			Byte[] tab2=Tableau_de_Byte;
			Tableau_de_Byte=null;
			Tableau_de_Byte=new Byte[entete+entetebis];
			
				for(int i=0;i<entete+entetebis;i++)
			{
				
				Tableau_de_Byte[i]=tab2[i];
		}
			couleur[,] tableauDeCouleur =new couleur[Height,Width];
			
		}
		public Bitmap()
		{
			
			Tableau_de_Byte=new byte[54];
			int hauteur = 1;
			int largeur = 1;
			do
			{

				Console.WriteLine("veuillez entrer une longeur multiple de 4");
				hauteur = Int32.Parse(Console.ReadLine());
				Console.WriteLine("veuillez entrer une largeur multiple de 4");
				largeur = Int32.Parse(Console.ReadLine());
			} while (hauteur % 4 != 0 || largeur % 4 != 0);

			header(hauteur, largeur);
			Console.WriteLine("voulez vous:\n 1-un fond aléatoire\n 2-un fond unis que vous definirez");
			int choix = Int32.Parse(Console.ReadLine());
			couleur[,] tableauDeCouleur = new couleur[hauteur, largeur];
			if (choix == 1)
			{
				Random rnd = new Random();
				
				for (int i = 0; i < hauteur; i++)
				{
					for (int j = 0; j < largeur; j++)
					{
						int Rb = rnd.Next(0, 255);
						int Gb = rnd.Next(0, 255);
						int Bb = rnd.Next(0, 255);
						TableauDeCouleur[i, j] = new couleur(conversion_binaire(Bb), conversion_binaire(Gb), conversion_binaire(Rb));
					}
				}
			}
			if (choix == 2)
			{
				int R = -1;
				int B = -1;
				int G = -1;
				Console.WriteLine(" de quelle couleur doit etre le fond");
				do
				{
					Console.WriteLine("entrez la valeur de la couleur rouge, entre 0 et 255");
					 R = Int32.Parse(Console.ReadLine());
				} while (R < 0 || R > 255);
				do
				{
					Console.WriteLine("entrez la valeur de la couleur verte, entre 0 et 255");
					G = Int32.Parse(Console.ReadLine());
				} while (R < 0 || R > 255);
				do
				{
					Console.WriteLine("entrez la valeur de la couleur bleu, entre 0 et 255");
					B = Int32.Parse(Console.ReadLine());
				} while (R < 0 || R > 255);
				
				for (int i = 0; i < hauteur; i++)
				{
					for (int j = 0; j < largeur; j++)
					{
						
						tableauDeCouleur[i, j] = new couleur(conversion_binaire(B), conversion_binaire(G), conversion_binaire(R));
					}
				}
			}
			TableauDeCouleur = tableauDeCouleur;
			Height = hauteur;
			Width = largeur;

		}

		public void header(int hauteur,int largeur)
		{
			
			for(int i=0;i<entete;i++)
			{
				Tableau_de_Byte[i]=0;
			}
			Tableau_de_Byte[0]=66;
			Tableau_de_Byte[1]=77;
			
			
			Byte[] Tableau_de_Bytebis=conversion_binairetab((largeur*hauteur*3)+54);
			for(int i=0;i<4;i++)
			{
				Tableau_de_Byte[2+i]=Tableau_de_Bytebis[i];
			}

			Tableau_de_Byte[entete] = 40;
			Tableau_de_Bytebis=conversion_binairetab(hauteur);
			for(int i=0;i<4;i++)
			{
			
		Tableau_de_Byte[entete+8+i]=Tableau_de_Bytebis[i];
			}


			Tableau_de_Byte[entete + 21] = 18;
			Tableau_de_Bytebis =conversion_binairetab(largeur);
			for(int i=0;i<4;i++)
			{
			
		Tableau_de_Byte[entete+4+i]=Tableau_de_Bytebis[i];
			}

			Tableau_de_Byte[entete+12]=1;
			Tableau_de_Byte[10] = 54;
			Tableau_de_Byte[entete+14]=24;
			Tableau_de_Bytebis = conversion_binairetab(largeur*hauteur*3);
			for (int i = 0; i < 4; i++)
			{

				Tableau_de_Byte[entete + 20 + i] = Tableau_de_Bytebis[i];
			}
			Tableau_de_Byte[entete + 24] = 18;
			Tableau_de_Byte[entete + 25] = 11;
			Tableau_de_Byte[entete + 28] = 18;
			Tableau_de_Byte[entete + 29] = 11;
		}
		#region modification d'image
		/// <summary>
		/// channge la couleur d'un pixel en une autre couleur
		/// </summary>
		/// <param name="Rouge"></param>
		/// <param name="Vert"></param>
		/// <param name="Bleu"></param>
public void change_color(int Rouge,int Vert,int Bleu)
		{
			
			byte RougeB=conversion_binaire(Rouge);
			byte vertB=conversion_binaire(Vert);
			byte bleuB=conversion_binaire(Bleu);
		for (int i=0;i<(Width);i++)
		
			
			for(int j=0;j<Width;j++)
			{
				TableauDeCouleur[i,j].rouge=RougeB;
				TableauDeCouleur[i,j].vert=vertB;
				TableauDeCouleur[i,j].bleu=bleuB;
			
			}
				}
				
public void change_color(int Rouge,int Vert,int Bleu,int positionX, int positionY)
		{
			
			byte RougeB=conversion_binaire(Rouge);
			byte vertB=conversion_binaire(Vert);
			byte bleuB=conversion_binaire(Bleu);
			TableauDeCouleur[positionX,positionY].bleu=bleuB;
			TableauDeCouleur[positionX,positionY].rouge=RougeB;
			TableauDeCouleur[positionX,positionY].vert=vertB;
				
				}
			
				
		/// <summary>
		/// fais la moyenne des couleur d'un pixel afin de creer des nuances de gris
		/// </summary>
		public void nuanceDeGris()
		{
			for (int i=0;i<Height;i++){
				for(int j=0;j<Width;j++)
				{
					
				
				int nuance=TableauDeCouleur[i,j].rouge+TableauDeCouleur[i,j].vert+TableauDeCouleur[i,j].bleu;
				nuance=nuance/3;
				Byte nuance2=conversion_binaire(nuance);
				TableauDeCouleur[i,j].rouge=nuance2;
				TableauDeCouleur[i,j].vert=nuance2;
				TableauDeCouleur[i,j].bleu=nuance2;
			}
		
			}
					
			
		}
		
		#endregion	
		#region creation
		/// <summary>
		/// fais un tableau de couleur
		/// </summary>
		public void colortab()// creer un tableau des couleurs d'une image en fonction de la position 
		{	
		TableauDeCouleur=new couleur[Height,Width];
		int index=entete+entetebis;
		for (int j=0;j<Height;j++)
		{
			for(int i=0;i<Width;i++)
			    {
				
		
			TableauDeCouleur[j,i]=new couleur(Tableau_de_Byte[index],Tableau_de_Byte[index+1],Tableau_de_Byte[index+2]);
			//TableauDeCouleur[j,i].Display();
			index=index+3;
			}
			}
				}
				
				
			
			
			
			
		/// <summary>
		/// permet de convertir un tableau de byte en entier ou un entier en Byte
		/// </summary>
		/// <param name="tab"></param>
		/// <returns></returns>
					
		public int conversion_binaire(Byte[] tab)
		{
			 int converti = BitConverter.ToInt32(tab, 0);
			 // Console.WriteLine(converti);
			return converti;
		}
		public byte conversion_binaire(int Entier)
		{
			if (Entier > 255)
			{
				Entier = 255;
			}
			if (Entier < 0)
			{
				Entier = 0;
			}
			byte converti = Convert.ToByte(Entier);
	
			return converti;
		}
		public Byte[] conversion_binairetab(int Entier)
		{
			Byte[] converti = BitConverter.GetBytes(Entier);
	
			return converti;
		}
		/// <summary>
		/// recupere la taille de l'image avec un index de départ
		/// </summary>
		/// <param name="tab"></param>
		/// <param name="demarage"></param>
		/// <returns></returns>
		public int recuperer_taille(Byte[] tab,int demarage)//prends en para la ou commence la taille dans le header
		{
			Byte[] tableaubis=new Byte[4];
			for(int j=0;j<4;j++)
				{
				tableaubis[j]=tab[demarage+j];
				//Console.Write(tableaubis[j]+" ");
				}
			return conversion_binaire(tableaubis);
		}
	/// <summary>
	/// permet de construir l'image en créant un tableau de byte
	/// </summary>
	/// <returns></returns>
		public byte[] construction_image()
		{
			Byte[] img=new Byte[(Height*Width*3)+tailledeloffset];
				
	for (int i=0;i<entete+entetebis;i++)
            {
            img[i]=Tableau_de_Byte[i];
            }
	for (int i=0;i<Height;i++)
            {
	
		for(int j=0;j<Width;j++)
		{
			img[entete+entetebis+((j+i*Width)*3)]=TableauDeCouleur[i,j].rouge;
			img[entete+entetebis+((j+i*Width)*3)+1]=TableauDeCouleur[i,j].vert;
			img[entete+entetebis+((j+i*Width)*3)+2]=TableauDeCouleur[i,j].bleu;
			
	}
	}
	return img;
		}
		
		#endregion
		/// <summary>permet d'afficher le header d'une imagee
		/// 
		/// </summary>
		public  void afficher_les_octets()
		{
			//14 premier octets entete fichier 
																//2premier type
																//taille en little indian 
				Console.WriteLine("header");					//40 octet entet images
		for (int i=0;i<entete;i++)
            {
			
            Console.Write("  "+Tableau_de_Byte[i]);
            }	

		//rest image
		Console.WriteLine();
		Console.WriteLine("reste");
	for (int i=entete;i<entete+entetebis;i++)
            {
            Console.Write("  "+Tableau_de_Byte[i]);
            
            }
	Console.WriteLine();
	Console.WriteLine("deuxieme reste");
	for (int i=entetebis+entete;i<Tableau_de_Byte.Length;i++)
            {
            Console.Write(Tableau_de_Byte[i]+"\t");
            }
	}
		/// <summary>
		/// permet de faire une croix d'une certaine couleur dans une image
		/// </summary>
		/// <param name="R"></param>
		/// <param name="G"></param>
		/// <param name="B"></param>
		public void croix(int R,int G, int B)
		{
			int milieux_lageur=Width/2;
			int milieux_longueur=Height/2;
			for(int i=0;i<Height;i++)
			{
				for(int j=0; j<Width;j++)
				{
					if (i==milieux_lageur || j==milieux_longueur)
					{
						change_color(R,G,B,i,j);
					}
				}
			}
		}
		/// <summary>
		/// permet de faire une croix d'epaisseur >1
		/// </summary>
		/// <param name="R"></param>
		/// <param name="G"></param>
		/// <param name="B"></param>
		/// <param name="grosseur"></param>
		public void croix(int R,int G, int B, int grosseur)
		{
			int milieux_lageur=Width/2;
			int milieux_longueur=Height/2;
			for(int i=0;i<Height;i++)
			{
				for(int j=0; j<Width;j++)
				{

					if (i==milieux_lageur||i==milieux_lageur+grosseur||i==milieux_lageur-grosseur || j==milieux_longueur||j==milieux_longueur+grosseur||j==milieux_longueur-grosseur)
					{
						change_color(R,G,B,i,j);
					}
				}
			}
		}
		/// <summary>
		/// permet de faire un entrelacement sur une image de taille variable avec une fond totalement dé-unis
		/// </summary>
		/// <param name="R"></param>
		/// <param name="G"></param>
		/// <param name="B"></param>
		public  void sablier(int R,int G,int B )
		{
			
			int milieux_lageur=Width/2;
			int milieux_longueur=Height/2;
			for(int i=0;i<2;i++)
			{
				for(int k=0; k<Width;k++)
				{
					for(int j=0;j<Height;j++)
					{
						
							change_color(R, G, B, milieux_lageur+(milieux_lageur/2), k );
						if (k == j&& milieux_lageur + (milieux_lageur / 2) - j>=0 )
						{
							change_color(R, G, B, milieux_lageur + (milieux_lageur / 2) - j , k);
							change_color(R, G, B, milieux_lageur + (milieux_lageur / 2) - j, Width -1- k);
						}

						change_color(R, G, B, milieux_lageur - (milieux_lageur / 2), k);
						if (k == j && milieux_lageur - (milieux_lageur / 2) + j <Width)
						{
							change_color(R, G, B, milieux_lageur - (milieux_lageur / 2) + j, k);
							change_color(R, G, B, milieux_lageur - (milieux_lageur / 2) + j, Width - 1 - k);
						}

						






					}
				}
			}
			
		}
			/// <summary>
			/// permet de construire une image avec des parametres de taille existant
			/// </summary>
			/// <param name="Height"></param>
			/// <param name="Width"></param>
			/// <returns></returns>
				public byte[] construction_image(int Height,int Width)
		{
			Byte[] img=new Byte[(Height*Width*3)+tailledeloffset];//todo:  offset à 54 pour le moment
			for (int i=0;i<entete;i++)
            {
				img[i]=Tableau_de_Byte[i];
            }		
	for (int i=entete;i<entete+entetebis;i++)
            {
            img[i]=Tableau_de_Byte[i];
            }
	for (int i=0;i<Height;i++)
            {
	
		for(int j=0;j<Width;j++)
		{
			img[entete+entetebis+((j+i*Width)*3)]=TableauDeCouleur[i,j].rouge;
			img[entete+entetebis+((j+i*Width)*3)+1]=TableauDeCouleur[i,j].vert;
			img[entete+entetebis+((j+i*Width)*3)+2]=TableauDeCouleur[i,j].bleu;
			
	}
	}
	return img;
		}
		/// <summary>
		/// le menue ou l'on peut selectionner quoi faire
		/// </summary>
		public static void menu()
		{
			int choix = 0;
			Console.WriteLine("que voulez vous faire?\n 1-travailler sur une image existante? \n 2-faire une image");
			do
			{
				choix = Int32.Parse(Console.ReadLine());
			} while (choix != 1 && choix != 2);
			if (choix == 1)
			{
				
				Console.WriteLine("vous souhaitez: \n 1-afficher l'entete du fichier?\n 2-faire des nuances de gris\n 3-utiliser une matrice de convolutionpour faire un legere flou \n 4-Decouvrir mon projet personel");
				choix = Int32.Parse(Console.ReadLine());
				if (choix == 1)
				{
					Console.WriteLine("donnez le nom de votre image");
					string nom_image = Console.ReadLine();
					Bitmap image = new Bitmap(nom_image);
					image.afficher_les_octets();
					Console.WriteLine("comment doit s'appeller la nouvelle image?");
					string nouveau = Console.ReadLine();
					System.IO.File.WriteAllBytes(nouveau, image.construction_image());
				}
				if (choix == 2)
				{
					Console.WriteLine("donnez le nom de votre image");
					string nom_image = Console.ReadLine();
					Bitmap image = new Bitmap(nom_image);
					image.nuanceDeGris();
					Console.WriteLine("comment doit s'appeller la nouvelle image?");
					string nouveau = Console.ReadLine();
					System.IO.File.WriteAllBytes(nouveau, image.construction_image());
				}
				if (choix == 3)
				{
					Console.WriteLine("donnez le nom de votre image");
					string nom_image = Console.ReadLine();
					Bitmap image = new Bitmap(nom_image);
					int[,] mat=new int[,]{{0,0,0,0,0},{0,1,1,1,0},{0,1,1,1,0},{0,1,1,1,0},{0,0,0,0,0}};

					image.matrice_de_conv(mat);
					Console.WriteLine("comment doit s'appeller la nouvelle image?");
					string nouveau = Console.ReadLine();
					System.IO.File.WriteAllBytes(nouveau, image.construction_image());
				}
				if (choix == 4)
				{
					
						Console.WriteLine("donnez le nom de votre image");
						string nom_image = Console.ReadLine();
						Bitmap image = new Bitmap(nom_image);
						image.projet_perso();
						Console.WriteLine("comment doit s'appeller la nouvelle image?");
						string nouveau = Console.ReadLine();
						System.IO.File.WriteAllBytes(nouveau, image.construction_image());
					
					
				}
					choix = -1;
			}
			if (choix == 2)
			{
				Console.WriteLine("vous souhaitez:\n 1-un entrelassement\n 2-un cercle");
				choix = Int32.Parse(Console.ReadLine());
				int R = 0;
				int G = 0;
				int B = 0;
				Bitmap image = new Bitmap();
				
					Console.WriteLine(" de quelle couleur doit etre la forme");
				do
				{
					Console.WriteLine("entrez la valeur de la couleur rouge, entre 0 et 255");
					R = Int32.Parse(Console.ReadLine());
				} while (R < 0 || R > 255);
				do
				{
					Console.WriteLine("entrez la valeur de la couleur verte, entre 0 et 255");
					G = Int32.Parse(Console.ReadLine());
				} while (R < 0 || R > 255);
				do
				{
					Console.WriteLine("entrez la valeur de la couleur bleu, entre 0 et 255");
					B = Int32.Parse(Console.ReadLine());
				} while (R < 0 || R > 255);
				if (choix == 1)
				{
					image.sablier(R, G,B);
				}
				if (choix == 2)
				{
					Console.WriteLine("cercle");
					image.Cercle(R, G, B);
				}
				Console.WriteLine("comment doit s'appeller la nouvelle image?");
				string nouveau = Console.ReadLine();
				System.IO.File.WriteAllBytes(nouveau, image.construction_image());
				choix = -1;
			}
			

		}
		public void Cercle(int R,int G,int B)
		{
			for (int i = 0; i < Height; i++)
			{
				for (int j = 0; j < Width; j++)
				{
					Double distance = Math.Sqrt((Math.Pow((i - Height / 2), 2) + Math.Pow((j - Width/2), 2)));
					if (distance< Width / 4)
					{
						TableauDeCouleur[i, j].rouge = conversion_binaire(R);
						TableauDeCouleur[i, j].bleu = conversion_binaire(B);
						TableauDeCouleur[i, j].vert = conversion_binaire(G);
					}
				}
			}
		}
		/// <summary>
		/// mon inovation, consistant à creer une matrice de convolution aléatoire et a l'appliquer à une image
		/// </summary>
		public void projet_perso()
		{
			int[,] matricecon = new int[5, 5];
			Random rnd = new Random();
			for (int i = 0; i < matricecon.GetLength(0); i++)
			{
				for (int j = 0; j < matricecon.GetLength(0); j++)
				{
					
					matricecon[j, i] = rnd.Next(-1, 5);
				}
			}
		
			matrice_de_conv(matricecon);
		}
		/// <summary>
		/// applique un effet en utilisant une matrice de convolution
		/// </summary>
		/// <param name="mat"></param>
			public void matrice_de_conv(int[,] mat)
			{
				if (mat.GetLength(0)!=5 || mat.GetLength(1)!=5)
				{
					
				}
				else{
					couleur[,] tableauDeCouleurBis= new couleur[TableauDeCouleur.GetLength(0),TableauDeCouleur.GetLength(1)];
				
				for (int i=0; i<Width;i++)
				{
					for (int j=0; j<Height;j++)
					{
						int resultat_de_lapplication_mat_r=0;
						int resultat_de_lapplication_mat_v=0;
						int resultat_de_lapplication_mat_b=0;
						tableauDeCouleurBis[i,j]=new couleur(0,0,0);
						int compteur=0;
						for(int h=0;h<mat.GetLength(1);h++)
						{
							for(int k=0;k<mat.GetLength(0);k++)
							{
								if (mat[h,k]!=0)
								{
									
									resultat_de_lapplication_mat_r+=mat[h,k]*TableauDeCouleur[i,j].rouge;
								
							
								resultat_de_lapplication_mat_v+=mat[h,k]*TableauDeCouleur[i,j].vert;
								
							
									compteur++;
								resultat_de_lapplication_mat_b+=mat[h,k]*TableauDeCouleur[i,j].bleu;
								}
							}
						}
						byte resultat_de_lapplication_mat_r2=conversion_binaire(resultat_de_lapplication_mat_r/compteur);
						byte resultat_de_lapplication_mat_v2=conversion_binaire(resultat_de_lapplication_mat_v/compteur);
						byte resultat_de_lapplication_mat_b2=conversion_binaire(resultat_de_lapplication_mat_b/compteur);
						
						tableauDeCouleurBis[i,j]=new couleur(resultat_de_lapplication_mat_r2,resultat_de_lapplication_mat_v2,resultat_de_lapplication_mat_b2);
					}
				}
				TableauDeCouleur=tableauDeCouleurBis;
				}
				
			}
			

	}}
