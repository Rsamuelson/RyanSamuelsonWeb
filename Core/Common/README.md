# Common Layer

This will contain all cross-cutting concerns.

There's always cross cutting concerns to deal with and this common layer is merely a necessary evil to deal with that. Every layer needs these commonalities - but challenge yourself to justify why it should belong here. 
Shed a tear, and consider implementing things like:
  - Interfaces for `DateTime` like `IDateTimeProvider`
  - Interfaces for logging (ex: `ILogger`)