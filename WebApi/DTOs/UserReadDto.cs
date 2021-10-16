using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolApi.DTOs
{
    public class UserReadDto
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string TransactionNumber { get; set; }

        public IEnumerable<FileReadDto> FileReadDto { get; set; }


    }
}
