﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcmeCorpEntities;

namespace AcmeCorpEF
{
    public class EFUnitOfWorkFactory : IUnitOfWorkFactory
    {
        
        public IUnitOfWork Create()
        {
            return new EFUnitOfWork("name=AcmeCorpContainer");
        }
    }
}
