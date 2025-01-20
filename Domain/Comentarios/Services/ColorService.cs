using Domain.Categorias;
using Domain.Categorias.Models;
using Domain.Categorias.Models.ValueObjects;
using Domain.Comentarios.Abstractions.Services;
using Domain.Comentarios.Models.ValueObjects;
using Domain.Core;
using Domain.Core.Abstractions;

namespace Domain.Comentarios.Services
{
    
    public class ColorService : IColorService
    {
        static private readonly List<WeightValue<Color>> _colors = [
                new WeightValue<Color>(20,Color.Amarillo),
                new WeightValue<Color>(20,Color.Azul),
                new WeightValue<Color>(20,Color.Rojo),
                new WeightValue<Color>(20,Color.Verde),
                new WeightValue<Color>(5,Color.Multi),
                new WeightValue<Color>(5,Color.Invertido),
                new WeightValue<Color>(1,Color.White),
            ];
        private readonly IDateTimeProvider _time;
        private readonly ICategoriasRepository _categoriasRepository;
        public ColorService(IDateTimeProvider time, ICategoriasRepository categoriasRepository)
        {
            _time = time;
            _categoriasRepository = categoriasRepository;
        }
        public async Task<Color> GenerarColor(SubcategoriaId subcategoria)
        {
            List<WeightValue<Color>> colors = [.._colors];

            Subcategoria? _subcategoria = await _categoriasRepository.GetSubcategoria(subcategoria);
            
            if(_time.UtcNow.Hour > 22 || _time.UtcNow.Hour < 5 && _subcategoria!.EsParanormal)
            {
                colors.Add(new WeightValue<Color>(1,Color.Black));
            }

            return colors.Pick();
        }
    }

}