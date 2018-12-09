using System;
using System.Collections.Generic;
using System.Text;

namespace DevWeek2018
{
    public static class SessionFactoryBuilder
    {

        public static NHibernate.ISessionFactory BuildSessionFactory()
        {

            FluentNHibernate.Cfg.Db.MsSqlConfiguration databaseConfiguration = FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2012
                .MaxFetchDepth(5)
                .IsolationLevel(System.Data.IsolationLevel.ReadCommitted)
                .ConnectionString("Data Source=127.0.0.1,1433,db_port;User ID=devweekuser;Password=devweekpwd;Database=DemoDB;Application Name=OragonSamplesTest")
                .ShowSql().FormatSql()
                .DefaultSchema("dbo");

            FluentNHibernate.Cfg.FluentConfiguration configuration = FluentNHibernate.Cfg.Fluently
                           .Configure()
                           .Database(databaseConfiguration)
                           .Cache(it =>
                               it.UseQueryCache()
                               .ProviderClass<NHibernate.Cache.HashtableCacheProvider>()
                           )                           
                           .Diagnostics(it =>
                               it.Enable(true)
                               .OutputToConsole()
                           )
                           .Mappings(it =>
                           {
                               it.FluentMappings.AddFromAssemblyOf<Data.Mappings.ClassroomMapper>();
                           })                           
                           .ExposeConfiguration(config =>
                           {                                

                               NHibernate.Tool.hbm2ddl.SchemaUpdate update = new NHibernate.Tool.hbm2ddl.SchemaUpdate(config);

                               update.Execute(true, true);                              

                           });
            ;

            NHibernate.ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory;

        }
    }
}
