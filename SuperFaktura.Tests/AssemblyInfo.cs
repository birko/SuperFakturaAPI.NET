using Xunit;

// Integration tests share one sandbox account and create, edit and delete its data;
// running test classes in parallel makes them pick records another test is just deleting.
[assembly: CollectionBehavior(DisableTestParallelization = true)]
