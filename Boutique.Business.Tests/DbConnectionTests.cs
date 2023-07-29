using Boutique.DataAccess.Concrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Boutique.Business.Tests
{
    public class Tests
    {
        private readonly BoutiqueContext dbContext = new BoutiqueContext();
        private bool isConnectionOk;
        [SetUp]
        public void Setup()
        {
            isConnectionOk = dbContext.Database.CanConnect();
        }
        [Test]
        public void Db_Connection_Is_Ok()
        {
            Assert.That(isConnectionOk, Is.EqualTo(true));
        }
    }
}