#region Using Namespaces...

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.Entity.Validation;
using DataModel.GenericRepository;

#endregion

namespace DataModel.UnitOfWork
{
    /// <summary>
    /// Unit of Work class responsible for DB transactions
    /// </summary>
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region Private member variables...

        private DBModels _context = null;
        private GenericRepository<hmis_user_base> _userRepository;
        private GenericRepository<hmis_role_base> _roleRepository;
        private GenericRepository<hmis_permission_base> _permissionRepository;
        private GenericRepository<Token> _tokenRepository;
        private GenericRepository<hmis_patient_base> _patientbaseRepository;
        private GenericRepository<hmis_patient_ext> _patientextRepository;
        private GenericRepository<hmis_patient_admission_ext> _patientAdmissionExtRepository;
        private GenericRepository<hmis_patient_admission_base> _patientAdmissionBaseRepository;
        private GenericRepository<hmis_patient_operation> _patientOperationRepository;
        private GenericRepository<hmis_department_type_master> _masterDepartmentTypeRepository;
        private GenericRepository<hmis_department_master> _masterDepartmentRepository;
        private GenericRepository<hmis_doctor_master> _masterDoctorRepository;

        private GenericRepository<vw_user_roles> _vwRoleRepository;
        #endregion

        public UnitOfWork()
        {
            _context = new DBModels();
        }

        #region Public Repository Creation properties...

        /// <summary>
        /// Get/Set Property for product repository.
        /// </summary>
        public GenericRepository<hmis_user_base> UserRepository
        {
            get
            {
                if (this._userRepository == null)

                    this._userRepository = new GenericRepository<hmis_user_base>(_context);
                return _userRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for user repository.
        /// </summary>
        public GenericRepository<hmis_role_base> RoleRepository
        {
            get
            {
                if (this._roleRepository == null)
                this._roleRepository = new GenericRepository<hmis_role_base>(_context);
                return _roleRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for token repository.
        /// </summary>
        public GenericRepository<hmis_permission_base> PermissionRepository
        {
            get
            {
                if (this._permissionRepository == null)
                   // this._context.Configuration.LazyLoadingEnabled = false;
                this._permissionRepository = new GenericRepository<hmis_permission_base>(_context);
                return _permissionRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for token repository.
        /// </summary>
        public GenericRepository<Token> TokenRepository
        {
            get
            {
                if (this._tokenRepository == null)
                    this._tokenRepository = new GenericRepository<Token>(_context);
                return _tokenRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for user repository.
        /// </summary>
        public GenericRepository<vw_user_roles> vwRoleRepository
        {
            get
            {
                if (this._vwRoleRepository == null)
                    this._vwRoleRepository = new GenericRepository<vw_user_roles>(_context);
                return _vwRoleRepository;
            }
        }


        /// <summary>
        /// Get/Set Property for Patient repository.
        /// </summary>
        public GenericRepository<hmis_patient_base> PatientBaseRepository
        {
            get
            {
                if (this._patientbaseRepository == null)
                    this._context.Configuration.LazyLoadingEnabled = false;
                this._patientbaseRepository = new GenericRepository<hmis_patient_base>(_context);
                return _patientbaseRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for Patient repository.
        /// </summary>
        public GenericRepository<hmis_patient_ext> PatientExtRepository
        {
            get
            {
                if (this._patientextRepository == null)
                    this._patientextRepository = new GenericRepository<hmis_patient_ext>(_context);
                return _patientextRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for Patient Admission Base Entity Repository.
        /// </summary>
        public GenericRepository<hmis_patient_admission_base> PatientAdmissionBaseRepository
        {
            get
            {
                if (this._patientAdmissionBaseRepository == null)
                    this._context.Configuration.LazyLoadingEnabled = false;
                this._patientAdmissionBaseRepository = new GenericRepository<hmis_patient_admission_base>(_context);
                return _patientAdmissionBaseRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for Patient Admission Extension Entity Repository.
        /// </summary>
        public GenericRepository<hmis_patient_admission_ext> PatientAdmissionExtRepository
        {
            get
            {
                if (this._patientAdmissionExtRepository == null)
                    this._patientAdmissionExtRepository = new GenericRepository<hmis_patient_admission_ext>(_context);
                return _patientAdmissionExtRepository;
            }
        }


        /// <summary>
        /// Get/Set Property for Patient Post Admission - Operation  Entity Repository.
        /// </summary>
        public GenericRepository<hmis_patient_operation> PatientOperationRepository
        {
            get
            {
                if (this._patientOperationRepository == null)
                    this._patientOperationRepository = new GenericRepository<hmis_patient_operation>(_context);
                return _patientOperationRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for Department Type Master Data.
        /// </summary>
        public GenericRepository<hmis_department_type_master> DepartmentTypeMasterRepository
        {
            get
            {
                if (this._masterDepartmentTypeRepository == null)
                {
                    this._context.Configuration.LazyLoadingEnabled = false;
                    this._masterDepartmentTypeRepository = new GenericRepository<hmis_department_type_master>(_context);
                }

                return _masterDepartmentTypeRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for department master data.
        /// </summary>
        public GenericRepository<hmis_department_master> DepartmentMasterRepository
        {
            get
            {
                if (this._masterDepartmentRepository == null)
                {
                    this._context.Configuration.LazyLoadingEnabled = false;
                    this._masterDepartmentRepository = new GenericRepository<hmis_department_master>(_context);
                }                    
                return _masterDepartmentRepository;
            }
        }

         /// <summary>
        /// Get/Set Property for Patient Post Admission - Operation  Entity Repository.
        /// </summary>
        public GenericRepository<hmis_doctor_master> DoctorMasterRepository
        {
            get
            {
                if (this._masterDoctorRepository == null)
                {
                    this._context.Configuration.LazyLoadingEnabled = false;
                    this._masterDoctorRepository = new GenericRepository<hmis_doctor_master>(_context);
                }                   
                return _masterDoctorRepository;
            }
        }
        #endregion

        #region Public member methods...
        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        #endregion

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false; 
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}