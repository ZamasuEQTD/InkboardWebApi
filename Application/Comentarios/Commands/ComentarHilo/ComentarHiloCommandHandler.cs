

using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Application.Medias.Abstractions.Providers;
using Application.Medias.Services;
using Domain.Categorias;
using Domain.Comentarios.Abstractions.Services;
using Domain.Comentarios.Models;
using Domain.Comentarios.Models.ValueObjects;
using Domain.Comentarios.Services;
using Domain.Core;
using Domain.Core.Abstractions;
using Domain.Core.Services;
using Domain.Hilos;
using Domain.Hilos.Models;
using Domain.Hilos.Models.ValueObjects;
using Domain.Media;
using Domain.Media.Models;
using Domain.Usuarios.Models.ValueObjects;

namespace Application.Comentarios.Commands.ComentarHilo {
    public class ComentarHiloCommandHandler : ICommandHandler<ComentarHiloCommand>
    {
         static private readonly List<FileType> ARCHIVOS_SOPORTADOS = [
            FileType.Video,
            FileType.Imagen,
            FileType.Gif,
        ];

        private readonly IHilosRepository _hilosRepository;
        private readonly ICategoriasRepository _categoriasRepository;
        private readonly ICurrentUser _user;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IColorService _colorService;
        private readonly MediaProcesador _mediaProcesador;
        private readonly IMediasRepository _mediasRepository;
        private readonly EmbedProcesador _embedProcesador;
        public ComentarHiloCommandHandler(IUnitOfWork unitOfWork, IHilosRepository hilosRepository, ICurrentUser userContext, ICategoriasRepository categoriasRepository, IDateTimeProvider timeProvider, IColorService colorService, MediaProcesador mediaProcesador, IMediasRepository mediasRepository, EmbedProcesador embedProcesador)
        {
            _unitOfWork = unitOfWork;
            _hilosRepository = hilosRepository;
            _user = userContext;
            _categoriasRepository = categoriasRepository;
            _timeProvider = timeProvider;
            _colorService = colorService;
            _mediaProcesador = mediaProcesador;
            _mediasRepository = mediasRepository;
            _embedProcesador = embedProcesador;
        }

        public async Task<Result> Handle(ComentarHiloCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new HiloId(request.Hilo));

            if (hilo is null) return HiloErrors.NoEncontrado;

            if (!hilo.EstaActivo) return HiloErrors.HiloInactivo;

            Result<Texto> texto = Texto.Create(request.Texto);

            if (texto.IsFailure) return texto.Error;

            MediaSpoileable? reference = null;

            HashedMedia? media = null;

            if (request.File is not null)
            {
                if (!ARCHIVOS_SOPORTADOS.Contains(request.File.Type)) return HiloErrors.ArchivoDePortadaInvalida;

                media = await _mediaProcesador.Procesar(request.File);

                request.File.Stream.Dispose();
            }
            else if (request.EmbedFile is not null)
            {
               media = await _embedProcesador.Procesar(request.EmbedFile);
            }

            if(media is not null){
                reference = new MediaSpoileable(media.Id, media, request.Spoiler);
                
                _mediasRepository.Add(reference);
            }
            
            Comentario c = new Comentario(
                hilo.Id,
                new IdentityId(_user.UsuarioId),
                await _colorService.GenerarColor(hilo.SubcategoriaId),
                texto.Value,
                TagsService.GenerarTag(),
                reference?.Id,
                hilo.Configuracion.DadosActivado? DadosService.Generar() : null,
                hilo.Configuracion.IdUnicoActivado ? TagsService.GenerarTagUnico(RandomSeedGeneratorService.GenerarSeed( hilo.Id.Value.ToString() + new IdentityId(_user.UsuarioId).Value.ToString())) :null
            );

            Result result = hilo.Comentar(c, _timeProvider.UtcNow);

            if (result.IsFailure) return result.Error;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}