using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Proxies;
using Unity;

namespace QIQO.Business.Client.Tests
{
    [TestClass]
    public class ProxyUnitTests
    {
        private IUnityContainer container;
        [TestInitialize]
        public void Initialize()
        {
            container = new UnityContainer();
            container.RegisterType<IAccountService, AccountClient>();
            // Unity.Container = container;
        }

        [TestMethod]
        public void GetAccountByIDTest()
        {
            //AccountClient proxy = new AccountClient("BasicHttpBinding_IAccountService");
            //proxy.FindAccountByCompany(new Entities.Company() { CompanyKey = 1 });
            //proxy.GetAccountByID(3);
            //proxy.Open();
            //Account account = proxy.GetAccountByID(3);
            //proxy.Close();

            using (var proxy = new AccountClient("BasicHttpBinding_IAccountService"))
            {
                var account = proxy.GetAccountByID(3, true);
                Assert.AreEqual("Sportsman's Grille - Brentwood", account.AccountName);
            };

            //Console.WriteLine(account.AccountName);

            //Assert.AreEqual("Sportsman's Grille - Brentwood", account.AccountName);
        }

        [TestMethod]
        public void ServiceFactoryTest()
        {
            IServiceFactory sf = new ServiceFactory(container);
            var proxy = sf.CreateClient<IAccountService>();

            using (proxy)
            {
                var account = proxy.GetAccountByID(3, true);
                Assert.AreEqual("Sportsman's Grille - Brentwood", account.AccountName);
            };
        }
    }
}
