using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleApi.Domain.Entities
{
    public class ArticleEntity
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Abstract { get; set; }
        public string? DOI { get; set; }
        public List<string>? Authors { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
