using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolApi.Models
{
    public class File
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FileName { get; set; }
        public string FileUrl { get; set; }

        public string UserId { get; set; }

    }
}
