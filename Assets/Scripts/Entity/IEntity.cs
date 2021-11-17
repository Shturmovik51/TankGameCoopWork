namespace TankGame
{
    public interface IEntity
    {
        public Ability Ability { get; set; }
        public Health Health { get; }
    }
}