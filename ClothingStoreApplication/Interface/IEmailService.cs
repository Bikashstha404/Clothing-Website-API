﻿using ClothingStoreDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreApplication.Interface
{
    public interface IEmailService
    {
        void SendEmail(EmailModel emailModel);
    }
}
