namespace InfnetStreaming.Domain.SeedWork
{
    public abstract class Entidade
    {
        public Guid Id { get; private set; }

        protected Entidade()
            => Id = Guid.NewGuid();
        
    }
}
