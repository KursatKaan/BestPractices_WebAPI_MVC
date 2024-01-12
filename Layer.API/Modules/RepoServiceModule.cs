using Autofac;
using Layer.Caching;
using Layer.Core.Abstract.Repositories;
using Layer.Core.Abstract.Services;
using Layer.Core.Abstract.UnitOfWorks;
using Layer.Repository;
using Layer.Repository.Repositories;
using Layer.Repository.UnitOfWorks;
using Layer.Service.Mapping;
using Layer.Service.Services;
using System.Reflection;
using Module = Autofac.Module;

namespace Layer.API.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();


            // Assembly, kaynak dosyaların derlenip bir araya getirilmiş halidir.

            var apiAssembly = Assembly.GetExecutingAssembly(); // Şu anda çalışan uygulamanın assembly'sini elde eder. Yani, kodun derlendiği ve çalıştığı birimdir. 
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext)); // Belirli bir "AppDbContext" tipinin tanımlandığı assembly'yi elde eder.
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile)); // Belirli bir "MapProfile" tipinin tanımlandığı assembly'yi elde eder.

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly) //Bu metod, belirtilen assembly'lerdeki tipleri kaydetmek için kullanılır.
                .Where(x => x.Name.EndsWith("Repository")) // Sonu "Repository" ile biten sınıfları al.
                .AsImplementedInterfaces() // Bu sınıfların interfaceleri'ne otomatik olarak bağlan.
                .InstancePerLifetimeScope(); // Her bağlı lifetime scope için bir örnek oluşturulmasını sağlar.

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Service")) // Sonu "Service" ile biten sınıfları al.
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductServiceWithCaching>().As<IProductService>().InstancePerLifetimeScope();
        }
    }
}



