///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var distDirectory = Directory("./dist");
var solutionFile = File("./src/dotnet-microservices.sln");

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
   // Executed BEFORE the first task.
   Information("Running tasks...");
});

Teardown(ctx =>
{
   // Executed AFTER the last task.
   Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////
Task("Clean")
   .Does(() => {
      CleanDirectory(distDirectory);
   });

Task("Restore")
   .IsDependentOn("Clean")   
   .Does(() => {
      DotNetCoreRestore(solutionFile);
   });

Task("Build")
   .IsDependentOn("Clean")
   .IsDependentOn("Restore")
   .Does(() => {
        DotNetCoreBuild(solutionFile,
            new DotNetCoreBuildSettings()
            {
                Configuration = configuration,
                ArgumentCustomization = args => args.Append("--no-restore"),
            });
   });

Task("Test")
   .IsDependentOn("Build")
   .Does(() => {
        var projects = GetFiles("./src/**/*.Tests.csproj");
        foreach(var project in projects)
        {
            Information("Testing project " + project);
            DotNetCoreTest(
                project.ToString(),
                new DotNetCoreTestSettings()
                {
                    Configuration = configuration,
                    NoBuild = true,
                    ArgumentCustomization = args => args.Append("--no-restore --test-adapter-path:. --logger:\"junit;LogFilePath=test-results/xunit_results.xml\""),
                });
        }
   });
Task("BuildAndTest")   
   .IsDependentOn("Clean")
   .IsDependentOn("Restore")
   .IsDependentOn("Build")
   .IsDependentOn("Test")
   ;

Task("Default")
   .IsDependentOn("BuildAndTest")
   //.IsDependentOn("Publish")
   ;

RunTarget(target);