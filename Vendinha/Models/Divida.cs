using System.ComponentModel.DataAnnotations;

namespace Vendinha.Models
{
    public class Divida
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }
        public virtual Cliente? Cliente { get; set; }

        [Range(0.01, 999999.99)]
        public decimal Valor { get; set; }

                public bool Paga { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public DateTime? DataPagamento { get; set; }

        public virtual void PrintDados()
        {
            Console.WriteLine("Valor: {0:C}", Valor);
            Console.WriteLine("Situação: {0}", Paga ? "Paga" : "Em aberto");
            Console.WriteLine("Criada em: {0:dd/MM/yyyy}", DataCriacao);
            if (DataPagamento.HasValue)
            {
                Console.WriteLine("Paga em: {0:dd/MM/yyyy}", DataPagamento);
            }
        }
    }
}