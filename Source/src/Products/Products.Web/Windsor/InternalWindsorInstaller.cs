﻿using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CompositeUI.Infrastructure;

namespace CompositeUI.Products.Web.Windsor
{
    internal class InternalWindsorInstaller : IWindsorInstaller
    {
        private readonly string _path;

        public InternalWindsorInstaller(string path)
        {
            _path = path;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            RegisterWebProjectClasses(container);
            //RegisterAllPublicClasses(container);
        }

        private void RegisterWebProjectClasses(IWindsorContainer container)
        {
            container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient());
            container.Register(Component.For<ImportantClass>());
        }

        private void RegisterAllPublicClasses(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssemblyInDirectory(new AssemblyFilter(_path).FilterByName(p => p.Name.StartsWith("CompositeUI") && !p.Name.StartsWith("CompositeUI.Products.Web")))
                    .Pick()
                    .Unless(p => typeof(IApplicationConfiguration).IsAssignableFrom(p))
                    .WithService.AllInterfaces()
                    .LifestylePerWebRequest());

            container.Register(
                Classes.FromAssemblyInDirectory(new AssemblyFilter(_path).FilterByName(p => p.Name.StartsWith("CompositeUI") && !p.Name.StartsWith("CompositeUI.Products.Web")))
                    .BasedOn<IApplicationConfiguration>()
                    .WithService.AllInterfaces());
        }
    }
}