using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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

        [Column(TypeName = "bit")]
        public bool Estado { get; set; } 

        [Column(TypeName = "text")]
        public string Texto { get; set; } = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Link { get; set; } = string.Empty;

        public virtual Tema? Tema { get; set; }

        public virtual User? User { get; set; }

    }
}
