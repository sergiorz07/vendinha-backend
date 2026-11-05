using System.ComponentModel.DataAnnotations;

namespace Vendinha.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 3)]
        public string NomeCompleto { get; set; } = string.Empty;

        
        [Required, StringLength(11, MinimumLength = 11)]
        public string Cpf { get; set; } = string.Empty;

        public DateTime DataNascimento { get; set; }

        private string? email;
        [StringLength(120)]
        public string? Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = string.IsNullOrWhiteSpace(value)
                    ? null
                    : value.ToLower().Trim();
            }
        }

        public int Idade
        {
            get
            {
                var hoje = DateTime.Today;
                var idade = hoje.Year - DataNascimento.Year;
                var hojeAnoNascimento = hoje.AddYears(-idade);
                if (DataNascimento > hojeAnoNascimento)
                {
                    idade--;
                }
                return idade;
            }
        }

        public virtual ICollection<Divida> Dividas { get; set; } = new List<Divida>();

        public virtual void PrintDados()
        {
            Console.WriteLine("Nome: {0}", NomeCompleto);
            Console.WriteLine("CPF: {0}", Cpf);
            Console.WriteLine("E-mail: {0}", Email ?? "(não informado)");
            Console.WriteLine("Data de Nascimento: {0:dd/MM/yyyy}", DataNascimento);
            Console.WriteLine("Idade: {0} anos", Idade);
        }
    }
}