using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Marketplace;

namespace markDice
{
    public partial class CubeTest : PhoneApplicationPage
    {
        Estado estado = new Estado();
        List<Dice> listaDice = new List<Dice>();
        List<Dice> listaDiceNova = new List<Dice>();
        List<Dice> listaDiceTemplate = new List<Dice>();
        int activePanel = 0;
        LicenseInformation linformation = new LicenseInformation();

        public CubeTest()
        {
            InitializeComponent();
            estado.loadState();
            createTemplates();
            addTemplateDices();
            addCreatedDices();
            buttonRoll.IsEnabled = false;

        }

        //roll
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {       

            buttonAdd.IsEnabled = false;
            buttonRoll.IsEnabled = false;

            int i = 0;

            listaDiceNova = new List<Dice>();

            foreach (Dice diceNow in listaDice)
            {
                if (linformation.IsTrial() && diceNow.IdDado != "1")
                {
                    MessageBox.Show("Sorry, only the full version can roll created dices! Please buy it!");                    
                    this.NavigationService.GoBack();
                }
                else
                {
                    sortear(diceNow);
                    i++;
                }
            }

            listaDice = listaDiceNova;
            addSorteados();

            if (listaDiceNova.Count > 0)
                listaDiceNova[0].StoryboardMalaco.Completed += new EventHandler(storyboardMalaco_Completed);
        }

        //add panel myDices
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (activePanel == 0)
            {
                PanelExample.Visibility = System.Windows.Visibility.Collapsed;
                PanelTemplate.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                PanelTemplate.Visibility = System.Windows.Visibility.Collapsed;
                PanelExample.Visibility = System.Windows.Visibility.Visible;
            }
        }

        //back panel myDices e Template
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            PanelExample.Visibility = System.Windows.Visibility.Collapsed;
            PanelTemplate.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void diceExample_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Dice diceOrigem = ((Dice)(sender));

            Dice newDice = new Dice();

            newDice.imgCimaImg = diceOrigem.imgCimaImg;
            newDice.imgFrenteImg = diceOrigem.imgFrenteImg;
            newDice.imgDireitaImg = diceOrigem.imgDireitaImg;
            newDice.imgEsquerdaImg = diceOrigem.imgEsquerdaImg;
            newDice.imgBaixoImg = diceOrigem.imgBaixoImg;
            newDice.imgTrasImg = diceOrigem.imgTrasImg;
            newDice.IdDado = diceOrigem.IdDado;

            newDice.iniciar();
            listaDice.Add(newDice);
            ContentPanel.Children.Add(newDice);

            PanelExample.Visibility = System.Windows.Visibility.Collapsed;
            PanelTemplate.Visibility = System.Windows.Visibility.Collapsed;
            Canvas.SetZIndex(PanelExample, Canvas.GetZIndex(newDice) + 1);
            Canvas.SetZIndex(PanelTemplate, Canvas.GetZIndex(newDice) + 1);

            buttonRoll.IsEnabled = true;
        }

        private void addCreatedDices()
        {
            Dice newDice;
            int i = 1;
            int altura = 0;
            foreach (DiceEntity dado in estado.Dados)
            {
                newDice = new Dice();
                newDice.MouseLeftButtonDown += new MouseButtonEventHandler(diceExample_MouseLeftButtonDown);

                newDice.imgCimaImg = Util.getImgFromByte(dado.ImgCima);
                newDice.imgDireitaImg = Util.getImgFromByte(dado.ImgDireita);
                newDice.imgFrenteImg = Util.getImgFromByte(dado.ImgFrente);
                newDice.imgBaixoImg = Util.getImgFromByte(dado.ImgBaixo);
                newDice.imgEsquerdaImg = Util.getImgFromByte(dado.ImgEsquerda);
                newDice.imgTrasImg = Util.getImgFromByte(dado.ImgTras);
                newDice.IdDado = dado.IdDado;

                //ESQUERDA
                if ((i % 2) != 0)
                    newDice.Margin = new Thickness(10, 10 + altura, 0, 0);
                //DIREITA
                else
                {
                    newDice.Margin = new Thickness(180, 10 + altura, 0, 0);
                    altura += 160;
                    panelInterno.Height += double.Parse("160");
                }

                newDice.tirarEventoManipulacao();
                newDice.iniciar();
                panelInterno.Children.Add(newDice);

                i++;
            }
        }

        private void addTemplateDices()
        {
            Dice newDice;
            int i = 1;
            int altura = 0;
            foreach (Dice dado in listaDiceTemplate)
            {
                newDice = new Dice();
                newDice.MouseLeftButtonDown += new MouseButtonEventHandler(diceExample_MouseLeftButtonDown);

                newDice.imgCimaImg = dado.imgCimaImg;
                newDice.imgDireitaImg = dado.imgDireitaImg;
                newDice.imgFrenteImg = dado.imgFrenteImg;
                newDice.imgBaixoImg = dado.imgBaixoImg;
                newDice.imgEsquerdaImg = dado.imgEsquerdaImg;
                newDice.imgTrasImg = dado.imgTrasImg;
                newDice.IdDado = dado.IdDado;

                //ESQUERDA
                if ((i % 2) != 0)
                    newDice.Margin = new Thickness(10, 10 + altura, 0, 0);
                //DIREITA
                else
                {
                    newDice.Margin = new Thickness(180, 10 + altura, 0, 0);
                    altura += 160;
                    panelInterno.Height += double.Parse("160");
                }

                newDice.tirarEventoManipulacao();
                newDice.iniciar();
                panelInternoTemplate.Children.Add(newDice);

                i++;
            }
        }

        private void addSorteados()
        {
            panelInternoSorteado.Children.Clear();

            Image imageSorteado;

            int i = 1;
            int altura = 0;
            panelInternoSorteado.Height = double.Parse("140");

            foreach (Dice dado in listaDiceNova)
            {
                imageSorteado = new Image();
                imageSorteado.Source = dado.ImgCimaSorteada;

                imageSorteado.Width = double.Parse("150");
                imageSorteado.Height = double.Parse("150");

                //ESQUERDA
                if ((i % 2) != 0)
                    imageSorteado.Margin = new Thickness(40, 10 + altura, 0, 0);
                //DIREITA
                else
                {
                    imageSorteado.Margin = new Thickness(220, 10 + altura, 0, 0);
                    altura += 180;

                    if (i != listaDiceNova.Count)
                        panelInternoSorteado.Height += double.Parse("180");
                }

                panelInternoSorteado.Children.Add(imageSorteado);

                i++;
            }
        }

        private void sortear(Dice diceRoda)
        {
            Dice diceSorteado = new Dice();
            diceSorteado.imgCimaImg = diceRoda.imgCimaImg;
            diceSorteado.imgFrenteImg = diceRoda.imgFrenteImg;
            diceSorteado.imgDireitaImg = diceRoda.imgDireitaImg;
            diceSorteado.imgEsquerdaImg = diceRoda.imgEsquerdaImg;
            diceSorteado.imgBaixoImg = diceRoda.imgBaixoImg;
            diceSorteado.imgTrasImg = diceRoda.imgTrasImg;
            diceSorteado.IdDado = diceRoda.IdDado;

            diceSorteado.InitializeComponent();
            diceSorteado.rodar();
            diceSorteado.LayoutRoot = diceRoda.LayoutRoot;
            diceSorteado.Trans.X = diceRoda.Trans.X;
            diceSorteado.Trans.Y = diceRoda.Trans.Y;

            listaDiceNova.Add(diceSorteado);

            ContentPanel.Children.Add(diceSorteado);

            Random rdm = new Random();

            diceSorteado.StoryboardMalaco.Begin();

            //ver como dar um dispose na parada ao inves de deixar inivisivel            
            diceRoda.canvas.Visibility = System.Windows.Visibility.Collapsed;
            ExtendedGC.Collect(diceRoda);
        }

        private void btnBackSorteado_Click(object sender, RoutedEventArgs e)
        {
            PanelSorteado.Visibility = System.Windows.Visibility.Collapsed;
        }

        void storyboardMalaco_Completed(object sender, EventArgs e)
        {
            PanelSorteado.Visibility = System.Windows.Visibility.Visible;
            buttonAdd.IsEnabled = true;
            buttonRoll.IsEnabled = true;
        }

        private void createTemplates()
        {
            //Dado Normal
            Dice dadoNormal = new Dice();
            dadoNormal.imgCimaImg = new BitmapImage(new Uri("Images/Normal1.jpg",UriKind.Relative));
            dadoNormal.imgEsquerdaImg = new BitmapImage(new Uri("Images/Normal2.jpg", UriKind.Relative));
            dadoNormal.imgFrenteImg = new BitmapImage(new Uri("Images/Normal3.jpg", UriKind.Relative));
            dadoNormal.imgDireitaImg = new BitmapImage(new Uri("Images/Normal4.jpg", UriKind.Relative));
            dadoNormal.imgBaixoImg =new BitmapImage(new Uri("Images/Normal5.jpg", UriKind.Relative));
            dadoNormal.imgTrasImg = new BitmapImage(new Uri("Images/Normal6.jpg", UriKind.Relative));
            dadoNormal.IdDado = "1";            

            //Dado Numero
            Dice dadoNumero = new Dice();
            dadoNumero.imgCimaImg = new BitmapImage(new Uri("Images/numero1.jpg", UriKind.Relative));
            dadoNumero.imgEsquerdaImg = new BitmapImage(new Uri("Images/numero2.jpg", UriKind.Relative));
            dadoNumero.imgFrenteImg = new BitmapImage(new Uri("Images/numero3.jpg", UriKind.Relative));
            dadoNumero.imgDireitaImg =new BitmapImage(new Uri("Images/numero4.jpg", UriKind.Relative));
            dadoNumero.imgBaixoImg = new BitmapImage(new Uri("Images/numero5.jpg", UriKind.Relative));
            dadoNumero.imgTrasImg = new BitmapImage(new Uri("Images/numero6.jpg", UriKind.Relative));
            dadoNumero.IdDado = "1";            

            //Dado action
            Dice dadoAction = new Dice();
            dadoAction.imgCimaImg = new BitmapImage(new Uri("Images/action1.jpg", UriKind.Relative));
            dadoAction.imgEsquerdaImg = new BitmapImage(new Uri("Images/action2.jpg", UriKind.Relative));
            dadoAction.imgFrenteImg = new BitmapImage(new Uri("Images/action3.jpg", UriKind.Relative));
            dadoAction.imgDireitaImg = new BitmapImage(new Uri("Images/action4.jpg", UriKind.Relative));
            dadoAction.imgBaixoImg = new BitmapImage(new Uri("Images/action5.jpg", UriKind.Relative));
            dadoAction.imgTrasImg = new BitmapImage(new Uri("Images/action6.jpg", UriKind.Relative));
            dadoAction.IdDado = "1";            

            //Dado body
            Dice dadoBody = new Dice();
            dadoBody.imgCimaImg = new BitmapImage(new Uri("Images/body1.jpg", UriKind.Relative));
            dadoBody.imgEsquerdaImg = new BitmapImage(new Uri("Images/body2.jpg", UriKind.Relative));
            dadoBody.imgFrenteImg = new BitmapImage(new Uri("Images/body3.jpg", UriKind.Relative));
            dadoBody.imgDireitaImg = new BitmapImage(new Uri("Images/body4.jpg", UriKind.Relative));
            dadoBody.imgBaixoImg = new BitmapImage(new Uri("Images/body5.jpg", UriKind.Relative));
            dadoBody.imgTrasImg = new BitmapImage(new Uri("Images/body6.jpg", UriKind.Relative));
            dadoBody.IdDado = "1";            
            
            listaDiceTemplate.Add(dadoNormal);
            listaDiceTemplate.Add(dadoNumero);
            listaDiceTemplate.Add(dadoAction);
            listaDiceTemplate.Add(dadoBody);

        }

        private void btnMyDicesTemplate_Click(object sender, RoutedEventArgs e)
        {
            PanelExample.Visibility = System.Windows.Visibility.Visible;
            PanelTemplate.Visibility = System.Windows.Visibility.Collapsed;
            activePanel = 1;
        }

        private void btnTemplateTemplate_Click(object sender, RoutedEventArgs e)
        {
            PanelExample.Visibility = System.Windows.Visibility.Collapsed;
            PanelTemplate.Visibility = System.Windows.Visibility.Visible;
            activePanel = 0;
        }
    }
}
