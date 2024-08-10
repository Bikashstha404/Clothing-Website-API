using ClothingStoreDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreApplication.Response
{
    public class SendEmailResponse
    {
        public bool Success { get; set; }
        public EmailModel EmailModel { get; set; }
        public string Message { get; set; }
    }
}
