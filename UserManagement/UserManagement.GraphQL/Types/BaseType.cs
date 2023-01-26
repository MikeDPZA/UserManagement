namespace UserManagement.GraphQL.Types;

public abstract class BaseType
{
    public Guid Id { get; set; }
    public String Name { get; set; }
}