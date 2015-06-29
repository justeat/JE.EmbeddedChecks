# JE.EmbeddedChecks

## The point
### TL;DR
0. embed checks in your applications
1. trigger them from some regular event (eg, load balancer ping, cron, etc)
2. publish check pass/fail information to monitoring
3. -> operators know where to start looking, very quickly, when checks fail
  * ... instead of digging through logs or paging other engineers

Continuous delivery feedback cycles become shorter.

Pipelines become simpler to orchestrate.

### Long version
It's really useful for applications to proactively tell operators which parts of themselves are healthy vs unhealthy. Load balancers are a good example. A standard pattern is to ping a web-server's endpoint to see whether to include that web-server in the load-balanced cluster, or not. If the web-server is ready to included, it responds with `200 OK`, otherwise anything else.

This is a good "presence" check - "I am here", but if the web-server says "no" or fails to respond, the operator must go and delve in logs to find out why.

Embedded checks take this idea further:
* Check dependencies. Covers:
  * Configuration
  * Reachability
  * Response times
* Check functionality, against stubbed responses

Embedded checks are a way to save the operator time:
* The checks allow the operator to triangulate to the root cause very quickly
* The checks can run in any environment, not just production
* The checks' history allow more advanced resiliency patterns like circuit breakers some data to decide whether to open or close

Embedded checks are a way to save engineers time:
* onboarding is simpler - clone, run, see whether checks pass. If they don't, the checks have clues for how to make progress
  * application is now more self-documenting
  * dependencies are now more clearly surfaced

## Features
### Embedded check definition + runner

