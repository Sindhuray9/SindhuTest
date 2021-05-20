using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIConsume.Models
{
    public class userViewModal
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}