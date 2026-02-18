namespace RBAC.Domain.Entities;

public class Action
{
    private Action() { }

    public Action(string code, string name)
    {
        Code = code;
        Name = name;
    }

    public long Id { get; private set; }
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
}
