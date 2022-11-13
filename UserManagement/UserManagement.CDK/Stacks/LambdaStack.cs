using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;

namespace UserManagement.CDK.Stacks;

public class LambdaStack : Stack
{
    public readonly IFunction UserManagementApiFunction;
    
    internal LambdaStack(Constructs.Construct scope, string id, IStackProps? props = null) : base(scope, id, props)
    {
        UserManagementApiFunction = new Function(this,
            "UserManagementApiFunction",
            new FunctionProps()
            {
                Runtime = Runtime.DOTNET_6,
                Handler = "UserManagement.Api::UserManagement.Api.LambdaEntryPoint::FunctionHandlerAsync",
                Code = Code.FromAsset(Path.Join("UserManagement.Api", "src", "UserManagement.Api", "bin", "debug", "net6.0")),
                MemorySize = 256,
                Timeout = Duration.Seconds(30),
                FunctionName = "UserManagementApiFunction"
            });

    }
}