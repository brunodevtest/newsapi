using NewsServer.Controllers;
using NewsServer.Model;
using Microsoft.AspNetCore.Mvc;

using Moq;

namespace libraryAPI.Test
{
    public class ArticleControllerTest
    {

        ArticleController _controller;

        public ArticleControllerTest()
        {
            _controller = new ArticleController();
        }

        [Fact]
        public async void GetLatestTest()
        {
            // Arrange
            var page = 1;
            var moqList = new List<Article>();
            moqList.Add(new Article { id = 1, title = "Sample Article", by = "Bruno" });
            moqList.Add(new Article { id = 2, title = "Article Test", by = "John" });
            var mockArticleService = new Mock<IArticleService>();
            mockArticleService.Setup(service => service.GetLatest(page)).ReturnsAsync(moqList);

            // Act
            var list = await mockArticleService.Object.GetLatest(page);

            // Assert
            Assert.NotNull(list);
            Assert.Equal(list.Count, 2);
            Assert.Equal(list[0].title, "Sample Article");
            Assert.Equal(list[0].by, "Bruno");
        }
    }

    public abstract class IArticleService : ArticleController
    {
        public abstract Task<List<Article>> GetLatest(int page);
    }

}