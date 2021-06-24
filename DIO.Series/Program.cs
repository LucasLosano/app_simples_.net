using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario;

            do
            {
                opcaoUsuario = ObterOpcaoUsuario();

                switch(opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }


            }while(opcaoUsuario != "X");
            
            Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
        }
        private static string ObterOpcaoUsuario(){
            Console.WriteLine();
            Console.WriteLine("DIO Séries a seu dispor!!!");
            Console.WriteLine("Informe a opção desejado:");

            Console.WriteLine("1 - Listar séries");
            Console.WriteLine("2 - Inserir nova série");
            Console.WriteLine("3 - Atualizar série");
            Console.WriteLine("4 - Excluir série");
            Console.WriteLine("5 - Visualizar série");
            Console.WriteLine("C - Limpar Tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }

        private static void ListarSeries(){
            Console.WriteLine("Listar séries");

            var lista = repositorio.Lista();
            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum série cadastrada");
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluida();
                if (!excluido)
                {
                    Console.WriteLine($"#ID {serie.retornaId()}: {serie.retornaTitulo()}");
                }
            } 
        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");
            Serie novaSerie = CreateSerie(repositorio.ProximoId());      
            repositorio.Insere(novaSerie);     
        }

        private static void AtualizarSerie()
        {
            Console.WriteLine("Digite o id da série:");
            int entradaId = int.Parse(Console.ReadLine());
            Console.WriteLine("Atualizar série: " + repositorio.RetornaPorID(entradaId).retornaTitulo());
            
            Serie novaSerie = CreateSerie(entradaId);
            repositorio.Atualiza(entradaId, novaSerie);
        }

        private static void ExcluirSerie()
        {
            Console.WriteLine("Digite o id da série:");
            int entradaId = int.Parse(Console.ReadLine());

            repositorio.Exclui(entradaId);
        }

        private static void VisualizarSerie()
        {
            Console.WriteLine("Digite o id da série:");
            int entradaId = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorID(entradaId);

            Console.WriteLine(serie.ToString());
        }

        private static Serie CreateSerie(int id)
        {
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine($"{i} - {Enum.GetName(typeof(Genero), i)}");
            }

            Console.WriteLine("Digite o gênero entre as opções acima:");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o título da série:");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o ano de iníciop da série:");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a descrição da série:");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(
                id: id,
                genero: (Genero)entradaGenero,
                titulo: entradaTitulo,
                ano: entradaAno,
                descricao: entradaDescricao);
            return novaSerie;
        }
    }
}
