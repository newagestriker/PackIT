﻿using PackIT.Domain.Consts;
using PackIT.Shared.Abstractions.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackIT.Application.Commands
{
    public record CreatepackingListWithItems(Guid Id, string Name, ushort Days, Gender Gender,LocalizationWriteModel Localization ) : ICommand;

    public record LocalizationWriteModel(string City, string Country);
}
