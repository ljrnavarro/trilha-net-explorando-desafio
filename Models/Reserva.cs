namespace DesafioProjetoHospedagem.Models
{
    public class Reserva
    {
        public List<Pessoa> Hospedes { get; set; }
        public Suite Suite { get; set; }
        public int DiasReservados { get; set; }

        public bool ResevaConcluida { get; set; }

        public int Id { get; set; }

        public Reserva() { }

        public Reserva(int diasReservados)
        {
            DiasReservados = diasReservados;
        }

        public void CadastrarHospedes(List<Pessoa> hospedes)
        {
            if (hospedes.Count == 0) { return; throw new Exception("Lista de hóspedes precisa ser maior que zero(0)"); }
            if (Suite == null) { throw new Exception("É necessário cadastrar uma suíte antes de cadastrar hóspedes"); }
            if (Suite.Capacidade < hospedes.Count)
            {
                throw new Exception($"A capacidade da suíte é {Suite.Capacidade} e o número de hóspedes é {hospedes.Count}. Não é possível cadastrar mais hóspedes do que a capacidade da suíte.");
            }
            
            Hospedes = hospedes;
        }

        public void CadastrarSuite(Suite suite)
        {
            Suite = suite;
        }

        public int ObterQuantidadeHospedes()
        {
            return Hospedes?.Count ?? 0;
        }

        public decimal CalcularValorDiaria()
        {
            if (Suite == null)
            {
                throw new Exception("É necessário cadastrar uma suíte antes de calcular o valor da diária.");
            }
            if (DiasReservados <= 0)
            {
                throw new Exception("O número de dias reservados deve ser maior que zero.");
            }
        
            decimal valor = DiasReservados * Suite.ValorDiaria;
            
            if (DiasReservados >= 10)
            {
                valor -= valor * 0.1m; 
            }

            return valor;
        }
    }
}