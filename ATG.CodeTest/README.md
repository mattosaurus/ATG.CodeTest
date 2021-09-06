## Approach
I've tried to refactor the code to make it as reusable and generic as possible as well as to improve consistancy in the naming of methods and objects. Responsibilities have also beeen split out where approiriate so classes are now only concerened with their immediate area of responsibility.

Using interfaces for the repositories and services allows easier mocking for testing purposes as well as helps with dependency injection.

Avoiding hardcoding of settings values means the code is more reusable.

## Changes
- Lot repositories can be different data sources as long as they implement ``IRepository``
- ``FailoverLot`` is misleadingly named if this is what is recording details of historic conectivity incidents, this has been renamed to ``Incident``
- The logic for determining if the failover state is enabled doesn't belong in ``LotService``, this has been broken out into its own ``IncidentService``
- The logic for determining if the system is in a failover state seems to be incorrect. It is checking if more than 50 events have occured more than 10 minutes in the future. This has been updated to be > 50 or more events have occured in the last 10 minutes. This is not documented in the spec and I would normally check with the project manager to confirm the desired logic.
- Incidents are assumed to have their own data store and have been broken out of ``LotService`` into ``IncidentRepository``
- Incidents are accessed as an ``IQueriable`` rather than a ``List`` as this allows serverside filtering.
- Inconsistant naming of the ``LoadCustomer`` method in ``LotRepository`` has been fixed.
- Services have interfaces to allow for dependency injection and testing.
- Repositories are stored in an ``IEnumerable<IRepository>`` to make adding extra repositories in at a later date easier.
- Possibly the logic for determing what repository to use should be managed by a rules engine.
- Why if a lot if returned from the main repository and it is marked as archived are we fetching it from the archive, we already have the details? I've left this logic in but removing this would save excess repository lookups and greatly simplify implementing a rules engine.