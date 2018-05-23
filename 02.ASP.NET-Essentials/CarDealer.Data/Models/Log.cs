namespace CarDealer.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Log
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(6)]
        public string Operation { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(9)]
        public string ModyfiedTable { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
