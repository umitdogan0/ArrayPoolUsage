﻿using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using MemoryManagement;

var config = DefaultConfig.Instance
    .AddJob(Job
        .MediumRun
        .WithLaunchCount(1)
        .WithToolchain(InProcessNoEmitToolchain.Instance));

_ = BenchmarkRunner.Run<TestClass>(config);
