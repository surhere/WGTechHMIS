﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBModels : DbContext
    {
        public DBModels()
            : base("name=DBModels")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<hmis_patient_base> hmis_patient_base { get; set; }
        public virtual DbSet<hmis_patient_ext> hmis_patient_ext { get; set; }
        public virtual DbSet<hmis_permission_base> hmis_permission_base { get; set; }
        public virtual DbSet<hmis_user_base> hmis_user_base { get; set; }
        public virtual DbSet<hsmis_role_base> hsmis_role_base { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<hmis_link_role_persmissions> hmis_link_role_persmissions { get; set; }
        public virtual DbSet<hmis_link_user_roles> hmis_link_user_roles { get; set; }
        public virtual DbSet<hmis_user_activity_log> hmis_user_activity_log { get; set; }
        public virtual DbSet<hmis_user_ext> hmis_user_ext { get; set; }
        public virtual DbSet<vw_user_roles> vw_user_roles { get; set; }
        public virtual DbSet<hmis_patient_admission_base> hmis_patient_admission_base { get; set; }
        public virtual DbSet<hmis_patient_admission_ext> hmis_patient_admission_ext { get; set; }
        public virtual DbSet<hmis_patient_operation> hmis_patient_operation { get; set; }
    }
}
