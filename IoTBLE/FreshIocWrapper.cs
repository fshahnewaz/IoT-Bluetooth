using System;
using System.Linq.Expressions;
using IoTBLE.Mobile;
using FreshTinyIoC;
namespace IoTBLETool
{
    public class FreshIocWrapper : IIoTBLEIoc
    {
        public FreshIocWrapper ()
        {
        }

        public T Resolve<T> () where T : class
        {
            return FreshTinyIoC.FreshTinyIoCContainer.Current.Resolve<T> ();
        }

        public void Register<TRegister> (Expression<Func<TRegister>> constructor) where TRegister : class
        {
            FreshTinyIoCContainer.Current.Register<TRegister>( (FreshTinyIoCContainer container, NamedParameterOverloads namedParams) => {
                return constructor.Compile () ();
            });
        }

        public void RegisterSingleton<TRegister> (Expression<Func<TRegister>> constructor) where TRegister : class
        {
            var instance = constructor.Compile () ();
            FreshTinyIoC.FreshTinyIoCContainer.Current.Register<TRegister> (instance);
        }
    }
}
