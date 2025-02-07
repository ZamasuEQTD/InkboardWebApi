using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Application.Core.Exceptions;
using Application.Core.Services;
using Application.Medias.Abstractions.Providers;
using Application.Medias.Services;
using Domain.Categorias.Models.ValueObjects;
using Domain.Core;
using Domain.Encuestas;
using Domain.Encuestas.Models.ValueObjects;
using Domain.Hilos;
using Domain.Hilos.Models;
using Domain.Media;
using Domain.Media.Models;
using Domain.Usuarios.Models.ValueObjects;

namespace Application.Hilos.Commands.PostearHilo
{
    public class PostearHiloCommandHiloCommandHandler : ICommandHandler<PostearHiloCommand, Guid>
    {
        static private readonly List<FileType> ARCHIVOS_SOPORTADOS = [
            FileType.Video,
            FileType.Imagen,
            FileType.Gif,
        ];

        private readonly MediaProcesador _mediaProcesador;
        private readonly IHilosRepository _hilosRepository;
        private readonly IMediasRepository _mediasRepository;
        private readonly IEncuestasRepository _encuestasRepository;
        private readonly ICurrentUser _user;
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmbedProcesador _embedProcesador;
        private readonly UserAutorProvider _userAutorProvider;
        public PostearHiloCommandHiloCommandHandler(IUnitOfWork unitOfWork, ICurrentUser user, IEncuestasRepository encuestasRepository, IMediasRepository mediasRepository, IHilosRepository hilosRepository, MediaProcesador mediaProcesador, EmbedProcesador embedProcesador, UserAutorProvider userAutorProvider)
        {
            _unitOfWork = unitOfWork;
            _embedProcesador = embedProcesador;
            _user = user;
            _encuestasRepository = encuestasRepository;
            _mediasRepository = mediasRepository;
            _hilosRepository = hilosRepository;
            _mediaProcesador = mediaProcesador;
            _userAutorProvider = userAutorProvider;
        }

        public async Task<Result<Guid>> Handle(PostearHiloCommand request, CancellationToken cancellationToken)
        {
            EncuestaId? encuestaId = null;

            if (request.Encuesta.Count != 0)
            {
                var encuesta = Encuesta.Create(request.Encuesta);

                _encuestasRepository.Add(encuesta);

                encuestaId = encuesta.Id;
            }

            MediaSpoileable reference;

            HashedMedia media;

            Console.WriteLine(request.File);
            if (request.File is not null)
            {

                if (!ARCHIVOS_SOPORTADOS.Contains(request.File.Type)) return HiloErrors.ArchivoDePortadaInvalida;

                media = await _mediaProcesador.Procesar(request.File);

                request.File.Stream.Dispose();
            }
            else if (request.Embed is not null)
            {
               media = await _embedProcesador.Procesar(request.Embed);
            }
            else return HiloErrors.SinPortada;

            reference = new MediaSpoileable(media.Id, media, request.Spoiler);

            _mediasRepository.Add(reference);

            Hilo hilo = new Hilo(
                new IdentityId(_user.UsuarioId),
                request.Titulo,
                request.Descripcion,
                new SubcategoriaId(request.Subcategoria),
                reference.Id,
                _userAutorProvider.GetAutorRole(),
                encuestaId,
                new(
                    request.DadosActivados,
                    request.IdUnicoAtivado
                )
            );

            _hilosRepository.Add(hilo);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success(hilo.Id.Value);
        }
    }
}