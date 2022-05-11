using IncıBeyazEsya.Business.Abstract;
using IncıBeyazEsya.Business.Concrate.Managers;
using IncıBeyazEsya.DataAcces.Abstract;
using IncıBeyazEsya.DataAcces.Concrate;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncıBeyazEsya.Business.DepencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductService>().To<ProductManager>().InSingletonScope();
            Bind<IOrderDetailsService>().To<OrdersDetailManagers>().InSingletonScope();
            Bind<IOrdersService>().To<OrdersManager>().InSingletonScope();
            Bind<ITownsService>().To<TownsManager>().InSingletonScope();
            Bind<IPaymentsService>().To<PaymentsManager>().InSingletonScope();
            Bind<IUsersService>().To<UsersManager>().InSingletonScope();
            Bind<IInvoicesDetailService>().To<InvoiceDetailManager>().InSingletonScope();
            Bind<IInvoicesService>().To<InvoicesManager>().InSingletonScope();
            Bind<IDistrictsService>().To<DistrictsManager>().InSingletonScope();
            Bind<ICitiesService>().To<CitiesManager>().InSingletonScope();
            Bind<IAdressService>().To<AdressManager>().InSingletonScope();
            Bind<INorthwindContext>().To<NorthwindContext>().InSingletonScope();
            Bind<IStoredProcedureService>().To<StoredProcedureManager>().InSingletonScope();

        }
    }
}
