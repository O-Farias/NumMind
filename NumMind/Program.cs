using System;

partial class Program
{
    static void Main()
    {
        Random random = new Random();
        int numeroSecreto = random.Next(1, 101); // Número entre 1 e 100
        int tentativa = 0;
        int palpite;
        bool acertou = false;

        Console.WriteLine("🎯 Bem-vindo ao NumMind!");
        Console.WriteLine("Tente adivinhar o número secreto entre 1 e 100.");

        while (!acertou)
        {
            Console.Write("Digite seu palpite: ");
            string? entrada = Console.ReadLine();

            if (entrada != null && int.TryParse(entrada, out palpite))
            {
                tentativa++;

                if (palpite < numeroSecreto)
                    Console.WriteLine("🔺 O número é maior! Tente novamente.");
                else if (palpite > numeroSecreto)
                    Console.WriteLine("🔻 O número é menor! Tente novamente.");
                else
                {
                    Console.WriteLine($"🎉 Parabéns! Você acertou em {tentativa} tentativas.");
                    acertou = true;
                }
            }
            else
            {
                Console.WriteLine("⚠️ Entrada inválida! Digite um número.");
            }
        }
    }
}