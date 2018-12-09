using DevWeek2018.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevWeek2018
{
    class Program
    {
        static void Main(string[] args)
        {
            using (NHibernate.ISessionFactory sessionFactory = SessionFactoryBuilder.BuildSessionFactory())
            {

                using (NHibernate.ISession session = sessionFactory.OpenSession())
                {
                    ///session.FlushMode = NHibernate.FlushMode.

                    //=============================================================================================
                    //using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    //{

                    //    Language languageEN = new Language() { LanguageId = "EN", Name = "English" };
                    //    Language languageES = new Language() { LanguageId = "ES", Name = "Spanish" };

                    //    Classroom classroom1 = new Classroom() { Name = "Class 1" };
                    //    Classroom classroom2 = new Classroom() { Name = "Class 2" };

                    //    Do(session.Save, languageEN, languageES, classroom1, classroom2);


                    //    transaction.Commit();
                    //}
                    //=============================================================================================


                    //=============================================================================================
                    //var EN = session.Get<Language>("EN");
                    //=============================================================================================


                    //=============================================================================================
                    Classroom classroom3 = new Classroom() { Name = "Class 3" };
                    Language languagePT = new Language() { LanguageId = "PT", Name = "Portuguese" };
                    Student studentLuiz = new Student()
                    {
                        FullName = "Luis Carlos Faria",
                        Language = languagePT,
                        Classrooms = new List<Classroom>() {
                            classroom3
                        }
                    };

                    Student studentTatiana = new Student()
                    {
                        FullName = "Tatiana",
                        Language = languagePT,
                    };

                    Classroom classroom4 = new Classroom()
                    {
                        Name = "Class 4",
                        Students = new List<Student> { studentTatiana }

                    };

                  

                    Do(session.Save, classroom3, classroom4, languagePT, studentLuiz, studentTatiana);
                    session.Flush();
                    //=============================================================================================


                    Console.WriteLine("Hello World!");
                }


            }

        }

        private static void Do(Action<object> action, params IDomainEntity[] entities)
        {
            if (entities != null && entities.Any())
            {
                foreach (IDomainEntity entity in entities)
                {
                    action(entity);
                }
            }
        }

        private static void Do(Func<object, object> func, params IDomainEntity[] entities)
        {
            if (entities != null && entities.Any())
            {
                foreach (IDomainEntity entity in entities)
                {
                    func(entity);
                }
            }
        }



    }
}
