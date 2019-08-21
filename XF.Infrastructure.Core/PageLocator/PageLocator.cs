using FormsControls.Base;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace XF.Infrastructure.Core
{
    public class PageLocator : IPageLocator
    {

        public virtual AnimationPage CreatePage(Type pageType)
        {
            try
            {
                return Activator.CreateInstance(pageType) as AnimationPage;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }


        public virtual IViewModel CreateViewModel(Type viewModelType)
        {
            try
            {
                return Activator.CreateInstance(viewModelType) as IViewModel;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        public virtual IModel CreateModel(Type modelType)
        {
            try
            {
                return Activator.CreateInstance(modelType) as IModel;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        protected virtual Type FindPageForViewModel(Type viewModelType)
        {
            var pageTypeName = viewModelType
                .AssemblyQualifiedName
                .Replace("ViewModel", "Page");

            var pageType = Type.GetType(pageTypeName);
            if (pageType == null)
                throw new ArgumentException(pageTypeName + " type not exist");

            return pageType;
        }

        protected virtual Type FindModelForViewModel(Type viewModelType)
        {
            var modelTypeName = viewModelType
                .AssemblyQualifiedName
                .Replace("ViewModel", "Model");

            var modelType = Type.GetType(modelTypeName);
            if (modelType == null)
                throw new ArgumentException(modelType + " type not exist");

            return modelType;
        }


        public AnimationPage ResolvePageAndViewModel(Type viewModelType, object args)
        {
            var viewModel = this.CreateViewModel(viewModelType);
            viewModel.Init(args);
            return this.ResolvePage(viewModel);
        }

        
        public AnimationPage ResolvePage(IViewModel viewModel)
        {
            var pageType = this.FindPageForViewModel(viewModel.GetType());
            var page = this.CreatePage(pageType);
            page.BindViewModel(viewModel);
            return page;
        }

        public ContentPage ResolveContentPage(IViewModel viewModel)
        {
            var pageType = FindPageForViewModel(viewModel.GetType());
            var page = this.CreatePage(pageType);
            return page as ContentPage;
        }
    }
}
