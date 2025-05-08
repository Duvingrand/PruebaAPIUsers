using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.DTOs
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{Name} {LastName}";
        public string Address { get; set; } = string.Empty;
        public string TellNumber { get; set; } = string.Empty;
        public string BirthDay { get; set; } = string.Empty;
        public string DocumentID { get; set; } = string.Empty;
    }
}