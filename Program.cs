using FuncionárioComArquivo.Funcionario;
using System;
using System.Globalization;

namespace FuncionarioComArquivo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Digite o caminho do arquivo: ");
            string caminhoArquivo = Console.ReadLine();
            
            List<Funcionario> funcionarios = new List<Funcionario>();

            try{
                using (StreamReader sr = new StreamReader(caminhoArquivo))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] dados = sr.ReadLine().Split(',');
                        string nome = dados[0];
                        string email = dados[1];
                        double salario = double.Parse(dados[2], CultureInfo.InvariantCulture);
                        funcionarios.Add(new Funcionario(nome, email, salario));
                    }
                }

                Console.Write("Entre com o salario: ");
                double salarioFuncionario = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                var emailFuncionario = funcionarios.Where(p => p.Salario > salarioFuncionario)
                    .OrderBy(p => p.Email).Select(p => p.Email);
                Console.WriteLine("Email das pessoas que o salario é maior que 2000.00: ");
                foreach (string email in emailFuncionario)
                {
                    Console.WriteLine(email);
                }

                var funcionarioM = funcionarios.Where(p => p.Nome[0] == 'M').Sum(p => p.Salario);
                Console.Write("Soma do salario de pessoas que o nome começa com a letra M: " + 
                    funcionarioM.ToString("F2", CultureInfo.InvariantCulture));
            }
            catch(IOException ex)
            {
                Console.WriteLine("Ocorreu um erro");
                Console.WriteLine(ex.Message);
            }
        }
    }
}