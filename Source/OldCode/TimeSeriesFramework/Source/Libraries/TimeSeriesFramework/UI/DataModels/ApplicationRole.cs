﻿//******************************************************************************************************
//  ErrorLog.cs - Gbtc
//
//  Copyright © 2010, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the Eclipse Public License -v 1.0 (the "License"); you may
//  not use this file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://www.opensource.org/licenses/eclipse-1.0.php
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  04/13/2011 - Aniket Salver
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TVA.Data;
using System.Data;

namespace TimeSeriesFramework.UI.DataModels
{
    /// <summary>
    ///  Represents a record of ApplicationRole information as defined in the database.
    /// </summary>
    class ApplicationRole : DataModelBase
    {
        #region [Members]

        //Fileds
        private string m_ID;
        private string m_nodeID;
        private string m_name;
        private string m_description;
        private DateTime m_createdOn;
        private string m_createdBy;
        private DateTime m_UpdatedOn;
        private string m_updatedBy;
        private ObservableCollection<ApplicationRole> m_currentRoleGroups;
        private ObservableCollection<ApplicationRole> m_possibleRoleGroups;
        private ObservableCollection<ApplicationRole> m_currentRoleUsers;
        private ObservableCollection<ApplicationRole> m_possibleRoleUsers;

        #endregion

        #region[Properties]

        /// <summary>
        /// Gets or sets <see cref="ApplicationRole"/> ID.
        /// </summary>
        // Field is populated by database via auto-increment and has no screen interaction, so no validation attributes are applied
        public string ID
        {
            get
            {
                return m_ID;
            }
            set
            {
                m_ID = value;
                OnPropertyChanged("ID");
            }
        }

        /// <summary>
        /// Gets or sets <see cref="ApplicationRole"/> NodeID.
        /// </summary>
        [Required(ErrorMessage = " ApplicationRole Name is a required field, please provide value.")]
        [StringLength(36, ErrorMessage = "ApplicationRole Name cannot exceed 36 characters.")]
        public string NodeID
        {
            get
            {
                return m_nodeID;
            }
            set
            {
                m_nodeID = value;
                OnPropertyChanged("NodeID");
            }
        }

        /// <summary>
        /// Gets or sets <see cref="ApplicationRole"/> Name.
        /// </summary>
        [Required(ErrorMessage = " ApplicationRole Name is a required field, please provide value.")]
        [StringLength(50, ErrorMessage = "ApplicationRole Name cannot exceed 50 characters.")]
        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Gets or sets <see cref="ApplicationRole"/> Description.
        /// </summary>
        // Field is populated by database via auto-increment and has no screen interaction, so no validation attributes are applied
        public string Description
        {
            get
            {
                return m_description;
            }
            set
            {
                m_description = value;
                OnPropertyChanged("Description");
            }
        }

        /// <summary>
        /// Gets or sets <see cref="ApplicationRole"/> CreatedOn.
        /// </summary>
        // Field is populated by database via auto-increment and has no screen interaction, so no validation attributes are applied
        public DateTime CreatedOn
        {
            get
            {
                return m_createdOn;
            }
            set
            {
                m_createdOn = value;
                OnPropertyChanged("CreatedOn");
            }
        }

        /// <summary>
        /// Gets or sets <see cref="ApplicationRole"/> CreatedBy.
        /// </summary>
        // Field is populated by database via auto-increment and has no screen interaction, so no validation attributes are applied
        public string CreatedBy
        {
            get
            {
                return m_createdBy;
            }
            set
            {
                m_createdBy = value;
                OnPropertyChanged("CreatedBy");
            }
        }

        /// <summary>
        /// Gets or sets <see cref="ApplicationRole"/> UpdatedOn.
        /// </summary>
        // Field is populated by database via auto-increment and has no screen interaction, so no validation attributes are applied
        public DateTime UpdatedOn
        {
            get
            {
                return m_UpdatedOn;
            }
            set
            {
                m_UpdatedOn = value;
                OnPropertyChanged("UpdateOn");
            }
        }

        /// <summary>
        /// Gets or sets <see cref="ApplicationRole"/> UpdatedBy.
        /// </summary>
        // Field is populated by database via auto-increment and has no screen interaction, so no validation attributes are applied
        public string UpdatedBy
        {
            get
            {
                return m_updatedBy;
            }
            set
            {
                m_updatedBy = value;
                OnPropertyChanged("UpdatedBy");
            }
        }

        /// <summary>
        /// Gets or sets <see cref="ApplicationRole"/> CurrentRoleGroups.
        /// </summary>
        // Field is populated by database via auto-increment and has no screen interaction, so no validation attributes are applied
        public ObservableCollection<ApplicationRole> CurrentRoleGroups
        {
            get
            {
                return m_currentRoleGroups;
            }
            set
            {
                m_currentRoleGroups = value;
                OnPropertyChanged("CurrentRoleGroups");
            }
        }

        /// <summary>
        /// Gets or sets <see cref="ApplicationRole"/> PossibleRoleGroups.
        /// </summary>
        // Field is populated by database via auto-increment and has no screen interaction, so no validation attributes are applied
        public ObservableCollection<ApplicationRole> PossibleRoleGroups
        {
            get
            {
                return m_possibleRoleGroups;
            }
            set
            {
                m_possibleRoleGroups = value;
                OnPropertyChanged("PossibleRoleGroups");
            }
        }

        /// <summary>
        /// Gets or sets <see cref="ApplicationRole"/> CurrentRoleUsers.
        /// </summary>
        // Field is populated by database via auto-increment and has no screen interaction, so no validation attributes are applied
        public ObservableCollection<ApplicationRole> CurrentRoleUsers
        {
            get
            {
                return m_currentRoleUsers;
            }
            set
            {
                m_currentRoleUsers = value;
                OnPropertyChanged("CurrentRoleUsers");
            }
        }

        /// <summary>
        /// Gets or sets <see cref="ApplicationRole"/> PossibleRoleUsers.
        /// </summary>
        // Field is populated by database via auto-increment and has no screen interaction, so no validation attributes are applied
        public ObservableCollection<ApplicationRole> PossibleRoleUsers
        {
            get
            {
                return m_possibleRoleUsers;
            }
            set
            {
                m_possibleRoleUsers = value;
                OnPropertyChanged("PossibleRoleUsers");
            }
        }

        #endregion

        #region [Static]

        // Static Methods

        /// <summary>
        /// Loads <see cref="ApplicationRole"/> information as an <see cref="ObservableCollection{T}"/> style list.
        /// </summary>
        /// <param name="database"><see cref="AdoDataConnection"/> to connection to database.</param>
        /// <returns>Collection of <see cref="ApplicationRole"/>.</returns>
        public static ObservableCollection<ApplicationRole> Load(AdoDataConnection database)
        {
            bool createdConnection = false;
            try
            {
                createdConnection = CreateConnection(ref database);

                ObservableCollection<ApplicationRole> applicationRoleList = new ObservableCollection<ApplicationRole>();
                DataTable applicationRoleTable = database.Connection.RetrieveData(database.AdapterType, "Select * From ApplicationRole Where NodeID = @nodeID Order By Name");

                foreach (DataRow row in applicationRoleTable.Rows)
                {
                    applicationRoleList.Add(new ApplicationRole()
                    {
                        ID = row.Field<int>("ID").ToString(),
                        Name = row.Field<string>("Name"),
                        Description = row.Field<string>("Description"),
                        NodeID = row.Field<int>("NodeID").ToString(),
                        CreatedOn = row.Field<DateTime>("CreatedOn"),
                        CreatedBy = row.Field<string>("CreatedBy"),
                        UpdatedOn = row.Field<DateTime>("UpdatedOn"),
                        UpdatedBy = row.Field<string>("UpdatedBy")
                    });
                }

                return applicationRoleList;
            }
            finally
            {
                if (createdConnection && database != null)
                    database.Dispose();
            }            
        }

        /// <summary>
        /// Gets a <see cref="Dictionary{T1,T2}"/> style list of <see cref="ApplicationRole"/> information.
        /// </summary>
        /// <param name="database"><see cref="AdoDataConnection"/> to connection to database.</param>
        /// <param name="isOptional">Indicates if selection on UI is optional for this collection.</param>
        /// <returns>Dictionary<int, string> containing ID and Name of application roles defined in the database.</returns>
        public static Dictionary<int, string> GetLookupList(AdoDataConnection database, bool isOptional)
        {
            bool createdConnection = false;
            try
            {
                createdConnection = CreateConnection(ref database);

                Dictionary<int, string> applicationRoleList = new Dictionary<int, string>();
                if (isOptional)
                    applicationRoleList.Add(0, "Select Application Role");

                DataTable applicationRoleTable = database.Connection.RetrieveData(database.AdapterType, "SELECT ID, Name FROM ApplicationRole ORDER BY Name");

                foreach (DataRow row in applicationRoleTable.Rows)
                    applicationRoleList[row.Field<int>("ID")] = row.Field<string>("Name");

                return applicationRoleList;
            }
            finally
            {
                if (createdConnection && database != null)
                    database.Dispose();
            }
        }

        /// <summary>
        /// Saves <see cref="ApplicationRole"/> information to database.
        /// </summary>
        /// <param name="database"><see cref="AdoDataConnection"/> to connection to database.</param>
        /// <param name="applicationRole">Information about <see cref="ApplicationRole"/>.</param>
        /// <param name="isNew">Indicates if save is a new addition or an update to an existing record.</param>
        /// <returns>String, for display use, indicating success.</returns>
        public static string Save(AdoDataConnection database, ApplicationRole applicationRole, bool isNew)
        {
            bool createdConnection = false;
            try
            {
                createdConnection = CreateConnection(ref database);

                if (isNew)
                    database.Connection.ExecuteNonQuery("Insert Into ApplicationRole (Name, Description, NodeID, UpdatedBy, UpdatedOn, CreatedBy, CreatedOn) Values (@name, @description, @nodeID, @updatedBy, @updatedOn, @createdBy, @createdOn)",
                        DefaultTimeout,applicationRole.Name, applicationRole.Description, applicationRole.NodeID, applicationRole.UpdatedBy, applicationRole.UpdatedOn, applicationRole.CreatedBy, applicationRole.CreatedOn);
                else
                    database.Connection.ExecuteNonQuery("Update ApplicationRole Set Name = @name, Description = @description, NodeID = @nodeID, UpdatedBy = @updatedBy, UpdatedOn = @updatedOn Where ID = @id", DefaultTimeout,
                        applicationRole.Name, applicationRole.Description, applicationRole.NodeID, applicationRole.UpdatedBy, applicationRole.UpdatedOn, applicationRole.ID);

                return "Application Role information saved successfully";
            }
            finally
            {
                if (createdConnection && database != null)
                    database.Dispose();
            }
        }

        /// <summary>
        /// Deletes specified <see cref="ApplicationRole"/> record from database.
        /// </summary>
        /// <param name="database"><see cref="AdoDataConnection"/> to connection to database.</param>
        /// <param name="applicationRoleID">ID of the record to be deleted.</param>
        /// <returns>String, for display use, indicating success.</returns>
        public static string Delete(AdoDataConnection database, int applicationRoleID)
        {
            bool createdConnection = false;

            try
            {
                createdConnection = CreateConnection(ref database);

                // Setup current user context for any delete triggers
                CommonFunctions.SetCurrentUserContext(database);

                database.Connection.ExecuteNonQuery("DELETE FROM ApplicationRole WHERE ID = @applicationRoleID", DefaultTimeout, applicationRoleID);

                return "Company deleted successfully";
            }
            finally
            {
                if (createdConnection && database != null)
                    database.Dispose();
            }
        }

        #endregion
    }
}
