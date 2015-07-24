# 2.0.0
## Adds
* timing information to checks and runs
* extended publisher to allow publishing about whole runs
* JE.EmbeddedChecks.StatsD publisher to allow alerts off statsd/graphite monitoring
* Checks are now named

## Breaks
* Changed MetaData dictionary to be `<string, object>`.
* Renamed `ConsolePublisher` to `TracePublisher` and made it write to Sys.Diag.Trace instead of stdout.

# 1.0.0
Hello world.
