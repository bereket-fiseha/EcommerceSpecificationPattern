﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DuplicateEntityException : BadRequestException
    {
        public DuplicateEntityException(string className,string fieldName,dynamic fieldValue):
            base($"The {className} entity with {fieldName} {fieldValue} already exists, the field {fieldName} is unique!")
        {
        }
    }
}
