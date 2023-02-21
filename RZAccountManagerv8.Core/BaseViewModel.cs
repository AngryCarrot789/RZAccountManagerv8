using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RZAccountManagerv8.Core {
    /// <summary>
    /// An abstract class that implements <see cref="INotifyPropertyChanged"/>, allowing data binding between a ViewModel and a View, 
    /// along with some helper function to, for example, run an <see cref="Action"/> before or after the PropertyRaised event has been risen
    /// <para>
    ///     This class should normally be inherited by a ViewModel, such as a MainViewModel for the main view
    /// </para>
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Sets the given reference property's value to the newValue, and raises the PropertyChanged event, for the given property name
        /// </summary>
        /// <typeparam name="T">The property type</typeparam>
        /// <param name="property">A reference to property's holding field, whose value will be set with newValue</param>
        /// <param name="newValue">The possible new value of this property</param>
        /// <param name="propertyName">Property name. This will be auto-filled by the compiler</param>
        public void RaisePropertyChanged<T>(ref T property, T newValue, [CallerMemberName] string propertyName = null) {
            if (propertyName == null) {
                throw new ArgumentNullException(nameof(propertyName), "Property Name is null");
            }

            property = newValue;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Checks if the given property reference and the new value are the same. If so, nothing happens. Otherwise, sets the
        /// given reference property's value to the newValue, and raises the PropertyChanged event, for the given property name
        /// </summary>
        /// <typeparam name="T">The property type</typeparam>
        /// <param name="property">A reference to property's holding field, whose value will be set with newValue</param>
        /// <param name="newValue">The possible new value of this property</param>
        /// <param name="propertyName">Property name. This will be auto-filled by the compiler</param>
        public void RaisePropertyChangedCheckEqual<T>(ref T property, T newValue, [CallerMemberName] string propertyName = null) {
            if (propertyName == null) {
                throw new ArgumentNullException(nameof(propertyName), "Property Name is null");
            }

            if (EqualityComparer<T>.Default.Equals(property, newValue)) {
                return;
            }

            property = newValue;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the given property name
        /// </summary>
        /// <param name="propertyName">Property name. This will be auto-filled by the compiler</param>
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null) {
            if (propertyName == null) {
                throw new ArgumentNullException(nameof(propertyName), "Property Name is null");
            }

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Calls the given pre-property-changed callback, raises the property changed event, and invokes the given post-property-changed callback
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">The private field that is used for "setting"</param>
        /// <param name="newValue">The new value of this property</param>
        /// <param name="postChangedCallback">The method that gets called after the property changed, and contains the new value as a parameter</param>
        /// <param name="preChangedCallback">The method that gets called before the property changed, and contains the old value as a parameter</param>
        /// <param name="propertyName">Property name. This will be auto-filled by the compiler</param>
        public void RaisePropertyChangedWithCallback<T>(ref T property, T newValue, Action<T> postChangedCallback = null, Action<T> preChangedCallback = null, [CallerMemberName] string propertyName = null) {
            if (propertyName == null) {
                throw new ArgumentNullException(nameof(propertyName), "Property Name is null");
            }

            if (EqualityComparer<T>.Default.Equals(property, newValue)) {
                return;
            }

            preChangedCallback?.Invoke(property);
            property = newValue;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            postChangedCallback?.Invoke(property);
        }

        /// <summary>
        /// Calls the given pre-property-changed callback, raises the property changed event, and invokes the given post-property-changed callback
        /// </summary>
        /// <param name="callback">The method that gets called after the property changed, and contains the new value as a parameter</param>
        /// <param name="propertyName">Property name. This will be auto-filled by the compiler</param>
        public void RaisePropertyChangedWithCallback(Action callback, [CallerMemberName] string propertyName = null) {
            if (propertyName == null) {
                throw new ArgumentNullException(nameof(propertyName), "Property Name is null");
            }

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            callback?.Invoke();
        }

        /// <summary>
        /// If checkEquality is true, and the propery and newValue are equal, then nothing will happen.
        /// Otherwise, the given pre-property-changed callback will be invoked, then the property changed
        /// event will be raised, and then the post-property-changed callback will be raised
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">the private field that is used for "setting"</param>
        /// <param name="newValue">the new value of this property</param>
        /// <param name="postChangedCallback">The method that gets called after the property changed, and contains the new value as a parameter</param>
        /// <param name="preChangedCallback">The method that gets called before the property changed, and contains the old value as a parameter</param>
        /// <param name="propertyName">Property name. This will be auto-filled by the compiler</param>
        public void RaisePropertyChangedForceWithCallback<T>(ref T property, T newValue, Action<T> postChangedCallback = null, Action<T> preChangedCallback = null, [CallerMemberName] string propertyName = null) {
            if (propertyName == null) {
                throw new ArgumentNullException(nameof(propertyName), "Property Name is null");
            }

            preChangedCallback?.Invoke(property);
            property = newValue;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            postChangedCallback?.Invoke(property);
        }

        /// <summary>
        /// Calls the given pre-property-changed callback, raises the property changed event, and invokes the given post-property-changed callback
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">The private field that is used for "setting"</param>
        /// <param name="newValue">The new value of this property</param>
        /// <param name="postChangedCallback">The method that gets called after the property changed, and contains the new value as a parameter</param>
        /// <param name="preChangedCallback">The method that gets called before the property changed, and contains the old value as a parameter</param>
        /// <param name="propertyName">Property name. This will be auto-filled by the compiler</param>
        public void RaisePropertyChangedWithCallback<T>(ref T property, T newValue, Action postChangedCallback = null, Action preChangedCallback = null, [CallerMemberName] string propertyName = null) {
            if (propertyName == null) {
                throw new ArgumentNullException(nameof(propertyName), "Property Name is null");
            }

            if (EqualityComparer<T>.Default.Equals(property, newValue)) {
                return;
            }

            preChangedCallback?.Invoke();
            property = newValue;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            postChangedCallback?.Invoke();
        }

        /// <summary>
        /// If checkEquality is true, and the propery and newValue are equal, then nothing will happen.
        /// Otherwise, the given pre-property-changed callback will be invoked, then the property changed
        /// event will be raised, and then the post-property-changed callback will be raised
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">the private field that is used for "setting"</param>
        /// <param name="newValue">the new value of this property</param>
        /// <param name="postChangedCallback">The method that gets called after the property changed, and contains the new value as a parameter</param>
        /// <param name="preChangedCallback">The method that gets called before the property changed, and contains the old value as a parameter</param>
        /// <param name="propertyName">Property name. This will be auto-filled by the compiler</param>
        public void RaisePropertyChangedForceWithCallback<T>(ref T property, T newValue, Action postChangedCallback = null, Action preChangedCallback = null, [CallerMemberName] string propertyName = null) {
            if (propertyName == null) {
                throw new ArgumentNullException(nameof(propertyName), "Property Name is null");
            }

            preChangedCallback?.Invoke();
            property = newValue;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            postChangedCallback?.Invoke();
        }
    }
}
