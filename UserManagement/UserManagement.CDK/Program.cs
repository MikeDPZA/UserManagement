using Amazon.CDK;
using Amazon.CDK.AWS.Cognito;
using UserManagement.CDK.StackProps;
using UserManagement.CDK.Stacks;

namespace UserManagement.CDK
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            var lambdaStack = new LambdaStack(app, "UserManagementLambdaStack");
            var cognitoStack = new CognitoStack(app, "UserManagementCognitoStack");
            var apiGatewayStack = new ApiGatewayStack(app, "UserManagementApiGatewayStack", new ApiGatewayStackProps()
            {
                RestApiFunction = lambdaStack.UserManagementApiFunction
            });
            app.Synth();
        }
    }
}