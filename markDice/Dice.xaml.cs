using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using System.Globalization;

namespace markDice
{
	public partial class Dice : UserControl
	{	
        
		public Dice()
		{	
			//essa parada aki é para poder dar os Bind no XAML            
            this.DataContext = this;
            InitializeComponent();            
		}

        private string idDado;
        private ImageSource ImgFrenteImg;
        private ImageSource ImgCimaImg;
        private ImageSource ImgDireitaImg;
        private ImageSource ImgBaixoImg;
        private ImageSource ImgTrasImg;
        private ImageSource ImgEsquerdaImg;
        private bool podeMexer = true;

        public string IdDado 
        {
            get { return idDado; }
            set { idDado = value; }
        }
        public ImageSource imgFrenteImg
        {
            get { return ImgFrenteImg; }
            set { ImgFrenteImg = value; }
        }
        public ImageSource imgCimaImg
        {
            get { return ImgCimaImg; }
            set { ImgCimaImg = value; }
            
        }		
		public ImageSource imgDireitaImg
		{
            get { return ImgDireitaImg; }
            set { ImgDireitaImg = value; }		    
		}
        public ImageSource imgBaixoImg
        {
            get { return ImgBaixoImg; }
            set { ImgBaixoImg = value; }
        }
        public ImageSource imgTrasImg
        {
            get { return ImgTrasImg; }
            set { ImgTrasImg = value; }

        }
        public ImageSource imgEsquerdaImg
        {
            get { return ImgEsquerdaImg; }
            set { ImgEsquerdaImg = value; }
        }

        private ImageSource imgCimaSorteada;
        private ImageSource imgFrenteSorteada;
        private ImageSource imgLadoSorteada;

        public ImageSource ImgCimaSorteada
        {
            get { return imgCimaSorteada; }
            set { imgCimaSorteada = value; }
        }
        public ImageSource ImgFrenteSorteada
        {
            get { return imgFrenteSorteada; }
            set { imgFrenteSorteada = value; }
        }
        public ImageSource ImgLadoSorteada
        {
            get { return imgLadoSorteada; }
            set { imgLadoSorteada = value; }
        }

        double startingPositionOfImageX;
        double startingPositionOfImageY;
        double deltaPositionOfImageX;
        double deltaPositionOfImageY;

        private void canvas_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            startingPositionOfImageX = Trans.X;
            startingPositionOfImageY = Trans.Y;
        }

        private void canvas_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            if (e.OriginalSource == myImg && podeMexer)
            {
             
            Point translation =  e.DeltaManipulation.Translation;
            
            deltaPositionOfImageX += e.DeltaManipulation.Translation.X;
            deltaPositionOfImageY += e.DeltaManipulation.Translation.Y;

            Trans.X = deltaPositionOfImageX + startingPositionOfImageX;
            Trans.Y = deltaPositionOfImageY + startingPositionOfImageY;
             
            }
        }

        private void canvas_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            deltaPositionOfImageX = 0;
            deltaPositionOfImageY = 0;
        }

        public void tirarEventoManipulacao()
        {
            this.podeMexer = false;
        }

        public void rodar()
        {            
            Random rmd = new Random();
            int sorteadoCima = rmd.Next(1, 7);

            switch (sorteadoCima)
            {
                case 1:
                    ImgCimaSorteada = imgCimaImg;
                    ImgFrenteSorteada = imgFrenteImg;
                    ImgLadoSorteada = imgDireitaImg;
                    break;
                case 2:
                    ImgCimaSorteada = imgEsquerdaImg;
                    ImgFrenteSorteada = imgCimaImg;
                    ImgLadoSorteada = imgTrasImg;
                    break;
                case 3:
                    ImgCimaSorteada = imgFrenteImg;
                    ImgFrenteSorteada = imgBaixoImg;
                    ImgLadoSorteada = imgDireitaImg;
                    break;
                case 4:
                    ImgCimaSorteada = imgDireitaImg;
                    ImgFrenteSorteada = imgFrenteImg;
                    ImgLadoSorteada = imgBaixoImg;
                    break;
                case 5:
                    ImgCimaSorteada = imgBaixoImg;
                    ImgFrenteSorteada = imgTrasImg;
                    ImgLadoSorteada = imgDireitaImg;
                    break;
                case 6:
                    ImgCimaSorteada = imgTrasImg;
                    ImgFrenteSorteada = imgEsquerdaImg;
                    ImgLadoSorteada = imgFrenteImg;
                    break;
                default:
                    break;
            }
        }

        public void iniciar()
        {
            ImgCimaSorteada = imgCimaImg;
            ImgFrenteSorteada = imgFrenteImg;
            ImgLadoSorteada = imgDireitaImg;
        }
	}
}