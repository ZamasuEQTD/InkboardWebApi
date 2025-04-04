namespace Application.Encuestas.Abstractions
{
    public interface IEncuestaHub
    {
        public Task EnviarEncuestaVotada(Guid encuestaId, Guid respuestaId);
    }
}


