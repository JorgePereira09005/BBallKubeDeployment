using System;
using System.ComponentModel.DataAnnotations;

namespace BBallService.Models
{
    // Let's pretend a player can have multiple agents.
    public class Representation
    {

        public int AgentID { get; set; }

        public int PlayerID { get; set; }

        [Range(0, 20)]
        public decimal Comission { get; set; }

        public Player Player { get; set; }

        public Agent Agent { get; set; }
    }
}
