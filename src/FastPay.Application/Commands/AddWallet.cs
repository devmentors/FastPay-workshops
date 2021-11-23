﻿using System;
using FastPay.Application.Abstractions;

namespace FastPay.Application.Commands
{
    public record AddWallet(Guid UserId, string Currency) : ICommand;
}