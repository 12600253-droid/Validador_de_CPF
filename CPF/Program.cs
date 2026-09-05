using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPF
{
    internal class Program
    {
        static void Main(string[] args)
        { // Artur Naves
            
            Console.WriteLine("=================================================");
            Console.WriteLine("       VALIDADOR DE DOCUMENTOS (COTEMIG)        ");
            Console.WriteLine("=================================================");
            Console.WriteLine("Nome participante: Artur Naves");
            Console.WriteLine("--------------------------------------------------");
            Console.Write("Digite o número do CPF (apenas algarismos): ");
            string cpfTexto = Console.ReadLine();

            if (string.IsNullOrEmpty(cpfTexto))
            {
                Console.WriteLine("Erro: O campo não pode estar vazio.");
                return;
            }

            bool temTamanhoCerto = (cpfTexto.Length == 11);
            bool ehSequenciaInvalida = (cpfTexto == new string(cpfTexto[0], 11));

            if (!temTamanhoCerto || ehSequenciaInvalida)
            {
                ExibirResultado(cpfTexto, "INVÁLIDO");
                Console.ReadKey();
                return;
            }

            int[] digitos = new int[11];
            for (int i = 0; i < 11; i++)
            {
   
                digitos[i] = int.Parse(cpfTexto[i].ToString());
            }

         
            int soma1 = 0;
            int multiplicador1 = 10;
            for (int i = 0; i < 9; i++)
            {
                soma1 += digitos[i] * multiplicador1;
                multiplicador1--;
            }
            int resto1 = (soma1 * 10) % 11;
            int resultadoDigito1 = (resto1 == 10) ? 0 : resto1;

    
            int soma2 = 0;
            int multiplicador2 = 11;
            for (int i = 0; i < 10; i++)
            {
                soma2 += digitos[i] * multiplicador2;
                multiplicador2--;
            }
            int resto2 = (soma2 * 10) % 11;
            int resultadoDigito2 = (resto2 == 10) ? 0 : resto2;

      
            bool primeiroOk = (resultadoDigito1 == digitos[9]);
            bool segundoOk = (resultadoDigito2 == digitos[10]);

            if (primeiroOk && segundoOk)
            {
                string regiao = ObterRegiaoFiscal(digitos[8]);
                ExibirResultado(cpfTexto, "VÁLIDO", regiao);
            }
            else
            {
                ExibirResultado(cpfTexto, "INVÁLIDO (Dígitos verificadores não conferem)");
            }

         
            Console.WriteLine("\nPressione qualquer tecla para encerrar...");
            Console.ReadKey();
        }

      
        static string ObterRegiaoFiscal(int nonoDigito)
        {
            switch (nonoDigito)
            {
                case 1: return "1ª Região (DF, GO, MS, MT, TO)";
                case 2: return "2ª Região (AC, AM, AP, PA, RO, RR)";
                case 3: return "3ª Região (CE, MA, PI)";
                case 4: return "4ª Região (AL, PB, PE, RN)";
                case 5: return "5ª Região (BA, SE)";
                case 6: return "6ª Região (Minas Gerais)";
                case 7: return "7ª Região (ES, RJ)";
                case 8: return "8ª Região (SP)";
                case 9: return "9ª Região (PR, SC)";
                case 0: return "10ª Região (RS)";
                default: return "Desconhecida";
            }
        }

     
        static void ExibirResultado(string doc, string validacao, string regiao = "---")
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"DOCUMENTO: {doc}");
            Console.WriteLine("TIPO: CPF");
            Console.WriteLine($"VALIDAÇÃO: {validacao}");

            if (validacao == "VÁLIDO")
            {
                Console.WriteLine($"REGIÃO FISCAL: {regiao}");
            }

            Console.WriteLine("--------------------------------------------------");
        }
    }
}