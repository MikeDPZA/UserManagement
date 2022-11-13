using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using UserManagement.CDK.StackProps;

namespace UserManagement.CDK.Stacks;

public class ApiGatewayStack: Stack
{
    public readonly LambdaRestApi UserManagementApi;
    
    internal ApiGatewayStack(Constructs.Construct scope, string id, ApiGatewayStackProps props) : base(scope, id)
    {
        UserManagementApi = new LambdaRestApi(this, "UserManagementRestApi", new LambdaRestApiProps()
        {
            RestApiName = "UserManagementREST",
            Handler = props.RestApiFunction,
            Proxy = true,
        });

        UserManagementApi
            .Root
            .AddResource("UserManagement");
    }

}