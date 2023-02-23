using System.ComponentModel.DataAnnotations.Schema;

namespace Sdc.Data
{
    public class MAT_Shot
    {
        public int Id { get; set; }
        public int? Number { get; set; }
        public bool IsFilled { get; set; }
        public bool IsForClosing { get; set; }
        public bool IsClosingDart { get; set; }
        public bool IsSkipped { get; set; }
        public bool IsDouble { get; set; }
        public bool IsTriple { get; set; }
        public bool IsOvershot { get; set; }

        public virtual List<MAT_Shot_x_Round> Rounds { get; set; }
    }
}
