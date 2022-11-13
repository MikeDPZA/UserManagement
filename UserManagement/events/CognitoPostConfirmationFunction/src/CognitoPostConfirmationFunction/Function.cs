using System.Text.Json;
using Amazon.Lambda.Core;
using UserManagement.Common.Events;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace CognitoPostConfirmationFunction;

public class Function
{
    
    /// <summary>
    /// This function is called after a user is confirmed
    /// </summary>
    /// <param name="evnt">This is the event that is passed to the lambda function. It contains the
    /// user attributes and the user pool id.</param>
    /// <param name="context">This is a standard interface that is passed to all Lambda functions. It contains
    /// information about the execution environment.</param>
    /// <returns>
    /// An object.
    /// </returns>
    public CognitoPostConfirmationEvent FunctionHandler(CognitoPostConfirmationEvent evnt, ILambdaContext context)
    {
        LambdaLogger.Log($"UserCreation Event: {JsonSerializer.Serialize(evnt)}");
        return evnt;
    }
}