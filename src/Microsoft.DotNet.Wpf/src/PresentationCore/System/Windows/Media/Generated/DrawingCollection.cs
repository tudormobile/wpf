// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

//
//
// This file was generated, please do not edit it directly.
//
// Please see MilCodeGen.html for more information.
//

using MS.Internal;
using MS.Internal.KnownBoxes;
using MS.Internal.Collections;
using MS.Utility;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Media.Effects;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Markup;
using System.Windows.Media.Converters;

namespace System.Windows.Media
{
    /// <summary>
    /// A collection of Drawing objects.
    /// </summary>

    public sealed partial class DrawingCollection : Animatable, IList, IList<Drawing>
    {
        //------------------------------------------------------
        //
        //  Public Methods
        //
        //------------------------------------------------------

        #region Public Methods

        /// <summary>
        ///     Shadows inherited Clone() with a strongly typed
        ///     version for convenience.
        /// </summary>
        public new DrawingCollection Clone()
        {
            return (DrawingCollection)base.Clone();
        }

        /// <summary>
        ///     Shadows inherited CloneCurrentValue() with a strongly typed
        ///     version for convenience.
        /// </summary>
        public new DrawingCollection CloneCurrentValue()
        {
            return (DrawingCollection)base.CloneCurrentValue();
        }




        #endregion Public Methods

        //------------------------------------------------------
        //
        //  Public Properties
        //
        //------------------------------------------------------


        #region IList<T>

        /// <summary>
        ///     Adds "value" to the list
        /// </summary>
        public void Add(Drawing value)
        {
            AddHelper(value);
        }

        /// <summary>
        ///     Removes all elements from the list
        /// </summary>
        public void Clear()
        {
            WritePreamble();

            // As part of Clear()'ing the collection, we will iterate it and call
            // OnFreezablePropertyChanged and OnRemove for each item.
            // However, OnRemove assumes that the item to be removed has already been
            // pulled from the underlying collection.  To statisfy this condition,
            // we store the old collection and clear _collection before we call these methods.
            // As Clear() semantics do not include TrimToFit behavior, we create the new
            // collection storage at the same size as the previous.  This is to provide
            // as close as possible the same perf characteristics as less complicated collections.
            FrugalStructList<Drawing> oldCollection = _collection;
            _collection = new FrugalStructList<Drawing>(_collection.Capacity);

            for (int i = oldCollection.Count - 1; i >= 0; i--)
            {
                OnFreezablePropertyChanged(/* oldValue = */ oldCollection[i], /* newValue = */ null);

                // Fire the OnRemove handlers for each item.  We're not ensuring that
                // all OnRemove's get called if a resumable exception is thrown.
                // At this time, these call-outs are not public, so we do not handle exceptions.
                OnRemove( /* oldValue */ oldCollection[i]);
            }

            ++_version;
            WritePostscript();
        }

        /// <summary>
        ///     Determines if the list contains "value"
        /// </summary>
        public bool Contains(Drawing value)
        {
            ReadPreamble();

            return _collection.Contains(value);
        }

        /// <summary>
        ///     Returns the index of "value" in the list
        /// </summary>
        public int IndexOf(Drawing value)
        {
            ReadPreamble();

            return _collection.IndexOf(value);
        }

        /// <summary>
        ///     Inserts "value" into the list at the specified position
        /// </summary>
        public void Insert(int index, Drawing value)
        {
            if (value == null)
            {
                throw new System.ArgumentException(SR.Collection_NoNull);
            }

            WritePreamble();

            OnFreezablePropertyChanged(/* oldValue = */ null, /* newValue = */ value);

            _collection.Insert(index, value);
            OnInsert(value);


            ++_version;
            WritePostscript();
        }

        /// <summary>
        ///     Removes "value" from the list
        /// </summary>
        public bool Remove(Drawing value)
        {
            WritePreamble();

            // By design collections "succeed silently" if you attempt to remove an item
            // not in the collection.  Therefore we need to first verify the old value exists
            // before calling OnFreezablePropertyChanged.  Since we already need to locate
            // the item in the collection we keep the index and use RemoveAt(...) to do
            // the work.  (Windows OS #1016178)

            // We use the public IndexOf to guard our UIContext since OnFreezablePropertyChanged
            // is only called conditionally.  IList.IndexOf returns -1 if the value is not found.
            int index = IndexOf(value);

            if (index >= 0)
            {
                Drawing oldValue = _collection[index];

                OnFreezablePropertyChanged(oldValue, null);

                _collection.RemoveAt(index);

                OnRemove(oldValue);


                ++_version;
                WritePostscript();

                return true;
            }

            // Collection_Remove returns true, calls WritePostscript,
            // increments version, and does UpdateResource if it succeeds

            return false;
        }

        /// <summary>
        ///     Removes the element at the specified index
        /// </summary>
        public void RemoveAt(int index)
        {
            RemoveAtWithoutFiringPublicEvents(index);

            // RemoveAtWithoutFiringPublicEvents incremented the version

            WritePostscript();
        }


        /// <summary>
        ///     Removes the element at the specified index without firing
        ///     the public Changed event.
        ///     The caller - typically a public method - is responsible for calling
        ///     WritePostscript if appropriate.
        /// </summary>
        internal void RemoveAtWithoutFiringPublicEvents(int index)
        {
            WritePreamble();

            Drawing oldValue = _collection[ index ];

            OnFreezablePropertyChanged(oldValue, null);

            _collection.RemoveAt(index);

            OnRemove(oldValue);


            ++_version;

            // No WritePostScript to avoid firing the Changed event.
        }


        /// <summary>
        ///     Indexer for the collection
        /// </summary>
        public Drawing this[int index]
        {
            get
            {
                ReadPreamble();

                return _collection[index];
            }
            set
            {
                if (value == null)
                {
                    throw new System.ArgumentException(SR.Collection_NoNull);
                }

                WritePreamble();

                if (!Object.ReferenceEquals(_collection[ index ], value))
                {

                    Drawing oldValue = _collection[ index ];
                    OnFreezablePropertyChanged(oldValue, value);

                    _collection[ index ] = value;

                    OnSet(oldValue, value);
                }


                ++_version;
                WritePostscript();
            }
        }

        #endregion

        #region ICollection<T>

        /// <summary>
        ///     The number of elements contained in the collection.
        /// </summary>
        public int Count
        {
            get
            {
                ReadPreamble();

                return _collection.Count;
            }
        }

        /// <summary>
        ///     Copies the elements of the collection into "array" starting at "index"
        /// </summary>
        public void CopyTo(Drawing[] array, int index)
        {
            ReadPreamble();

            ArgumentNullException.ThrowIfNull(array);

            // This will not throw in the case that we are copying
            // from an empty collection.  This is consistent with the
            // BCL Collection implementations. (Windows 1587365)
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(index, array.Length - _collection.Count);

            _collection.CopyTo(array, index);
        }

        bool ICollection<Drawing>.IsReadOnly
        {
            get
            {
                ReadPreamble();

                return IsFrozen;
            }
        }

        #endregion

        #region IEnumerable<T>

        /// <summary>
        /// Returns an enumerator for the collection
        /// </summary>
        public Enumerator GetEnumerator()
        {
            ReadPreamble();

            return new Enumerator(this);
        }

        IEnumerator<Drawing> IEnumerable<Drawing>.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IList

        bool IList.IsReadOnly
        {
            get
            {
                return ((ICollection<Drawing>)this).IsReadOnly;
            }
        }

        bool IList.IsFixedSize
        {
            get
            {
                ReadPreamble();

                return IsFrozen;
            }
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                // Forwards to typed implementation
                this[index] = Cast(value);
            }
        }

        int IList.Add(object value)
        {
            // Forward to typed helper
            return AddHelper(Cast(value));
        }

        bool IList.Contains(object value)
        {
            return Contains(value as Drawing);
        }

        int IList.IndexOf(object value)
        {
            return IndexOf(value as Drawing);
        }

        void IList.Insert(int index, object value)
        {
            // Forward to IList<T> Insert
            Insert(index, Cast(value));
        }

        void IList.Remove(object value)
        {
            Remove(value as Drawing);
        }

        #endregion

        #region ICollection

        void ICollection.CopyTo(Array array, int index)
        {
            ReadPreamble();

            ArgumentNullException.ThrowIfNull(array);

            // This will not throw in the case that we are copying
            // from an empty collection.  This is consistent with the
            // BCL Collection implementations. (Windows 1587365)
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(index, array.Length - _collection.Count);

            if (array.Rank != 1)
            {
                throw new ArgumentException(SR.Collection_BadRank);
            }

            // Elsewhere in the collection we throw an AE when the type is
            // bad so we do it here as well to be consistent
            try
            {
                int count = _collection.Count;
                for (int i = 0; i < count; i++)
                {
                    array.SetValue(_collection[i], index + i);
                }
            }
            catch (InvalidCastException e)
            {
                throw new ArgumentException(SR.Format(SR.Collection_BadDestArray, this.GetType().Name), e);
            }
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                ReadPreamble();

                return IsFrozen || Dispatcher != null;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                ReadPreamble();
                return this;
            }
        }
        #endregion

        #region IEnumerable

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region Internal Helpers

        /// <summary>
        /// A frozen empty DrawingCollection.
        /// </summary>
        internal static DrawingCollection Empty
        {
            get
            {
                if (s_empty == null)
                {
                    DrawingCollection collection = new DrawingCollection();
                    collection.Freeze();
                    s_empty = collection;
                }

                return s_empty;
            }
        }

        /// <summary>
        /// Helper to return read only access.
        /// </summary>
        internal Drawing Internal_GetItem(int i)
        {
            return _collection[i];
        }

        /// <summary>
        ///     Freezable collections need to notify their contained Freezables
        ///     about the change in the InheritanceContext
        /// </summary>
        internal override void OnInheritanceContextChangedCore(EventArgs args)
        {
            base.OnInheritanceContextChangedCore(args);

            for (int i=0; i<this.Count; i++)
            {
                DependencyObject inheritanceChild = _collection[i];
                if (inheritanceChild!= null && inheritanceChild.InheritanceContext == this)
                {
                    inheritanceChild.OnInheritanceContextChanged(args);
                }
            }
        }

        #endregion

        #region Private Helpers

        private Drawing Cast(object value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (!(value is Drawing))
            {
                throw new System.ArgumentException(SR.Format(SR.Collection_BadType, this.GetType().Name, value.GetType().Name, "Drawing"));
            }

            return (Drawing) value;
        }

        // IList.Add returns int and IList<T>.Add does not. This
        // is called by both Adds and IList<T>'s just ignores the
        // integer
        private int AddHelper(Drawing value)
        {
            int index = AddWithoutFiringPublicEvents(value);

            // AddAtWithoutFiringPublicEvents incremented the version

            WritePostscript();

            return index;
        }

        internal int AddWithoutFiringPublicEvents(Drawing value)
        {
            int index = -1;

            if (value == null)
            {
                throw new System.ArgumentException(SR.Collection_NoNull);
            }
            WritePreamble();
            Drawing newValue = value;
            OnFreezablePropertyChanged(/* oldValue = */ null, newValue);
            index = _collection.Add(newValue);
            OnInsert(newValue);


            ++_version;

            // No WritePostScript to avoid firing the Changed event.

            return index;
        }

        internal event ItemInsertedHandler ItemInserted;
        internal event ItemRemovedHandler ItemRemoved;

        private void OnInsert(object item)
        {
            if (ItemInserted != null)
            {
                ItemInserted(this, item);
            }
        }

        private void OnRemove(object oldValue)
        {
            if (ItemRemoved != null)
            {
                ItemRemoved(this, oldValue);
            }
        }

        private void OnSet(object oldValue, object newValue)
        {
            OnInsert(newValue);
            OnRemove(oldValue);
        }

        #endregion Private Helpers

        private static DrawingCollection s_empty;


        #region Public Properties



        #endregion Public Properties

        //------------------------------------------------------
        //
        //  Protected Methods
        //
        //------------------------------------------------------

        #region Protected Methods

        /// <summary>
        /// Implementation of <see cref="System.Windows.Freezable.CreateInstanceCore">Freezable.CreateInstanceCore</see>.
        /// </summary>
        /// <returns>The new Freezable.</returns>
        protected override Freezable CreateInstanceCore()
        {
            return new DrawingCollection();
        }
        /// <summary>
        /// Implementation of Freezable.CloneCore()
        /// </summary>
        protected override void CloneCore(Freezable source)
        {
            DrawingCollection sourceDrawingCollection = (DrawingCollection) source;

            base.CloneCore(source);

            int count = sourceDrawingCollection._collection.Count;

            _collection = new FrugalStructList<Drawing>(count);

            for (int i = 0; i < count; i++)
            {
                Drawing newValue = (Drawing) sourceDrawingCollection._collection[i].Clone();
                OnFreezablePropertyChanged(/* oldValue = */ null, newValue);
                _collection.Add(newValue);
                OnInsert(newValue);
            }

        }
        /// <summary>
        /// Implementation of Freezable.CloneCurrentValueCore()
        /// </summary>
        protected override void CloneCurrentValueCore(Freezable source)
        {
            DrawingCollection sourceDrawingCollection = (DrawingCollection) source;

            base.CloneCurrentValueCore(source);

            int count = sourceDrawingCollection._collection.Count;

            _collection = new FrugalStructList<Drawing>(count);

            for (int i = 0; i < count; i++)
            {
                Drawing newValue = (Drawing) sourceDrawingCollection._collection[i].CloneCurrentValue();
                OnFreezablePropertyChanged(/* oldValue = */ null, newValue);
                _collection.Add(newValue);
                OnInsert(newValue);
            }

        }
        /// <summary>
        /// Implementation of Freezable.GetAsFrozenCore()
        /// </summary>
        protected override void GetAsFrozenCore(Freezable source)
        {
            DrawingCollection sourceDrawingCollection = (DrawingCollection) source;

            base.GetAsFrozenCore(source);

            int count = sourceDrawingCollection._collection.Count;

            _collection = new FrugalStructList<Drawing>(count);

            for (int i = 0; i < count; i++)
            {
                Drawing newValue = (Drawing) sourceDrawingCollection._collection[i].GetAsFrozen();
                OnFreezablePropertyChanged(/* oldValue = */ null, newValue);
                _collection.Add(newValue);
                OnInsert(newValue);
            }

        }
        /// <summary>
        /// Implementation of Freezable.GetCurrentValueAsFrozenCore()
        /// </summary>
        protected override void GetCurrentValueAsFrozenCore(Freezable source)
        {
            DrawingCollection sourceDrawingCollection = (DrawingCollection) source;

            base.GetCurrentValueAsFrozenCore(source);

            int count = sourceDrawingCollection._collection.Count;

            _collection = new FrugalStructList<Drawing>(count);

            for (int i = 0; i < count; i++)
            {
                Drawing newValue = (Drawing) sourceDrawingCollection._collection[i].GetCurrentValueAsFrozen();
                OnFreezablePropertyChanged(/* oldValue = */ null, newValue);
                _collection.Add(newValue);
                OnInsert(newValue);
            }

        }
        /// <summary>
        /// Implementation of <see cref="System.Windows.Freezable.FreezeCore">Freezable.FreezeCore</see>.
        /// </summary>
        protected override bool FreezeCore(bool isChecking)
        {
            bool canFreeze = base.FreezeCore(isChecking);

            int count = _collection.Count;
            for (int i = 0; i < count && canFreeze; i++)
            {
                canFreeze &= Freezable.Freeze(_collection[i], isChecking);
            }

            return canFreeze;
        }

        #endregion ProtectedMethods

        //------------------------------------------------------
        //
        //  Internal Methods
        //
        //------------------------------------------------------

        #region Internal Methods









        #endregion Internal Methods

        //------------------------------------------------------
        //
        //  Internal Properties
        //
        //------------------------------------------------------

        #region Internal Properties





        #endregion Internal Properties

        //------------------------------------------------------
        //
        //  Dependency Properties
        //
        //------------------------------------------------------

        #region Dependency Properties



        #endregion Dependency Properties

        //------------------------------------------------------
        //
        //  Internal Fields
        //
        //------------------------------------------------------

        #region Internal Fields




        internal FrugalStructList<Drawing> _collection;
        internal uint _version = 0;


        #endregion Internal Fields

        #region Enumerator
        /// <summary>
        /// Enumerates the items in a DrawingCollection
        /// </summary>
        public struct Enumerator : IEnumerator, IEnumerator<Drawing>
        {
            #region Constructor

            internal Enumerator(DrawingCollection list)
            {
                Debug.Assert(list != null, "list may not be null.");

                _list = list;
                _version = list._version;
                _index = -1;
                _current = default(Drawing);
            }

            #endregion

            #region Methods

            void IDisposable.Dispose()
            {

            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            /// true if the enumerator was successfully advanced to the next element,
            /// false if the enumerator has passed the end of the collection.
            /// </returns>
            public bool MoveNext()
            {
                _list.ReadPreamble();

                if (_version == _list._version)
                {
                    if (_index > -2 && _index < _list._collection.Count - 1)
                    {
                        _current = _list._collection[++_index];
                        return true;
                    }
                    else
                    {
                        _index = -2; // -2 indicates "past the end"
                        return false;
                    }
                }
                else
                {
                    throw new InvalidOperationException(SR.Enumerator_CollectionChanged);
                }
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the
            /// first element in the collection.
            /// </summary>
            public void Reset()
            {
                _list.ReadPreamble();

                if (_version == _list._version)
                {
                    _index = -1;
                }
                else
                {
                    throw new InvalidOperationException(SR.Enumerator_CollectionChanged);
                }
            }

            #endregion

            #region Properties

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }

            /// <summary>
            /// Current element
            ///
            /// The behavior of IEnumerable&lt;T>.Current is undefined
            /// before the first MoveNext and after we have walked
            /// off the end of the list. However, the IEnumerable.Current
            /// contract requires that we throw exceptions
            /// </summary>
            public Drawing Current
            {
                get
                {
                    if (_index > -1)
                    {
                        return _current;
                    }
                    else if (_index == -1)
                    {
                        throw new InvalidOperationException(SR.Enumerator_NotStarted);
                    }
                    else
                    {
                        Debug.Assert(_index == -2, "expected -2, got " + _index + "\n");
                        throw new InvalidOperationException(SR.Enumerator_ReachedEnd);
                    }
                }
            }

            #endregion

            #region Data
            private Drawing _current;
            private DrawingCollection _list;
            private uint _version;
            private int _index;
            #endregion
        }
        #endregion

        #region Constructors

        //------------------------------------------------------
        //
        //  Constructors
        //
        //------------------------------------------------------


        /// <summary>
        /// Initializes a new instance that is empty.
        /// </summary>
        public DrawingCollection()
        {
            _collection = new FrugalStructList<Drawing>();
        }

        /// <summary>
        /// Initializes a new instance that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity"> int - The number of elements that the new list is initially capable of storing. </param>
        public DrawingCollection(int capacity)
        {
            _collection = new FrugalStructList<Drawing>(capacity);
        }

        /// <summary>
        /// Creates a DrawingCollection with all of the same elements as collection
        /// </summary>
        public DrawingCollection(IEnumerable<Drawing> collection)
        {
            // The WritePreamble and WritePostscript aren't technically necessary
            // in the constructor as of 1/20/05 but they are put here in case
            // their behavior changes at a later date

            WritePreamble();

            ArgumentNullException.ThrowIfNull(collection);

            bool needsItemValidation = true;
            ICollection<Drawing> icollectionOfT = collection as ICollection<Drawing>;

            if (icollectionOfT != null)
            {
                _collection = new FrugalStructList<Drawing>(icollectionOfT);
            }
            else
            {
                ICollection icollection = collection as ICollection;

                if (icollection != null) // an IC but not and IC<T>
                {
                    _collection = new FrugalStructList<Drawing>(icollection);
                }
                else // not a IC or IC<T> so fall back to the slower Add
                {
                    _collection = new FrugalStructList<Drawing>();

                    foreach (Drawing item in collection)
                    {
                        if (item == null)
                        {
                            throw new System.ArgumentException(SR.Collection_NoNull);
                        }
                        Drawing newValue = item;
                        OnFreezablePropertyChanged(/* oldValue = */ null, newValue);
                        _collection.Add(newValue);
                        OnInsert(newValue);
                    }

                    needsItemValidation = false;
                }
            }

            if (needsItemValidation)
            {
                foreach (Drawing item in collection)
                {
                    if (item == null)
                    {
                        throw new System.ArgumentException(SR.Collection_NoNull);
                    }
                    OnFreezablePropertyChanged(/* oldValue = */ null, item);
                    OnInsert(item);
                }
            }


            WritePostscript();
        }

        #endregion Constructors
    }
}
