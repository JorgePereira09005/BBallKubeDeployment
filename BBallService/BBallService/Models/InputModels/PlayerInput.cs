using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBallService.Models.InputModels
{
    public class PlayerInput
    {
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime SigningDate { get; set; }

        [Range(0, 99)]
        public int Number { get; set; }

        public bool IsAllStar { get; set; }

        public int TeamID { get; set; }

        public Player ConvertToPlayer()
        {
            return new Player
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Salary = this.Salary,
                SigningDate = this.SigningDate,
                Number = this.Number,
                IsAllStar = this.IsAllStar,
                TeamID = this.TeamID
            };
        }
    }
}
