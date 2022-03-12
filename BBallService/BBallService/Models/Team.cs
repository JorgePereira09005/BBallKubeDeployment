using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBallService.Models
{
    public class Team
    {
        public int ID { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime FoundingDate { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public ICollection<Player> Roster { get; set; }
    }
}
