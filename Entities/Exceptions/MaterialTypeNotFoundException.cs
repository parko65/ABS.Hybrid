using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions;
public sealed class MaterialTypeNotFoundException : NotFoundException
{
    public MaterialTypeNotFoundException(string name)
        : base($"Material type with name: {name} doesn't exist in the database.")
    {
    }

    public MaterialTypeNotFoundException(int id)
        : base($"Material type with id: {id} doesn't exist in the database.")
    {
    }
}
