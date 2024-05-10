public interface IEntity
{
    Option<ITargetable> Targetable { get; }
    Option<IMoveable> Moveable { get; }
    Option<IJumpable> Jumpable { get; }
    Option<IDamageable> Damageable { get; }
}