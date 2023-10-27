using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("TB_COMPRA_USUARIO")]
    public class CompraUsuario 
    {
        [Column("CUS_ID")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Produto")]
        public int? IdProduto { get; set; }
      
        [Column("CUS_ESTADO")]
        [Display(Name = "Estado")]
        public EstadoCompra Estado { get; set; }

        [Column("CSU_QTD")]
        [Display(Name = "Quantidade")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade da compra deve ser maior que zero.")]
        public int QtdCompra { get; set; }

        [Display(Name = "Usuário")]
        [ForeignKey("ApplicationUser")]
        public string? UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [NotMapped]
        [Display(Name = "Quantidade Total")]
        public int QuantidadeProdutos { get; set; }

        [NotMapped]
        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }

        [NotMapped]
        [Display(Name = "Endereço de entrega")]
        public string EnderecoCompleto { get; set; }

        [NotMapped]
        public List<Produto> ListaProdutos { get; set; }

        [Display(Name = "Compra")]
        public int IdCompra { get; set; }
       
    }
}
