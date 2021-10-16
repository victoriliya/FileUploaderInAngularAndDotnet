using CoolApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolApi.DTOs
{
    public class PaginatedResultDto<T>
    {
        public PageMetaData PageMetaData { get; set; }

        public IEnumerable<T> ResponseData { get; set; } = new List<T>();
    }
}
