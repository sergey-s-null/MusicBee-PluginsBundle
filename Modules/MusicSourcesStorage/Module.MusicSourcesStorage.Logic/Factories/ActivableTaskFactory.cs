﻿using Module.MusicSourcesStorage.Logic.Delegates;
using Module.MusicSourcesStorage.Logic.Entities.Tasks;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Void = Module.MusicSourcesStorage.Logic.Entities.Void;

namespace Module.MusicSourcesStorage.Logic.Factories;

public static class ActivableTaskFactory
{
    public static IActivableTask<Void, TResult> FromResult<TResult>(
        TResult result)
    {
        return CreateWithoutArgs(() => result);
    }

    public static IActivableTask<TArgs, TResult> Create<TArgs, TResult>(
        Func<TArgs, CancellationToken, TResult> func)
    {
        return Create<TArgs, TResult>(
            (args, progressCallback, token) =>
            {
                progressCallback(0);
                var result = func(args, token);
                progressCallback(1);
                return result;
            }
        );
    }

    public static IActivableTask<TArgs, TResult> Create<TArgs, TResult>(
        TaskFunction<TArgs, TResult> func)
    {
        return new ActivableTask<TArgs, TResult>(func);
    }

    public static IActivableTask<Void, TResult> CreateWithoutArgs<TResult>(
        Func<TResult> func)
    {
        return CreateWithoutArgs(_ => func());
    }

    public static IActivableTask<Void, TResult> CreateWithoutArgs<TResult>(
        Func<CancellationToken, TResult> func)
    {
        return Create<Void, TResult>(
            (_, progressCallback, token) =>
            {
                progressCallback(0);
                var result = func(token);
                progressCallback(1);
                return result;
            }
        );
    }

    public static IActivableTask<TArgs, Void> CreateWithoutResult<TArgs>(
        Action<TArgs> action)
    {
        return CreateWithoutResult<TArgs>((args, _) => action(args));
    }

    public static IActivableTask<TArgs, Void> CreateWithoutResult<TArgs>(
        Action<TArgs, CancellationToken> action)
    {
        return CreateWithoutResult<TArgs>((args, progressCallback, token) =>
        {
            progressCallback(0);
            action(args, token);
            progressCallback(1);
        });
    }

    public static IActivableTask<TArgs, Void> CreateWithoutResult<TArgs>(
        TaskAction<TArgs> func)
    {
        return new ActivableTask<TArgs, Void>((args, progressCallback, token) =>
        {
            func(args, progressCallback, token);
            return Void.Instance;
        });
    }
}