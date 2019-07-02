using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace QIQO.Business.Client.Core.UI
{
    public class ModelWrapperBase<T> : NotifyDataErrorInfoBase,
      IValidatableTrackingObject, IValidatableObject
    {
        private readonly Dictionary<string, object> _originalValues;

        private readonly List<IValidatableTrackingObject> _trackingObjects;

        public ModelWrapperBase(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            Model = model;
            _originalValues = new Dictionary<string, object>();
            _trackingObjects = new List<IValidatableTrackingObject>();
            InitializeComplexProperties(model);
            InitializeCollectionProperties(model);
            Validate();
        }

        protected virtual void InitializeComplexProperties(T model)
        {
        }

        protected virtual void InitializeCollectionProperties(T model)
        {
        }

        public T Model { get; private set; }

        public bool IsChanged => _originalValues.Count > 0 || _trackingObjects.Any(t => t.IsChanged);

        public bool IsValid => !HasErrors && _trackingObjects.All(t => t.IsValid);

        public void AcceptChanges()
        {
            _originalValues.Clear();
            foreach (var trackingObject in _trackingObjects)
            {
                trackingObject.AcceptChanges();
            }
            RaisePropertyChanged("");
        }

        public void RejectChanges()
        {
            foreach (var originalValueEntry in _originalValues)
            {
                typeof(T).GetProperty(originalValueEntry.Key).SetValue(Model, originalValueEntry.Value);
            }
            _originalValues.Clear();
            foreach (var trackingObject in _trackingObjects)
            {
                trackingObject.RejectChanges();
            }
            Validate();
            RaisePropertyChanged("");
        }

        protected TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            var propertyInfo = Model.GetType().GetProperty(propertyName);
            return (TValue)propertyInfo.GetValue(Model);
        }

        protected TValue GetOriginalValue<TValue>(string propertyName)
        {
            return _originalValues.ContainsKey(propertyName)
              ? (TValue)_originalValues[propertyName]
              : GetValue<TValue>(propertyName);
        }

        protected bool GetIsChanged(string propertyName)
        {
            return _originalValues.ContainsKey(propertyName);
        }

        protected void SetValue<TValue>(TValue newValue,
          [CallerMemberName] string propertyName = null)
        {
            var propertyInfo = Model.GetType().GetProperty(propertyName);
            var currentValue = propertyInfo.GetValue(Model);
            if (!Equals(currentValue, newValue))
            {
                UpdateOriginalValue(currentValue, newValue, propertyName);
                propertyInfo.SetValue(Model, newValue);
                Validate();
                RaisePropertyChanged(propertyName);
                RaisePropertyChanged(propertyName + "IsChanged");
            }
        }

        private void Validate()
        {
            ClearErrors();

            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);
            Validator.TryValidateObject(this, context, results, true);

            if (results.Any())
            {
                var propertyNames = results.SelectMany(r => r.MemberNames).Distinct().ToList();

                foreach (var propertyName in propertyNames)
                {
                    Errors[propertyName] = results
                      .Where(r => r.MemberNames.Contains(propertyName))
                      .Select(r => r.ErrorMessage)
                      .Distinct()
                      .ToList();
                    OnErrorsChanged(propertyName);
                }
            }
            RaisePropertyChanged(nameof(IsValid));
        }

        private void UpdateOriginalValue(object currentValue, object newValue, string propertyName)
        {
            if (!_originalValues.ContainsKey(propertyName))
            {
                _originalValues.Add(propertyName, currentValue);
                RaisePropertyChanged("IsChanged");
            }
            else
            {
                if (Equals(_originalValues[propertyName], newValue))
                {
                    _originalValues.Remove(propertyName);
                    RaisePropertyChanged("IsChanged");
                }
            }
        }

        protected void RegisterCollection<TWrapper, TModel>(
         ChangeTrackingCollection<TWrapper> wrapperCollection,
         List<TModel> modelCollection) where TWrapper : ModelWrapperBase<TModel>
        {
            wrapperCollection.CollectionChanged += (s, e) =>
            {
                modelCollection.Clear();
                modelCollection.AddRange(wrapperCollection.Select(w => w.Model));
                Validate();
            };
            RegisterTrackingObject(wrapperCollection);
        }

        protected void RegisterComplex<TModel>(ModelWrapperBase<TModel> wrapper)
        {
            RegisterTrackingObject(wrapper);
        }

        private void RegisterTrackingObject(IValidatableTrackingObject trackingObject)
        {
            if (!_trackingObjects.Contains(trackingObject))
            {
                _trackingObjects.Add(trackingObject);
                trackingObject.PropertyChanged += TrackingObjectPropertyChanged;
            }
        }

        private void TrackingObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsChanged))
            {
                RaisePropertyChanged(nameof(IsChanged));
            }
            else if (e.PropertyName == nameof(IsValid))
            {
                RaisePropertyChanged(nameof(IsValid));
            }
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
