﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackIT.Shared.Abstractions.Query
{
    public interface IQuery
    {
    }

    public interface IQuery<TResult> : IQuery
    {
    }

}
