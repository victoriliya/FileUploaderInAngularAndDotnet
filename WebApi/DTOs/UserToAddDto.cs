using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoolApi.DTOs
{
    public class UserToAddDto
    {
        [Required(ErrorMessage = "The name field is require")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The email field is require")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        public string TransactionNumber { get; set; }

        public IEnumerable<FileToAddDto> FilesToAddDto { get; set; }


    }
}
