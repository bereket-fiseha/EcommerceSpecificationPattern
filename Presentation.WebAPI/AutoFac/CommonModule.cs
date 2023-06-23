using Autofac;
using Domain.Interface.Repository.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RIS.WebAPI.DI
{
    public class CommonModule:Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
           // builder.RegisterType<IGenericRepository>().As<IGenericRepository>();

            builder.RegisterAssemblyTypes(Assembly.Load("Application")).Where(x => x.Name.EndsWith("Service"))
                .As(x => x.GetInterfaces().FirstOrDefault(y => y.Name == "I" + x.Name));
            builder.RegisterAssemblyTypes(Assembly.Load("Infrastructure")).Where(x => x.Name.EndsWith("Repository"))
               .As(x => x.GetInterfaces().FirstOrDefault(y => y.Name == "I" + x.Name));


        }
    }
}
