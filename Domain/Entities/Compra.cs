using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("TB_COMPRA")]
    public class Compra 
    {
        [Column("COM_ID")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("COM_ESTADO")]
        [Display(Name = "Estado")]
        public EstadoCompra Estado { get; set; }

        [Column("COM_DATA_COMPRA")]
        [Display(Name = "Data Compra")]
        public DateTime? DataCompra { get; set; }

        [Display(Name = "Usuário")]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
      

    }
}
