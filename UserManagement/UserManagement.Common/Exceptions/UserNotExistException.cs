namespace UserManagement.Common.Exceptions;

public class UserNotExistException: Exception
{
    public UserNotExistException(string message): base(message) { }
    public UserNotExistException(): base() { }
}