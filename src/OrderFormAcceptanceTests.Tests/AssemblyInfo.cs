using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Fixtures)]
#if DEBUG
[assembly: LevelOfParallelism(4)]
#else
    [assembly: LevelOfParallelism(2)]
#endif
