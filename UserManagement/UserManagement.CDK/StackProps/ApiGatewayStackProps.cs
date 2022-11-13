using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;

namespace UserManagement.CDK.StackProps;

public class ApiGatewayStackProps: IStackProps
{
    public IFunction RestApiFunction { get; init; }
}