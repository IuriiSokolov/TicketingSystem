// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace TicketingSystem.MigrationService;

public class RetryingSqlServerRetryingExecutionStrategy(ExecutionStrategyDependencies dependencies) : NpgsqlRetryingExecutionStrategy(dependencies)
{
    protected override bool ShouldRetryOn(Exception? exception)
    {
        if (exception is not null && exception is NpgsqlException sqlException)
        {
            // EF Core issue logged to consider making this a default https://github.com/dotnet/efcore/issues/33191
            if (sqlException.ErrorCode is 4060)
            {
                // Don't retry on login failures associated with default database not existing due to EF migrations not running yet
                return false;
            }
            // Workaround for https://github.com/dotnet/aspire/issues/1023
            else if (sqlException.ErrorCode is 0 || (sqlException.ErrorCode is 203 && sqlException.InnerException is Win32Exception))
            {
                return true;
            }
        }

        return base.ShouldRetryOn(exception);
    }
}
