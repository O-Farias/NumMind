using System;

partial class Program
{
    static void Main()
    {
        bool sair = false;
        while (!sair)
        {
            ExibirTitulo();
            ExibirMenu();

            string? opcao = Console.ReadLine();
            Console.Clear();

            switch (opcao)
            {
                case "1":
                    JogarNumMind();
                    break;
                case "2":
                    ExibirInstrucoes();
                    break;
                case "3":
                    sair = true;
                    break;
                default:
                    ExibirMensagemErro("Opção inválida!");
                    break;
            }
        }
    }

    static void ExibirTitulo()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(@"
    _   __              __  ___           __
   / | / /_  ______ ___/  |/  /___ ___  / /
  /  |/ / / / / __ `__/ /|_/ / __ `__ \/ / 
 / /|  / /_/ / / / / / /  / / / / / / /_/  
/_/ |_/\__,_/_/ /_/ /_/  /_/_/ /_/ /_(_)   
        ");
        Console.ResetColor();
    }

    static void ExibirMenu()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n=== Menu Principal ===");
        Console.WriteLine("1. Jogar");
        Console.WriteLine("2. Instruções");
        Console.WriteLine("3. Sair");
        Console.Write("\nEscolha uma opção: ");
        Console.ResetColor();
    }

    static void ExibirMensagemErro(string mensagem)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n⚠️ {mensagem}");
        Console.ResetColor();
        Thread.Sleep(1500);
    }

    static void ExibirInstrucoes()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n=== Instruções ===");
        Console.WriteLine("1. O computador escolherá um número entre 1 e 100");
        Console.WriteLine("2. Tente adivinhar o número!");
        Console.WriteLine("3. Você receberá dicas se o número é maior ou menor");
        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ResetColor();
        Console.ReadKey();
    }

    static void JogarNumMind()
    {
        Random random = new Random();
        int numeroSecreto = random.Next(1, 101);
        int tentativa = 0;
        int palpite;
        bool acertou = false;

        Console.Clear();
        Console.WriteLine("🎯 Novo Jogo Iniciado!");
        Console.WriteLine("Tente adivinhar o número entre 1 e 100.");

        while (!acertou)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nDigite seu palpite: ");
            string? entrada = Console.ReadLine();

            if (entrada != null && int.TryParse(entrada, out palpite))
            {
                tentativa++;

                if (palpite < numeroSecreto)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("🔺 O número é maior! Tente novamente.");
                }
                else if (palpite > numeroSecreto)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("🔻 O número é menor! Tente novamente.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"🎉 Parabéns! Você acertou em {tentativa} tentativas.");
                    acertou = true;
                    Thread.Sleep(2000);
                }
            }
            else
            {
                ExibirMensagemErro("Entrada inválida! Digite um número.");
            }
            Console.ResetColor();
        }
    }
}