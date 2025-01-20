namespace Domain.Hilos.Models.ValueObjects
{
    public class ConfiguracionDeComentarios
    {
        public bool DadosActivado { get; private set; }
        public bool IdUnicoActivado { get; private set; }
        public ConfiguracionDeComentarios(bool dados, bool idUnicoActivado)
        {
            DadosActivado = dados;
            IdUnicoActivado = idUnicoActivado;
        }

        private ConfiguracionDeComentarios(){}
    }
}