using System;
using System.Collections.Generic;
using System.Threading;

partial class Program
{
    enum Dificuldade
    {
        Facil,
        Medio,
        Dificil
    }

    enum ModoJogo
    {
        Normal,
        ContraRelogio,
        Multiplayer,
        LimiteTentativas
    }

    enum Conquista
    {
        PrimeiraVitoria,
        VitoriaRapida,
        MestreDaLogica,
        ContraOTempo,
        MultiplayerMaster,
        SobPressao
    }
    static Dificuldade dificuldadeAtual = Dificuldade.Medio;
    static ModoJogo modoAtual = ModoJogo.Normal;
    static bool conquistasRegistradas = false;
    static HashSet<Conquista> conquistasDesbloqueadas = new();

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
                    ConfigurarDificuldade();
                    break;
                case "4":
                    ConfigurarModoJogo();
                    break;
                case "5":
                    ExibirConquistas();
                    break;
                case "6":
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
        Console.WriteLine("3. Alterar Dificuldade");
        Console.WriteLine("4. Escolher Modo de Jogo");
        Console.WriteLine("5. Conquistas");
        Console.WriteLine("6. Sair");

        Console.WriteLine("\n" + new string('═', 30));

        Console.Write($"\nDificuldade atual: ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(dificuldadeAtual);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"Modo atual: ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(modoAtual);

        ExibirBarraProgresso(conquistasDesbloqueadas.Count, Enum.GetValues(typeof(Conquista)).Length);
        Console.WriteLine($" Conquistas: {conquistasDesbloqueadas.Count}/{Enum.GetValues(typeof(Conquista)).Length}");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\nEscolha uma opção: ");
        Console.ResetColor();
    }

    static void ExibirBarraProgresso(int atual, int total)
    {
        const int LARGURA = 20;
        int progresso = (int)((double)atual / total * LARGURA);

        Console.Write("\n[");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(new string('█', progresso));
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(new string('░', LARGURA - progresso));
        Console.ResetColor();
        Console.Write("]");
    }

    static void ExibirConquistas()
    {
        Console.Clear();
        Console.WriteLine("=== 🏆 Conquistas ===\n");
        foreach (Conquista conquista in Enum.GetValues(typeof(Conquista)))
        {
            bool desbloqueada = conquistasDesbloqueadas.Contains(conquista);
            Console.ForegroundColor = desbloqueada ? ConsoleColor.Green : ConsoleColor.Gray;
            Console.Write(desbloqueada ? "✅ " : "❌ ");
            Console.WriteLine(conquista);
        }
        Console.ResetColor();
        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    static void DesbloquearConquista(Conquista conquista)
    {
        if (!conquistasDesbloqueadas.Contains(conquista))
        {
            conquistasDesbloqueadas.Add(conquista);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n🏆 Nova Conquista Desbloqueada!");
            Console.WriteLine($"    {conquista}");
            Console.ResetColor();
            Thread.Sleep(2000);
        }
    }

    static void ConfigurarModoJogo()
    {
        Console.Clear();
        Console.WriteLine("=== Modos de Jogo ===");
        Console.WriteLine("1. Normal");
        Console.WriteLine("2. Contra-Relógio (60 segundos)");
        Console.WriteLine("3. Multiplayer (2 jogadores)");
        Console.WriteLine("4. Limite de Tentativas (5 tentativas)");
        Console.Write("\nEscolha o modo: ");

        string? opcao = Console.ReadLine();
        switch (opcao)
        {
            case "1":
                modoAtual = ModoJogo.Normal;
                break;
            case "2":
                modoAtual = ModoJogo.ContraRelogio;
                break;
            case "3":
                modoAtual = ModoJogo.Multiplayer;
                break;
            case "4":
                modoAtual = ModoJogo.LimiteTentativas;
                break;
            default:
                ExibirMensagemErro("Opção inválida!");
                return;
        }
        Console.WriteLine($"Modo alterado para: {modoAtual}");
        Thread.Sleep(1500);
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
        Console.WriteLine("1. O computador escolherá um número com base na dificuldade:");
        Console.WriteLine("   Fácil: 1-50");
        Console.WriteLine("   Médio: 1-100");
        Console.WriteLine("   Difícil: 1-1000");
        Console.WriteLine("\nModos de Jogo:");
        Console.WriteLine("- Normal: Sem limites");
        Console.WriteLine("- Contra-Relógio: 60 segundos para adivinhar");
        Console.WriteLine("- Multiplayer: 2 jogadores alternam tentativas");
        Console.WriteLine("- Limite de Tentativas: 5 chances para acertar");
        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ResetColor();
        Console.ReadKey();
    }

    static void JogarNumMind()
    {
        switch (modoAtual)
        {
            case ModoJogo.Normal:
                JogarModoNormal();
                break;
            case ModoJogo.ContraRelogio:
                JogarModoContraRelogio();
                break;
            case ModoJogo.Multiplayer:
                JogarModoMultiplayer();
                break;
            case ModoJogo.LimiteTentativas:
                JogarModoLimiteTentativas();
                break;
        }
    }

    static void JogarModoNormal()
    {
        int maxNumero = dificuldadeAtual switch
        {
            Dificuldade.Facil => 50,
            Dificuldade.Medio => 100,
            Dificuldade.Dificil => 1000,
            _ => 100
        };

        Random random = new Random();
        int numeroSecreto = random.Next(1, maxNumero + 1);
        int tentativa = 0;
        bool acertou = false;

        Console.Clear();
        Console.WriteLine($"🎯 Novo Jogo Iniciado! (Modo: {modoAtual})");
        Console.WriteLine($"Tente adivinhar o número entre 1 e {maxNumero}.");

        while (!acertou)
        {
            tentativa = ProcessarTentativa(numeroSecreto, tentativa, out acertou);
        }
        if (acertou)
        {
            VerificarConquistas(tentativa, true);
        }
    }

    static void JogarModoContraRelogio()
    {
        int maxNumero = dificuldadeAtual switch
        {
            Dificuldade.Facil => 50,
            Dificuldade.Medio => 100,
            Dificuldade.Dificil => 1000,
            _ => 100
        };

        Random random = new Random();
        int numeroSecreto = random.Next(1, maxNumero + 1);
        int tentativa = 0;
        bool acertou = false;
        var inicio = DateTime.Now;
        var tempoLimite = TimeSpan.FromSeconds(60);

        Console.Clear();
        Console.WriteLine($"🎯 Novo Jogo Iniciado! (Modo: {modoAtual})");
        Console.WriteLine($"Tente adivinhar o número entre 1 e {maxNumero}.");
        Console.WriteLine("⏰ Você tem 60 segundos!");

        while (!acertou)
        {
            if (DateTime.Now - inicio > tempoLimite)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n⏰ Tempo esgotado!");
                Console.WriteLine($"O número era: {numeroSecreto}");
                Console.ResetColor();
                Thread.Sleep(2000);
                return;
            }

            var tempoRestante = tempoLimite - (DateTime.Now - inicio);
            Console.WriteLine($"\nTempo restante: {tempoRestante.Seconds} segundos");

            tentativa = ProcessarTentativa(numeroSecreto, tentativa, out acertou);
        }
        if (acertou)
        {
            VerificarConquistas(tentativa, true);
        }
    }

    static void JogarModoMultiplayer()
    {
        int maxNumero = dificuldadeAtual switch
        {
            Dificuldade.Facil => 50,
            Dificuldade.Medio => 100,
            Dificuldade.Dificil => 1000,
            _ => 100
        };

        Random random = new Random();
        int numeroSecreto = random.Next(1, maxNumero + 1);
        int tentativa = 0;
        bool acertou = false;
        int jogadorAtual = 1;

        Console.Clear();
        Console.WriteLine($"🎯 Novo Jogo Iniciado! (Modo: {modoAtual})");
        Console.WriteLine($"Tente adivinhar o número entre 1 e {maxNumero}.");

        while (!acertou)
        {
            Console.WriteLine($"\n👤 Vez do Jogador {jogadorAtual}");
            tentativa = ProcessarTentativa(numeroSecreto, tentativa, out acertou);
            jogadorAtual = jogadorAtual == 1 ? 2 : 1;
        }
        if (acertou)
        {
            VerificarConquistas(tentativa, true);
        }

        Console.WriteLine($"🏆 Jogador {(jogadorAtual == 1 ? 2 : 1)} venceu!");
        Thread.Sleep(2000);
    }

    static void JogarModoLimiteTentativas()
    {
        int maxNumero = dificuldadeAtual switch
        {
            Dificuldade.Facil => 50,
            Dificuldade.Medio => 100,
            Dificuldade.Dificil => 1000,
            _ => 100
        };

        const int LIMITE_TENTATIVAS = 5;
        Random random = new Random();
        int numeroSecreto = random.Next(1, maxNumero + 1);
        int tentativa = 0;
        bool acertou = false;

        Console.Clear();
        Console.WriteLine($"🎯 Novo Jogo Iniciado! (Modo: {modoAtual})");
        Console.WriteLine($"Tente adivinhar o número entre 1 e {maxNumero}.");
        Console.WriteLine($"Você tem {LIMITE_TENTATIVAS} tentativas!");

        while (!acertou && tentativa < LIMITE_TENTATIVAS)
        {
            Console.WriteLine($"\nTentativas restantes: {LIMITE_TENTATIVAS - tentativa}");
            tentativa = ProcessarTentativa(numeroSecreto, tentativa, out acertou);
        }

        if (acertou)
        {
            VerificarConquistas(tentativa, true);
        }

        if (!acertou)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ Tentativas esgotadas!");
            Console.WriteLine($"O número era: {numeroSecreto}");
            Console.ResetColor();
            Thread.Sleep(2000);
        }
    }

    static int ProcessarTentativa(int numeroSecreto, int tentativa, out bool acertou)
    {
        acertou = false;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("\nDigite seu palpite: ");
        string? entrada = Console.ReadLine();

        if (entrada != null && int.TryParse(entrada, out int palpite))
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
        return tentativa;
    }

    static void ConfigurarDificuldade()
    {
        Console.Clear();
        Console.WriteLine("=== Configurar Dificuldade ===");
        Console.WriteLine("1. Fácil (1-50)");
        Console.WriteLine("2. Médio (1-100)");
        Console.WriteLine("3. Difícil (1-1000)");
        Console.Write("\nEscolha a dificuldade: ");

        string? opcao = Console.ReadLine();
        switch (opcao)
        {
            case "1":
                dificuldadeAtual = Dificuldade.Facil;
                break;
            case "2":
                dificuldadeAtual = Dificuldade.Medio;
                break;
            case "3":
                dificuldadeAtual = Dificuldade.Dificil;
                break;
            default:
                ExibirMensagemErro("Opção inválida!");
                return;
        }
        Console.WriteLine($"Dificuldade alterada para: {dificuldadeAtual}");
        Thread.Sleep(1500);
    }

    static void VerificarConquistas(int tentativas, bool vitoria)
    {
        if (vitoria)
        {
            if (!conquistasRegistradas)
            {
                DesbloquearConquista(Conquista.PrimeiraVitoria);
                conquistasRegistradas = true;
            }

            if (tentativas <= 5)
                DesbloquearConquista(Conquista.VitoriaRapida);

            if (dificuldadeAtual == Dificuldade.Dificil)
                DesbloquearConquista(Conquista.MestreDaLogica);

            switch (modoAtual)
            {
                case ModoJogo.ContraRelogio:
                    DesbloquearConquista(Conquista.ContraOTempo);
                    break;
                case ModoJogo.Multiplayer:
                    DesbloquearConquista(Conquista.MultiplayerMaster);
                    break;
                case ModoJogo.LimiteTentativas:
                    DesbloquearConquista(Conquista.SobPressao);
                    break;
            }
        }
    }
}