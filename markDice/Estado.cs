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
using System.Collections.Generic;

namespace markDice
{
    public class Estado
    {
        List<DiceEntity> dados = new List<DiceEntity>();
        List<byte[]> list3LastPics = new List<byte[]>();

        public List<DiceEntity> Dados
        {
            get { return dados; }
            set { dados = value; }
        }

        public List<byte[]> List3LastPics
        {
            get { return list3LastPics; }
            set { list3LastPics = value; }
        }

        public void addDado(DiceEntity dado)
        {
            dado.IdDado = DateTime.Now.Ticks.ToString();
            Dados.Add(dado);
            saveState();
        }

        public void addLast3pics(byte[] source)
        {
            if (!List3LastPics.Contains(source))
            {
                //VOLTA PARA O INICIO
                list3LastPics.Reverse();
                //POE NO FINAL
                List3LastPics.Add(source);
                //INVERTE PARA certo e apagar o ultimo
                list3LastPics.Reverse();

                if (List3LastPics.Count > 3)
                    List3LastPics.RemoveAt(3);
            }
            saveState();
        }

        private void saveState()
        {
            IsolatedStorageHelper.SaveObject("DiceState", this.MemberwiseClone());
        }

        public void loadState()
        {
            Estado diceState = IsolatedStorageHelper.GetObject<Estado>("DiceState");
            if (diceState != null)
            {
                Dados = diceState.Dados;
                List3LastPics = diceState.List3LastPics;
            }

        }

        internal void editDado(DiceEntity dadoEditado)
        {
            foreach (DiceEntity dado in Dados)
            {
                if (dado.IdDado == dadoEditado.IdDado)
                {
                    dado.ImgBaixo = dadoEditado.ImgBaixo;
                    dado.ImgCima = dadoEditado.ImgCima;
                    dado.ImgDireita = dadoEditado.ImgDireita;
                    dado.ImgEsquerda = dadoEditado.ImgEsquerda;
                    dado.ImgFrente = dadoEditado.ImgFrente;
                    dado.ImgTras = dadoEditado.ImgTras;
                    break;
                }
            }
            saveState();
        }
    }
}
