using System;
using System.Collections.Generic;
using System.Text;

namespace First.App.Business.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        
        
    }
}
