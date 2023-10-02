using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FelizMente.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Nome { get; set; } = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Usuario { get ; set; } = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(510)]
        public string Foto { get ; set; } = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Senha { get ; set; } = string.Empty;

        [Column(TypeName = "bit")]
        public bool Tipo { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Postagem>? Postagem { get; set; }
    }
}
