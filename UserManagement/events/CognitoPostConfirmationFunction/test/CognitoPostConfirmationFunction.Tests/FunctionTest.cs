using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using UserManagement.Common.Events;

namespace CognitoPostConfirmationFunction.Tests;

public class FunctionTest
{
    [Fact]
    public void TestToUpperFunction()
    {

        // Invoke the lambda function and confirm the string was upper cased.
        var function = new Function();
        var context = new TestLambdaContext();
        // var upperCase = function.FunctionHandler(new CognitoPostConfirmationEvent(), context);

        // Assert.Equal("HELLO WORLD", upperCase);
    }
}
