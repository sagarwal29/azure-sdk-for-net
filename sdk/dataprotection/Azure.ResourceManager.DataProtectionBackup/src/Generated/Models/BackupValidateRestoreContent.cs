// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;

namespace Azure.ResourceManager.DataProtectionBackup.Models
{
    /// <summary> Validate restore request object. </summary>
    public partial class BackupValidateRestoreContent
    {
        /// <summary> Initializes a new instance of BackupValidateRestoreContent. </summary>
        /// <param name="restoreRequestObject">
        /// Gets or sets the restore request object.
        /// Please note <see cref="BackupRestoreContent"/> is the base class. According to the scenario, a derived class of the base class might need to be assigned here, or this property needs to be casted to one of the possible derived classes.
        /// The available derived classes include <see cref="BackupRecoveryPointBasedRestoreContent"/>, <see cref="BackupRecoveryTimeBasedRestoreContent"/> and <see cref="BackupRestoreWithRehydrationContent"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"> <paramref name="restoreRequestObject"/> is null. </exception>
        public BackupValidateRestoreContent(BackupRestoreContent restoreRequestObject)
        {
            if (restoreRequestObject == null)
            {
                throw new ArgumentNullException(nameof(restoreRequestObject));
            }

            RestoreRequestObject = restoreRequestObject;
        }

        /// <summary>
        /// Gets or sets the restore request object.
        /// Please note <see cref="BackupRestoreContent"/> is the base class. According to the scenario, a derived class of the base class might need to be assigned here, or this property needs to be casted to one of the possible derived classes.
        /// The available derived classes include <see cref="BackupRecoveryPointBasedRestoreContent"/>, <see cref="BackupRecoveryTimeBasedRestoreContent"/> and <see cref="BackupRestoreWithRehydrationContent"/>.
        /// </summary>
        public BackupRestoreContent RestoreRequestObject { get; }
    }
}
