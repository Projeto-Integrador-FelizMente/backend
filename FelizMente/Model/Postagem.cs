using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FelizMente.Model
{
    public class Postagem : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Titulo { get; set; } = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Estado { get; set; } = string.Empty;

        [Column(TypeName = "text")]
        public string Texto { get; set; } = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Link { get; set; } = string.Empty;

        public virtual Tema? Tema { get; set; }

        public virtual User? User { get; set; }

    }
}
