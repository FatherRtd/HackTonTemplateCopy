using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using HackTonTemplate.Maps;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace HackTonTemplate.Extensions
{
    public static class IServiceCollectionExtensions
    {
        private static string DBName = "DataBase.db";
        public static IServiceCollection AddFluentNibernateConnection(this IServiceCollection services)
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard
                    .UsingFile(DBName))
                .Mappings(x => FillMappings(x.FluentMappings))
                .BuildSessionFactory();
            services.AddSingleton(x => sessionFactory);
            services.AddScoped(x => sessionFactory.OpenSession());
            return services;
        }

        private static void BuildSchema(Configuration config)
        {
            new SchemaExport(config)
                .Create(false, true);
        }

        private static FluentMappingsContainer FillMappings(FluentMappingsContainer container)
        {
            return container.AddFromAssembly(typeof(EventMap).Assembly);
        }
    }
}
