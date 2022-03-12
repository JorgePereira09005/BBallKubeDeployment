using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBallService.Models.OutputModels
{
    public class PlayerOutput
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        public string? PictureUrl { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime SigningDate { get; set; }

        [Range(0, 99)]
        public int Number { get; set; }

        public bool IsAllStar { get; set; }

        public int TeamID { get; set; }

        public Team Team { get; set; }

        public ICollection<Representation> Representations { get; set; }
    }
}
