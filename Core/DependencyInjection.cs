﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration config)
    {
        return services;
    }
}