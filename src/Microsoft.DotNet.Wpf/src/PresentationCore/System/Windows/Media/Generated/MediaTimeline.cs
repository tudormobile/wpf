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
    public partial class MediaTimeline : Timeline
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
        public new MediaTimeline Clone()
        {
            return (MediaTimeline)base.Clone();
        }

        /// <summary>
        ///     Shadows inherited CloneCurrentValue() with a strongly typed
        ///     version for convenience.
        /// </summary>
        public new MediaTimeline CloneCurrentValue()
        {
            return (MediaTimeline)base.CloneCurrentValue();
        }




        #endregion Public Methods

        //------------------------------------------------------
        //
        //  Public Properties
        //
        //------------------------------------------------------




        #region Public Properties

        /// <summary>
        ///     Source - Uri.  Default value is null.
        /// </summary>
        public Uri Source
        {
            get
            {
                return (Uri) GetValue(SourceProperty);
            }
            set
            {
                SetValueInternal(SourceProperty, value);
            }
        }

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
            return new MediaTimeline();
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

        //
        //  This property finds the correct initial size for the _effectiveValues store on the
        //  current DependencyObject as a performance optimization
        //
        //  This includes:
        //    Source
        //
        internal override int EffectiveValuesInitialSize
        {
            get
            {
                return 1;
            }
        }



        #endregion Internal Properties

        //------------------------------------------------------
        //
        //  Dependency Properties
        //
        //------------------------------------------------------

        #region Dependency Properties

        /// <summary>
        ///     The DependencyProperty for the MediaTimeline.Source property.
        /// </summary>
        public static readonly DependencyProperty SourceProperty;

        #endregion Dependency Properties

        //------------------------------------------------------
        //
        //  Internal Fields
        //
        //------------------------------------------------------

        #region Internal Fields





        internal static Uri s_Source = null;

        #endregion Internal Fields



        #region Constructors

        //------------------------------------------------------
        //
        //  Constructors
        //
        //------------------------------------------------------

        static MediaTimeline()
        {
            // We check our static default fields which are of type Freezable
            // to make sure that they are not mutable, otherwise we will throw
            // if these get touched by more than one thread in the lifetime
            // of your app.


            // Initializations
            Type typeofThis = typeof(MediaTimeline);
            SourceProperty =
                  RegisterProperty("Source",
                                   typeof(Uri),
                                   typeofThis,
                                   null,
                                   null,
                                   null,
                                   /* isIndependentlyAnimated  = */ false,
                                   /* coerceValueCallback */ null);
        }



        #endregion Constructors
    }
}
