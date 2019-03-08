using Cashback.API.Controllers;
using Cashback.Infrastructure.Data.Interfaces;
using Cashback.Infrastructure.Data.Models;
using Cashback.Shared;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cashback.API.Tests.Controllers
{
    public class CatalogControllerTests
    {
        private readonly CatalogController _catalogController;
        private readonly Mock<IGenreRepository> _genreRepository;
        private readonly Mock<ICatalogRepository> _catalogRepository;

        public CatalogControllerTests()
        {
            _genreRepository = new Mock<IGenreRepository>();
            _catalogRepository = new Mock<ICatalogRepository>();

            _catalogController = new CatalogController(_genreRepository.Object,
                _catalogRepository.Object);
        }

        [Theory]
        [InlineData("rock", 0, 10)]
        public async Task MustCallListCatalogAsync(string genre, int offset, int limit)
        {
            var items = new PagedList<Album>(limit, offset, new List<Album>()
            {
                new Album()
                {
                    Id = 1,
                    AlbumName = "Fake album",
                    Identifier = "",
                    Price = 10
                }
            });
            _catalogRepository.Setup(s => s.ListAlbums(genre, offset, limit))
                .Returns(Task.FromResult((IPagedList<Album>)items));

            var result = await _catalogController.ListCatalog(genre,
                                                        offset,
                                                        limit);

            _catalogRepository.Verify(v => v.ListAlbums(It.IsAny<string>(),
                                                        It.IsAny<int>(),
                                                        It.IsAny<int>()));
            _catalogRepository.VerifyNoOtherCalls();

            Assert.IsAssignableFrom<IPagedList<AlbumViewModel>>(result);
        }

        [Fact]
        public async Task MustCallListGenres()
        {
            IEnumerable<Genre> items = new List<Genre>();
            _genreRepository.Setup(s => s.ListAllGenres())
                .Returns(Task.FromResult(items));

            var result = await _catalogController.ListGenres();

            _genreRepository.Verify(v => v.ListAllGenres());
            _genreRepository.VerifyNoOtherCalls();

            Assert.IsAssignableFrom<IEnumerable<GenreViewModel>>(result);
        }

        [Fact]
        public async Task MustCallGetAlbumById()
        {
            _catalogRepository.Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new Album() {
                }));

            var result = await _catalogController.GetAlbumById(It.IsAny<int>());

            _catalogRepository.Verify(v => v.GetByIdAsync(It.IsAny<int>()));
            _catalogRepository.VerifyNoOtherCalls();

            Assert.IsType<AlbumViewModel>(result);
        }
    }
}
