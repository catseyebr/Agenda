﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda
{
    public class ManipuladorArquivos
    {
        private static string EnderecoArquivo = AppDomain.CurrentDomain.BaseDirectory + "contatos.txt";

        public static List<Contato> LerArquivo()
        {
            List<Contato> contatosList = new List<Contato>();
            if (File.Exists(EnderecoArquivo))
            {
                using (StreamReader sr = File.OpenText(EnderecoArquivo))
                {
                    while (sr.Peek() >= 0)
                    {
                        string linha = sr.ReadLine();
                        string[] linhaComSplit = linha.Split(';');
                        if(linhaComSplit.Count() == 3)
                        {
                            Contato contato = new Contato(linhaComSplit[0],linhaComSplit[1],linhaComSplit[2]);
                            contatosList.Add(contato);
                        }
                    }
                }
            }
            else
            {

            }
            return contatosList;
        }

        public static void EscreverArquivo(List<Contato> contatosList)
        {
            using (StreamWriter sw = new StreamWriter(EnderecoArquivo, false))
            {
                foreach (Contato contato in contatosList)
                {
                    string linha = string.Format("{0};{1};{2}", contato.Nome, contato.Email, contato.NumeroTelefone);
                    sw.WriteLine(linha);
                }
                sw.Flush();
            }
        }
    }
}
