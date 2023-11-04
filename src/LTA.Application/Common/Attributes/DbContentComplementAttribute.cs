namespace LTA.Application.Common.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class DbContentComplementAttribute : Attribute
{
    public DbContentComplementAttribute(Type databaseTableType)
    {
        DatabaseTableType = databaseTableType;
    }

    public Type DatabaseTableType { get; }
}