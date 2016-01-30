using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.IO;

namespace markDice
{   
    public class DiceEntity
    {
        private string idDado;
        private byte[] imgFrente;
        private byte[] imgTras;
        private byte[] imgDireita;
        private byte[] imgEsquerda;
        private byte[] imgCima;
        private byte[] imgBaixo;

        public string IdDado
        {
            get { return idDado; }
            set { idDado = value; }
        }
        public byte[] ImgFrente
        {
            get { return imgFrente; }
            set { imgFrente = value; }
        }
        public byte[] ImgTras
        {
            get { return imgTras; }
            set { imgTras = value; }
        }
        public byte[] ImgDireita
        {
            get { return imgDireita; }
            set { imgDireita = value; }
        }
        public byte[] ImgEsquerda
        {
            get { return imgEsquerda; }
            set { imgEsquerda = value; }
        }
        public byte[] ImgCima
        {
            get { return imgCima; }
            set { imgCima = value; }
        }
        public byte[] ImgBaixo
        {
            get { return imgBaixo; }
            set { imgBaixo = value; }
        }

        public ImageSource getImgFrente()
        {
            return Util.getImgFromByte(ImgFrente);
        }
        public ImageSource getImgTras()
        {
            return Util.getImgFromByte(ImgTras);
        }
        public ImageSource getImgDireita()
        {
            return Util.getImgFromByte(ImgDireita);
        }
        public ImageSource getImgEsquerda()
        {
            return Util.getImgFromByte(ImgEsquerda);
        }
        public ImageSource getImgCima()
        {
            return Util.getImgFromByte(ImgCima);
        }
        public ImageSource getImgBaixo()
        {
            return Util.getImgFromByte(ImgBaixo);
        }

        public void setImgFrente(Image img)
        {
            ImgFrente = Util.toByte(img);
        }
        public void setImgTras(Image img)
        {
            ImgTras = Util.toByte(img);
        }
        public void setImgDireita(Image img)
        {
            ImgDireita = Util.toByte(img);
        }
        public void setImgEsquerda(Image img)
        {
            ImgEsquerda = Util.toByte(img);
        }
        public void setImgCima(Image img)
        {
            ImgCima = Util.toByte(img);
        }
        public void setImgBaixo(Image img)
        {
            ImgBaixo = Util.toByte(img);
        }
    }
}
