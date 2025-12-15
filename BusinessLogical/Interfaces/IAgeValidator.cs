using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogical.Models;

namespace BusinessLogical.Interfaces
{
    public interface IAgeValidator
    {
        ValidationResult Validate(int age);
    }
}