using System.Text;
using DesafioProjetoHospedagem.Models;
using Spectre.Console;


string option = string.Empty;

List<Suite> suites = new List<Suite>();
List<Pessoa> pessoas = new List<Pessoa>();
List<Reserva> reservas = new List<Reserva>();

// Ask for the user's favorite fruit
void MenuPrncipal()
{
    option = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Sistema de [green]Hospedagem[/].")
            .PageSize(10)
            .MoreChoicesText("[grey](Mover para cima e para baixo para selecionar)[/]")
            .AddChoices(new[] {
            "[[1]] - Cadastrar Suite",
            "[[2]] - Cadastrar Pessoa",
            "[[3]] - Cadastrar Reserva" ,
            "[[4]] - Checkout",
            "[[5]] - Listar Reservas",
            "[[6]] - Listar Suites",
            "[[7]] - Listar Pessoas",
            "[[8]] - Sair",
            }));
}

void MenuCadastrarSuite()
{
    bool exibirMenuCadastrarSuite = true;

    var tabela = new Table();
    tabela.AddColumn(new TableColumn("Tipo Suite").Centered());
    tabela.AddColumn(new TableColumn("Capacidade").Centered());
    tabela.AddColumn(new TableColumn("Valor Diária").Centered());
    tabela.Border(TableBorder.Square);

    while (exibirMenuCadastrarSuite)
    {
        var tipoSuite = AnsiConsole.Prompt(
         new TextPrompt<string>("Tipo da [green]Suite[/]?")
             .PromptStyle("green")
             .ValidationErrorMessage("[red]Tipo não pode ser vazio[/]")
             .AllowEmpty()
             );

        var capacidade = AnsiConsole.Prompt(
            new TextPrompt<int>("Capacidade da [green]Suite[/]?")
                .PromptStyle("green")
                .ValidationErrorMessage("[red]Capacidade deve ser um número maior que 0[/]")
                .Validate(input => input > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Capacidade deve ser um número maior que 0[/]"))
        );
        var valorDiaria = AnsiConsole.Prompt(
            new TextPrompt<decimal>("Valor da [green]Diária[/]?")
                .PromptStyle("green")
                .ValidationErrorMessage("[red]Valor deve ser um número maior que 0[/]")
                .Validate(input => input > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Valor deve ser um número maior que 0[/]"))
        );

        Suite suite = new Suite(tipoSuite, capacidade, valorDiaria);
        suites.Add(suite);
        AnsiConsole.MarkupLine($"[green]Suite {tipoSuite} cadastrada com sucesso![/]");

       
        tabela.AddRow(suite.TipoSuite, suite.Capacidade.ToString(), suite.ValorDiaria.ToString("C"));
        AnsiConsole.Write(tabela);

        
        var cadastrarOutraSuite = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Deseja cadastrar outra suíte?")
                .AddChoices(new[] { "Sim", "Não" })
        );
        if (cadastrarOutraSuite == "Não")
            exibirMenuCadastrarSuite = false;
        else
        {
            
            Console.Clear();
            tipoSuite = string.Empty;
            capacidade = 0;
            valorDiaria = 0;
        }
    }
}

void MenuCadastrarPessoa()
{
    bool exibirMenuCadastrarPessoa = true;

    var tabela = new Table();
    tabela.AddColumn(new TableColumn("Id").Centered());
    tabela.AddColumn(new TableColumn("Pessoa").Centered());

    while (exibirMenuCadastrarPessoa)
    {
        var nomePessoa = AnsiConsole.Prompt(
         new TextPrompt<string>("Nome da [green]Pessoa[/]?")
             .PromptStyle("green")
             .ValidationErrorMessage("[red]Nome da pessoa não pode ser vazio[/]")
             .AllowEmpty()
             );

        var sobreNome = AnsiConsole.Prompt(
        new TextPrompt<string>("Sobrenome da [green]Pessoa[/]?")
            .PromptStyle("green")
            .ValidationErrorMessage("[red]Sobre da pessoa não pode ser vazio[/]")
            .AllowEmpty()
            );

        Pessoa pessoa = new Pessoa(nomePessoa, sobreNome);
        pessoa.Id = pessoas.Count + 1; 
        pessoas.Add(pessoa);
        AnsiConsole.MarkupLine($"[green]Pessoa {pessoa.NomeCompleto} cadastrada com sucesso![/]");

        tabela.AddRow(pessoa.Id.ToString(), pessoa.NomeCompleto);
        AnsiConsole.Write(tabela);
       
        var cadastrarOutraPessoa = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Deseja cadastrar outra pessoa?")
                .AddChoices(new[] { "Sim", "Não" })
        );
        if (cadastrarOutraPessoa == "Não")
            exibirMenuCadastrarPessoa = false;
        else
        {
            Console.Clear();
            nomePessoa = string.Empty;
            sobreNome = string.Empty;
        }
    }
}

void MenuCadastrarReserva()
{
    //Cadastrar a reserva
    bool exibirMenuCadastrarReserva = true;

    var tabela = new Table();
    tabela.AddColumn(new TableColumn("ID").Centered());
    tabela.AddColumn(new TableColumn("Dias Reservados").Centered());
    tabela.AddColumn(new TableColumn("Quantidade de Hóspedes").Centered());
    tabela.AddColumn(new TableColumn("Valor da Diária").Centered());
    tabela.AddColumn(new TableColumn("Suite").Centered());
    tabela.AddColumn(new TableColumn("Capacidade").Centered());
    tabela.AddColumn(new TableColumn("Valor Diária").Centered());
    tabela.AddColumn(new TableColumn("Hóspedes").Centered());
    tabela.ShowRowSeparators();

    Reserva reserva;


    while (exibirMenuCadastrarReserva)
    {
        if (suites.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Nenhuma suíte cadastrada. Cadastre uma suíte antes de cadastrar uma reserva.[/]");
            return;
        }
        if (pessoas.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Nenhuma pessoa cadastrada. Cadastre uma pessoa antes de cadastrar uma reserva.[/]");
            return;
        }
        var diasReservados = AnsiConsole.Prompt(
            new TextPrompt<int>("Quantos dias a [green]reserva[/] será feita?")
                .PromptStyle("green")
                .ValidationErrorMessage("[red]Dias reservados deve ser um número maior que 0[/]")
                .Validate(input => input > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Dias reservados deve ser um número maior que 0[/]"))
        );
        var suiteSelecionada = AnsiConsole.Prompt(
            new SelectionPrompt<Suite>()
                .Title("Selecione a [green]Suite[/]:")
                .PageSize(10)
                .MoreChoicesText("[grey](Use as setas para navegar)[/]")
                .AddChoices(suites)
                .UseConverter(suite => $"{suite.TipoSuite} - Capacidade: {suite.Capacidade} - Valor Diária: {suite.ValorDiaria:C}")
        );
        var pessoasSelecionadas = AnsiConsole.Prompt(
            new MultiSelectionPrompt<Pessoa>()
                .Title("Selecione os [green]Hóspedes[/]:")
                .PageSize(10)
                .MoreChoicesText("[grey](Use as setas para navegar e espaço para selecionar)[/]")
                .AddChoices(pessoas)
                .UseConverter(pessoa => $"{pessoa.NomeCompleto} (ID: {pessoa.Id})")
                );

        try
        {
            List<Pessoa> hospedes = new List<Pessoa>(pessoasSelecionadas);

            reserva = new Reserva(diasReservados);
            reserva.Id = reservas.Count + 1; 
            reserva.CadastrarSuite(suiteSelecionada);
            reserva.CadastrarHospedes(hospedes);
            reservas.Add(reserva);
            AnsiConsole.MarkupLine($"[green]Reserva cadastrada com sucesso! Hóspedes: {reserva.ObterQuantidadeHospedes()}, Valor da diária: {reserva.CalcularValorDiaria():C}[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Erro ao cadastrar reserva: {ex.Message}[/]");
            continue; 
        }

        tabela.AddRow(
            reserva.Id.ToString(),
            reserva.DiasReservados.ToString(),
            reserva.ObterQuantidadeHospedes().ToString(),
            reserva.CalcularValorDiaria().ToString("C"),
            reserva.Suite?.TipoSuite ?? "Nenhuma",
            reserva.Suite?.Capacidade.ToString() ?? "N/A",
            reserva.Suite?.ValorDiaria.ToString("C") ?? "N/A",
            string.Join(", ", reserva.Hospedes?.Select(h => h.NomeCompleto) ?? new List<string> { "Nenhum hóspede" })
        );

        AnsiConsole.Write(tabela);

        var cadastrarOutraReserva = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Deseja cadastrar outra reserva?")
                .AddChoices(new[] { "Sim", "Não" })
        );
        if (cadastrarOutraReserva == "Não")
            exibirMenuCadastrarReserva = false;
        else
        {
            diasReservados = 0;
            suiteSelecionada = null;
            pessoasSelecionadas = new List<Pessoa>();
        }

    }
}

void MenuCheckout()
{
    if (reservas.Count == 0)
    {
        AnsiConsole.MarkupLine("[red]Nenhuma reserva cadastrada.[/]");
        return;
    }
    var reservaSelecionada = AnsiConsole.Prompt(
        new SelectionPrompt<Reserva>()
            .Title("Selecione a [green]Reserva[/] para checkout:")
            .PageSize(10)
            .MoreChoicesText("[grey](Use as setas para navegar)[/]")
            .AddChoices(reservas)
            .UseConverter(reserva => $"ID: {reserva.Id} - Dias Reservados: {reserva.DiasReservados} - Hóspedes: {reserva.ObterQuantidadeHospedes()}")
    );
    reservaSelecionada.ResevaConcluida = true;
    AnsiConsole.MarkupLine($"[green]Checkout realizado com sucesso para a reserva ID: {reservaSelecionada.Id}[/]");

    //deseja fazer checkout de outra reserva?
    var checkoutOutraReserva = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Deseja fazer checkout de outra reserva?")
            .AddChoices(new[] { "Sim", "Não" })
    );
    if (checkoutOutraReserva == "Não")
    {
        return; 
    }
    else
    {
        MenuCheckout(); 
    }
}

bool exibirMenu = true;

// Realiza o loop do menu
while (exibirMenu)
{
    Console.Clear();
    MenuPrncipal();

    switch (option.Substring(0, 5).Replace("[[", "").Replace("]]", ""))
    {
        case "1":
            MenuCadastrarSuite();
            break;

        case "2":
            MenuCadastrarPessoa();
            break;

        case "3":
            MenuCadastrarReserva();
            break;

        case "4":
            MenuCheckout();
            break;

        case "5":
            Console.Clear();
            AnsiConsole.WriteLine("Listando Reservas:");

            if (reservas.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]Nenhuma reserva cadastrada.[/]");
            }
            else
            {
                var tabelaReservas = new Table();
                tabelaReservas.Border(TableBorder.Square);
                tabelaReservas.AddColumn(new TableColumn("ID").Centered());
                tabelaReservas.AddColumn(new TableColumn("Dias Reservados").Centered());
                tabelaReservas.AddColumn(new TableColumn("Quantidade de Hóspedes").Centered());
                tabelaReservas.AddColumn(new TableColumn("Valor Total").Centered());
                tabelaReservas.AddColumn(new TableColumn("Suite").Centered());
                tabelaReservas.AddColumn(new TableColumn("Capacidade").Centered());
                tabelaReservas.AddColumn(new TableColumn("Valor Diária").Centered());
                tabelaReservas.AddColumn(new TableColumn("Checkout Realizado").Centered());
                tabelaReservas.AddColumn(new TableColumn("Hóspedes").Centered());
                tabelaReservas.ShowRowSeparators();
                foreach (var reserva in reservas)
                {
                    tabelaReservas.AddRow(
                        reserva.Id.ToString(),
                        reserva.DiasReservados.ToString(),
                        reserva.ObterQuantidadeHospedes().ToString(),
                        reserva.CalcularValorDiaria().ToString("C"),
                        reserva.Suite?.TipoSuite ?? "Nenhuma",
                        reserva.Suite?.Capacidade.ToString() ?? "N/A",
                        reserva.Suite?.ValorDiaria.ToString("C") ?? "N/A",
                        reserva.ResevaConcluida ? "Sim" : "Não",
                        string.Join(", ", reserva.Hospedes?.Select(h => h.NomeCompleto) ?? new List<string> { "Nenhum hóspede" })
                    );
                }
                AnsiConsole.Write(tabelaReservas);
            }
            break;

        case "6":
            Console.Clear();
            AnsiConsole.WriteLine("Listando Suites:");
            if (suites.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]Nenhuma suíte cadastrada.[/]");
            }
            else
            {
                foreach (var suite in suites)
                {
                    AnsiConsole.MarkupLine($"[green]Tipo:[/] {suite.TipoSuite}, [blue]Capacidade:[/] {suite.Capacidade}, [yellow]Valor Diária:[/] {suite.ValorDiaria:C}");
                }
            }
            break;

        case "7":
            Console.Clear();
            AnsiConsole.WriteLine("Listando Pessoas:");
            if (pessoas.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]Nenhuma pessoa cadastrada.[/]");
            }
            else
            {
                foreach (var pessoa in pessoas)
                {
                    AnsiConsole.MarkupLine($"[green]Nome Completo:[/] {pessoa.NomeCompleto}");
                }
            }
            break;

        case "8":
            exibirMenu = false;
            break;

        default:
            Console.WriteLine("Opção inválida");
            break;
    }

    Console.WriteLine("Pressione uma tecla para continuar");
    Console.ReadLine();
}

Console.WriteLine("O programa se encerrou");
