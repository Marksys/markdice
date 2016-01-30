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
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.IO;

namespace markDice
{
    public partial class CreateDice : PhoneApplicationPage
    {
        // Constructor
        public CreateDice()
        {
            InitializeComponent();
            estado.loadState();
            addCreatedDices();
            this.set3LastPics();
        }

        public int imgLastClicked = 0;
        public Estado estado = new Estado();        
        private string idDadoEditado;

        private void image2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.mudaImagem(image2, false);
        }

        private void image3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.mudaImagem(image3, false);
        }

        private void image4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.mudaImagem(image4, false);
        }

        public void mudaImagem(Image fonte, bool mudar3LastPics)
        {
            switch (imgLastClicked)
            {
                case 1:
                    imageCima.Source = fonte.Source;
                    break;
                case 2:
                    imageEsquerda.Source = fonte.Source;
                    break;
                case 3:
                    imageFrente.Source = fonte.Source;
                    break;
                case 4:
                    imageDireita.Source = fonte.Source;
                    break;
                case 5:
                    imageBaixo.Source = fonte.Source;
                    break;
                case 6:
                    imageTras.Source = fonte.Source;
                    break;
                default:
                    break;
            }

            imgLastClicked = 0;
            canvas1.Visibility = Visibility.Collapsed;

            if (mudar3LastPics)
            {
                estado.addLast3pics(Util.toByte(fonte));
                this.set3LastPics();
            }
        }

        private void imaged1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            canvas1.Visibility = Visibility.Visible;
            imgLastClicked = 1;
        }

        private void imaged2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            canvas1.Visibility = Visibility.Visible;
            imgLastClicked = 2;
        }

        private void imaged3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            canvas1.Visibility = Visibility.Visible;
            imgLastClicked = 3;
        }

        private void imaged4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            canvas1.Visibility = Visibility.Visible;
            imgLastClicked = 4;
        }

        private void imaged5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            canvas1.Visibility = Visibility.Visible;
            imgLastClicked = 5;
        }

        private void imaged6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            canvas1.Visibility = Visibility.Visible;
            imgLastClicked = 6;
        }

        //Gallery
        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            //Gallery
            button1.IsEnabled = false;
            button4.IsEnabled = false;

            PhotoChooserTask photo = new PhotoChooserTask();
            photo.ShowCamera = true;            
            photo.Show();           

            photo.Completed += new EventHandler<PhotoResult>(photo_Completed);
        }

        void photo_Completed(object sender, PhotoResult e)
        {
            BitmapImage image = new BitmapImage();
            if (e.ChosenPhoto != null)
            {
                image.SetSource(e.ChosenPhoto);

                Image img = new Image();
                img.Source = image;
                mudaImagem(img, true);                
            }
            button1.IsEnabled = true;
            button4.IsEnabled = true;
        }

        //Save Button
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            button2.IsEnabled = false;
            //Save Button
            DiceEntity novoDado = new DiceEntity();
            novoDado.ImgCima = Util.toByte(imageCima);
            novoDado.ImgEsquerda = Util.toByte(imageEsquerda);
            novoDado.ImgFrente = Util.toByte(imageFrente);
            novoDado.ImgDireita = Util.toByte(imageDireita);
            novoDado.ImgBaixo = Util.toByte(imageBaixo);
            novoDado.ImgTras = Util.toByte(imageTras);
            if (idDadoEditado != null)
            {
                novoDado.IdDado = idDadoEditado;
                estado.editDado(novoDado);
            }
            else
            {
                estado.addDado(novoDado);
                idDadoEditado = null;
            }
            
            this.NavigationService.GoBack();
        }

        //Back do canvas SelectImages
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            //Back
            canvas1.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void set3LastPics()
        {
            if (estado.List3LastPics != null)
            {
                int i = 0;
                foreach (byte[] imgSource in estado.List3LastPics)
                {
                    if (i == 0)
                        image2.Source = Util.getImgFromByte(imgSource);
                    if (i == 1)
                        image3.Source = Util.getImgFromByte(imgSource);
                    if (i == 2)
                        image4.Source = Util.getImgFromByte(imgSource);

                    i++;
                }
            }
        }

        //Back Canvas MyDices
        private void button2_Click_1(object sender, RoutedEventArgs e)
        {
            PanelExample.Visibility = System.Windows.Visibility.Collapsed;
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

        private void diceExample_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Dice diceOrigem = ((Dice)(sender));

            Dice newDice = new Dice();

            imageCima.Source = diceOrigem.imgCimaImg;
            imageFrente.Source = diceOrigem.imgFrenteImg;
            imageDireita.Source = diceOrigem.imgDireitaImg;
            imageBaixo.Source = diceOrigem.imgBaixoImg;
            imageTras.Source = diceOrigem.imgTrasImg;
            imageEsquerda.Source = diceOrigem.imgEsquerdaImg;
            idDadoEditado = diceOrigem.IdDado;
            PanelExample.Visibility = System.Windows.Visibility.Collapsed;
        }

        //Load Button
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            PanelExample.Visibility = System.Windows.Visibility.Visible;
        }
    }
}