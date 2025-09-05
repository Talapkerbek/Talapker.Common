﻿using System.Reflection;
using JasperFx;
using JasperFx.Core;
using Microsoft.AspNetCore.Builder;
using Npgsql;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.FluentValidation;

namespace Talapker.Common.Wolverine;

public static class Extensions
{
    public static ConfigureHostBuilder AddWolverineWithAssemblyDiscovery
    (
        this ConfigureHostBuilder host,
        Assembly handlersAssembly,
        Action<WolverineOptions>? configure = null
    )
    {
        
        host.UseWolverine(opts =>
        {
            // If we encounter a concurrency exception, just try it immediately
            // up to 3 times total
            opts.Policies.OnException<ConcurrencyException>().RetryTimes(3);

            // It's an imperfect world, and sometimes transient connectivity errors
            // to the database happen
            opts.Policies.OnException<NpgsqlException>()
                .RetryWithCooldown(50.Milliseconds(), 100.Milliseconds(), 250.Milliseconds());
    
            /*opts.UseKafka("")
                .ConfigureClient(client =>
                {
                    // configure both producers and consumers

                })
                .ConfigureConsumers(consumer =>
                {
                    // configure only consumers
                })

                .ConfigureProducers(producer =>
                {
                    // configure only producers
                })
            
                .ConfigureProducerBuilders(builder =>
                {
                    // there are some options that are only exposed
                    // on the ProducerBuilder
                })
            
                .ConfigureConsumerBuilders(builder =>
                {
                    // there are some Kafka client options that
                    // are only exposed from the builder
                })
            
                .ConfigureAdminClientBuilders(builder =>
                {
                    // configure admin client builders
                });*/
           
            opts.Discovery.IncludeAssembly(handlersAssembly);
    
            // Adding outbox on all publish
            opts.Policies.UseDurableOutboxOnAllSendingEndpoints();
            opts.Policies.UseDurableLocalQueues();
            
            // Adding inbox on all consumers
            opts.Policies.UseDurableInboxOnAllListeners();
            opts.Policies.AutoApplyTransactions();
            
            opts.UseFluentValidation();
            
            configure?.Invoke(opts);
        });
        
        return host;
    }
}