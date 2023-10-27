using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("TB_LOGSISTEMA")]
     public class LogSistema : Base
    {
        [Column("LOG_JSONINFORMACAO")]
        [Display(Name = "Json Informação")]
        public string JsonInformacao { get; set; } = string.Empty;

        [Column("LOG_TIPOLOG")]
        [Display(Name = "Tipo Log")]
        public TipoLog TipoLog { get; set; }

        [Column("LOG_CONTROLLER")]
        [Display(Name = "Nome Controller")]
        public string NomeController { get; set; } = string.Empty;

        [Column("LOG_ACTION")]
        [Display(Name = "Nome Action")]
        public string NomeAction { get; set; } = string.Empty;

        [Display(Name = "Usuário")]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser ApplicationUser { get; set; } 
    }
}
