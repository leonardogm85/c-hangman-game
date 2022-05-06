namespace HangmanGame
{
    /// <summary>
    /// Representa o jogo.
    /// </summary>
    class Game
    {
        /// <summary>
        /// Indica o número máximo de erros que o jogador pode ter antes do jogo terminar.
        /// </summary>
        private int maxErrors;

        /// <summary>
        /// Contrutor.
        /// </summary>
        /// <param name="maxErrors">Número máximo de erros possíveis.</param>
        public Game(int maxErrors)
        {
            this.maxErrors = maxErrors;
        }

        /// <summary>
        /// Jogar o jogo.
        /// </summary>
        public void Play()
        {
            // Cria o objeto que vai gerenciar a palavra ativa do jogo.
            Word word = new Word();

            while (true)
            {
                Console.WriteLine("--- JOGO DA FORCA ---\n");
                Console.WriteLine("Você pode errar no máximo {0} vezes.\n", maxErrors);

                // Variável para controlar os erros do jogador.
                int errors = 0;

                // Set para armazenar as letras já tentadas (para evitar que o jogador  as tente novamente).
                ISet<char> triedLetters = new HashSet<char>();

                // O jogo fica ativo enquanto a palavra não for encontrada por completo e enquanto o jogador
                // não atingir o número máximo de erros.
                while (!word.Finished && errors < maxErrors)
                {
                    Console.WriteLine(word.PartialWord);

                    // Solicita a letra ao jogador.
                    Console.Write("\nDigite uma letra: ");
                    string letter = Console.ReadLine();

                    // Se o jogador não digitar nada, solicita novamente.
                    if (string.IsNullOrWhiteSpace(letter))
                    {
                        continue;
                    }

                    // Verifica se o jogador já não tentou esta letra.
                    if (triedLetters.Contains(letter[0]))
                    {
                        // Se já tentou, solicita novamente.
                        Console.WriteLine("A letra {0} já foi tentada.\n", letter[0]);
                        continue;
                    }
                    else
                    {
                        // Se não tentou, adiciona a letra no set de letras tentadas.
                        triedLetters.Add(letter[0]);
                    }

                    // Procura a letra na palavra.
                    bool found = word.Guess(letter[0]);

                    if (found)
                    {
                        // Se encontrou, mostra a mensagem.
                        Console.WriteLine("Parabéns! A letra {0} foi encontrada!", letter[0]);
                    }
                    else
                    {
                        // Se não encontrou, incrementa o número de erros e mostra a mensagem.
                        errors++;
                        Console.WriteLine("Sinto muito, a letra {0} não existe na palavra. Você errou {1} vez(es).", letter[0], errors);
                    }

                    Console.WriteLine();
                }

                // Se o loop terminou, foi porque o jogador ganhou ou perdeu.
                if (errors < maxErrors)
                {
                    // Se o número máximo de erros não foi atingido, o jogador ganhou.
                    Console.Write("\nVocê adivinhou a palavra: {0}. Deseja jogar mais uma vez? (S/N): ", word.CompleteWord);
                }
                else
                {
                    // Se o número máximo de erros fou atingido, o jogador perdeu.
                    Console.Write("\nVocê não adivinhou a palavra, que era: {0}. Deseja jogar mais uma vez? (S/N): ", word.CompleteWord);
                }

                // Aguarda opção do jogador sobre jogar novamente.
                string playAgain = Console.ReadLine();

                if (playAgain.Length > 0 && (playAgain[0] == 's' || playAgain[0] == 'S'))
                {
                    // Se o jogador digitou 's' ou 'S', joga novamente.
                    Console.WriteLine("OK, vamos jogar novamente!");

                    // Vai para a próxima palavra.
                    word.Next();

                    // Limpa o console.
                    Console.Clear();
                }
                else
                {
                    // Qualquer outra opção digitada termina o jogo.
                    Console.WriteLine("Fim do jogo!");
                    break;
                }
            }
        }
    }
}
