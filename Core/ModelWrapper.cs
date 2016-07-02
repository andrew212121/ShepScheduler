using ShepScheduler.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Core
{
	public class ModelWrapper<T>: ViewModelBase where T:class
	{
		public T Model { get; private set;}

		public ModelWrapper(T Model)
		{
			if(Model == null)
			{
				throw new ArgumentNullException("Model cannot be null in ModelWrapper!");
			}
			this.Model = Model;
		}

		protected void SetValue<TValue>(TValue value, [CallerMemberName] string propertyName = null)
		{
			var propertyInfo = Model.GetType().GetProperty(propertyName);
			var currentValue = propertyInfo.GetValue(Model);
			propertyInfo.SetValue(Model, value);
			RaisePropertyChanged(propertyName);
		}

		protected TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
		{
			var propertyInfo = Model.GetType().GetProperty(propertyName);
			return (TValue)propertyInfo.GetValue(Model);

		}

		protected void RegisterCollection<TWrapper,TModel>(ObservableCollection<TWrapper> wrapperCollection, List<TModel> modelCollection)
			where TWrapper:ModelWrapper<TModel>
			where TModel:class
		{
			wrapperCollection.CollectionChanged += (s, e) =>
			{
				if(e.OldItems != null)
				{
					foreach(var item in e.OldItems.Cast<TWrapper>())
					{
						modelCollection.Remove(item.Model);
					}
				}
				if (e.NewItems != null)
				{
					foreach (var item in e.NewItems.Cast<TWrapper>())
					{
						modelCollection.Add(item.Model);
					}
				}
			};
		}
	}
}
