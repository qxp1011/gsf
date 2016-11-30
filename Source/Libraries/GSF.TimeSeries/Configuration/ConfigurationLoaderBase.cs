﻿//******************************************************************************************************
//  ConfigurationLoaderBase.cs - Gbtc
//
//  Copyright © 2016, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the MIT License (MIT), the "License"; you may not use this
//  file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://opensource.org/licenses/MIT
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  11/30/2016 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.Data;
using GSF.Diagnostics;
using GSF.TimeSeries.Adapters;

namespace GSF.TimeSeries.Configuration
{
    /// <summary>
    /// Represents a base class for <see cref="IConfigurationLoader"/> implementations.
    /// </summary>
    public abstract class ConfigurationLoaderBase : IConfigurationLoader
    {
        #region [ Members ]

        // Events

        /// <summary>
        /// Occurs when the configuration loader has a message to provide about its current status.
        /// </summary>
        public event EventHandler<EventArgs<string>> StatusMessage;

        /// <summary>
        /// Occurs when the configuration loader encounters a non-catastrophic exception.
        /// </summary>
        public event EventHandler<EventArgs<Exception>> ProcessException;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Creates a new <see cref="ConfigurationLoaderBase"/> instance.
        /// </summary>
        protected ConfigurationLoaderBase()
        {
            Log = Logger.CreatePublisher(GetType(), MessageClass.Application);
            Log.InitialStackMessages = new LogStackMessages("ComponentName", GetType().Name);
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Log messages generated by an adapter.
        /// </summary>
        protected LogPublisher Log { get; }

        /// <summary>
        /// Gets the flag that indicates whether augmentation is supported by this configuration loader.
        /// </summary>
        public abstract bool CanAugment
        {
            get;
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Loads the entire configuration data set from scratch.
        /// </summary>
        /// <returns>The configuration data set.</returns>
        public abstract DataSet Load();

        /// <summary>
        /// Augments the given configuration data set with the changes
        /// tracked since the version of the given configuration data set.
        /// </summary>
        /// <param name="configuration">The configuration data set to be augmented.</param>
        public abstract void Augment(DataSet configuration);

        /// <summary>
        /// Raises the <see cref="StatusMessage"/> event and sends this data to the <see cref="Logger"/>.
        /// </summary>
        /// <param name="level">The <see cref="MessageLevel"/> to assign to this message</param>
        /// <param name="status">New status message.</param>
        /// <param name="eventName">A fixed string to classify this event; defaults to <c>null</c>.</param>
        /// <param name="flags"><see cref="MessageFlags"/> to use, if any; defaults to <see cref="MessageFlags.None"/>.</param>
        /// <remarks>
        /// <see pref="eventName"/> should be a constant string value associated with what type of message is being
        /// generated. In general, there should only be a few dozen distinct event names per class. Exceeding this
        /// threshold will cause the EventName to be replaced with a general warning that a usage issue has occurred.
        /// </remarks>
        protected virtual void OnStatusMessage(MessageLevel level, string status, string eventName = null, MessageFlags flags = MessageFlags.None)
        {
            try
            {
                Log.Publish(level, flags, eventName ?? "ConfigurationLoader", status);

                using (Logger.SuppressLogMessages())
                    StatusMessage?.Invoke(this, new EventArgs<string>(AdapterBase.GetStatusWithMessageLevelPrefix(status, level)));
            }
            catch (Exception ex)
            {
                // We protect our code from consumer thrown exceptions
                OnProcessException(MessageLevel.Info, new InvalidOperationException($"Exception in consumer handler for StatusMessage event: {ex.Message}", ex), "ConsumerEventException");
            }
        }

        /// <summary>
        /// Raises the <see cref="ProcessException"/> event.
        /// </summary>
        /// <param name="level">The <see cref="MessageLevel"/> to assign to this message</param>
        /// <param name="exception">Processing <see cref="Exception"/>.</param>
        /// <param name="eventName">A fixed string to classify this event; defaults to <c>null</c>.</param>
        /// <param name="flags"><see cref="MessageFlags"/> to use, if any; defaults to <see cref="MessageFlags.None"/>.</param>
        /// <remarks>
        /// <see pref="eventName"/> should be a constant string value associated with what type of message is being
        /// generated. In general, there should only be a few dozen distinct event names per class. Exceeding this
        /// threshold will cause the EventName to be replaced with a general warning that a usage issue has occurred.
        /// </remarks>
        protected virtual void OnProcessException(MessageLevel level, Exception exception, string eventName = null, MessageFlags flags = MessageFlags.None)
        {
            try
            {
                Log.Publish(level, flags, eventName ?? "ConfigurationLoader", exception?.Message, null, exception);

                using (Logger.SuppressLogMessages())
                    ProcessException?.Invoke(this, new EventArgs<Exception>(exception));
            }
            catch (Exception ex)
            {
                // We protect our code from consumer thrown exceptions
                Log.Publish(MessageLevel.Info, "ConsumerEventException", $"Exception in consumer handler for ProcessException event: {ex.Message}", null, ex);
            }
        }

        #endregion
    }
}
