# DIO - Trilha .NET - Explorando a linguagem C#
www.dio.me

## Desafio de projeto
Para este desafio, você precisará usar seus conhecimentos adquiridos no módulo de explorando a linguagem C#, da trilha .NET da DIO.

## Contexto
Você foi contratado para construir um sistema de hospedagem, que será usado para realizar uma reserva em um hotel. Você precisará usar a classe Pessoa, que representa o hóspede, a classe Suíte, e a classe Reserva, que fará um relacionamento entre ambos.

O seu programa deverá cálcular corretamente os valores dos métodos da classe Reserva, que precisará trazer a quantidade de hóspedes e o valor da diária, concedendo um desconto de 10% para caso a reserva seja para um período maior que 10 dias.

## Regras e validações
1. Não deve ser possível realizar uma reserva de uma suíte com capacidade menor do que a quantidade de hóspedes. Exemplo: Se é uma suíte capaz de hospedar 2 pessoas, então ao passar 3 hóspedes deverá retornar uma exception.
2. O método ObterQuantidadeHospedes da classe Reserva deverá retornar a quantidade total de hóspedes, enquanto que o método CalcularValorDiaria deverá retornar o valor da diária (Dias reservados x valor da diária).
3. Caso seja feita uma reserva igual ou maior que 10 dias, deverá ser concedido um desconto de 10% no valor da diária.


![Diagrama de classe estacionamento](diagrama_classe_hotel.png)

## Solução
Para finalização do projeto foram implementadas algumas moficiações:

** 1**. Utilização da biblioteca Spectre.Console para construção do menu interativo
** 2**. Funcionalidades: Cadastro de pessoas / hospedes , Cadastro de Suites, Cadastro de reservas, checkout e listagens em geral
** 3**. Telas:

    <img width="572" height="302" alt="image" src="https://github.com/user-attachments/assets/2d9b313d-0632-4872-8585-d41a085253ef" />
    <img width="377" height="260" alt="image" src="https://github.com/user-attachments/assets/fe289b6f-b8e1-4f67-b463-b328cbd7c14a" />
    <img width="392" height="275" alt="image" src="https://github.com/user-attachments/assets/07db4544-aaed-4647-b812-07c7f884d686" />
    <img width="314" height="215" alt="image" src="https://github.com/user-attachments/assets/09873c57-6dc6-43d0-b022-390638afa977" />
    <img width="348" height="220" alt="image" src="https://github.com/user-attachments/assets/a3bcaffb-772d-4344-9aee-8922dfc29a12" />
    <img width="574" height="155" alt="image" src="https://github.com/user-attachments/assets/40bead85-137e-4aed-be1c-974b1ce219a0" />
    <img width="578" height="308" alt="image" src="https://github.com/user-attachments/assets/3fa8a35b-4358-4f9e-aab1-3b0278165089" />
    <img width="578" height="215" alt="image" src="https://github.com/user-attachments/assets/1fe43e34-bf33-427c-9b52-34b4dcec8a59" />
    <img width="322" height="192" alt="image" src="https://github.com/user-attachments/assets/581557e8-2399-4ab9-83b3-9b669e904cb9" />
    <img width="278" height="140" alt="image" src="https://github.com/user-attachments/assets/14bcf414-a625-4f82-bf28-2e88f1caba5d" />

