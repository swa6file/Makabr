using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;
using Ninject.Modules;
using BusinessLogical.Interfaces;
using BusinessLogical.Services;
using BusinessLogical.Validators;

namespace BusinessLogical
{
    public class SimpleConfigModule : NinjectModule
    {
        public override void Load()
        {
            string connectionString = "Data Source=SOVLE\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";
            Bind<string>().ToConstant(connectionString)
                .WhenInjectedInto<EntityRepository<Worker>>();
            Bind<IRepository<Worker>>().To<EntityRepository<Worker>>().InSingletonScope();

            Bind<IWorkerValidator>().To<WorkerValidator>();
            Bind<IWorkerFilterService>().To<WorkerFilterService>();
            Bind<IConstructionInfoService>().To<ConstructionInfoService>();
            Bind<ISpecializationService>().To<SpecializationService>();

            Bind<INameValidator>().To<NameValidator>();
            Bind<IAgeValidator>().To<AgeValidator>();
            Bind<ISalaryValidator>().To<SalaryValidator>();
            Bind<ILogic>().To<Logic>().InSingletonScope();
        }
    }
}
